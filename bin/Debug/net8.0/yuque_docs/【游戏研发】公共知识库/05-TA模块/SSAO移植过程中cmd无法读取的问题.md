

# 一、概述
在UniversalRenderPipeline 14.0.10下对内置ScreenSpaceAmbientOcclusion的移植过程中出现的无法读取CommandBuffer命令的问题

# 二、问题描述
<font style="color:black;">对内置ScreenSpaceAmbient</font>Occlusion功能移植过程中出现了无法正常显示的问题如<font style="color:black;">下（左图）所示</font>

<font style="color:black;">（右图为正确效果）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864260725-4123f67a-0842-45f1-b703-08d4c48f890e.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864267948-71bc1945-718d-4356-863f-643bd7649d29.png)

# 三、分析
### （一<font style="color:black;">）准备工作</font>
<font style="color:black;">打开</font><font style="color:black;">frame Debugger</font><font style="color:black;">发现部分</font><font style="color:black;">SSAO</font><font style="color:black;">功能没有进入渲染流水线</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864383139-244188d9-e594-4688-9baa-2d4ea5efb502.png)

### （二）检测代码逻辑
<font style="color:black;">在AddRenderPasses（）方法打印变量shouldAdd判断ScreenSpaceAmbientOcclusionPass是否进入管线</font>![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864548545-fd8a50a5-603d-4d13-b205-5ae7a361381c.png)

<font style="color:black;">结果未发现问题</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864572785-7bd89e7e-e8c1-40f3-b911-300783178f75.png)

<font style="color:black;">继续对其他关键函数</font><font style="color:black;">Debug.Log(111);</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864896208-42ca3f7f-ce73-4f9a-accb-dfb89aedd748.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718864987738-a87e2afb-40e5-4c5e-8375-6c54fb3c7d23.png)

<font style="color:black;">结果还是未发现问题</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718865005288-1c6c1c93-06c7-4017-aa1a-281f8109ea9c.png)

<font style="color:black;">引用</font><font style="color:black;">RenderObjects</font><font style="color:black;">类调用新的</font><font style="color:black;">pass</font><font style="color:black;">替代</font><font style="color:black;">ScreenSpaceAmbientOcclusionPass</font>

<font style="color:black;">RenderObjects.FilterSettings filter = new RenderObjects.FilterSettings();</font>

<font style="color:black;">定位为CommandBuffer没有调用导致的渲染问题</font>

<font style="color:black;">修改写法如如下</font>

<font style="color:black;"></font>

<font style="color:black;">内置SSAO写法</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718865372818-bcebddb7-eacf-440b-8956-3f3779c9d84b.png)



<font style="color:black;">修改后写法</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1718865075415-5f31b8c4-45a3-4ae3-b89c-18fce5b17211.png)

<font style="color:black;">成功运行</font>

<font style="color:black;">渲染对象</font><font style="color:black;">renderingData</font><font style="color:black;">实例化自</font><font style="color:black;">RenderingData</font><font style="color:black;">类，</font><font style="color:black;">RenderingData</font><font style="color:black;">类内部</font><font style="color:black;">commandBuffer</font><font style="color:black;">对象实例化自</font><font style="color:black;">CommandBuffer</font><font style="color:black;">类，移植后</font><font style="color:black;">CommandBuffer</font><font style="color:black;">类无法直接调用</font><font style="color:black;">CommandBuffer</font><font style="color:black;">命令</font>

<font style="color:black;">需要从</font><font style="color:black;">CommandBufferPool</font><font style="color:black;">类</font><font style="color:black;">Get</font><font style="color:black;">相关渲染命令再</font><font style="color:black;">Release</font>

# 四、总结
<font style="color:black;">传统内置CommandBuffer写法移植后无法成功调用</font>

<font style="color:black;">会导致的相关渲染指令没有执行</font>

<font style="color:black;"></font>

<font style="color:black;"></font>

<font style="color:black;"></font>

