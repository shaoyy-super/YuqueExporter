本文档主要为美术提供使用，所以只汇总了Character、Effects、PBR、Scene、Water、MK4、UIEffect这几类shader。

## 1. Character
Character包含以下shader：

|  | Shader Name |  |  |
| --- | --- | --- | --- |
| 1.1 | URP/MoleGame/Character/EyeBall |  |  |
| 1.2 | URP/MoleGame/Character/HairPBR |  |  |
| 1.3 | URP/MoleGame/Character/Shadow |  |  |
| 1.4 | URP/MoleGame/Character/Skin |  |  |


### 1.1 URP/MoleGame/Character/EyeBall
PBR眼球效果，提供基础的PBR参数，Cube Map和自发光效果，还可以配合雾效、Evolve、冰、石头、溶解、全息效果使用

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |
| Model Height | 模型高度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emissive Intensity | 自发光强度 |  |  |
| EyeHighlightCubeMap | 环境映射图 |  |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |
| 特殊效果 | 详见第9章 |  |  |


### 1.2 URP/MoleGame/Character/HairPBR
PBR头发效果，除PBR基础参数，还提供了主高光、次高光、自发光、模型高度的调节。另外，可以配合双面、雾效、Evolve、冰、石头、溶解和全息效果使用

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |
| Model Height | 模型高度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emissive（自发光） | Emissive Intensity | 自发光强度 |  |
|  | Emissive Color | 自发光颜色 |  |
| Specular(R) Spec Shift(G) Spec Mask(B) | 该贴图RGB通道分别为高光、高光偏移、高光参数 |  |  |
| Specular(高光) | Primary Specular | Specular Color | 高光颜色 |
|  |  | Specular Primary Shift | 高光首次偏移 |
|  |  | Primary Specular Scale | 首次高光缩放 |
|  | Secondary Specular | Secondary Specular Color | 第二高光颜色 |
|  |  | Specular Secondary Shift | 第二高光偏移 |
|  |  | Second Specular Scale | 第二高光缩放 |
| Anisotropy(各向异性) | _Anisotropy | 各向异性参数1 |  |
|  | _Anisotropy2 | 各向异性参数2 |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Two Sided | 双面渲染 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |
| 特殊效果 | 详见第9章 |  |  |


### 1.3 URP/MoleGame/Character/Shadow
阴影效果，通过粒子贴图和调节透明度达到阴影效果

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Particle Texture | 粒子贴图 |  |  |
| Alpha Intensity | 透明度强度 |  |  |


### 1.4 URP/MoleGame/Character/Skin
PBR皮肤效果，颜色、主贴图控制皮肤本身颜色，曲率控制次表面散射（缩写SSS），Kelemen LUT采样模拟皮肤表面油脂层，法线贴图控制皮肤的凹凸。另外，可以配合双面、雾效、Evolve、冰、石头、溶解和全息效果使用

| Base Function |  |
| --- | --- |
| Color | 颜色 |
| Albedo | 主贴图 |
| Scaled_AO_Curvature_Smooth | 缩放AO贴图中的RGB参数 |
| AO_Curvature_Smoothess | R对应AO，G对应曲率，B对应光滑度附：曲率贴图生成工具 |
| Normal(Normal) | 法线纹理 |
| Normal Scale | 缩放法线 |
| Model Height | 模型高度 |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| SSS Lookup | SSS Lookup贴图，次表面散射显示查找表，展示皮肤透光时使用，可能根据不同皮肤透光颜色不同有修改 |  |  |
| Curve Factor | 曲线因子，根据曲率采样SSS Lookup时使用，调整大小影响透光强度 |  |  |
| Kelemen LUT | Kelemen LUT贴图，计算皮肤油脂镜面反射时使用，基本不改动 |  |  |
| Specular Factor | 皮肤镜面反射强度，调整大小影响反射强度 |  |  |


| Additional Function |  |
| --- | --- |
| Rendering Mode | 渲染模式 |
| Two Sided | 双面渲染 |
| 特殊效果 | 详见第9章 |


## 2. Effects
Effects包含以下shader：

|  | Shader Name |  |  |  |
| --- | --- | --- | --- | --- |
| 2.1 | URP/MoleGame/Effects/Distortion_Noise |  |  |  |
| 2.2 | URP/MoleGame/Effects/Double_Sided_Dissolve |  |  |  |
| 2.3 | URP/MoleGame/Effects/Explosive_Core |  |  |  |
| 2.4 | URP/MoleGame/Effects/General |  |  |  |
| 2.5 | URP/MoleGame/Effects/Polar_Coordinates |  |  |  |
| 2.6 | URP/MoleGame/Effects/Tornado |  |  |  |
| 2.7 | URP/MoleGame/Effects/Distortion_Edge_Alpha |  |  |  |
| 2.8 | URP/MoleGame/Effects/Glass |  |  |  |


### 2.1 URP/MoleGame/Effects/Distortion_Noise
单面扭曲效果，mask起遮罩功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Mask Texture | Mask贴图 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Parameter | 扭曲参数 |  |  |
| ZTest State | 深度测试，开启或关闭 |  |  |


### 2.2 URP/MoleGame/Effects/Double_Sided_Dissolve
溶解效果，双面可见，内部颜色强度可调节，溶解贴图控制溶解的形状

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Dissolve Texture | 溶解Mask贴图 |  |  |
| Dissolve Texture Speed U | 溶解Mask贴图U速度 |  |  |
| Dissolve Texture Speed U | 溶解Mask贴图V速度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Dissolve Factor | 溶解参数 |  |  |
| Vertical Factor | 垂直方向参数 |  |  |
| Inside Color Intensity | 内部颜色强度 |  |  |
| Final Alpha | 透明度参数 |  |  |


