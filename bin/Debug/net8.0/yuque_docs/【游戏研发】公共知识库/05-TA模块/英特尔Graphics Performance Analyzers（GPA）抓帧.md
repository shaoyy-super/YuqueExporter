### <font style="color:rgb(34, 34, 34);">英特尔 GPA介绍：</font>
1. **图形监视器 - Graphics Monitor**<font style="color:rgb(77, 77, 77);">  
</font><font style="color:rgb(77, 77, 77);">使用图形监视器工具访问和排列用于 Microsoft DirectX、Vulkan、</font>[<font style="color:rgb(77, 77, 77);">OpenGL</font>](https://so.csdn.net/so/search?q=OpenGL&spm=1001.2101.3001.7020)<font style="color:rgb(77, 77, 77);"> 和 Metal 应用程序图形性能分析的指标集。  
</font><font style="color:rgb(77, 77, 77);">在 Graphics Monitor 中，您可以选择桌面或通用 Windows 应用程序进行分析，并使用相应的工具打开捕获的帧、流和跟踪。</font>
2. **图形帧分析器****<font style="color:rgb(77, 77, 77);">- </font>****Graphics Frame Analyzer**：允许开发人员分析图形应用程序的单个帧，<font style="color:rgb(0, 0, 0);">详细地查看所捕获的帧文件</font>以识别渲染问题，例如低效的绘制调用、过度绘制和着色器性能。
3. **跟踪分析器****<font style="color:rgb(77, 77, 77);">- </font>****Graphics Trace Analyzer**：<font style="color:rgb(77, 77, 77);">使用图形跟踪分析器来识别在功能之间分配 GPU 和 CPU 资源以及应用程序数据的问题。将代码中系统事件的执行配置文件随时间跨各种流可视化，分析应用程序级和系统级性能数据，例如线程活动、上下文切换、API 调用，识别应用程序中的同步和负载平衡问题。</font>
4. **系统分析器****<font style="color:rgb(77, 77, 77);">- </font>****System Analyzer**：监控 GPU 使用率、帧速率和 CPU 利用率等实时指标，<font style="color:rgb(0, 0, 0);">以便判断应用会占用大量 CPU 资源还是大量 GPU 资源，</font>提供对应用程序整体性能的洞察。



下载地址：

[Download Intel® Graphics Performance Analyzers](https://www.intel.cn/content/www/cn/zh/developer/tools/graphics-performance-analyzers/download.html)

下载安装（可以一路next）后得到：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720764814799-26b7713c-876c-44af-aa92-ce821c7e677a.png)



### 如何在游戏上运行GPA：
打开Graphics Monitor 

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720764869778-bcae3236-b4f7-482f-b97b-926b8895b485.png)

方法一：

第一行输入游戏exe执行文件路径

第二行输入上一级文件路径

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720764973747-e6cbfff9-a29f-4aef-baba-a857f1d0769f.png)

配置图形设置 单帧/流/轨迹

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720764999019-d31f5a45-102a-488b-8e7b-c609da795434.png)

设置完毕

点击运行![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720766068250-f7fcca9d-2b90-4300-8092-4b990575c8a3.png)运行游戏



方法二：

打开设置中的自动检测，检测运行的游戏

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723008907667-924f40b6-0bf1-4c85-a3c0-8697d52a9851.png)



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722855904118-cce5385a-136b-46ef-aa9d-e30d24c0cf36.png)

左上角显示实时信息表示GPA成功运行

Ctrl + F1切换视图



# <font style="color:rgb(34, 34, 34);">图形帧分析器</font>**Graphics Frame Analyzer**<font style="color:rgb(34, 34, 34);">：</font>
### 1.  配置和捕获单个帧
两种帧捕获方式：手动捕获/自动捕获，具体在设置中进行设置

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720765042425-f8b5593c-b1d2-43e5-bfd2-ab7007bb4088.png)



手动捕获方式1：游戏运行到需要抓帧的画面时Ctrl+Shfit+C

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720765498063-2ac301d8-8b82-4222-8742-6f216f82617a.png)

手动捕获方式2:打开系统分析

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720765296620-d3577b93-4cf2-4642-91b2-f311a482e697.png)

当游戏运行到需要抓帧的画面时，点击这个摄像机进行捕获

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720765384345-3d865b37-5e78-468b-83a8-5a575b869095.png)



自动捕获：

设置中添加

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720765808383-907d2590-4978-4104-97cf-237e19e5560b.png)



游戏运行时手动捕获/等着触发器自动捕获

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720765157429-f6bab8f7-3177-4abd-a44f-2c026a2805bf.png)

捕获到的帧显示在右侧

### 2.配置和捕获数据流（多个帧）
图形设置选择流捕获模式

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720767216664-00fbdd4a-b430-4f06-be05-12f7a64ff848.png)

打开扩展分析模式

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720767979112-c4a766c9-b5f6-4e8f-9ccb-24d16eb1ed20.png)

才能选择对应的图形设置的流捕获模式（不打开扩展分析模式需勾选![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722853847864-ea1217a8-ef36-4b0b-ac4b-c04c10ec2a1b.png)）

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720768053699-7aba8a54-6072-4094-a836-d24a807e93a9.png)

