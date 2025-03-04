# 1.设计框架
## 一、**概述**
### 1.1 **文档说明**
<font style="color:rgb(38, 38, 38);">●</font><font style="color:rgb(38, 38, 38);">游戏的核心玩法（打歌）在进行首次设计时，考虑的是pvp模式，无卡牌养成要素。</font>

<font style="color:rgb(38, 38, 38);">在游戏引入卡牌养成要素后，和核心玩法关联的【组队】【战斗中得分表现】【战斗结算】都相应的会发生变化。</font>

### 1.2 文档维护
| <font style="color:white;">版本</font> | <font style="color:white;">时间</font> | | <font style="color:white;">负责人</font> | <font style="color:white;">修改内容</font> |
| --- | --- | --- | --- | --- |
| <font style="color:black;">v1.0</font> | <font style="color:black;">2024/10/17</font> | | 孙新元 | <font style="color:black;">文档创建</font> |
|  |  | |  |  |


### 1.3 设计思路
1，引入养成后，pve关卡通关结算主要依赖（付费+养成），次要依赖操作。

2，基于上条，单曲总分将由几个部分组成：

      **<font style="color:#AD1A2B;">服装加成（付费+收集）+特殊舞段得分（养成）+notes操作</font>**

**<font style="color:#AD1A2B;"></font>**

3，操作相关额外给奖励评级及奖励通道

4，服饰与明星卡在结算效果中的数值规划，偏向于：

            *服饰：大部分pve关卡，低难度/无排行榜追求时，几乎不影响；pvp关卡中，是底线要求。

            *服饰：配合运营需求+活动关卡中，作为排行榜用户是较强的卡点

            *服饰与养成是乘法关系。

            *服饰/养成与操作是加法关系。开服初期，两者占比55开。伴随运营，服饰/明星卡数值膨胀，服饰+明星卡占比提高。

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729501834452-4e294ad6-1c3b-41cc-85fe-8e31e6e572ab.png)

## 二、**pve结算规则，结算公式**
pve战斗结算与下列属性有关：

【服饰加成系数】

【卡牌基础属性值（即外貌，声乐，舞蹈，表演，音律】

### 2.1 **服装风格属性值与关卡的关系**
1，<font style="color:#117CEE;">【服装风格值】</font>：共5种， 典雅，清新，甜美，帅气，性感

     （服装属性值是一种整数数值）

 2，<font style="color:#117CEE;">【pve关卡服装风格值要求】</font>：每个pve关卡需配置服装风格值要求。

<font style="color:#117CEE;">      【主服装风格】【副服装风格】：</font>其中部分为【主风格】要求，其他为【副风格要求】，可能有复数个主服装风格要求。（通常主风格要求分值要求 较高）

举例：某关卡，可能的挑战服装匹配值要求是：

| <font style="color:#000000;">典雅</font> | <font style="color:#000000;">1      （副风格要求）</font> |
| --- | --- |
| <font style="color:#000000;">清新</font> | <font style="color:#000000;">340  （副风格要求）</font> |
| <font style="color:#000000;">甜美</font> | <font style="color:#c00000;">1100</font><font style="color:#000000;">（主风格要求）</font> |
| <font style="color:#000000;">帅气</font> | <font style="color:#000000;">100 （副风格要求）</font> |
| <font style="color:#000000;">性感</font> | <font style="color:#000000;">800 （副风格要求）</font> |


3，每名**主力格偶像**单独判断（全身）所穿着衣服总值是否符合风格值要求

4，当某名主力格偶像穿着的衣服满足风格值要求，则获得加成。

主服装风格通常要求值较高，加成也多，根据读表确定值

（下表假定：（偶像身着服装风格总值-管卡服装风格要求值）/关卡服装风格要求值=X）

| <font style="color:#000000;">情况</font> | <font style="color:#000000;">主服装风格加成百分比</font> | 副服装风格加成百分比 |
| --- | --- | --- |
| X<0 | 0 | 0 |
| 0<=X< 10% | 10% | 1% |
| 10%<=X<25% | 20% | 2% |
| 25%<=X<50% | 30% | 3% |
| 50%<=X | 40% | 4% |


**服饰加成值计算公式：****<font style="color:#AD1A2B;">【单个偶像服饰加成总值】=sum【主服装风格加成值+所有副服装风格加成值】</font>**

**举例：**下文左表列出一个关卡的服饰风格要求，某角色穿着情况，根据公式给出关卡的服装加成比：

下表中，服饰加成总值=3%+2%+30%+4%=39%     