### 2.3 URP/MoleGame/Effects/Explosive_Core
爆炸效果，双面可见，提供旋转、增强漫反射颜色、菲尼尔功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Second Texture | Mask贴图 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Diffuse Add Parameter | Diffuse调节参数 |  |  |
| Fresnel Parameter 1 | 菲尼尔参数 |  |  |
| Fresnel Parameter 2 | 菲尼尔参数 |  |  |
| UV Rotate Parameter | UV旋转参数 |  |  |


### 2.4 URP/MoleGame/Effects/General
通用特效，提供溶解、扭曲、顶点动画、菲尼尔、alpha裁剪、灰色重着色、切边羽化(交界处透明)、自定义数据、是否在UI中的基本特效功能

| 体积雾参数 |  |  |  |
| --- | --- | --- | --- |
| Apply Volumetric Fog |  | 如果该材质需要叠加体积雾的颜色时打勾，否则为Off，若要开启该选项，需要先确认体积雾中Use For Transparent开启。该功能性能消耗比较大，谨慎使用！ |  |
| Transparent Fog Blend Mode |  | 该功能基于上面的选项，若要叠加体积雾的颜色，该选项为材质球与体积雾颜色的混合模式。 |  |


| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Main Texture Channel Mask(Default: RGBA) | 选择贴图通道（默认R通道） |  |  |
| Main Texture Rotate Angle | 主纹理UV旋转角度 |  |  |
| Intensity(Default:1) | 强度 |  |  |
| Desaturate(Default:0) | 去色参数 |  |  |


| Normal Function |  |  |  |
| --- | --- | --- | --- |
| Normal Texture | 法线贴图 |  |  |
| Normal Scale | 法线缩放 |  |  |


| Mask Function |  |  |  |
| --- | --- | --- | --- |
| Mask Texture | Mask贴图 |  |  |
| Mask Texture Speed U | Mask贴图U速度 |  |  |
| Mask Texture Speed V | Mask贴图V速度 |  |  |
| Mask Texture Channel Mask(Default: R) | 选择贴图通道（默认R通道） |  |  |
| Mask Texture Rotate Angle | Mask贴图UV旋转角度 |  |  |
| Mask Texture2 | Mask贴图 |  |  |
| Mask Texture Speed U | Mask贴图U速度 |  |  |
| Mask Texture Speed V | Mask贴图V速度 |  |  |
| Mask Texture Channel Mask(Default: R) | 选择贴图通道（默认R通道） |  |  |
| Mask Texture Rotate Angle | Mask贴图UV旋转角度 |  |  |
| Mask Texture3 | Mask贴图 |  |  |
| Mask Texture Speed U | Mask贴图V速度 |  |  |
| Mask Texture Speed V | Mask贴图V速度 |  |  |
| Mask Texture Channel Mask(Default: R) | 选择贴图通道（默认R通道） |  |  |
| Mask Texture Rotate Angle | Mask贴图UV旋转角度 |  |  |


| Dissolution Function（溶解） |  |  |  |
| --- | --- | --- | --- |
| Dissolution  Texture | Dissolution 贴图 |  |  |
| Dissolution  Texture Speed U | Dissolution 贴图U速度 |  |  |
| Dissolution  Texture Speed V | Dissolution 贴图V速度 |  |  |
| Dissolution  Texture Channel Mask(Default: R) | 选择贴图通道（默认R通道） |  |  |
| Dissolution  Texture Rotate Angle | Dissolution  纹理UV旋转角度 |  |  |
| Dissolution Reverse | 是否取反 |  |  |
| Dissolution Percent | 溶解参数 |  |  |
| Dissolution Soft Edge | 边缘软化参数 |  |  |
| Dissolution Edge Color | 边缘颜色 |  |  |
| Dissolution Edge Width | 边缘宽度 |  |  |


| Distortion Function（扭曲主贴图） |  |  |  |
| --- | --- | --- | --- |
| Distortion  Texture | Distortion  贴图 |  |  |
| Distortion  Texture Speed U | Distortion  贴图U速度 |  |  |
| Distortion Texture Speed V | Distortion  贴图V速度 |  |  |
| Distortion  Texture Channel Mask(Default: R) | 选择贴图通道（默认R通道） |  |  |
| Distortion Texture Rotate Angle | 主纹理UV旋转角度 |  |  |
| Distortion Intensity | 强度 |  |  |


| Vertex Function（顶点位移） |  |  |  |
| --- | --- | --- | --- |
| Vertex Texture | Vertex 贴图 |  |  |
| Vertex Texture Speed U | Vertex 贴图U速度 |  |  |
| Vertex Texture Speed V | Vertex 贴图V速度 |  |  |
| Vertex Texture Channel Mask(Default: R) | 选择贴图通道（默认R通道） |  |  |
| Vertex Texture Rotate Angle | Vertex 纹理UV旋转角度 |  |  |
| Vertex Intensity | 强度 |  |  |


| Fresnel Function |  |  |  |
| --- | --- | --- | --- |
| Fresnel Add | Fresnel Color | 菲尼尔颜色 |  |
|  | Fresnel Power(Front) | 菲尼尔正面参数 |  |
|  | Fresnel Intensity(Font) | 菲尼尔正面强度 |  |
|  | Fresnel Power(Back) | 菲尼尔背面参数 |  |
|  | Fresnel Intensity(Back) | 菲尼尔背面强度 |  |
| Fresnel Alpha | Fresnel Alpha | On使用Fresnel Alpha，Off使用Fresnel Add |  |
|  | Fresnel Alpha Intensity | 强度 |  |