切换到Option的stream模式

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720769412564-65d1f133-b7aa-41da-9853-69fa36935b29.png)

选择对应设置

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720769623447-3ef60093-2e06-4e3b-a5ae-bc2eacfab02a.png)

左上角Stream recording开启，开始捕获流，按L 开启/关闭

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720770280311-aae1ce69-a3da-4b1e-a1c2-d503c7342f75.png)

捕获到的流显示在右侧

### 3.怎么浏览捕获的单帧
直接双击捕获到的帧

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722589466641-eee500ec-8356-4531-a6d1-1d984b88a800.png)

打开Graphics Frame Analyzer

###### 视图介绍
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722589495241-a497a73c-241c-40d2-b7d3-e02659dda529.png)

大纲视图

从上到下，从左到右顺序视图介绍：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722589641822-2e06d94c-e2c6-4df3-b6d3-fb9c46af3ac8.png)

对可视化柱状图数据进行配置

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722922364991-c4a0efe4-a5b0-4710-b68a-d299a82bea8b.png)X 下拉按钮。选择 X 轴的可用指标，以查看帧中的特定性能方面

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722922371996-37714972-6beb-44f3-9768-e2a7629d1ad3.png)<font style="color:rgb(38, 38, 38);">Y 下拉按钮。选择 Y 轴的可用指标，以查看帧中的特定性能方面</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722922378912-55dd065b-deb6-4052-9b38-5be9bfbb4f1e.png)<font style="color:rgb(38, 38, 38);">分组依据下拉按钮。按以下条件对条形图上的事件进行分组</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722922385315-7ea0ce4f-e5df-4d88-8398-27a5c8c8198f.png)<font style="color:rgb(38, 38, 38);">图表区域下拉按钮。在“条形图视图”的顶部添加标记，以突出显示“分组依据”下拉列表中可用的相同组。</font>（部分功能仅支持DX12）

详细选项：

X轴

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722592917069-f51fcae1-b1c2-4789-b477-f7686f899897.png)  
Constant - no metric：常数-无度量

  
Cs Invocations：Cs调用

表示计算着色器调用的次数。计算着色器每个线程组每个线程调用一次。每个线程组的线程数由计算着色器的 numthreads 属性 ( numthreads(tX, tY, tZ) ) 定义

  
Clipper Invocations：裁剪器调用

表示剪辑器处理的基元的数量。

  
Ds Invocations：Ds调用

表示域着色器调用的次数。每个固定函数曲面细分器输出点调用一次域着色器。

  
GPU Time Elapsed：GPU运行时间

  
Gs Invocations：Gs调用

表示几何着色器调用的次数。如果没有几何着色器与渲染调用相关联，则该值为 0。

  
Hs Invocations：Hs调用

度量 HS 调用表示 Hull Shader 调用的次数。每个补丁调用一次 Hull Shader。



MS  Invocations：MS调用（仅支持DX12）



MS Primitives：MS基元（仅支持DX12）

  
ps Invocations：ps调用

表示像素着色器调用的次数。每个像素调用一次像素着色器。

  
Pixels Rendered：像素渲染

表示通过深度测试的像素数量（如果启用，则包括 Z 缓冲区和模板）。如果深度测试被禁用，“渲染像素”将计算从上一个管道阶段通过的所有像素。

  
Post-Clip Primitives：Post-Clip基元

表示从剪辑器流出的基元数量。该度量包括通过简单剪辑测试（简单接受）的原始基元和剪辑器在剪辑操作后创建的新基元。

  
Post-GS Primitives：Post-GS基元

表示从几何着色器 (GS)（如果启用）流出到裁剪器的图元数量。如果几何着色器与所选渲染调用相关联，则此指标很重要；如果几何着色器代码生成的图元数量是动态的，则此指标更为重要。

  
Primitive Count：原始数据

显示发送到 3D 硬件的基元数量。

  
VS Invocations：VS调用

表示顶点着色器调用的次数

  
Vertex Count  ：顶点计数

表示在 D3D 输入汇编器 (IA) 阶段发送到 3D 硬件管道的顶点数。顶点数取决于基元类型和基元数。使用以下公式：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722939925389-9216897a-980f-4ff1-b0fa-a6b42911c225.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722939934211-4b997153-b16d-46af-b909-ac2c475257d4.png)



Y轴同上

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722593171267-c138a9a3-76af-48c4-be04-e3a8f1a66aa2.png)  
Constant - no metric ：常数-无度规  
cs Invocations ：cs调用

Clipper Invocations ：裁剪器调用  
Ds Invocations ：Ds调用  
GPU Time Elapsed ：GPU运行时间  
Gs Invocations ：Gs调用  
Hs Invocations ：Hs调用

MS  Invocations：MS调用（仅支持DX12）

MS Primitives：MS基元（仅支持DX12）  
ps Invocations ：ps调用  
Pixels Rendered ：像素渲染  
Post-Clip Primitives ：Post-Clip基元  
Post-GS Primitives ：Post-GS基元

Primitive Count ：原始数据  
Vs Invocations ：VS调用  
Vertex Count   ：顶点计数



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722593184240-8428807c-098d-4c90-a9bf-432b9c2e1099.png)

