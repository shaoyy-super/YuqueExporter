## <font style="color:rgb(34, 34, 34);">RenderDoc介绍：</font>
+ <font style="color:rgb(34, 34, 34);">优化GPU</font>
+ <font style="color:rgb(34, 34, 34);">逆向其他游戏</font>
+ <font style="color:rgb(34, 34, 34);">调试Shader</font>

## <font style="color:rgb(34, 34, 34);">准备环境：</font>
### <font style="color:rgb(34, 34, 34);">1.</font>**<font style="color:rgb(34, 34, 34);">下载地址</font>****<font style="color:rgb(101, 123, 131);">:</font>****<font style="color:rgb(34, 34, 34);">https://renderdoc.org/</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331273050-c7f9249c-9e27-4cbb-a4ee-b957e5f03748.png)
### <font style="color:rgb(34, 34, 34);">2.</font>**<font style="color:rgb(34, 34, 34);">下载木木模拟器（64位的模拟器，32位的不可以</font>**
<font style="color:rgb(34, 34, 34);">如果已经安装了，可以查看是不是64位的</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331305862-3dd75521-ecf4-47db-8825-2eb722969790.png)

### <font style="color:rgb(34, 34, 34);">3.配置RenderDoc的环境</font>
<font style="color:rgb(34, 34, 34);">1.设置环境变量：RENDERDOC_HOOK_EGL=0（这个也可以不需要设置），设置这个环境变量的目的是</font><font style="color:rgb(68, 68, 68);">为了防止 RenderDoc 把模拟器里面实现的 GLES API 给 Hook 了的同时还 Hook 了 DirectX 造成冲突。</font>

