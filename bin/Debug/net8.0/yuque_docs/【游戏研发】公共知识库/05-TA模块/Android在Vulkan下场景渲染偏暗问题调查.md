# 一、概述
Box项目在Anroid真机上场景颜色偏暗，下边是对该问题的调查过程

# 二、问题描述
选择Vulkan API出Android包，进入主界面后，发现主场景整体偏暗，如下（左图）所示，右图为正常效果

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715585890189-246adca8-e33d-42a6-b7eb-6123bf2924b1.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715585913851-7e7c1f42-2e3a-4dbb-86d4-8030d483e41b.png)

# 三、分析
### （一）准备工作
为了方便快速打包测试和对比，先搭建测试环境

1、在Box项目创建了一个空的场景，只放一个Cube随便设置一个颜色，

2、创建一个空的URP工程，搭建一样的场景

如下所示

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715590925087-6a070c0a-76ad-477c-bc35-276231c293cc.png)

### （二）检测代码逻辑
看到颜色变暗，第一时间怀疑是颜色空间转换问题，然后就去检测相关代码和Shader，是不是真机上没有走到对应的逻辑，少了颜色转换

1、先查看`FinalPost.shader`逻辑

如下所示，正常应该走到 `ApplyRCAS`逻辑，怀疑没走到这里，就直接修改成固定的颜色值 half3 color = real3(1,1,1)，然后出包看场景效果，发现修改的颜色生效了，说明Shader走到了这里

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715587082649-ac1b74a1-cbae-4530-a003-6adf113613dd.png)



2、检查`EdgeAdaptiveSpatialUpsampling.shader`逻辑

因为我们项目对EASU做了优化，所以猜这里可能出问题，如下图两个地方

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715591250168-560ce983-b393-48b2-a8d5-3eab754860f4.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715591263357-30db8e04-e538-48f5-a5e3-80dac82ff78f.png)



在这里尝试还原成原来的算法，依然有问题。

在还原代码的时候遇到一个Unity的坑，就是无论你怎么修改`FSRCommon.hlsl`，`FinalPost.shader`一直没有任何变化，就算把RCAS相关方法全部删掉依然不报错，但是`EdgeAdaptiveSpatialUpsampling.shader`这个就能正常生效，后来发现必须得同步修改`FinalPost.shader`，才会刷新。这里怀疑是Unity内部对不同复杂度的shader做了不同的缓存处理。反正这里修改了`FSRCommon.hlsl`，必须得随便打一个回车保存一下`FinalPost.shader`，才能让最新的修改生效



### （三）RenderDoc抓帧对比
通过上边的修改代码和shader并不能定位到问题，然后就通过真机抓帧，对比差异

1、Box项目打包

一开始是使用Vulkan出的安卓包，发现RenderDoc抓帧后无法解析，报错什么不支持Vulkan。然后我就尝试换成OpenGLES打包，结果发现OpenGLES下的包是正常的。

然后就继续打Vulkan的包，看RenderDoc对应的报错，有两个问题造成  
1）连真机通过RenderDoc启动后，需要开启RenderDoc管理文件的权限，有两个RenderDoc都需要开启

2）RenderDoc设置选项问题，根据错误提示修改即可

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715592483670-661b7e60-9d50-486a-97f4-c634fe06c20a.png)



到这里，就可以通过RenderDoc正常的抓Vulkan的包，把数据Save到本地，方便与正常版本对比

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715592619973-3b4b7ffb-8a9e-49ba-945d-bce24e646d92.png)



2、Test项目打包

同样的方式对Test项目打包抓帧，保存帧数据



3、对比最后输出的颜色值

后来又去对比正常情况和异常情况下最后输出的颜色，发现是一样的，如下图所示，输出都是(0.10588, 0.1098, 0.73333, 1)，从这里看出我们的计算输出是没问题，结果完全一致。



**通过对比以下两张图可以发现，虽然看上去效果不一样，但是输出颜色值其实是一样的。这是RenderDoc自身渲染逻辑造成的，所以我们定位问题的时候还是要看具体的颜色值，这样对比才是准确的。**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715589668873-7d6aefbb-ee10-425e-9363-168a67a07435.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715589684536-59265e55-5295-4268-8067-165013292688.png)



4、对比sRGB状态

通过抓帧对比，发现正常情况Input纹理格式是`R8G8B8A8_SRGB`，有问题情况下格式是`R8G8B8A8_UNRM`，然后就怀疑FinalBlit是不是根据不同的纹理格式，内部有什么逻辑决定是否进行线性空间转换

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715590299432-dfa59d97-6d56-4ee4-9b1b-011b8c8aae13.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715588968683-001fc1bf-c72f-4c3c-b997-10a4b3abd4c1.png)

然后就分别在Box项目和Test项目相同的位置打印RT对应的sRGB属性，如下图所示，结果发现打印的都是false，所以这一条也pass



![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715588182134-3e9242cf-74e4-4c2e-9009-06a47d318793.png)





### （四）还原代码
查到这里，依然没能定位到问题，只能用最笨的方法，还原Box项目对URP相关逻辑的改动，注释掉改动的代码，用回原始的逻辑，出包测试依然不行。

然后就继续还原，把放到项目内的URP Package删掉，直接引用Unity原始的URP Package，打包测试，依然有问题！

到目前为止，基本上可以确认不是代码和Shader的问题造成的，然后就开始对比项目设置，最后发现Box项目和Test项目有一个ColorGamut选项有差异，Box多了一个 **DisplayP3**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715590116775-9cfca812-9a7a-4e4a-8ea2-6b6f3eb78e46.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715590146098-1b0495c2-0ed8-40c0-836c-c9b06d6225f2.png)

然后就试着把Box项目的DisplayP3删掉，打包测试，运行正常！到这里可以确定是设置选项DisplayP3的问题！

# 四、总结
该问题是在Vulkan下，并且ColorGamut添加了DisplayP3的情况下出现。



1、调查一开始以为是平台差异造成的代码逻辑执行错误，定位下来并不是

2、然后以为是项目对URP的优化造成的，回退代码发现也不是

3、再就是以为的纹理格式设置问题，结果也不是

排除了以上几个逻辑，再通过对比项目设置的差异，最后发现是色域设置问题。