Debug Regions：调试区域，按调试区域对事件进行分组  
Draw Calls：渲染指令  
Render Targets：渲染目标，对合并的事件进行分组

Command Lists：命令列表（仅支持DX12）

Pipeline States：管线设置（仅支持DX12）

Shader Sets：着色器设置，绑定的shader  
Custom Regions  ：自定义区域，在API日志中对区域进行分组合命名



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722593188105-7473877b-b72a-4c58-8135-fcc654094bfd.png)

图表区域

Debug Regions：调试区域，按调试区域对事件进行分组

Render Targets：渲染目标，对合并的事件进行分组

Command Lists：命令列表（仅支持DX12）

Pipeline States：管线设置（仅支持DX12）

Shader Sets：着色器设置，绑定的shader  
Custom Regions：自定义区域，在API日志中对区域进行分组合命名  
Disabled  ：禁用



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722589778501-f8946f77-b7cb-4648-9262-e99cc437b021.png)

柱状图区域，上层为细分柱状图，下层为大纲柱状图

每个柱状图对应API Log里的一个GPU Event

柱状图越粗代表消耗越大

<font style="color:rgb(38, 38, 38);">使用柱状图可以查看单个事件或事件组及其对整个帧时间的贡献。事件是生成 GPU 活动的图形 API 函数，例如绘制调用或清除调用。加载捕获文件后，项目将显示为竖线。这些项目可以是图例中列出的任何类型。柱状图显示 GPU 执行顺序中的项目，从左侧开始。默认情况下，条形的高度表示 GPU 执行的持续时间。GPU 持续时间以微秒为单位。</font>



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722589905275-7c078066-e961-4dad-89c1-ae95f7987f40.png)

API Log：显示API日志 ，可以打开![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722589964108-e309f715-b37d-405c-9de6-c2614b8aa0e3.png)显示更详细信息，与柱状图对应，每一条GPU Event对应柱状图

搜索框可写入=  按条件搜索或点击![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722594551257-f125effc-78b2-45ec-bfa1-d9981d0ce640.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722594660605-81eedf80-bc83-478e-8b96-7830e096d6a7.png)导出APILog到CSV文件

Pixel History：显示只影响像素的事件，用于<font style="color:rgba(0, 0, 0, 0.75);">分析应用程序的像素历史记录</font>

<font style="color:rgba(0, 0, 0, 0.75);">双击选择像素后显示对应相像素填充历史信息</font>

Resource History：只显示用户选择了的资源，选择了相应资源后显示对应历史信息

Frame Statisyics：帧 统计 ，统计每种帧的使用次数

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722910014654-464dea34-c695-43a3-b609-6a73b3ded12c.png)

对应柱状图每个种类的GPU Event

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722910007280-fb6d819f-a257-4a50-8298-70d46860c70b.png)



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722590554513-92299c41-8289-48ee-a1f9-686bdd2eab57.png)

主视图：显示具体信息

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722838479182-85b9b0b4-4097-4826-8c6e-d49a9df7fbc0.png)

度量统计：

显示各种GPU指标

单击隐藏![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927235608-ae4c57cf-af36-42ca-9dfe-b312b234f8a1.png)未选择的指标，在查看所有可能的组和指标之间切换，或仅查看选定的组和指标。（图标切换为显示未选择的指标。您可以在状态之间切换。

单击“![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927235755-6f63411d-d651-477b-b9cf-970a69fc8a3d.png)导出指标值”，将所选指标的指标结果导出到.csv文件。（图标切换为![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927235878-2fc7e4f5-4c5d-4f2b-8043-1a3947e2b920.png)显示进度和取消导出）。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722853994257-29944fc5-9a03-4ff0-a49c-cffa8213ac36.png)

左边（Selection）是当前选中的GPU Event的Metrics统计

右边（Frame）是全部Frame的Metrics统计

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722854009318-f5e22de1-4533-49a8-9717-0354a46b5dd0.png)

Current当前数据

Original原始数据

Delta%/Delta 增量百分比/增量

##### 
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927278898-95d70e05-128d-4003-b27e-799752b5c4e0.png)

Experiments实验选项

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927333219-81098495-925f-4a37-89da-c3a9ee826efd.png)**<font style="color:rgb(38, 38, 38);">2x2 纹理</font>**

<font style="color:rgb(38, 38, 38);">识别由应用程序中使用的纹理贴图导致的潜在性能瓶颈。场景的所有采样纹理都将替换为包含四种不同颜色的 2x2 纹理,如果点击后帧率提升的话 那么就需要优化texture比如降低分辨率 减少材质中的图片</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927346239-579a1fbd-9166-4700-b388-df354fc74155.png)**<font style="color:rgb(38, 38, 38);">简单的像素着色器</font>**

<font style="color:rgb(38, 38, 38);">非常简单的像素着色器替换应用原有的像素着色器，替换默认的fragment shader </font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927361534-ba8b3f68-1797-4029-a440-153d4dfdd1a6.png)**<font style="color:rgb(38, 38, 38);">1x1 剪刀式矩车</font>**