<font style="color:rgb(34, 34, 34);">在Windows上启动 RenderDoc它`将会捕获本地的 OpenGL ES 并忽略其他应用对任何OpenGL ES 底层 API 调用。对于使用调用 D3D11 在 Windows 上模拟 GLES 的 ANGLE 等库，这意味着 GLES 本身会被捕获和调试。一般不希望发生这种情况，希望捕获期间忽略 OpenGL ES，则可以将</font><font style="color:rgb(101, 123, 131);background-color:rgb(245, 245, 245);">RENDERDOC_HOOK_EGL</font><font style="color:rgb(34, 34, 34);">环境变量设置为</font><font style="color:rgb(101, 123, 131);background-color:rgb(245, 245, 245);">0</font>

<font style="color:rgb(34, 34, 34);">2.在 RenderDoc Tools->Settings->General 里面找到 Allow global process hooking 并勾选</font>

<font style="color:rgb(34, 34, 34);">一般情况下我们调试抓帧，直接只需要，监听对应的exe执行文件即可，然后Launch即可。</font>

<font style="color:rgb(34, 34, 34);">如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331364071-92c45816-345f-4d69-b245-5fd5d9c2d30d.png)



<font style="color:rgb(34, 34, 34);">但是，我们现在运行的是模拟器游戏，无法直接执行对应的执行文件，所以采用这种比较间接的方式，我们hook住的是模拟器的核心。</font>

<font style="color:rgb(34, 34, 34);">勾选此选项将插入一个全局钩子，该钩子会导致每个新创建的进程加载一个非常小的 shim dll。shim dll 将加载，创建一个线程来检查进程是否与指定的路径或文件名匹配，然后卸载。如果进程匹配，它也将注入 RenderDoc 并且捕获将继续正常进行。</font>

<font style="color:rgb(34, 34, 34);"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331388761-62179b28-9ef8-4a86-a8ea-ae89c9d9c504.png)



<font style="color:rgb(34, 34, 34);">3.配置AndroidSDK和JDK环境变量</font>

<font style="color:rgb(34, 34, 34);">RenderDoc中： Tools->Settings</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331403057-d874d86e-b030-40e0-86ec-59f7c4874a1e.png)



### <font style="color:rgb(34, 34, 34);">4.模拟器设置</font>
<font style="color:rgb(34, 34, 34);">选择OpenGL模式，这样可以看到Shader的内容，会编译成GLSL Shader</font>

<font style="color:rgb(34, 34, 34);">如果选择的是DirectX 渲染模式，无法进行抓帧（启动模拟器的时候会失败，无法启动成功，目前我这边的机器是这样，不确定是不是显卡级别太低）</font>

<font style="color:rgb(34, 34, 34);">不需要开启模拟器的root权限</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331424405-a82ff54d-759d-4deb-b749-22b438bf1556.png)

## <font style="color:rgb(34, 34, 34);">RenderDoc连接模拟器抓</font>
### <font style="color:rgb(34, 34, 34);">第一步：启动RenderDoc，如下图所示</font>
**<font style="color:rgb(34, 34, 34);">选择模拟器的核心</font>**<font style="color:rgb(34, 34, 34);">：C:\Program Files\NemuVbox\Hypervisor\NemuHeadless.exe</font>

**<font style="color:rgb(34, 34, 34);">查找模拟器核心文件的方式：</font>**<font style="color:rgb(34, 34, 34);">一般是一个叫 XXXHeadLess.exe 的文件，也可能是其他的。找到的方法很简单，模拟器里面随便运行一个手游，然后任务管理器里面按照 CPU 使用排序，排在最前面的就是，右键点击之，选择打开文件所在位置。就可以找到核心文件的位置。</font>

<font style="color:rgb(34, 34, 34);">在 RenderDoc里 的 Launch Application 页面里面。Executable Path 选择刚才找到的模拟器核心（最新的Mumu12的执行文件是C:\Program Files\MuMu\emulator\MuMuPlayer-12.0\shell\MuMuPlayer.exe）。然后在下面 Global Process Hook 里面点 Enable Global Hook，如果提示需要 Administrator 启动就确定以后再点 Enable Global Hook 按钮。</font>

**<font style="color:rgb(34, 34, 34);">如下图所示：</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331446213-ef219201-e65c-4b67-b51f-bcc381a1b15b.png)



### <font style="color:rgb(34, 34, 34);">第二步：启动模拟器</font>
<font style="color:rgb(34, 34, 34);">出现如下图所示，表示连接成功，否则反复打开模拟器（关闭模拟器保证关闭成功，保证模拟器进程强制关闭）</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331468883-bff0d314-0164-4724-b5f5-d4b158ce25d5.png)

### <font style="color:rgb(34, 34, 34);">第三步：在RenderDoc做如下操作，如下图所示：</font>
<font style="color:rgb(34, 34, 34);">RenderDoc File 菜单 Attach to Running Instance , 在 localhost 下面可以看到模拟器核心程序，选中并点击 Connect to app ，之后就正常抓帧即可。</font>

**<font style="color:rgb(34, 34, 34);">File->Attach to Running Instance</font>**<font style="color:rgb(34, 34, 34);"> 如下：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331482962-909de6c4-5e8a-4ae6-9f03-e8fff4338ed0.png)



**<font style="color:rgb(34, 34, 34);">显示如下图所示表示连接成功</font>**<font style="color:rgb(34, 34, 34);">：RenderDoc 连接到模拟器</font>

<font style="color:rgb(34, 34, 34);">注意：Mumu12这里也是选择MenuHeadless，不选择player</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331498016-f0966e00-ec68-4cb8-8fa1-10d0c5b85f9e.png)

**<font style="color:rgb(34, 34, 34);">点击Connect to Appt弹出如下界面:</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331511033-f7c2af77-a450-41c0-bd4a-88f75e713a31.png)



**<font style="color:rgb(34, 34, 34);">在模拟器中按下F12或者Capture Frame Immediately进行抓帧</font>**

## <font style="color:rgb(34, 34, 34);">RenderDoc常用功能介绍：</font>
<font style="color:rgb(34, 34, 34);">RenderDoc各个窗口功能：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331523505-c4c9a421-044e-4f4e-9160-9d9a6e57cecf.png)



<font style="color:rgb(34, 34, 34);">只介绍一些我们需要的窗口：</font>

### <font style="color:rgb(34, 34, 34);">Launch Application 窗口：启动窗口</font>


![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331539605-c1012414-c6bb-4a2a-a7ce-9db3b1b123c0.png)



### <font style="color:rgb(34, 34, 34);">MeshViewer: Mesh视图</font>
<font style="color:rgb(34, 34, 34);">在Mesh Viewer中可以查看Mesh的顶点数据等</font>

<font style="color:rgb(34, 34, 34);">同时也可以保存数据，导出成CSV格式或者Bytes数据，通过其他工具转换成Fbx文件</font>

<font style="color:rgb(34, 34, 34);">CSV数据可以通过CSVConvert工具转换成Fbx数据。</font>

<font style="color:rgb(34, 34, 34);">如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331551374-44aef456-18c3-4008-b743-fd51fa980e95.png)



### <font style="color:rgb(34, 34, 34);">TextureViewer: 贴图视图</font>
<font style="color:rgb(34, 34, 34);">右侧的Inputs:输入贴图，可以导出保存Outputs:输出的贴图</font>

<font style="color:rgb(34, 34, 34);"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331564478-0f03308d-5530-4ffd-bc43-8d6276b6192f.png)



**<font style="color:rgb(34, 34, 34);">选中右侧的输入贴图，点击保存，可以把贴图导出，具体的格式可以在导出的时候设置</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331576913-bb9950d5-95eb-4c2f-a5ee-66ec9d8af10a.png)



### <font style="color:rgb(34, 34, 34);">Event Brose : 事件浏览</font>
<font style="color:rgb(34, 34, 34);">可以看到每一个DrawCall，选择对应的DrawCall绘制命令，可以看到对应的信息</font>

<font style="color:rgb(34, 34, 34);">当我们选中某一个DrawCall命令，可以查看TextureView ,MeshView,PipelineState等，查看具体的信息。</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331595876-ddc19e21-690b-49fb-9d6c-e64708557d59.png)



### <font style="color:rgb(34, 34, 34);">PipelineState:管线</font>
<font style="color:rgb(34, 34, 34);">如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331611171-58f09fad-f2c9-4f6a-bfb6-c58b487c072a.png)

<font style="color:rgb(34, 34, 34);">下面具体介绍每个阶段的内容：</font>

**<font style="color:rgb(34, 34, 34);">VTX阶段：</font>**<font style="color:rgb(34, 34, 34);">可以查看顶点着色器的输入属性有哪些</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331620325-30b04fa9-d51c-4d1e-91d7-bef901616014.png)



**<font style="color:rgb(34, 34, 34);">VS阶段：</font>**

<font style="color:rgb(34, 34, 34);">顶点着色器阶段，我们可以通过下面的一些操作来查看，编辑，保存GLSL Shader，</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331631220-ccceaff5-ba6d-4f55-93ca-67c37b1ecfcd.png)

**<font style="color:rgb(34, 34, 34);">曲面细分着色器（Tessellation Shader）</font>**

<font style="color:rgb(34, 34, 34);">曲面细分着色器（Tessellation Shader）是一个可选的着色器，它用于细分图元。</font>

**<font style="color:rgb(34, 34, 34);">TCS阶段:</font>**<font style="color:rgb(34, 34, 34);">(</font><font style="color:rgb(73, 73, 73);background-color:rgb(244, 237, 227);">TESS Control Shader</font><font style="color:rgb(73, 73, 73);background-color:rgb(244, 237, 227);"> </font><font style="color:rgb(34, 34, 34);">)（曲面细分中的一个Shader，OpenGL 4.0以后才有的曲面细分Shader）</font>

**<font style="color:rgb(34, 34, 34);">输入</font>**<font style="color:rgb(34, 34, 34);">：Patch，一个Patch可以看成是多个顶点的集合。它包括每个顶点的属性（坐标，颜色，纹理坐标等）。用户可以指定一个patch里面要包含几个顶点。同时，一个patch还可以有自己的属性，该属性被它内部的所有顶点共有，即这些顶点只有一套patch属性，而不是每个顶点拥有一个自己的patch属性。</font>

**<font style="color:rgb(34, 34, 34);">输出</font>**<font style="color:rgb(34, 34, 34);">：Patch ， gl_TessLevelOuter ， gl_TessLevelInner。</font>

**<font style="color:rgb(34, 34, 34);">功能</font>**<font style="color:rgb(34, 34, 34);">：TCS会根据需求把Patch自己的属性以及它内部的顶点属性做一些修改。然后输出Patch。当然，它也可以不做任何修改，直接传给后面的shader。我们知道Tessellation（曲面细分）的作用就是把一个图元分割成很多图元，比如把一个三角形分割成很多更小的三角形。因此，在分割的时候我们得要知道这个三角形的每个边要被分割成多少段，然后在三角形内部，我们还要怎么继续分割，这两个紫色的内容就是存储在 gl_TessLevelOuter 和gl_TessLevelInner。TCS可以根据需要设置这两个值。所以，TCS的主要作用是设置Patch以及它内部顶点的属性。同时也是最重要的，设置图元接下来被细分的度。（TCS不做分割动作）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331647651-d1474e84-d3c5-43cf-941d-ccc24a375860.png)



<font style="color:rgb(34, 34, 34);">gl_TessLevelOuter[0]表示要生成几条曲线。这里我们选择1条。</font>

<font style="color:rgb(34, 34, 34);">gl_TessLevelOuter[1] 表示将曲线细分成几段。这个很是决定细分程度的关键参数。我们分别使用1、8、32测试。</font>

<font style="color:rgb(34, 34, 34);">三角形分割的图片:</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331660736-19611952-47b1-462f-acba-f75073566242.png)

**<font style="color:rgb(34, 34, 34);">TES阶段:</font>**<font style="color:rgb(34, 34, 34);">(</font><font style="color:rgb(73, 73, 73);background-color:rgb(244, 237, 227);">TESS Evaluation Shader</font><font style="color:rgb(73, 73, 73);background-color:rgb(244, 237, 227);"> </font><font style="color:rgb(34, 34, 34);">)（曲面细分中的一个Shader）</font>

**<font style="color:rgb(34, 34, 34);">输入</font>**<font style="color:rgb(34, 34, 34);">：一系列顶点。这些顶点是三角形被分割后产生的新顶点。下面是每个TES程序都必须有的一段代码：</font>

<font style="color:rgb(34, 34, 34);">layout( triangles, fractional_odd_spacing, ccw ) in;</font>

<font style="color:rgb(34, 34, 34);">它表示TES的输入是三角形（当然你也可以写成其他类型的图元）</font>

**<font style="color:rgb(34, 34, 34);">输出</font>**<font style="color:rgb(34, 34, 34);">：也是一系列顶点。</font>

**<font style="color:rgb(34, 34, 34);">功能</font>**<font style="color:rgb(34, 34, 34);">：其实在TCS与TES之间有个过程叫</font>**<font style="color:rgb(34, 34, 34);">Tessellation Primitive Generator（简称TGP）</font>**<font style="color:rgb(34, 34, 34);">，它首先会去查看TES的</font>**<font style="color:rgb(34, 34, 34);">输入</font>**<font style="color:rgb(34, 34, 34);">是什么，哦，它要三角形。那么，TGP就会把TCS传入的Patch内部的顶点看成是若干个三角形（注意Patch内部的顶点不一定只有三个）。然后，TGP每次从当前Patch里面取出三个顶点做一个三角形的分割，直到Patch里面的顶点全部被取出。</font>

<font style="color:rgb(34, 34, 34);">每个三角形具体怎么被分割呢？</font>

<font style="color:rgb(34, 34, 34);">其实，gl_TessLevelOuter 和gl_TessLevelInner会被传入给TGP。它们的作用就被体现出来。这就是为什么我前面说的TCS不做分割，只计算分割的度。（注意TGP不是shader，它只是pipeline里面的一个状态而已）</font>

**<font style="color:rgb(34, 34, 34);">TES的功能：</font>**<font style="color:rgb(34, 34, 34);">其实TGP传入的顶点的坐标值并不是世界坐标值，而是一个三角形内部的坐标表示形式，三角形顶点上有坐标的，TGP然后根据这个坐标去计算内部新成立的顶点在该局部坐标系内部的坐标。因此，TES就是要把每个顶点的局部坐标变换成世界坐标，以及把顶点相应属性（颜色，纹理坐标等）转换成真正且有效的属性值。每处理一个顶点就输出一个顶点。</font>

<font style="color:rgb(34, 34, 34);">注意：TES并不知道这些顶点会被组成什么图元，它只要求TGP把patch内部的顶点当成什么图元去分割。TES和VS一样，输入是顶点，输出也是顶点。在TES后面有个图元装配的过程，它会根据TES的输入（看上面的那行代码），转换成相应的图元。这里图元装配器会把TES输出的顶点装配成一个一个的三角形。</font>

<font style="color:rgb(34, 34, 34);">曲面细分Shader具体的可以参考：</font>[https://www.cnblogs.com/zenny-chen/p/4280100.html](https://www.cnblogs.com/zenny-chen/p/4280100.html)

**<font style="color:rgb(34, 34, 34);">GS阶段:</font>**<font style="color:rgb(34, 34, 34);">几何着色器(Geometry Shader)</font>

<font style="color:rgb(34, 34, 34);">Geometry Shader是一个可选的着色器，可可以被用于执行逐图元的着色操作，或者被用于产生更多的图元。</font>

**<font style="color:rgb(34, 34, 34);">RS:(Rasterizer</font>**<font style="color:rgb(34, 34, 34);">光栅化)</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331675613-19fdb193-4da3-4e6b-a0b6-b27aa0d3b61e.png)



<font style="color:rgb(34, 34, 34);">在这里一般我们关注 Cull Mode</font>

**<font style="color:rgb(34, 34, 34);">FS阶段:</font>**<font style="color:rgb(34, 34, 34);">Fragment Shader(片段着色器)</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331686174-922ea7d6-602e-4306-8e4c-eb2cfebbb221.png)

<font style="color:rgb(34, 34, 34);">片段着色器，我们一般可以看到那些贴图采样，以及贴图的格式信息，尺寸信息等</font>

**<font style="color:rgb(34, 34, 34);">FB阶段</font>**<font style="color:rgb(34, 34, 34);">：FrameBuffer(颜色缓存)</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331696551-c39e7220-a2e2-491d-b739-dcc1d2bb29f3.png)



<font style="color:rgb(77, 77, 77);">这个窗口主要关注Alpha混合（是否开启，以及混合模式等），Z-Test（深度测试是否开启，测试函数等等），Stencil Test（模板测试）等</font>

## <font style="color:rgb(34, 34, 34);">模拟器真实抓帧反编译Shader</font>
<font style="color:rgb(34, 34, 34);">模拟器运行一个使用Unity开发的游戏，我找的游戏是《最终幻想：勇气启示录 幻影战争》</font>

<font style="color:rgb(34, 34, 34);">按照上述流程，开始模拟器抓帧</font>

### **<font style="color:rgb(34, 34, 34);">第一步：</font>**<font style="color:rgb(34, 34, 34);">在Event Browser中找到对应的DrawCall绘制命令</font>
<font style="color:rgb(34, 34, 34);">以中间的模型为例：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331709687-3cfa826b-9f4c-4a01-9ad0-e9f8c16d341f.png)



### <font style="color:rgb(34, 34, 34);">第二步：找到Texture View，导出对应的贴图数据</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331720747-74db51eb-e8f3-410e-99f0-8664e09f7856.png)
### <font style="color:rgb(34, 34, 34);">第三步：找到MeshView，导出Mesh数据</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331732430-db55ac0d-6daa-4b7c-841b-e84e957f951b.png)
**<font style="color:rgb(34, 34, 34);">CSV转化成FBX文件，网上有很多工具，可以下载，如下图所示：</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331744019-93690150-68a9-4ce3-aeca-6940e1a42737.png)



### <font style="color:rgb(34, 34, 34);">第四步：导出Vertex Shader 和 Fragment Shader(这个Shader是GLSL Shader)如下图所示：</font>
**<font style="color:rgb(34, 34, 34);">查看Shader:</font>**

```plain
#version 310 es 

 precision highp atomic_uint;


uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4]; // 矩阵 mat4*4 4X4矩阵 当前模型的矩阵用于将顶点转化到世界空间
uniform 	vec4 hlslcc_mtx4x4unity_MatrixV[4];//观察矩阵 用于将顶点转化到观察空间
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];//观察投影矩阵 用于将顶点转化到裁剪空间
uniform 	vec4 _MainTex_ST;
uniform 	mediump vec3 _FogParam; //雾效参数

//顶点属性 -对应shader中的struct a2v
in highp vec4 in_POSITION0;
in highp vec3 in_NORMAL0;
in highp vec2 in_TEXCOORD0;

//输出属性 -对应shader中的struct v2f
out highp vec4 vs_TEXCOORD4;
out highp vec3 vs_TEXCOORD5;
out mediump vec4 vs_TEXCOORD6;
out highp vec2 vs_TEXCOORD0;
out mediump float vs_TEXCOORD7;

vec4 u_xlat0;
bool u_xlatb0;
vec4 u_xlat1;
mediump float u_xlat16_2;
float u_xlat9;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    
    //顶点的世界坐标
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    
    //转化成世界坐标系内的缩放，旋转或则平移
    vs_TEXCOORD4 = hlslcc_mtx4x4unity_ObjectToWorld[3] * in_POSITION0.wwww + u_xlat0;
    
    u_xlat0 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat0;
    // 上面的代码是 坐标变化 局部坐标变换到裁剪空间
    //相当于 o.vertex = UnityObjectToClipPos(v.vertex);


   //把法线转化到世界坐标系下
    u_xlat0.xyz = in_NORMAL0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
    u_xlat0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat0.xyz;
    u_xlat0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat0.xyz;
   
    u_xlat9 = dot(u_xlat0.xyz, u_xlat0.xyz); //点乘
    u_xlat9 = inversesqrt(u_xlat9); //开平方根的倒数
    vs_TEXCOORD5.xyz = vec3(u_xlat9) * u_xlat0.xyz; // 归一化

    vs_TEXCOORD6 = vec4(0.0, 0.0, 0.0, 0.0);

   //贴图采样
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;

    u_xlat0.x = u_xlat1.y * hlslcc_mtx4x4unity_MatrixV[1].z;
    u_xlat0.x = hlslcc_mtx4x4unity_MatrixV[0].z * u_xlat1.x + u_xlat0.x;
    u_xlat0.x = hlslcc_mtx4x4unity_MatrixV[2].z * u_xlat1.z + u_xlat0.x;
    u_xlat0.x = hlslcc_mtx4x4unity_MatrixV[3].z * u_xlat1.w + u_xlat0.x;

    u_xlat16_2 = (-u_xlat0.x) + (-_FogParam.y);
    u_xlat16_2 = u_xlat16_2 * _FogParam.z;

