# 测试环境
机型：华为mate20pro

GPU：Mali-G76 OpenGLES 3.2

分辨率：1920*1080

# 测试内容
eyelash shader； eye shader； skin shader；hair shader； 

Render Scale：1，2

MASS：1，8

# 测试结果
具体测试数据内容在“角色shader（减少变量）”报告里

[IdolBox - GOT Online GPU性能分析 (uwa4d.com)](https://www.uwa4d.com/u/got/gpu.html/renderAnalysis?dataKey=20240918170630nit4655gpu278&project=9542&engine=1)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1726714302345-15005def-f99f-46f3-af61-3180c5df8bc7.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1726715496313-7b6fb7e5-5836-4df5-a5b2-6ea953e0404f.png)

# UWA报告解读
测试时记录了下不同材质在不同RenderScale和MSAA开关的性能状况

对应静态性能报告（以下数值都已去除了固定消耗求均值）

### Eyelash shader
（变体None）

| Eyelash<br/>Shader | Render Scale | MSAA | Fragment Shaded(万个) | 除去固定消耗后的Fragment Shaded | 固定消耗的Fragment Shaded |
| --- | :---: | :---: | --- | :---: | :---: |
|  | 1 | 0 | 172 | 142 | 30 |
|  | 2 | 0 | 600 | 570 |  |
|  | 2 | 8 | 1060 |  | |


```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Fragment

Eyelash shader
===========

Work registers: 20 (62% used at 100% occupancy)
Uniform registers: 8 (12% used)
Stack spilling: false
16-bit arithmetic: 40%

                                A      LS       V       T    Bound
Total instruction cycles:    0.88    0.00    0.25     0.50     A
Shortest path cycles:        0.62    0.00    0.25     0.50     A
Longest path cycles:         0.88    0.00    0.25     0.50     A

A = Arithmetic, LS = Load/Store, V = Varying, T = Texture

Shader properties
=================

Has uniform computation: true
Has side-effects: false
Modifies coverage: true
Uses late ZS test: false
Uses late ZS update: true
Reads color buffer: false

Note: This tool shows only the shader-visible property state.
API configuration may also impact the value of some properties.


```

+ Eyelash的fragment shaded = 142万个左右；
+ shader cycles =2.3百万Cycles；
+ arithmetic cycles = 1.17百万Cycles;
+ loadstore cycles = 0.01百万Cycles;
+ interpolator cycles = 0.35百万Cycles;
+ texture cycles = 0.71百万Cycles;
+ shader instrucitons = 0.91百万个；
+ 对比真机和静态性能单位：
+ 1.17 / 0.88 = ~1.32
+ 0.35/ 0.25= ~1.4 
+ 0.71/ 0.5 = ~1.42

**单位均大约为1.38左右。**

### Eye shader
(变体为_ADDITIONAL_LIGHTS _MAIN_LIGHT_SHADOWS)

| Eye<br/>Shader | Render Scale | MSAA | Fragment Shaded(万个) | 除去固定消耗后的Fragment Shaded | 固定消耗的Fragment Shaded |
| --- | :---: | :---: | --- | :---: | :---: |
|  | 1 | 0 | 260 | 113 | 150 |
|  | 2 | 0 | 600 | 453 |  |
|  | 2 | 8 | 1060 |  | |


```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Fragment

Eye shader
===========

Work registers: 61 (95% used at 50% occupancy)
Uniform registers: 102 (159% used)
Stack spilling: false
16-bit arithmetic: 13%

                                A      LS       V       T    Bound
Total instruction cycles:  		10.42    0.00    2.12    3.50    A
Shortest path cycles:       	10.42    0.00    2.12    3.50    A
Longest path cycles:       	  10.42    0.00    2.12    3.50    A

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

Eye的fragment shaded = 113万个左右；

shader cycles = 21.62百万Cycles；

arithmetic cycles = 13.2百万Cycles;

loadstore cycles = 0.01百万Cycles;

interpolator cycles = 3.03百万Cycles;

texture cycles = 5.37百万Cycles;

shader instrucitons = 13.19百万个；

对比真机和静态性能单位：

13.2/ 10.42 = ~1.26

3.03/ 2.12= ~1.42

5.37 / 3.5 = ~1.53

**单位均大约为1.4左右。**

### Skin shader
（变体为_MAIN_LIGHT_SHADOWS）

| Skin<br/>Shader | Render Scale | MSAA | Fragment Shaded(万个) | 除去固定消耗后的Fragment Shaded | 固定消耗的Fragment Shaded |
| --- | :---: | :---: | --- | :---: | :---: |
|  | 1 | 0 | 260 | 113 | 150 |
|  | 2 | 0 | 600 | 453 |  |
|  | 2 | 8 | 1060 |  | |


```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Fragment

Skin shader
===========

Work registers: 31 (96% used at 100% occupancy)
Uniform registers: 102 (159% used)
Stack spilling: false
16-bit arithmetic: 24%

                                A      LS       V       T    Bound
Total instruction cycles:    	7.13    0.00    1.88    3.00     A
Shortest path cycles:       	7.13    0.00    1.88    3.00     A
Longest path cycles:         	7.13    0.00    1.88    3.00     A

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

+ Skin 的fragment shaded = 113万个左右；
+ shader cycles =18.53百万Cycles；
+ arithmetic cycles =9.2百万Cycles;
+ loadstore cycles =0.01百万Cycles;
+ interpolator cycles = 2.54百万Cycles;
+ texture cycles = 4.55百万Cycles;
+ shader instrucitons = 9.96百万个；
+ 对比真机和静态性能单位：
+ 9.2/ 7.13 = ~1.29
+ 2.54/ 1.88= ~1.35
+ 4.55 / 3.00= ~1.51

**单位均大约为1.38左右。**

### Hair shader
（变体_MAIN_LIGHT_SHADOWS _SURFACE_TYPE_TRANSPARENT）

| Hair<br/>Shader | Render Scale | MSAA | Fragment Shaded(万个) | 除去固定消耗后的FragmentShaded | 固定消耗的Fragment Shaded |
| --- | :---: | :---: | --- | :---: | :---: |
|  | 1 | 0 | 378 | 227 | 150 |
|  | 2 | 0 | 1060 | 909 |  |
|  | 2 | 8 | 1540 |  | |


```properties
Mali Offline Compiler v8.1.0 (Build c0c058)
Copyright (c) 2007-2023 Arm Limited. All rights reserved.

Configuration
=============

Hardware: Mali-G76 r0p0
Architecture: Bifrost
Driver: r44p0-00rel0
Shader type: OpenGL ES Fragment

Hair shader
===========

Work registers: 31 (96% used at 100% occupancy)
Uniform registers: 128 (200% used)
Stack spilling: false
16-bit arithmetic: 20%

                                A      LS       V       T    Bound
Total instruction cycles:   	11.17   0.00    2.12    2.50     A
Shortest path cycles:       	11.17   0.00    2.12    2.50     A
Longest path cycles:       	  11.17   0.00    2.12    2.50     A

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

Hair shader 的Fragment Shaded高也与头发shader执行两个Pass有关（先写深度，在画半透）

+ Hair 的fragment shaded = 227万个左右；
+ shader cycles =20百万Cycles；
+ arithmetic cycles =13.61百万Cycles;
+ loadstore cycles = 0.02百万Cycles;
+ interpolator cycles = 2.54百万Cycles;
+ texture cycles = 3.95百万Cycles;
+ shader instrucitons = 13.63百万个；
+ 对比真机和静态性能单位：
+ 13.61/ 11.17 = ~ 1.21
+ 2.54/ 2.12= ~1.20 
+ 3.95/ 2.5= ~1.58

**单位均大约为1.33左右。**

### 对比结论：
1. 前面的固定消耗和后面有差别应该是刚进场景的原因，后面稳定在150左右。
2. Skin Shader和Eye Shader的fragment shader执行次数相差无几，符合预期；
3. 对比gpu clocks、gpu freq可以看出，gpu压力eyelash<skin<eye<hair；
4. 指令复杂度可以从gpu shader cycles和gpu shader instructions均值由低到高排布看出，分别为eyelash<skin<eye<hair。

# 静态分析报告
[SkinShader_Mali-G76.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841660-4e3eb77c-7be3-4b8e-92c9-cbffe576dc2e.xls)

[SkinShader_Mali-G77.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841433-0ea2f667-15c8-49fb-b707-a489b6ca0edd.xls)

[eyelash_Mali-G76.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841424-dfb5d5d1-ecd9-4865-8732-929ee2c937cb.xls)

[eyelash_Mali-G77.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841449-f62a37df-2e28-47a8-bbe2-07373391f251.xls)

[EyeShader_Mali-G76.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841487-59ab77ca-7c77-4d16-9a24-f19d7b8a18b0.xls)

[EyeShader_Mali-G77.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841442-99ecb817-3f13-43bf-8219-2c6d4ffa9627.xls)

[Hair_Mali-G76.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841684-e9a920c3-5d8d-42d9-abaa-514d8479cd7f.xls)

[Hair_Mali-G77.xls](https://snh48group.yuque.com/attachments/yuque/0/2024/xls/39137189/1726729841640-ab3521c8-d8a8-43c3-b7ee-2e7c38adf6ce.xls)



根据静态分析报告可以看出有一些关键字的组合会出现Has Stack Spilling等于TRUE的情况，寄存器溢出到栈上的的关键字组合里，出现最多的关键字分别是_FAKE_SHADOW、_CUSTOM_SCREEN_SPACE_OCCLUSION、_RECEIVE_ADDITIONAL_SELF_SHADOW和_RECEIVE_ADDITIONAL_SELF_SHADOW2。



通过控制关键字变量的方法来测试这两个关键字为什么会经常导致寄存器溢出到栈上。



### _FAKE_SHADOW
在HairShader中找到包含以下关键字的变体1：

_FAKE_SHADOW _MAIN_LIGHT_SHADOWS _RECEIVE_ADDITIONAL_SELF_SHADOW _RECEIVE_ADDITIONAL_SELF_SHADOW2

找到关键字组合为以下的变体2：

 _MAIN_LIGHT_SHADOWS _RECEIVE_ADDITIONAL_SELF_SHADOW _RECEIVE_ADDITIONAL_SELF_SHADOW2

他们只有是否开启_FAKE_SHADOW的区别

向下查找关键字“#ifdef FRAGMENT”，复制出宏包围的代码，保存到尾缀为.frag文件中

将两份关键字里的代码复制放到代码比较工具里，发现主要区别为![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727090580704-53e78fa5-eafa-4530-afdc-613c7e36b2f5.png)

开启_FAKE_SHADOW关键字的代码里有一段_IsSwitchPass的判断，对应原文的这一段![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727154790386-eedc24d8-83f2-496b-941c-462e57d494fd.png)	这个判断会根据_IsSwitchPass是否为0来选择采样hlslcc_zcmp_CustomSelfShadowMapRT还是采样hlslcc_zcmp_AdditionalSelfShadowMapRT，而且会采样4次。在shader里的判断句会走两遍，然后再根据判断执行其中一项，所有开启这个关键字就会采样8次RT，比不开启_FAKE_SHADOW多采样4次。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727090796530-b77a46b8-97b5-44de-b445-2dcec486b132.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727090788458-265e5ebf-d259-47a2-a040-75fe3e484347.png)

对应原文这一段

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727154960394-7b6c7712-6cdd-4b21-b2cb-ba52b521ad29.png)

通过比较命令行里的报告也能看出。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727091162455-1334bf5c-8c60-4771-b94a-8519753956e3.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727091290261-d70d8d1f-6790-40c5-a8c7-0ef8fedf7e31.png)



### _CUSTOM_SCREEN_SPACE_OCCLUSION
通过相同的方法，其他关键字组合为_MAIN_LIGHT_SHADOWS _RECEIVE_ADDITIONAL_SELF_SHADOW _RECEIVE_ADDITIONAL_SELF_SHADOW2，用代码比较工具发现，无论是Hair Shader和Skin Shader他们其他地方代码都一样，只不过含有_CUSTOM_SCREEN_SPACE_OCCLUSION关键字的代码里会多采样一张_ScreenSpaceOcclusionTexture的贴图。![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727145713316-4d8199ed-1310-4106-8855-a0818b0dc3e7.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727145777584-b42e4ba6-6b49-4115-a44d-78d54014571f.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727145757235-fb6e2425-4d5f-42ab-a544-aa0463db6eb7.png)

这个消耗是可接受的，所以可以得出在Has Stack Spilling等于TRUE且含有_CUSTOM_SCREEN_SPACE_OCCLUSION关键字的关键字组合里，其他关键字的组合消耗已经很高了，再开启这个关键字只不过是压倒寄存器的最后一个关键字！



### _RECEIVE_ADDITIONAL_SELF_SHADOW
开

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727148366314-3f37c6f8-8b53-43f1-8dfc-db69fc3de851.png)

关

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727148382021-ae5cc7e4-a64a-4433-9c79-1d5a4fbad7cc.png)

开启这个关键字的区别主要是存在额外光的计算前采样了4次hlslcc_zcmp_AdditionalSelfShadowMapRT，且还在额外光的计算前，和额外光的计算里走了比较长的计算，光看代码长度就能知道开销![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727149697777-8a88bbc9-f520-4d76-89bf-8bfb77ddde38.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727148486293-16155deb-fb63-457b-8209-9524b8a2f454.png)

蓝色部分对应原文的这里![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727155937069-e6e35044-0ea7-44fe-b9b9-3cbb1150be78.png)

### _RECEIVE_ADDITIONAL_SELF_SHADOW2
开

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727148808245-95cad252-655e-4973-91a6-325ed9fd357e.png)

关

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727148816422-fda72213-4280-41c6-90c0-37b257b25d95.png)

开启这个关键字主要区别为还会在额外光计算前采样四次hlslcc_zcmp_CustomSelfShadowMapRT，且走了一段比较长的计算。![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727149426393-699c097a-fbb9-4b2e-8a60-9002c8119e6d.png)

同时开启_RECEIVE_ADDITIONAL_SELF_SHADOW _RECEIVE_ADDITIONAL_SELF_SHADOW2比两个都不开启多采样了8次RT，且计算量也增大了不少。



要优化的话主要是从_FAKE_SHADOW、_RECEIVE_ADDITIONAL_SELF_SHADOW、_RECEIVE_ADDITIONAL_SELF_SHADOW2这些关键字入手。如采样4次变成采样1次，优化计算过程等。避免用if，如果不能通过关键字就行判断就事先把变量计算好，来重复利用等；

# 最后给每个shader单独评价
eyelash:采样的贴图极少，计算也极少

skin：采样的贴图数量可以，计算也不多

eye：采样贴图数量较多，计算也比较复杂，可以合并贴图，减少贴图采样数，以及优化计算；

hair：采样的贴图数量少，计算比较复杂，因为是半透材质，所有计算可能会复杂一点，还是得优化计算；