<font style="color:rgb(38, 38, 38);">检查像素渲染时间，绕过渲染管道中的像素处理，这种覆盖模式可以在像素着色器运行之后和像素值写入渲染目标之前，弃用所有的像素，将像素处理减少到平均每绘制调用一个像素。如果帧速率在该模式下有明显增加，说明像素填充率是瓶颈</font>

<font style="color:rgb(38, 38, 38);"> </font>![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927381404-afb1f326-a8d5-41b3-915e-ac64526026e5.png)**<font style="color:rgb(38, 38, 38);">禁用事件</font>**

<font style="color:rgb(38, 38, 38);">阻止呈现选定的事件。此实验可以帮助您测试场景效率。</font>



##### API log资源详细介绍
每个GPU Event分为3个阶段：input输入  execution执行  output输出

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722854218907-ae37c81f-ffaa-42ff-bff6-0411407063e7.png)



单击![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722590623844-ffafc390-b9dd-41c9-b8f5-a622214746f4.png)显示详细信息

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722840234269-f2d84f37-abe4-4485-8408-ef43cf9567dc.png)

每个GPU Event具体内容不同，所有文件一共7种

从左到右：全部 贴图 缓冲 shader设置 模型 采样器 参数 状态

选择一条双击可显示更多细节



###### 贴图Textures：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722914469085-944f5c0e-5186-4a93-8ffd-2bf9bf503e74.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923593778-4696589f-e883-42a1-b4c0-fc7550429341.png)单独显示 ![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923605881-aa159e29-8f81-4498-9e3c-b28120b33c1b.png)打开pixel history ![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923623337-617204c2-073e-4756-aae7-14e4f7558019.png)显示详细信息

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923636952-5867081d-a331-42af-952d-3fef066f2c7d.png)<font style="color:rgb(38, 38, 38);"> 将打开的纹理替换为所选事件的简单 2x2 像素纹理。该实验将创建一个具有相同名称和相同类型的新纹理</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923653435-8a641411-f418-4810-ad5b-a56385b2afbf.png)<font style="color:rgb(38, 38, 38);">禁用所有大于所选事件的纹理当前 mipmap 级别的已打开纹理的 mipmap。该实验将创建一个具有相同名称和相同类型的新纹理</font>

点击![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926523782-be45029a-2c91-4dc3-ba15-0668ab5db5aa.png)还原贴图状态



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923208674-bb9e9f67-332c-486c-a867-9e1f879173be.png)

Normal Mode：显示最后一个事件的渲染目标状态，该事件在读取渲染目标之前或帧结束之前写入此渲染目标

Scrub Mode ：显示最后选定事件的渲染目标状态。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923320274-d1eeaddd-6701-4b62-8199-293346a8c1db.png)

Current： 显示经过修改的渲染目标。

Original ：显示未进行修改的渲染目标。

Diff： 表示当前模式和原始模式之间的差异。

Overdraw ：显示具有蓝色刻度 Overdraw 可视化效果的渲染目标。颜色越亮，像素更新的次数就越多。将鼠标悬停在像素上可查看下方的命中计数。



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722923389401-1a0b6a98-3ad8-4ce9-aa11-ba53f6866c84.png)

Original 将渲染所有选定的事件，而不进行任何修改。

Highlight 将所有选定事件设置为粉红色纯色填充模式。

Wireframe（仅适用于 DirectX 11、DirectX 12 和 Vulcan）以粉红色线框模式显示所有选定的事件。

Hide 不会显示所有选定的事件。

（选定的调用”（Selected Calls） 和“其他调用”（Other Calls） 查看选项仅适用于渲染到选定渲染目标的绘制调用。）



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722841033590-c41d16a3-a4d2-4561-bbc4-73ea89d78da4.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722841361058-8cc07e2e-799b-4928-b47f-c233f6f3269c.png)

鼠标放在对应位置可以实时显示RGBA

对某一像素双击可在pixel history显示填充历史信息

后缀分类：

RTV - 渲染目标视图

<font style="color:rgb(22, 22, 22);">能够呈现到临时中间缓冲区，而不是呈现到要呈现到屏幕的后台缓冲区。 利用此功能，可在图形管道中将可呈现的复杂场景用作反射纹理或用于其他用途，也可将其用于在呈现前向场景添加其他像素着色器效果。</font>

DSV - 深度模板视图

<font style="color:rgb(22, 22, 22);">深度模板视图提供用于保存深度和模板信息的格式和缓冲区。 深度缓冲区用于剔除由较近的对象对其进行遮挡时，对查看者不可见的像素的绘制。 模板缓冲区可用于剔除定义的形状外部的所有绘制。</font>

<font style="color:rgb(77, 77, 77);">UAV</font> - 无序存取视图

<font style="color:rgb(22, 22, 22);">无序的访问视图提供类似的功能，但支持以任何顺序读取和写入到纹理（或其他资源）。</font>

SRV - 着色器资源视图

<font style="color:rgb(22, 22, 22);">着色器资源视图通常以方便着色器访问纹理的方式围绕纹理。</font>

genmips - GenerateMips 调用自动生成的子资源

为给定的着色器资源生成 mipmap

copy - 复制调用中使用的子资源

###### 缓冲Buffer：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722915573220-32512a15-6cb5-4d10-ab55-915d41608082.png)

