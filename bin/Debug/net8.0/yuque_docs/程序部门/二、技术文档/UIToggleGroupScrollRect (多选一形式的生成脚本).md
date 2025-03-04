### 序言: 为什么会有这个脚本
在前端界面显示时通常会有一组按钮, 玩家在这些按钮中选一个会使该按钮处于激活态并且其他按钮处于未激活态, 并触发相关事件. 每次这样一个功能都需要各个界面单独管理一套, 代码相对繁琐且不易阅读. 同时如果使用UGUI本身的ToggleGroup管理又相对复杂且效率较差. 故产生了此脚本处理该系列问题.



### 脚本参数使用:
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1713853365958-b45b810a-e084-4e31-8cc3-f2d9ba1a145d.png)

本脚本继承了UIScrollRect, 基础参数与其一致, 额外添加了ActiveToggleIndex, 用于设置生成时期望激活的toggle(从0开始的下标).

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1713853626675-8af7caf3-b708-4915-a1bb-11f3dc6c99b4.png)

<font style="color:#DF2A3F;">生成的Prefab预制需要在根节点下挂载Toggle脚本, 否则不会加入管理序列内. </font>

### 脚本扩展方法:
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1713857539536-2734640b-0bdb-498e-a58b-d3665aa89cd0.png)

SetToggleScrollRectSelectAction: 设置生成的预制被激活时执行的方法. 传入预制体的GameObject和生成的下标(从0开始).

SetToggleScrollRectAction: 设置生成预制体时执行的初始化方法. 传入预制体的GameObject和生成的下标(从0开始).

SetToggleScrollRectDragEndAction: 设置对应的滑动列表滑动结束事件.

RefreshToggleScrollRect: 刷新生成的预制体.

SetToggleScrollRectActiveIndex: 设置当前激活的Toggle, 传入下标(从0开始).

GetToggleScrollRectActiveIndex: 获得当前激活的Toggle下标(从0开始). 

SetToggleScrollRectCanScroll: 开关组件的滑动效果.

### 脚本基础代码展示:
思路: 继承UIScrollRect实现相关的滑动列表及组件生成方法. 通过Dictionary<int, Toggle>的形式管理生成的预制中的Toggle(避免动态生成时的回收机制影响到对应toggle的索引). 记录单个激活的Toggle, 在每次玩家点击触发OnValueChanged的时候关掉之前的Toggle并记录新的(避免逐个通知带来的消耗).

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1713855060093-f5a6fc47-d7a9-4377-a8d8-5d117bc21d19.png)

