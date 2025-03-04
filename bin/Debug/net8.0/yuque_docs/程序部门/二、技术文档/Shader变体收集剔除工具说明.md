如果你已经了解变体和SVC的相关背景知识，仅想了解工具使用，请直接从第四小节看起。

## 一.变体的背景知识
### 1.1动态分支
在几乎所有的编程语言中，分支结构都是很基础且常见的，用来实现在不同的条件下做不同的事。一般的，可以通过 if...else...来实现这种分支结构。在Shader中，分支结构有一些需要特别注意的，由于GPU的特性，Shader中使用if...else...的动态分支，常常会带来性能问题，尤其对于非对称的分支情况。以下取自Unity文档的解释：

+ <font style="color:rgb(0, 0, 0);">基于非均匀变量的分支意味着GPU必须同时执行不同的操作（因此打破并行性），或者“扁平化分支”，并通过对两个分支执行操作，然后丢弃一个结果来保持并行性。基于统一变量的分支意味着GPU必须压平分支。这两种方法都会导致GPU性能下降。</font>
+ <font style="color:rgb(0, 0, 0);"> 对于任何类型的动态分支，GPU必须为最坏的情况分配寄存器空间。如果一个分支比另一个分支成本高得多，这意味着GPU会浪费寄存器空间。这可能会导致着色器程序并行调用的次数减少，从而降低性能。</font>

### <font style="color:rgb(0, 0, 0);">1.2静态分支</font>
使用宏来定义静态分支（#if defined (XXXXXX)），但这通常不够灵活。比如在编辑器里有两个材质使用了相同的shader，但其中一个要打开宏，另一个则关闭，又比如，随着游戏运行时的业务逻辑，机型渲染设置调整，场景切换等原因，需要在运行时动态的开关一些功能。对于全局唯一的，在编译时确定了的，使用宏定义的静态分支，这些需求不可能被满足。

### 1.3着色器变体
动态分支灵活易用但存在性能问题，静态分支没有性能问题，但很难满足运行时动态切换，不同材质不同组合的游戏需求。

着色器分支的出现用来解决这个矛盾，个人觉得可以理解为静态分支的加强版，这里姑且也称之为一种分支类型。以下是这种分支类型的实现原理：

首先ShaderLab在打包时，<font style="color:rgb(27, 34, 41);">不同平台使用不同的着色器编译器来编译，如下：</font>

+ <font style="color:rgb(0, 0, 0);">使用 DirectX 的平台会使用 Microsoft 的 FXC HLSL 编译器。</font>
+ <font style="color:rgb(0, 0, 0);">使用 OpenGL (Core & ES) 的平台会使用 Microsoft 的 FXC HLSL 编译器，然后使用HLSLcc 将字节代码转换为 GLSL。</font>
+ <font style="color:rgb(0, 0, 0);">使用 Metal 的平台会使用 Microsoft 的 FXC HLSL 编译器，然后使用 HLSLcc将字节代码转换为 Metal。</font>
+ <font style="color:rgb(0, 0, 0);">使用 Vulkan 的平台会使用 Microsoft 的 FXC HLSL 编译器，然后使用HLSLcc将字节代码转换为 SPIR-V。</font>
+ <font style="color:rgb(0, 0, 0);">其他平台（如游戏主机平台）使用其各自的编译器。</font>

以DX12举例，使用D3DCompileFromFile 编译HLSL<font style="color:rgb(77, 77, 77);">到DXBC:</font>

```csharp
ComPtr<ID3DBlob> byteCode = nullptr;//二进制dxbc 
D3DCompileFromFile(filename.c_str(), defines, D3D_COMPILE_STANDARD_FILE_INCLUDE, 
                   entrypoint.c_str(), target.c_str(), compileFlags, 0, &byteCode, nullptr);
```



注意到第二个参数defines，形如：

```csharp
const D3D_SHADER_MACRO defines[] = { "FOG", "1", 
                                     "ALPHA_TEST", "1", 
                                         NULL, NULL };
```



