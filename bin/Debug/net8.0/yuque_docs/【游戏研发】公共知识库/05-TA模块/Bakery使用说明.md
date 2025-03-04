# 使用说明
## 环境设置
烘焙之前需要先确认项目的渲染设置（Bakery只能DX11模式下工作）。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731591691488-9be69210-af9a-4086-a4a0-312f6139d615.png)

## 烘焙器
**Setting mode：**Bakery有三种烘焙方式

+ Simple：简单模式，速度快，效果差。
+ Advanced：高级渲染模式，速度慢，效果最好，适合最后的烘焙。
+ Experiment：实验模式，速度中，效果还可以，平常用。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731398785401-d3619d09-70f6-44a2-8689-614c88b87496.png)

**Render mode：**

+ Full Lighting：为所有的光照提供完整的直接和间接的照明。
+ Indirect：这是一种Mixed模式，会查看每个灯光的Baked Contribution，如果其设置为Direct And Indirect，则灯光会像在Full Lighting模式下一样烘焙。设置为Indirect Only，则仅烘焙此灯光的GI。
+ Shadowmask：一是生成烘焙颜色，二是生成静态对象的阴影贴图。
+ Subtractive：<font style="color:rgb(77, 77, 77);">减法模式，这个选项对光照贴图没有任何特殊的作用，事实上，它就像全光照一样工作。唯一的区别是它还设置了实时 Unity 灯光以使用减法模式。</font>
+ Ambient Occlusion Only：仅环境光遮挡，只烘焙AO。

**Directional mode：**

+ None
+ Baked Normal Maps：烘焙shadowmask时会考虑物体本身的法线，但实时阴影不会，所以会导致实时阴影和烘焙阴影过渡时产生不一致的问题。
+ Donminant Direction
+ RNM
+ SH

对项目来说，若只需常规的lightmap，选None即可，只会生成烘焙颜色贴图，可与unity的烘焙器相统一，能直接替换使用。

若后续需要烘焙高光，则选Donminant Direction即可，会生成烘焙颜色和方向贴图。若ibl选该方案需要修改shader进行适配。

**Light probe mode（光照探针模式）：**

+ Legacy：使用Render Light Probes按钮生成探针。点光源和定向光源在光照贴图器中计算，而区域/天空/间接光照通过在每个探针位置渲染立方体贴图来收集。结果存储为L2 球谐函数。缺点是立方体贴图渲染性能缓慢，并且在游戏中的着色器不物理表示光照表面或您的项目设置为移动设备（Unity 可以剪掉高强度值）的情况下，光照贴图和探针之间可能不匹配。
+ L1：点击渲染时，光照探针将与光照贴图一起渲染。其烘焙性能优越，且能保证探针照明与光照贴图匹配。结果保存为L1球谐函数。

**Asset UV processing（资源UV优化）：**

+ Don't change：不修改模型资产2u。
+ Adjust UV padding：会对2u进一步调整使得uv岛之间具有适当的填充，但这修改后的烘焙uv无法记录到Mesh里，导致不同步。所以不推荐使用。
+ 需配合fbx勾选Generate Lightmap UVs，会自动生成2u。

**Distance shadowmask：**

+ 使用距离阴影遮罩。

**<font style="color:rgb(77, 77, 77);">Denoiser (降噪器)：</font>**

+ <font style="color:rgb(77, 77, 77);">OptiX 5</font>
+ <font style="color:rgb(77, 77, 77);">OptiX 6</font>
+ <font style="color:rgb(77, 77, 77);">OptiX 7</font>
+ <font style="color:rgb(77, 77, 77);">OpenImageDenoise</font>

降噪。越高降噪效果越好，但烘焙会变慢。建议选OptiX 7。前三者是GPU上运行，<font style="color:rgb(77, 77, 77);">OpenImageDenoise是在CPU上运行，速度稍慢，但质量相当。</font>

**<font style="color:rgb(77, 77, 77);">Lightmapping tasks</font>**

