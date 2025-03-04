# 一、ARM Mali GPU架构演进
## Utgard(oldest)
shader之间无法共享gpu计算性能，GP-vertex shader, PP-frag shader采用不同的硬件架构和指令集。

## Midgard
shader之间统一处理器架构，不同种类shader可共享计算部分工作。提高硬件利用率。

+ **Arithmetic** unit
+ **Load/Store** unit
+ **Texture** unit



+ **0 - 4 registers: Maximum thread capacity**
+ **5 - 8: half**
+ **8 - 16: Quarter**

## Bifrost
+ 开始在单处理器内部引入TLP：大量线程每4个一组，然后一组一组在单处理器中运行。同组的线程执行相同的指令，类似SIMD。
+ 开始使用**IDVS(index-driven vertex shading)**流程。将vertex shader分成position shader以及varying shader，position shader只计算position，varying shader计算剩余的non-position属性。这样也可以做到在position shader计算完毕后就执行primitive culling，将图元相关操作提前。
+ **Arithmetic** unit
+ **Load/Store** unit
+ **Varying** unit
+ **Texture** unit



+ **0 - 32 registers: Maximum**
+ **33 - 64: half**

## Valhall
更加依赖TLP，放弃了句式指令和多单元指令这些依赖软件的ILP特性，减小了调度粒度的同时也缩短了处理器管线。线程组拆的越细，越能充分利用多计算单元的特性来实现并行计算。

+ Arithmetic fused multiplay accumulate unit(**FMA**): can issue a single 32-bit operation or two 16-bit operations per thread and per clock cycle.
+ Arithmetic convert unit(**CVT**): do format conversion and integer addition. can issue a single 32-bit operation or two 16-bit operations per thread and per clock cycle.
+ Arithmetic special functions unit(**SFU**)
+ **Load/Store** unit
+ **Varying** unit
+ **Texture** unit



+ **0 - 32 registers: Maximum**
+ **33 - 64: half**

## Arm 5th generation(latest)
## 演进趋势
1. shader之间统一处理器架构，方便提高硬件利用率，跑满硬件性能。
2. ILP(Instruciton level parallelism)和TLP(Thread level parallelism)中，TLP的比重逐渐加大。

细节可见[ARM Mali GPU架构演进 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/168712183)