双击进入显示详细信息，按![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722842228019-3943f648-f6bc-4f07-9c98-497142178952.png)切换列表和图表视图

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926981900-3e43d7db-f9a2-42b9-b99f-73aaa016bf5c.png)单独显示 

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926981553-6be5ac62-f105-464e-b9af-e9f9c6df2db3.png)resource history显示使用所选缓冲区的图形 API 函数。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927043586-855d0129-b919-4fac-a91a-a0b9dd76d161.png)以.csv文件格式从缓冲区导出内容。系统将提示指定文件名和位置。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722927043823-bc25968f-4272-43bc-ae75-b105b577fc13.png)“恢复默认布局：如果未从着色器接收到缓冲区布局，则还原用户所做的任何缓冲区布局修改，并恢复默认的着色器定义或管道状态定义的缓冲区布局或默认格式

后缀分类：

VBV - 顶点缓冲区视图

<font style="color:rgb(22, 22, 22);">顶点缓冲区包含用于定义几何图形的顶点数据。 顶点数据包括位置坐标、颜色数据、纹理坐标数据、法线数据等。</font>

<font style="color:rgb(22, 22, 22);">最简单的顶点缓冲区只包含位置数据。 </font>

copy - 复制调用中使用的子资源

args - 绘制和调度间接参数

IBV - 索引缓冲区视图

<font style="color:rgb(22, 22, 22);">索引缓冲区包含到顶点缓冲区的整数偏移量，用于更高效地渲染基元。 索引缓冲区包含一组连续的 16 位或 32 位索引；每个索引用于标识顶点缓冲区中的一个顶点。</font>

count - 查询结果缓冲区

UAV  - 无序存取视图

<font style="color:rgb(22, 22, 22);">无序的访问视图提供类似的功能，但支持以任何顺序读取和写入到纹理（或其他资源）。</font>

SRV - 着色器资源视图

<font style="color:rgb(22, 22, 22);">以方便着色器访问纹理的方式围绕纹理</font>

CBV - 常量缓冲区视图

<font style="color:rgb(22, 22, 22);">常量缓冲区包含着色器常量数据。 其优点在于数据持续存在，并且可由任意 GPU 着色器访问，直到需要更改数据。</font>

<font style="color:rgb(22, 22, 22);">常量缓冲区的典型数据是世界、投影和视图矩阵，它们在绘制一帧的整个过程中保持恒定。</font>

###### 着色器设置 shader set：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722915734899-5d131410-3eb3-4f49-911b-9e333d611fad.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926672630-28cd27ca-227f-4506-80c4-ff28548bd06d.png)<font style="color:rgb(38, 38, 38);">在resource history显示使用所选着色器集的绘制调用</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926828949-84c07b4d-091d-4679-9f4c-99bf7aa2ae2f.png)<font style="color:rgb(38, 38, 38);">应用 ：编译修改后的着色器源代码，并将其应用于选定的绘制调用或调度调用。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926828965-9528abaa-31b7-44d2-9d5d-b1b8702a7891.png)<font style="color:rgb(38, 38, 38);">还原 ：放弃对打开的着色器所做的所有更改。</font>

<font style="color:rgb(38, 38, 38);">类型过滤器表达式</font><font style="color:rgb(38, 38, 38);">字段 - 过滤器着色器代码值。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926828984-76e92e45-d790-4df9-bd27-0889331d26a7.png)<font style="color:rgb(38, 38, 38);">格式化按钮 ：将默认格式应用于打开的着色器代码。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926828964-4707e732-7159-4bec-aff9-6db0ceb2235f.png)<font style="color:rgb(38, 38, 38);">预处理按钮 ： 通过应用定义列表中的所有定义来预处理打开的着色器代码。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926828841-5ba1b733-83b2-459b-9e27-db69611e4441.png)<font style="color:rgb(38, 38, 38);">加载源代码按钮 ： 将当前着色器源代码替换为导入的代码。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926829066-905cc452-681c-4641-9bbd-b36ff9b4c2f7.png)<font style="color:rgb(38, 38, 38);">“将着色器配置文件导出到 CSV”按钮 - 以 CSV 格式保存当前在着色器编辑器中显示的着色器代码。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722842782635-6b6ed039-3a14-4071-838f-d5a93806e0e7.png)

选择对应资源

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722915879739-39b5d651-fa39-4de3-9720-4a04c31e21c6.png)

<font style="color:rgb(38, 38, 38);">绑定按钮 - 显示在打开的着色器中使用的资源和常量的列表。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722842961613-8e5006e7-2fbb-4064-b9d0-47ea3218f964.png)

点击Fx打开对应顶点/像素着色器



###### 模型geometry：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722916014064-3832b48f-0257-45ff-997c-3c9ff714dba8.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722843152280-a8ef51c8-b191-41e6-bac4-030ebe2e30d1.png)法线属于Object空间

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722843367054-2d24c3df-9705-4d62-8f8f-3870096c35cc.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722843577363-bed1f5c6-ae57-4cc0-90d5-3c02ff214fda.png)点击fx显示对应显示几何图形详细信息







