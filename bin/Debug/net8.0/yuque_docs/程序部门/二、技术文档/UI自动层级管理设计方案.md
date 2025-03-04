# 一、简介
原UI层级管理方案是每个界面手写固定值，有两个明显问题：1）很多人对数值没有规划，乱写；2）一旦某个地方出现层级问题，被迫调整已经存在的UI层级，就很容易发生改好a，搞坏b；修b又搞坏a的循环中。

基于【后开的界面，就应该放在最上面让玩家看到】的大前提，决定讲UI的层级进行自动设置，不再手动维护。（Top层的UI很特殊，不适应上面的前提）

# 二、结构设计
+ UILayerMgr.lua：层级管理器，采用独立模块供UIMgr使用，也可以酌情被特殊的功能使用
+ UIMgr、CSys：层级相关的配置只保留LayerName
+ LayerSubject.cs：单例，记录需要关注层级变化的对象（回调），发生层级变化后回调给有效的Observer
+ SortingLayerCtrl：进行相对层级调整的辅助脚本



# 三、详细结构设计
### 层级管理策略
1. 根据LayerName分为3-4个层，每层内的Order独立计算
2. 新打开的UI，Order是在层内最大Order基础上+5
3. Top层比较特殊，它仍然采用固定Order的方式。一方面这一层的内容非常少，可控；另一方面，Top的UI并不遵循后开的UI一定要显示出来的规则，比如网络弹窗就是要在最上面，后面弹出来的UI是什么。
4. 考虑有些组件，需要依赖UI的层级设置自己的层级，所以，每次改完主UI层级后，抛个消息给ILayerObserver，让组件做出更新



UI间的层级间隔为5，使用常量定义，如果有需要可以适当扩大。

若后续出现某个界面自己要占用大量Order范围，可以拓展成每个UI自己定义要占据的范围，LayerMgr根据配置处理Order。



### 类图
![](https://cdn.nlark.com/yuque/0/2024/png/43256857/1720772617795-30e42c8b-78ce-4fa5-bada-9e1647776d5b.png)



UILayerMgr

开关任意UI时，都要通知给LayerMgr，内部需要以栈的结构记录当前开着的UI及其起始的Order

+ _layers：先以LayerName分组，不同组的Order计数相互独立；组内用栈记录 { UI，Order}
+ GetLayerOrder(LayerName)：查询当前某层的Order，用于UI内需要自己子节点层级（或者非UI层级）的情况



LayerSubject

+ _observer：Transform是观察变化的节点，Action是处理变化的回调。触发变更时，通过trans的层级关系，决定是否执行回调。



SortingLayerCtrl

+ TargetType { Canvas, Effect }
+ RefCanvas：参考层级信息的Canvas，通常是最近的父级Canvas
+ OnEnable：激活时刷新子自身层级，并向LayerSubject注册事件



### 已知问题的解决方案
1. LayerMgr._layers用栈记录的每个UI的Order，开新UI时始终取栈顶的层级+间隔
2. 还是相同的方案，用LayerSubject统一收集层级变化的回调，内部按需进行回调
3. Top层的UI使用固定的Order
4. 使用TML的模块自己处理：播TML时传参修改特效的层级，使用UILayerMgr.GetLayerOrder辅助确定Order
5. SortingLayerCtrl修改特效层级时，保持里面PS间的层级间隔不变：UI_Order +Offset + PS原始Order - PS最小Order
6. 未执行Close 的UI就是开着的UI，应一直占用Order，其余问题自己处理
7. 特殊模块的需求自己处理。资源条如果时独立UI应该开-关配对；如果是一个Item Prefab应该是实例化的模块负责设置正确的Order。同4
8. 统一使用SortingLayerCtrl动态修改层级



# 四、流程设计
![](https://cdn.nlark.com/yuque/0/2024/png/43256857/1720772594612-98675bb8-9a0b-4688-a251-daf2330db511.png)



