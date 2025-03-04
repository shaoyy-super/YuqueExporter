# 一、大致介绍
在没有外部插件干扰的情况下，**Unity Shader变体统计**工作状况如下：

1. 收集变体时可以收集multi_compile或者shader_feature的关键字。
2. 只添加shader，没有关键字组合：不增加或者减少变体数，依然会打全部multi_compile。
3. 一个no_keywords组合：不增加或者减少变体数，依然打全部multi_compile。
4. n个其中包含**不重复shader_feature关键字组合**的新条目: **新变体数** = **原变体数** * **2^n**。
5. unity_defined的关键字取决项目设置是否strip某些特性。因此需要注意这些关键字很有可能不会被统计到变体数中。会被默认剔除掉。

**Asset Bundle Browser**工作机制如下：

6. 两个AB包涉及到同一个shader时，最终收集（非剔除阶段）阶段的变体数以第一个AB包的结果为准。也有可能会导致同一个shader被打多次，需要具体测试搞清原理。

   实际情况分为两类：

    1. **Shader位于Assets内，AssetBundleBrowser能顺利引用到Shader。**
        1. 单独将一个shader打成AB包。变体数正确，等于全部multi_compile组合数。
        2. 单独将一个svc打成AB包。变体数正确，同时可以看到AssetBundleBrowser内自动包含了相关shader文件，变体等于全部multi_compile组合数 * 2^(不重复的shader_feature组合数)。
        3. 多个AB包分别放入不同的svc文件，但是都自动引用了同一份shader，且没有一份AB包手动包含了该Shader。对于每份AB包自身而言，变体数正确，但是编译了多次同一个shader，且对于整个项目而言，变体数并非正确，无法找到一个AB包能明确说里面的shader包含了整个项目所需的全部变体。
        4. 多个AB包分别放入不同的svc文件，其中一份AB包手动包含了该Shader，能看到只有手动包含的那份AB包中有相关shader，其他AB包中原来自动引用的shader也不见了。对于手动包含了该shader的AB包而言，变体数正确，但从项目角度来说，漏了其余所有AB包里的shader_feature变体。
        5. 不同svc文件以及相关的shader文件都打进同一个AB包时。变体数正确，svc所需的全部变体都进行了编译，且只编译了一次。
    2. **Shader位于Packages内，AssetBundleBrowser不能顺利引用到Shader。**
        1. 单独将一个shader打成AB包。变体数正确，等于全部multi_compile组合数。
        2. 单独将一个svc打成AB包。变体数正确，同时没法看到AssetBundleBrowser内自动包含了相关shader文件，只能推测其包含了，变体等于全部multi_compile组合数 * 2^(不重复的shader_feature组合数)。
        3. 多个AB包分别放入不同的svc文件，且没有一份AB包手动包含了该Shader，其他AB包因为看不到所以只能推测自动引用了同一份shader。对于每份AB包自身而言，变体数正确，但是编译了多次同一个shader，且对于整个项目而言，变体数并非正确，无法找到一个AB包能明确说里面的shader包含了整个项目所需的全部变体。
        4. 多个AB包分别放入不同的svc文件，其中一份AB包手动包含了该Shader，其他AB包因为看不到所以只能推测自动引用了同一份shader。对于手动包含了该Shader的AB而言，变体数正确，但从项目角度来说，漏了其余所有AB包里的shader_feature变体。
        5. 不同svc文件以及相关的shader文件都打进同一个AB包时。变体数正确，svc所需的全部变体都进行了编译，且只编译了一次。



# 二、Unity Shader Compile Pipeline
## [optional] pre Shader Variant Collection 变体收集，生成SVC文件




![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/1660870/1733824310052-cb1d06a8-afab-44ce-b5ba-85aae93aa2b2.jpeg)



## 1. Execute Build Asset Bundle