| Breathe |  |  |  |
| --- | --- | --- | --- |
|  | Breathe Speed | 屏闪频率 |  |
|  | Breathe Interval | 屏闪间隔 |  |


| Alpha Clip |  |  |  |
| --- | --- | --- | --- |
| Cutoff(Default:0.5) | 透明度裁剪 |  |  |


| Double Face Color |  |  |  |
| --- | --- | --- | --- |
| Font Color | 正面颜色 |  |  |
| Back Color | 背面颜色 |  |  |


| Gray Overlay（通过贴图明度控制自发光区域） |  |  |  |
| --- | --- | --- | --- |
| Gray Threshold(Default:1) | 灰色重着色 |  |  |
| Highlight Intensity | 高光强度 |  |  |
| Gray Overlay Color | 颜色 |  |  |


| Depth Overlay（切边部分羽化） |  |  |  |
| --- | --- | --- | --- |
| Depth Fade Offset | 偏移 |  |  |
| Depth Fade Intensity | 强度 |  |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| ZTest | 深度测试 |  |  |
| ZWrite | 深度写入 |  |  |
| CustomData | 自定义数据 将HDR Intensity和Dissolution Percent写入到UV的ZW通道中  （粒子的CustomData控制自发光和溶解） |  |  |
| UI | Used In UI | 是否在UI中使用（伽玛矫正UI特效要勾选） |  |
| Special Factor | Alpha Overflow | 用来替换旧的shader |  |
| Depth Clip | 对alpha进行裁切写入深度图。该功能主要用于开启景深的场景中，透明物体面片过大会使后面大范围背景变得清晰。 |  |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |


### 2.5 URP/MoleGame/Effects/Polar_Coordinates
极坐标特效，提供扭曲功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Dissolve Texture | 溶解Mask贴图 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Twisting Strength | 扭曲强度 |  |  |
| Color Lightness | 颜色亮度 |  |  |


### 2.6 URP/MoleGame/Effects/Tornado
龙卷风特效，双面可见，提供遮罩、旋转，扭曲，菲尼尔，修改顶点等功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Mask Texture | Mask贴图 |  |  |
| Mask Texture Speed U | Mask贴图U速度 |  |  |
| Mask Texture Speed U | Mask贴图V速度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Second Texture | 主贴图的Mask贴图 |  |  |
| Vertex | Vertex Texture | 顶点贴图 |  |
|  | Vertex Parameter | 顶点参数 |  |
| Diffuse Add Parameter | Diffuse 调节参数 |  |  |
| Fresnel | Fresnel Parameter1 | 菲尼尔参数 |  |
|  | Fresnel Parameter2 | 菲尼尔参数 |  |
| UV Rotate Parameter | UV旋转参数 |  |  |


### 2.7 URP/MoleGame/Effects/Distortion_Edge_Alpha
双面扭曲，提供菲尼尔功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Mask Texture | Mask贴图 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| ZTest | 深度测试 |  |  |
| Parameter | 调节边缘 |  |  |
| Fresnel | Fresnel Mul Factor | 菲尼尔参数 |  |
|  | Fresnel Pow Factor | 菲尼尔参数 |  |
| Cull Mode | 剔除模式，Back剔除背面, Front剔除前面，Off关闭 |  |  |


### 2.8 URP/MoleGame/Effects/Glass
玻璃材质，除PBR基础参数外，厚度图表明玻璃的透明度(参数可调节)，法线贴图影响玻璃的折射效果

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理(这里法线贴图用来修改高光，模拟玻璃折射的效果) |  |  |
| Normal Scale | 缩放法线 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Thickness(厚度) | Thickness Texture | 厚度纹理贴图，通过透明度模拟玻璃厚度 |  |
|  | Edge Thickness | 边缘厚度 |  |
|  | Reflection Intensity | 反射强度 |  |


## 3. PBR
PBR包含以下shader：

|  | Shader Name |  |  |  |
| --- | --- | --- | --- | --- |
| 3.1 | URP/MoleGame/PBR/FASceneMap |  |  |  |
| 3.2 | URP/MoleGame/PBR/FASceneObject |  |  |  |
| 3.3 | URP/MoleGame/PBR/FAStandard |  |  |  |
| 3.4 | URP/MoleGame/PBR/SimpleGlass |  |  |  |
| 3.5 | URP/MoleGame/PBR/FAWorldMap |  |  |  |


### 3.1 URP/MoleGame/PBR/FASceneMap
PBR场景地图特效，除基础的PBR参数外提供自发光、粒子和扫描线效果，可搭配雾效使用

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |
| Model Height | 模型高度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emisslve(自发光) | Emissive Intensity | 自发光强度 |  |
|  | Emission Texture | 自发光纹理贴图 |  |
|  | Emission Color | 自发光颜色 |  |
| Particle(粒子效果) | Particle Texture | 粒子纹理 |  |
|  | Particle Mask Texture | 粒子遮罩纹理 |  |
|  | Particle Color | 粒子颜色 |  |
|  | Particle SpeedX | 粒子纹理X方向速度 |  |
|  | Particle Intensity | 粒子强度 |  |
| Scane(扫描线) | Scane Color | 扫描线颜色 |  |
|  | Scane Screen Space | 扫描使用屏幕空间 |  |
|  | Scane Line Angle(rad) | 扫描角度 |  |
|  | Scaning Line Speed | 扫描线速度 |  |
|  | Scaning Line Frequency | 扫描线频率 |  |
|  | Scaning Line Intensity | 扫描线强度 |  |
|  | Scane Splash Frequency | 扫描线飞溅频率 |  |
|  | Scane Splash Level | 扫描飞溅等级 |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |


