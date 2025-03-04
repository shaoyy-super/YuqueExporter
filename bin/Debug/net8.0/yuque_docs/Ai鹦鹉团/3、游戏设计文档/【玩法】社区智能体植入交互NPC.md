# 1.设计框架
## 一、**概述**
### 1.1 **文档说明**
<font style="color:rgb(38, 38, 38);">●本文档详细说明了在社区场景内的智能体NPC所涉及的功能和玩法介绍</font>

### 1.2 文档维护
| <font style="color:white;">版本</font> | <font style="color:white;">时间</font> | | <font style="color:white;">负责人</font> | <font style="color:white;">修改内容</font> |
| --- | --- | --- | --- | --- |
| <font style="color:black;">v1.0</font> | <font style="color:black;">2025/1/13</font> | | 朱柯萍 | <font style="color:black;">文档创建</font> |
|  |  | |  |  |


### 1.3 设计思路


![画板](https://cdn.nlark.com/yuque/0/2025/jpeg/38390214/1736500641836-238b9766-31a9-4c22-8095-96c092ede4b4.jpeg)



## 二、**功能模块**
### 2.1 **NPC位置**
![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1726817314206-dab0c24d-25cb-4900-8b4c-628f6552470f.png)

 **随机路线**

+ <font style="color:rgb(38, 38, 38);">在社区场景内设置NPC移动路线，NPC将根据路线进行随机移动</font>
+ <font style="color:rgb(38, 38, 38);">移动方式可配表设置多个坐标点位，NPC在相邻两个点位间随机移动</font>
+ <font style="color:rgb(38, 38, 38);">没到达一个点位后将在该位置停留一段时间，具体停留时间可通过配表控制</font>

 **交互动作**

+ <font style="color:rgb(38, 38, 38);">NPC可与处于空闲状态的可交互设施进行交互，如休闲长凳等</font>
+ <font style="color:rgb(38, 38, 38);">NPC靠近交互设施后将自动播放NPC与设施的交互动作，并持续一定时间，具体持续时间可通过配表控制</font>
+ <font style="color:rgb(38, 38, 38);">播放完成交互动作后，将播放离开动作并回到随机路线移动的状态</font>

### 2.2 **NPC交互**
**对话选项**

+ <font style="color:rgb(38, 38, 38);">对话分为聊天式和选项式</font>
+ <font style="color:rgb(38, 38, 38);">点击NPC角色形象后出现对话选项，点击选项后根据所选择内容展开后续选项或做出相应反馈</font>
+ <font style="color:rgb(38, 38, 38);">点击对话选项将出现私聊界面</font>

![](https://cdn.nlark.com/yuque/0/2025/png/38390214/1736843085562-a4739358-4cf5-496c-9d83-7f9814d346f5.png)

### 2.1 **NPC聊天**
  **聊天界面**

+ <font style="color:rgb(38, 38, 38);">NPC将自动成为玩家的第一个好友，不需要添加也无法删除该好友</font>
+ <font style="color:rgb(38, 38, 38);">私聊功能与普通聊天功能一致</font>
+ <font style="color:rgb(38, 38, 38);">玩家可以输入文字内容和语音内容与NPC进行对话</font>
+ <font style="color:rgb(38, 38, 38);">输入后将收到自动回复，在回复生成期间将暂时不能进行后续的内容输入，需要在收到上一条回复后才能继续进行输入。</font>

![](https://cdn.nlark.com/yuque/0/2025/png/38390214/1736911329196-b2e78439-5a5f-4b7f-99cf-fe344431c334.png)

### 2.1 游戏内**智能体集合**
聊天NPC

直播广场

主线剧情内邂逅素人-捏脸-性格设定

设定随机场景出现（小屋/社区/直播间），可人为设置出现场景



官方智能体/素人智能体



单独案子：官方设定-角色设计/声线-编辑内容

好友创建智能体（匹配条件预设-复制别人的/创建新的）