## 2. 【变体预过滤阶段(Prefiltering)】（闭源，只可大致确认流程）Build List based on the Unity Default Rule 排列组合默认需要编译的变体
+ Multi_Compile的会被默认认为需要编译
+ Shader_feature的取决于是否有相关材质球被认为需要编译
+ 某些Unity内置关键字可能会在此步被strip掉, 如：会默认必定编译_MAIN_LIGHT_SHADOWS，而不会编译_的版本。
+ Preparing Variants发生在此步，此时尚未到三方插件的ShaderPreprocessor，所以如果数目不对，肯定是shader和变体收集的问题。
+ Preparing Variants的数目可能涉及到多pass变体数的总和，所以可能会特别大，当其进行到StandardLit pass时，变体数精准等于计算出来的结果。
+ 此阶段当预编译变体数 >= 1,000,000时，会分成多批进行处理。
+ 理论上来说
    1. _**multi_compile且去掉strip后的组合数**_ = _**每行multi_compile(不包含strip)关键字个数的累乘**_
    2. _**此阶段最后需要编译的变体数**_ = 

**(2^(**_**材质球中所新引用且不重复的shader_feature关键字个数**_

 + _**SVC里存在的**_**不重复shader_feature关键字组合**_**数**_**))**

 * _**multi_compile且去掉strip后的组合数**_

 * _**Graphics Tier数**_

 * _**Platform API数**_

### 实际原理：
#### Unity Shader Variant Prefiltering (Unity Editor mechanism)
[ShaderKeywordFilter.FilterAttribute](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/ShaderKeywordFilter.FilterAttribute.html)

上面这篇文章介绍了Unity打AB包初期变体预过滤过程中所使用的机制。

##### Unity 打AB包shader预过滤脚本执行顺序
1. **UnityEditor.BuildPipeline.BuildAssetBundles**

开始执行打AB包

2. **Loop: PerShaderPass Prefilter, and then Strip**
    1. --- **UnityEditor.BuildPipeline.[Native Transition]** ---

打AB包具体实现在内部封闭代码中。

    2. **UnityEditor.ShaderKeywordFilter.ShaderKeywordFilterUtil.GetKeywordFilterVariants**

根据QualitySettings里的Levels数建立对应数目的关键字过滤规则集。

        1. **UnityEditor.ShaderKeywordFilter.SettingsNode.GetVariantArray**

根据Level里的插件（如: URP、HDRP）Asset(SettingsNode)以及Asset里的Renderer(SettingsNode)获取项目不同特性的开关状态，并预增加或删除相应关键字规则。每条规则(SettingsVariant)都来自SettingsNode的属性反射data。(SettingsNode的判断取决于class里面参数及引用资源是否含有ShaderKeywordFilter的相关属性)

    3. **--- UnityEditor.BuildPipeline.[Native Transition] ---**

打AB包具体实现在内部封闭代码中。变体预过滤阶段完成。将根据不同项目规则排列组合出的所有变体(此时变体总集也包含Graphics API差异，Graphics Tier差异)传给ProcessShader，然后进行下一步剔除变体操作。

    4. UnityEditor.Build.BuildPipelineInterfaces.OnPreprocessShaders

进入变体剔除流程

    5. UnityEditor.Rendering.ShaderVariantStripper.OnProcessShader
    6. ...

##### 案例分析预过滤排列组合出的变体数
假设捕鱼项目中的QualitySettings设置5级Level。除了Low，其余Level共用同一项URPAsset，Low使用URPAsset Low。URPAsset开启MainLightShadows以及AdditionalLightShadows。URPAsset Low两者都关闭。

Shader使用Hair.shader。阴影全都不开的状态下，ForwardLit pass的变体总数192个。

###### 断点测试
1. SettingsVariant(即预过滤变体规则列表，一条对应一组ShaderKeywordFilters规则): 16组 >= 5(5个URP Assets) x 3(每个Asset 3个RendererData)(大于的原因，有资源引用)
2. 实际不重复变体总数：576

