**1.设计框架**

# **<font style="color:rgb(38, 38, 38);">一、概述</font>**<font style="color:rgb(38, 38, 38);">  
</font>
## **<font style="color:rgb(38, 38, 38);">1.1 文档说明</font>**
<font style="color:rgb(38, 38, 38);">  
</font><font style="color:rgb(38, 38, 38);">●</font><font style="color:rgb(38, 38, 38);">本文档详细说明了偶像养成小游戏-Vocal练习声乐课</font>

## <font style="color:rgb(38, 38, 38);"></font>**<font style="color:rgb(38, 38, 38);">1.2 文档维护</font>**
| <font style="color:white;">版本</font><font style="color:rgb(38, 38, 38);">   </font> | <font style="color:white;">时间</font><font style="color:rgb(38, 38, 38);">   </font> | | <font style="color:white;">负责人</font><font style="color:rgb(38, 38, 38);">   </font> | <font style="color:white;">修改内容</font><font style="color:rgb(38, 38, 38);">   </font> |
| --- | --- | --- | --- | --- |
| <font style="color:black;">v1.0</font><font style="color:rgb(38, 38, 38);">   </font> | <font style="color:black;">2024/09/26</font><font style="color:rgb(38, 38, 38);">   </font> | | <font style="color:rgb(38, 38, 38);">朱柯萍</font> | <font style="color:black;">文档创建</font><font style="color:rgb(38, 38, 38);">   </font> |
| <font style="color:rgb(38, 38, 38);">   </font> | <font style="color:rgb(38, 38, 38);">   </font> | | <font style="color:rgb(38, 38, 38);">   </font> | <font style="color:rgb(38, 38, 38);">   </font> |


## **<font style="color:rgb(38, 38, 38);">1.3 设计思路</font>**
<font style="color:rgb(38, 38, 38);">结合AI替换声线技术来完成Vocal声乐课小游戏，增加偶像角色的声乐属性得分</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730186902318-ec863210-c389-4eec-afba-3265e8fca1d2.png)<font style="color:rgb(38, 38, 38);">  
  
</font><font style="color:rgb(38, 38, 38);">场景内选择偶像和导师选择（剪影）——歌曲选择弹窗</font>

# **<font style="color:rgb(38, 38, 38);">二、功能说明</font>**
## **<font style="color:rgb(38, 38, 38);">2.1 AI运用</font>**
<font style="color:rgb(38, 38, 38);">运用AI音生曲技术，替换歌曲声线</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1727332979780-33ede08b-4199-444a-ac5b-08113966e837.png)<font style="color:rgb(38, 38, 38);">  
</font>

## <font style="color:rgb(38, 38, 38);"></font>**<font style="color:rgb(38, 38, 38);">2.2 小游戏规则</font>**
**<font style="color:rgb(38, 38, 38);"></font>**

<font style="color:rgb(38, 38, 38);">AI替换声线</font>

<font style="color:rgb(17, 124, 238);">歌曲原曲</font><font style="color:rgb(38, 38, 38);">：玩家需要先从曲库中选择一首需要翻唱的歌曲原曲，曲库内歌曲与AI音生曲网站内曲库一致，可直接调用</font>

<font style="color:rgb(17, 124, 238);">指导老师</font><font style="color:rgb(38, 38, 38);">：即原曲演唱者</font>

<font style="color:rgb(17, 124, 238);">演唱偶像</font><font style="color:rgb(38, 38, 38);">：玩家所选择的卡面对应的偶像角色</font>

<font style="color:rgb(17, 124, 238);">歌曲生成</font><font style="color:rgb(38, 38, 38);">：歌曲播放过程中先播放指导老师演唱片段，然后偶像跟唱，演唱结束后玩家可进行作品分享</font><font style="color:rgb(38, 38, 38);">  
</font>

# **<font style="color:rgb(38, 38, 38);">三、功能需求</font>**
从主界面小游戏入口或任务功能的专项训练可进入小游戏玩法界面