### 3.2 URP/MoleGame/PBR/FASceneObject
PBR场景物体特效，除PBR基础参数外提供雾效、自发光、顶部细节、溶解等功能，可配合雾效使用

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Clip | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emission Texture(自发光) | Emissive Intensity | 自发光强度 |  |
|  | Emission Texture | 自发光纹理贴图 |  |
|  | Emission Color | 自发光颜色 |  |
| Normal Add | Normal2 Map | 第二套法线纹理 |  |
|  | Normal2 Scale | 第二套法线缩放参数 |  |
| Top Detail(顶部细节) | Top Color | 顶部颜色 |  |
|  | Top Texture | 顶部纹理贴图 |  |
|  | Top AO_Matel_Smoothess | 顶部AO贴图参数缩放 |  |
|  | Top Scaled_AO_Metal_Smoothess | 顶部AO贴图， R通道AO参数，G通道金属度， B通道光滑度 |  |
|  | Top Normal Map | 顶部法线贴图 |  |
|  | Top Normal Scale | 顶部法线缩放 |  |
|  | Top Mask Texture | 顶部Mask贴图 |  |
|  | Top Mask Texture2 With UV2 | 顶部第二套Mask贴图（使用UV2） |  |
|  | Top Intensity | 顶部Intensity参数 |  |
|  | Top Power | 顶部Pow参数 |  |
|  | Top Offest | 顶部偏移参数 |  |
| Dissolve(溶解) | Dissolve Percent | 溶解参数 |  |
|  | Dissolve Mask | 溶解Mask贴图 |  |
|  | EdgeColor Length | 边缘颜色长度 |  |
|  | Edge Color | 边缘颜色 |  |
|  | Dissolve Mask Texture 2 With UV2 | 第二套溶解Mask纹理贴图（使用UV2） |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Reflection Mode | 反射模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |


### 3.3 URP/MoleGame/PBR/FAStandard
PBR标准特效，提供双面、自发光、雾效、Evolve、冰、石头、溶解、全息、镭射效果

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |
| Model Height | 模型高度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emisslve(自发光) | Emissive Intensity | 自发光强度 |  |
|  | Emission Color | 自发光颜色 |  |
| Dissolve Percent | 溶解参数 |  |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Two Sided | 双面渲染 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |
| Iridescence | 虹膜效果 |  |  |
| 特殊效果 | 详见第9章 |  |  |


### 3.4 URP/MoleGame/PBR/SimpleGlass
PBR通用特效，提供PBR基础参数和自发光，可配合雾效、Evolve、冰、石头、溶解、全息效果

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |
| Model Height | 模型高度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emisslve(自发光) | Emissive Intensity | 自发光强度 |  |
|  | Emission Color | 自发光颜色 |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |
| 特殊效果 | 详见第9章 |  |  |


### 3.5 URP/MoleGame/PBR/FAWorldMap
PBR世界地图特效，提供两个颜色贴图、自发光效果以及地图Area和Erode侵蚀

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |
| AO_Metal_Smoothess | AO贴图，R通道为AO参数，G通道为金属度，B通道为光泽度 |  |  |
| Scaled_Ao_Metal_Smooth | 缩放AO贴图中的RGB参数 |  |  |
| Normal Map | 法线纹理 |  |  |
| Normal Scale | 缩放法线 |  |  |
| Model Height | 模型高度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Albedo2 | 第二套贴图 |  |  |
| Albedo Gradient | 两套贴图之间的混合参数 |  |  |
| Emisslve(自发光) | Emissive Intensity | 自发光强度 |  |
|  | Emission Color | 自发光颜色 |  |
| Area | Area Color1 | 区域颜色1 |  |
|  | Area Color2 | 区域颜色2 |  |
| View Distence | 视觉距离 |  |  |
| Erode(侵蚀) | Erode Mask | 侵蚀Mask贴图 |  |
|  | Erode Percent | 侵蚀参数 |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |


## 4. Scene
Scene包含以下shader：

|  | Shader Name |  |  |  |
| --- | --- | --- | --- | --- |
| 4.1 | URP/MoleGame/Scene/AtomSphere_A |  |  |  |
| 4.2 | URP/MoleGame/Scene/Building_light |  |  |  |
| 4.3 | URP/MoleGame/Scene/Hologram |  |  |  |
| 4.4 | URP/MoleGame/Scene/lunkuoguang |  |  |  |
| 4.5 | URP/MoleGame/Scene/TacticalScan |  |  |  |
| 4.6 | URP/MoleGame/Scene/TintColor |  |  |  |


### 4.1 URP/MoleGame/Scene/AtomSphere_A
原子球场景特效，主贴图和Mask都是流动UV，并且Mask可以选择采样类型（UV或世界坐标）

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Main Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Main Texture Speed U | 主贴图U速度 |  |  |
| Main Texture Speed V | 主贴图V速度 |  |  |
| Mask Texture | Mask贴图 |  |  |
| Mask Texture Speed U | Mask贴图U速度 |  |  |
| Mask Texture Speed V | Mask贴图V速度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Mask Texture UV Type | 采样类型，UV1，UV2使用UV采样，positionWS使用世界坐标系采样 |  |  |
| Mask Texture2 | Mask贴图 |  |  |
| Mask Texture Speed U | Mask贴图U速度 |  |  |
| Mask Texture Speed V | Mask贴图V速度 |  |  |
| Mask Texture 2 UV Type | 采样类型，UV1，UV2使用UV采样，positionOS使用模型坐标系采样 |  |  |
| Intensity | 强度 |  |  |