预期来源：

    - URPAsset Low: 192 * 1 (阴影都不开，等于默认变体总数)
    - URPAsset:  192 * 2 * 1 = 384	 
        * (MainLightShadows开启，对于Hair.shader会产生_MAIN_LIGHT_SHADOWS以及_MAIN_LIGHT_SHADOWS_CASCADE两种变体，不会产生_变体)
        * (AdditionalLightShadows开启SelectOnly，仅会产生_ADDITIONAL_LIGHT_SHADOWS两种变体)

预期： 192 + 384 = 576

预期 = 实际，符合推测



## 3. 【变体剔除阶段(Stripping)】Call URP ShaderPreprocessor URP变体收集剔除流程，其实主要是剔除
+ Call RenderPipeline.Core ShaderPreprocessor
    - do beforeShaderStripping
    - do URP ShaderScriptableStripper for every ShaderVariant // Unity URP渲染特性及部分内置关键字剔除发生在此环节。
        1. URP剔除特性，有以下例子:
            1. DeferredStencil、BlitHDROverlay...
            2. Universal 2D、Meta、ShadowCaster、Decals...
            3. DebugDisplay、ForwardPlus、LightLayers...
        2. URP剔除特性有以下注意点：
            1. 假如自定义shader选择部分关键字功能仿照URP的来，比如说MAIN_LIGHT_SHADOWS相关，ADDITIONAL_LIGHT_SHADOWS相关。如果关键字设置和unity完全一样，则当Unity开启相关功能时，会把shader里没带上相关关键字的组合给强制在这步剔除掉。可以认为，当此部分功能选择仿照urp来时，就要明白这块关键字会完全受urp特性开关控制。
    - do SVC filter // URP根据SVC文件进行变体收集
        1. _**此阶段最后需要编译的变体数**_ = _**上一阶段最后的变体数**_ - _**被剔除URP特性的变体数**_
    - do afterShaderStripping



## 4. 【变体剔除阶段(Stripping)】(Optional) Call External ShaderPreprocessor
+ Call **Mole.SVC ShaderPreprocessor**
    1. 判断Mole.SVC.settings是否启用
    2. 判断shader是否在Mole.SVC.settings的排除列表里，如果在排除列表则不执行该插件的剔除工作。(依然会受到URP ShaderPreprocessor的影响)
    3. 判断shader是否是Unity内置资源，如果是则不执行相关剔除
    4. 在上一阶段最后变体集的基础上，再**根据Mole.SVC设置的规则进行过滤**。不在SVC规则内的即剔除，无关multi_compile还是shader_feature，哪怕以材质球形式存在也得在SVC中明确才行。  
Mole.SVC匹配关键字组合的逻辑：
        * 组合的shader和pass != SVC当前条目的 不匹配
        * 组合的关键字个数 != SVC中当前条目关键字个数的 不匹配
        * 组合的关键字个数及内容 完全= SVC中当前条目的 匹配
        * 组合的关键字个数及内容 完全= SVC当前条目 + Mole.SVC全局关键字的 匹配
    5. _**此阶段最后需要编译的变体数**_ = _**上一阶段最后的变体数**_** **- _**不在Mole.SVC变体规则里的变体数**_
    6. **注意，对此阶段来说，multi_compile和shader_feature的关键字没有任何区别，因为这一步发生在multi_compile和shader_feature起效之后**。
    7. Mole.SVC将过多的SVC拆分成几个小SVC后，不会删除原来大的SVC，但是Shader里的Prefab在引用svc时不会引用原来大的SVC，这不是错误。
    8. 此阶段**技巧**：
        1. Exclude Shaders里的Shader变体收集 和没有第三方插件时的流程及结果是一致的。
        2. Global Keywords里指定的关键字组合会被保留下来。但是需要注意，它只会保留变体收集原生流程里已经收集到的带此关键字组合的变体，原生流程里收集不到此关键字的话， 哪怕在Global Keywords里指定了此关键字，也依然没法往变体列表中新增此关键字组合。  
