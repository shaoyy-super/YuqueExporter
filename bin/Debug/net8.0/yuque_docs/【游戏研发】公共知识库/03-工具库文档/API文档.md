# 一.捏脸
捏脸的运行时接口全部集中在CustomFaceController，以下为该类型的API列表。注意，对于脸部变形器和材质的设置，要么完全使用CustomFaceController提供的接口，要么完全自己实现，不能混用，因为CustomFaceController内部有一些皮肤颜色同步，变形器同步，混合贴图材质等特殊处理，并非只简单的设置材质参数，设置变形器，混用可能会导致CustomFaceController内部保存的一些状态失效。

## 初始化
:::tips
+ **Method**：public void Init(GameObject headGo, Texture2D mainTex)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| headGo | GameObject | 角色Prefab |
| mainTex | Texture2D | 面部默认纹理 |


## 塑形
### 设置塑形权重
:::tips
+ **Method**：public void SetBlendShapeWeight(string name, float value)

:::

| 参数 | 类型 |  | 注释 |
| --- | --- | --- | --- |
| name | string | Blendshape名称 | 颜色 |
| value | float | 权重值 |  |


### 获取塑形权重
:::tips
+ **Method**：public float GetBlendshapeWeight(string name)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| name | string | Blendshape名称 |


### 设置附加蒙皮数组
:::tips
+ **Method**：public void SetExSkinnedMeshRenderList(List<SkinnedMeshRenderer> skinnedMeshRenderers)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| skinnedMeshRenderers | List<SkinnedMeshRenderer> | 可能需要被同步调整的带变形器的蒙皮数组（如头发） |




## 妆容：
### 设置妆容数据
:::tips
+ **Method**：public void SetMakeup(int makeupType, Texture2D texture, Color[] colors, Vector2 pos, float angel, Vector2 scale)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| makeupType | int | 妆容部位（0=眼妆, 1=眉毛, 2=腮红, 3=唇彩, 4=脸妆） |
| texture | Texture2D | 设置纹理 |
| colors | Color[] | 纹理颜色数组，目前只有眼妆和腮红使用三个颜色，其他的只使用数组的第一个颜色 |
| pos | Vector2 | 纹理偏移位置 |
| angel | float | 纹理角度 |
| scale | Vector2 | 纹理纵横缩放 |




