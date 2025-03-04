官方解释：在 Unity 中，许多着色器内部有多个“变体”，以适应不同的光照模式、光照贴图、阴影等。这些变体由着色器通道类型和一组着色器关键字标识。

[https://docs.unity3d.com/ScriptReference/ShaderVariantCollection.ShaderVariant.html](https://docs.unity3d.com/ScriptReference/ShaderVariantCollection.ShaderVariant.html)

在写shader时，往往会在shader中定义多个宏，并在shader代码中控制开启宏或关闭宏时物体的渲染过程。最终编译的时候也是根据这些不同的宏来编译生成多种组合形式的shader源码。其中每一种组合就是这个shader的一个变体(Variant)。

## **变体生成现象：编辑器提示**
以**<u>TestUnlitShader</u>**为例：

包括所有shader_featrue 3组：4*4*3 = 48个变体

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721358958824-7f65d90c-0d73-4a4d-a822-2ab08bd2b525.png)

剔除unused shader_feature 3组：4*4 = 16个变体

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721358968777-99412261-05a5-4a8b-aa61-cb60f94a4ae0.png)

同样的keywords 换一种写法：包括所有shader_featrue 3组：4*3*2=24个变体

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721358975330-f734c4c1-7d21-4280-9ecd-e893668c219f.png)

同样的keywords 换一种写法：剔除unused shader_feature 3组： 4*3=12个变体

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721358979710-c15c84a2-de5a-4bc0-87dc-3dba04ec9645.png)

## **打包实际生成变体**
在完全没有引用的情况下，打进包内的变体数量 32个

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721358985154-c6a70544-3c91-4b4c-bc5a-19f8e907ffb0.png)

输出详细信息：Tier：Tier1; Platform:GLES3x, Vulkan ; Variant

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721358991683-efbc3e08-6ff7-4044-a9ab-35ccb496cd22.png)

shader_feature C1,C2增加到SVC里之后

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359001328-b28fcd04-8739-4b35-b69d-9b5432a495a1.png)

shader_feature C1,C2增加到SVC里之后， 打进包内的变体96个

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359014566-5659f582-a223-49ac-b889-ce3be978ea0f.png)

输出详细信息：Tier：Tier1; Paltform:GLES3x, Vulkan ; Variant

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359021451-477ddb76-0452-405e-bae8-beff790a0f91.png)

## **得出****Unity****变体生成，打包规则：**
1.multi_compile 与 shader_feature

shader_feature A 其实是 shader_feature _ A的简写，所以此时是对应了两个变体：一个是定义了nokeyword 另一个是A

multi_compile A 不存在简写说法，此时只有一个A变体，如果要表示未定义变体，应该显示定义 multi_compile _ A

2.生成规则

2.1 multi_compile 定义的keyword 默认会生成所有组变体

例如上图：multi_compile _ A1 A2 A3 ; multi_compile _ B1 B2 B3

总共会生成 4*4 = 16个变体

所以应当谨慎使用multi_compile，否则会导致变体数量激增

2.2 shader_feature 定义的keyword只有引用了对应的keywrod才会生成

2.3必定生成首个shader_feature 开启所对应的变体

例如上图：multi_compile _ A1 A2 A3 ; multi_ compile _ B1 B2 B3；

shader_feature _ C1 C2

添加了C1, C2 所有的变体没有_（nokeyword）,这时候默认_(nokeyword)是一定会生成的。所以这时候总共生成 4*4*3 = 48个变体

3.打包规则

生成变体组合数 * platform 数 * tier数 = 打进包的shader变体数

关于GraphicsTier：The Graphics Tier Unity uses. You can only set a [GraphicsTier](https://docs.unity.cn/cn/2020.2/ScriptReference/Rendering.GraphicsTier.html) in the Built-in Render Pipeline. See Also: [Graphics.activeTier](https://docs.unity.cn/cn/2020.2/ScriptReference/Graphics-activeTier.html).

[https://docs.unity.cn/cn/2020.2/ScriptReference/Rendering.GraphicsTier.html](https://docs.unity.cn/cn/2020.2/ScriptReference/Rendering.GraphicsTier.html)

platform：GLES3x， Vulkan （PlayerSetting->Graphics APIS）

## **怎么收集剔除**
### **前置知识点**
1.ProjectSetting->Graphics 编辑器会追踪记录当前是用到的所有shader 和 具体的变体，可以手动保存到ShaderVariantCollection资源里。这个功能可以收集我们需要的变体。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359038811-53be4c3c-7dc1-4645-b8b4-c9b2bf755350.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359050218-f5918741-c8f4-4945-b2ee-1e3d45ea82a2.png)

2.IPreprocessShaders 接口， 在编译shader之前实现此接口以接受回调。

data里包含即将打进包的shader变体，我们可以在这里做筛选，剔除我们不需要的。

https://docs.unity.cn/cn/2020.2/ScriptReference/Build.IPreprocessShaders.OnProcessShader.html

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359058873-6188fe8c-b009-428a-b292-aa5c3ee21b8e.png)

### **收集**
1.自动收集

a.清空当前编辑器追踪缓存（ClearCurrentShaderVariantCollection）

b.收集所有游戏中会使用到的材质，在各种光照条件下渲染一次

c.加载渲染所有游戏场景scene

d.编辑器save当前所有的shader变体到svc

经过上面的流程，我们认为已经收集完整了，会把对应shader的svc保存到Assets/ShaderVariants/...相对目录