这就是为什么全局渲染特性，不应该用shader_feature的原因。全局渲染特性很有可能没有相关的材质球能提供相关的svc记录，这就导致变体收集原生流程不会收集此关键字的组合，进而导致其不会参与编译。



## 5. 【变体编译】Compile Shader
### PS. 总结
1. 理论上，只要没有URP和Mole.SVC以外插件的影响，**2阶段中的 **_**初始变体数**_ 永远 _**>=**_ 后续 _**各阶段筛选后的变体数**_ 。
2. 不存在于 _2阶段初始变体集合_ 中的变体，除非采用新的第三方插件或者自己编写ShaderPreprocess，否则是无法通过修改Mole.SVC设置来将其添加进最后需要编译的变体集合的。
3. Mole.SVC设置 - Exclude Shaders里Shader的变体收集流程 和没有Mole.SVC插件时的流程及结果是一致的。也就是说Exclude Shaders里的Shader会只走Unity URP及非Mole.SVC插件的变体收集剔除流程。
4. Exclude Shaders的设置高于Mole.SVC插件内其他设置。
5. **Global Keywords里指定的关键字组合会被保留下来。但是需要注意，它只会保留变体收集原生流程里已经收集到的带此关键字组合的变体，原生流程里收集不到此关键字的话， 哪怕在Global Keywords里指定了此关键字，也依然没法往变体列表中新增此关键字组合。只能保护其不被剔除，没法往集合中新增变体**。
6. 要自己手动添加变体，目前比较麻烦，不太推荐这种做法，大概率不会被Mole.SVC识别到。

### PS. Recommended Practice 推荐实践
1. 基于上述结论，需要 _**运行时通过代码进行全局渲染特性控制**_ 的，如：雨、雪、全局压扁等全局渲染特性，必须用**multi_compile**来指定关键字。
2. 需要 _**运行时通过代码动态开关单个材质**_ 的，如：溶解、单个fov控制、单个压扁等材质渲染特性，推荐用**multi_compile_local**来指定。
3. 需要强制编译的，都采用multi_compile及其衍生系列。
4. _**只在编辑模式下修改全局渲染特性、不涉及运行时开关**__，且相关Shader以材质球形式被引用(这意味着编辑器可以通过脚本开关其shader_feature关键字。相对的就是有些shader，默认强制编译，不会以材质球的形式去引用它，此时就无法通过脚本开关其shader_feature关键字，也就会导致此类shader永远不会编译shader_feature系列变体。)_ 的，如：不同平台设置不同的SSAO模式、debug或release包带不同特性时，推荐用**shader_feature**来指定。

> 总体来说，shader_feature使用范围比较局限。比较类似于URP在执行变体收集剔除时，通过ShaderPreprocessor根据项目设置，将不用的特性剔除出变体集合，如：LightLayers、ForwardPlus、SoftShadows等。不过需要注意，URP剔除的特性，有不少就是之前提的不会以材质球形式被引用的情况，所以只能大概相比，没法完全当做一样的情况。LightLayers可以被认为比较适合用shader_feature来指定，不过这会引起编辑器修改此类设置时，引起全局静态材质球资源被添加或删除某个shader_feature关键字，进而引起大量的git文件修改操作。  
- 从**规避修改不必要的文件角度**来说，或许继续通过**multi_compile**来实现编辑模式下修改全局渲染特性，但是维护自定义的ShaderPreprocessor来剔除不必要的关键字组合比较合理。  
- 从**编译性能的角度**来说，不涉及运行时修改的全局渲染特性，**shader_feature**更为合理，其能确保收集变体时，仅收集必要的变体，再在此基础上进行剔除。可以省去大量的收集时间。
>

5. _**只在编辑模式下修改材质， 不涉及运行时开关**_ 的，如：是否带有法线图、是否带有UV动画等，推荐用**shader_feature_local**来指定。
6. 如果部分功能关键字需要仿照URP的来，那就确保那个功能完全按照URP的写法设置。这样才能确保后续通过项目设置调整特性开关时，shader的这部分特性能符合预期，否则会很难排查。