## 睫毛
### 设置睫毛数值属性
:::tips
+ **Method**：public void SetEyelashValueProperty(string property, float value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 睫毛Shader属性名称 |
| value | float | 数值 |


### 设置睫毛向量属性
:::tips
+ **Method**：public void SetEyelashV4Property(string property, Vector4 value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 睫毛Shader属性名称 |
| value | Vector4 | 向量 |


### 设置睫毛颜色属性
:::tips
+ **Method**：public void SetEyelashColorProperty(string property, Color value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 睫毛Shader属性名称 |
| value | Color | 颜色 |


### 设置睫毛纹理属性
:::tips
+ **Method**：public void SetEyelashTextureProperty(string property, Texture2D value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 睫毛Shader属性名称 |
| value | Texture2D | 纹理 |




## 眼睛
### 设置眼睛数值属性
:::tips
+ **Method**：public void SetEyeValueProperty(string property, float value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 眼睛Shader属性名称 |
| value | float | 数值 |


### 设置眼睛向量属性
:::tips
+ **Method**：public void SetEyeV4Property(string property, Vector4 value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 眼睛Shader属性名称 |
| value | Vector4 | 向量 |


### 设置眼睛颜色属性
:::tips
+ **Method**：public void SetEyeColorProperty(string property, Color value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 眼睛Shader属性名称 |
| value | Color | 颜色 |


### 设置眼睛纹理属性
:::tips
+ **Method**：public void SetEyeTextureProperty(string property, Texture2D value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 眼睛Shader属性名称 |
| value | Texture2D | 纹理 |




## 皮肤
### 设置皮肤数值属性
:::tips
+ **Method**：public void SetSkinValueProperty(string property, float value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 皮肤Shader属性名称 |
| value | float | 数值 |


### 设置皮肤向量属性
:::tips
+ **Method**：public void SetSkinV4Property(string property, Vector4 value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 皮肤Shader属性名称 |
| value | Vector4 | 向量 |


### 设置皮肤颜色属性
（非肤色）

:::tips
+ **Method**：public void SetSkinColorProperty(string property, Color value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 皮肤Shader属性名称 |
| value | Color | 颜色 |


### 设置皮肤纹理属性
:::tips
+ **Method**：public void SetSkinTextureProperty(string property, Texture2D value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| property | string | 皮肤Shader属性名称 |
| value | Texture2D | 纹理 |




### 刷新角色皮肤材质球
因为我们的衣服预制体里可能会带有部分裸模皮肤，所以在换装后，需要通知捏脸模块刷新皮肤颜色。

:::tips
+ **Method**：public void UpdateSkinMaterialPropertyBlocks()

:::





下面有两个特殊的皮肤设置接口，需调用特殊的API，不可使用上面的通用接口，这是由内部实现决定的。

### 设置皮肤颜色
:::tips
+ **Method**：public void SetSkinColor(Color color)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| color | Color | 皮肤颜色 |


### 设置纹理合成材质球的面部底图
:::tips
+ **Method**：public void SetFaceMixTexture(Texture2D texture)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| texture | Texture2D | 合成材质球的面部底图 |


## 塌陷捏脸结果
:::tips
+ **Method**：public void GenerateResult()

:::

:::info
💡 调用改接口前确保没有动画播放，并且头部不能有位置偏移

:::



# 二.捏人
捏脸的运行时接口全部集中在CustomFaceController，以下为该类型的API列表。

## 系统API
静态类CustomizerLocator.cs，内部提供一组接口，用于使用者向捏人系统内部注入依赖的对象或参数。

### 初始化
:::tips
+ **Method**：public static void InitBodyModule(ICustomizerResLoader customizerResLoader)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizerResLoader | ICustomizerResLoader | 资源加载器，提供配置读取功能。 |


## 捏人控制器
CustomBodyController.cs提供了所有对某个角色捏人使用的接口。当同时使用捏人和换装时，建议先初始化捏人控制器，因为裸模状态具有更干净的骨骼结构，避免了挂点衣服同名骨骼的干扰。

### 初始化
:::tips
+ **Method**：public void Init(ICustomizerInfo customizerInfo, BodyData bodyData, GameObject targetObj, GameObject templateObj, int modelId,  bool supportRecalculateNormals = false)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizerInfo | ICustomizerInfo | 提供访问所有骨骼部位滑条值的功能。 |
| customizeData | CustomizeData | 捏人骨骼及范围数据，决定了每个滑条将如何影响骨骼。 |
| targetObj | GameObject | 捏人目标节点（"Model"节点的父节点） |
| templateObj | GameObject | 模板物体，可以是targetObj对应的预制体对象（因其保存了TPose时的骨骼数据）。也可以是在场景中保持TPose的，和targetObj具有相同骨骼的对象实例。 |
| modelId | int | 裸模id，对应男模/女模或其它 |
| supportRecalculateNormals | bool | 是否需要支持计算法线 |




### 标记某个骨骼部位的值发生变化
:::tips
+ **Method**：public void SetBoneDirty(string partKey, bool isBuild = false)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| partKey | string | 部位的键，对应一个滑条 |
| isBuild | bool | 是否立即调整骨骼。(调整骨骼是一个相对比较消耗CPU时间的行为，建议滑条调整时，此参数为false) |




### 标记所有骨骼部位的值发生变化
:::tips
+ **Method**：public void SetAllBoneDirty(bool isBuild = false)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| isBuild | bool | 是否立即调整骨骼 |




### 法线计算器
法线计算器是一种用于修正骨骼发生非均匀缩放时法线表示错误的工具，项目根据自己的捏人骨骼调整幅度大小，及效果接受程度来选择是否使用。



如果在初始化时传入了支持计算法线，此处可以获取到法线计算器对象

:::tips
+ **Method**：public MeshNormalsCalculator GetMeshNormalsCalculator()

:::

法线计算器的主要接口为RecalculateNormals()，内部将同步完成法线的计算，一般骨骼发生大幅度改变时，换装结束后，需要重新计算法线。



如果使用换装模块，在CustomClothesController.Init中传入法线计算器，换装模块将在内部自动的设置需要计算法线的SkinMesh并在每次换装时更新。



如果不使用换装模块，同时需要计算法线功能。

需要在RecalculateNormals之前，调用一次SetTargetSkinMesh设置需要计算法线的SkinMesh组件。

调用SetUseMesh传入mesh相关数据，并在mesh发生变化时(骨骼，材质，submesh，顶点信息等)再次调用SetUseMesh进行更新。



### 检查并调整骨骼
建议在捏人场合时，在Update中间隔固定帧调用此方法，将前几帧标记为变化的骨骼部位，一起进行调整。

:::tips
+ **Method**：public void BuildBoneIfDirty()

:::



### 设置鞋跟类型
脚部大小的调整，依赖鞋跟类型数据。同时，设置鞋跟类型，将会在捏人系统内部调整胯骨骨骼位置，来使得模型鞋底贴地面。

:::tips
+ **Method**：public void SetShoesSoleType(ShoesSoleType shoesSoleType, bool isBuild = false)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| shoesSoleType | ShoesSoleType | 新的鞋跟类型 |
| isBuild | bool | 是否立即调整骨骼 |




### 销毁
当模型物体被销毁/回收时，需主动调用。内部会将进行清除操作，根据参数选择是否重置骨骼状态为TPose，并移除身体修改器组件。一旦调用销毁，CustomBodyController的其他接口将不能再使用。（不要在object的OnDestory里调用）

:::tips
+ **Method**：public void Destroy(bool needResetBone)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| needResetBone | bool | 是否需要重置骨骼和avatar为初始状态 |




# 三.换装
## 系统API
静态类CustomizerLocator.cs，内部提供一组接口，用于使用者向换装系统内部注入依赖的对象或参数，同时控制缓存释放。

### 初始化
:::tips
+ **Method**：public static void InitClothesModule(ICustomizerGlobalResLoader customizerResLoader)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizerResLoader | ICustomizerGlobalResLoader | 提供配置读取功能的实现。 |




## 换装控制器
CustomClothesController.cs提供了所有对某个角色换装使用的接口。需要注意的是，由于冲突的存在，调用换装后，可能身上有一些部位会被顶下去，所以在换装的接口里newClothesDataDic用于内部向外返回此次换装后，所有部位的最新的itemId映射。



### 初始化
:::tips
+ **Method**：public void Init(ICustomizerActorResLoader actorResLoader, ICustomizerInfo customizerInfo, GameObject obj, int modelId, MeshNormalsCalculator meshNormalsCalculator = null)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| actorResLoader | ICustomizerActorResLoader | 用于衣服预制体加载回收释放等 |
| customizerInfo | ICustomizerInfo | 提供访问对应部位上已穿戴的itemId功能的实现。 |
| obj | GameObject | 角色模型根节点，"Model"节点的父节点。 |
| modelId | int | 裸模id，对应男模/女模 |
| meshNormalsCalculator | MeshNormalsCalculator | 法线计算器 |




### 是否在替换中
衣服预制体加载可能是一个异步过程，在上传换装未结束时，需拦截下一次换装操作，及所有对换装控制器的调用。

:::tips
+ **Method**：public void IsReplacing()

:::



### 替换某个部位对应的item
普通部位/散件组合套装/真套装都使用此接口。当头发/身体/脚未被覆盖时，会使用默认的。如在穿连衣裙时，替换了下装，那么内部会使用默认的上装。

:::tips
+ **Method**：public void  Replace(int clothesType, int itemId, Dictionary<int, int> newClothesDataDic, Action<ReplaceClothesResult> onFinish)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| clothesType | int | 要替换的部位类型 |
| itemId | int | 新的itemId，小于等于0为脱下 |
| newClothesDataDic | Dictionary<ClothesType, int> | 换装数据结果，含有所有部位的新的itemId |
| onFinish | Action<ReplaceClothesResult> | 此次换装结束回调 |




### 获取item和身上哪些已穿戴的部位冲突
即穿上itemid后，有哪些身上已穿戴的部位会因为冲突而被卸下

:::tips
+ **Method**：public void  GetItemConflictSet(int itemId, HashSet<int> result)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| itemId | int | 要查询的itemId |
| result | HashSet<int> | 返回结果，冲突的主类型id的Set |




### 获取某个部位上的资源名
有变体的话，染色需要获取部位资源名

:::tips
+ **Method**：public string GetClothesPrafabName(int part)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| part | int | 要查询的部位 |




### 强制设置穿搭方案
脱掉所有部位的衣服，强制使用传入穿搭方案生成模型，不检查传入的部位之间的冲突。

:::tips
+ **Method**：public void ForceBatchReplace(Dictionary<int, int> plan, Dictionary<int, int> newClothesDataDic, Action<ReplaceClothesResult> onFinish)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| plan | Dictionary<int, int> | 部位-itemid映射表 |
| newClothesDataDic | Dictionary<int, int> | 换装数据结果，含有所有部位的新的itemId |
| onFinish | Action<ReplaceClothesResult> | 此次换装结束回调 |




### 获取含有blendShape的SkinMesh列表
得到当前所有已穿戴衣服中含有blendShape的SkinMesh列表，用以换装后，向捏脸系统同步最新的含有blendshape的skinmesh列表。

:::tips
+ **Method**：public void GetHasBlendShapeClothesSkinMeshs(List<SkinnedMeshRenderer> result)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| result | List<SkinnedMeshRenderer> | 返回结果 |




### 染色-设置颜色
对身上某个已穿部位进行染色。

:::tips
+ **Method**：public void SetClothesMatProperty(int part, string skinMeshNodeName, string key, Color value)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| part | int | 部位 |
| skinMeshNodeName | string | 要染色的SkinMesh节点名 |
| key | string | 属性名 |
| value | Color | 属性值 |


### 染色-设置参数
对身上某个已穿部位进行染色参数设置。

:::tips
+ **Method**：public void SetClothesMatProperty(int part, string skinMeshNodeName, string key, float value)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| part | int | 部位 |
| skinMeshNodeName | string | 要染色的SkinMesh节点名 |
| key | string | 属性名 |
| value | float | 属性值 |




### 销毁
当模型物体被销毁/回收时，需主动调用。内部会将所有身上部位的衣服回收，并移除服装修改器组件。一旦调用销毁，CustomClothesController的其他接口将不能再使用。（不要在object的OnDestory里调用）

:::tips
+ **Method**：public void Destroy()

:::



## 换装结果回调对象
ReplaceClothesResult.cs，在每次换装结束的回调中，会回传入一个该对象实例，注意该对象为内部复用对象，禁止持有。



| 属性名 | 属性类型 | 属性描述 |
| --- | --- | --- |
| isSucceed | int | 是否成功换装。false代表调用参数不合法，详细错误见log。 |
| isShoesSoleTypeChange | bool | 鞋跟类型是否发生了变化 |
| newShoesSoleType | ShoesSoleType | 新的鞋跟类型。当isShoesSoleTypeChange为True时有值。 |
| conflictRemoveSet | HashSet<ClothesType> | 此次换装，有哪些原本已穿戴的部位因为冲突被移除了。 |




# 四.捏脸塑形版本转换
## 简述
脸部形状基于BlendShapes，当面部模型资源发生变化时，为保留形状，需进行版本转换。如玩家在V1.0进行捏脸塑形，当软件更新为V1.1后，需要尽可能的在V1.1的资源下，恢复在V1.0时的形状。

类似的，下文把在A版本下的捏脸滑条值转化为B版本的滑条值，称之为src版本转化为dest版本。注意，只有当两个版本的face的mesh顶点数量及排布一样时，才可以进行互相转化。



在此用一个BSVersionData对象来表示一个版本的模型数据，使用BSVersionConversionUtil提供静态方法来进行计算转换。



## BSVersionData.cs
### 初始化
初始化一个版本数据，作为后续计算的输入。初始化接口耗时相对较长，占用托管内存较多，不建议进行频繁的初始化和清空。

:::tips
+ **Method**：public void Init(TextAsset data, BSVersionDataTag tag, HashSet<string> whiteBSNameSet = null, Dictionary<string, List<string>> equivalentBSGroupMap = null)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| data | TextAsset | 版本BS描述文件 |
| tag | BSVersionDataTag | BSVersionDataTag.OnlySrc：只会用作转化源。<br/>BSVersionDataTag.OnlyDest：只会用作转化目标。<br/>BSVersionDataTag.All：都有可能。<br/>建议按需选择，影响对象占用堆内存大小。 |
| whiteBSNameSet | HashSet<string> | 可选参数。表示开放给玩家调整的BS名字的集合，当作为src或者dest时，仅考虑在该集合内的BS 。<br/>如果为null，认为data内的所有BS都在白名单内。 |
| equivalentBSGroupMap | Dictionary<string, List<string>> | 可选参数。等效值BS组。key为主键，对应list为分量数组。如key=“I_Eye_S”，list = {"I_Eye_SFB", "I_Eye_SLR", "I_Eye_SUD"}，即表示调整I_Eye_S等效于调整三个分量，幅度相同。<br/>当转换后显示异常时，请尝试查找异常的BS名字并找出对应的组，在初始化时传入。 |




### 清空
释放内部持有的矩阵等大的托管内存对象，释放后不可再使用。

:::tips
+ **Method**：public void Clear()

:::

## BSVersionConversionUtil.cs
### 转换
由srcBlendShapeValueMap，及srcDta和destData，转化为destBlendShapeValueMap。

:::tips
+ **Method**：public static Dictionary<string, float> Calculate(Dictionary<string, float> srcBlendShapeValueMap, BSVersionData srcData, BSVersionData destData)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| srcBlendShapeValueMap | Dictionary<string, float> | 转化源blendShape的映射表 |
| srcData | BSVersionData | 转换源数据 |
| destData | BSVersionData | 转换目标数据 |




## 使用示例代码
```plain
public class BSVersionConversionTest : MonoBehaviour
{
    [SerializeField]
    TextAsset dataA;

    [SerializeField]
    SkinnedMeshRenderer skinMeshA;

    [SerializeField]
    Button randomABtn;

    [SerializeField]
    Button syncABtn;

    [SerializeField]
    TextAsset dataB;

    [SerializeField]
    SkinnedMeshRenderer skinMeshB;

    [SerializeField]
    Button randomBBtn;

    [SerializeField]
    Button syncBBtn;

    BSVersionData bSVersionFloatDataA;
    BSVersionData bSVersionFloatDataB;
    // Start is called before the first frame update
    void Start()
    {
        bSVersionFloatDataA = new BSVersionData();
        bSVersionFloatDataA.Init(dataA, BSVersionDataTag.All, null, null);
        bSVersionFloatDataB = new BSVersionData();
        Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
        keyValuePairs.Add("I_Eye_S", new List<string>());
        keyValuePairs["I_Eye_S"].Add("I_Eye_SFB");
        keyValuePairs["I_Eye_S"].Add("I_Eye_SLR");
        keyValuePairs["I_Eye_S"].Add("I_Eye_SUD");
        bSVersionFloatDataB.Init(dataB, BSVersionDataTag.All, null, keyValuePairs);

        randomABtn.onClick.AddListener(()=>
        {
            RandomSkinMeshBlendShape(skinMeshA);
        });

        randomBBtn.onClick.AddListener(()=>
        {
            RandomSkinMeshBlendShape(skinMeshB);
        });

        syncABtn.onClick.AddListener(() =>
        {
            var result = BSVersionConversionUtil.Calculate(GetSkinMeshBlendShape(skinMeshA), bSVersionFloatDataA, bSVersionFloatDataB);
            SetSkinMeshBlendShape(skinMeshB, result);
        });

        syncBBtn.onClick.AddListener(() =>
        {
            var result = BSVersionConversionUtil.Calculate(GetSkinMeshBlendShape(skinMeshB), bSVersionFloatDataB, bSVersionFloatDataA);
            SetSkinMeshBlendShape(skinMeshA, result);
        });
    }

    void RandomSkinMeshBlendShape(SkinnedMeshRenderer skinnedMeshRenderer)
    { 
        Dictionary<string, float> cacheValues = new Dictionary<string, float>();
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
            //if (blendShapeName.StartsWith("I_") && BlendShapeMasks.Contains(blendShapeName))
            if (blendShapeName.StartsWith("I_"))
            {
                if (!cacheValues.ContainsKey(blendShapeName))
                    cacheValues.Add(blendShapeName, Random.Range(-60, 60));
                skinnedMeshRenderer.SetBlendShapeWeight(i, cacheValues[blendShapeName]);
            }
            else
            { 
                skinnedMeshRenderer.SetBlendShapeWeight(i, 0);
            }
        }
    }

    Dictionary<string, float> GetSkinMeshBlendShape(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        Dictionary<string, float> result = new Dictionary<string, float>();
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
            if (blendShapeName.StartsWith("I_") && !result.ContainsKey(blendShapeName))
            { 
                result.Add(blendShapeName, skinnedMeshRenderer.GetBlendShapeWeight(i));
            }
        }
        return result;
    }

    void SetSkinMeshBlendShape(SkinnedMeshRenderer skinnedMeshRenderer, Dictionary<string, float> dic)
    {
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
            if (blendShapeName.StartsWith("I_"))
            {
                if (dic.ContainsKey(blendShapeName))
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(i, dic[blendShapeName]);
                }
                else
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(i, 0);
                }
            }
        }
    }
}
```



Step1：随机设置1.0的BS。左侧相对右侧标准脸，已有明显形变。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727092911201-cbe9270e-1630-4a1d-b4f6-583829f573f4.png)



Step2：转换为1.1的数据，并同步到右侧1.1模型上。如下图，1.1模型已经被转换

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727092933008-56394aa7-c4e4-4464-a564-3fa5f37f95ba.png)

同样的，随机一个1.1的脸

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727143405823-1e916318-dac0-486f-8364-9c47288713fe.png)



点击转换，可以看到左侧1.0脸往右侧1.1脸靠近，但没能做到完全一样，这是因为1.0的BS不支持达到像1.1这样夸张的效果。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727143448403-d654ce1c-c172-4f3b-85ba-2c53b0da0934.png)  


# 五.自定义
## 简述
对于 常量数字约定/预制体命名约定/一些可插拔的模块/我们认为业务层可能需要做一些事情的时机，我们提供了一些允许业务层自行实现的节点和时机，以避免对业务方造成过多的约束。

## 衣服预制体转化监听
业务侧实现IClothesPrefabConvertReceiver.cs后挂载，即可获得剔除前后的执行时机回调。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726142227060-6d338201-b335-4433-8dc7-c60948344663.png)

****

下图是一个使用示例：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726142334155-6b61be9f-f5ce-494c-93c2-d405b5c840d9.png)

## 布料系统自实现扩展接口
![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726138721336-6076bb23-261a-4d49-84aa-c32b016f8c33.png)

同时我们提供一个碰撞框引用的接口，实现该接口的类需要同时继承MonoBehaviour并挂在布料组件所在的GameObject上

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726138797168-e9a6f424-a18e-46bf-be1e-b159e7d39538.png)



## 配置表读取
业务层的配置读取接口我们在ICustomizerGlobalResLoader中提供了两种方式

GetConfig根据表名获取ConfigTable对象，ConfigTable对象由我们自带的json库所提供，需结合导表工具使用。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726142837650-a2b3df45-e262-4ad8-81a7-334151fd842c.png)

如果业务层已有配置解决方案，不想使用我们自带的json库，可以实现ReadConfig方法，通过表名获取反序列化后字典。目前有5张表，对应下图中5个实现了ICustomizeConfigBase的类。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726143035334-d02a3342-314b-4fae-906a-aaa4be1c4bcb.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726143089549-e0c633f5-31e4-4a08-a26d-af099c8c8070.png)

请至少保证业务侧实现了该接口定义的两个方法中的一个，另一个可以返回为null。

## CustomDefine
包内使用了很多Define字符串和数字，甚至数组。

同时，包内代码大量使用了Shader属性名，有些不由业务侧通过方法调用传入。

为了在极端情况下，业务侧Shader属性命名发生更改/项目更改命名规范，包内代码能够正常运行，我们使用ScriptableObject资产作为捏人捏脸换装功能的全局配置项。包内自带一个的默认设置文件。



可以通过上侧工具栏，Customize->创建CustomDefine来覆盖包内自带的默认设置。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726305093608-11530e10-7154-4605-b6b7-5be4ac7e4dca.png)

![Define](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726305421342-9b9e7c4c-7e32-444a-9590-0b749db16cb8.png)

在非运行时，我们查找项目内CustomDefine类型的资产文件，请保持唯一，如果没有，将使用包内自带的默认设置。

在运行时，如果需要覆盖，应在任何包内代码执行前，赋值CustomizerLocator.customDefine



以下为默认的设置值及其注释

```lua
[SerializeField]
public int MaxClothesLayer = 3;//最大的衣服层级数量，用于冲突检测
[SerializeField]
public ShoesSoleType DefaultShoneType = ShoesSoleType.Middle;//默认的鞋跟类型
[SerializeField]
public string ClothesAniLayerNamePrefix = "Clothes";//手持动画分层层级前缀
[SerializeField]
public string ModelFootMeshNamePrefix = "Foot";//SkinMesh节点命名前缀
[SerializeField]
public string ModelNodeName = "Model";//Prefab模型节点命名
[SerializeField]
public string ModelRootBoneNodeName = "Model/Root";//Prefab模型骨骼根节点命名
[SerializeField]
public string ModelBodyNodeName = "Model/Body";//Prefab模型身体SkinMesh命名
[SerializeField]
public string ModelEyeNodeName = "Model/Basis_eye";//Prefab模型眼睛SkinMesh命名
[SerializeField]
public string ModelFaceNodeName = "Model/Face";//Prefab模型脸SkinMesh命名
[SerializeField]
public string FaceBlendShderName = "Unlit/FaceBlend";//面部混合Shader名
[SerializeField]
public string ModelScalpSkinMeshNameKey = "Scalp";//命名含有该字符串的SkinMesh组件被认为是头皮，头发染色时会同步对头皮染色
[SerializeField]
public string IgnoreDyeMatNameKey = "_FC";//不支持染色的材质的命名需包含该字符串
[SerializeField]
public string ModelMeshRendererScalpPath = "Model/Scalp";//该节点的MeshRenderer组件被认为时头皮，头发染色时会同步对头皮染色（优先于ModelScalpSkinMeshNameKey）
[SerializeField]
public string ClothesCustomBoneRootName = "BoneRoot";//衣服骨骼剔除后，自己独有骨骼的父节点命名
[SerializeField]
public string ClothesClothCompRootName = "ClothRoot";//需要转化时同步的布料组件的根节点命名
[SerializeField]
public string ModelFootLName = "Foot_L";//平脚SkinMesh节点命名
[SerializeField]
public string ModelFootMName = "Foot_M";//中脚SkinMesh节点命名
[SerializeField]
public string ModelFootHName = "Foot_H";//高跟脚SkinMesh节点命名
[SerializeField]
public string ClothesTempModelRoot = "ArtNoBuild/TempClothesModel";//衣服TEMP资源存放的根路径（相对于Assets）
[SerializeField]
public string ClothesRealModelRoot = "Art";//衣服TEMP资源存放的根路径（相对于Assets）
[SerializeField]
public string DefaultPluginFolder = "CustomizerDemo";//默认的配置数据存放路径
[SerializeField]
public int MixSuitPartCfgId = 98;//混合套装id
[SerializeField]
public int SuitPartCfgId = 99;//套装id
[SerializeField]
public int SocksPartCfgId = 6;//袜子id
[SerializeField]
public int ShoesPartCfgId = 7;//鞋子id
[SerializeField]
public int HairPartCfgId = 2;//头发id
[SerializeField]
public int ClothesMaxDyeChannelNum = 6;//衣服最大的染色区域数量
[SerializeField]
public int FaceMatIndex = 0;//脸上，面部材质球索引
[SerializeField]
public int EyelashMatIndex = 1;//脸上，睫毛材质球索引
[SerializeField]
public string[] ModelPartSkinMeshNodeNameArray = new string[]
{
    "Arm_L",
    "Arm_R",
    "Basis_eye",
    "Body",
    "Elbow_L",
    "Elbow_R",
    "Face",
    "Foot_H",
    "Foot_L",
    "Hand_L",
    "Hand_R",
    "Head",
    "Hip",
    "Knee_L",
    "Knee_R",
    "Leg_L",
    "Leg_R",
    "Thigh_L",
    "Thigh_R",
    "Waist",
};//裸模SkinMesh节点命名列表
```

```lua
//皮肤
[SerializeField]
public string Skin_BaseMap = "_BaseMap";//面部主纹理(texture)
[SerializeField]
public string Skin_SSSMap = "_SSSMap";//皮肤SSS纹理(texture)
[SerializeField]
public string Skin_SSSIntensity = "_SSSIntensity";//皮肤SSS纹理浓度(float)
[SerializeField]
public string Skin_Curvature = "_SkinCurvature";//皮肤纹理曲率(float)
[SerializeField]
public string Skin_Thickness = "_SkinThickness";//皮肤厚度(float)
[SerializeField]
public string Skin_MaskMap = "_MaskMap";//皮肤遮罩纹理(texture)
[SerializeField]
public string Skin_Smoothness = "_Smoothness";//皮肤粗糙度(float)
[SerializeField]
public string Skin_Metallic = "_Metallic";//皮肤金属度(float)
[SerializeField]
public string Skin_OcclusionStrength = "_OcclusionStrength";//皮肤咬合强度(float)
[SerializeField]
public string Skin_BumpMap = "_BumpMap";//皮肤法线纹理(texture)
[SerializeField]
public string Skin_BumpScale = "_BumpScale";//皮肤法线缩放
[SerializeField]
public string Skin_BaseColor = "_BaseColor";//皮肤颜色
//脸部合成材质
[SerializeField]
public string FaceMix_MainTex = "_MainTex";//纹理合成的面部底图(texture)
[SerializeField]
public string FaceMix_Color = "_Color";//皮肤颜色(color)
//脸部合成材质-眼妆
[SerializeField]
public string FaceMix_EyeMakeupMap = "_EyeMakeupMap";//眼妆贴图(texture)
[SerializeField]
public string FaceMix_EyeMakeupMapAngle = "_EyeMakeupMapAngle";//眼妆角度(float)
[SerializeField]
public string FaceMix_EyeMakeup1Color = "_EyeMakeup1Color";//眼妆颜色1(color)
[SerializeField]
public string FaceMix_EyeMakeup1Alpha = "_EyeMakeup1Alpha";//眼妆透明度1(float)
[SerializeField]
public string FaceMix_EyeMakeup2Color = "_EyeMakeup2Color";//眼妆颜色2(color)
[SerializeField]
public string FaceMix_EyeMakeup2Alpha = "_EyeMakeup2Alpha";//眼妆透明度2(float)
[SerializeField]
public string FaceMix_EyeMakeup3Color = "_EyeMakeup3Color";//眼妆颜色3(color)
[SerializeField]
public string FaceMix_EyeMakeup3Alpha = "_EyeMakeup3Alpha";//眼妆透明度3(float)
//脸部合成材质-眉毛
[SerializeField]
public string FaceMix_EyebrowMap = "_EyebrowMap";//眉毛贴图(texture)
[SerializeField]
public string FaceMix_EyebrowMapAngle = "_EyebrowMapAngle";//眉毛角度(float)
[SerializeField]
public string FaceMix_EyebrowMapColor = "_EyebrowMapColor";//眉毛颜色(color)
[SerializeField]
public string FaceMix_EyebrowMapAlpha = "_EyebrowMapAlpha";//眉毛透明度(float)
//脸部合成材质-腮红
[SerializeField]
public string FaceMix_BlushMap = "_BlushMap";//腮红贴图(texture)
[SerializeField]
public string FaceMix_BlushMapAngle = "_BlushMapAngle";//腮红角度(float)
[SerializeField]
public string FaceMix_BlushMap1Color = "_BlushMap1Color";//腮红颜色1(color)
[SerializeField]
public string FaceMix_BlushMap2Color = "_BlushMap2Color";//腮红颜色2(color)
[SerializeField]
public string FaceMix_BlushMap3Color = "_BlushMap3Color";//腮红颜色3(color)
[SerializeField]
public string FaceMix_BlushMapAlpha = "_BlushMapAlpha";//腮红透明度(float)
//脸部合成材质-唇彩
[SerializeField]
public string FaceMix_LipsMap = "_LipsMap";//唇彩贴图(texture)
[SerializeField]
public string FaceMix_LipsMapAngle = "_LipsMapAngle";//唇彩角度(float)
[SerializeField]
public string FaceMix_LipsMapColor = "_LipsMapColor";//唇彩颜色(color)
[SerializeField]
public string FaceMix_LipsMapAlpha = "_LipsMapAlpha";//唇彩透明度(float)
//脸部合成材质-面纹
[SerializeField]
public string FaceMix_TatooMap = "_TatooMap";//面纹贴图(texture)
[SerializeField]
public string FaceMix_TatooMapAngle = "_TatooMapAngle";//面纹角度(float)
[SerializeField]
public string FaceMix_TatooMapColor = "_TatooMapColor";//面纹颜色(color)
[SerializeField]
public string FaceMix_TatooMapAlpha = "_TatooMapAlpha";//面纹透明度(float)
//眼球
[SerializeField]
public string Eye_MainTex = "_MainTex";//眼睛纹理(texture)
[SerializeField]
public string Eye_ScleraTex = "_ScleraTex";//巩膜贴图(texture)
[SerializeField]
public string Eye_EyeMask = "_EyeMask";//眼睛遮罩贴图(texture)
[SerializeField]
public string Eye_Normal = "_Normal";//眼睛法线贴图(texture)
[SerializeField]
public string Eye_Normal2 = "_Normal2";//虹膜平面法线图(texture)
[SerializeField]
public string Eye_EyeSize = "_EyeSize";//眼睛大小(float)
[SerializeField]
public string Eye_IrisContrast = "_IrisContrast";//虹膜明暗(float)
[SerializeField]
public string Eye_IrisSize = "_IrisSize";//虹膜大小(float)
[SerializeField]
public string Eye_NormalScale = "_NormalScale";//法线强度(float)
[SerializeField]
public string Eye_EyeBallColor = "_EyeBallColor";//眼睛颜色(color)
[SerializeField]
public string Eye_EyeBallMetalness = "_EyeBallMetalness";//眼球金属度(float)
[SerializeField]
public string Eye_EyeBallGloss = "_EyeBallGloss";//眼球粗糙度(float)
[SerializeField]
public string Eye_IrisBaseColor = "_IrisBaseColor";//虹膜颜色(color)
[SerializeField]
public string Eye_IrisPupilMetalness = "_IrisPupilMetalness";//虹膜瞳孔金属度(float)
[SerializeField]
public string Eye_LensGloss = "_LensGloss";//晶状体粗糙度(float)
[SerializeField]
public string Eye_IrisMargin = "_IrisMargin";//虹膜边缘虚实(float)
[SerializeField]
public string Eye_IrisMarginColor = "_IrisMarginColor";//虹膜边缘颜色(color)
[SerializeField]
public string Eye_IrisBasePosition = "_IrisBasePosition";//虹膜位置对齐(V4)
[SerializeField]
public string Eye_IrisParallaxPower = "_IrisParallaxPower";//虹膜视差强度(float)
[SerializeField]
public string Eye_FinalIllumination = "_Final_illumination";//最终颜色强度(color)
//睫毛
[SerializeField]
public string Eyelash_MainTex = "_MainTex";//睫毛纹理(texture)
[SerializeField]
public string Eyelash_Cutoff = "_Cutoff";//透明裁剪(float)
//头发
[SerializeField]
public string Hair_ColorGradualChange = "_HairColorGradualChange";//头发是否使用渐变(flo
[SerializeField]
public string Hair_MainColor = "_MainColor";//头发单色颜色(color)
[SerializeField]
public string Hair_RootColor = "_HairRootColor";//发根颜色(color)
[SerializeField]
public string Hair_MiddleColor = "_HairMiddleColor";//发中颜色(color)
[SerializeField]
public string Hair_TipColor = "_HairTipColor";//发梢颜色(color)
[SerializeField]
public string Hair_MiddleRadius = "_HairMiddleRadius";//发中范围(float)
[SerializeField]
public string Hair_TipRadius = "_HairTipRadius";//发梢范围(float)
[SerializeField]
public string Hair_GradualTranstion = "_HairGradualTranstion";//渐变过渡光滑度(float)
[SerializeField]
public string Hair_Noise = "_Noise";//打乱各项异性高光(float)
[SerializeField]
public string Hair_NoiseTiling = "_NoiseTiling";//各项异性高光密集度(float)
[SerializeField]
public string Hair_Metallic = "_Metallic";//金属度(float)
[SerializeField]
public string Hair_Roughness = "_Roughness";//粗糙度(float)
[SerializeField]
public string Hair_LightColor1 = "_LightColor1";//高光1颜色(color)
[SerializeField]
public string Hair_LightStrength1 = "_LightStrength1";//高光1强度(float)
[SerializeField]
public string Hair_LightExponent1 = "_LightExponent1";//高光1范围(float)
[SerializeField]
public string Hair_LightPosition1 = "_LightPosition1";//高光1移动(float)
[SerializeField]
public string Hair_LightColor2 = "_LightColor2";//高光2颜色(color)
[SerializeField]
public string Hair_LightStrength2 = "_LightStrength2";//高光2强度(float)
[SerializeField]
public string Hair_LightExponent2 = "_LightExponent2";//高光2范围(float)
[SerializeField]
public string Hair_LightPosition2 = "_LightPosition2";//高光2移动(float)
//衣服
[SerializeField]
public string Clothes_Mask1Color = "_Mask1Color";//颜色1(color)
[SerializeField]
public string Clothes_Mask2Color = "_Mask2Color";//颜色2(color)
[SerializeField]
public string Clothes_Mask3Color = "_Mask3Color";//颜色3(color)
```

当设置成功后，业务侧/包内都可以使用如UT.Customizer.Define.S.Skin_BaseMap来访问真实的Shader属性名。



## 配置表导出
CustomizeDataPopup.cs提供了一组静态方法，用于获取customize_face_f_000内编辑好的数据，业务侧调用获取后，可自行选择如何序列化这些数据。



从CustomizeData获取脸部塑形数据为例：

:::tips
+ **Method**：public static void GetShapeTableData(CustomizeData customizeData, List<Dictionary<string, string>> tableData, Dictionary<string, string> descData)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizeData | CustomizeData | 要导出的数据 |
| tableData | List<Dictionary<string, string>> | 数据表，List的每一项代表一行数据。键为表头，值为数值。 |
| descData | Dictionary<string, string> | 每一列的中文描述 |




以下为所有表导出的使用示例：

```lua
private static void CreateSheet(string sheetName, IWorkbook workbook, CustomizeData customizeData)
{
    ISheet shapeSheet = workbook.CreateSheet(sheetName);
    int rowIndex = 0;
    IRow row = shapeSheet.CreateRow(rowIndex);
    List<Dictionary<string, string>> dataTable = new List<Dictionary<string, string>>();
    Dictionary<string, string> descTable = new Dictionary<string, string>();
    switch (sheetName)
    {
        case "Shape":
            CustomizeDataPopup.GetShapeTableData(customizeData, dataTable, descTable);
            break;
        case "FacialAnimation":
            CustomizeDataPopup.GetShapeAnimTableData(customizeData, dataTable, descTable);
            break;
        case "Makeup":
            CustomizeDataPopup.GetMakeupTableData(customizeData, dataTable, descTable);
            break;
        case "Eyelash":
            CustomizeDataPopup.GetEyelashTableData(customizeData, dataTable, descTable);
            break;
        case "Eye":
            CustomizeDataPopup.GetEyeTableData(customizeData, dataTable, descTable);
            break;
        case "Skin":
            CustomizeDataPopup.GetSkinTableData(customizeData, dataTable, descTable);
            break;
        case "HairColor":
            CustomizeDataPopup.GetHairColorTableData(customizeData, dataTable, descTable);
            break;
        case "ClothesColor":
            CustomizeDataPopup.GetClothesColorTableData(customizeData, dataTable, descTable);
            break;
        default:
            break;
    }
    if (dataTable.Count == 0)
        return;
    row.CreateCell(0).SetCellValue("id");
    var idx = 1;
    foreach (var item in descTable)
    {
        if (item.Key != "id")
        { 
            row.CreateCell(idx).SetCellValue(item.Key);
            idx++;
        }
    }
    rowIndex += 1;
    row = shapeSheet.CreateRow(rowIndex);
    for (int i = 0; i < descTable.Count; i++)
    {
        row.CreateCell(i).SetCellValue("null");
    }
    rowIndex += 1;
    row = shapeSheet.CreateRow(rowIndex);
    for (int i = 0; i < descTable.Count; i++)
    {
        row.CreateCell(i).SetCellValue("Normal");
    }
    rowIndex += 1;
    row = shapeSheet.CreateRow(rowIndex);
    row.CreateCell(0).SetCellValue("唯一id");
    idx = 1;
    foreach (var item in descTable)
    {
        if (item.Key != "id")
        { 
            row.CreateCell(idx).SetCellValue(item.Value);
            idx++;
        }
    }
    rowIndex += 1;
    for (int i = 0; i < dataTable.Count; i++)
    {
        Dictionary<string, string> data = dataTable[i];
        int col = 0;
        row = shapeSheet.CreateRow(rowIndex);
        rowIndex += 1;
        foreach (var item in descTable)
        {
            if (item.Key == "id")
            { 
                row.CreateCell(col).SetCellValue(Convert.ToInt32(data["id"]));
                col++;
            }
        }
        foreach (var item in descTable)
        {
            if (item.Key != "id")
            {
                int intValue = 0;
                float floatValue = 0;
                if (int.TryParse(data[item.Key], out intValue))
                    row.CreateCell(col).SetCellValue(intValue);
                else if(float.TryParse(data[item.Key], out floatValue))
                    row.CreateCell(col).SetCellValue(floatValue);
                else
                    row.CreateCell(col).SetCellValue(data[item.Key]);
                col++;
            }
        }
    }
    //合并单元格
    for (int i = 0; i < descTable.Count; i++){
        CellRangeAddress cellRangeAddress = new CellRangeAddress(0, 1, i, i);
        shapeSheet.AddMergedRegion(cellRangeAddress);
    }
}
```



对于shader属性来说，表的键命名可能shader属性名，或者属性名_Path（纹理），或者属性名_Min/_Max（float调整范围）。

注意，为了保持配置表的稳定，CustomizeDataPopup提供的方法组的返回里字典的key，为固定的未改名版本，即包内基类定义的，而非业务侧覆写的。所以当有改属性名需求时，业务层自己实现映射关系表。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1726146200560-18eb075d-e3cd-443e-8f1c-e32521a5af75.png)



# 六.依赖项
运行时核心dll位置：示例工程/client/Assets/Scripts/CustomizerRuntime

共两个dll:

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727601856471-788e7da3-ddf5-441b-8b65-36f03aec87f0.png)



编辑器核心dll：示例工程/client/Assets/Scripts/Editor/CustomizerEditor

共一个dll:

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727601876154-610213f1-9223-45d6-94a9-e2fcf5c9fc82.png)



UT.Customizer.dll依赖项：

布料插件位置：示例工程/client/Assets/UnityPackages/MagicaCloth2

Json插件位置：示例工程/client/Assets/UnityPackages/com.ut.json

后三个为Unity官方库，需自行在PackageManager中添加

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727601637120-b0008140-59d0-4672-a45e-1f2bba009f0a.png)



UT.Customizer.Editor.dll依赖项：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727602127250-1d899484-94cc-46a7-a456-c6235d085e90.png)