那么传入不同的defines列表，就决定了生成了不同的变体。

unity提供multi_compile和shader_feature来定义关键字列表，使用静态分支来创建不同的关键字组合对应的小型、专门的着色器程序（变体）。在运行时，Unity使用与关键字列表匹配的变体。这综合了静态分支和动态分支的优点，当然也带来了新的问题：构建时间，包体大小，内存占用，加载时长等，以下的内容正是为了解决这些问题的。





三种分支结构没有绝对的孰优孰劣，只是适用情景不同，应当如何选择合适的分支结构可以参考官方文档：[https://docs.unity3d.com/cn/current/Manual/shader-branching.html](https://docs.unity3d.com/cn/current/Manual/shader-branching.html)

## 二.multi_compile or shader_feature？
### 2.1multi_compile
#pragma multi_compile A B C 

#pragma multi_compile D E

如上定义了两个关键字列表，这会生成六个变体，即全排列组合：A D、A E、B D、B E、C D、C E。不管项目中的材质是否用到了所有变体，这些都会全部生成。从这个特性可以看出，**multi_compile适用于在运行时动态切换的情况，因为包体中包含了所有的关键字组合情况，不存在变体丢失的问题。缺点是，随着关键字列表的增加，变体数量以指数级上升**。

### 2.2shader_feature
#pragma shader_feature A

shader_feature相对于multi_compile，有两个明显的不同，第一是，在打包时，unity会获取所有材质的关键字信息，只把使用了的变体组合打进包里。第二是，如上述定义的，其实是#pragma shader_feature _ A的简写，即存在A和不存在A两个变体，multi_compile则必须要手动声明没有关键字的情况。除此之外，暂未发现其他差别。这些不同指向了它的适用情况，即**在编辑器上通过材质，去组合这些关键字，而不需要在运行时动态修改的情况。缺点是，在运行时动态开关关键字，可能会带来错误的显示，因为对应的变体列表很有可能不在包体里**。

### 2.3默认开启的关键字
![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1714472187482-14715316-c35d-47ad-ae88-51b1485027db.png)

## 三.SVC
### 3.1 热更新
shader_feature很好，但在热更的需求下，会存在一些问题，为了能够让unity正确的获取到依赖shader的材质，以拿到所有的使用了的变体组合，需要把shader和mat打在同一个AB里，基于这会带来毁灭级的shader冗余和内存压力（重复加载）。

如果全部使用multi_compile，把所有shader打在一个AB里，运行时可以任意的切换，也不会有AB冗余，但大量未使用的变体，同样会带来不可接受的内存开销。

那么自然是优点全都要，缺点想办法解决。Unity提供了ShaderVariantCollection（以下简称SVC），SVC可以用来记录shader_feature和multi_compile产生的变体，Unity中可以通过Create-> Shader Variant Collection创建SVC对象。如果恰好，里面只记录了项目中使用了的变体组合，并且将SVC文件和shader打包在一起，即可避免变体丢失，避免AB冗余。

### 3.2预热
另一个方面，游戏初始化的时候，一般需要把所有shader加载出来，并调用Shader.WarmupAllShaders来预热（主要耗时点）所有变体，避免游戏过程中的卡顿。随着变体数量的增加，会导致游戏的启动时间更长，而SVC对象提供了仅预热该对象记录的变体，可以实现按需预热，或者分帧预热，在官方文档中，这是SVC的真正用途。

### 3.3收集
问题在于如何做到恰好，不遗漏任何变体。

在unity编辑器中，变体是自动记录的：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1714472323268-ab2532dc-82a6-43b3-9049-da9018215228.png)

只要将游戏在编辑器里充分的跑一遍，渲染到全部的组合情况，再save to asset。或者手动在svcAsset里去add所有组合情况。对于稍微有点规模的项目，这两张方案都会很容易导致变体收集的或多或少，导致渲染错误或者性能问题。

