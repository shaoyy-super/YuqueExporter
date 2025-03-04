# 1.版本管理
[此处为语雀卡片，点击链接查看](https://www.yuque.com/cod5mf/iwqppn/xpnpy04dguvvw5sy#auzW2)

# 2.设计目的
管理游戏内的buff分类，buff效果，buff定义等各种属性的文档。包括逻辑层和表现层两部分的全部内容。



# 3.功能介绍
buff表的结构和相关规则。

buff在界面中的展示信息和效果。

## 3.0.功能详情
### 3.1名称定义
buff：代表游戏中临时存在的各种状态。区别于类似养成获得的固定属性和效果，buff在战斗过程中获得，战斗结算后清除，在战斗中临时生效的各种属性。

### 3.2buff表
3.2.1总览

每个buff在buff表中拥有一列来实现和描述该buff的具体效果，完全相同的buff根据功能不同，也需要不同的两个id单独管理，一般情况下除制作的通用buff外，不允许在不同系统之间调用同id的buff。

![](https://cdn.nlark.com/yuque/0/2024/png/43733765/1720429016873-b531a688-2c0a-4d98-97fe-cc002830f565.png)

3.2.2buff的唯一id

1-1000公用buff，策划定制的包括测试buff，buff模板，通用buff。

1000-10000通用技能buff，无需与通用技能id关联，数量级会远低于技能id

10000-99999天赋技能专用buff，尽量与技能id相关联。

功能用buff根据功能新开字段。



3.2.3技能名称

游戏中会在ui上显示的技能名称，请填写标准名称。长度根据功能自行控制。



3.2.4buff描述

需要显示在ui界面上的buff描述，支持富文本



3.2.5icon

配置图片位置



3.2.6debuff分类

buff的基础分类，确定buff的作用范围

1：buff增益效果，包括属性增加，增益状态，特殊效果等

2：debuff减益效果，包括属性降低，减益状态，特殊效果等

3：buff特殊效果，一些特殊效果，难以界定的效果状态。

一般只用来ui显示，一般不涉及计算逻辑



3.2.7buff类型

逻辑上区分不同类别的buff，在叠加和驱散时会用到

例如：同类型的buff属性叠加。攻击增加30%和攻击增加20%的buff，叠加后变为攻击增加50%效果

驱散防御降低类型的buff，那么，类型10的buff会被全部清除。



3.2.8buff参数

暂时无用，后续如果将部分参数提到配置中来可能会用



3.2.9buff目标

无用，可以去掉



3.2.10基础命中概率

技能拥有的基础命中概率，部分命中效果会被角色属性中的命中增益。配置的是技能的基础命中



3.2.11buff叠加

数值叠加计算按照数值给出的公式计算

叠加逻辑规则

![](https://cdn.nlark.com/yuque/0/2024/png/43733765/1720507252363-40b3c8c0-55ef-4094-8749-83c17189e60e.png)

1.不可叠加（分开计数）

每个buff，单独行动，同idbuff也可以单独存在并生效

灵魂燃烧，每回合扣3点血，3回合



2.普通buff（同id刷新属性，刷新时间，增加层数）

同类型不同id的buff不相互叠加，同id的buff覆盖叠加

覆盖叠加会刷新buff时间并添加层数。

如果上限为1，那么新buff会刷新效果。



3.通用计数buff（同id不刷新属性，不刷新时间，只增加层数）

只增加计数buff层数，不刷新buff时间



4.特殊效果buff（同id刷新属性，不刷新时间，增加层数）

暂时没有，需要再加



5.同id无法叠加

buff存在时，无法再次添加同id的buff。但是可以和同类型的buff共存

加速：宠物速度增加30%



6.同类覆盖叠加（刷新属性，刷新时间，叠加层数）

相同类型不同id的buff会叠加为同一个，以最后一个buff为实际效果，叠加层数

举例：

老：id1001，攻击增加30%，还剩1回合，1层。可叠加3层

新：id1002，攻击10%，持续3回合，1层。可叠加10层

叠加后，id1002，攻击10%，3回合，2层。可叠加10层

特殊类型：眩晕，1回合，会被眩晕2回合覆盖，上限层数设置为1即可。

中毒/燃烧等大类可以互相覆盖叠加的类型buff



7.同类型不可叠加

拥有此类型buff时，无法添加同类型buff。

举例：眩晕



2.2.12清除方式

可驱散：可以被其他buff清除

不可驱散：无法被其他buff移除



2.2.13叠加上限

buff可以叠的层数，必须为大于0小于99的整数



2.2.14持续回合

buff存在的时间，以回合为单位，必须为大于0小于99的整数



2.2.15effect

buff关联属性的计算，只有属性变化的buff，无需脚本



2.2.16脚本

buff如果需要实现一定的效果，使用脚本配置



2.2.17tlm

主要是挂buff特效





## 3.1.buff UI与部分规则
### 3.1.1 Buff图标规则：
3.11 普通Buff图标将由图标，底框，箭头三个部分组成：<font style="color:#DF2A3F;">（层数和回合数）（示意图）</font>

        图标：buff的图标，主要代表buff效果的图形具象化。

        底框：图标的外框与底图颜色共同组成底框，主要色彩表示buff类型。框应保持外形统一。

        箭头：buff的效果类别的图形展示，与buff具体的类型相关。

       场地buff图标将有单独的ui位置与相关设置，这里不展开场地buff的相关设计。

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721014594190-f456dbaf-f5ef-42f7-90d9-ce17b881c1f8.png)![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721025560254-17dc7b83-1134-452b-9761-f7601256ab6b.png)

3.12 buff图标是根据buff效果的图形具象化，具体图案根据不同的buff内容设计。底框主要是颜色，颜色原则上表现出相对应的buff效果类型即可，箭头则于类型强绑定。

例如：

       增益buff：绿，蓝色系图标与底框+向上箭头蓝色

       减益buff：红，紫色系图标与底框+向下箭头红色

       其他buff：白，灰色系图标与底框，无箭头

**<font style="color:#DF2A3F;">美术可以自己调整，主要成体系，能够直观表现buff效果就可以。</font>**

**<font style="color:#DF2A3F;">箭头也可设计自行决定是否保留</font>**



### 3.1.2 Buff图标显示规则：
以下规则适用于玩家宠物与敌人宠物。<font style="color:#DF2A3F;">（敌人的显示也可以标注一下）</font>

#### 3.21 BUFF获取规则
3.211 获取BUFF时：buff图标获取时，直接出现图标在宠物的血条上方左边作为起始边。血条上buff图标外框应保持相同形状。BUFF只显示一排，一排暂定最多显示5个【看具体图标大小】，当buff图标超过5个时，<font style="color:#DF2A3F;">显示箭头标记出现代表有超过显示区域的图标</font>，超过的图标不再显示。

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721013334460-53f38b9e-8e8a-48e2-9a44-9918a6849d45.png)本人宠物的显示

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721013349819-888e48a9-cd64-4be3-a6f4-ed8c42b5132f.png)敌人宠物的显示