![](https://cdn.nlark.com/yuque/0/2024/webp/1660870/1724985847123-30435c63-f425-4ce7-8548-ee9d20f8eb9d.webp)



# 二、静态性能分析
在cmd或者powershell输入以下命令来使用：

`malioc -c <Core> <path_to_shader_src> --format <text | json> -o <path_output> [-Dmacro]`

+ -Dfoo=bar 给shader source code设定宏定义
+ 一般采用**Mali-G76(Bifrost)**、**Mali-G77(Valhall)**、**Mali-G78(Valhall)**三种架构来测试。这三种都是性能比较好的高端gpu。



```properties
Configuration
==========

Hardware: Mali-G72 r0p3                 // 硬件信息
Architecture: Bifrost                   // GPU架构
Driver: r44p0-00re10                    // 驱动
Shader type: OpenGL ES Fragment         // 图形API及Shader类型

Main shader
=========

Work registers: 26 (81% used at 100% occupancy)     // 工作使用的寄存器数量，也是临时变量存储的地方。减少数量可以提升性能，有助于增加并行的线程数，保持GPU繁忙。最好根据前文GPU架构来将work registers降到Maximum标准内，不然优化效果有限。
Uniform registers: 48 (75% used)                    // 着色器需要的常量及时间常量，所有线程都有共享uniform register。
Stack spilling: false                               // 是否有栈溢出，变量被放在栈内存而非寄存器。有的话，GPU读取性能消耗大
16-bit arithmetic: 11%                              // 以16位或更低精度执行的算术运算的百分比。数值越高代表shader性能越好。16位精度相比32位，效率是双倍的。

                                   A      LS       V       T          Bound
Total instruction cycles:       2.50    0.00    1.00    2.00              A       // Shader生成的所有指令的累积执行周期数，与控制流无关
Shortest path cycles:           2.50    0.00    1.00    2.00              A       // 通过着色器程序的最短控制流路径的循环数的估计
Longest path cycles:            2.50    0.00    1.00    2.00              A       // 通过着色器程序的最长控制流路径的循环数的估计

A = Arithmetic, LS = Load/Store, V = Varying, T = Texture        // A = Arithmetic 在Mali Valhall架构的GPU实现了两个并行处理单元，算术单元A被拆分为：FMA，CVT，SFU。
                                                                 // FMA: Fused Multiply Accumulate。乘加加速器，主要的算术管道，实现了着色器中广泛使用的浮点乘法器。
                                                                 // CVT: Arithmetic Conversion。负责基本的整型操作、数据类型的转换、分支处理。混合类型运算中，会转换成最宽的。
                                                                 // SFU: Special Function Unit。三角函数、指数、对数等。
                                                                 // LS = Load/Store operations  读取和存储的操作消耗，处理所有非纹理内存访问。like buffer access, image access..
                                                                 // V = Varying operations  在Shader中不同单位插值的消耗
                                                                 // T = Texture operations   所有纹理采样和过滤操作消耗
                                                                 // Bound 循环计数最高的功能单元，识别瓶颈单元。

Shader properties
=============

Has uniform computation: true      // 是否有任何计算仅依赖于文字常量或统一值。尽量把这系列计算移植到cpu上进行。每个线程计算的结果都是一样的。
Has side-effects: false            // 此着色器是否具有在固定图形管道之外的内存中可见的副作用。如：内存写入、图片存储、atomic operation、写入全局变量。
Modifies coverage: false           // 片段着色器是否具有可以通过着色器执行更改的覆盖掩码，例如，通过使用discard语句。是否包含AlphaTest，AlphaToCoverage。
Uses late ZS test: false           // 片段着色器是否包含了强制late zs test的逻辑。会禁用early zs test和hidden surface removal的使用，导致性能损失。
Uses late ZS update: false         // 具有Modifies coverage的shader必须使用late zs update。这会降低early zs test在同一坐标上的later片段的效率。
Reads color buffer: false          // 片段着色器是否包含了从color buffer中读取的逻辑。如果读取，将会被视为透明着色器，进而无法通过hidden surface removal去除遮挡物。

```



以下会给出urp/lit和cloth/dyeing_lit的对比：

### URP/Lit
```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Vertex

Main shader
===========

Position variant
----------------

Work registers: 32 (100% used at 100% occupancy)
Uniform registers: 90 (140% used)
Stack spilling: false
16-bit arithmetic: 0%

                                A      LS       T    Bound
Total instruction cycles:    0.92    1.00    0.00       LS
Shortest path cycles:        0.92    1.00    0.00       LS
Longest path cycles:         0.92    1.00    0.00       LS

A = Arithmetic, LS = Load/Store, T = Texture

Varying variant
---------------

Work registers: 30 (93% used at 100% occupancy)
Uniform registers: 82 (128% used)
Stack spilling: false
16-bit arithmetic: 0%

                                A      LS       T    Bound
Total instruction cycles:    1.92   18.00    0.00       LS
Shortest path cycles:        1.92   18.00    0.00       LS
Longest path cycles:         1.92   18.00    0.00       LS

A = Arithmetic, LS = Load/Store, T = Texture

Shader properties
=================

Has uniform computation: true

Recommended attribute streams
=============================

Position attributes
  - in_POSITION0 (location=dynamic)

Non-position attributes
  - in_NORMAL0 (location=dynamic)
  - in_TANGENT0 (location=dynamic)
  - in_TEXCOORD0 (location=dynamic)


```

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

Work registers: 63 (98% used at 50% occupancy)
Uniform registers: 128 (200% used)
Stack spilling: false
16-bit arithmetic: 14%

                                A      LS       V       T    Bound
Total instruction cycles:   22.33    4.00    1.62   33.00        T
Shortest path cycles:        9.47    0.00    1.62    3.50        A
Longest path cycles:        17.33    4.00    1.62   19.00        T

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



### Cloth/Dyeing_Lit
```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Vertex

Main shader
===========

Position variant
----------------

Work registers: 32 (100% used at 100% occupancy)
Uniform registers: 128 (200% used)
Stack spilling: false
16-bit arithmetic: 0%

                                A      LS       T    Bound
Total instruction cycles:    1.04    1.00    0.00        A
Shortest path cycles:        1.04    1.00    0.00        A
Longest path cycles:         1.04    1.00    0.00        A

A = Arithmetic, LS = Load/Store, T = Texture

Varying variant
---------------

Work registers: 30 (93% used at 100% occupancy)
Uniform registers: 128 (200% used)
Stack spilling: false
16-bit arithmetic: 3%

                                A      LS       T    Bound
Total instruction cycles:    3.50   18.00    0.00       LS
Shortest path cycles:        3.50   18.00    0.00       LS
Longest path cycles:         3.50   18.00    0.00       LS

A = Arithmetic, LS = Load/Store, T = Texture

Shader properties
=================

Has uniform computation: true

Recommended attribute streams
=============================

Position attributes
  - in_POSITION0 (location=dynamic)

Non-position attributes
  - in_NORMAL0 (location=dynamic)
  - in_TANGENT0 (location=dynamic)
  - in_TEXCOORD0 (location=dynamic)


```

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

Work registers: 63 (98% used at 50% occupancy)
Uniform registers: 128 (200% used)
Stack spilling: false
16-bit arithmetic: 38%

                                A      LS       V       T    Bound
Total instruction cycles:   13.67    4.00    1.88    5.00        A
Shortest path cycles:       12.42    0.00    1.88    4.50        A
Longest path cycles:        13.58    4.00    1.88    5.00        A

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



## 优化建议
+ work registers那里与前文GPU架构中registers数目对应上。优化如果能将寄存器数目限制到Maximum标准内还是值得一做的，不然意义不大。这个主要会影响gpu shader并行的数量。
    - 优化方法主要就是优化计算算法，减少中间变量的产生，使用位操作等。
+ 16-bit arithmetic越高越好。
+ 尽量减少SFU的使用，如: sin, cos, log, sqrt, pow, atan, atan2等。
+ 由于混合类型运算中，会对齐到最高精度的类型，尽量减少无意义的类型转换。
+ 尽量先算标量，再算向量。这样可以减少实际计算次数。

另外有一点提一下：Unity Shader中没有写在CBuffer里的变量，都会被写到全局默认的$Global CBuffer中。



[如何量化shader的性能标准学习心得 - 昂流 - 博客园 (cnblogs.com)](https://www.cnblogs.com/lanyelinxiang/p/17875590.html)

[使用Mali Compiler对Unity Shader进行优化 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/448732749)



# 三、自动化分析
[shader_analyzer.py](https://snh48group.yuque.com/attachments/yuque/0/2024/py/1660870/1725502914222-8f28fd81-3eef-4d40-a460-2e252e744e78.py)

使用这个python脚本可以对Unity Compiled出来的shader进行自动分析并汇总成表格。

## python脚本使用方法
1. 打开Cmd或者PowerShell，输入`python --version`看是否会输出python版本号，如果没有输出，则自行去Microsoft store搜一下python进行安装，再输入上述命令确认安装成功。
2. 因为shader_analyzer.py脚本需要，得手动安装一下xlwt的库。打开cmd或者powershell，输入`**pip install xlwt**`。
3. 用Unity将想要分析的shader按**opengl3x**平台compiled出来，找到其文件路径。一般在和Assets同级的Temp文件夹下。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725503474814-8fdd85ab-9c48-4f19-a25c-5ca8f5ae443b.png)![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725503506126-4f61d203-f79c-4c18-8d77-deac493a29b5.png)
4. **检查一下Compiled出来的文件名是否超过31位**，如果超过，需要将其重命名为一个更短的名字。不然xlwt写表时会报名字异常。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725503827666-e6813ef9-76d6-4812-9f17-ddc5ee471edf.png)
5. 在cmd或者powershell中切换到**shader_analyzer.py所在目录**，然后输入`**python .\shader_analyzer.py D:\Work\ut-graphics\TestProjects\UTRendering_Test\Temp\Cloth_Dyeing_Lit.shader**`并按回车确认。
6. 脚本会自动在shader_analyzer.py所在目录创建**一个文件夹用于存放其自动按关键字拆分出来的frag文件**以及**两份excel汇总表格**。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725504001239-56c59ace-da75-4eb6-87ab-7f16ef345e3b.png)![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725504016663-866162ec-0503-44a6-bb37-c856c4f9b128.png)
7. 挑一份excel表格打开，即可看到不同关键字组合的性能消耗。![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1725504163149-feb6af1d-55a0-471f-bfbd-ff3c1960a774.png)
8. 提醒一下，不要对关键字组合过多的shader使用这个脚本，不然会卡住，像urp/lit那种关键字组合量，就不太好全部分析一遍然后汇总。最好只是选择自己需要的关键字组合然后进行针对性分析。



# 四、资源位置
mali_offline_compiler以及shader_analyzer.py均在`"\\192.168.0.231\AI及Web3游戏研发中心\公共资源\04_软件\TA工具\mali_offline_compiler v8.1.zip"`压缩文件内。



# 参考
[ARM Mali GPU架构演进 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/168712183)

[Arm Mali GPU Training——Mail 手机GPU 教程(一) - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/416181627)

[如何量化shader的性能标准学习心得 - 昂流 - 博客园 (cnblogs.com)](https://www.cnblogs.com/lanyelinxiang/p/17875590.html)

[使用Mali Compiler对Unity Shader进行优化 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/448732749)

