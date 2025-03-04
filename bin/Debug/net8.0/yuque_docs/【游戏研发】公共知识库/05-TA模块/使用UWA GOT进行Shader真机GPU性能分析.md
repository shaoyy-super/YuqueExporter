# 一、介绍
一般来说对Shader进行性能分析时，除了对代码本身进行静态分析，还需要在真机上实际测试Shader效果，观察GPU及手机整体的表现状况，如帧率、功耗、发热等。UWA GOT即提供了这样一个SDK模块，可方便地嵌入到Unity打的Development Build中，然后在真机上运行此Build并执行记录数据功能。之后再将记录的数据一键上传到UWA GOT网页，即可很快获取到关于统计数据的解读分析。细节介绍可参考[UWA 快速开始 | UWA (uwa4d.com)](https://www.uwa4d.com/doc-main.html?page=quick-start)。



要使用UWA GOT，

1. 首先需要访问[https://www.uwa4d.com/](https://www.uwa4d.com/)并注册自己的账号;
2. 然后将账号的**手机号**或者**邮箱**发给UWA的项目负责人。负责人会**邀请该账号**加入到具体的项目组里。
3. 再次访问[https://www.uwa4d.com/](https://www.uwa4d.com/)便可以看到项目及一系列测试状况。如下图：![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724913280628-269d1e14-b27e-4d4f-bf32-e5fe342fbf3f.png)
4. Shader分析一般只用到其中的**GPU性能分析**栏目，因此再次点击**GPU性能分析**后，便能看到更细节的GPU性能一系列的统计。其中**报告列表**里的记录便是每次真机测试并上传的报告，可点进去查看具体报告。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724913449601-880d7e1a-a78b-49b8-bcd1-44ae2387edb1.png)

# 二、安装及使用
## 安装
UWA GOT本质上是通过在Unity游戏项目中集成UWA的统计SDK，来实现测试包在运行时记录设备硬件状况，然后再提供一个数据上传功能，将数据上传到UWA云端后进行数据分析和解读。

因此安装就是在Unity中集成一个插件，可参考[UWA - 使用说明](https://www.uwa4d.com/main/businessUseDescription.html?engine=unity&service=got&article=0)。

1. 访问[UWA - 使用说明](https://www.uwa4d.com/main/businessUseDescription.html?engine=unity&service=got&article=0)，点击右上角的**SDK下载**来获取插件文件。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724914056395-5fecca5a-cbde-4e70-8f09-bd4fc2798bcf.png)
2. 解压文件后，将其中的**UWA_SDKvxxx.unitypackage**拖入到Unity项目并导入全部文件。(如果要查看vulkan的渲染统计，请使用**UWA_SDKvxxx(Vulkan).unitypackage**)
3. 点击Unity项目菜单栏"Tools -> UWA SDK"，打开UWA工具栏。按需配置UWA的测试模式，一般全部打开即可。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724914420220-6e101c53-a314-4592-bf6c-57c752c7d7c2.png)
4. UWA SDK的安装到此就结束了。<font style="color:#DF2A3F;">此项目仅能用来测试，如果要发布项目，需要删掉UWA SDK的文件夹或者在Player Settings中添加DISABLE_UWA_SDK宏定义。</font>

<font style="color:#DF2A3F;"></font>

## 使用
使用分为两部分，首先是将需要测试的Unity项目打成调试包。其次才是在真机上运行调试包，并记录上传统计数据。

### 打包
1. 一般测试单个Shader时，建议直接新建一个空工程，场景中放一个plane，垂直于主相机视线，材质着色器设为需要测试的Shader，属性按需调整。
2. 主相机确保只显示plane的画面，可以不显示全plane，但不要显示plane外的天空盒。可以将**Game视窗**分辨率设为**1920x1080 Landscape**来观察。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724914993141-c07e0c0f-c855-4005-b4a2-6ea580c9ac6e.png)
3. 点击File -> Build Settings调整打包配置：
    1. 安卓平台：
        1. 首先切换平台到Android；
        2. 然后勾上**Development Build**，UWA GOT只支持Development模式。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724915876779-28630eba-8562-4250-ba53-64ec56818400.png)
        3. 点击左下角**Player Settings**，确保**Other Settings**里的**Graphics APIs**中**OpenGLES3**在Vulkan前面。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724915912509-cd19696d-6a83-4463-a50e-8171644b2c8f.png)
        4. 点击Other Settings上面的**Resolution and Presentation**，找到Orientation，将**Default Orientation**设置为**Landscape Right**。这是为了将手机设置为横屏显示游戏，这样才能确保游戏画面完全充满手机屏幕。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724915942042-ec36b962-6103-4422-bc02-e7af3251703f.png)
        5. 点击**Quality**，再点击最上面的**Balanced**，确保当前是在给**Android**平台进行设置。然后仔细检查一下安卓平台 Balanced level下的**RenderPipelineAsset**是否有设置正确，如**RenderScale**、**Vsync**等。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724996847958-ddbf143d-cc5e-4606-b098-7037147a7531.png)