# 三、案例分析（不重要。细节过于复杂，找出经典案例比较麻烦）
## UTRendering_Test里的FASceneObject Shader
以UTRendering_Test中 r/rendering-0.1.14版本的FASceneObject.shader为例，分析Shader变体收集及编译过程中的变体数变化流程。

### Keywords [20] [49,152 * 32 = 1,572,864]
#### multi_compile [16] [3 * 26 * 4 * 28 = 3 * 2^16 = 196,608] [196,608 / 2 / 2 = 49,152]
+ _  ||  _LOW_DETAIL
+ ~~_  || ~~ _MAIN_LIGHT_SHADOWS
+ _  ||  _RECEIVE_SELF_SHADOW  ||  _RECEIVE_ADDITIONAL_SELF_SHADOW2
+ _  ||  _RECEIVE_ADDITIONAL_SELF_SHADOW
+ _  ||  _CUSTOM_SCREEN_SPACE_OCCLUSION
+ ~~_  ||  LIGHTMAP_ON~~
+ _  ||  _PIXELFOG_ON
+ ::local::  _  ||  _ALPHATEST_ON  ||  _ALPHABLEND_ON  ||  _ALPHAPREMULTIPLY_ON
+ ::local::  _  ||  _ENABLE_EMISSIONTEX_ON
+ ::local::  _  ||  _DIRT_ON
+ ::local::  _  ||  _TOPDETAIL_ON
+ ::local::  _  ||  _DISSOLVE_ON
+ ::local::  _  ||  _MASK_UV2_ON
+ ::local::  _  ||  _ENABLE_NORMALADD_ON
+ ::local::  _  ||  _HEIGHTGRADIENT_ON
+ ::local::  _  ||  _UT_INSTANCING_ON

#### shader_feature [4] [23 * 22 = 32]
+ _  ||  _FORWARD_PLUS_Z_BINING
+ _  ||  _ENABLE_HEIGHT_FOG_SHADING_DEBUG
+ _  ||  _ENABLE_SHADING_DEBUG
+ ::local::  _  ||  _PLANARREFLECTION
+ ::local::  _  ||  _CUSTOMAMBIRNTCOLOR
+ ~~::Pass:DepthNormals::  ::local::  _  ||  _NORMALMAP_ON~~

### Unity Shader Compile Pipeline 各流程变体数
| svc settings | Before URP | After URP | Before Mole.SVC | After Mole.SVC |
| --- | --- | --- | --- | --- |
| no keywords; mole.svc disenabled | 49152 | 49152 | 49152 | 49152 |
| _dirt_on, _dissolveon; mole.svc dis | 49152 | 49152 | 49152 | 49152 |
| _CUSTOMAMBIRNTCOLOR; mole.svc dis | 98304 | 98304 | 98304 | 98304 |
| Mat(_CUSTOMAMBIRNTCOLOR)(not in AB); mole.svc dis | 49152 | 49152 | 49152 | 49152 |
| Mat(_CUSTOMAMBIRNTCOLOR)(in AB); mole.svc dis | 98304 | 98304 | 98304 | 98304 |
| Mat(_CUSTOMAMBIRNTCOLOR)(in AB), _PLANARREFLECTION; mole.svc dis | 147456 | 147456 | 147456 | 147456 |
| Mat(_CUSTOMAMBIRNTCOLOR)(in another AB), _PLANARREF...; mole.svc dis | 147456 | 147456 | 147456 | 147456 |
| --- | --- | --- | --- | --- |
| Mat(_CUSTOMAMBIRNTCOLOR)(in AB), _PLANARREFLECTION; mole.svc e, exclude | 147456 | 147456 | 147456 | 0 |
| Mat(_CUSTOMAMBIRNTCOLOR)(in AB), _PLANARREFLECTION; mole.svc e | 147456 | 147456 | 147456 | 2 |