###### 采样器sampler ：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722916208894-023305ee-8933-4661-85c5-98babc0e4682.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926981900-3e43d7db-f9a2-42b9-b99f-73aaa016bf5c.png)单独显示 

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722926981553-6be5ac62-f105-464e-b9af-e9f9c6df2db3.png)resource history显示使用所选缓冲区的图形 API 函数。

查看采样器详细信息



###### 参数argument：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722916270246-3d939ef2-824b-4ba6-9699-ea754bb02c31.png)

显示被选择的API函数的参数，只显示单个函数的数据



###### 状态state：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722916364867-afb470e8-d255-4337-826b-b87c36830faf.png)

显示图形管线，图形计算的设置

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722916414825-f25bfef1-719d-4f6a-927c-edc4cb4e9857.png)ON/OFF切换数据组





### 4.怎么浏览捕获的流
直接双击捕获到的流

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722591173294-92e72abd-68be-4cd6-b40a-aba30ab237e5.png)

打开

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722921862649-3f9363c2-2348-4a74-bb71-719fe32d3982.png)

选择某一帧可以Open打开

其他GPU指标仅支持DX12

### <font style="color:rgb(38, 38, 38);">5.怎么从单帧导出抓取资源</font>
点击![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722846189146-a017489b-0dd2-4651-92e2-864eb8a5ee6e.png)

缓冲资源  API Log可导出CSV文件

模型可导出OBJ

贴图可导出PNG

模型文件导出OBJ后无UV（没有vt数据）

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722846383179-0c85bfaa-0fd9-4f00-94ba-11ef87172a7f.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722846404774-e3affa8e-e779-473d-a394-84ce7847d50f.png)

##### 将模型UV还原方法：
###### 1.从GPA导出CSV文件
选择一条GPU Event的shader

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722932173702-15cc60f8-f522-4e2b-a56f-ff5a91439b6b.png)

导出 顶点 ，UV， 和index（IBV后缀）的CSV文件

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722932799101-3928dcb9-6ec1-47fd-81d9-61aa9b9ea959.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722932810509-2a26b64c-59a1-4707-9c0a-841438909136.png)

得到

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722932913114-5a8b92fd-3ecb-47d5-9714-85d17f17dbce.png)

###### 2.执行Python脚本
安装python

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722932938905-91ecb03f-b2a4-4096-94fd-371aa8da1bce.png)

下载脚本[reorder_gpa_uv.py](https://snh48group.yuque.com/attachments/yuque/0/2024/py/45145007/1722943485970-bea73541-0da1-45cb-a097-d35c4d3073af.py)[reorder_gpa_position.py](https://snh48group.yuque.com/attachments/yuque/0/2024/py/45145007/1722943485972-7f84929b-5756-433a-94ff-41657c1adbec.py)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722933112998-b04a4aa2-8686-4084-9093-cc3407364671.png)

win+R 打开cmd

按此顺序执行：

python<font style="color:#DF2A3F;">空格</font>"reorder_gpa_position的路径"<font style="color:#DF2A3F;">空格</font> "Position的CSV文件的路径"  

回车执行

python<font style="color:#DF2A3F;">空格</font>"reorder_gpa_uv的路径"<font style="color:#DF2A3F;">空格</font>"UV的CSV文件的路径"  

回车执行

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722933252260-0da6430f-4257-47d1-a296-5b28bd670479.png)

出现此行说明成功

得到：  
：![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722935044198-c2a73066-de88-4637-91ff-e57a098a7626.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722935054021-45faaf36-cb81-4aa3-a4f1-7a6b4e6f18bc.png)

两个新的CSV文件

###### 3.修复UV
打开一个unity工程

Package Manager下载FBX Exporter

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722933618567-cc02b648-f789-4c1e-b9df-339120bdd3c4.png)

导入JaveLin_GPA_CSV2FBX脚本

[JaveLin_GPA_CSV2FBX.cs](https://snh48group.yuque.com/attachments/yuque/0/2024/txt/45145007/1722943448942-c35bc98d-cac6-4586-a2eb-8d5157d2f41e.txt)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722933754333-d2fed455-d9ae-4aae-bb0d-e2f0e6508566.png)

unity编辑器新建文件夹（这里是2）并把index文件和Position和UV的fixed文件导入

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722933876913-55622350-3e6c-467d-a73b-f498735f2891.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722933888478-83ca8bc1-eecc-4312-8fc8-e3ec7c0fa000.png)

工具栏打开Tolls-JaveLin_GPA_CSV2FBX

拖入对应文件：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722934495795-f286a51c-7249-4985-b46c-10c216771c3f.png)

得到

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722934508883-b8ead28a-18ec-4ae8-8cbf-0c2d4436eb55.png)

进入Mesh

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722934531299-86b3de72-7adb-423f-9204-19b2ecbd8e8b.png)

UV成功被还原

参考阅读：

[教你如何使用GPA导出模型，另送一个 GPA CSV2MESH Tool in unity-CSDN博客](https://blog.csdn.net/linjf520/article/details/127066726)







# <font style="color:rgb(34, 34, 34);">图形跟踪分析器</font>**Graphics Trace Analyzer**<font style="color:rgb(34, 34, 34);">：（待补充）</font>
### 1.如何捕捉Trace：
手动捕获方式1

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722848854599-8d89ab82-be91-4541-a9b2-5a119a23e444.png)