在unity编辑器里，创建一些简单的3d对象，放置于摄像机的可见位置，如cube，将所有的材质依次加载出来，赋予这些3d对象，使其渲染到gameview，在不同的光照环境里重复这个行为。然后依次打开所有的场景文件，通过代码计算相机的最终位置，使其能够渲染出场景里的所有3d对象。然后将unity自动收集到的变体存到svc资产中。这也是项目内，变体收集的大概实现思路。

## 四.变体收集的实现步骤及使用
### 4.1引入
首先引用"com.mole.svc": "http://192.168.2.240/programmer/publicmodule.git?path=/Plugin/MoleShaderVariantCollection"，点击MoleTools/Shader/创建ShaderVariantCollectionSetting，得到如图的配置资产：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1714472484102-57542aab-eb18-4da5-9c97-87d0b74feae8.png)



### 4.2自动收集算法实现流程
0.如果Setting.Enabled==false，结束流程

1.重新导入所有Shader

2.清空Unity已经收集到的所有变体

3.对Setting.MaterialRootPath数组里循环进行收集，拿到里面所有的预制体，再拿到预制体所有Renderer依赖所有的材质，遍历检查材质的ShaderName，是否在Setting.BuildInSupportLightingShaderName或者Setting.SupportLightingShaderName列表里，如果在，将mat添加到allMaterials。

4.将Setting.MaterialRootPath路径下的所有材质，添加进allMaterials（如果不在里面的话）。

5.逐个打开Setting.CustomLightingEnvScenes里的场景，如果是空数组的话，将自动在Setting.TempPath下创建一个默认场景，其灯光设置由代码默认生成，具体可以打开这个场景查看。

6.在摄像机视野范围内创建4*4*4个Sphere，依次从allMaterials里取出材质赋予这些Sphere，并将meshrenderer的设置改为如下，渲染一帧。：

```csharp
renderer.lightProbeUsage = LightProbeUsage.BlendProbes;
renderer.reflectionProbeUsage = ReflectionProbeUsage.BlendProbes;
renderer.shadowCastingMode = ShadowCastingMode.On;
renderer.receiveShadows = true;
renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
```



7.重复第5步和第6步，直到在Setting.CustomLightingEnvScenes中的每一个场景里，渲染一遍所有的材质。

8.依次打开Setting.SceneRootPath路径下的所有场景，将相机调整到合适位置，使其能够渲染到所有物体，渲染一帧。

9.重复第8步，直到所有场景都被渲染一次。

10.拿到Unity收集到的变体信息，在Setting.ShaderVariantsPath下，按照Shader的原始路径分文件夹，为每一个Shader创建SVCAsset。并不是所有Shader都会创建变体文件，有一些情况会被跳过：shader被记录在Setting.ExcludeShaders || shader路径中包含“Editor” || shader路径中包含Setting.ManualCollectionIgnorePaths中的任意一项，

11.与上一次收集的结果（LoadSCVAsset）做对比，当某个shader的svc变体信息与上一次不一样时，覆写（注意，此处为替换逻辑，而非在原来的基础上加减变体）。

12.遍历所有SVC对象，将变体数量大于Setting.MaxVariantCountPerSVC的，拆分为多个SVC资产，防止运行时调用SVC的预热时，单帧耗时过高。

13.在Setting.ShaderCollectionName指向的预制体里，找到ShaderCollection组件，并将所有收集到的SVC（变体数量小于等于Setting.MaxVariantCountPerSVC的）文件填充到该组件的shaderVariantCollections属性（数组）里



### 4.3手动收集
由于我们的变体剔除策略（后面会详细说），在自动收集后，可能需要进行手动收集。需要确保在手动收集前，进行过自动收集，手动收集相当于执行了自动收集的第10-13步。不同的是，手动收集的变体，会存放在Setting.ShaderVariantsPath + "/" + Setting.ManualRelativePath下。另外与自动收集不同的是，手动收集的结果是只加不减的。



点击MoleTools/Shader/手动收集变体即可进行收集



