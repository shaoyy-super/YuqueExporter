# 1 工具介绍（5.6 Windows版）
Mali™ Offline Compiler是一个命令行工具，为使用ESSL（OpenGL ES Shading Language），或者OpenCL C编写的shader提供静态分析功能。

它可以用来：

+ 确认shader的语法是否正确
+ 识别性能瓶颈
+ 测量任何改动对性能的影响

## 1.1 支持的API
Mali™ Offline Compiler支持以下API版本：

+ OpenGL ES 2.0和3.0-3.2
+ OpenCL 1.0-1.2

## 1.2 支持的驱动版本
| **Mali-400 系列Driver（Utgard架构）** | **Mali-T600 系列Driver（Midgard架构）** |
| :--- | :--- |
| r4p0-00rel1<br/>r5p0-01rel0<br/>r6p1-00rel0<br/>r7p0-00rel0 | r4p0-00rel0<br/>r4p1-00rel0<br/>r5p0-00rel0<br/>r5p1-00rel0<br/>r6p0-00rel0<br/>r7p0-00rel0<br/>r8p0-00rel0<br/>r9p0-00rel0<br/>r10p0-00rel0<br/>r11p0-00rel0<br/>r12p0-00rel0<br/>r13p0-00rel0 |


注：7.4版本已经支持Bifrost和Valhall架构

## 1.3 安装
直接解压安装包即可。为了方便使用，可以把解压目录添加到path环境变量中。

注：从7.0以后开始集成到Arm Mobile Studio中，没有单独安装包。

## 1.4 使用
### 1.4.1 准备源代码
推荐使用约定的扩展名来表示着色器类型：

| **扩展名** | **shader类型** |
| :--- | :--- |
| .vert | vertex |
| .frag | fragment |
| .comp | compute |


这样，即使不指定类型参数，编译器也能根据扩展名判断shader的类型。

如果想分析Unity的shader，需要将代码做一个转换：

首先在Unity中打开待分析shader的Inspector，点击黄框按钮，将弹出面板设置为下图的状态（只勾选了 GLES 3x）：

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1720581017881-e6a713a1-32ff-40ec-9eab-082795a9ce59.png)

点击“Compile and show code”按钮，Unity会开始编译该Shader的各个变体的“预编译版本”，编译完成后会自动弹出预编译版本的.shader文件，其中每个变体都有对应的vertex和 fragment代码，分别以“#ifdef VERTEX”和“#ifdef FRAGMENT”作为开头。可以把该宏包括的代码复制出来（不要包括宏本身）单独保存到一份文件里作为源码。假设源码路径是<path_to_shader_src>。

### 1.4.2 常用参数设置
| **参数** | **说明** |
| :--- | :--- |
| -d或 --driver | 目标驱动，默认Mali-T600_r13p0-00rel0 |
| -c或 --core | 目标硬件核心，默认Mali-T880 |
| -r或 --evision | 目标硬件release和patch，默认r2p0 |
| -f或 --fragment | 指定shader类型为fragment |
| -v或 --vertex | 指定shader类型为vertex |
| -C或 --compute | 指定shader类型为compute |


### 1.4.3 完整命令示例
<font style="background-color:#D8DAD9;">malisc.exe -f -c Mali-T880 -dMali-T600_r13p0-00rel0 -r r2p0 <path_to_shader_src></font>

如果文件扩展名已经按照约定的方式命名，并且使用默认的目标硬件平台，那么前面的参数都是可以省略的，可以写作：

<font style="background-color:#D8DAD9;">malisc.exe <path_to_shader_src></font>

## 1.5 分析报告
命令执行完后，会打印出性能分析报告：

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1720581018223-9e72920f-702e-4815-8a4b-90e82fbf8422.png)

可以看到，分析报告主要包括两部分——寄存器信息和性能表。

### 1.5.1 寄存器信息
寄存器信息会列出使用的寄存器数量，是否有寄存器溢出。