## **<font style="color:rgb(38, 38, 38);">3.1 小游戏界面</font>**
~~<font style="color:rgb(38, 38, 38);">1.看板娘</font>~~

<font style="color:rgb(38, 38, 38);">2小游戏选择（若后续需要增加游戏数量，则超出部分左右滑动）</font>

<font style="color:rgb(38, 38, 38);">3.课程奖励（需要配表控制） </font>

<font style="color:rgb(38, 38, 38);">4.开始训练按钮</font>

<font style="color:rgb(38, 38, 38);">5.返回按钮</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730173503584-99d666f6-4d0c-4c2a-b496-349e3dd17706.png)

+ <font style="color:rgb(38, 38, 38);">每日首次进行小游戏游玩免费，若重复游玩则需要花费一定金币或钻石，具体费用后续商定</font>
+ <font style="color:rgb(38, 38, 38);">奖励物品是否可重复获得需后续商定</font>

## **<font style="color:rgb(38, 38, 38);">3.2选择界面</font>**
1. **<font style="color:rgb(38, 38, 38);">选择导师</font>**
+ <font style="color:rgb(38, 38, 38);">四个导师坐在3D场景内，玩家需要选中其中吧一个导师</font>
+ <font style="color:rgb(38, 38, 38);">选中后该导师将从坐下的状态变成站立的状态</font>
+ <font style="color:rgb(38, 38, 38);">同时镜头想右侧转动，其他导师从场景内消失，被选中的导师在左侧站立，右侧显示偶像剪影</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730365090657-35282e19-9a13-42d6-845a-e13dcc0cbd39.png)<font style="color:#DF2A3F;">增加确认按钮，点击确定后镜头拉近，变为半身像</font>

<font style="color:#DF2A3F;">默认随机歌曲，点击歌曲选择可重新选择，bgm播放当前歌曲的伴奏带</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730365067789-260cbb25-6568-4500-b9f9-1a1f043b3783.png)

<font style="color:#DF2A3F;">加开始演唱按钮，去掉选择歌曲按钮</font>

2. **<font style="color:rgb(38, 38, 38);">选择歌曲</font>**
+ <font style="color:rgb(38, 38, 38);">选择歌曲弹窗内将显示上一步所选择的导师所演唱的歌曲</font>
+ <font style="color:rgb(38, 38, 38);">玩家可以选择其中一首歌曲</font>
+ <font style="color:rgb(38, 38, 38);">关闭弹窗可返回选择导师界面重新选择</font>
+ <font style="color:#DF2A3F;">左侧显示专辑封面</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730343288732-f171a194-ac2f-4424-8254-1d46f1da2f48.png)

3. **<font style="color:rgb(38, 38, 38);">偶像选择界面</font>**
+ 点击偶像剪影可打开偶像选择弹窗
+ 玩家需要选择其中一名偶像来进行训练
+ 此处显示的为偶像人物卡，与卡面服装效果无关
+ 选择完成后点击下一步按钮即可进入演唱界面

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730343719263-89da56f6-750b-442b-b3b3-f84c2ab2100b.png)



## **<font style="color:rgb(38, 38, 38);">3.4演唱界面</font>**
<font style="color:rgb(38, 38, 38);">1.返回按钮</font>

<font style="color:rgb(38, 38, 38);">2.指导老师角色显示</font>

<font style="color:rgb(38, 38, 38);">3.偶像跟唱角色显示</font>

<font style="color:rgb(38, 38, 38);">4.歌曲名称/原唱</font>

<font style="color:rgb(38, 38, 38);">5.开始演唱按钮</font>

<font style="color:rgb(38, 38, 38);">6.3D角色模型</font>

<font style="color:rgb(38, 38, 38);">7.演唱进度条——</font><font style="color:#DF2A3F;"></font>

<font style="color:#DF2A3F;"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730365109379-bfd63140-ebe7-42c4-9bbf-7aeb59fd37ac.png)

<font style="color:#DF2A3F;">去掉重选歌曲按钮</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730179571901-4c059d21-db27-4f4a-9cb4-5cbd31bb8794.png)

