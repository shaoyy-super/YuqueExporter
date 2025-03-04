# 
| **<font style="color:rgb(46,117,181);">日期</font>** | **<font style="color:rgb(46,117,181);">内容</font>** | **<font style="color:rgb(46,117,181);">撰写者</font>** |
| :---: | :---: | :---: |
| <font style="color:rgb(46,117,181);">2024/10/23</font> | <font style="color:rgb(46,117,181);">绘梦直播部分</font> | <font style="color:rgb(46,117,181);">朱梅迪</font> |
| <font style="color:rgb(46,117,181);"></font> | <font style="color:rgb(46,117,181);"></font> | <font style="color:rgb(46,117,181);"></font> |
| <font style="color:rgb(46,117,181);"></font> | <font style="color:#D22D8D;"></font> | <font style="color:rgb(46,117,181);"></font> |


<font style="color:rgb(0,0,0);">请选择【视图】-【Web版式】，并勾选导航窗格阅读</font>

<font style="color:#601BDE;">紫色字体表示需读取配置内容</font> <font style="color:#DF2A3F;"> 红色字体表示特别注意</font>  

<font style="color:#74B602;">绿色字体表示飘字及系统公告文字</font>  <font style="color:#ED740C;">橙色字体表示动画或者特效表现</font>

<font style="color:#ED740C;"></font>

# **<font style="color:#000000;">绘梦直播AI场景应用策划案</font>**
## 一、**玩法详情**
<font style="color:#DF2A3F;">       </font><u><font style="color:#DF2A3F;">注：简略版设计目的为快速接入北京ai接口，调试功能用；美观度、功能逻辑合理性、玩法乐趣等方面暂不做考虑。</font></u>

### （一）**玩法开启**
#### （1）. 开启条件
无



#### （1）. 功能入口
临时入口，放在主界面即可



### （二）**界面跳转逻辑**
#### （1）. 功能进入界面
![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1729672194369-7b552d15-9776-4774-8542-9191684eb495.png)

1. -控件名称：按钮【返回】。

    -操作效果：返回至上一界面。

    -控件状态：单一状态。



2. -控件名称：弹窗标题。

    -操作效果：无。

    -控件状态：单一状态，显示文本：绘梦直播内容选择。



<font style="color:#000000;">3.  -控件名称：</font>按钮【关闭】<font style="color:#000000;">。</font>

<font style="color:#000000;">     -操作效果：点击</font>返回至上一界面<font style="color:#000000;">。</font>

<font style="color:#000000;">     -控件状态：</font>单一状态。



4.  -控件名称：绘画内容选择。

     -操作效果：点击勾选框，勾选对应的作品内容。

     -控件状态：

        1. 问题：问题为文本显示，文本内容为：选择偶像绘画的作品内容；
        2. 按钮选项：按钮有勾选、未勾选2种状态；暂定六个选项，前五个为梳妆女生、化妆女生、跳舞女生、吃饭女生、唱歌女生，第六个为随机。

<font style="color:#000000;">     -控件规则：</font>

        1. <font style="color:#000000;">控件出现时，默认【随机】按钮为</font>勾选<font style="color:#000000;">状态；</font>
        2. <font style="color:#000000;">五种内容对应北京AI生图的正面提示词内容，调取北京ai生图时，自动补充文本内容为：一个正在xx的女生。（xx内容为玩家选择的梳头、化妆、跳舞、吃饭、唱歌）。</font>

                     ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1729589492792-6b8c9d88-c5d6-4930-9a86-26efe7dc6cc1.png)



<font style="color:#000000;">5.  -控件名称：</font>按钮【确定】<font style="color:#000000;">。</font>

<font style="color:#000000;">     -操作效果：点击</font>进入通用加载界面，按照选择的选项，调取北京ai绘图；绘图完成架即加载完成<font style="color:#000000;">。</font>

<font style="color:#000000;">     -控件状态：</font>单一状态。



<font style="color:#DF2A3F;">注：北京ai绘画中的【lora模型】暂定为自动随机选择。</font>

<font style="color:#DF2A3F;">北京ai绘画地址：</font>[Meta48 生成式 AI社区](http://124.17.0.231/#/made)

![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1729590151166-72558b22-c580-4cd2-bea2-edbf59b65424.png)![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1729590170603-fcc2a1fd-e6c6-4bb9-98d0-43f4af408bde.png)





#### （2）. 绘画完成界面
![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1729671127320-0fde2309-4ad8-4490-8d9a-8a41c961a2ba.png)

1. -控件名称：按钮【返回】。

    -操作效果：返回至功能进入界面的弹窗。

    -控件状态：单一状态。



2. -控件名称：绘画结果。

    -操作效果：无。

    -控件状态：单一状态，显示北京ai返回的绘画结果。



<font style="color:#000000;">3.  -控件名称：</font>按钮【完成】<font style="color:#000000;">。</font>

<font style="color:#000000;">     -操作效果：</font>返回至功能进入界面的弹窗<font style="color:#000000;">。</font>

<font style="color:#000000;">     -控件状态：</font>单一状态。



### **（三）配置表**
         暂无



### **（四）邮件**
         暂无



## 二、红点提示逻辑
         暂无



## 三、埋点检测需求
         暂无



## 四、美术需求
         暂无



## 五、数值需求
         暂无



## 六、功能需求清单
         暂无





# **<font style="color:#000000;">Vocal声乐练习课策划案</font>**
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

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1728902854367-a8fc37aa-b6cb-4a38-87d0-6b890306fba5.png)<font style="color:rgb(38, 38, 38);">  
  
</font>

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
<font style="color:rgb(38, 38, 38);">1.看板娘</font>

