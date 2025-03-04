## 问题描述：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723801863356-aa68d393-342c-491e-af06-a79cb6db86ca.png)

如上图，打包到真机后屏幕上出现这种很脏的黑色线条

## 静态分析：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723802479178-43325f80-f01e-4a13-a4bd-f9ea26fc9ff2.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723802491256-48aaef8f-d683-4ebc-919c-6f9150c01d4f.png)

观察得知：这些先无法影响到UI以及半透，得出问题来源在Opaque物体材质或者DrawOpaque到DrawTransparent渲染阶段之间的某个Pass



## Renderdoc截帧分析：
### Step1: 定位问题来源
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723802871655-52b5c448-e761-4fda-84d7-08b1d6f84541.png)

DrawOpaqueObjects阶段，表现正常，排除是材质问题导致，继续往下查



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723802965731-d65481f7-888a-4791-881b-916382d050bd.png)

SSAO阶段，产生了出现问题的黑线，确定问题出现在SSAO Pass

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724033273230-233cc21a-c615-4c2f-a3a9-fc781c793aab.png)

SSAO Pass中还有4个drawcall，一个一个看，发现第一个drawcall就有问题，进入shader调试



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723803188787-72d79be5-d82f-4dd5-874f-a61352dcff35.png)

通过Pipeline State -> FS -> Edit进入片元shader编辑页面（此处SSAO是后效，因此不可能是顶点shader的问题）



![SSAO片元shader](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723805877359-ba8ae700-4e93-4d01-9fba-d1ca13df5651.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723805977318-bdf7d854-6340-4929-8ac7-9e5880722321.png)

发现使用了较低精度的变量，一般碰到类似过曝、变糊等问题，首先怀疑是精度问题，这里同样先从精度开始测试



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723806120035-040c1726-0fff-49df-8e77-188067bf74c5.png)

Ctrl + F 将所有mediump全量替换为highp，然后Apply Changes（需要保持Texture Viewer保持激活状态，否则Apply Changes不会刷新？）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723806233531-b814e5cb-cbfa-4fb7-95ed-0d7e72af8fd6.png)

对比原图：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723806303860-a5d18b40-0f2f-4789-a804-44f8b31e1408.png)

发现条纹状细线消失

切换至SSAO完成后：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723806433927-30a33143-39a3-4609-9d28-1a9fc818a0fe.png)

效果正常



回到shader，逐步测试具体是哪个或哪些参数的精度不足导致的问题

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724033838579-db79c99a-069c-44cd-a3a7-50a25d1b91b9.png)

最终定位到是 u_xlat16_3 和 u_xlat16_13这两个参数的问题

shader打包出去后是不会保留原有参数名的，因此我们依旧无法得知该参数具体是什么，继续定位出现问题的参数



### Step2: 筛选问题参数，逆向问题源头
**u_xlat16_3：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724045416085-775ab293-d883-43a4-9691-821cb5996777.png)

勾选 Match whole word 精确筛选参数（避免在搜索u_xlat16_3时搜到类似u_xlat16_30这样的参数）

**u_xlat16_13：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724034564337-81e2c444-9c4d-40db-a452-07c7188e836c.png)

GLSL中全局变量的名称会正常保留，因此通过shader中的全局变量可以帮助定位到变量的位置所在（vulkan不显示全局变量，因此打包时需要打OpenGL包）



由易入难，先从使用量较少的 u_xlat16_3 入手

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724052665199-64d3b464-ca21-496f-ac06-df38ba9082c4.png)

观察到与u_xlat16_3相关的计算中有 ** _CameraViewXExtent 和 _CameraViewTopLeftCorner这两个全局变量**，回到项目中全局查找“_CameraViewXExtent”的使用情况

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724055081209-f202a388-e7ba-4afd-aa70-81dca339899c.png)

整体使用情况不多，符合数组[]条件的只有两处SSAO.hlsl，依据hlsl路径判断：上方hlsl是shadergraph使用的，跳过，因此，实际使用的应是下方的SSAO.hlsl



相关代码位置：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724057678297-acf08895-bcc5-4ca6-bdd0-b5ff81261498.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724057723049-f57cc63a-a582-4d8c-81a0-fe77db256a72.png)

_CameraViewXExtent[]在SSAO.hlsl中只有上图中一处使用，上方的计算与Renderdoc也能够吻合，因此仅需找到何处给这个函数的uv传了值



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724060489466-4434fbb9-023d-4816-8b4b-123c3a7d32eb.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724060543314-71ad266a-ab55-4be2-a03c-3b5da152b359.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724060582070-65514689-7129-448e-8e3a-72fb8e4c2a2a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724060597953-e7de8118-d397-43b3-a4c5-c52082f784da.png)