3.212 BUFF顺序：buff显示的先后顺序由buff的类型权重与获取顺序决定，

部分buff类型在排序中具备更高权重，将会优先显示，在配置表中，他们的显示权重列填写权重数据，代表为优先显示buff。

在buff显示权重未填写数据的情况下，此类bufff按照获取顺序显示。新获取的buff图标将会从左边起始边出现，超过显示区域的buff将不会出现。

<font style="color:#DF2A3F;">优先展示buff按权重排序，其他buff按获得顺序排序</font>

 

3.213 BUFF获取跳字：buff获取时，将会从宠物身上跳出BUFF获取Tips。

获取tips将由箭头+图标+buff名称+整体tips底板四部分组成。

buff名称将会根据buff类型的不同显示不同的字体颜色

所有获取跳字出现顺序取决于buff获取的先后顺序，

跳字存在队列顺序，按前后顺序轮序出现。

每个tips的出现时间栈顶为2秒。

出现方式动画为向上淡入与原地淡出。

[此处为语雀卡片，点击链接查看](https://www.yuque.com/cod5mf/iwqppn/xpnpy04dguvvw5sy#f72jF)



#### 3.22 BUFF持续与变化规则
3.221 buff图标都保持可点击性，点击后，呼出悬停弹窗，显示buff的详情文本。目前暂定自动作战的情况下图标仍旧可以点击。

备注：自动战斗情况下，若显示浮窗时buff消失，则浮窗消失。

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1720678801654-1bbc701f-3c73-400e-b6ce-2711c7cbe228.png)

3.222 buff在持续过程中，出现变化时，会有轻微动态效果显示告知玩家buff出现变化。动态效果可以参考放大，闪烁等等。可引起动态的变化情况如下：buff得到刷新，叠加层数