4. 点击**Build Settings**的**Build**按钮进行打包。（如果此时电脑连着手机可以直接点击**Build And Run**，这样可以省去手动安装Apk的步骤，也能跳过一些手机安装Apk要求输入账密的过程。）

### 运行并采集、上传数据
+ ps. 手机上运行时注意下**手机是否自带屏幕分辨率**设置。华为系列手机有可能在操作系统层面将分辨率锁在一个较低的值，到时候会比较难判断是游戏设置还是手机设置引起的问题。
1. 启动刚打包的应用，会看到应用最上面多了一行UWA的UI；如果弹窗GPU某个指标不支持，需要查看UWA GPU 支持文档，来查看UWA GOT SDK对不同设备的支持状况[GOT Online支持设备列表](https://www.uwa4d.com/main/supported.html)。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724916190217-b804de88-17b1-4509-8b64-de6a1990de90.png)
2. 点击**GOT**，然后会弹出更具体的模式选择，此时如果是第一次进入则需要选择**齿轮**按钮，进行模式配置。如果之前已经配置过，则直接点击**GPU**按钮即可。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724916322752-69f049b2-1c3b-4164-b23d-0755a30c94e1.png)
3. 点击**齿轮**按钮后，再点击到**GPU**栏目，下面会有两个资源分析的开关，一般都开着。然后点击**Save & Start**即可启动统计。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724916569947-a5c0ecaa-cefa-4496-9869-4bb77b293c3e.png)
4. 开始统计后，至少需要保持应用运行60秒，然后才能点击左上角GPU右边的三角符号展开结束的UI。![](https://cdn.nlark.com/yuque/0/2024/jpeg/1660870/1724916651039-88c26556-0975-4702-b362-895b284f624c.jpeg)
5. 点击展开UI后的红色Stop按钮，即可结束统计；![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724916728622-5259bd4d-18de-49d5-b207-30408d6fafd9.png)
6. 结束统计后，会自动弹出要求上传数据的弹窗。需要登录到自己的UWA账号上，上面栏目保持勾选Online即可；![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724916771578-cab088d8-6877-4fa1-990f-8a3f1d293791.png)
7. 然后需要选择这份测试记录所属的项目。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724916830511-d06701ef-ab5d-4ba9-8d33-7618c8791ad6.png)
8. 之后点击提交数据，并**恰当填写备注**，**说明本次测试记录是测试什么shader，环境如何**即可。
9. 上传完毕后，数据采集工作即完成了。后面就是访问UWA的网站，然后找到自己上传的测试记录，并查看性能报告。

# 三、报告解读
访问UWA网站，并点开自己上传的记录后，就可以具体分析此次性能报告。下面将以**URP/Lit**）的性能报告来举例说明各项参数的意义：