##  RichMan中shorttime/zhangyijie/dev_shader_zhangyijie_20240929版本的FASceneObject.shader
1. 其同时存在_MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE的可能性，所以此处 _2而非_1
2. 存在LIGHTMAP_ON 和 _的切换
3. SHADOWS_SHADOWMASK被剔除了  
因此其**纯multi_compile的变体数** = 4 * 3 * 2^16<sup> </sup>= 3 * 2^18 = 786,432。符合观测。

### 为什么在使用Mole.SVC收集变体后，收集阶段（非剔除阶段）的变体数会来到3m呢?
推测是SVC中带有shader_feature关键字的组合，造成了**786,432 * (2^n)**的翻倍。

1. 手动去掉SVC中错误的NormalMap关键字后，SVC中FASceneObject分为4份，带有shader_feature的关键字有1个;  
推测期望变体数 = 786,432 * 2 = 1,572,864
    - 实际测试结果：1,000,000 + 572,864 = 1,572,864个。相当于多了一个shader_feature关键字。对原来进行翻倍。
2. 原始情况下，
    - 实际测试结果：1,000,000 + 572864 = 1,572,864个  
Mole.SVC剔除了 999,792 + 572864 = 1,572,656个  
最后编译了         208  + 0      = 208个
    - 看样子仅存于DepthNormals pass的_NormalMap关键字没被统计进StandardLit。
3. 将两个multi_compile关键字改回成shader_feature后，变体数按照预期变成1,572,864的1/4 = 393216个。
4. 结论：
    1. preparing variants的数据并非反应某个pass的总变体数，而是反映的预先收集的变体数，后续还需要经过剔除流程，剔除之后的才会进入编译流程。
    2. Unity预先收集的变体数超过1,000,000个后，会对其进行分批处理。此时的preparing variants不一定能反映真实数据。

### 打AB包时，多个AB包依赖了同一份shader，那这个shader的变体会是什么情况？  
按照第一个AB包里的状况进行剔除及编译。
经过直接使用AssetBundleBrowser测试发现：

1. 多个AB包依赖同一份shader时，假如没有手动给任一AB包直接放入Shader，则每份AB包都会单独对该shader走一遍收集、剔除、编译流程。会导致重复编译很多次，资源冗余。
2. 多个AB包依赖了同一份shader，假如手动给某一个AB包直接放入了该Shader，则只有那份AB包会对该shader走一遍收集、剔除、编译流程。性能、效果最好。
3. 用RSBundle打包规则将Art/Shader和UT-Rendering，放在最前面(ut更前)，打进同一个包时： 变体数符合预期。对该Shader只筛选并编译一次。
4. 用RSBundle打包规则将Art/Shader和UT-Rendering打，放在最前面(ut更前)，进各自的包，不合并时：变体数少了，有些svc指定的shader_feature没有被预收集。符合预期，UT-Rendering包里没有SVC文件。对该Shader只筛选并编译一次。
5. 用RSBundle打包规则将Art/Shader和UT-Rendering打，放在最前面(ut更后)，进各自的包，不合并时：变体数少了，和上述结果一样，有些svc指定的shader_feature没有被预收集，不符合预期，Art/Shader里有SVC文件。对该Shader只筛选并编译一次。

### 为什么一次打包流程中，FASceneObject被重复执行了多次？
RichMan没有设置打UT-Rendering包，所以UT里的shader被重复执行了多次。最好是将UT-Rendering和Art/Shader打进同一个包中。且将它们的优先级放到偏前面的位置。

### 将两个关键字从shader_feature变为multi_compile后就好大了？
应该不是这个问题，问题根源依然在打包设置的不好，应该将UT-Rendering和Art/Shader打进同一个包，并放在很前面的位置。在按照此推荐设置后，改为shader_feature后，变体数变为之前的1/4。

