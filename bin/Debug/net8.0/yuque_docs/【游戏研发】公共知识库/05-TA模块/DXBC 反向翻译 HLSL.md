#### <font style="color:rgb(25, 27, 31);">DXBC简单介绍：</font>
<font style="color:rgb(25, 27, 31);">HLSL 脚本不能直接被 GPU 执行，需要先使用 FXC 编译器离线编译成 DXBC（DirectX Byte Code），也即 DirectX 字节码DXBC，也是GPU真正执行的指令</font>

<font style="color:rgb(25, 27, 31);">一个GPA抓取的DXBC：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723699605349-d3a19bd2-b112-4655-a75f-b50fd671a95f.png)

<font style="color:rgb(25, 27, 31);">基本结构：</font>

<font style="color:rgb(25, 27, 31);">顶点着色器或像素着色器的版本信息。这里版本为4.0。</font>

<font style="color:rgb(25, 27, 31);">       vs_4_0</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">声明着色器常量缓冲区，外部传入的变量，使用文本值为缓冲区编制索引。</font>

<font style="color:rgb(25, 27, 31);">      dcl_constantbuffer cb0[80], immediateIndexed</font>

<font style="color:rgb(25, 27, 31);">      dcl_constantbuffer cb1[7], immediateIndexed</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">着色器输入寄存器。</font>

<font style="color:rgb(25, 27, 31);">      dcl_input v0.xyz</font>

<font style="color:rgb(25, 27, 31);">      dcl_input v1.xyz</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">着色器输出寄存器。</font>

<font style="color:rgb(25, 27, 31);">      dcl_output_siv o0.xyzw, position</font>

<font style="color:rgb(25, 27, 31);">      dcl_output o1.xyz</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">声明临时寄存器的数量。</font>

<font style="color:rgb(25, 27, 31);">      dcl_temps 2</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">执行运算</font>

<font style="color:rgb(25, 27, 31);">  mul r0.xyzw, v0.yyyy, cb1[1].xyzw      //等价于r0.xyzw = v0.yyyy * cb1[1].xyzw</font>

<font style="color:rgb(25, 27, 31);">  mad r0.xyzw, cb1[0].xyzw, v0.xxxx, r0.xyzw    //等价于r0.xyzw = cb1[0].xyzw*v0.xxxx+r0.xyzw</font>

<font style="color:rgb(25, 27, 31);">  mad r0.xyzw, cb1[2].xyzw, v0.zzzz, r0.xyzw</font>

<font style="color:rgb(25, 27, 31);">  add r0.xyzw, r0.xyzw, cb1[3].xyzw   //等价于r0.xyzw += cb1[3].xyzw </font>

<font style="color:rgb(25, 27, 31);">  mul r1.xyzw, r0.yyyy, cb0[77].xyzw</font>

<font style="color:rgb(25, 27, 31);">  mad r1.xyzw, cb0[76].xyzw, r0.xxxx, r1.xyzw</font>

<font style="color:rgb(25, 27, 31);">  mad r1.xyzw, cb0[78].xyzw, r0.zzzz, r1.xyzw</font>

<font style="color:rgb(25, 27, 31);">  mad o0.xyzw, cb0[79].xyzw, r0.wwww, r1.xyzw</font>

<font style="color:rgb(25, 27, 31);">  dp3 r0.x, v1.xyzx, cb1[4].xyzx   //等价于r0.x=dot（ v1.xyzx，cb1[4].xyzx）</font>

<font style="color:rgb(25, 27, 31);">  dp3 r0.y, v1.xyzx, cb1[5].xyzx</font>

<font style="color:rgb(25, 27, 31);">  dp3 r0.z, v1.xyzx, cb1[6].xyzx</font>

<font style="color:rgb(25, 27, 31);">  dp3 r0.w, r0.xyzx, r0.xyzx</font>

<font style="color:rgb(25, 27, 31);">  max r0.w, r0.w, l(0.0000)    //等价于r0.w= MAX（r0.w.，l(0.0000)）</font>

<font style="color:rgb(25, 27, 31);">  rsq r0.w, r0.w     //等价于r0.w = rsqrt(r0.w)，倒数的平方根</font>

<font style="color:rgb(25, 27, 31);">  mul o1.xyz, r0.wwww, r0.xyzx</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">着色器停止执行</font>

<font style="color:rgb(25, 27, 31);">  ret</font>



其余指令参考阅读：