需要尽量限制寄存器的使用，因为寄存器使用越多，最大的线程数越少，会影响GPU并行处理能力。

对于Midgard架构，寄存器数量和最大线程数的关系如下：

| **寄存器数量** | **最大线程数** |
| :--- | :--- |
| 0-4 | Max |
| 5-8 | Max/2 |
| 8-16 | Max/4 |


补充：

Bifrost架构，寄存器数量和最大线程数的关系：

| **寄存器数量** | **最大线程数** |
| :--- | :--- |
| 0-32 | Max |
| 32-64 | Max/2 |


Valhall架构，寄存器数量和最大线程数的关系：

| **寄存器数量** | **最大线程数** |
| :--- | :--- |
| 0-32 | Max |
| 32-64 | Max/2 |


如果寄存器溢出到栈上，GPU处理起来开销会比较大，一般优化的方法是：

+ 降低变量精度
+ 减少变量的活跃区间
+ 简化shader程序

### 1.5.2 性能表
性能表包含三行：

1. **Instructions Emitted**

不考虑程序的控制流，统计执行所有程序产生的指令所需的周期总数。

2. **Shortest Path Cycles**

估算shader程序最短的控制流所需的指令周期。

3. **Longest Path Cycles**

估算shader程序最长控制流所需的指令周期，通常我们比较关心这个指标。需要注意的是并不是所有时候都能够估算最长指令周期，比如循环的次数如果由uniform变量来控制的时候就无法估算，此项会显示N/A。

统计数据根据功能单元来划分：计算（A）、Load/Store（L/S）和纹理（T），另外每行还会列出瓶颈（Bound）。

根据这些数据，我们可以看出那种类型的操作导致性能瓶颈，并针对性的做出优化方案。

可能采取的优化方式如下：

**降低计算负载**

1. 降低精度——mediump计算能够比highp计算快一倍

2. 避免分歧的分支 - warp线程中分歧的分支会降低运算效率，因为当执行有分歧的代码路径时，不是所有的线程都被激活。

3. 向量化操作 - Mali Midgard GPU使用SIMD算数逻辑，所以源码中矩阵和向量操作比标量操作效率更高。

4. 将逐片元处理移至逐顶点处理，以降低运算频率

**降低L/S负载**

1. 提升访问密度，可以在compute shader中使用向量加载，访问模式可以是在每个warp中访问相邻的线程的相邻数据。这样能够使多线程能够以单独的cache line访问返回数据。

2. 减少cache压力，通过减少精度和提升访问的空间局部性

3. 对只读纹理的访问，使用texture()代替imageLoad()调用。

4. 避免使用原子调用，因为它们每线程开销很高。

**降低纹理负载**

1. 降低滤波质量

2. 双线性滤波的速度是三线性滤波的2倍

3. 降低各向异性最大值，限制每个shader操作纹理采样的次数

4. 降低采样精度——mediump采样器类型比highp类型需要更少的数据

# 2 案例分析
## 2.1 不同shader变体的性能差异
以URP/MoleGame/Effects/General为例：

在Unity中打开该shader的Inspector，点击“Compile and show code”，生成临时的shader文件，在项目Temp目录下可以找到“Compiled-URP-MoleGame-Effects-General.shader”。

寻找没有任何关键字的变体，开头为：

<font style="background-color:#D8DAD9;">Global Keywords: <none></font>

<font style="background-color:#D8DAD9;">Local Keywords: <none></font>

向下查找关键字“#ifdef FRAGMENT”，复制出宏包围的代码，保存到effect.frag文件中。

然后寻找激活了极坐标特性的变体，开头为：

<font style="background-color:#D8DAD9;">Global Keywords: <none></font>

<font style="background-color:#D8DAD9;">Local Keywords: _POLAR_COORDINATES_ON</font>

类似操作将fragment代码保存到effect1.frag文件中。

执行：

