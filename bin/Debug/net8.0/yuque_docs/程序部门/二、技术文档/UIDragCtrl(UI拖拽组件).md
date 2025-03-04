### 方案选择
策划需求：UI上展示的模型支持点击、拖拽跟随，拖拽初模型和拖拽末模型交换

预设方案：

      方案1：射线检测模型碰撞器  优点：Lua不用关心拖拽过程，C#处理拖拽跟随;  缺点:1、射线检测必须是模型碰撞器范围，范围不易扩展  2、点击与拖拽事件冲突

     方案2：UI区域检测    优点：范围调整更灵活，事件处理更灵活  缺点：1、UI区域和模型需要在Lua实现绑定关系  2、特殊情况下需要注意自适应

综上所述，考虑范围调整的需求性更大，而且表现更好，所以采用UI区域检测实现

### 组件适用情景
1、点击范围a区域,响应关于GameObjectA绑定的事件（需求1）

2、拖拽开始区域为a时：选中GameObjectA，拖拽进行时：GameObjectA跟随鼠标（需求2）

3、拖拽开始区域为a时：选中GameObjectA，拖拽进行时：GameObjectA跟随鼠标，   拖拽结束鼠标停留的区域为b，则GameObjectA和GameObjectB互换位置。可支持多个区域（需求3）

以上三种需求都支持

### 组件结构
1、UIDragCtrl.cs   负责Unity的交互事件处理，开放接口有：

事件：

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725945658634-cc69baf4-d17d-45b3-bcf7-5821f34d40a2.png)

拖拽范围数组：

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725945696682-ffa5fbbd-6bac-4be8-93fd-19d654186cd8.png)

2、ModelDragCtrl.lua  负责处理UI区域和模型的绑定，实现拖拽跟随，拖拽结束复位基础逻辑，不涉及数据处理，开放接口：

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725946205419-024f04b8-0950-4067-940b-54e8a22f0387.png)

注意:二者不是强绑定，ModelDragCtrl.lua只是在lua层使用UIDragCtrl.cs对模型拖拽，交换的二次封装，避免同学二次开发相同功能，UIDragCtrl.cs是实现点击，拖拽的事件处理，可在lua端根据自己功能在回调中实现逻辑

### 用法
UIDragCtrl.cs 

1、跨区域需求，实例如下：

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725946835436-c2f70895-51fd-4cb2-b7d9-f51d7f4ae240.png)

DragModel节点是接收UI事件的UI ，ImageArea1、ImageArea2、ImageArea3三个UI区域（该区域不限制UI类型，用的时候只取其RectTransfrom），UIDragCtrl的CheckAreas数组包含三个UI区域，需要对应功能中的三个模型节点，用数组的索引值映射关系，![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725947198360-b9933fab-11ee-4860-ba10-b2a019745f31.png)算鼠标事件点的相对坐标用的，无特殊需求无需设置即可(运行时会取自身)

Lua端通过回调中的索引值，找到对应的模型节点实现需求

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725947420526-0d0cc243-02f4-4b85-a10f-15b6cee21198.png)

2、单个区域需求

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725947486935-ec749354-c061-475f-a157-c002182f6198.png)

无需关心索引值，只需关心事件坐标实现需求即可



ModelDragCtrl.lua

New的时候传入UIDragCtrl.cs的实例和节点列表，如下：

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725947653589-affd081b-cc9c-4271-9e1d-ca34f4a397ff.png)

可添加回调事件

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1725947731668-f17ef827-bd8f-4a74-9008-870a0a22cd15.png)



### 组件效果
[20240910_135746.mp4](https://snh48group.yuque.com/attachments/yuque/0/2024/mp4/44744345/1725947922907-a0fab5f1-b975-45fa-8934-f8bf5b177098.mp4)