[mov （sm4 - asm） - Win32 应用 |Microsoft 学习](https://learn.microsoft.com/en-us/windows/win32/direct3dhlsl/mov--sm4---asm-)

#### 使用RenderDoc抓取DXBC并翻译HLSL
下载并安装RenderDoc

Scene窗口加载RenderDoc

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723692819046-af0ec819-6b3c-404b-bb51-3e85ae6fc470.png?x-oss-process=image%2Fformat%2Cwebp)

下载代码

[GitHub - javelinlin/HLSLDecompiler: HLSL Decompiler forked from 3Dmigoto （v2: modify from repository, and fixing some bugs)](https://github.com/javelinlin/HLSLDecompiler)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688416515-5f139d19-a1df-404c-95d0-a4ba7523e36d.png)根据这个路径找到

cmd_Decompiler.exe和hlsl_decompiler_wrapper.bat和d3dcompiler_46.dll

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688440114-548f275d-8b85-4168-881b-bf46f091dc01.png)

将其复制到没有中文的路径

配置RenderDoc

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688501105-22c6359d-a3c1-4d3a-a12f-5c33418c255e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688558426-f35ced86-bbb4-4c6a-8248-0d46df637f6e.png)



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688572449-8bb634cd-8d78-4b3b-8813-cdfefc5706c6.png)

复制：

Name : DXBC->HLSL

Tool Type : Custom Tool

Excutable: hlsl_decompiler_wrapper.bat 的绝对目录（具体路径按自己电脑上）

Command Line : {input_file} {output_file} 注意两个参数之间有空格，因为是 bat 的参数

Input/Output : DXBC 和 HLSL

点击OK保存



为了方便测试用HLSL写一个最简单的兰伯特[Lambert.txt](https://snh48group.yuque.com/attachments/yuque/0/2024/txt/45145007/1723787597145-9e9bb9ca-5763-436f-9777-f94cc47c174f.txt)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723696785544-32c0a8b9-c886-48bd-8801-f66fbf3d2633.png)

挂上模型和材质

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688276414-1640af03-3735-4830-81a5-6d91a8ef8b78.png)

Game窗口点击![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688288684-679a5b67-5037-4980-8517-6c18026d89ac.png)进行抓帧

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688819080-4be01b13-48d1-4787-b85a-181fb1079806.png)

打开抓取的文件

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688866242-7fdf0025-0baf-43fd-8666-ce7d57225e87.png)

找到渲染不透明的Draw Call

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723688984087-cf86f5b8-e0c3-47d1-8e41-4cee53f5a1e8.png)

打开Pipeline管线选择VS/PS（顶点/片元着色器）打开View视图



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689014926-30b63ca7-f026-4eac-b693-2ea7e4d26344.png)

下拉菜单选择DXBC->HLSL(或者直接打开Edit视图)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689047655-28490f85-9363-4985-8375-d893d7b93fb5.png)——>![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689061193-647d449b-d167-4f38-9ae2-b2457e49ca1f.png)

得到反向翻译结果

#### 使用intel GPA抓取DXBC并翻译HLSL
使用同上场景

打一个window包

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689548717-9110c7d6-fe24-4a49-87e2-119a83dad693.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723696882407-90bf4c4e-fbe7-4d17-8c48-83883c5f4e2b.png)

GPA的Graphics Monitor 打开Auto - Detect开关自动检测运行的游戏

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689595795-01109993-8daa-4cf7-8ed6-b3cae265b554.png)

双击exe文件运行

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723696922658-ffbe48bf-c0bb-4b6d-916d-d9862bc6c536.png)

游戏运行过程中按Ctrl+Shift+C抓取并打开

（参考阅读：[英特尔Graphics Performance Analyzers（GPA）抓帧（待补充） (yuque.com)](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/wxfe39/plxl5ntle00kchft)）

选择一个Draw Call的SH

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723695769145-cbfc5bdd-4707-4950-8819-2b2a2f4ce153.png)

得到DXBC

怎么翻译HLSL待补充



#### 如何通过翻译结果逆向定位shader
由于打包后变量名都不会保留

只能根据计算过程定位原shader位置



以兰伯特为例：

对比顶点/片元着色器

顶点着色器：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689182416-404bcbf8-bd0e-4701-8ba1-20199a7cc838.png)

进入unity内置函数：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1724125109199-adfffea6-9bd1-434e-bb4a-03f5aa353799.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1724125121411-5ddfecb3-d308-4085-be68-39f3c05cc09a.png)

DXBC                                                       HLSL

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723715235335-070836c5-cdc4-4aef-b7b3-b7d1e6f68332.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689229430-b1644dd4-ef2f-46e5-b16c-7ae22ebe737d.png)

顶点着色器做了两件事情：

1.顶点的MVP变换（本命技能）

