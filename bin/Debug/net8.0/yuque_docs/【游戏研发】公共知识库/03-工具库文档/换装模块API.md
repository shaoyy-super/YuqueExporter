## 1.系统API
静态类CustomizerLocator.cs，内部提供一组接口，用于使用者向换装系统内部注入依赖的对象或参数，同时控制缓存释放。

### 初始化
:::tips
+ **Method**：public static void InitClothesModule(ICustomizerResLoader customizerResLoader)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| customizerResLoader | ICustomizerResLoader | 资源加载器，提供配置读取,预制体加载/释放功能的实现。 |




## 2.换装控制器
CustomClothesController.cs提供了所有对某个角色换装使用的接口。

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
衣服预制体加载可能是一个异步过程，在上传换装未结束时，需拦截下一次换装操作。

:::tips
+ **Method**：public void IsReplacing()

:::



### 替换某个部位对应的item
普通部位/散件组合套装/真套装都使用此接口。当头发/身体/脚未被覆盖时，会使用默认的。如在穿连衣裙时，替换了下装，那么内部会使用默认的上装。

:::tips
+ **Method**：public void  Replace(ClothesType clothesType, int itemId, Dictionary<ClothesType, int> newClothesDataDic, Action<ReplaceClothesResult> onFinish)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| clothesType | ClothesType | 要替换的部位类型枚举 |
| itemId | int | 新的itemId，小于等于0为脱下 |
| newClothesDataDic | Dictionary<ClothesType, int> | 换装数据结果，含有所有部位的新的itemId |
| onFinish | Action<ReplaceClothesResult> | 此次换装结束回调 |




### 获取item和身上哪些已穿戴的部位冲突
即穿上itemid后，有哪些身上已穿戴的部位会因为冲突而被卸下

:::tips
+ **Method**：public void  GetItemConflictSet(int itemId, HashSet<ClothesType> result)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| itemId | int | 要查询的itemId |
| result | HashSet<ClothesType> | 返回结果 |




### 强制设置穿搭方案
脱掉所有部位的衣服，强制使用传入穿搭方案生成模型，不检查传入的部位直接的冲突。当头发/身体/脚未被覆盖时，会使用默认的。

:::tips
+ **Method**：public void  GetItemConflictSet(int itemId, HashSet<ClothesType> result)

:::

| 参数名 | 参数类型 | 参数描述 |
| --- | --- | --- |
| itemId | int | 要查询的itemId |
| result | HashSet<ClothesType> | 返回结果 |




### 销毁
当模型物体被销毁/回收时，需主动调用。内部会将所有身上部位的衣服回收，并移除服装修改器组件。一旦调用销毁，CustomClothesController的其他接口将不能再使用。

:::tips
+ **Method**：public void Destroy()

:::



## 3.换装结果回调对象
ReplaceClothesResult.cs，在每次换装结束的回调中，会回传入一个该对象实例，注意该对象为内部复用对象，禁止持有。



| 属性名 | 属性类型 | 属性描述 |
| --- | --- | --- |
| isSucceed | int | 是否成功换装。false代表调用参数不合法，详细错误见log。 |
| isShoesSoleTypeChange | bool | 鞋跟类型是否发生了变化 |
| newShoesSoleType | ShoesSoleType | 新的鞋跟类型。当isShoesSoleTypeChange为True时有值。 |
| conflictRemoveSet | HashSet<ClothesType> | 此次换装，有哪些原本已穿戴的部位因为冲突被移除了。 |


## 4.依赖的excel配置
换装依赖四张配置表，详细见excel文件内注释。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1721036766047-64d6cfc1-6275-47c6-be6d-4056bf1e92b0.png)

