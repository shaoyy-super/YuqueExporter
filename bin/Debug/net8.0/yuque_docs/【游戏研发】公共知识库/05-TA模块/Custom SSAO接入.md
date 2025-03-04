提供给需要Custom SSAO接入阅读



##### 1.场景中添加volume 
1.确认RenderData文件添加了EvironmentAllInOneFeature 

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723110196985-3a56bef2-1404-43c2-8a9f-1e904a8bb16d.png)



场景添加volume

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723623142219-83d16f98-e77b-4e99-a737-6931a0736661.png)

2.volume预设添加EnvironmentVolume添加Custom SSAO

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723629722762-ad11eb14-fb4e-4609-aa9a-3cea0da49389.png)

new一个或者拖入已有预设

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723623225352-8a03407d-7fd9-4328-8ed8-7611315999d7.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723623297194-aba117c8-3091-40af-81d4-503a2bc3410a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723623312773-db56b677-1e89-48fe-b081-4357b95e2244.png)

3.打开SSAO

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1724053700585-454800fc-8fd0-4c79-95dc-aa891dc3a418.png)

##### 2.导入计算函数
导入：

ScreenSpaceAmbientOcclusionPass

UT-ScreenSpaceAmbientOcclusion.shader

UT-SSAO.hlsl（并UT-ScreenSpaceAmbientOcclusion.shader内正确引用）

（根据内置SSAO Pass，ScreenSpaceAmbientOcclusion.shader和SSAO.hlsl修改）

shader导入unity内置光照包

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

##### 
##### 3.简单介绍
1. <font style="color:rgb(24, 25, 28);">先在ScreenSpaceAmbientOcclusionPass申请深度/深度法线图和屏幕UV坐标反算出当前顶点世界坐标的位置，</font>

<font style="color:rgb(24, 25, 28);">屏幕上的每一个像素，根据周边深度值计算一个遮蔽因子(Occlusion Factor)。这个遮蔽因子之后会被用来减少或者抵消片段的环境光照分量。遮蔽因子是通过采集片段周围球型核心(Kernel)的多个深度样本，并和当前片段深度值对比而得到的。高于片段深度值样本的个数就是要求遮蔽因子。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1724060895511-6a9cd632-a763-4783-b750-625b9b942089.png)



<font style="color:rgb(24, 25, 28);">如图，中心黑点为采样位置，蓝色箭头为平面的法向量，空心点为未受遮蔽的采样点(Sample Point)，实心白点为受遮蔽的采样点，左边采样点的AO值计算方法为：未受遮蔽的采样点 / 采样点总数 = 0 / 11 = 0，故该点未受遮蔽，AO值为0；同理右边为3/11。</font>



2. <font style="color:rgb(24, 25, 28);">UT-ScreenSpaceAmbientOcclusion.shader内采样深度/深度法线图获取深度/法线信息，使用沿着法线正方向半球内的随机点进行采样, 获得新的坐标点，进行比较, 判断是否被遮挡, 然后加权处理获得AO信息.</font>
3. <font style="color:rgb(24, 25, 28);">AO这时候充满噪点, 根据BlurQuality进行模糊处理</font>
4. <font style="color:rgb(24, 25, 28);">最后和场景颜色RT进行混合叠加.</font>





##### 4.修改shader
shader需要有DepthOnly和DepthNormal Pass在PrePass阶段获取深度/深度法线

写法可参考Lit.shader



shader中加关键字 

#pragma multi_compile_fragment _ _CUSTOM_SCREEN_SPACE_OCCLUSION

不能用内置_SCREEN_SPACE_OCCLUSION否则打包时会被剔除

CustomGetScreenSpaceAmbientOcclusion函数内关键字也要修改（推荐把函数复制出来）



shader内定义关键字

float2 uvScreen = GetNormalizedScreenSpaceUV(input.positionCS);（positionCS怎么获取略）

#if defined(_CUSTOM_SCREEN_SPACE_OCCLUSION)

			AmbientOcclusionFactor aoFactor = CustomGetScreenSpaceAmbientOcclusion(uvScreen);

#endif

获取直接AO和间接AO：

		aoFactor.directAmbientOcclusion

		aoFactor.indirectAmbientOcclusion

具体用法根据shader适配

直接AO常见用法：影响主光源

mainLight.color *= aoFactor.directAmbientOcclusion;

间接AO常见用法：参与PBR光照计算

(如果shader本身采样了AO贴图，与自带AO取较小值

Occlusion = min(Occlusion,aoFactor.indirectAmbientOcclusion);)

（Occlusion是计算PBR间接光的参数）











##### 5.参数面板说明：
**useSSAO**：总控开关

**AOMethod**：AO生成方法，采样BlueNoise/InterleavedGradientNoise梯度计算

**DownSample**：降采样，开启后屏幕采样/2，采样像素数变成1/4

**AfterOpaque：不透明之后计算，切换计算顺序**

走Opaque之前会使用AOFactor参与计算影响光照，走Opaque之后会直接用AOFactor压暗alpha通道

**Source**：深度来源，dep/depnormal，Source为depth，SSAO 不使用DepthNormalsPass 来生成法线纹理，改用深度纹理重建法线向量，开启AfterOpaque会强制走Depth

**NormalSamples**：法线采样质量，仅Depth模式生效

**DirectLightingStrength**：直射光强度，开AfterOpaque会失效

**Radius**：法线纹理采样半径

**RadiusFactor**：半径调整参数

**Samples**：采样样本数量

**BlurQuality**：噪声质量

高（Bilateral）：Bilateral模糊，三次处理

中等（Gaussian）：Gaussian模糊，需要两次处理

低（Kawase）：Kawase 模糊，需要一次处理

**FallOff**：衰减

##### 6.可能出现的问题：
1.BlueNoise模式直接崩溃

可能因为RenderData文件引用的贴图丢失

需要手动导入

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1724055485059-95caf96c-34b1-435a-987a-1090ca710a60.png)

（如果改动记得连着RenderData文件一起上传）





3.传参都正确但是场景SSAO不生效

不同模型尺寸需适配不同参数

如太大的模型使用太小的RADIUS参数会导致效果微乎其微无法发现

需尝试不同参数