+ **<font style="color:rgb(77, 77, 77);">Adjust sample positions：</font>**<font style="color:rgb(77, 77, 77);">防止漏光</font>
+ **<font style="color:rgb(77, 77, 77);">Unload scenes before render：</font>**<font style="color:rgb(77, 77, 77);">渲染前卸载场景，在烘焙之前卸载unity场景以释放视频内存。</font>
+ **<font style="color:rgb(77, 77, 77);">Denoise：</font>**<font style="color:rgb(77, 77, 77);">应用去噪算法。</font>
+ **<font style="color:rgb(77, 77, 77);">Fix Seams：</font>**<font style="color:rgb(77, 77, 77);">修复接缝，会尝试混合由 UV 不连续性创建的接缝。</font>

**Auto-Atlasing：**

+ 自动图集，分割场景方便流加载。

**Texels per unit：**

+ 1unit = 1m，每个单位内纹素的数量，用来设置场景物件的烘焙精度。推荐设置为：室外10左右，室内20左右。值越高，烘焙精度越高，当然lightmap的尺寸也会更大。

**Max resolution：**

+ 最大lightmap分辨率。

**Min resolution：**

+ 最小lightmap分辨率。

**Scale per Map type（逐纹理按类型缩放，烘焙完毕后会根据缩放值设置分辨率）：**

+ Main lightmap scale：主光照纹理缩放
+ Shadowmask scale：阴影遮罩缩放
+ Direction scale：方向纹理缩放

**Checker preview：**

+ 用于预览场景分配的lightmap scale是否合理，若Texels pre unit修改了，需要点击Refresh checker刷新一下。

**Bounces：**

+ <font style="color:rgb(77, 77, 77);">定义光线应该从表面反弹多少次。值越高，间接光照的效果越好，但烘焙时间也会更长。</font>

**Samples：**

+ 全局光照采样次数，会影响GI的质量。

**Hacks：**

+ Emissive boost：自发光反弹。值越高，自发光烘焙效果越好。如需要烘焙自发光，建议设为15。
+ Indirect boost：间接光反弹。值越高，间接光照效果越明显。
+ Backface GI：背面穿透光光照效果。用于计算物体透光性。
+ Ambient occlusion：AO设置。

以上设置若不需要相关功能均用默认即可，不要修改。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731589966752-513aac3d-5571-47af-b0b0-eefeb0e9878a.png)

**Performance****：**

+ RTX mode：开启光追模式。需要开启。
+ Export terrain trees：导出地形树数据，关闭。
+ Samples multiplier：采样倍率。保持默认1。

**Output options：**

+ 输出数据缓存路径。建议设置在SSD固态硬盘中，能加快渲染速度。

## 场景构建
### 场景物体
参与烘焙的物体需<font style="color:rgb(25, 27, 31);">保证Static下的Contribute GI为勾选状态。且Lighting组件参数如下设置。Scale In Lightmap的参数根据实际情况和效果需求进行设置。如保持地面为1，近处建筑为0.5~1，远处为0.2~0.5等。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731591941279-47e36e82-33cb-4f73-a348-e0c2788d0845.png)

<font style="color:rgb(25, 27, 31);">如果有需要参与Lightmap的投影计算，但不接受投影的物体，也需要保证Static下的Contribute GI为勾选状态。但Lighting组件中的参数要做如下设置：</font>

+ <font style="color:rgb(25, 27, 31);">1、首先将Receive Global Illumination改为Lightmaps，然后将Scale In Lightmap数值调整为0.</font>
+ <font style="color:rgb(25, 27, 31);">2、烘焙后再将Receive Global Illumination改为Light Probes。</font>

### 布光
参与烘焙的light需挂载brakey相关light脚本。

注：若Bakery烘焙器上Render Mode设置为Full Lighting则灯光bakery脚本不会出现Baked contribution选项。

譬如Directional Light需要挂载Bakery Direct Light.cs，Point Light需要挂载Bakery Point Light.cs。