[此处为语雀卡片，点击链接查看](https://www.yuque.com/cod5mf/iwqppn/xpnpy04dguvvw5sy#mlB02)

3.222 buff备时间限制-回合数：在血条上方的小型buff图标上不显示回合数，只是时间显示buff回合数到最后一个回合，则buff图标出现不断闪烁的淡入淡出，用动态表示buff即将消失。



3.223 buff具备层数概念，当buff具备多层效果时，例如2级减速。层数直接显示buff图标的右上角。



3.224 战斗场景中，玩家点击宠物的头像，则进入此宠物的相关信息界面，界面中将会展示所有存在的buff与相关的buff文本解释。

#### 3.23 BUFF移除
3.231 图标在移除时，将会以淡出的动画移除，在位置空出后，下一位buff图标晋一位移动。

#### 3.24 场景buff
3.241 场景buff的显示区域固定为场景对局时间下方，代表场景效果。场景buff图标根据buff的相关内容设计，例如火山场景buff则为红色主体色。

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1720681917161-c040dd49-bb10-4476-b442-25570b9844a2.png)

3.242 场景buff图标点击后后， 出现悬停文本框。填写buff详细文本。

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1720681927976-f305a484-adcc-441b-886f-04713273935e.png)·

3.244 场景buff不会同时存在两个，前一个场景buff会被后一个场景buff顶替。

3.245 场景buff获取时，具备特殊文本跳字与显示位置，区别于作用于每个宠物的通用buff。例如暴雨天场景buff出现时，出现文字tips：“场地暴雨生效”，同时出现场地效果。

[此处为语雀卡片，点击链接查看](https://www.yuque.com/cod5mf/iwqppn/xpnpy04dguvvw5sy#usCBD)

3.246 场景buff具备层数与回合数属性，层数同样出现在场景buff的右下标。场景buff在维持回合数即将消失时，场景buff也具备例如闪烁动画，表示buff即将消失。



#### 3.25 宠物详情界面规则
玩家可以点击宠物的名字与血条都可以打开宠物详情界面。

在详情界面上展示宠物的相关技能与当前状态信息。

![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721015137234-ea8de81d-8a62-4074-bf1d-ce1257b3e42d.png)

3.251 宠物信息的详情界面，最左侧是所有宠物列表信息，顺序固定为前三个玩家宠物，后三个敌人宠物

宠物。

3.252 点击任何宠物名称，血条等附近区域会呼出此详情界面。呼出后会默认打开点击的宠物信息。

3.252 技能区域卡牌可以手指左右滑动查看选择更多卡牌，点击一张卡牌后，下方文字区域显示详细技能效果。

卡牌默认排序前三张天赋技能，后面是通用技能

3.253 <font style="color:#DF2A3F;">效果区域可以手指左右滑动查看当前buff，</font>

<font style="color:#DF2A3F;">buff排序优先使用buff的显示权重，对于高权重的buff会排在前面显示。</font>

<font style="color:#DF2A3F;">若buff属于没有显示权重或显示权重相同时，则根据获取时间顺序显示。</font>

### 3.2.buff tml管理
属性buff/状态buff/buff跳字/护盾的处理

buff





# 4.美术需求清单（必备）
###  4.1 2D需求
| **<font style="color:white;">编号</font>** | **<font style="color:white;">分类</font>** | **<font style="color:white;">名称</font>** | **<font style="color:white;">大小-美术</font>** | **<font style="color:white;">资源命名</font>** | | **<font style="color:white;">美术需求</font>** | | | **<font style="color:white;">特殊说明（逻辑说明）-预留</font>** | **<font style="color:white;">参考</font>** | **<font style="color:white;">成品缩略图</font>** |
| :---: | --- | --- | :---: | :---: | --- | --- | --- | --- | --- | --- | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">普通buff图标</font> | / | | <font style="color:black;"></font> | | <font style="color:black;">根据buff效果与buff类型进行相对应涉及。</font> | | | 1 普通Buff图标将由图标，底框，箭头三个部分组成：图标：buff图标，代表buff效果。底框：主要色彩表示buff类型。框应保持外形统一。箭头：buff效果类别的图形展示，与buff具体的类型相关。<br/>2 buff图标用在多种界面上，大小有对应变化。<br/>3 图标因具备层数与回合数有阿拉伯数字角标。 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721021103826-4d2b84fe-4439-44cc-a32f-369f6ae479e7.png) | |
| <font style="color:black;">2</font> | 场景buff图标 | <font style="color:black;">/</font> | | <font style="color:black;"></font> | | 根据场景buff效果和类型进行对应设计。 | | | 1 场景buff图标根据buff的相关内容设计，例如火山场景buff则为红色主体色。<br/>2 场景buff整体需要外形保持一致。 |  | |


### 4.2 界面需求
<font style="color:#DF2A3F;">界面性质包括：一级界面、二级界面、弹框界面、弹出框</font>

| **<font style="color:white;">编号</font>** | **<font style="color:white;">界面</font>** | | | **<font style="color:white;">性质</font>** | | <font style="color:#FFFFFF;">界面需求</font> | <font style="color:#FFFFFF;">界面参考图</font> | <font style="color:#FFFFFF;"></font> | <font style="color:#FFFFFF;"></font> |
| :---: | --- | --- | --- | :---: | --- | --- | --- | --- | --- |
| <font style="color:black;">1</font> | <font style="color:black;">战斗场景界面-buff图标区域</font> | | | <font style="color:black;">一级界面</font> | | 战斗场景界面的一部分，在宠物血条上方存在buff图标显示当前宠物buff。 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721023362320-0cdab347-850f-4a4c-b699-4b64a897faa1.png) |  |  |
| <font style="color:black;">2</font> | 宠物战斗详情界面 | | | <font style="color:black;">二级界面</font> | | 点击宠物血条名称等区域进入宠物详情界面，显示当前我方与敌方宠物相关的技能与buff显示。 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721024675324-79c141f1-5240-4cac-a0de-7b3195218d66.png) |  |  |
| <font style="color:black;">3</font> | <font style="color:black;">buff详情小型弹窗</font> | | | <font style="color:black;">弹出框</font> | | 1 点击血条上方buff小图标弹出的文字详情弹窗<br/>2 场景buff点击图标后弹出的文本详情弹窗 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721023351086-b2d573f6-1960-49c2-94fe-9d50d09c2e6b.png) |  |  |