## 术语说明
+ GPU时钟周期 = 1 / GPU主频，即完成一个时钟周期需要的时间；
+ GPU主频：1s内能执行多少次时钟周期。

[什么是时钟脉冲，CPU为什么需要时钟，时钟信号是怎么产生的？_cpu的脉冲信号是什么产生的-CSDN博客](https://blog.csdn.net/weixin_44395686/article/details/105318472)

## 性能简报
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724918250242-b74a92f9-66ed-49c6-ac7c-7e14111c6dd8.png)

+ **GPU Bound 帧数占比**：表示GPU性能高压区间，此时GPU耗费的时钟周期数过高，导致可能无法支持满帧运作。算法：GPU Clocks * TargetFPS >= GPU MaxFreq * 80%. 符合条件时，这一帧就被标记为GPU Bound，背景设成浅红色。此数越小越好。



## GPU分析
### GPU渲染分析![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983444275-7322c9b8-f946-4862-955f-046bbeb144eb.png)
+ **GPU Clocks**: 平均每帧的GPU时钟周期数。
+ **GPU Shaded**: 平均每帧的Fragment Shader总执行次数。与分辨率、每个像素点被渲染了几遍相关。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983468492-73424706-45f5-4fcd-a708-7e303c721d4e.png)
+ **GPU Shader Cycles**: 平均每帧所有涉及到Shader处理的GPU时钟周期数。该值是当前帧所有用到的Shader的复杂度的体现。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983494643-738cb32e-5d24-4111-b67c-585e4f54cd6e.png)
    - Shader Interpolator Cycles: 对应mali offiline comipler中的V - Varying Unit
    - Shader Arithmetic Cycles: 对应mali中的A - Arithmetic Unit
    - Shader LoadStore Cycles: 对应mali中的LS - Load/Store Unit
    - Shader Texture Cyles: 对应mali中的T - Texture Unit

> GPU Shader Cycles比GPU Clocks多是因为GPU是多单元并行计算，所以其平均每帧总计算量应该是单元数*平均每帧的时钟周期数。然后GPU Shader Cycles是指平均每帧花费在shader上的计算，那么这个数字就会介于平均每帧总计算量和GPU Clocks之间，因为GPU Clocks = 1 * 平均每帧的时钟周期数。
>

+ **GPU Shader Instructions**: 平均每帧GPU执行的Shader指令数均值。Shader指令包括FMA，CVT，SFU，MSG等。
+ **GPU Primitive**: 平均每帧GPU处理的总图元数。该值代表输入GPU的总三角面片数，与渲染模块中Triangle数一致。Input Primitives与Unity Statistics中的Tris三角形数基本一致。
+ **FPS**: 实际平均每秒渲染的帧数。
+ **GPU Freq**: GPU频率均值。该值每隔固定帧数采样一次。



#### 计算分析
> Case:
>
> ![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725020387416-7740e9fa-8a38-4d7c-b1f5-11199b6aed6f.png)
>
> 测试时记录了下同一材质不同RenderScale的性能状况。可以发现renderscale不同时，fragment shaded并未按预想的变化。比如renderscale = 1，shaded: 782，renderscale = 2, shaded: 1545，大概是两倍，但预期应该是4倍。这是因为Fragment Shaded里包含了不跟随renderscale变化的定额消耗，或者说固定消耗。然后可以大概算出这个固定消耗，之后再去计算要测试的shader的真正消耗。
>

> 设固定差值为X，renderscale = 1时要测试shader的消耗为y。则有如下关系：
>
> x + 0.1 * 0.1 * y = 526;
>
> x + 0.5 * 0.5 * y = 589;
>
> x + 1 * 1 * y = 782;
>
> x + 2 * 2 * y = 1545;
>
> 可以解出:
>
> x = ~525(万个)
>
> y = ~258(万个)
>
> 所以这个shader在此情况下的真实frag shaded大概在258万个。
>