#ifdef UNITY_ADRENO_ES3
    u_xlat16_2 = min(max(u_xlat16_2, 0.0), 1.0);
#else
    u_xlat16_2 = clamp(u_xlat16_2, 0.0, 1.0);
#endif
//把u_xlat16_2 的值限定在[0,1]之间，类似 saturate(x)

#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(0.0<_FogParam.x);
#else
    u_xlatb0 = 0.0<_FogParam.x;
#endif
    vs_TEXCOORD7 = (u_xlatb0) ? u_xlat16_2 : 0.0;
    return;
}
```

**<font style="color:rgb(34, 34, 34);">顶点Shader:(GLSL)，同时可以查看顶点Shader的参数</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331795891-01aef9cb-a0bf-4321-a85a-bb8cea92020f.png)

**<font style="color:rgb(34, 34, 34);">片段Shader:</font>**

```plain
#version 310 es

 precision highp atomic_uint;


precision highp float;
precision highp int;
uniform 	vec3 _WorldSpaceCameraPos;
uniform 	vec4 hlslcc_mtx4x4unity_MatrixV[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MaskTex_ST;
uniform 	mediump vec4 _MainColor;
uniform 	mediump vec3 _DirectLightColor;
uniform 	mediump float _RimScale;
uniform 	mediump vec2 _RimExp;
uniform 	mediump vec3 _RimColor;
uniform 	mediump float _SpecularPower;
uniform 	mediump vec4 _ColorMod;
uniform 	mediump vec4 _ColorFade;
uniform 	mediump float _CrystalFade;
uniform 	mediump float _CrystalFadePow;
uniform 	mediump vec3 _FogParam;
uniform 	mediump vec3 _FogColor;
uniform mediump sampler2D _MainTex;
uniform mediump sampler2D _MaskTex;
uniform mediump sampler2D _NoiseTex;
in highp vec4 vs_TEXCOORD4;
in highp vec3 vs_TEXCOORD5;
in mediump vec4 vs_TEXCOORD6;
in highp vec2 vs_TEXCOORD0;
in mediump float vs_TEXCOORD7;
layout(location = 0) out mediump vec4 SV_Target0;
vec3 u_xlat0;
mediump float u_xlat16_0;
bool u_xlatb0;
mediump vec3 u_xlat16_1;
vec3 u_xlat2;
mediump vec4 u_xlat16_2;
mediump vec3 u_xlat16_3;
vec2 u_xlat4;
mediump float u_xlat16_4;
mediump vec3 u_xlat16_5;
mediump vec2 u_xlat16_9;
float u_xlat12;
mediump float u_xlat16_13;
void main()
{
    u_xlat0.xyz = (-vs_TEXCOORD4.xyz) + _WorldSpaceCameraPos.xyz;
    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat12 = inversesqrt(u_xlat12);
    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
    u_xlat0.x = dot(vs_TEXCOORD5.xyz, u_xlat0.xyz);
#ifdef UNITY_ADRENO_ES3
    u_xlat0.x = min(max(u_xlat0.x, 0.0), 1.0);
#else
    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
#endif
    u_xlat0.x = (-u_xlat0.x) + 1.0;
    u_xlat16_1.x = log2(u_xlat0.x);
    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(0.5, 1.0);
    u_xlat0.xy = u_xlat0.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
    u_xlat16_0 = texture(_MaskTex, u_xlat0.xy).x;
    u_xlat16_5.x = u_xlat16_0 * 6.0;
    u_xlat16_5.x = u_xlat16_1.x * u_xlat16_5.x;
    u_xlat16_5.x = exp2(u_xlat16_5.x);
    u_xlat16_5.x = u_xlat16_5.x * 1.5;
    u_xlat16_5.x = u_xlat16_0 * u_xlat16_5.x;
    u_xlat16_1.y = u_xlat16_5.x * _RimScale;
    u_xlat16_9.xy = vs_TEXCOORD0.xy * vec2(0.5, 1.0) + vec2(0.5, 0.0);
    u_xlat0.xy = u_xlat16_9.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
    u_xlat16_0 = texture(_MaskTex, u_xlat0.xy).x;
    u_xlat16_9.x = u_xlat16_0 * 10.0;
    u_xlat16_1.x = u_xlat16_1.x * u_xlat16_9.x;
    u_xlat16_1.x = exp2(u_xlat16_1.x);
    u_xlat16_1.x = u_xlat16_1.x * 0.649999976;
    u_xlat16_1.x = u_xlat16_0 * u_xlat16_1.x;
    u_xlat16_1.x = u_xlat16_1.x * _RimScale;
    u_xlat16_1.xy = u_xlat16_1.xy * _RimExp.yx;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_1.xy = min(max(u_xlat16_1.xy, 0.0), 1.0);
#else
    u_xlat16_1.xy = clamp(u_xlat16_1.xy, 0.0, 1.0);
#endif
    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.y;
    u_xlat16_5.xyz = _RimColor.xyz + _RimColor.xyz;
    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_5.xyz;
    u_xlat0.xy = vs_TEXCOORD5.yy * hlslcc_mtx4x4unity_MatrixV[1].xy;
    u_xlat0.xy = hlslcc_mtx4x4unity_MatrixV[0].xy * vs_TEXCOORD5.xx + u_xlat0.xy;
    u_xlat0.xy = hlslcc_mtx4x4unity_MatrixV[2].xy * vs_TEXCOORD5.zz + u_xlat0.xy;
    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
    u_xlat16_0 = texture(_NoiseTex, u_xlat0.xy).z;
    u_xlat4.xy = vs_TEXCOORD0.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
    u_xlat16_4 = texture(_MaskTex, u_xlat4.xy).y;
    u_xlat16_13 = u_xlat16_4 * _SpecularPower;
    u_xlat4.xy = vs_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat16_2 = texture(_MainTex, u_xlat4.xy);
    u_xlat16_3.xyz = vec3(u_xlat16_13) * vec3(u_xlat16_0) + u_xlat16_2.xyz;
    SV_Target0.w = u_xlat16_2.w * _MainColor.w;
    u_xlat16_3.xyz = u_xlat16_3.xyz * _MainColor.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * _DirectLightColor.xyz;
    u_xlat16_1.xyz = u_xlat16_3.xyz * vec3(4.0, 4.0, 4.0) + u_xlat16_1.xyz;
    u_xlat16_3.xyz = (-u_xlat16_1.xyz) + _FogColor.xyz;
    u_xlat16_3.xyz = vec3(vs_TEXCOORD7) * u_xlat16_3.xyz + u_xlat16_1.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(0.0<_FogParam.x);
#else
    u_xlatb0 = 0.0<_FogParam.x;
#endif
    u_xlat16_1.xyz = (bool(u_xlatb0)) ? u_xlat16_3.xyz : u_xlat16_1.xyz;
    u_xlat16_3.xyz = _ColorMod.xyz + _ColorMod.xyz;
    u_xlat0.xyz = (-_ColorMod.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
    u_xlat0.xyz = vs_TEXCOORD6.www * u_xlat0.xyz + u_xlat16_3.xyz;
    u_xlat2.xyz = u_xlat0.xyz * u_xlat16_1.xyz;
    u_xlat16_1.xyz = (-u_xlat16_1.xyz) * u_xlat0.xyz + _ColorFade.xyz;
    u_xlat16_1.xyz = _ColorFade.www * u_xlat16_1.xyz + u_xlat2.xyz;
    u_xlat16_13 = log2(_CrystalFade);
    u_xlat16_13 = u_xlat16_13 * 1.5;
    u_xlat16_13 = exp2(u_xlat16_13);
    u_xlat16_3.xyz = vec3(vec3(_CrystalFadePow, _CrystalFadePow, _CrystalFadePow)) + vec3(-0.0, -0.0, -0.800000012);
    u_xlat16_3.xyz = vec3(u_xlat16_13) * u_xlat16_3.xyz + vec3(0.0, 0.0, 0.800000012);
    u_xlat16_3.xyz = u_xlat16_1.xyz * u_xlat16_3.xyz + (-u_xlat16_1.xyz);
    u_xlat16_13 = inversesqrt(_CrystalFade);
    u_xlat16_13 = float(1.0) / u_xlat16_13;
    SV_Target0.xyz = vec3(u_xlat16_13) * u_xlat16_3.xyz + u_xlat16_1.xyz;
    return;
}
```

**<font style="color:rgb(34, 34, 34);">片段Shader参数如下:</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331839041-5ab00ad0-b31f-4998-9c10-09c5335b7bc2.png)



### <font style="color:rgb(34, 34, 34);">第五步：资源和Shader导入Unity中</font>
<font style="color:rgb(34, 34, 34);">1.把贴图资源，Mesh资源和GLSL的顶点Shader,片段Shader导入Unity中，如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331857183-fd5badbf-0c98-4985-8844-257ad9b1b132.png)

<font style="color:rgb(34, 34, 34);">2.在Unity创建一个</font>**<font style="color:rgb(34, 34, 34);">内置管线渲染Shader</font>**<font style="color:rgb(34, 34, 34);">（支持GLSLShader）,把对应的顶点Shader和片段Shader添加进去</font>

**<font style="color:rgb(34, 34, 34);">注意以下两点：</font>**

<font style="color:rgb(34, 34, 34);">首先需要自己定义Shader中使用到的属性</font>

<font style="color:rgb(34, 34, 34);">GLSL Shader会定义一些float3型的3维向量，在Unity不存在，所以我们可以定义成Vector(四维向量)</font>

```plain
Shader "Unlit/ActorGLSLShader01"
{
	Properties
	{
        //这部分是我们自定义的属性 根据Vertex Shader 和 Fragment Shader定义的属性
		_MainTex("Main Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_MainColor("Main Color",Color) = (1,1,1,1)
		_DirectLightColor("Direct Light Color",Color) = (1,1,1,1)
		_RimScale("Rim Scale",float) = 1
		_RimExp("Rim Exp",vector) = (1,1,1,1)
		_RimColor("Rim Color",color)=(1,1,1,1)
		_SpecularPower("Specular Power",float)=1
		_FogParam("Fog Param",vector)=(1,1,1,1)
		_FogColor("Fog Color",color)=(1,1,1,1)
		_ColorMod("Color Mod",vector)=(1,1,1,1)
		_ColorFade("ColorFade",vector)=(0,0,0,0)
		_CrystalFadePow("Crystal Fade Pow",float)=1
		_CrystalFade("Crystal Fade",float)=1
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" "LightMode"="Forwardbase"}
		Cull Back
	    ZWrite On
		LOD 100
		Pass
		{
			GLSLPROGRAM
			// #include "UnityCG.glslinc"
			// #include "GLSLSupport.glslinc"
			//顶点着色器
 内置管线的写法
			#ifdef VERTEX
                //下面这些是我们冲RenderDoc中拷贝来的GLSL Shader
				#version 310 es // OpenGL ES 版本 3.1

				precision highp atomic_uint;  //precision关键字可以批量声明一些变量精度 precision highp float;，表示顶点着色器中所有浮点数精度为高精度
				//lowp、mediump和highp 低精度 中精度 高精度
				uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4]; // unity_ObjectToWorld 矩阵 mat4*4 4X4矩阵 当前模型的矩阵用于将顶点转化到世界空间
				uniform 	vec4 hlslcc_mtx4x4unity_MatrixV[4];//观察矩阵 用于将顶点转化到观察空间 hlslcc_mtx4x4unity_MatrixV
				uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];//观察投影矩阵 用于将顶点转化到裁剪空间 hlslcc_mtx4x4unity_MatrixVP
				uniform 	vec4 _MainTex_ST; //贴图的偏移 和 缩放
				uniform 	mediump vec3 _FogParam; //雾效参数

				//顶点属性 -对应shader中的struct a2v
				in highp vec4 in_POSITION0;
				in highp vec3 in_NORMAL0;
				in highp vec2 in_TEXCOORD0;

				//输出属性 -对应shader中的struct v2f
				out highp vec4 vs_TEXCOORD4; // 世界坐标
				out highp vec3 vs_TEXCOORD5; // 法线的世界坐标（单位化） 
				out mediump vec4 vs_TEXCOORD6; // 输出颜色 
				out highp vec2 vs_TEXCOORD0; // UV
				out mediump float vs_TEXCOORD7; //

				vec4 u_xlat0;
				bool u_xlatb0;
				vec4 u_xlat1;
				mediump float u_xlat16_2;
				float u_xlat9;
				void main()
				{
					//坐标默认是vector4 (x,y,z,1)
					//局部坐标转化世界坐标系
					u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
                    
					//世界坐标  
					u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];//u_xlat0+1
					
					//转化成世界坐标
					vs_TEXCOORD4 = hlslcc_mtx4x4unity_ObjectToWorld[3] * in_POSITION0.wwww + u_xlat0;
					
					u_xlat0 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat0;
					u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat0;
					gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat0;
					// 上面的代码是 坐标变化 局部坐标变换到裁剪空间
					//相当于 o.vertex = UnityObjectToClipPos(v.vertex);


					//把法线转化到世界坐标系下
					u_xlat0.xyz = in_NORMAL0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					u_xlat0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * in_NORMAL0.xxx + u_xlat0.xyz;
					u_xlat0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * in_NORMAL0.zzz + u_xlat0.xyz;
					
					u_xlat9 = dot(u_xlat0.xyz, u_xlat0.xyz); //点乘
					u_xlat9 = inversesqrt(u_xlat9); //开平方根的倒数
					vs_TEXCOORD5.xyz = vec3(u_xlat9) * u_xlat0.xyz; // 归一化 
                    //等同于normalize()

                    //黑色
					vs_TEXCOORD6 = vec4(0.0, 0.0, 0.0, 0.0);

					//uv 坐标转换(TRANSFORM_TEX)
					vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;

					u_xlat0.x = u_xlat1.y * hlslcc_mtx4x4unity_MatrixV[1].z;
					u_xlat0.x = hlslcc_mtx4x4unity_MatrixV[0].z * u_xlat1.x + u_xlat0.x;
					u_xlat0.x = hlslcc_mtx4x4unity_MatrixV[2].z * u_xlat1.z + u_xlat0.x;
					u_xlat0.x = hlslcc_mtx4x4unity_MatrixV[3].z * u_xlat1.w + u_xlat0.x;

					u_xlat16_2 = (-u_xlat0.x) + (-_FogParam.y);
					u_xlat16_2 = u_xlat16_2 * _FogParam.z;

					#ifdef UNITY_ADRENO_ES3
						u_xlat16_2 = min(max(u_xlat16_2, 0.0), 1.0);
					#else
						u_xlat16_2 = clamp(u_xlat16_2, 0.0, 1.0);
					#endif
					//把u_xlat16_2 的值限定在[0,1]之间，类似 saturate(x)

					#ifdef UNITY_ADRENO_ES3
						u_xlatb0 = !!(0.0<_FogParam.x);
					#else
						u_xlatb0 = 0.0<_FogParam.x;
					#endif
					vs_TEXCOORD7 = (u_xlatb0) ? u_xlat16_2 : 0.0;
					return;
				}
			#endif

			//片段着色器
			#ifdef FRAGMENT
				#version 310 es
				precision highp atomic_uint;
				precision highp float; // 片段函数中的float 都是高精度
				precision highp int;
				uniform 	vec3 _WorldSpaceCameraPos; 
				uniform 	vec4 hlslcc_mtx4x4unity_MatrixV[4];
				uniform 	vec4 _MainTex_ST;
				uniform 	vec4 _MaskTex_ST;
				uniform 	mediump vec4 _MainColor;
				uniform 	mediump vec3 _DirectLightColor;
				uniform 	mediump float _RimScale; //边缘缩放系数
				uniform 	mediump vec2 _RimExp;
				uniform 	mediump vec3 _RimColor;//边缘颜色
				uniform 	mediump float _SpecularPower;
				uniform 	mediump vec4 _ColorMod;
				uniform 	mediump vec4 _ColorFade;
				uniform 	mediump float _CrystalFade;
				uniform 	mediump float _CrystalFadePow;
				uniform 	mediump vec3 _FogParam;
				uniform 	mediump vec3 _FogColor;
				uniform mediump sampler2D _MainTex;
				uniform mediump sampler2D _MaskTex;
				uniform mediump sampler2D _NoiseTex;

				//顶点函数的输出 作为片段函数的输入
				in highp vec4 vs_TEXCOORD4;
				in highp vec3 vs_TEXCOORD5;
				in mediump vec4 vs_TEXCOORD6;
				in highp vec2 vs_TEXCOORD0;
				in mediump float vs_TEXCOORD7;
				//输出 
				layout(location = 0) out mediump vec4 SV_Target0;
				vec3 u_xlat0;
				mediump float u_xlat16_0;
				bool u_xlatb0;
				mediump vec3 u_xlat16_1;
				vec3 u_xlat2;
				mediump vec4 u_xlat16_2;
				mediump vec3 u_xlat16_3;
				vec2 u_xlat4;
				mediump float u_xlat16_4;
				mediump vec3 u_xlat16_5;
				mediump vec2 u_xlat16_9;
				float u_xlat12;
				mediump float u_xlat16_13;
				void main()
				{
					//获取单位视角方向  相机世界空间位置减去顶点世界空间位置
					u_xlat0.xyz = (-vs_TEXCOORD4.xyz) + _WorldSpaceCameraPos.xyz;
					u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					u_xlat12 = inversesqrt(u_xlat12);
					u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;  //归一化
					
					//法线和视角方向的乘积
					u_xlat0.x = dot(vs_TEXCOORD5.xyz, u_xlat0.xyz);
					#ifdef UNITY_ADRENO_ES3
						u_xlat0.x = min(max(u_xlat0.x, 0.0), 1.0);
					#else
						u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					#endif
					// u_xlat0.x 限定在[0,1]之间

					u_xlat0.x = (-u_xlat0.x) + 1.0; //反向？
					u_xlat16_1.x = log2(u_xlat0.x); 

					//计算uv 采样贴图_MaskTex的左侧一半 
					u_xlat0.xy = vs_TEXCOORD0.xy * vec2(0.5, 1.0);
					//重新计算uv
					u_xlat0.xy = u_xlat0.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
					//采样 _MaskTex 
					u_xlat16_0 = texture(_MaskTex, u_xlat0.xy).x; //_MaskTex 的RGBA 中的 R 

					u_xlat16_5.x = u_xlat16_0 * 6.0;
					u_xlat16_5.x = u_xlat16_1.x * u_xlat16_5.x;
					u_xlat16_5.x = exp2(u_xlat16_5.x);//指数2的x次方

					u_xlat16_5.x = u_xlat16_5.x * 1.5;
					u_xlat16_5.x = u_xlat16_0 * u_xlat16_5.x;
					u_xlat16_1.y = u_xlat16_5.x * _RimScale;

					//计算uv 采样贴图_MaskTex的右侧一半 
					u_xlat16_9.xy = vs_TEXCOORD0.xy * vec2(0.5, 1.0) + vec2(0.5, 0.0);
					u_xlat0.xy = u_xlat16_9.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
					u_xlat16_0 = texture(_MaskTex, u_xlat0.xy).x; //_MaskTex 的RGBA 中的 R 

					u_xlat16_9.x = u_xlat16_0 * 10.0;
					u_xlat16_1.x = u_xlat16_1.x * u_xlat16_9.x;
					u_xlat16_1.x = exp2(u_xlat16_1.x);
					u_xlat16_1.x = u_xlat16_1.x * 0.649999976;
					u_xlat16_1.x = u_xlat16_0 * u_xlat16_1.x;
					u_xlat16_1.x = u_xlat16_1.x * _RimScale;
					u_xlat16_1.xy = u_xlat16_1.xy * _RimExp.yx;
					#ifdef UNITY_ADRENO_ES3
						u_xlat16_1.xy = min(max(u_xlat16_1.xy, 0.0), 1.0);
					#else
						u_xlat16_1.xy = clamp(u_xlat16_1.xy, 0.0, 1.0);
					#endif
					u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.y;
					u_xlat16_5.xyz = _RimColor.xyz + _RimColor.xyz;
					u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_5.xyz;

					//观察者空间 采样NoiseTex
					//法线转化到观察者空间
					u_xlat0.xy = vs_TEXCOORD5.yy * hlslcc_mtx4x4unity_MatrixV[1].xy;
					u_xlat0.xy = hlslcc_mtx4x4unity_MatrixV[0].xy * vs_TEXCOORD5.xx + u_xlat0.xy;
					u_xlat0.xy = hlslcc_mtx4x4unity_MatrixV[2].xy * vs_TEXCOORD5.zz + u_xlat0.xy;
					u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
					u_xlat16_0 = texture(_NoiseTex, u_xlat0.xy).z;//_NoiseTex 的RGBA 中的 B
					
					//采样_MaskTex 贴图
					u_xlat4.xy = vs_TEXCOORD0.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
					u_xlat16_4 = texture(_MaskTex, u_xlat4.xy).y; //_MaskTex 的RGBA 中的 G
					u_xlat16_13 = u_xlat16_4 * _SpecularPower;

					////采样_MainTex 贴图
					u_xlat4.xy = vs_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					u_xlat16_2 = texture(_MainTex, u_xlat4.xy);
					u_xlat16_3.xyz = vec3(u_xlat16_13) * vec3(u_xlat16_0) + u_xlat16_2.xyz;

					SV_Target0.w = u_xlat16_2.w * _MainColor.w;

					u_xlat16_3.xyz = u_xlat16_3.xyz * _MainColor.xyz;
					u_xlat16_3.xyz = u_xlat16_3.xyz * _DirectLightColor.xyz;
					u_xlat16_1.xyz = u_xlat16_3.xyz * vec3(4.0, 4.0, 4.0) + u_xlat16_1.xyz;
					u_xlat16_3.xyz = (-u_xlat16_1.xyz) + _FogColor.xyz;
					u_xlat16_3.xyz = vec3(vs_TEXCOORD7) * u_xlat16_3.xyz + u_xlat16_1.xyz;
					#ifdef UNITY_ADRENO_ES3
						u_xlatb0 = !!(0.0<_FogParam.x);
					#else
						u_xlatb0 = 0.0<_FogParam.x;
					#endif
					u_xlat16_1.xyz = (bool(u_xlatb0)) ? u_xlat16_3.xyz : u_xlat16_1.xyz;
					u_xlat16_3.xyz = _ColorMod.xyz + _ColorMod.xyz;
					u_xlat0.xyz = (-_ColorMod.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
					u_xlat0.xyz = vs_TEXCOORD6.www * u_xlat0.xyz + u_xlat16_3.xyz;
					u_xlat2.xyz = u_xlat0.xyz * u_xlat16_1.xyz;
					u_xlat16_1.xyz = (-u_xlat16_1.xyz) * u_xlat0.xyz + _ColorFade.xyz;
					u_xlat16_1.xyz = _ColorFade.www * u_xlat16_1.xyz + u_xlat2.xyz;
					u_xlat16_13 = log2(_CrystalFade);
					u_xlat16_13 = u_xlat16_13 * 1.5;
					u_xlat16_13 = exp2(u_xlat16_13);
					u_xlat16_3.xyz = vec3(vec3(_CrystalFadePow, _CrystalFadePow, _CrystalFadePow)) + vec3(-0.0, -0.0, -0.800000012);
					u_xlat16_3.xyz = vec3(u_xlat16_13) * u_xlat16_3.xyz + vec3(0.0, 0.0, 0.800000012);
					u_xlat16_3.xyz = u_xlat16_1.xyz * u_xlat16_3.xyz + (-u_xlat16_1.xyz);
					u_xlat16_13 = inversesqrt(_CrystalFade);
					u_xlat16_13 = float(1.0) / u_xlat16_13;
					SV_Target0.xyz = vec3(u_xlat16_13) * u_xlat16_3.xyz + u_xlat16_1.xyz;
					return;
				}
			#endif
			ENDGLSL
		}
	}
}
```

<font style="color:rgb(34, 34, 34);">3.这个时候Unity会报错，是因为默认平台下是采用DirectX来渲染的，不支持GLSL Shader，这个时候我们需要修改默认平台的渲染方式:</font>

<font style="color:rgb(34, 34, 34);">方式1：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331915406-3f99e864-aaaa-44ac-b364-81794c97a416.png)

<font style="color:rgb(34, 34, 34);">方式2：强制以OpenGL的方式打开：</font>

<font style="color:rgb(34, 34, 34);">"D:\Program Files\Unity2018.4.1f1\Unity\Editor\Unity.exe" -force-opengl</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331929583-d001bb37-24cf-4528-9388-6f65cefbd36f.png)

<font style="color:rgb(34, 34, 34);">4.创建材质，把Shader赋值给材质，贴图赋值到对应的属性上去，根据上面查看Shader的参数，把具体参数设置在面板上，如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331939958-9cc00fe8-0560-45fb-878a-1e4be86a058f.png)

<font style="color:rgb(34, 34, 34);">5.还原的效果如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331950496-c0f4d225-738d-4dfe-a891-c31664382ac9.png)

## <font style="color:rgb(34, 34, 34);">RenderDoc Andoid真机抓帧</font>
### <font style="color:rgb(34, 34, 34);">第一步：安卓手机连接电脑，开启开发者模式</font>
<font style="color:rgb(101, 123, 131);background-color:rgb(245, 245, 245);">adb devices //查看手机是否连接成功</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331968158-08fc3dcf-85f1-4859-a811-e6bcbcc06bb5.png)

### <font style="color:rgb(34, 34, 34);">第二步：启动RenderDoc ，选择连接的手机</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331977255-c64cc50b-69a9-4729-9f54-c79b499e5b89.png)
<font style="color:rgb(34, 34, 34);">出现如下图所示，表示连接成功</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331983796-09f896a3-5ad0-4bc0-b788-d74e77d8167e.png)

**<font style="color:rgb(34, 34, 34);">注意</font>**<font style="color:rgb(34, 34, 34);">：三星的手机会弹出这个界面（其他的手机未知）：</font>

<font style="color:rgb(34, 34, 34);">再次</font><font style="color:rgb(18, 18, 18);">选择手机之后，会通过adb，往手机上安装一个RenderDocCmd的插件app，如下图所示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715331992914-a1b270a0-d001-407e-9f45-70486f316991.png)

<font style="color:rgb(34, 34, 34);">第一次需要把圈出的RenderDocCmd点开并授予权限，否则会出现连接不上的问题。</font>

### <font style="color:rgb(34, 34, 34);">第三步：调试Android手机上App</font>
<font style="color:rgb(34, 34, 34);">在Launch Application 界面，选择</font><font style="color:rgb(18, 18, 18);">Executable Path，在弹出的窗口中选择你要连接的包名（apk 的包名），ok之后点击Launch按钮</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332011049-2c637e02-2ace-419d-9a85-dc9d86a59100.png)

<font style="color:rgb(34, 34, 34);">选择调试的程序：选择的是Activity</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332020326-71a61bd5-c229-400c-8e55-a01c84b53385.png)

<font style="color:rgb(34, 34, 34);">正常我们抓帧的游戏都是Release版本的，这个时候我们直接Launch是会报错的，我们需要把</font>

<font style="color:rgb(34, 34, 34);">Release包编译成Debug包。</font>

<font style="color:rgb(34, 34, 34);">调试一个非Debug模式的apk，会报如下错误：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332030067-6f654ee3-354b-49f7-8740-30b5237865a6.png)

<font style="color:rgb(34, 34, 34);">解决方案如下：</font>

**<font style="color:rgb(34, 34, 34);">方式1：</font>**

**<font style="color:rgb(34, 34, 34);">注意：</font>**<font style="color:rgb(34, 34, 34);">RenderDoc提供了一个重新打包Apk是Debug的方式，点击中间黄色区域，RenderDoc可以直接把Release的Apk，编译成Debug版本，并安装在手机上。</font>

<font style="color:rgb(34, 34, 34);">这个时候开始编译一个Debug版本的游戏：如下图所示</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332040553-5fbbc1ba-9b28-4778-a033-e3abbe4aeb27.png)

<font style="color:rgb(34, 34, 34);">编译成功的提示：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332051265-bdb0515e-d36b-40e3-adc4-bfc1d1b3c27d.png)

**<font style="color:rgb(34, 34, 34);">方式2：</font>**<font style="color:rgb(34, 34, 34);">使用apktool工具，把Release包，重新打包成一个Debug包，修改</font><font style="color:rgb(68, 68, 68);">debuggable=true</font><font style="color:rgb(34, 34, 34);">。</font>

<font style="color:rgb(34, 34, 34);">最后，再按照上述的流程，启动Launch</font>

<font style="color:rgb(34, 34, 34);">启动Launch出现提示：</font>

**<font style="color:rgb(34, 34, 34);">问题1：</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332067670-014bea7b-3c86-4205-80e8-4c11848cefbc.png)

<font style="color:rgb(34, 34, 34);">解决方式：关闭手机上的所有进程，保证不会有RenderDocCmd在后台，或者重启手机，在RenderDoc中再次连接手机，出现如下图所示的，就表示成功了</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332079907-299f9451-f741-4129-aced-e709dd59e462.png)

<font style="color:rgb(34, 34, 34);">点击Launch之后会自动启动游戏（不要手动去启动游戏），出现如下红色圈的内容表示启动成功。</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332088478-5fc4c854-c57e-4e3e-a9f2-69fd0b2ece61.png)

<font style="color:rgb(34, 34, 34);">这个时候我们就可以进行抓帧操作了</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332096011-fdfb73be-2fda-4801-a32b-bce697a0479e.png)

<font style="color:rgb(34, 34, 34);">后续流程就和模拟器抓帧类似了。</font>

### <font style="color:rgb(34, 34, 34);">第四步：修改片元着色器，调试Shader</font>
<font style="color:rgb(34, 34, 34);">通常我们有一些Shader在一些机器上显示有问题，这个时候我们可以进行真机调试，修改片元着色器，查看是那部分出错。</font>

<font style="color:rgb(34, 34, 34);">原来效果：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332110235-30db53e1-b164-4f80-845f-6ce4a98b571a.png)

<font style="color:rgb(34, 34, 34);">如下图所示：</font>

<font style="color:rgb(34, 34, 34);">修改片元着色器的结果，改成白色，修改完成后 刷新一下(Refresh)</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332118806-36b58bac-765a-42bc-a005-008015945778.png)

<font style="color:rgb(34, 34, 34);">我们在TextureViewer中可以查看结果：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715332128080-ad99e303-84ad-4912-9f6d-cb1a3f0b5669.png)

# <font style="color:rgb(34, 34, 34);">文档</font>
<font style="color:rgb(34, 34, 34);">可以参考下面的文档（包含Window 应用的抓帧）</font>

[Renderdoc的使用](https://tcs.teambition.net/storage/302827886613161558df51d94823d2d07ee6?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxNTkzNTIxOCwiaWF0IjoxNzE1MzMwNDE4LCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMwMjgyNzg4NjYxMzE2MTU1OGRmNTFkOTQ4MjNkMmQwN2VlNiJ9.fzWP43Yyn9szhhhENh_B2ddzpJ_OJ2PoySSr_cfd_PQ&download=Renderdoc%E4%BD%BF%E7%94%A8.pdf)



# <font style="color:rgb(34, 34, 34);">参考：</font>
[https://blog.kangkang.org/index.php/archives/504/comment-page-1](https://blog.kangkang.org/index.php/archives/504/comment-page-1)

[https://blog.csdn.net/weixin_42198546/article/details/110387643](https://blog.csdn.net/weixin_42198546/article/details/110387643)

<font style="color:rgb(34, 34, 34);">GLSL转HLSL：</font><font style="color:rgb(34, 34, 34);"> </font>[https://www.jianshu.com/p/4433d1c4498c](https://www.jianshu.com/p/4433d1c4498c)

[https://zhuanlan.zhihu.com/p/80704313](https://zhuanlan.zhihu.com/p/80704313)

[https://zhuanlan.zhihu.com/p/339470661](https://zhuanlan.zhihu.com/p/339470661)

[https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/612888ef41cef60001826697](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/612888ef41cef60001826697)