挂载脚本后参数需要调同步，点击Match lightmapped to real-time即可。

如下图：

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731398054112-74281d60-b5ad-4c89-b911-8e6137679eac.png)

### 特殊
1. 场景内所有物体不必以单个烘焙模式bake，而是可以分为不同的bakery group，不同物体根据不同的bakery group进行bake。

做法：在project创建Bakery Lightmap Group，Auto代表会默认选择Bakery烘焙器上的参数。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731405753495-93b09c46-6cf9-407c-a06d-84e2f36f7840.png)

2. Bakery可以能将一堆物体可以烘焙入一张lightmap，无论物体的数量。

做法：先创建一个父物体A，挂载Bakery Lightmap Group Selector.cs，然后将需要打入同一张lightmap的

   物体挂在父物体之下。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731406369329-09eb2278-e449-4a73-b773-5d073ade430d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731406382776-6b2709ce-1a7d-454b-8598-5d2e45e8ef7a.png)

# <font style="color:rgb(77, 77, 77);">效果</font>
## 烘焙高光
烘焙高光需要dir贴图，即bakery Directional mode选择Dominant Direction模式。

倘若场景中有多个灯光，烘焙出的dir信息是由灯光方向权重混合插值出的结果，其权重影响因子有：

+ 灯光颜色饱和度 g>r>b
+ 灯光远近
+ 灯光强度

下面的图是不同情况对dir的影响权重对照图：

| **dir** | ![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731585328144-4a717809-3ee9-41cf-9105-bf70620984e3.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731585332629-bddf95c4-d0a1-4fa3-83f6-b6629e3c3361.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731585336425-81c59b30-b056-408a-8210-d4de55aa7d42.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731585339732-b7915729-cabc-4101-9803-b209d232c869.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731585342357-745d98ac-348d-4a5a-81ca-b000d5a35ac8.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731585346969-e8d5a9e2-33b3-4781-b3d9-31879769e298.png) |
| :---: | --- | --- | --- | --- | --- | --- |
| **灯光颜色** | （1，0，0） | （0，1，0） | （0，0，1） | （1，0，0） | （0，1，0） | （0，1，0） |
| **灯光远近** | 1 | 1 | 1 | 0.1 | 1 | 1 |
| **灯光强度** | 1 | 1 | 1 | 1 | 1 | 6 |


烘焙高光效果对照图（材质：FASceneObject）：

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731586368123-bfc54dbf-6d7b-4b5e-97b2-6e7ef9cadfbf.png)![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731586377769-55945089-2a1a-40a2-8a5f-a57a7763dfdd.png)

左：non-directional						右：directional

可以看出立体感好了一截。

# 烘焙推荐参数
## 烘焙器
+ **Setting mode：**Advanced
+ **Render mode：**Shadowmask，若无需静态阴影，则选Indirect
+ **Directional mode：**None，Baked Nomal Maps也可选，这两者属于non-directional，若需要directional，则选Dominant Direction。
+ **Light probe mode：**L1
+ **Asset UV processing：**Don't change或者Remove UV adjustments
+ **Denoise：**Optix 7。其他也可选，不强制。
+ **Distance shadowmask：**√
+ **Texels per unit：**室外10左右，室内20左右。可通过Show checker来查看lightmap scale是否合适。
+ **RTX mode：**√
+ **Output path：**自定义输出路径

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731650197232-8f0832da-c628-4bbb-94d4-added92f7a1a.png)

## 烘焙布光
这边建议Unity灯光和Bakery结合使用，unity灯光负责实时，Bakery负责间接光烘焙。

### 平行光
1. Mode设置：Mixed。
2. 脚本添加：Bakery Direct Light
3. 灯光效果设置完后，点击Match lightmaped to real-time
4. Baked contribution：Indirect And Shadowmask，没有静态阴影需求的话则设置为Indirect Only
5. Shadow spread：0

Shadow samples：1