| RenderScale | Fragment Shaded(万个) | 差值(万个) | y(万个) | x(万个) |
| --- | --- | --- | --- | --- |
| 0.1 | 526 | 589 - 526 = 63 | 63 / (0.25 - 0.01) = 262.5 | 523.375 |
| 0.5 | 589 | 782 - 589 = 193 | 193 / (1 - 0.25) = 257.333 | 524.66675 |
| 1 | 782 | 1545 - 782 = 763 | 763 / (4 - 1) = 254.333 | 527.667 |
| 2 | 1545 | | | |




**对比分析Urp/lit和Cloth/Dyeing_lit:**

**URP/Lit:**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725022642536-832ad5c8-ab43-4f8e-8c02-2a41b4f0060a.png)![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725022649261-5d322fb6-32c7-47ac-ad4a-9a6c557f22ff.png)

对应静态性能报告：

```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Fragment

Main shader
===========

Work registers: 31 (96% used at 100% occupancy)
Uniform registers: 66 (103% used)
Stack spilling: false
16-bit arithmetic: 38%

                                A      LS       V       T    Bound
Total instruction cycles:    5.30    0.00    1.88    3.00        A
Shortest path cycles:        5.30    0.00    1.88    3.00        A
Longest path cycles:         5.30    0.00    1.88    3.00        A

A = Arithmetic, LS = Load/Store, V = Varying, T = Texture

Shader properties
=================

Has uniform computation: true
Has side-effects: false
Modifies coverage: false
Uses late ZS test: false
Uses late ZS update: false
Reads color buffer: false

Note: This tool shows only the shader-visible property state.
API configuration may also impact the value of some properties.


```

算得：

+ urp/lit的**fragment shaded** = **257万个左右；**
+ **shader cycles** = **31.8百万Cycles；**
    - **arithmetic cycles = 16.82百万Cycles;           **
    - **loadstore cycles = 0.01百万Cycles;**
    - **interpolator cycles = 5.74百万Cycles;          **
    - **texture cycles = 9.17百万Cycles;                  **
+ **shader instrucitons** = **16.8百万个； **

对比真机和静态性能单位**：**

+ **16.82 / 5.3 = ~3.17**
+ **5.74 / 1.88 = ~3.05**
+ **9.17 / 3 = ~3.05**
+ **单位均大约为3左右，符合预期。**

****

**Cloth/Dyeing_lit:**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725022604343-7482f0b7-d323-4ef7-bf1d-98c2ea831f98.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725022616951-d68a3b6d-2ae5-4fc4-b352-e37aecd415bb.png)

对应静态性能报告:

```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Fragment

Main shader
===========

Work registers: 31 (96% used at 100% occupancy)
Uniform registers: 128 (200% used)
Stack spilling: false
16-bit arithmetic: 48%

                                A      LS       V       T    Bound
Total instruction cycles:    9.92    0.00    1.88    4.50        A
Shortest path cycles:        9.92    0.00    1.88    4.50        A
Longest path cycles:         9.92    0.00    1.88    4.50        A

A = Arithmetic, LS = Load/Store, V = Varying, T = Texture

Shader properties
=================

Has uniform computation: true
Has side-effects: false
Modifies coverage: false
Uses late ZS test: false
Uses late ZS update: false
Reads color buffer: false

Note: This tool shows only the shader-visible property state.
API configuration may also impact the value of some properties.


```

算得：

+ cloth/dyeing_lit的**fragment shaded** = **257万个左右；**
+ **shader cycles** = **48百万Cycles；**
    - **arithmetic cycles = 29百万Cycles;**
    - **loadstore cycles = 0.01百万Cycles;**
    - **interpolator cycles = 5.74百万Cycles;**
    - **texture cycles = 13.47百万Cycles;**
+ **shader instrucitons** = **29.3百万个； **

对比真机和静态性能单位**：**

+ **29 / 9.92 = ~2.92**
+ **5.74 / 1.88 = ~3.05**
+ **13.47 / 4.5 = ~2.99**
+ **单位均大约为3左右，符合预期。**