2.法线信息的空间变换和归一化



对应翻译后的HLSL：

cbuffer cb1 : register(b1)

{

  float4 cb1[7];

}

cbuffer cb0 : register(b0)

{

  float4 cb0[80];

}



// 3Dmigoto declarations

#define cmp -



void main(

  float4 v0 : POSITION0,

  float3 v1 : NORMAL0,

  out float4 o0 : SV_POSITION0,

  out float3 o1 : TEXCOORD0)

{

  float4 r0,r1;

  uint4 bitmask, uiDest;

  float4 fDest;



  r0.xyzw = cb1[1].xyzw * v0.yyyy;

  r0.xyzw = cb1[0].xyzw * v0.xxxx + r0.xyzw;

  r0.xyzw = cb1[2].xyzw * v0.zzzz + r0.xyzw;

  r0.xyzw = cb1[3].xyzw + r0.xyzw;

  r1.xyzw = cb0[77].xyzw * r0.yyyy;

  r1.xyzw = cb0[76].xyzw * r0.xxxx + r1.xyzw;

  r1.xyzw = cb0[78].xyzw * r0.zzzz + r1.xyzw;

  o0.xyzw = cb0[79].xyzw * r0.wwww + r1.xyzw;

  r0.x = dot(v1.xyz, cb1[4].xyz);

  r0.y = dot(v1.xyz, cb1[5].xyz);

  r0.z = dot(v1.xyz, cb1[6].xyz);

  r0.w = dot(r0.xyz, r0.xyz);

  r0.w = max(1.17549435e-038, r0.w);

  r0.w = rsqrt(r0.w);

  o1.xyz = r0.xyz * r0.www;

  return;

}



cb1和cb0属于外部传入变量

v0和v1属于传入的模型的顶点和法线信息

o0和o1属于在顶点着色器中完成计算接下来传入片元着色器的顶点和法线信息

r0和r1属于临时变量



  r0.xyzw = cb1[1].xyzw * v0.yyyy;

  r0.xyzw = cb1[0].xyzw * v0.xxxx + r0.xyzw;

  r0.xyzw = cb1[2].xyzw * v0.zzzz + r0.xyzw;

  r0.xyzw = cb1[3].xyzw + r0.xyzw;

这部分开辟了临时变量 r0，将v0的每一列的与 cb1的每一行单独相乘再相加，符合矩阵相乘运算过程，

结合shader中的计算可以推断，cb1[0 1 2 3]是MVP矩阵中的UNITY_MATRIX_M（的每一行），此过程即对应将顶点信息从Object空间变换到了World空间

  r1.xyzw = cb0[77].xyzw * r0.yyyy;

  r1.xyzw = cb0[76].xyzw * r0.xxxx + r1.xyzw;

  r1.xyzw = cb0[78].xyzw * r0.zzzz + r1.xyzw;

  o0.xyzw = cb0[79].xyzw * r0.wwww + r1.xyzw;

接下来开辟临时变量 r1，用r0的每一列的与 cb0的每一行单独相乘再相加，同理得出cb0[76 77 78 79]是MVP矩阵中的UNITY_MATRIX_VP（的每一行）。此过程即对应将顶点信息从World空间变换到了Clip空间

（一开始觉得肯定有3个矩阵，踩坑）

至此顶点的空间变换完成，输出 o0即为齐次裁剪空间的顶点坐标。

同理

  r0.x = dot(v1.xyz, cb1[4].xyz);

  r0.y = dot(v1.xyz, cb1[5].xyz);

  r0.z = dot(v1.xyz, cb1[6].xyz);

即为法线信息的空间变换，v1对应模型传入法线信息，cb1[4 5 6]对应(float3x3)GetWorldToObjectMatrix());

即3x3的UNITY_MATRIX_I_M矩阵



  r0.w = dot(r0.xyz, r0.xyz);

  r0.w = max(1.17549435e-038, r0.w);

  r0.w = rsqrt(r0.w);

  o1.xyz = r0.xyz * r0.www;

点积后取大于0的开方，对应法线的归一化

对应SafeNormalize(normalOS);

 至此法线的空间变换完成，输出 o1即为World空间的法线信息。





片元着色器：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689324931-7b402171-3c44-4764-9041-08158b76ecc2.png)

DXBC                                                       HLSL

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723715258078-f0c2f431-df1c-4fb8-9b2d-29a7312ea8b7.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1723689266548-ebc2a319-a523-439b-a72f-c281549ed4d7.png)

片元着色器根据兰伯特公式输出颜色：

自定义材质颜色*主光源颜色*  max(0,cos(入射光和法线的夹角))