<font style="background-color:#D8DAD9;">malisc.exe effect.frag</font>

结果为：

```properties
5 work registers used, 14 uniform registersused, spilling not used.
                        A       L/S    T       Bound
Instructions Emitted:   50     8       3       A
Shortest Path Cycles:   12     5       1       A
Longest Path Cycles:    16     8       3       A
```

执行：

<font style="background-color:#D8DAD9;">malisc.exe effect1.frag</font>

结果为：

```properties
5 work registers used, 16 uniform registersused, spilling not used.
                        A       L/S    T       Bound
Instructions Emitted:   76     8       3       A
Shortest Path Cycles:   20     5       1       A
Longest Path Cycles:    25     8       3       A
```

可以看到，添加了极坐标特性后，uniform寄存器增加了，说明有更多的参数；A列数据也增加了，说明极坐标功能会有更多的计算。

## 2.2 FAStandard与URP Lit shader的性能差异
### 2.2.1 Universal Render Pipeline/Lit性能
筛选“Universal Render Pipeline/Lit”的变体代码，包含以下关键字：

<font style="background-color:#D8DAD9;">Global Keywords: _MAIN_LIGHT_SHADOWS _ADDITIONAL_LIGHTS</font>

<font style="background-color:#D8DAD9;">Local Keywords: _ALPHATEST_ON _NORMALMAP _METALLICSPECGLOSSMAP</font>

分析结果为：

```properties
8 work registers used, 10 uniform registersused, spilling not used.
                        A       L/S    T       Bound
Instructions Emitted:   73     18      5       A
Shortest Path Cycles:   1      1       1       A, L/S, T
Longest Path Cycles:    1      N/A     N/A     N/A
```

Shortest Path Cycles的数值只有1，是因为程序中在开始有一个discard调用，所以最短路径就是跳过绘制。

Longest Path Cycles的数值包含N/A是由于程序中包含不确定次数的循环，将循环次数修改为0次的结果如下：

```properties
6 work registers used, 9 uniform registers used, spilling not used.

                        A       L/S     T       Bound
Instructions Emitted:   38      6       4       A
Shortest Path Cycles:   1       1       1       A, L/S, T
Longest Path Cycles:    13      6       4       A
```

循环次数改为1次，结果为：

```properties
8 work registers used, 10 uniform registers used, spilling not used.

                        A       L/S     T       Bound
Instructions Emitted:   72      18      5       A
Shortest Path Cycles:   1       1       1       A, L/S, T
Longest Path Cycles:    24      18      5       A
```

### 2.2.2 URP/MoleGame/PBR/FAStandard(NC)性能
筛选“URP/MoleGame/PBR/FAStandard(NC)”的变体代码，包含以下关键字：

<font style="background-color:#D8DAD9;">Global Keywords:_CUSTOM_SCREEN_SPACE_OCCLUSION _MAIN_LIGHT_SHADOWS _RECEIVE_SELF_SHADOW </font>

<font style="background-color:#D8DAD9;">Local Keywords: _ALPHATEST_ON</font>

将代码中for循环改为0次，分析结果为：

```properties
8 work registers used, 16 uniform registers used, spilling used.

                        A       L/S     T       Bound
Instructions Emitted:   140     70      8       A
Shortest Path Cycles:   3.6     35      0       L/S
Longest Path Cycles:    1       N/A     N/A     N/A
```



得到的结果中仍然存在N/A，检查代码，发现除了for循环外，还包括若干类似下面的逻辑：

```c
while(true){   
    u_xlati47 = int(1 << int(u_xlatu15.y)); 
    u_xlati47 = int(uint(u_xlatu22.x& uint(u_xlati47))); 
    if(u_xlati47 ! = 0) {break;}  
    u_xlatu15.y = u_xlatu15.y + 1u;
}
```



将其改为保留最大路径：

