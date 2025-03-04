## 一. 简介
当我们在使用Lua完成特性的时候，你是否经常因为无法创建物品而感到烦恼。我们的项目框架向你提供了多种解决方案，以解决不同情况下创建GO的需求。

PrefabHelper主要用于创建数量并不多，而且每一个GO都来自同一个预制体的情况，这一点要区分AssetHolder（可以来自多个预制体）和UIScrollRect（可以创建大量物品）。PrefabHelper还存在一个专门用于创建UI类的，但是他们实际没有继承关系，这个类叫UIPrefabHelper。

#### PrefabHelper组件
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729046238124-8626261c-5c82-4354-ada1-e9e8d4b8e18b.png)

#### UIPrefabHelper组件
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729046283338-2d3392d0-83d9-4dde-bf9e-73fb1c33ba3a.png)

需要注意的是，PrefabHelper和UIPrefabHelper存在相同的点也存在许多的不同，以下是几个决定性的不同点：

| | PrefabHelper | UIPrefabHelper |
| --- | --- | --- |
| 预制体 | 拖拽，只能有一个 | 拖拽，可以有多个 |
| 父物体 | 不能替换，默认为自己 | 需要拖拽设置 |
| 对象池 | 自己的根节点 | GameObjectCountPoolMgr生成的实例 |
| Lua调用 | GL_Tools.ShowGrid/直接调用 | GL_Tools.ShowUIGridByIndex/<br/>ShowUIGridByIndexWithCount/直接调用 |
| 预制体来源 | 可以直接拖拽场景中的物体作为创建的模板 | 不能直接拖拽场景中的物体（虽然物体还是会被创建出来但是会弹出一个Error），只能从资源中创建 |




## 二. 用法
#### PrefabHelper的用法
想要使用PrefabHelper，需要完成以下几个步骤

![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44684279/1729059986531-3a293e02-488c-4671-a62b-44c297ceb9b2.jpeg)

请注意，尽量不要直接通过Lua调用组件上的Create方法，而是通过C4L设置接口调用，**如需直接调用，需请示对应项目组长或者主程确认用法**

具体调用方式，可以参考山海项目，BattleHpBuffCtrl.lua脚本Refresh方法第57行（因后期改动行数可能会发生变化）的调用方式，以及捕鱼项目C4LCustom.cs中SetPrefabHelperAction等一系列方法的调用模式



#### UIPrefabHelper的用法
UIPrefabHelper的用法和PrefabHelper大同小异，唯一不同的地方是它需要传入本次需要创建的Prefab的ID，具体如下：

![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44684279/1729060661450-f754fe30-2df0-4457-860c-70f74a98cb60.jpeg)

具体用法可以参考山海项目中C4LCustom中的FillItems方法

#### 参数的传递
##### PrefabHelper的参数传递
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729061493092-c323b659-d437-4aab-92a3-f2fa74acb3b4.png)

PrefabHelper可以设置一个Lua回调函数OnInitItem，每创建一个物体返回一次，返回创建的GO的引用和这个GO对应的Index

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729061514845-ed89f9a7-91c3-446f-b3a5-d8219e75b62f.png)

PrefabHelper用于创建物体的Create方法会接受一个int值，为创建物体的总数量

##### UIPrefabHelper的参数传递
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729061700241-9e0af230-d8c5-4e38-857c-6c04c671831c.png)

UIPrefabHelper可以设置一个Lua回调函数OnInitItem，每创建一个物体返回一次，返回创建的GO的引用和这个GO对应的Index

UIPrefabHelper可以根据预制体列表中物体的名字或者Id创建物体，

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729061875387-d1c58d37-6f1e-40de-ad6c-bde03bd094c6.png)

InitByPrefabName接受三个参数，第一个参数为预制体名字，第二个参数为需要创建的物体的数量，第三个为一个额外参数，如果传入，将被保存在Helper中的extParam

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729061964968-3873684c-6184-4eb0-b715-11af93141ef7.png)

InitByIndex同样接受三个参数，第一个参数为预制体的index从0开始，这个index代表预制体在拖入组件的list中放置的位置，第二第三个参数同上