cbuffer cb1 : register(b1)

{

  float4 cb1[1];

}

cbuffer cb0 : register(b0)

{

  float4 cb0[8];

}

// 3Dmigoto declarations

#define cmp -



void main(

  float4 v0 : SV_POSITION0,

  float3 v1 : TEXCOORD0,

  out float4 o0 : SV_TARGET0)

{

  float4 r0;

  uint4 bitmask, uiDest;

  float4 fDest;



  r0.x = saturate(dot(cb0[6].xyz, v1.xyz));

  r0.xyzw = cb1[0].xyzw * r0.xxxx;

  o0.xyz = cb0[7].xyz * r0.xyz;

  o0.w = r0.w;

  return;

}

同理：

cb1和cb0属于外部传入变量

v0和v1是片元着色器传入的顶点和法线信息

o0是最后输出的颜色

r0是临时变量



  r0.x = saturate(dot(cb0[6].xyz, v1.xyz));

对应 saturate(dot(mlight.direction,i.worldNormal));   

此出可得出cb0[6]对应主光源的入射线方向mlight.direction



  r0.xyzw = cb1[0].xyzw * r0.xxxx;

cb1[0]对应自定义的材质颜色_BaseColor（1，1，1，1）



  o0.xyz = cb0[7].xyz * r0.xyz;

cb0[7]对应主光源的颜色mlight.color



输出o0即为最后输出颜色





#### 其他常见shader计算对应翻译后的HLSL：
##### 采样一张贴图：
 float2 UV = input.uv;

 half4 BaseColor = SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,UV) ;

抓帧翻译后：

  r0.xyzw = t0.SampleBias(s0_s, v0.xy, cb0[5].x).xyzw;  //cb0[5]很明显是UV，v0是贴图，s0_s是采样器

  o0.xyzw = cb1[0].xyzw * r0.xyzw;



##### 法线的TBN变换
 float2 UV = input.uv;

 half3 WorldNormal = normalize(input.normalWS);

 half3 WorldTangent = normalize(input.tangentWS.xyz);

 half3 WorldBinormal = normalize(cross(WorldNormal,WorldTangent) * input.tangentWS.w);

 half3x3 TBN = half3x3(WorldTangent,WorldBinormal,WorldNormal);

 half3 NormalTS =     UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap,sampler_NormalMap,UV),_Normal);

 WorldNormal = normalize(mul(NormalTS,TBN));



抓帧翻译后：

  r0.x = dot(v2.xyz, v2.xyz);

  r0.x = rsqrt(r0.x);

  r0.xyz = v2.xyz * r0.xxx;

  r0.w = dot(v3.xyz, v3.xyz);

  r0.w = rsqrt(r0.w);   //两次归一化

  r1.xyz = v3.xyz * r0.www;

  r2.xyz = r1.yzx * r0.zxy;

  r2.xyz = r0.yzx * r1.zxy + -r2.xyz;

  r2.xyz = v3.www * r2.xyz;   //相乘再相加，向量乘法对应Normal叉乘Tangent 

  r0.w = dot(r2.xyz, r2.xyz);

  r0.w = rsqrt(r0.w);  //对结果再次归一化

  r2.xyz = r2.xyz * r0.www;  

  r3.xyz = t0.SampleBias(s0_s, v0.xy, cb0[5].x).xyw;   //采样法线贴图

  r3.x = r3.z * r3.x;

  r3.xy = r3.xy * float2(2,2) + float2(-1,-1);//解包法线信息（乘2减1）从像素（0，1）解包到法线（-1，1）

  r3.zw = cb1[1].zz * r3.xy;

  r0.w = dot(r3.xy, r3.xy);

  r0.w = min(1, r0.w);

  r0.w = 1 + -r0.w;

  r0.w = sqrt(r0.w);

  r0.w = max(1.00000002e-016, r0.w); 

  r2.xyz = r3.www * r2.xyz;

  r1.xyz = r3.zzz * r1.xyz + r2.xyz;

  r0.xyz = r0.www * r0.xyz + r1.xyz; //法线与TBN矩阵相乘

  r0.w = dot(r0.xyz, r0.xyz);

  r0.w = rsqrt(r0.w);//再次归一化

  o0.xyz = r0.xyz * r0.www;

  o0.w = 1;

  return;



。。。待补充

##### 综上：
出现矩阵/向量与某一列单独乘再相加结构：进行了空间坐标变换

出现dot再rsqrt：进行了归一化

出现条件判断结构：可能根据关键字进行了AdditionalLight或者ShadowCoord之类的额外计算



。。。待补充











