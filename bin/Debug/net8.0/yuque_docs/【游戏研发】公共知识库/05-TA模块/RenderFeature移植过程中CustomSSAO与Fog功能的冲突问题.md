# 一、简述
将CustomSSAO与Fog功能的Pass整合进EvironmentAllInOneFeature过程中出现的当CustomSSAO的Pass渲染顺序为AfterRenderingOpaques时出现的Fog失效问题

# 二、问题描述
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720594399490-5c211aee-597c-4759-9b40-0cb1be07ccd9.png)

上图为正常效果，下图为失效效果

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720594404286-b9b97b6b-d713-4b25-b8ef-e14a8a66f760.png)

SSAO Pass的RenderPassEvent为AfterRenderingOpaque时FogPass内的雾效设置不生效，为AfterRenderingPrePasses + 1时雾效能正常生效



# 三、分析
### （一）**基本情况说明：**
CustomSSAO Pass由内置SSAO改写，功能逻辑基本一致

Fog Pass仅起传参作用，为材质shader声明全局参数

EvironmentAllInOneFeature中调用SSAOPass的顺序为根据面板选择AfterRenderingPrePasses + 1和AfterRenderingOpaques，FogPass的顺序为AfterRenderingOpaques，

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720597493487-2f08d5c0-6f22-4973-b641-47ad7da33c21.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1720506459505-5fab7ce0-c13f-456b-a075-b61ffdb336fc.png)

同排序下FogPass的插入顺序在SSAO之后

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720509670222-dcbf7e91-e277-4233-af4f-a9f8368b424c.png)

### 
### （二）检测代码逻辑
### 怀疑一：
因为某种原因导致fog Pass未执行

SetFogParamPass中进行代码调试：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720594084690-b82a666b-838c-445a-bd87-62955af1c643.png)

结果雾效失效时SetFogParamPass中对材质shader进行全局声明的部分跑到了

排除



### 怀疑二：
Artshowcase项目下自带的lightsettingvolume中部分功能与EvironmentAllInOneFeature的冲突导致Fog被覆盖

测试环境搭建：

新建一个工程，搬运EnvironmentVolume所有相关代码，（排除lightsettingvolume）

搭建测试场景

结果：CustomSSAO渲染顺序为AfterRenderingOpaques时，Fog在第一帧失效，后续则正常生效

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720601529946-b19984cc-281e-44a6-9c43-4ce23b4e4974.png)

代码排查结果：

lightsettingvolume中确实存在某种因素影响雾的计算

排查Artshowcase项目有关Fog计算的部分，发现自带MoleVolumeRenderEvent类会自动读取LightSettingVolume类下useFog默认值（false）导致每帧开始前跑到了重置函数SetNoFogParam()

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720597716165-a5e791d7-2cba-4340-9908-2a559ea0ed82.png)

按此梳理原工程中执行逻辑：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720664227323-98465650-97ce-4d09-b86d-2ea48a2046b7.png)

Fog传参的Pass是在Opaque之后，不透明对象都已经渲染完了（里面并没有雾效参数）  
然后到了下一帧开始又被重置，根据正常执行顺序，新工程中由于不受重置影响，第二帧开始正常显示是合理的，但原工程中如果雾效是因为每帧开始被重置导致失效，无论SSAO的执行顺序都应无法生效，当SSAO在AfterRenderingPrePasses+1的时候出现了仍无法解释的雾效已经传参成功，也无法解释为什么SSAO的渲染顺序会对雾效存在影响。

故怀疑某一过程干扰了执行顺序，因为变量中LightSettingVolume与FogPass功能仅为设置全局shader参数，不涉及可能影响排序的内容，故怀疑SSAO Pass中存在某种因素改变了执行顺序



### 怀疑三：
CustomSSAO的Pass的调用顺序导致的某种渲染命令执行的逻辑错误

代码排查：

在Artshowcase中将SSAO的AfterOpaque选项打开使SSAO Pass在AfterRenderingOpaques顺序执行

（fog失效有bug的情形）



SSAO Pass主要由3部分组成（太长了没法全放）

OnCameraSetup():

Execute()：

OnCameraCleanup()：



注释掉OnCameraSetup()内资源初始化和质量分级相关函数：

屏幕直接黑屏 排除

注释掉Execute()：内相关执行函数

Fog生效

注释掉OnCameraCleanup()：内相关函数

无变化，Fog依然失效 ，排除

注释掉其他相关传参函数

无变化，Fog依然失效 ，排除

定位问题到Execute()：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720592954944-6d58885a-1b61-4189-af89-eb8517195fc9.png)

继续按上述方法逐步注释

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720593159446-19338c48-9d88-4806-a33c-afcb31387ebc.png)

注释到context.Submit();时

Fog生效

查阅文档context.Submit();会使得CommandBuffer内渲染命令立刻提交执行

注释掉context.Submit(); 成功

怀疑二没有考虑CommandBuffer的影响

重新梳理逻辑：

          Pass在AfterRenderingPrePasses + 1                            AfterRenderingOpaques   

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720665001487-e0e1661d-209a-47cd-80dd-57493a53e16b.png)

SSAO Pass在AfterRenderingPrePasses + 1顺序下不透明对象渲染命令被传入CommandBuffer延迟生效，然后FogPass立刻生效将材质shader参数传递，使材质shader执行时能渲染出雾效

在AfterRenderingOpaques顺序下不透明对象渲染命令被传入CommandBuffer，然后执行SSAO Pass时CommandBuffer内渲染命令被提前执行（此时材质shader内参数未传递）导致渲染结果使用了仍是一开始被重置的参数列表。



# 四、总结
在 SRP 中使用 ScriptableRenderContext 构建渲染命令列表

CustomSSAO的pass中添加的<font style="color:black;">context.Submit();指令使得</font>CommandBuffer内渲染指令立即被执行，当CustomSSAO渲染顺序在不透明物体之后会导致不透明物体渲染完了Fog的参数还没有传进shader，从而导致雾效失效。



解决方式：

<font style="color:black;">1. 直接删除MoleVolumeRenderEvent类的重置部分代码（即怀疑二），使得AfterRenderingOpaques顺序下shader从第二帧开始也能获取参数（逻辑上第一帧是失效的，第二帧开始渲染效果才正确，因为肉眼无法辨别第一帧失效，最后效果仍正确）</font>

<font style="color:black;">2. 删除context.Submit();命令使得渲染命令正常延迟执行</font>

<font style="color:black;">3. 直接把Fog的pass渲染顺序提前到BeforeRenderingOpaques，在渲染不透明材质前完成传参</font>

<font style="color:black;"></font>

<font style="color:black;">相关阅读：</font>

[https://docs.unity3d.com/Manual/srp-using-scriptable-render-context.html](https://docs.unity3d.com/Manual/srp-using-scriptable-render-context.html)