图形设置选择Trace，

运行游戏后

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722848918279-2e8eee75-a66a-4aa1-a8fb-b36c027b07ff.png)

Ctrl + Shift +T抓取Trace

手动捕获方式2

打开系统分析，当游戏运行到需要抓帧的画面时，点击![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722856573389-b7e3e9de-9e96-4b13-881b-c2da692d4d39.png)进行捕获

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722856498315-3131e871-b7f8-4f5a-a0f5-d8e2413d0254.png)

自动捕获Trigger同上

打开Graphics Trace Analyzer双击抓取的Trace打开

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722849944225-30f851b2-198b-46a9-b1dc-6cd980fcb366.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722849054013-7fd2f5df-9f42-4500-99b4-52538a691fc3.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722946403375-1c47c55e-e302-4738-9141-0ecfdf32d0cf.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722928247881-f217c464-8361-4099-b07a-482637fdeb71.png)突出显示所选内容 - 使用垂直条突出显示所选事件及其相对于其他事件的相对位置。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722928247880-a6a74bb9-548a-43a8-95f9-9087f3cbcf57.png)交叉轨道依赖项选择 - 启用/禁用自动突出显示多个轨道中的所有相关事件，而不是仅突出显示所选事件。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722928247896-ea070157-2a30-497a-a2cf-2b28f8c63b03.png)非活动线程叠加 - 启用/禁用在 CPU 线程轨道中突出显示非活动线程区域。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722928247895-6c5f5852-b218-4bb2-9ae1-7fa4a22b9146.png)“以时间单位/百分比为单位跟踪利用率持续时间”按钮 - 在跟踪利用率持续时间的百分比和时间单位之间切换。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722928305818-fdb46d0d-52e2-4d97-b0fd-233207502005.png)“匹配整个单词”按钮允许按全名搜索和过滤事件。

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722928305807-150ed2c1-e92a-4bb6-9886-eab3914803cf.png)“选择已筛选的任务”按钮在“时间线查看器”窗格中选择已筛选的事件。





+ <font style="color:rgba(0, 0, 0, 0.75);">CPU 受限：逻辑处理器大部分时间都在忙于执行应用程序代码，而 GPU 没有加载；例如， GPU 指标值较低。</font>
+ <font style="color:rgba(0, 0, 0, 0.75);">GPU受限： 与 GPU 占用相关的 GPU 指标较高，而 CPU 内核大多处于空闲状态。</font>

### 2.CPU线程（待补充）：
（暂时截不到图）

<font style="color:rgb(38, 38, 38);">CPU 线程：跟踪表示每个线程的活动：图形 API 调用（绘制调用、缓冲区锁定、资源更新、呈现）和用户定义的调试注释标记</font>

### ![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722947417555-0c08a42f-28a5-4f10-b447-e62815dc8f78.png)
<font style="color:rgb(38, 38, 38);">CPU 核心：跟踪显示来自不同进程的线程（包括分析的应用程序）的执行方式。</font>

<font style="color:rgb(38, 38, 38);"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722947473570-2d57fe37-3b4b-4298-a96a-1284dc5955ba.png)

<font style="color:rgb(38, 38, 38);">CPU 帧：跟踪显示包含两个连续帧的缓冲区交换调用之间的图形命令的范围。</font>

<font style="color:rgb(38, 38, 38);"></font>

（暂时截不到图）

<font style="color:rgb(38, 38, 38);">CPU 指标：显示所选指标集的 CPU 性能。CPU 和 GPU 指标可帮助您比较 CPU 和 GPU 利用率，并发现有问题的区域。</font>



### 3.GPU线程（待补充）：
![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722947308072-a3c8a67f-66f3-451a-9f3d-6d7c38873697.png)

<font style="color:rgb(38, 38, 38);">GPU 队列：显示 GPU 如何执行在屏幕上形成最后一帧的命令。GPU 队列指示 GPU 是繁忙还是空闲。</font>

<font style="color:rgb(38, 38, 38);"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722948111722-a24ceda6-f873-43b6-b87a-3e3b99910248.png)<font style="color:rgb(38, 38, 38);"></font>

<font style="color:rgb(38, 38, 38);">驱动程序队列：显示图形驱动程序如何计划在 GPU 上执行图形命令。驱动程序队列显示提交了多少个图形命令，以及其中有多少个正在等待执行。</font>

<font style="color:rgb(38, 38, 38);"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722947578744-2915cc6a-9a71-448e-aca0-031981af8a23.png)

<font style="color:rgb(38, 38, 38);">GPU 指标：显示所选指标集的 GPU 性能。将指标跟踪放在 GPU 队列旁边，以查看应用程序执行与 GPU 工作负载之间的相关性。例如，确定 GPU 在处理某个包期间是否繁忙。</font>

<font style="color:rgb(38, 38, 38);"></font>

<font style="color:rgb(38, 38, 38);">（暂时截不到图）</font>

<font style="color:rgb(38, 38, 38);">翻转队列：反映了应用程序当前调用、当前 GPU/CPU 队列包、桌面窗口管理器 （DWM） 执行的组合工作以及垂直同步 （VSync） 事件之间的关系。</font>

