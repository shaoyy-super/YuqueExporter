## 1.系统API
静态类CustomizerLocator.cs，内部提供一组接口，用于使用者向捏人系统内部注入依赖的对象或参数。

### 初始化
:::tips
+ **Method**：public static void InitBodyModule(ICustomizerResLoader customizerResLoader)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizerResLoader | ICustomizerResLoader | 资源加载器，提供配置读取功能。 |




## 2.捏人控制器
CustomBodyController.cs提供了所有对某个角色捏人使用的接口。当同时使用捏人和换装时，建议先初始化捏人控制器，因为裸模状态具有更干净的骨骼结构，避免了挂点衣服同名骨骼的干扰。

### 初始化
:::tips
+ **Method**：public void Init(ICustomizerInfo customizerInfo, BodyData bodyData, GameObject targetObj, GameObject templateObj, int modelId,  bool supportRecalculateNormals = false)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizerInfo | ICustomizerInfo | 提供访问所有骨骼部位滑条值的功能。 |
| bodyData | BodyData | 捏人骨骼及范围数据，决定了每个滑条将如何影响骨骼。实现了ICloneable的可序列化对象。 |
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
如果在初始化时传入了支持计算法线，此处获取到法线计算器对象

:::tips
+ **Method**：public MeshNormalsCalculator GetMeshNormalsCalculator()

:::

法线计算器的主要接口为RecalculateNormals()，内部将同步完成法线的计算，一般骨骼发生大幅度改变时，换装结束后，需要重新计算法线。



如果使用换装模块，在CustomClothesController.Init中传入法线计算器，换装模块将在内部自动的设置需要计算法线的SkinMesh并在每次换装时更新。



如果不使用换装模块，同时需要计算法线功能。

需要在RecalculateNormals之前，调用一次SetTargetSkinMesh设置需要计算法线的SkinMesh组件。

接着调用SetUseMesh传入mesh相关数据，并在mesh发生变化时(骨骼，材质，submesh，顶点信息等)再次调用SetUseMesh进行更新。



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
当模型物体被销毁/回收时，需主动调用。内部会将进行清除操作，根据参数选择是否重置骨骼状态为TPose，并移除身体修改器组件。一旦调用销毁，CustomBodyController的其他接口将不能再使用。

:::tips
+ **Method**：public void Destroy(bool needResetBone)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| needResetBone | bool | 是否需要重置骨骼和avatar为初始状态 |


## 3.BodyData
捏人骨骼参数，实现了ICloneable的可序列化数据对象。一般每个裸模会有一个BodyData对象实例，用来描述对应滑条值如何影响骨骼。

### 得到所有开启的捏人调整项的键
:::tips
+ **Method**：public HashSet<string> GetSupportBodyPartKeySet()

:::



## 4.依赖的excel配置
捏人依赖1张配置表，详细见excel文件内注释。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1721039884739-0c166e82-b11b-48f8-95bb-22b552d49bcb.png)