| <font style="color:#000000;">风格</font> | <font style="color:#000000;"> 关卡风格需求</font> | 某上阵角色服饰值 | 加成 |
| --- | --- | --- | --- |
| <font style="color:#000000;">典雅</font> | <font style="color:#000000;">1      副风格</font> | 100 | 3% |
| <font style="color:#000000;">清新</font> | <font style="color:#000000;">100  副风格</font> | 110 | 2% |
| <font style="color:#000000;">甜美</font> | <font style="color:#c00000;">1000 </font><font style="color:#000000;">主风格</font> | 1300 | 30% |
| <font style="color:#000000;">帅气</font> | <font style="color:#000000;">100  副风格要求</font> | 200 | 4% |
| <font style="color:#000000;">性感</font> | <font style="color:#000000;">100  副风格要求</font> | 80 | 0 |




**<font style="color:#D22D8D;">需求：解决服装部件过多的卡顿问题</font>**

**<font style="color:#D22D8D;">大部件服饰，小部件服饰，妆容的数值分配问题</font>**

### 2.2 **pve关卡得分公式**
**pve关卡得分计算公式：****<font style="color:#AD1A2B;">【关卡总得分】=操作总得分+特殊舞段总得分</font>**

其中，**操作总得分计算规则详见原始传统玩法得分公式（即最高分1000000的计算方式）**

**<font style="color:#AD1A2B;">特殊舞段总得分=sum【单个特殊舞段得分】</font>**

**<font style="color:#AD1A2B;">单个特殊舞段落得分=sum【单个偶像特殊舞段得分】</font>**

**<font style="color:#AD1A2B;">单个偶像特殊舞段落得分=【该偶像服饰加成值】*【特殊舞段明星卡对应属性】</font>**

**（数值设计备注：开服特殊舞段总得分占比约50%，之后随数值膨胀逐渐提升）**

****

### 2.4 **音律倍率值**
**主力队音律值，提供额外的倍率=全队音律值总和/10000（万分比）**

**在计算notes操作得分时，将关卡得分100000 乘此倍率，得到一个新的总得分值。**

**（数值备注，该值不会发生明显的数值膨胀迭代）**

### 2.3 **pve关卡通关评级**
pve关卡根据关卡总得分，给予总评级

评级将影响最终的获得，获得包括：

【基础获得】制作人经验，金币（基础货币）

【其他掉落】：道具

评级枚举：

评级C以下视为失败，其他均视为成功。

| 评级 | 基础获得 | 道具获得 | 成就获得 | 得分比例参考 |
| --- | --- | --- | --- | --- |
| C以下 | 无 | 无 |  |  |
| C | 100% | 100% |  | 35% |
| B | 100% | 100% |  | 50% |
| A | **<font style="color:#AD1A2B;">110%</font>** | 100% |  | 70% |
| S | **<font style="color:#AD1A2B;">110%</font>** | 100% | **<font style="color:#AD1A2B;">有</font>** | 100% |


## 三、**pve打歌中表现**
### 3.1 **特殊舞段得分表现**
<font style="color:#117CEE;">【编辑器相关】</font><font style="color:#117CEE;">在歌舞中，插入与养成属性关联的编辑：</font>

**编辑段落类别：**在歌舞编辑过程中，要能标注出“notes段落”和“特殊段落”，其中“特殊段落”阶段，操作ui会隐藏。

**编辑属性结算点：**在歌舞编辑过程中，要能标注出某些“属性结算点”，并标记出使用**外貌,声乐,舞蹈,表演**哪一项进行结算

<font style="color:#74B602;">【打歌主玩法界面】</font>

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1730101975232-fc13b0c3-fcd0-4508-83eb-5a04511c4bb3.png)



<font style="color:#117CEE;">【pve得分条】</font>

如图为pve模式歌曲得分条（并非原始玩法的操作得分）

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729234880615-0f2c3419-1b87-4a56-829c-01124af92f1e.png)

1，画面上方固定显示得分条

2，得分条固定存在C,B,A,S四个档次标记位

3，得分条下方固定显示当前得分

<font style="color:#117CEE;">【歌曲时间轴】</font>

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1730102035638-08bc88f7-05e1-441b-80d4-16be4ad784c9.png)

*显示歌曲的时间进度条，彩色小三角为需要进行：外貌，声乐，舞蹈，表演的节点。其颜色和结算属性一致。

*整屏长度=4分钟，均分。

<font style="color:#117CEE;">【开场舞蹈角色展示】</font>

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1731556415077-e580a0c1-e26f-49a8-8ffe-ecd1970a448a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1731556427966-d4c77b63-7064-46ef-93a2-2b7756533df6.png)

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1731556444231-ac018307-699d-4238-998c-d6a9b2486e0f.png)

<font style="color:#117CEE;">【开场支援技能生效表现】</font>

开场时，显示支援卡技能，进场并离场（如下图）

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729235108045-8d76b357-c391-4039-8956-a4f04239ea30.png)

<font style="color:#117CEE;">【开场服装加成生效表现】</font>

