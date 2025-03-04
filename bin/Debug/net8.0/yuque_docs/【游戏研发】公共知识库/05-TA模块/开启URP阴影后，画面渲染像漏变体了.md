设置CastShadow on后，没有运行一次游戏，然后提交URPAsset显示变动的m_prefilteringModeMainLight的参数。导致编辑器图形界面打shader AB包和命令行打shader AB包结果不一致的问题。



其**原因**为：

1. Unity打Shader AB包时，变体从排列组合、剔除到编译实际分为三个阶段：预过滤(Prefiltering：从空的变体集合到根据svc及urp规则排列组合出此次打包最大可能的变体集合)、剔除(Stripping：从预先准备好的最大可能变体集合中剔除不符合URP要求、不符合Mole.SVC要求的)、编译(Compiling：对符合要求的变体组合进行编译)。prefilteringModeMainLight主要影响预过滤阶段的变体集合。剔除阶段只会对其参数进行赋值操作，且发生在OnProcessShader初期。因此，变体关键字剔除过程中，程序关于相关特性的剔除是准确的。
2. 问题在于仅仅设置CastShadow on，没有提交prefilteringModeMainLight时，预过滤阶段的变体没有受到CastShadow的影响。也就是说，变体预过滤的过程，会受prefilteringModeMainLight的影响，这也是为什么不同方式打包结果不一致的问题，其问题出在一开始的地方。另外由于预过滤的过程不是开源代码，所以也没法确认里面到底是怎么进行的。经断点调试分析推测出来的具体原理可见[https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/wxfe39/ukt6aebbe2xm0ggc#wICL](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/wxfe39/ukt6aebbe2xm0ggc#wICL5)  


## 如此，就可以解释为什么开启CastShadow on后，头发失效
预过滤的变体中不包含头发的AdditionalLightShadows版本，所以头发全部被剔除了。

## 至于FishStandard，为什么只有部分Cutout或者半透明出问题，不透明没有出问题？
这是因为其不带有MAIN_LIGHT_SHADOWS的变体组合依然被打出来了，所以apk直接用了此组合。

## 为什么半透明睫毛出问题了？
因为svc里关于_ALPHAPREMULTIPLY_ON的组合全部带有了MAIN_LIGHT_SHADOWS，导致预过滤没收集到MAIN_LIGHT_SHADOWS时，半透明的组合直接不存在了，那么也无从谈起去从预过滤组合中寻找匹配svc里半透明的组合。

## 那么为什么开启CastShadow on后，头发会失效，但是FishStandard的不透明不会出问题？为什么FishStandard不透明的不带有MAIN_LIGHT_SHADOWS组合依然能被正常打出来？
这是因为**URP阶段的特性关键字剔除机制**决定的。经断点调查发现，在预过滤完成后，Unity首先完成基于内置的剔除，然后便是基于urp的关键字剔除，最后才是第三方的关键字剔除。而在urp的关键字剔除阶段，其主要特征就是会根据项目不同特性的开关状态，来决定剔除或保留对应关键字。在这之中，又涉及到urp如何识别不同特性对应的关键字，以及判断不同shader是否支持urp的该特性。

  
实际上，当一个自定义shader的某项关键字设置和urp的某项关键字设置完全一样时，urp即认为此shader具有该特性，且其变体组合会受到项目特性开关的影响。

举例来说，URP的_ADDITIONAL_LIGHT_SHADOWS的关键字设置写法如下： 

`#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS`

那么，只要自定义shader中存在那一行，urp就会认为该shader支持额外光阴影特性，并接管其关键字剔除。

以MAIN_LIGHT_SHADOWS为例：

`#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREENE`

当自定义shader内包含完整的这一行时，也就会在剔除阶段完全按照URP的逻辑来剔除相关关键字。但要注意，必须是完全一致才行，假如自定义shader中只有MAIN_LIGHT_SHADOWS，MAIN_LIGHT_SHADOWS_CASCADE，但没有MAIN_LIGHT_SHADOWS_SCREEN时，就不会完全符合URP逻辑的预期。

  
URP接管shader的部分关键字剔除设置这种影响是很严格的，当urp认为该shader具有该特性时，那么当项目特性打开时，这个shader就无法编译出不带有该特性关键字的变体组合，也就是说无法局部关掉该特性。当项目特性关闭时，这个shader也无法编译出带有该特性关键字的变体组合。  



### 具体分析：
![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/1660870/1733823760056-bb054430-67aa-4424-b332-b8e127173e46.jpeg)

此图描述了变体剔除的实际流程及集合演变情况，下面将以此图为例说明上述几个问题的具体情况：



| | Case: 出问题 | Case: 正常 | Case: 无阴影 | Case: 无阴影但有收集 |
| --- | --- | --- | --- | --- |
| 设置 | | | | |
| URPAsset::Main Light -> Cast Shadows | On | On | Off | Off |
| URPAsset::Prefiltering Mode Main Light Shadows | Remove | Select Main Light And Cascades | Remove | Select Main Light And Cascades |
| URPAsset::Additional Lights -> Cast Shadows | On | On | Off | Off |
| URPAsset::Prefiltering Mode Additional Light Shadows | Remove | Select Only | Remove | Select Only |




#### 开启CastShadow后，头发失效
下面分别以两份svc文件，一份是只包含SRP:no keyword的测试用，另一份是项目正常使用的svc文件，来对比分析。

| | Case: 出问题 | Case: 正常 | Case: 无阴影 | Case: 无阴影但有收集 |
| --- | --- | --- | --- | --- |
| Hair Shader 理论可能变体总数 | multi_compile(no unity default or urp default): 192, multi_compile: 4608, shader_feature: 24, total: 110592 | | | |
| Hair Shader + (测试用SVC，no shader_feature) | | | | |
| 1. 变体预过滤阶段 排列出的变体总数 | 192, <br/>no main shadow, no addi | 384, <br/>add {mainShadow, addiShadow},<br/>add {mainShadowCascade, addiShadow}, <br/>remove {_} | 192,<br/>no main shadow, no addi | 384,<br/>add {mainShadow, addiShadow},<br/>add {mainShadowCascade, addiShadow}, <br/>remove {_} |
| 2.1 剔除阶段中的URP剔除环节 剩余变体总数 | 0 | 384 | 192 | 0 |
| 2.2 剔除阶段中的Mole剔除环节 剩余变体总数 | 0 | 0 | 32 | 0 |
| Hair Shader + (项目正常使用的svc文件) | | | | |
| 1. 变体预过滤阶段 排列出的变体总数 | 384, <br/>add {alphaTest, transparent}, <br/>keep {_} | 768,<br/>add {mainShadow, addiShadow}, <br/>add {mainShadowCascade, addiShadow},<br/>add {alphaTest, transparent}, <br/>remove {_} | 384,<br/>add {alphaTest, transparent}, <br/>keep {_} | 768,<br/>add {mainShadow, addiShadow}, <br/>add {mainShadowCascade, addiShadow},<br/>add {alphaTest, transparent}, <br/>remove {_} |
| 2.1 剔除阶段中的URP剔除环节 剩余变体总数 | 0 | 768 | 384 | 0 |
| 2.2 剔除阶段中的Mole剔除环节 剩余变体总数 | 0 | 32 | 32 | 0 |


观察：

1. 对比**测试用svc**和**正式svc**的情况可以发现：_**svc的差异**_本质上会影响**1.变体预过滤阶段**中排列出的变体总数，但对于后续阶段的发展不起影响。
2. 对比测试用svc和正式用svc中**前两个case**可以发现：_**URPAsset的设置**_影响了**1.变体预过滤阶段**中排列出的总数，也影响了**2.1剔除阶段中的URP剔除环节**的剔除总数，也影响了**2.2剔除阶段中的Mole剔除环节**。



推论：

1. _**svc差异**_只影响**1.变体预过滤阶段**，这是符合预期的，它决定了会有多少Shader_Feature额外变体组合被排列到初始的变体集合中，而不应该影响后期URP变体剔除的逻辑。Mole会受影响，因为Mole主要基于SVC和强制关键字，将multi_compile退化成了shader_feature。不过Mole受到的影响在这个问题里并不重要，因为问题出在Mole环节之前。
2. _**URPAsset的设置**_对**1.变体预过滤阶段**的影响是很明显且确定的：**PrefilteringMode决定了预过滤**中是否会带上mainLightShadow或addiShadow相关的关键字。CastShadow并不决定这一点。
3. _**URPAsset的设置**_对**2.1剔除阶段中的URP剔除环节**的影响是有的，但逻辑并不明确，只能得出：
    1. Case出问题时，变体集合中的组合都不带有Shadow相关关键字。然后它们都被剔除了。
    2. Case无阴影但有收集时，变体集合中的组合都带有Shadow相关关键字。然后它们都被剔除了。
    3. Case正常时，变体集合中的组合都带有Shadow相关关键字，且它们都被保留了。
    4. 因此，**CastShadow决定了剔除阶段中URP剔除环节**是剔除MainLightShadow相关变体，还是保留相关变体。
4. 然后，根据**正常和无阴影的Case**来看，_**URPAsset设置**_并不会影响**2.2的Mole剔除环节**的逻辑，Mole剔除环节的逻辑不曾改变，只是不同Case下传过去的数据集不一样，导致有的逻辑表现了出来，有的逻辑直接return了。



结论：

出问题时，头发Shader变体只排列组合了不带有Shadow关键字的版本，然后便在2.1URP剔除环节，被CastShadow给全部剃掉了。具体看代码可以发现：因为头发Shader带有Shadow相关关键字，支持Shadow的特性，然后URP管线开启了此特性，但是变体集合中却收到一批未开启Shadow的组合，所以URP便将这批未开启Shadow的组合剔除了。



#### FishStandard，半透明出问题，不透明没有出问题
| | Case: 出问题 | Case: 正常 |
| --- | --- | --- |
| FishStandard Shader 理论可能变体总数 | multi_compile(no unity default or urp default): 16384 | |
| FishStandard Shader(不支持AddiShadow) + (测试用SVC，no shader_feature) | | |
| 1. 变体预过滤阶段 排列出的变体总数 | 16384, <br/>no main shadow | 32768, <br/>add {mainShadow},<br/>add {mainShadowCascade}, <br/>remove {_} |
| 2.1 剔除阶段中的URP剔除环节 剩余变体总数 | 16384 | 32768 |
| 2.2 剔除阶段中的Mole剔除环节 剩余变体总数 | 128 | 432 |
| FishStandard Shader(不支持AddiShadow) + (项目正常使用的svc文件) | | |
| 1. 变体预过滤阶段 排列出的变体总数 | 16384, <br/>no main shadow | 32768,<br/>add {mainShadow}, <br/>add {mainShadowCascade},<br/>remove {_} |
| 2.1 剔除阶段中的URP剔除环节 剩余变体总数 | 16384 | 32768 |
| 2.2 剔除阶段中的Mole剔除环节 剩余变体总数 | 128 | 432 |


观察：

1. 对比两个Case，会发现他们各环节逻辑都看着没什么问题，唯一问题只是**1.预过滤阶段**排列组合出的变体总数有点差别。而关于这点我们已经从上一个案例中分析得出，URPAsset的prefilteringMode确实是影响这点的关键。
2. 将**FishStandard与Hair的Case: 出问题**进行对比会发现，两者都是没有排列出阴影的变体，且都开了CastShadow，但是为什么FishStandard还能保留正常的变体，而Hair在2.1阶段就被剔除干净了？



推论：

1. 两个Shader的Case都是一样的，唯一差别只在于Shader本身了。确认Shader后可以发现，FishStandard不支持AddiShadow，而Hair支持AddiShadow。因此可以得出，MainLightShadow有无在当前情况下并未影响到URP剔除环节，而AddiShadow确实影响了URP剔除环节。



结论：

断点调试代码后发现，其实AddiShadow、MainLightShadow都会影响URP剔除环节，但是FishStandard和Hair的MainLightShadow特性支持不完全。原生lit.shader中的MainLightShadow包含_MAIN_LIGHT_SHADOWS, _MAIN_LIGHT_SHADOWS_CASCADE, _MAIN_LIGHT_SHADOWS_SCREEN，而FishStandard.shader和Hair.shader均只包含前两个关键字，这导致了URP剔除环节在检测MainLightShadow特性时，将这两个shader认为不涉及MainLightShadow特性，从而并未对其变体组合进行剔除。而Hair.shader包含了完整的_ADDITIONAL_LIGHT_SHADOWS，因此，URP剔除环节在检测Hair的AddiShadow特性时，将其认定为涉及AddiShadow特性，并根据当前AddiShadow的开启状态，将Hair变体组合中未开启AddiShadow关键字的组合全部剔除了。



## 管线开启MainLightShadow，相机没开RenderShadow时，渲染会如何进行？
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1733729037258-3a7473eb-7ee3-4aaa-807d-7b3b3d831a2b.png)

管线开启MainLightShadow，但是相机关闭RenderShadows时，Shader关键字依然带有MainLightShadow。但是相机不会走MainLightShadowPass，同时MainLightShadowMapTexture会采用一张1x1的空图。  



## 手动收集变体来修复这个问题是否可能？
mole.svc的手动收集变体实则是将URP变体收集的结果拆分成单shader单svc文件的形式，然后将它们保存到Assets/ShaderVariants/ManualMerge文件夹中。数据来源就是URP的运行时自动变体收集，在这个流程中不太适合手动选择一份svc文件，然后往里添加变体组合。因为svc文件本身的Editor GUI做的很差，不适合手动编辑。

同时，哪怕手动编辑svc文件，使其有了预期的组合，这个组合会出现在变体预过滤排列组合的过程中，然后很有可能会在到达molesvc剔除流程前被URP的剔除流程给影响到。