### 4.2 URP/MoleGame/Scene/Building_light
建筑光场景特效，提供主贴图流动UV、自发光、unity雾效功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Texture | 主贴图 |  |  |
| XSpeed | 主贴图U速度 |  |  |
| YSpeed | 主贴图V速度 |  |  |
| Alpha Cutoff | 透明度裁剪 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Saturation | 饱和度 |  |  |
| Roughness | 粗糙度 |  |  |
| Fog Intensity | 调节雾效强度 |  |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Reflection Mode | 反射模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |


### 4.3 URP/MoleGame/Scene/Hologram
全息场景特效，主贴图控制形状，提供波浪、故障艺术、菲尼尔和溶解功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| XSpeed | 主贴图U速度 |  |  |
| YSpeed | 主贴图V速度 |  |  |
| Alpha | 透明度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Wave（波浪效果） | Wave Height | 高度 |  |
|  | Wave Speed | 速度 |  |
| Glitch（故障效果） | Random Glitch Offset | 偏移 |  |
|  | Glitch Speed | 速度 |  |
|  | Glitch Tilling | 故障平铺 |  |
|  | Glitch Interval | 间隔 |  |
|  | Glitch Amount | 数量 |  |
|  | Glitch Constant | 故障反差 |  |
| Fresnel（菲尼尔） | Fresnel Scale | scale参数 |  |
|  | Fresnel Power | power参数 |  |
|  | Fresnel Color | 颜色 |  |
| Dissolve（溶解） | Dissolve Scale | 缩放参数 |  |
|  | Dissolve Hide | 隐藏参数 |  |
| Transparent Power | 透明度Power参数 |  |  |


### 4.4 URP/MoleGame/Scene/lunkuoguang
轮廓光场景特效，颜色为轮廓光颜色，提供主贴图和Mask贴图，可以调节自发光参数、crackle参数、mask的灰度图和光边缘强度

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Main Tex | 主贴图 |  |  |
| Mask Tex | Mask贴图 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Emission | 自发光参数 |  |  |
| Crackle | 裂纹参数 |  |  |
| Gray Mask | 灰度 |  |  |
| Edge Intensity | 边缘强度 |  |  |


### 4.5 URP/MoleGame/Scene/TacticalScan
战术扫描场景特效，提供颜色、主贴图(流动UV)、Mask(流动UV)和Extent纹理和呼吸功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Main Tex | 主贴图 |  |  |
| Main Text U | 主贴图U速度 |  |  |
| Main Text V | 主贴图V速度 |  |  |
| Mask Tex | Mask贴图 |  |  |
| Mask Tex U | Mask贴图U速度 |  |  |
| Mask Text V | Mask贴图V速度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Extent Tex | Mask贴图 |  |  |
| Center Position | Extent的中心点 |  |  |
| Offset | 偏移度 |  |  |
| Breathe（呼吸效果） | Breathe Intensity Min | 呼气最小强度 |  |
|  | Breathe Intensity Max | 呼吸最大强度 |  |
|  | Breathe Frequency | 呼吸频率 |  |
| Intensity | 强度 |  |  |


### 4.6 URP/MoleGame/Scene/TintColor
TintColor，修改颜色，目前主要用在camera上

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Tint Color | 颜色 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| ZTest | 深度测试 |  |  |
| Cull | 正反面剔除 |  |  |


## 5. Water
Water包含以下shader：

|  | Shader Name |  |  |  |
| --- | --- | --- | --- | --- |
| 5.1 | URP/MoleGame/Water/FoamyBicolored |  |  |  |


### 5.1 URP/MoleGame/Water/FoamyBicolored
模拟水特效，提供水基础效果之外还有深水、浅水、海岸、泡沫等效果

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Normal（法线） | Normal Texture | 法线纹理 |  |
|  | Normal Tilling | 法线偏移（影响高光位置） |  |
| Water（水效果） | Deep Water Color | 深水颜色 |  |
|  | Shallow Water Color | 浅水颜色 |  |
|  | Depth Transparency | 透明度模拟深度 |  |
|  | Shallow-Deep-Blend | 深浅混合 |  |
|  | Shallow-Deep-Fade | 深浅淡出 |  |
| Shore（岸） | Shore Fade | 岸边淡出 |  |
|  | Shore Transparency | 岸边透明度 |  |
| Reflection（反射） | Enable Reflections | 开启反射 |  |
|  | Reflection Intensity | 反射强度 |  |
| Specular（高光） | Specular | 高光参数 |  |
|  | Specular Color | 高光颜色 |  |
| Gloss（光泽） | Gloss | 光泽 |  |
|  | Light Wrapping | 光照参数 |  |
| Refraction | 折射 |  |  |
| Wave Speed | 波浪速度 |  |  |
| Foam（泡沫） | Foam Texture | 泡沫纹理 |  |
|  | Foam Tiling | 平铺参数 |  |
|  | Foam Blend | 混合参数 |  |
|  | Foam Visibility | 可见性参数 |  |
|  | Foam Intensity | 强度 |  |
|  | Foam Contrast | 反差 |  |
|  | Foam Color | 颜色 |  |
|  | Foam Speed | 速度 |  |


