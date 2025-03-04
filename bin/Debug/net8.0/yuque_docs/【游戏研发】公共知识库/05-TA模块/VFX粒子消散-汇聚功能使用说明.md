# kin'e'dkined使用说明
Hierachy中 右键 -> Visual Effects -> Visual Effect 创建VFX粒子系统

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739850211379-d02cc08a-3c7d-4df0-8d0b-baa1e70d8d0c.png)



Asset Template 中选择使用 Mesh Morph Template

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739850325412-d14de688-2cc4-43f0-abaf-4aa3a2683c03.png)

#  消散-汇聚Vfx功能说明
### 参数预览
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739850642763-add67236-7e27-4ccb-971e-90396688914b.png)



### 模型设置
**<font style="color:#DF2A3F;">模型名称中需加上[RW]后缀，否则无法获取到Mesh信息</font>**

**目前一个SkinnedMesh需配置一套Vfx粒子系统，不支持锁定视角和CostomFov功能**

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739856209075-93926e9b-5f53-4297-8bb6-ddd5e90bca1f.png)

**Mesh Source:**  粒子溶解模型（Skinned Mesh Renderer），粒子消散的来源模型

**UniformMeshBuffer Source:**  用来储存Mesh信息，脚本传值，无需关注

**Transform Source: ** 粒子溶解模型的Transform，脚本传值，后文会介绍

**Transform Souce Adjust: ** 粒子溶解模型的Transform调整值，只改rotation，尽量通过Transform Source绑定到正确的根节点位置，视情况移除

**Mesh Target: ** 粒子汇聚模型（Skinned Mesh Renderer），粒子汇聚的目标模型

**UniformMeshBuffer Target:**  功能同上

**Transform Target: ** 功能同上

**Transform Target Adjust: ** 功能同上



Vfx上挂载脚本

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739947257450-883ea4a3-1f57-4706-a4fb-052b2f03c5dd.png)

两个 **Uniform Mesh Baker**：

**Sample Count:**  Mesh顶点数，Bake后可见，仅供参考不可更改

**Sample Count Multiplier: ** 顶点的采样比率，需求是粒子会从模型表面发射并汇聚到另一个模型表面，因此需要采样Mesh信息，这个值可以采样顶点采样的密度，值越小粒子在模型表面可选择的发射与汇聚点就越少，但性能会更好，一般保持顶点数 x 比率 < 2000，不超过这个值的情况下（即顶点数<6667）保持默认值即可

**Mesh Property Name:**  要读取的Mesh来源，需在vfx中提前设置好 Mesh Source 和 Mesh Target, 然后负责烘焙粒子溶解模型的就填 Mesh Source，粒子汇聚模型就填Mesh Target

**Graphics Buffer Name: ** 写入的Mesh顶点数据Buffer名称，负责烘焙粒子溶解模型的就填 UniformMeshBuffer Source，粒子汇聚模型就填UniformMeshBuffer Target

**Initialize On Enable: ** 是否在激活时重新烘焙Mesh信息，<font style="color:#8A8F8D;">如果是常驻粒子就不勾，中间插入或者需要关闭后再开启就勾上，</font>这里后面的脚本中有自动去激活的功能，不用勾  


**两个Custom Morph Transform Binder:**

Property栏分别设置为 Transform Source 和 Transform Target

Transform栏分别绑定对应模型的根骨骼节点

### 发射设置
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739858929281-8dfea536-f9f6-4f0f-a5c1-81562c43fdc4.png)

**Particle Density: ** 粒子发射密度，每秒的粒子发射量 = Mesh顶点数 x Sample Count Multiplier x Particle Density，同屏粒子数不得超过5000（暂定），粒子数检查工具下面面会介绍



同屏粒子数检查：

加入预制件 Vfx_Info_Canvas

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739860239615-563ee7ec-0f69-4e46-806f-d64bf489b2dc.png)

在VfxInfo脚本中加入所有用到的Vfx粒子系统

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739860125761-2242817f-1f63-4aaa-b4e5-e292ecfadf05.png)

左上角就能看到实时的粒子数量

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739860340465-e6bfffec-866b-4f66-802b-93850b4d562d.png)



### 压扁设置
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739860581070-dd230d0e-a686-4a5f-a9e2-0ec1f59a43d8.png)

脚本控制，无需关注



### 消散-汇聚设置：
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739860630184-3f14c172-b36c-45a4-8e6a-7e803421aef7.png)

**Color Over Life:  **生命周期中的颜色渐变

**Lifetime Offset: ** 生命周期的偏移量，默认是2s-3s之间的随机值

**Force Over Life:  **生命周期中的推力曲线

**Force Strength: ** 推力大小，Force越大，粒子速度越快

**Kill Distance Threshold: ** 粒子清除距离阈值，当粒子到目标点的距离小于该值时，粒子会被杀死

**Distance Drag:**  粒子开始受到抓力的距离（距目标点位置）

**Drag Strength: ** 抓力强度

**Drag Curve:  **抓力曲线

**Particle Size Min: ** 粒子尺寸最小值

**Particle Size Max: ** 粒子尺寸最大值

**Turbulence Intensity: ** 扰动强度

**Turbulence Drag Coefficient: ** 扰动的粒子抓力



### 粒子设置
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739861530956-2a35f8e2-c127-4ba0-beee-29a5a4d1097d.png)

**MainTex:**  粒子贴图，带Alpha



# 粒子系统组织形式&额外脚本设置
**优先完成粒子效果制作**



### 粒子预制件
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739947700127-dbab9570-8203-4c03-9957-1c747066409b.png)

鉴于目前一个模型内会包含多个SkinnedMesh Renderer，目前的处理方法是为每一个SkinnedMesh配置一份VFX，然后挂载在同一节点下制作成预制件

父节点上需挂载脚本Morph Mesh Manager，该脚本在激活时会自动收集子节点中的VFX



### 模型预制件设置
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739947934051-703ecbce-1ff3-408f-8cfe-43c67179eb10.png)

模型预制件最外层需挂载VFXMorphPropertybinder脚本，将**所有**需要发射/接收粒子的SkinnedMesh Renderer及其对应的根节点Transform**<font style="color:#DF2A3F;">依次</font>**加入列表，**对应粒子预制件中的VFX顺序**

添加这一步的原因是程序需要在游戏运行中动态给VFX绑定资产（全部绑一起做成一个大的预制件会破坏现有的预制件加载逻辑）

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739948395322-4a839e7a-de54-437d-a761-b64a36046448.png)

顺序必须严格对应的原因是VFX中使用的模型数据是脚本预烘焙的，模型匹配不上会导致逻辑出错



### 测试脚本
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1739948545944-e228400f-5383-4b29-956e-c784391205de.png)

在程序没有提供对应的timeline工具前，可以使用Morph Init And Switch Test脚本进行功能测试

（运行时生效）

**Source:  **发射粒子的模型预制件

**Target:  **接收粒子的模型预制件

**MorphManager:  **粒子预制件上的管理脚本

**Switcher:  **勾选后会交换发射和接收粒子的模型