```c
//while(true){    
    u_xlati47 = int(1 << int(u_xlatu15.y));   
    u_xlati47 = int(uint(u_xlatu22.x& uint(u_xlati47)));    
    //if(u_xlati47 ! = 0) {break;}    
    u_xlatu15.y = u_xlatu15.y + 1u;
//}
```



结果为：

```properties
8 work registers used, 16 uniform registers used, spilling used.

                        A       L/S     T       Bound
Instructions Emitted:   140     58      8       A
Shortest Path Cycles:   3.6     35      0       L/S
Longest Path Cycles:    45      58      8       L/S
```



将for循环次数改为1次，结果为

```properties
8 work registers used, 16 uniform registers used, spilling used.

                        A       L/S     T       Bound
Instructions Emitted:   160     70      8       A
Shortest Path Cycles:   3.6     35      0       L/S
Longest Path Cycles:    53      70      8       L/S
```



首先比较寄存器方面：URP Lit没有寄存器溢出，而FAStandard存在寄存器溢出（spilling used）。

然后比较指令周期，将Longest Path Cycles的A单元变化数据统计在下表：

|  | 循环0次 | 循环1次 | 周期数变化 |
| --- | :--- | :--- | :--- |
| URP Lit | 13 | 24 | 11 |
| FAStandard | 45 | 53 | 8 |


分析表中数据，发现在普通的PBR渲染中，FAStandard与URP Lit shader性能存在较大差距，但是循环逻辑的周期数差距很小，所以推断性能差距主要是在循环以外的逻辑。

### 2.2.3 一些优化尝试
取for循环1次的结果，对比URP Lit和FAStandard的Shortest Path Cycles行，L/S的数值分别为1和35，差距很大，而且两者代码开始都有discard调用，discard之前的逻辑都不长，所以推断，在discard之前可能存在优化空间。FAStandard在discard调用前的代码如下：

```c
...
vec4 TempArray0[16];
...
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlat0 = ((gl_FrontFacing ? 0xffffffffu : uint(0)) != uint(0)) ? 1.0 : -1.0;
    TempArray0[0].x = 0.0588235296;
    TempArray0[1].x = 0.529411793;
    TempArray0[2].x = 0.176470593;
    TempArray0[3].x = 0.647058845;
    TempArray0[4].x = 0.764705896;
    TempArray0[5].x = 0.294117659;
    TempArray0[6].x = 0.882352948;
    TempArray0[7].x = 0.411764711;
    TempArray0[8].x = 0.235294119;
    TempArray0[9].x = 0.70588237;
    TempArray0[10].x = 0.117647059;
    TempArray0[11].x = 0.588235319;
    TempArray0[12].x = 0.941176474;
    TempArray0[13].x = 0.470588237;
    TempArray0[14].x = 0.823529422;
    TempArray0[15].x = 0.352941185;
    u_xlatu20.xy = uvec2(hlslcc_FragCoord.xy);
    u_xlati20 = int(int_bitfieldInsert(0,int(u_xlatu20.x),2,2) );
    u_xlati20 = int(int_bitfieldInsert(u_xlati20,int(u_xlatu20.y),0,2) );
    u_xlat20.x = TempArray0[u_xlati20].x;
    u_xlat20.x = (-u_xlat20.x) + _DitherOpacity;
#ifdef UNITY_ADRENO_ES3
    u_xlatb20 = !!(u_xlat20.x<0.0);
#else
    u_xlatb20 = u_xlat20.x<0.0;
#endif
```



猜测对应编译前的源码为：

```c
#ifdef _DITHER_FADEOUT
    void NiloDoDitherFadeoutClip(float2 SV_POSITIONxy, float ditherOpacity)
    {            // copy from https://docs.unity3d.com/Packages/com.unity.shadergraph@10.3/manual/Dither-Node.html?q=dither           float DITHER_THRESHOLDS[16] =
        {
            1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0, 
            13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
            4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0, 
            16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0
        };
        uint index = (uint(SV_POSITIONxy.x) % 4) * 4 + uint(SV_POSITIONxy.y) % 4;
        float clipSign = ditherOpacity - DITHER_THRESHOLDS[index];
        clip(clipSign);
    } 
#endif
```



