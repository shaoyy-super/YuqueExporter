# 一、简介
内容：主要实现野怪在地图上待机-行走到随机点-待机的AI行为

主要方案： 采用腾讯behaviac行为树进行AI设计



# 二、结构设计
##### 相关目录
工作空间（编辑器读取的目录）：client\ExtAssets\BehaviacWorkspace

运行时脚本目录：client\Assets\Scripts\Exts\Behaviac，三个子目录

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1720433508520-432a418b-ec0a-4572-a4d1-00cb1c8ca74d.png)

1、Exported 编辑器导出的CS脚本目录

2、Exts 自定义扩展的行为树相关的脚本目录

3、Runtime 行为树插件的运行时代码（非必要不修改）

 xml和bson导出目录: client\ExtAssets\Lua\Config\behaviac

<font style="color:#DF2A3F;">注意：编辑器中读取xml，发布包中读取bson，发布时要加上BEHAVIAC_RELEASE宏</font>

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1720433777487-7c0d01e6-f045-467e-9a2b-a228806e0efc.png)

##### 行为树的资源处理
新增FileCustomManager 重写行为树的FileManager文件打开行为树序列化文件的方法，修改加载方式

##### 热更处理
扩展行为树编辑器支持可视化显示C#通用函数调用Lua函数，生成xml时把Lua函数和参数转成Json格式的字符串当作C#通用函数的参数，运行时将字符串参数解析成Lua参数，作为C#通用函数的方法体执行

##### 行为树绑定管理
调用BehaviacManager.Creat绑定行为树代理

1、地图上的单位（如野怪）：参数必须为单位的根节点(方便获取UnityActorCtrl)

2、非单位绑定AI代理：参数为场景内的某一节点，可以是空节点

创建时会返回AgentId，为该实例Agent的唯一表示

# 三、详细结构设计
![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44744345/1720431547630-de92f645-5b87-4c80-8e55-7e45e007799a.jpeg)

##### AgentBase:自定义代理基类
agentId：Agent的唯一标识

InitAgent：Start时执行，初始化代理的部分变量，避免行为树运行中重复获取，例如MonsterAI获取NavMeshAgent组件

Play：播放行为树

Stop：停止行为树

Pause：暂停行为树

CancelPause：取消暂停行为树（即继续播放）

<font style="color:#DF2A3F;">Stop和Pause区别</font>：Stop是把当前行为树信息unload，行为树为null，重新播放需要Play新的treeName，故表现上则是开始执行新的行为树

 Puase 只是把停止轮询，重新播放CancelPause继续轮询，表现上则是继续播下一个动作

Play和Stop在AI行为树和流程行为树中无差异，所以在基类中实现(后续如有差异可重写)；Puase和CancelPause在AI行为树和流程行为树中大概率逻辑不同，且需要不同的变量辅助完成，所以为避免基类变量臃肿，由子类重写实现

##### UnitAgentBase：单位AI行为树代理基类
主要实现AI行为树的轮询逻辑及重写Puase和CancelPause

##### MonsterAIAgent：野怪AI行为树代理
InitAgent：重写AgentBase，目前主要获取UnityActorCtrl和NavMeshAgent

PlayAnimation：播放动画

MoveToTarget：移动到目标点 以当前位置为起始点，innerRadius为内圈半径，outRadius为外圈半径，在圆环内找到目标点，并执行移动的逻辑

CheckMoveEnd：核对是否移动到目标点

##### BehaviacTool：通用代理
GetRandomTime : 随机时间

RandomTargetPos：随机目标点

RunLuaFuntion：lua交互的方法（热更处理用）

##### BehaviacManager:
Creat:创建实例对应的行为树，并挂在到实例身上,返回行为树插件生成的AgentId作为唯一表示

Play：播放行为树

CreatAndPlay:创建并播放，方便Lua调取

Stop：停止行为树

Pause：暂停行为树

CancelPause：取消行为树

DestroyAgent：用于实例回收时行为树销毁，同时将行为树从behaviacDic中移除

Clear：清理行为树的数据，调取单例的销毁，目前在退出抓宠情景时调用

![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1720432942935-3a4c8c32-3970-4472-ae0d-ad84fc9fdba0.png)

# 四、流程设计
![](https://cdn.nlark.com/yuque/0/2024/png/44744345/1720433920243-8cff56a3-6eea-4c42-9193-94ff8eadb3eb.png)

# 