## 6. MK4
MK4包含以下shader：

|  | Shader Name |  |  |  |
| --- | --- | --- | --- | --- |
| 6.1 | URP/MK4/Billboards |  |  |  |
| 6.2 | URP/MK4/Movie |  |  |  |
| 6.3 | URP/MK4/Street_Lights |  |  |  |
| 6.4 | URP/MK4/Street_Rain_Triplanar |  |  |  |


### 6.1 URP/MK4/Billboards
广告牌特效，提供颜色和主贴图(流动UV)、背景图、Led图以及相关的故障、辉光和溶解效果

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Albedo Color | 颜色 |  |  |
| Main Texture | 主贴图 |  |  |
| Panner X | 主贴图U速度 |  |  |
| Panner Y | 主贴图V速度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Distortion（扭曲） | Distort Texture | 扭曲纹理贴图 |  |
|  | Distortions | 扭曲参数 |  |
| Background（背景） | Background | 背景纹理贴图 |  |
|  | Background Em | 影响参数 |  |
| LED | LED int | Led 参数 |  |
|  | LED | Led纹理贴图 |  |
|  | Glitch | 故障效果强度 |  |
|  | LED Glow | Led 光泽纹理图 |  |
|  | Glitch 1 | 故障效果参数 |  |
|  | Glitch 2 | 故障效果参数 |  |
|  | Smoothness | 光滑度 |  |
| Dissolve（溶解） | Enable Dissolve | 开启溶解 |  |
|  | Dissolve Percent | 溶解参数 |  |
|  | Dissolve Mask | 溶解纹理贴图 |  |
| Edge（边缘） | Edge Color Length | 边缘颜色长度 |  |
|  | Edge Color | 边缘颜色 |  |


### 6.2 URP/MK4/Movie
电影特效，提供颜色和主贴图(流动UV)纹理动画、Led贴图及相关的自发光、扭曲、溶解和边缘颜色等功能

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Colums | 列数 |  |  |
| Rows | 行数 |  |  |
| Movie Speed | 速度 |  |  |
| Metallic | 金属度 |  |  |
| Specular | 高光 |  |  |
| Smoothness | 光滑度 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Distortion（扭曲） | Texture 0 | 扭曲纹理贴图 |  |
|  | Distort | 扭曲参数 |  |
|  | Distort Speed | 扭曲速度 |  |
| LED | LED | Led纹理贴图 |  |
|  | Emission | 自发光参数 |  |
|  | Emission LED | Led 自发光参数 |  |
| Dissolve（溶解） | Enable Dissolve | 开启溶解 |  |
|  | Dissolve Percent | 溶解参数 |  |
|  | Dissolve Mask | 溶解纹理贴图 |  |
| Edge（边缘） | Edge Color Length | 边缘颜色长度 |  |
|  | Edge Color | 边缘颜色 |  |


### 6.3 URP/MK4/Street_Lights
街道光特效，主贴图做背景，自发光纹理做光，相关参数（如高光、光滑度、自发光、扭曲）可调节

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | Albedo | 主贴图 |  |
|  | Albedo Power | Albedo Power参数 |  |
| Emission | Emission | 自发光贴图 |  |
|  | Emission Power | Emission Power参数 |  |
| Background Color | 背景色 |  |  |
| Specular | 高光 |  |  |
| Smoothness | 光滑度 |  |  |
| Distort | 扭曲参数 |  |  |


### 6.4 URP/MK4/Street_Rain_Triplanar
街道雨特效，提供道路、雨、波浪、雾效、路标等效果

| Base Function |  |  |  |
| --- | --- | --- | --- |
| Color | 颜色 |  |  |
| Albedo | 主贴图 |  |  |
| Normal Map | 法线贴图 |  |  |
| Specular Smoothness | 高光光滑度参数贴图 |  |  |
| Specular | 高光缩放 |  |  |
| Smoothness | 光滑度缩放 |  |  |


| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Rain（雨） | Rain Mask | 雨Maks参数 |  |
|  | RainDrops Normal | 雨掉落法线贴图 |  |
|  | Raindrops int | 雨掉落数量 |  |
|  | Raindrops UV Tile | UV平铺 |  |
|  | Rain Speed | 速度 |  |
| Wave（波浪） | Wave Normal | 波浪法线纹理贴图 |  |
|  | Wave Normal int | 法线数量 |  |
|  | Wave Speed | 速度 |  |
|  | Wave UV Tile | UV平铺 |  |
| Road Symbols | 路标纹理贴图 |  |  |
| Fog Intensity | 雾效强度 |  |  |


| Additional Function |  |  |  |
| --- | --- | --- | --- |
| Rendering Mode | 渲染模式 |  |  |
| Use Unity Fog | 使用Unity雾效 |  |  |


为了方便美术将RainMask部分从Specular Smoothness贴图的a通道分割出来，新增如下参数：