在1.5.2节中提到过各种单元减少负载的方法，可以看出降低精度是一个非常通用的优化方式，这里我们尝试将vec4 TempArray0[16]的精度降低半精度，即mediump vec4，再次编译，结果为：

```properties
8 work registers used, 16 uniform registers used, spilling used.

                        A       L/S     T       Bound
Instructions Emitted:   160     54      8       A
Shortest Path Cycles:   2.6     19      0       L/S
Longest Path Cycles:    52      54      8       L/S
```



发现L/S的Cycle数减小了16，同时A的Cycle也减小了1，还是比较有效的。如果将程序中所有的浮点向量和浮点标量精度都改为mediump，性能还能有一些提升：

```properties
8 work registers used, 16 uniform registers used, spilling not used.

                        A       L/S     T       Bound
Instructions Emitted:   150     37      8       A
Shortest Path Cycles:   2.6     18      0       L/S
Longest Path Cycles:    48      37      8       A
```



可以看到精度降低，对L/S单元的性能提升是比较明显的，在Longest Path Cycles中，它已经从不再是性能瓶颈了；精度降低到一定程度后还能防止寄存器的溢出，现在显示”spilling not used”；但是对于A单元的性能提升效果有限，还需要从其他方面进行尝试。

# 附录
## 编译问题
Unity编译出来的代码直接拿来编译会有问题，出现在以下代码片段：

```c
...
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location =x)
#define UNITY_BINDING(x) layout(binding =x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif...
```



这里会报错：

ERROR: 0:47: S0059: 'binding' qualifier is not allowed in language version 300 es

GLSL ES 3.0不支持布局限定符中的binding属性。

参考OpenGL ES和OpengGL ES Shading Language版本对照表

| **GLSL ES版本** | **OpenGL ES版本** | **基于GLSL版本** | **日期** | **Shader预处理器** |
| :--- | :--- | :--- | :--- | :--- |
| 1.00.17 | 2.0 | 1.20 | 2009年5月12日 | #version 100 |
| 3.00.6 | 3.0 | 3.30 | 2016年1月29日 | #version 300 es |
| 3.10.5 | 3.1 | GLSL ES 3.00 | 2016年1月29日 | #version 310 es |
| 3.20.6 | 3.2 | GLSL ES 3.10 | 2019年7月10日 | #version 320 es |


尝试提升GLSL ES的版本，将复制出来的文件开头改为：

<font style="background-color:#D8DAD9;">#version 310 es</font>

<font style="background-color:#D8DAD9;">...</font>

然后测试编译通过。

## 参考文献
《Mali Offline Compiler v5.6.0 User Guide》

《Mali Offline Compiller 使用方式》——UWA

[<u>《Using Mali Offline Compiler》</u>](https://developer.arm.com/documentation/101863/7-4/Using-Mali-Offline-Compiler)

[<u>《Mali offline compiler - L/S cycles meaning》</u>](https://community.arm.com/support-forums/f/graphics-gaming-and-vr-forum/47763/mali-offline-compiler---l-s-cycles-meaning)

[<u>《Optimization advice for graphics content on mobile devices--High arithmetic load》</u>](https://developer.arm.com/documentation/102643/0100/High-arithmetic-load)

[<u>《Optimization advice for graphics content on mobile devices--High load store》</u>](https://developer.arm.com/documentation/102643/0100/High-load-store)

[<u>《Optimization advice for graphics content on mobile devices--High texture load》</u>](https://developer.arm.com/documentation/102643/0100/High-texture-load)

[<u>《OpenGL Shading Language》</u>](https://en.wikipedia.org/wiki/OpenGL_Shading_Language)