<font style="color:rgb(38, 38, 38);"></font>

<font style="color:rgb(38, 38, 38);">（暂时截不到图）</font>

<font style="color:rgb(38, 38, 38);">OpenCL 执行：跟踪可视化了 OpenCL 内核在 GPU 或 CPU 上的执行。</font>

<font style="color:rgb(38, 38, 38);"></font>

<font style="color:rgb(38, 38, 38);">（暂时截不到图）</font>

<font style="color:rgb(38, 38, 38);">并行执行：显示驱动程序如何并行执行提交的渲染命令</font>

# <font style="color:rgb(34, 34, 34);">系统分析仪</font>System Analyzer<font style="color:rgb(34, 34, 34);">：</font>
打开System Analyzer

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722849795393-5e7f80d0-b735-4203-a4fc-97effd059c4b.png)或者![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722849818224-a5d549d5-25b5-4075-a3b8-811a09c358da.png)



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722847720550-a533e290-1b44-48e0-8f64-79cd04b1bd25.png)

<font style="color:rgba(0, 0, 0, 0.75);">实时查看CPU，GPU和Graphics API指标</font>

<font style="color:rgba(0, 0, 0, 0.75);">可以捕获帧</font>![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722853694361-ebffc07d-a0c4-4259-919e-b583c021db7e.png)<font style="color:rgba(0, 0, 0, 0.75);">和轨迹</font>![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722853573392-213c7ad9-a10a-43e1-a16f-08fe8d268c88.png)<font style="color:rgba(0, 0, 0, 0.75);">，分别使用Graphics Frame Analyzer和Graphics Trace Analyzer进行详细分析</font>

# <font style="color:rgba(0, 0, 0, 0.75);"></font>
# <font style="color:rgba(0, 0, 0, 0.75);">如何使用英特尔Graphics Performance Analyzers进行性能优化</font>
### 1.优化方法概论
1. <font style="color:rgb(38, 38, 38);">识别有问题的场景。</font>
2. <font style="color:rgb(38, 38, 38);">检查此场景中的游戏是受 CPU 还是 GPU 限制。</font>
3. <font style="color:rgb(38, 38, 38);">确定主要的性能瓶颈。</font>
4. <font style="color:rgb(38, 38, 38);">向下钻取并确定根本原因。</font>
5. <font style="color:rgb(38, 38, 38);">解决问题。</font>

<font style="color:rgb(38, 38, 38);">。。。。待补充</font>

### <font style="color:rgb(38, 38, 38);">2.使用GPA分析</font>
常规工作流程

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1722937090897-35d4a248-e6d0-4734-90b9-cc0d4a1c2d2f.png)



##### 1.<font style="color:rgb(38, 38, 38);">使用</font>系统分析器<font style="color:rgb(38, 38, 38);">实时跟踪多个指标，快速捕获帧或跟踪，定位问题场景</font>
##### 2.通过<font style="color:rgb(38, 38, 38);">Trace Analyzer 定位图形应用程序执行中的问题是受 CPU 还是 GPU 限制</font>
<font style="color:rgb(38, 38, 38);">图形应用程序捕获的数据。在渲染过程中，应用程序会从不同的线程提交数百个图形命令。图形驱动程序解释 GPU 的命令，将它们放入命令缓冲区，将缓冲区推送到 CPU 队列中，并计划在 GPU 上执行命令，从而在屏幕上形成最后一帧。</font>

<font style="color:rgb(38, 38, 38);">两种常见的问题：</font>

<font style="color:rgb(38, 38, 38);">CPU </font><font style="color:rgb(51, 51, 51);">短时间内的计算量太大</font><font style="color:rgb(38, 38, 38);">，例如游戏逻辑、物理、命中检测等，出现高 CPU 负载和低 GPU 负载。</font>

<font style="color:rgb(38, 38, 38);">GPU 无法及时执行任务，例如几何变换、着色、后处理等，出现低 CPU 负载和高 GPU 负载</font>

<font style="color:rgb(38, 38, 38);">捕获跟踪后，使用跟踪分析器工具将其打开并查看主视图</font>

<font style="color:rgb(38, 38, 38);">如果游戏受 CPU 限制：</font>

<font style="color:rgb(38, 38, 38);">。。。。待补充</font>

<font style="color:rgb(38, 38, 38);">如果游戏受 GPU 限制：</font>

<font style="color:rgb(38, 38, 38);">使用图形帧分析器捕获有问题场景的流或帧，以</font><font style="color:rgb(38, 38, 38);">确定根本原因</font>

<font style="color:rgb(38, 38, 38);">常见优化方法：</font>

<font style="color:rgb(38, 38, 38);">优化美术资源（模型减面，调整压缩格式等）</font>

<font style="color:rgb(38, 38, 38);">简化着色器，减少变体，游戏开始前对shader进行预热</font>

<font style="color:rgb(38, 38, 38);">合理使用合批减少Draw Call</font>

<font style="color:rgb(38, 38, 38);">。。。。待补充</font>

<font style="color:rgb(38, 38, 38);"></font>



### 