注：Bakery会根据投影距离对阴影进行虚化，会导致与Unity实时阴影不兼容，因此需要修改这两个参数。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1731651408336-84970e9c-9e76-4564-b6a4-40e50845103d.png)

### 点光源
1. Mode设置为：Baked
2. 脚本添加：Bakery Point Light
3. 灯光效果设置完后，点击Match lightmaped to real-time
4. Baked contribution：Direct And Indirect

### 聚光灯
1. Mode设置为：Baked
2. 脚本添加：Bakery Point Light
3. <font style="color:rgb(25, 27, 31);">Projection Mask设置为Cone</font>
4. 灯光效果设置完后，点击Match lightmaped to real-time
5. Baked contribution：Direct And Indirect

### 天光
1. Lighting里把天光环境配置好
2. 通过Bakery->Create->Skylight创建Bakery天光
3. 点击Match this light to scene skybox将Lighting设置同步到Bakery

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732587411450-e004eb9c-7d72-4b7b-b139-ab2c5e8dae5e.png)

# 常见问题及处理
## 烘焙后画面变脏
### 问题
烘焙后的部分区域会像这样变的很脏

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732587666684-0b0ebea9-b52a-4cdc-88ba-061a125f50ca.png)

### 处理
该问题主要是烘焙精度不够导致的，首先我们再Bakery里勾选Show Checker，如下图可以看到该部分模型的烘焙精度远远小于其他物体。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732588202560-4703cd0d-a7f0-41ae-a240-8637948813b0.png)

我们可以通过以下两个方式来进行调整：

1. Bakery里的Texels per unit适当上调，但这是一个全局参数，会导致lightmap变大，慎重！若整体烘焙精度ok，只有少量模型精度不足，建议2的处理方案。
2. 可以调节该模型Mesh Renderer组件里的Scale In Lightmap参数，适当上调使其烘焙精度提高。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732588749196-d254884e-dfa1-4a69-98c2-12a43cf4b88f.png)

调整过后我们点击Show Checker下面的Refresh checker按钮刷新一下，如下图可以看到，其烘焙精度明显提高。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732588848272-09228b09-a3a5-4fb4-8416-38d7f23bb291.png)

这是处理过后的烘焙效果：

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732588990710-b095cc6f-008c-4b7e-b37d-17792e54bc1d.png)

## lightmap缝隙
### 问题
在烘焙后有些模型可能会出现明显的缝隙，如下图

是由于不匹配的硬边和uv分割导致的uv填充问题

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732763047911-a4cd7244-ab59-4350-9d91-48375f105d63.png)

### 处理
1. 看下Bakery中的Fix UV Seams是否勾上。
2. 若Fix UV Seams勾上后依然无法解决，则需要对模型进行处理了，可以通过删除不必要的边缘，增添支撑环或是修改uv split和硬边的位置来进一步优化。这有助于解决烘焙问题，提高烘焙质量。

推荐polycount的这篇文章，其详细讲述了硬边和uv的关系影响和相应的处理方法。

[https://polycount.com/discussion/107196/youre-making-me-hard-making-sense-of-hard-edges-uvs-normal-maps-and-vertex-counts/p1](https://polycount.com/discussion/107196/youre-making-me-hard-making-sense-of-hard-edges-uvs-normal-maps-and-vertex-counts/p1)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1732866506179-9c64515a-7ab0-479e-a09a-12302b011844.png)

## 烘焙后色彩过多
### 问题
烘焙后若产生如下图的问题，其产生原因是光线弹射过程中会染上周边的颜色，其本质为indirect色。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733284296257-efe51806-2c24-413e-9f18-571d48ead944.png)

### 处理
若想要减弱这种情况，可以在Light下适当减弱indirect强度（看效果需求而定，不强制修改）。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733290281400-649ba38e-b317-4314-a0d9-4734ca079975.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733290554063-73f23883-52c8-4ebb-bf21-d59334ab8605.png)

