## 一. 简介
在C#层，项目中存在多个根据列表动态生成物体的脚本，例如PrefabHelper，UIPrefabHelper，UIScrollRect，AssetHolder等，这些脚本每个都有自己的使用方法，以至于在生成物体时我们需要关心每一个脚本所需的独特之处。但是这实际上是没有太大意义的，为了简化动态生成物体的过程，我们在Lua层封装了UIItemLuaList脚本，这个脚本可以对动态生成GO整个过程进行统一处理，进而减少开发人员为了适应不同的C#脚本之间的差异而花费的额外时间。

## 二. 脚本内容
#### UIItemLuaList
作为这个系统最核心的脚本之一，它包含动态生成GO所需的所有功能，设置数据列表（SetItemList），设置选中目标回调（AddSelectCallback），刷新填充回调（AddFillCallback），设置选中的GO（SetSelectIndex），下面将解释每个方法的具体用法和作用

##### 构造方法
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729510444564-7f981372-8067-4d54-9b4a-8555e8378719.png)

构造方法可传入3-4个参数，c4l为包含列表的c4l；comp_index为列表在上述c4l中对应的index；item_base_or_prop_type为子物体对应的lua脚本，在山海项目中可以用UIPropType的枚举代替脚本名称。注意，上述脚本必须继承UI_ItemLuaBase；最后一个为可选的额外参数，**如果需要让子物体接收点击事件，需要在子物体的根物体上挂载一个按钮，并在这个额外参数中传入{use_custom_click = true}**

##### SetItemList
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729510456601-69aea47e-161f-4184-8425-684732c78f15.png)

此方法用于生成GO，设置列表。需传入一个列表，包含每一个物体需要的所有数据，列表将被设置为UIItemLuaList中的public成员，最终会被私有方法_OnItemInit传递给子物体对应的lua脚本中的OnShow方法作为参数；最后一个参数default_index为默认被选中的一个GO的序号，传入这个值会调用对应GO的选中回调。

##### AddSelectCallback
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729512873952-a05f8db7-ee35-4cb9-881f-1c0b0c6c6716.png)

此方法用于设置被选中目标的回调，被传入的参数为一个lua回调函数callback，它将被设置为脚本的public成员，对于callback函数，它被调用是会传入两个参数，第一个参数为被调用的item的脚本的实例，第二个为此实例对应的序号。callback函数将在私有方法_OnSelectItem中被调用

##### AddFillCallback
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729512893316-dc4dcbc3-091d-4762-a273-8d4633e58dd6.png)

此方法和AddFillCallback交互方式基本一致，区别在于它刷新的是填充回调，它会被私有方法_OnItemInit调用

##### SetSelectIndex
![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1729512903057-8b8124da-d5e8-4010-8f02-79407ceec451.png)

此方法在可以设置某个物体被选中，主要用于在物体没有被点击的时候模拟被选中的情况，例如需要设置默认GO的时候，此方法需要传入一个参数，为GO在列表中的index



## 三.使用流程
![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44684279/1729488647494-21a6becb-5ab7-43fd-a817-80c352087d1e.jpeg)



## 四.使用参考
如需使用，请参考捕鱼项目中UI_PersonalInfo_Ctrl中的item_lua_list的用法