不幸的是，很多地方都用到了这个函数

回到Renderdoc，找到 u_xlat16_3 的上下文

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724060913655-89acf474-1bf7-4fba-9a31-3451da5a0ddd.png)

比较显著的特点是，参数上方有大量的if判断，包括 u_xlat16_3 参数本身其实也是通过if得到的，经过代码比较：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724060489466-4434fbb9-023d-4816-8b4b-123c3a7d32eb.png?x-oss-process=image%2Fformat%2Cwebp)

只有图一符合条件，因此 u_xlat16_3 实际为P2，且P2为 half3半精度类型，符合之前测试得出的问题来源于精度问题的结论

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724065960325-18b48c80-88db-4d25-ad56-c9c2fee6af60.png)

<font style="color:#585A5A;">（题外话：可以观察到GLSL中对if中的两个分支实际都走到了，所以shader中对if的执行逻辑是TF两边都走完，然后再择一使用，因此if的实际开销是两个分支之和加一条if逻辑判断（if判断在GPU上也很贵），因此应尽量避免shader中if的使用，要用是也尽量转成lerp之类的方法）</font>



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724066494466-fd28900b-7414-493a-a4f3-40f9b5a14f37.png)

注意到，函数实际执行了两次，还有一个参数P1应该同样有精度问题，回到Renderdoc

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724067018460-34f24511-2600-4a22-b361-30315eb0b689.png)

另一个问题变量 u_xlat16_13 前面的计算逻辑与 u_xlat16_3 是完全一致的，因此， u_xlat16_13 实际就是P1

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724067154213-b96f0057-4f0e-47e1-a60b-b1c3a4892a4b.png)

将P1,P2由半精度half3改为全精度float3，并去除中间计算过程中的半精度修饰，重新打包

![](https://cdn.nlark.com/yuque/0/2024/jpeg/45354151/1724067282571-d07bc2f9-0c68-43de-933a-ffaabbb9a580.jpeg)

条纹消失，BUG排除



### 编译器的优化：
上方分析其实没有解释 u_xlat16_13 后面的计算都是在干什么，包括如果观察仔细的话其实GLSL编译出来的代码和源代码其实有部分是对不上的，这些都是由于shader编译后的优化

例：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724067538912-32e3bba3-d141-421e-bbc7-0cf1f53d8846.png)

 u_xlat16_13 其实在执行完最开始的3条指令后就没有作用了，从第一个红框开始他只是作为中间变量在使用，编译器自动将其废物利用了（所以Debug时尽量从前往后找问题）



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724068208532-c95bd446-f0ae-4204-b983-02bf6890a51d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724068070872-e7a9c705-5703-4e6f-b3a4-1529d37e034c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724068339361-282095c2-f09b-4a9d-b492-4945cdca46bc.png)

另比如：shader代码中的float参数在glsl中变成了二维的vec2，但实际是因为函数要走两次，zScale要用两次，但编译器做了优化把zScale两次的运算优化到了一次（vec4 * vec4 与 float * float在GPU上的开销是相同的），所以部分代码对不上（主要是通道）是正常现象



## 踩坑记录：
### 打包篇：
项目安卓打包文档链接： https://snh48group.yuque.com/rtukm5/project_doc/ttrebgc3egs7ui8p  



个人提示：

1. Lua环境明明配置正确，但始终显示配置不上。  解决方法：重启电脑
2. Gradle build failed

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724068933089-d3b57230-cb2c-4c11-b3b6-92764faa330c.png)

实际是gradle有东西没下下来。  解决方法：关闭梯子，重新打包

3. 按照说明文档全部配置正确，但仍报错。  解决方法：检查项目路径是否包含空格，有空格它就打不了
4. 真机Debug打包时，Bulid Settings -> Player Settings -> Graphics APIs 将Vulkan去掉或者挪到OpenGL下面，否则默认打的是Vulkan包，不好Debug

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1724069408938-2917be6a-de02-419e-ae0c-19581a92c210.png)



### Renderdoc篇：
**比较玄学，仅供参考**

1. 多备测试机，有时候抓帧抓不到可能不是包的问题，是手机的问题，换台就好了...
2. 帧过大可能导致在真机上无法打开（理论上不应该，但它确实发生了，400多M的帧在真机调试时有API报错，回项目内移除大部分模型，重新打包抓帧，帧大小降至90多M，然后就能开了，当然也有可能是某个材质有问题，然后正好被移除了）