2.手动容错

由于有些keyword 是游戏逻辑动态开启关闭的，或者场景里某些节点是动画控制开启的，我们在收集的时候有可能漏掉。

所以，定义了一个手动添加的容错流程Assets/ShaderVariants/ManualMerge/...相对目录放我们手动添加的svc， 这个目录不会自动改变， 只能手动添加。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359071349-2cc21f09-0f9c-4ff7-aa4d-ec40e408bdae.png)

### **剔除**
一个猜测：上面已经收集了所需要的所有变体，把这些SVC资源和shader放在一个包里打包，Unity就会打对应SVC里的所有变体，不会丢失？

上面的猜测没错，变体一定不会丢失。但是根据上面变体生成规则第一条：multi_compile 定义的所有keyword组合都会被打进包里，不管有没有引用。

所以这部分的工作就是，根据自动收集到的SVC 和 手动merge SVC, 和要打进包的variantList 做对比，keyword没有完全匹配的剔除掉。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359078787-f8cf302f-13ab-48d7-a690-9a5328a34218.png)

## **收集前vs收集后后**
### **收集剔除前：****shader****内存****146M****，****shaderBundle****：****16.2M**
![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359098855-a3facbf6-6531-4340-b23f-93692fb9a0ce.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359105178-fa2d429d-a933-4c32-a974-92d51b29aeda.png)

### **收集剔除后：****shader****内存 ****54M shaderBundle****：****1.58M**
![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359109168-08ded639-a891-4baf-966b-909ee61f5d4e.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359113523-31090841-8452-4750-bd41-83406c1c97cc.png)

## **使用说明**
### **引用**
"com.mole.svc": "http://192.168.51.223/pub/publicmodule.git?path=/Plugin/MoleShaderVariantCollection/Assets#r/shadervariantcollection-1.0.0"

### **配置**
**Enabled**

是：可以收集变体，打包的时候会根据收集到的svc做剔除逻辑

否：不可以收集，打包的时候跳过剔除逻辑

**ShaderVariantsPath**

收集到的svc输出目录。 默认：Assets/ShaderVariants

**ManualRelativePath**

相对**ShaderVairantsPath**的手动管理svc目录。 默认：ManualMerge

这个目录不会自动创建，需要的时候手动创建

**ShaderCollectionName**

收集到的svc集合prefab。 默认：Assets/Shader/Collection.prefab

一般是shader目录，和shader一起打包，运行时加载做shader warmup

如下图：

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359123548-4ed28cab-379d-4623-9158-f9fe0d1caf21.png)

**CustomLightingEnvScenes**

用来渲染收集到的材质球的场景，可以多个场景，比如：开灯光，开阴影，不开等...

如果自定义场景个数为0，会用默认的场景（几种光，硬阴影）渲染，默认的场景会保存在Assets/ShaderVariantCollector/Temp/DefaultLightingEnv.unity

**SceneRootPath**

搜索游戏场景的根目录默认：没有

**MaterialRootPath**

搜索材质球的根目录默认：没有

**SupportLightingShaderName**

支持灯光的自定义shader名字

**ExcludeShaders**

打包做变体剔除的时候，强制这个容器里的shader不做剔除。

这里我们可以把变体数比较少，而且很容易收集露掉的shader添加进来，比如UI shader。

这类shader就算不做剔除，也不会占太多空间，和内存。

**GlobalKeywords**

在我们收集到的变体基础上，再都加上一个GlobalKeyword组合变体， 和打包变体匹配。

比如：收集到AB变体，GlobalKeyword 为C，这时候 ABC也会被打进包

创建配置

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359130560-9207d899-b2a2-4b2b-964c-eab05321188c.png)

### **启动收集**
启动收集流程：这个过程大概持续5-8分钟

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359136621-e4339c1f-ac51-4bb4-8c2b-883995e97c23.png)

Game窗口会输出当前渲染的材质球

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359141904-3e7f1991-e1e8-4649-ac92-157d1d061eb0.png)

收集完之后，收集到的所有SVC资源引用会添加到 Shader/ShaderCollection.prefab上，这个资源会一起打进Bundle， 方便后面做**warmup**逻辑。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359149333-8e4be1ca-bf73-49d6-a0dd-c7081f12bd14.png)

## **实现代码说明**
### **收集：****ShaderVariantCollectionExporter.cs**
1._CollectDynamicLightingMaterials:可修改搜索的路径

2.renderAllMaterialForLighting

3.CollectAllMaterialAssetsForGame:可修改搜索的路径

4.renderAllGameMaterial

5.GetAllScenes:可修改搜索的路径

6.renderAllGameScene

7.SaveShaderVariants

### **剔除：****ShaderPreprocessor.cs**
1.OnProcessShader

2.isMatched

## **特殊丢失变体说明**
1：根据上面提到shader feature的打包逻辑，必须要有引用才会有机会传给IPreprocessShaders接口做剔除逻辑。

比如TMP字体shader，outline， underlay关键字都是定义的shader feature，打包的shader 是在单独一个Bundle里，没有引用任何字体，prefab，material，所以如果把TMP shader单独打进Bundle里，就需要手动创建一个ShaderVariantCollection asset，把需要的关键字组合添加进去，（或者全加进去也没关系，本身资源不大） 然后把资源跟shader一起打包。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359159414-25e863cf-8649-4a27-8844-28a72687d4f0.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1721359165770-937d44a7-33aa-4126-b49c-5eae9888ad2c.png)