### 4.4收集自定义参数总结
| Setting.Enabled | 收集功能的开关 |
| :--- | :--- |
| Setting.ShaderVariantsPath | 生成的SVC存放的根目录 |
| Setting.ManualRelativePath | 手动收集的SVC存放在Setting.ShaderVariantsPath下的相对路径 |
| Setting.ShaderCollectionName | 引用所有SVC的预制体路径 |
| Setting.MaxVariantCountPerSVC | 单个SVC最多容纳的变体数量（默认30） |
| Setting.CustomLightingEnvScenes | 不同灯光环境的场景，用于收集材质在不同灯光环境下的变体 |
| Setting.SceneRootPath | 场景文件的根目录，用于收集场景内的变体 |
| Setting.MaterialRootPath | 路径下所有预制体依赖的支持光照的材质+路径下所有的材质 = 要在多光照条件下进行变体收集的材质列表 |
| Setting.SupportLightingShaderName | 步骤3进行材质过滤 |
| Setting.ExcludeShaders | 跳过这些shader的变体收集 |
| Setting.ManualCollectionIgnorePaths | 跳过该路径下的Shader收集 |


## 五.变体剔除
### 5.1剔除方案
前面说到multi_compile适用于在运行时动态切换宏，并且可以放心切换，因为所有的变体都打进了包体。  
事实上这并不绝对，这和我们是否剔除这些变体有关，Unity文档里建议我们在IPreprocessShaders.OnProcessShader回调里剔除掉包含互斥关系关键字的变体，因为运行时不可能使用这些组合，剔除这些不可能出现的组合，会使得减少变体数量以优化性能。

变体剔除还有另一个好处，尽管我们使用了SVC收集了所有使用的变体，但在打包编译时，没有被收集的依然会进入编译环境，在OnProcessShader里剔除掉不在SVC里的shader_feature关键字组合，有助于加快打包速度。



multi_compile定义的宏产生的变体剔除，有不同程度的方案：

比如使用黑名单，在每个变体的关键字里查不可能同时出现的关键字组合，找到就剔除，这是相对宽容保险的策略。

或者白名单，仅允许我们定义的multi_compile组合通过，其余的全部剔除，这种策略比较激进，使multi_compile在某种程度上退化为了shader_feature。

或者增加一些规则，路径规则，命名规则，平台规则之类的。

我们采用了白名单策略。

### 5.2变体剔除流程
0.如果Setting.Enabled==false，退出

1.包含在Setting.ExcludeShaders里的，不剔除，退出

2.“Resources/unity_builtin_extra”，“Library/unity default resources”下的unity默认的Shader，不剔除，退出

3.找不到对应shader的SVC资产的，不剔除，退出

4.将该shader对应的手动收集的SVC和自动收集的SVC的变体列表合并在一起，产生一个<font style="color:rgb(243, 50, 50);">变体列表</font>

5.要编译的变体，如果和<font style="color:rgb(243, 50, 50);">变体列表</font>里的任意一种变体的关键字组合完全相等，确定不剔除

6.当不完全相等时，判断要编译的组合，是否等于 “<font style="color:rgb(243, 50, 50);">变体列表里任一变体的关键字组合 + 任意数量但不重复的Setting.GlobalKeywords中的关键字</font>”。如果是，确定不剔除

7.其余情况则全部剔除

### 5.3剔除自定义参数整理
| Setting.Enabled | 剔除功能的开关 |
| :--- | :--- |
| Setting.GlobalKeywords | 剔除白名单 |
| Setting.ExcludeShaders | 跳过这些shader的剔除 |


## 六.特殊情况
在这套收集和剔除的逻辑之上，有一个优先级最高的规则，即下图中配置的Shader，正如其标题，无论我们是否收集，是否剔除，这些shader里shader_feature和multi_compile定义的所有变体，会全部生成进入包体。需谨慎的使用。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1714473466646-4467b083-47d9-4d0c-a5e9-5266c666cfc1.png)