开场时，显示5名主力角色的服装风格加成

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729497849627-e4bf1060-0771-45b9-ab0b-2e87f4751f56.png)

服装风格加成的情况：

<font style="color:#74B602;">【高加成/中加成/低加成/无加成】</font>

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729497922226-572010f9-3e01-43c9-870b-7e08017493dd.png)![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729497943621-9c531235-45b1-4a10-811c-b741a7481050.png)![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729497957056-69b13570-1615-4fcc-bed1-4b5ff4355962.png)![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729497971145-51939195-4e15-446a-b278-e33a907166e6.png)



<font style="color:#117CEE;">【notes操作得分表现】</font>

在达到good great，perfect等操作时，在字母标记上获得分数并跳分表现。（如下图）

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729168275396-c4bc9160-e0e2-44aa-9b8c-b9b7a8d52814.png)<font style="color:#117CEE;"></font>



<font style="color:#117CEE;">【特殊舞段得分表现】</font>

动画描述：（效果如下图）

1，在特殊舞段，notes 相关ui隐藏

2，编辑器决定该舞段需要依据明星卡哪一项属性来结算分数，在中部显示该属性图标 总得分字样。

3，主力明星卡（5张）以此播放入场动画及离场动画，伴随每一张入场时，总得分跳数字。

4，5张明星卡均跳完后，总得分放大展示并离场。

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729163779090-5bb0484c-6a44-40db-a867-b327b41e4499.png)







## 四、**pve结算**
<font style="color:#74B602;">【歌曲得分结算界面】</font>

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1730097256922-d1143f90-4330-4646-a190-a6de715662b0.png)

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1730097274882-7e973ed3-e39b-4c52-bcd3-b6784b640c2e.png)

| <font style="color:#FFFFFF;">标识区域</font> | <font style="color:#FFFFFF;">交互对象</font> | <font style="color:#FFFFFF;">功能概述</font> |
| --- | --- | --- |
| <font style="color:#DF2A3F;">1</font> | 歌曲信息 | 显示歌曲封面图，歌曲难度，歌曲名 |
| <font style="color:#DF2A3F;">2</font> | 关卡得分 | 显示当前关卡的总得分和评级 |
| <font style="color:#DF2A3F;">3</font> | 偶像得分/操作得分 | 可左右滑动切页查看 |
| <font style="color:#DF2A3F;">4</font> | 显示center角色 |  |
| <font style="color:#DF2A3F;">5</font> | 单曲排行榜 | 点击打开单曲排行榜 |
| <font style="color:#DF2A3F;">6</font> | 奖励 | 点击打开操作奖励弹窗 |
| <font style="color:#DF2A3F;">7</font> | 继续 | 点击进入经验道具结算界面 |


### 4.1 **关卡得分和评级的显示**
下图中，pve玩法过程中的得分条，和结算界面的总得分是一个对象。

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729234880615-0f2c3419-1b87-4a56-829c-01124af92f1e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1730097398227-902beafc-6bd9-4610-bd3e-ff984023f908.png)

根据上文整体结算逻辑，显示整个关卡的得分

<font style="color:#117CEE;">评级枚举： C,B,A,S</font>

<font style="color:#117CEE;">评级使用中文：：普，良，优，神</font>

（注意：关卡得分，和操作得分不是一个得分）

### 4.2 **偶像得分详情**
![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1730097495034-74276e14-0051-43ed-b827-9ce116dd6d74.png)

*从左往右，得分从高到低

*偶像明星卡头像中，包括主属性，稀有度，位置的表示

*左上角为该明星卡服饰加成数值

*正下方为该明星卡得分

*最下方为得分占比

### 4.3 **pve关卡得分排行榜**
**<font style="color:#AD1A2B;">暂不扩展，在好友系统完成后补充，由于游戏有很强的养成元素+服装的数值膨胀，所以单关排行榜将经常刷新。在整体pvp，游戏竞争强度确认后更新。</font>**

**<font style="color:#AD1A2B;"></font>**

### 4.4 **pve关卡操作成就奖励**
保留单曲完美操作成就。给与操作党少量奖励。

![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729501679770-8d664741-16c3-4b5a-89b4-29797cd9522c.png)



### 4.5 **关卡掉落详情**
![](https://cdn.nlark.com/yuque/0/2024/png/48674012/1729501303187-ef10347e-8fa2-4c86-8049-a9256c1511fd.png)

分别显示制作人经验获得

基础货币获得

道具掉落获得



<font style="color:#DF2A3F;">暂未设计，将来待补充的功能：</font>

<font style="color:#DF2A3F;">1，结算时获得偶像羁绊值（即针对偶像房间的养成）</font>

<font style="color:#DF2A3F;">2，结算时体力多倍消耗，奖励多倍领取（降肝机制）</font>

<font style="color:#DF2A3F;"></font>



