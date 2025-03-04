## 脚本类文件`BlendshapeModifier.cs`
### 私有成员变量
| 变量名称 | 类型 | 注释 |
| --- | --- | --- |
| m_name2blendShape | Dictionary<string, BlendShape2SkinnedMesh> | Blendshape和蒙皮的对应关系字典 |
| m_addon2blendshape | Dictionary<string, List<BlendShape2SkinnedMesh>> | 额外添加的带变形器的蒙皮数据 |


### 公共接口
### 初始化
:::tips
+ **Method**：public void Init(GameObject headGo)

:::

### 参数
| 名称 | 类型 | 注释 |
| --- | --- | --- |
| headGo | GameObject | 角色头部 |


:::info
💡 注意，目前没有添加法线贴图和AO贴图。

:::

### 添加附加的带变形器的蒙皮网格
:::tips
+ **Method**：public void AddSkinnedMeshRenderer(SkinnedMeshRenderer renderer)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| renderer | SkinnedMeshRenderer | 带Blendshape的蒙皮 |


### 添加附加的带变形器的蒙皮网格
:::tips
+ **Method**：public void AddSkinnedMeshRenderer(SkinnedMeshRenderer renderer)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| renderer | SkinnedMeshRenderer | 带Blendshape的蒙皮 |


### 添加额外对象蒙皮变形器
:::tips
+ **Method**：public void AddExtraObject(GameObject go)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| go | GameObject | 带蒙皮变形器的GameObject |


### 删除额外对象蒙皮变形器
:::tips
+ **Method**：public void RemoveExtraObject(GameObject go)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| go | GameObject | 带蒙皮变形器的GameObject |


### 设置变形器权重
:::tips
+ **Method**：public void SetBlendShapeWeight(string name, float value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| name | string | Blendshape名称 |
| value | float | 权重值 |


### 获取变形器权重
:::tips
+ **Method**：public float GetBlendshapeWeight(string name)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| name | string | Blendshape名称 |


### 移除所有额外添加的蒙皮
:::tips
+ **Method**：internal void RemoveExtractSkinnedMeshRenderers()

:::

### 添加蒙皮数组
:::tips
+ **Method**：internal void AddExtractSkinnedMeshRenderers(List<SkinnedMeshRenderer> skinnedMeshRenderers)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| skinnedMeshRenderers | List<SkinnedMeshRenderer>  | 可能需要被同步调整的带变形器的蒙皮数组 |


### 注意事项
:::info
💡 如果用户的用户信息不存在，将会使用请求的数据创建一个新的用户信息。

:::