![](https://tcs.teambition.net/storage/312tb79acaa3fc23d4597f952c561e930671?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcyNDk4ODg3NCwiaWF0IjoxNzI0Mzg0MDc0LCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMxMnRiNzlhY2FhM2ZjMjNkNDU5N2Y5NTJjNTYxZTkzMDY3MSJ9.PEOiHIB7QZkI0a6bB5NVRZMcL_vvPga7JSaYc7Jayjg&download=%E4%BC%81%E4%B8%9A%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_16819750038464.png)

勾选Split Texture Channel后，Rain Mask Texture的r通道保存rain mask，原本Specular Smoothness贴图的a通道不生效。

例：原本Specular Smoothness贴图的a通道如下，勾选Split Texture Channel后，红色区域将在Rain Mask Texture的r通道生效，蓝色部分在Specular Smoothness贴图的g通道生效。

![](https://tcs.teambition.net/storage/312t172f349de2533b8d56041e86521218ac?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcyNDk4ODg3NCwiaWF0IjoxNzI0Mzg0MDc0LCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMxMnQxNzJmMzQ5ZGUyNTMzYjhkNTYwNDFlODY1MjEyMThhYyJ9.cbDtO_hcobSYX-zjJAXEqTwjj2Lxp7xAp2pqIoi1rWU&download=%E4%BC%81%E4%B8%9A%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_16819751674916.png)



## 7. UIEffect
UIEffect包含以下shader：

|  | Shader Name |  |  |  |
| --- | --- | --- | --- | --- |
| 7.1 | MoleGame/UIEffect/Glitch/DigitalStripe |  |  |  |
| 7.2 | MoleGame/UIEffect/Glitch/DigitalStripeAndScanlineJitter |  |  |  |
| 7.3 | MoleGame/UIEffect/Glitch/ScanLineJitter |  |  |  |
| 7.4 | MoleGame/UIEffect/Glitch/ScanLineJitterAndLineBlock |  |  |  |
| 7.5 | MoleGame/UIEffect/ScanAndMosaic |  |  |  |
| 7.6 | MoleGame/UIEffect/Blend |  |  |  |
| 7.7 | MoleGame/UIEffect |  |  |  |


### 7.1 MoleGame/UIEffect/Glitch/DigitalStripe
UI故障艺术数字条纹效果，可以调教条纹的强度、频率、长度和高度

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Resource | UIEffectResourceData |  |  |
| Intensity | 强度 |  |  |
| Frequency | 频率 |  |  |
| Stripe Length | 条纹长度 |  |  |
| Noise Texture Width | 噪点宽度 |  |  |
| Noise Texture Width | 噪点高度 |  |  |


### 7.2 MoleGame/UIEffect/Glitch/DigitalStripeAndScanlineJitter
UI故障艺术数字条纹和扫描线效果，可以将两种效果融合。可以调教条纹的强度、大小和扫描线的方向、间隔类型、频率

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Resource | UIEffectResourceData |  |  |
| DigitalStripe Parameter | Intensity | 强度 |  |
|  | Stripe Length | 条纹长度 |  |
|  | Noise Texture Width | 噪点宽度 |  |
|  | Noise Texture Width | 噪点高度 |  |
| ScanLineJitter Parameter | Jitter Direction | 抖动方向 |  |
|  | Interval Type | 间隔类型 |  |
|  | Frequency of Scan Line Jitter | 扫描线抖动频率 |  |
|  | Jitter Intensity | 抖动强度 |  |


### 7.3 MoleGame/UIEffect/Glitch/ScanLineJitter
UI故障艺术扫描线抖动效果，可以调节扫描线的方向、间隔类型、频率和强度

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Resource | UIEffectResourceData |  |  |
| Jitter Direction | 抖动方向 |  |  |
| Interval Type | 间隔类型 |  |  |
| Frequency | 扫描线抖动频率 |  |  |
| Jitter Intensity | 抖动强度 |  |  |


### 7.4 MoleGame/UIEffect/Glitch/ScanLineJitterAndLineBlock
UI故障艺术扫描线抖动和线块效果，可以实现两种效果的融合，可以调节扫描线方向、频率和强度，以及线块的方向、频率、数量、宽度、透明度和偏移等

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Resource | UIEffectResourceData |  |  |
| ScanLineJitter Parameter | Jitter Direction | 抖动方向 |  |
|  | Frequency_SL_J | 扫描线抖动频率 |  |
|  | Jitter Intensity | 抖动强度 |  |
| 幅度 | Amount | 幅度 |  |
| 线条数量 | Lines Width | 线条宽度 |  |
|  | Speed | 速度 |  |
| 强度 | Offset | 偏移 |  |
|  | Alpha | 透明度 |  |


### 7.5 MoleGame/UIEffect/ScanAndMosaic
UI故障艺术扫描和马赛克效果，可以调节扫描系数、淡出效果和马赛克的uv长度、密度和频率

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Resource | UIEffectResourceData |  |  |
| Scan Factor | 扫描参数 |  |  |
| Fade Out | 淡出 |  |  |
| Mosaic_U | U数量 |  |  |
| Mosaic_V | V数量 |  |  |
| Mosaic Density | 马赛克密度 |  |  |
| Mosaic Mask Speed | 马赛克速度 |  |  |


### 7.6 MoleGame/UIEffect/Blend
UI颜色混合模式，提供正常、叠加、柔和相加、正片叠底、两倍相乘、变暗、变亮、滤色、线性减淡选项

| Unique Function |  |  |  |
| --- | --- | --- | --- |
| Blend | 颜色混合模式，包含正常、叠加、正片叠底、两倍相乘、变暗、变亮、滤色、线性减淡 |  |  |


### 7.7 MoleGame/UIEffect
UIEffect插件，提供基本的图片效果(像素、灰度、Sepia、Nega）、颜色模式(Multiply、Fill、Add、Subtract)、模糊、外发光效果

具体使用指路=>[**https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60f23d2b41cef6000162e559**](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60f23d2b41cef6000162e559)



## 8. **Additional Function**
| Rendering Mode |  |  |  |
| --- | --- | --- | --- |
| Opaque | 不透明 |  |  |
| Cutout | 要么透明要么不透明 |  |  |
| Fade | 淡出 |  |  |
| Transparent | 透明 |  |  |
| Additive | 叠加 |  |  |


| Use Unity Fog |  |  |  |
| --- | --- | --- | --- |
| Use Unity Fog | 不使用Unity Fog则可以用Fog Intensity调节，使用Unity Fog则Fog Intensity属性消失 |  |  |
| Fog Intensity |  |  |  |


| Reflection Mode |  |  |  |
| --- | --- | --- | --- |
| None | 无 |  |  |
| ReflectionProbes | 使用Reflection Probes反射 |  |  |
| RealTimeReflection | 实时反射 |  |  |


| Iridescence |  |  |  |
| --- | --- | --- | --- |
| None | 无 |  |  |
| Thin-Film | 虹膜效果 | 厚度贴图 | 参数 |
|  |  | 厚度映射范围 | 参数 |
|  |  | 第一层折射率 | 参数 |
|  |  | 第二层折射率 | 参数 |
|  |  | 第二层消光系数 | 参数 |


## 9. 特殊效果
角色的特殊效果包括：进化、冰冻、石化、溶解（死亡）、全息影像和隐身。这些特殊效果通常需要使用UnityActorCtrl脚本统一进行控制。使用方法详见《[**统一控制角色特殊效果**](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/619c9422130974000144b000)》。

| 进化 |  |  |  |
| --- | --- | --- | --- |
| Special Effect Percent | 效果百分比 |  |  |
| TransitionColor | 进化传送线颜色 |  |  |
| TransitionLength | 进化传送线长度 |  |  |
| X->传送线粗细；Y->顶点Y轴偏移；Z->传送线断层大小，W表示模型原点在世界空间Y方向的偏移 | X->传送线粗细；Y->顶点Y轴偏移；Z->传送线断层大小，W表示模型原点在世界空间Y方向的偏移 |  |  |


| 冰冻 |  |  |  |
| --- | --- | --- | --- |
| Special Effect Percent | 效果百分比 |  |  |
| IceReflect | 反射纹理图 |  |  |
| IceDetail | 反射细节纹理贴图 |  |  |
| IceMask | Mask贴图 |  |  |
| FreezeColor | 冰冻颜色 |  |  |
| FresnelBase | 菲尼尔效果基础值 |  |  |
| FresnelScale | 菲尼尔缩放参数 |  |  |
| FresnelSensitive | 菲尼尔强度参数 |  |  |
| FresnelColor | 菲尼尔颜色 |  |  |
| Ice_Sin1 | 波纹数量 |  |  |
| Ice_Degree1 | 波纹边缘锯齿强度1 |  |  |
| Ice_Sin2 | 波纹边缘锯齿粒度 |  |  |
| Ice_Degree2 | 波纹边缘锯齿强度2 |  |  |
| X->传送线粗细；Y->顶点Y轴偏移；Z->传送线断层大小，W表示模型原点在世界空间Y方向的偏移 | X->传送线粗细；Y->顶点Y轴偏移；Z->传送线断层大小，W表示模型原点在世界空间Y方向的偏移 |  |  |


| 石化 |  |  |  |
| --- | --- | --- | --- |
| Special Effect Percent | 效果百分比 |  |  |
| StoneTex | stone纹理贴图 |  |  |
| Ice_Sin1 | 波纹数量 |  |  |
| Ice_Degree1 | 波纹边缘锯齿强度1 |  |  |
| Ice_Sin2 | 波纹边缘锯齿粒度 |  |  |
| Ice_Degree2 | 波纹边缘锯齿强度2 |  |  |
| X->传送线粗细；Y->顶点Y轴偏移；Z->传送线断层大小，W表示模型原点在世界空间Y方向的偏移 | X->传送线粗细；Y->顶点Y轴偏移；Z->传送线断层大小，W表示模型原点在世界空间Y方向的偏移 |  |  |


| 溶解（死亡） |  |  |  |
| --- | --- | --- | --- |
| Special Effect Percent | 效果百分比 |  |  |
| DissolveMask | 溶解Mask贴图 |  |  |
| RampMask | Ramp mask贴图 |  |  |
| Edge | EdgeColor Length | 边缘颜色宽度 |  |
|  | Edge Color | 边缘颜色 |  |


| 全息影像 |  |  |  |
| --- | --- | --- | --- |
| Dither Opacity | 颗粒半透明百分比 |  |  |
| Standard Color Intensity | 颜色强度 |  |  |
| Hologram Color | 全息颜色 |  |  |
| Scaning Line1 Alpha | 扫描线透明度 |  |  |
| Scaning Line1 Speed | 扫描线速度 |  |  |
| Scaning Line1 Frequency | 扫描线频率 |  |  |
| Rim Color | 边缘发光颜色 |  |  |
| Rim Power | 边缘光宽度 |  |  |
| Rim Intensity | 边缘光亮度 |  |  |
| Glitch Speed | 抖动速度 |  |  |
| Glitch Interval | 抖动间隔 |  |  |
| Glitch Offset | 抖动偏移向量 |  |  |
| Glitch Tilling | 抖动位置系数 |  |  |
| Glitch Constant | 抖动范围 |  |  |


| 隐身 |  |  |  |
| --- | --- | --- | --- |
| Special Effect Percent | 效果百分比 |  |  |
| Invisibility Mask | 隐身遮罩贴图，用于隐身过程渐变效果 |  |  |
| Invisibility Color Length | 隐身效果颜色宽度，渐变时隐身与未隐身的边界 |  |  |
| Invisibility Color | 隐身效果颜色 |  |  |