### 4.3 动效需求
| <font style="color:#FFFFFF;">编号</font> | <font style="color:#FFFFFF;">动效名称</font> | | | <font style="color:#FFFFFF;">动效功能需求</font> | | <font style="color:#FFFFFF;">动效美术需求</font> | <font style="color:#FFFFFF;">游戏内需要做动效的组件截图</font> | <font style="color:#FFFFFF;">参考链接</font> | |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1  | buff获取跳字 | | | 战斗场景宠物获取buff时，身上出现的buff跳字tips。 | | 1 在获取buff时，宠物身上出现的buff获取弹窗，显示基本的buff图标，名称以及对应箭头<br/>2 跳字弹窗在宠物身上指定区域从下出现（并不是界面最下方），到上方消失。 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721021885711-03e40149-6aa1-40d2-bd86-4880514c3381.png) |  | |
| 2 | 场景buff获取挑字 | | | 战斗场景获取场景buff时，界面出现的整体跳字tips<br/> | | 在获取场景buff时，窗口ui出现场景buff相关的文本弹窗tips<br/> | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721022200311-ac11ec7c-0315-4025-921a-da0ce0093041.png)  |  | |
| 3 | buff过程动效<br/> | | | buff图标在玩家战斗过程中，因叠加，刷新出现的动态变化 | | 参考类型游戏可以尝试图标放大，图标闪烁，图标出现光泽描边等效果，只需要起到提示的效果即可。 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721022768396-8bc7d7ee-0a23-491a-a799-39aead2663ae.png) |  | |
| 4 | buff即将消失动效 | | | 具备回合数的buff图标在即将消失时，会通过动效提示玩家-此效果即将消失 | | 参考类似游戏，可以尝试图标闪烁忽隐忽现等效果，只需要起到提示的效果即可。 | ![](https://cdn.nlark.com/yuque/0/2024/png/45603655/1721022768396-8bc7d7ee-0a23-491a-a799-39aead2663ae.png) |  | |
| 5 | 宠物战斗详情界面选中效果 | | | 在宠物详情界面选中技能卡牌效果，界面效果尽量通用，可找程序讨论可通用的办法 | | 点选技能卡牌后，在卡牌轮廓上存在选中的高亮效果 |  |  | |
















