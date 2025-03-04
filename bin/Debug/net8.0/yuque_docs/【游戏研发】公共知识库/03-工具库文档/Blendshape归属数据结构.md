## 脚本类文件`BlendShape2SkinnedMesh.cs`
### 公有成员变量
| 变量名称 | 类型 | 注释 |
| --- | --- | --- |
| name | string | Blendshape名称 |
| index | int | Blendshape在蒙皮中的序号 |
| renderer | SkinnedMeshRenderer | Blendshape所在的蒙皮 |


### 公共接口
### 设置变形器权重
:::tips
+ **Method**：internal void SetBlendShapeWeight(float value)

:::

| 参数 | 类型 | 注释 |
| --- | --- | --- |
| value | float | 权重值 |


### 获取变形器权重
:::tips
+ **Method**：internal float GetBlendShapeWeight()

:::

### 注意事项
:::info
💡 如果用户的用户信息不存在，将会使用请求的数据创建一个新的用户信息。

:::