<font style="color:rgb(38, 38, 38);">2小游戏选择（若后续需要增加游戏数量，则超出部分左右滑动）</font>

<font style="color:rgb(38, 38, 38);">3.课程奖励（需要配表控制） </font>

<font style="color:rgb(38, 38, 38);">4.开始训练按钮</font>

<font style="color:rgb(38, 38, 38);">5.返回按钮</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1729589312487-26dca8b4-ad4d-40cc-b20d-474df7ba0946.png)

+ <font style="color:rgb(38, 38, 38);">每日首次进行小游戏游玩免费，若重复游玩则需要花费一定金币或钻石，具体费用后续商定</font>

## **<font style="color:rgb(38, 38, 38);">3.2选择歌曲</font>**
<font style="color:rgb(38, 38, 38);">1.歌曲列表控件：歌曲名称/歌手名称/歌曲星级</font>

<font style="color:rgb(38, 38, 38);">2歌曲封面：歌曲名称/歌手名/歌曲类型</font>

<font style="color:rgb(38, 38, 38);">3.确定按钮</font>

<font style="color:rgb(38, 38, 38);">4.歌曲搜索栏</font>

<font style="color:rgb(38, 38, 38);">5.所选择偶像名字显示</font>

<font style="color:rgb(38, 38, 38);">6.页签切换：歌曲/偶像</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1729588921496-0a3f0fd6-5cd2-4381-a06f-a6dbe638465e.png)

+ <font style="color:rgb(38, 38, 38);">玩家需要选择想要翻唱的歌曲，选择歌曲界面与游戏选歌界面一致</font>
+ <font style="color:rgb(38, 38, 38);">切换偶像页签选择上阵的偶像</font>

## **<font style="color:rgb(38, 38, 38);">3.3选择偶像</font>**
<font style="color:rgb(38, 38, 38);">1.偶像选择列表（可上下滑动查看）</font>

<font style="color:rgb(38, 38, 38);">2.返回按钮</font>

<font style="color:rgb(38, 38, 38);">3.页签（歌曲/偶像）</font>

<font style="color:rgb(38, 38, 38);">4.确定按钮</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1729588770957-6e0a0ee2-a0a6-4c86-9862-874afda32a10.png)

<font style="color:rgb(38, 38, 38);">玩家需要在这一界面选择想要哪个偶像进行翻唱歌曲</font>

<font style="color:rgb(38, 38, 38);">点击确定后可进入演唱界面</font>

## **<font style="color:rgb(38, 38, 38);">3.4演唱界面</font>**
<font style="color:rgb(38, 38, 38);">1.返回按钮</font>

<font style="color:rgb(38, 38, 38);">2.指导老师名称</font>

<font style="color:rgb(38, 38, 38);">3.偶像跟唱角色显示</font>

<font style="color:rgb(38, 38, 38);">4.歌曲名称/原唱</font>

<font style="color:rgb(38, 38, 38);">5.作品分享按钮（演唱结束后显示）</font>

<font style="color:rgb(38, 38, 38);">5.3D角色模型</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1728901888526-20480c5c-3f7b-4f9b-a715-58574c994699.png)

+ 游戏开始后将匹配一个素人偶像
+ 游戏过程中将播放偶像和匹配的素人拿着麦克风在舞台上进行表演的动作
+ 偶像动作与性格相关，若该偶像为文静型则动作幅度较小；若偶像为活泼型则动作幅度较大如拿着麦克风唱跳等
+ 表演完成后导师将给偶像进行打分，得分高者将获得胜利并显示头戴王冠的效果
+ 玩家可以将演唱完成的歌曲加入曲库内



## **<font style="color:rgb(38, 38, 38);">3.5奖励领取界面</font>**
<font style="color:rgb(38, 38, 38);">1.演唱完成标题</font>

<font style="color:rgb(38, 38, 38);">2.完成奖励，奖励物品为主界面所显示的奖励物品  
</font>

![](https://cdn.nlark.com/yuque/0/2024/png/38390214/1729678207258-411d9df1-24b3-47ac-9e7b-153c892720a7.png)<font style="color:rgb(38, 38, 38);">  
  
  
</font>**<font style="color:rgb(38, 38, 38);">三、配置表结构</font>**<font style="color:rgb(38, 38, 38);">  
</font>



<font style="color:rgb(38, 38, 38);">  
</font><font style="color:rgb(38, 38, 38);">  
  
  
  
  
  
  
</font>

  
 





1，偶像写真玩法

【前提】游戏内，3d模型休闲待机动作+空白背景渲染，由ai图生图，

【需求】生成变换动作，变换背景，维持3d游戏美术风格的写真

<font style="color:#DF2A3F;">（程序反馈换动作时，衣服会大变，建议使用lora，然后基于lora文生图，服装可大部分保持不变）</font>

【准备材料】向3d美术，索要游戏3d角色，符合游戏渲染风格的截图。



2，应用场景未知

【需求】由任意风格的图，生成目前心乐舞河已有的5种不同美术风格的人物头像or半身像



3，导师声乐课玩法

【前提】获得ai组的名人声线和袁艺其的声线，

【需求】导师领唱，偶像跟唱

（除了音乐分轨外，还需要能抓住唱句分段）



4，跑通游戏角色原创语音

【前提】寻找男女cv音源包/或使用公司的女偶像说话声源包

【需求】生成游戏角色声线



5 制作人作曲玩法

【前提】问北京能不能，先生成单曲（无人声），再生成该曲子（带人唱）

最后生成没有背景，只有人声。



6，标识出人声的发出阶段，给舞蹈编辑器用

【前提】找北京要接口



7，占星gpt的

【前提】询问是否付费，询问和外部占星公众号的区别

























  


若有收获，就点个赞吧

  
 