**对比结论：**

1. 两者的fragment shader执行次数相差无几，符合预期；
2. dyeing的gpu clocks、gpu freq要高于urp/lit的，证明dyeing的gpu压力更大；
3. 两者的gpu primitive差不多，因为只改变材质没改变模型和视角，符合预期；
4. dyeing的gpu shader cycles和gpu shader instructions均高于urp/lit的，证明dyeing shader的指令复杂度更高。
5. **结合真机和静态性能报告**来看，只要真机测试所采用的shader关键字与静态性能分析时采用的**关键字组合相似**，且清楚shader中各种**for循环的具体次数**（比如场景中有几盏间接光），则**静态性能报告可以提供相当准确的性能预估**。



**对比编译后的源码来寻找优化空间**：

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725520064863-ca9f1b82-6b15-4db4-a7ff-c774a1fa1a2e.png)

Shader分析对比，首先确认变量部分有没有什么明显差异，可以优化的变量。然后主要就是对比main函数里的部分。

以上图特定关键字下的urp/lit和cloth/dyeing_lit对比来看：

1. 开头initialize surfaceData的部分，能明显看出urp/lit基本就是采样贴图，然后将值传给surfaceData。而dyeing_lit则需要多采样3套染色区域的法线图，以及将染色区域的金属、粗糙度、颜色和不染色区域混合。
    1. 其中，混合颜色、金属、粗糙度只占了比较小的部分，大概15行代码左右。这部分是必需做的，且优化空间不大。
    2. 混合法线图部分，因为要重复采样3套法线图且将它们混合起来，所以这里存在不少指令的重复操作。从BumpMap采样后，一直到Occlusion计算之前，就全都是法线的混合计算。相比urp/lit的一套法线，dyeing_lit多出了三套细节法线的计算以及混合，而且每套法线的计算，如normalize等，都是同样的逻辑，因此这里可能有不少的空间可以优化，将三套法线分开的计算混合，变成一次同时对三套法线进行混合再normalize，或者是别的思路。
        1. 这里可以细节分析一下Unity Shader函数对应编译后代码的典型例子：
            1. UnpackNormal

```cpp
real3 UnpackNormalAG(real4 packedNormal, real scale = 1.0)
{
    real3 normal;
    normal.xy = packedNormal.ag * 2.0 - 1.0;
    normal.z = max(1.0e-16, sqrt(1.0 - saturate(dot(normal.xy, normal.xy))));
    normal.xy *= scale;
    return normal;
}
```

```cpp
// 法线贴图采样
u_xlat16_0.xy = texture(_Mask1DetailNormalMap, u_xlat0.xy, _GlobalMipBias.x).yw;

// UnpackNormal
u_xlat16_20.xy = u_xlat16_0.yx * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
u_xlat16_50 = dot(u_xlat16_20.xy, u_xlat16_20.xy);
u_xlat16_50 = min(u_xlat16_50, 1.0);
u_xlat16_50 = (-u_xlat16_50) + 1.0;
u_xlat16_50 = sqrt(u_xlat16_50);
u_xlat16_7.z = max(u_xlat16_50, 1.00000002e-16);
u_xlat16_7.xy = u_xlat16_20.xy * vec2(_Mask1DetailNormalStrength);
```

            2. normalize

```cpp
detailNormalTS = normalize(detailNormalTS);
```

```cpp
// u_xlat16_7.xyz是上面UnpackNormal的结果
// normalize
u_xlat16_20.x = dot(u_xlat16_7.xyz, u_xlat16_7.xyz);
u_xlat16_20.x = inversesqrt(u_xlat16_20.x);
u_xlat16_20.xyz = u_xlat16_20.xxx * u_xlat16_7.xyz;
```

            3. BlendNormalRNM