<font style="color:#DF2A3F;">增加tips飘字</font>

+ 点击开始演唱按钮后将进入3 2 1倒计时，倒计时结束后开始播放导师演唱部分，导师演唱完成后播放偶像演唱部分
+ 游戏过程中将播放偶像和匹配的素人拿着麦克风在舞台上进行表演的动作
+ 偶像动作与性格相关，若该偶像为文静型则动作幅度较小；若偶像为活泼型则动作幅度较大如拿着麦克风唱跳等
+ 玩家可以将演唱完成的歌曲加入曲库内

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730343057671-cf4e5389-a566-4df5-82d0-93abac12d299.png)

### <font style="color:#DF2A3F;">3.4.1. 操作说明</font>
+ 演唱过程中，屏幕中央将出现蓄力条
+ 玩家需要点击下方按钮进行蓄力，需要保证浮标在安全区域内
+ 安全区域会随着屏幕中间的音波条上下起伏，音波条随着歌曲进行将从右向左移动
+ 若浮标高于安全区域则演唱过程中将出现破音的情况
+ 若浮标低于安全区域则演唱过程中将出现走音的情况
+ 长按按钮浮标将向上，松开按钮浮标将向下
+ <font style="color:#DF2A3F;">音波条修改为单一判定颜色，不需要分段显示</font>
+ <font style="color:#DF2A3F;">在歌词出现之前，隐藏音波条，没有歌词的部分也需要隐藏音波条</font>
+ <font style="color:#DF2A3F;">音波条截断部分增加渐变遮罩，当前过于突兀</font>

### <font style="color:#DF2A3F;">3.4.2. 得分说明</font>
+ <font style="color:#DF2A3F;">在原有规则基础上增加得分获取规则</font>
+ <font style="color:#DF2A3F;">增加悬浮球外圈圆形蓄能条和蓄满后的炸开特效和音效</font>
+ <font style="color:#DF2A3F;">增加悬浮球碰到边界时的音效</font>
+ <font style="color:#DF2A3F;">蓄能条每次蓄满炸裂时得分增加100，蓄能时间大概为走过一屏幕的时间</font>
+ <font style="color:#DF2A3F;">炸裂同时蓄能显示归零，重新开始计算</font>
+ <font style="color:#DF2A3F;">悬浮球位于音波条中间没有断连时每过一个小节线即可增加10分，一屏幕大概分成四个小节线</font>



![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1731466325875-2d4192d6-e551-4d2a-8589-38e72e233d56.png)

## **<font style="color:rgb(38, 38, 38);">3.5奖励领取界面</font>**
<font style="color:rgb(38, 38, 38);">1.演唱完成得分显示</font>

<font style="color:rgb(38, 38, 38);">2.完成奖励，奖励物品为主界面所显示的奖励物品</font>

<font style="color:rgb(38, 38, 38);">3.交互按钮：</font>

<font style="color:rgb(38, 38, 38);">重新演唱：返回演唱界面</font>

<font style="color:rgb(38, 38, 38);">加入曲库：将所演唱的歌曲加入曲库内，后续可反复播放试听</font>

<font style="color:rgb(38, 38, 38);">返回主页：返回小游戏选择界面  
</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1730367215419-c9c65ab5-8f60-4f47-93ae-dcc6e70d9105.png)<font style="color:rgb(38, 38, 38);">  
</font><font style="color:#DF2A3F;">增加“加入曲库”按钮</font><font style="color:rgb(38, 38, 38);">  
</font><font style="color:#DF2A3F;">得分界面修改，参考游戏结算，左侧显示人物模型（偶像角色）</font><font style="color:rgb(38, 38, 38);">  
</font>**<font style="color:rgb(38, 38, 38);">三、配置表结构</font>**<font style="color:rgb(38, 38, 38);">  
</font>



<font style="color:rgb(38, 38, 38);">  
</font><font style="color:rgb(38, 38, 38);">  
  
  
  
  
  
  
</font>

  
 

