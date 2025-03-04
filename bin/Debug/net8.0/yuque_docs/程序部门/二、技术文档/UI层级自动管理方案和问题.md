不同的UI分`SortingLayerName`和`SortingOrder`，之前的管理方式是程序自己去设置UI的层级，为了保证层级正确，需要看别人配置的层级是多少，不是很方便，所以才考虑由`UIMgr`统一去管理界面的层级

# 一、方案


大致思路：`UIMgr`会根据不同的`SortingLayerName`维护当前layer的层级计数，比如打开一个UI，增加计数N，关闭一个UI，减少计数N。

N是预留两个UI之间其他可能添加的层级，一般是大于1的



这其中会遇到一些问题

# 二、需要注意的问题
### 1、维护的层级计数，不能简单的在UI打开关闭时直接自增或自减
比如以下情况：

+ A、B、C都是Default层，初始计数为0，假设计数基数为5（每次自增或自减5）
+ 依次打开A、B界面，此时 **A Order 5**，**B Order 10**，当前Default层计数为**10**
+ 然后关闭 A界面，计数直接 **-5**，Default层计数为**5**，此时再打开C界面，计数 **+5**，那么 **C Order就是 10**

就会出现 **B和C界面层级相同**，甚至还会出现C层级比打开的界面层级还底的bug



**状态：已解决，可以考虑是否有其他更好的方式**

### 2、C#代码依赖Canvas层级的地方，要监听Panel Canvas 层级变化
比如之前的`SortingLayerContoller`、`MoleEffectMask`



**状态：已解决，可以考虑是否有其他更好的方式**



### 3、一些固定的界面，需要提前设置好层级
比如C#层的弹框提示、WaitingUI



**状态：已解决**

### 4、通过Timeline创建的节点，是放在固定的UI_Effect_Panel上，程序需要处理这块逻辑，保证层级正确


<font style="color:#DF2A3F;">状态：未解决</font>

### 5、制作粒子特效的时，对应粒子特效的层级相对关系，需要去处理（暂未处理）
比如特效本身有A、B、C，层级分别是1、2、3，正确方式应该处理成 (UI Order比如是10) 11、12、13

而不是把A、B、C都设置成11



<font style="color:#DF2A3F;">状态：未解决</font>

### 6、UI的生命周期要严格按照Show和Hide去调用
一些非正常操作，UIMgr无法管理到，需要考虑层级问题，比如移到屏幕之外、代码直接设置 gameObject 显示或隐藏



<font style="color:#DF2A3F;">状态：未解决，看下是否需要规范用法</font>

### 7、一些特殊界面，可能需要根据当前打开的界面，动态的去设置层级
比如通用资源条



<font style="color:#DF2A3F;">状态：未完全解决，之前是各自界面自己处理的，可以考虑下是否有更好的方式</font>

<font style="color:#DF2A3F;"></font>

### <font style="color:#000000;">8、有些界面的节点，直接挂了Canvas并设置了LayerName和SortingOrder</font>


可以考虑是否规范一下用法

<font style="color:#DF2A3F;"></font>

### 