```cpp
real3 BlendNormalRNM(real3 n1, real3 n2)
{
    real3 t = n1.xyz + real3(0.0, 0.0, 1.0);
    real3 u = n2.xyz * real3(-1.0, -1.0, 1.0);
    real3 r = (t / t.z) * dot(t, u) - u;
    return r;
}
```

```cpp
// u_xlat16_6.xyz是上一次法线贴图混合的结果法线，对于第一次混合来说就是基础法线贴图的法线
// u_xlat16_20.xyz是上面normalize的结果
// BlendNormalRNM
u_xlat16_7.xyz = u_xlat16_6.xyz + vec3(0.0, 0.0, 1.0);
u_xlat16_20.xyz = u_xlat16_20.xyz * vec3(-1.0, -1.0, 1.0);
u_xlat16_8.xyz = u_xlat16_7.xyz / u_xlat16_7.zzz;
u_xlat16_51 = dot(u_xlat16_7.xyz, u_xlat16_20.xyz);
u_xlat16_20.xyz = u_xlat16_8.xyz * vec3(u_xlat16_51) + (-u_xlat16_20.xyz);
```

            4. lerp

```cpp
return lerp(normalTS, BlendNormalRNM(normalTS, detailNormalTS), detailMask);
```

```cpp
// u_xlat16_6.xyz是上一次法线贴图混合的结果
// u_xlat16_20.xyz是BlendNormalRNM(normalTS, detailNormalTS)的结果
// lerp(x, y, m)
u_xlat16_20.xyz = (-u_xlat16_6.xyz) + u_xlat16_20.xyz;
u_xlat16_20.xyz = u_xlat16_3.xxx * u_xlat16_20.xyz + u_xlat16_6.xyz;

// lerp(x, y, m) = y * m + (1 - m) * x
//               = y * m + x - m * x
//               = m * (y - x) + x								#1
// 设 x = u_xlat16_6.xyz, y = u_xlat16_20, m = u_xlat16_3.xxx。
// 上面frag中第二行等号左边的u_xlat16_20并不具有意义，而是一个新的变量，
// 只是刚好u_xlat16_20后面没用了，所以用u_xlat16_20来存储新的变量。 
// 所以设 ans = 第二行左边的u_xlat16_20.xyz。
// 可得：
// 			  y' = -x + y
// 			 ans = m * y' + x
//           	 = m * (-x + y) + x
//               = m * (y - x) + x 								#2
//            #1 = #2
// 所以证明得到lerp(x, y, m)编译后的形式就是上述frag两行。
```

        2. 剩下的问题就是看如何将重复的这3次逻辑展开然后摊平到一次计算中，来尽量减少重复的函数调用。这个后续具体优化时再说明。
    3. 法线图那边还能看到采样后都只用了法线图的两个通道，然后z值是根据xy计算出来的，这样工作量又多了一些。查看unity设置后发现是normal map encoding用了DXT5nm-style，而非XYZ。用XYZ的格式就能确保法线图直接采样三通道，来省去z通道的计算量。
2. 相比urp/lit，dyeing_lit多了一个计算自发光的步骤，而urp是直接赋值的。编译器自动优化将 emssion前半部分的结果 * _EmissionStrength放到main()的最后计算了。
3. bakeGI那里，urp/lit用关键字屏蔽掉了。dyeing lit则是默认走的bakegi计算。



**优化建议：**

1. 关于gpu shader cycles，细化计算不同计算单元的差异后发现，可以主要聚焦**Arithmetic算术指令**的优化，将一些可以合并的计算合并。尽量避免逻辑上重复的计算，而是采取先准备数据，然后统一计算，这种对计算友好的代码方式，类似simd这种思路：一条指令同时对多个数据执行，而非每条数据执行自己的指令。
2. 其次便是减少**texture采样指令**。



### GPU带宽分析
+ **GPU Bandwidth**: GPU读、写带宽较高时会造成大量发热和耗电。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983523961-2be6f06a-2a9d-4faf-a692-60554003490d.png)
    - **Read Total**: 数据读取。大小取决于gpu单元每秒的数据读取量、L1、L2缓存的命中率。缓存命中率越高，Read Total越小。
        * **Front-end Read**: 对应GPU的Tile Unit， Tile List数据；(Tile-Based-Rendering架构下才有意义)
        * **LoadStore Read**: 对应GPU的Load/Store Unit，顶点输入属性数据、Uniform数据、颜色/深度数据；
        * **Texture Read**: 对应GPU的Texture Unit，纹理数据的读取。
+ **GPU Stall**: GPU停滞。平均每帧GPU发起外部请求到响应该请求的GPU时钟周期占当前帧GPU总时钟周期的本分比。这个比例越小越好，越大代表Cache Miss率越高，反映的是GPU带宽压力。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983534370-a91a4868-ec4d-4020-85f1-a9cc3eaa1ec8.png)
    - 一种是CPU无法及时提供足够的顶点给GPU。或者GPU卡在了复杂的像素着色阶段。
    - 另一种是GPU从内存中读写数据时，由于缓存未命中或者带宽限制，导致读写延迟。
    - CPU和GPU间的同步操作，GPU和GPU之间的同步。



## 渲染统计
主要查看DrawCall和Triangle数目。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983559263-6b3697c3-7757-4a21-9875-e1a6f3147528.png)

## 渲染资源分析
+ 可以筛选出未利用却被打包的资源。
+ 可以查看各资源实际的渲染利用率（该资源参与渲染的采样点占该资源在内存中的采样点的比例，类似于可见比例）。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983589003-4c659f80-62c2-437b-8c63-66b3ec23a335.png)![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983601003-3e1562a0-9be2-4878-8f91-3e4c678783ea.png)

## Overdraw
表示项目运行过程中整个屏幕被填充的倍数，是个估计值。

算法：**Fragment Shaded **/** 固定分辨率(1920 * 1080) **得出。统计了后处理在内的一些渲染，也包括BlitCopy，如Copy Depth，Copy Color等。



在用GOT真机采集时，可以手动点击UI里蓝色的**Dump(Overdraw)**来采集场景中具体的Overdraw情况。可以看到不同相机各自的Overdraw数值。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983627250-a1b59c8a-851e-471f-b111-90e0f0ead8f4.png)

## GPU温度
+ 注意一点，由于手机中GPU和CPU靠得很近，可能出现CPU发热严重，继而带动GPU发热的状况。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1724983645056-2101fbfc-c8a3-44c3-b3e5-cd3f75735910.png)

# 参考
[UWA 快速开始 | UWA (uwa4d.com)](https://www.uwa4d.com/doc-main.html?page=quick-start)

[UWA - 使用说明](https://www.uwa4d.com/main/businessUseDescription.html?engine=unity&service=got&article=0)

[GOT Online支持设备列表](https://www.uwa4d.com/main/supported.html)

[功能上新｜全新GPU性能优化方案 - UWA问答 | 博客 | 游戏及VR应用性能优化记录分享 | 侑虎科技 (uwa4d.com)](https://blog.uwa4d.com/archives/UWA_GOTOnline12.html)

[如何读懂UWA性能报告？—渲染篇 - UWA问答 | 博客 | 游戏及VR应用性能优化记录分享 | 侑虎科技 (uwa4d.com)](https://blog.uwa4d.com/archives/Simple_PA_Rendering.html)

[关于Unity渲染优化，你可能遇到这些问题 - UWA问答 | 博客 | 游戏及VR应用性能优化记录分享 | 侑虎科技 (uwa4d.com)](https://blog.uwa4d.com/archives/QA_Rendering.html)

[扒一扒Profiler中这几个“占坑鬼” - UWA问答 | 博客 | 游戏及VR应用性能优化记录分享 | 侑虎科技 (uwa4d.com)](https://blog.uwa4d.com/archives/presentandsync.html)

[移动GPU体系结构 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/444083670)

