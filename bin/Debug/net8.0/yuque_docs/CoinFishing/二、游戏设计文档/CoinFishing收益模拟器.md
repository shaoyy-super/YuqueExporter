# 一、模拟器介绍
根据预设好的规则生成用户，模拟游戏行为，每个用户会在游戏内自动进行投注，按所填入的参数生成投注结果，改变相应的结果数值，能从一定程度上反映出运营策略对最终用户留存、用户营收等参数的影响。

同时可以根据数整体数据曲线的过程，分析该调整的参数类型。

# 二、用户生成
赋予每个用户不同维度的标签，并模拟他们带着这些标签参与游戏。

## 2.1. 用户来源
先根据生成用户数生成用户，打下初始标签

| <font style="color:black;">用户来源</font> | <font style="color:black;">备注</font> | <font style="color:black;">数量</font> | <font style="color:black;">消费加权（万分比）</font> |
| :---: | :---: | :---: | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">来源ton</font> | <font style="color:black;">10000</font> | <font style="color:black;">10000</font> |
| <font style="color:black;">2</font> | <font style="color:black;">来源买量泛</font> | <font style="color:black;">10000</font> | <font style="color:black;">5000</font> |
| <font style="color:black;">3</font> | <font style="color:black;">来源裂变</font> | <font style="color:black;">10000</font> | <font style="color:black;">30000</font> |
| <font style="color:black;">4</font> | <font style="color:black;">来源买量精准</font> | <font style="color:black;">10000</font> | <font style="color:black;">15000</font> |
| <font style="color:black;">5</font> | <font style="color:black;">来源社交媒体</font> | <font style="color:black;">10000</font> | <font style="color:black;">15000</font> |


然后分别根据消费能力、消费心态、提现尺度、投注尺度的权重给现有用户这几个属性打上标签，

并模拟这批用户的“心情值”、“冲动值”。

## 2.2. 消费能力
生成用户自带存款，用户自身身上货币有两个，一个是美元，一个是ton(一种虚拟币），根据用户来源和权重生成用户身上的美金和ton数量。

| <font style="color:black;">用户来源</font> | <font style="color:black;">美元下限</font> | <font style="color:black;">美元上限</font> | <font style="color:black;">权重</font> |
| :---: | :---: | :---: | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">1000</font> | <font style="color:black;">3000</font> | <font style="color:black;">4000</font> |
| <font style="color:black;">1</font> | <font style="color:black;">5000</font> | <font style="color:black;">20000</font> | <font style="color:black;">2000</font> |
| <font style="color:black;">1</font> | <font style="color:black;">50000</font> | <font style="color:black;">200000</font> | <font style="color:black;">500</font> |
| <font style="color:black;">1</font> | <font style="color:black;">500000</font> | <font style="color:black;">2000000</font> | <font style="color:black;">120</font> |
| <font style="color:black;">2</font> | <font style="color:black;">200</font> | <font style="color:black;">500</font> | <font style="color:black;">10000</font> |
| <font style="color:black;">2</font> | <font style="color:black;">1000</font> | <font style="color:black;">3000</font> | <font style="color:black;">10000</font> |
| <font style="color:black;">2</font> | <font style="color:black;">5000</font> | <font style="color:black;">20000</font> | <font style="color:black;">2000</font> |
| <font style="color:black;">2</font> | <font style="color:black;">50000</font> | <font style="color:black;">200000</font> | <font style="color:black;">400</font> |
| <font style="color:black;">2</font> | <font style="color:black;">500000</font> | <font style="color:black;">2000000</font> | <font style="color:black;">80</font> |
| <font style="color:black;">3</font> | <font style="color:black;">1000</font> | <font style="color:black;">3000</font> | <font style="color:black;">10000</font> |
| <font style="color:black;">3</font> | <font style="color:black;">5000</font> | <font style="color:black;">20000</font> | <font style="color:black;">5000</font> |
| <font style="color:black;">3</font> | <font style="color:black;">50000</font> | <font style="color:black;">200000</font> | <font style="color:black;">2000</font> |
| <font style="color:black;">3</font> | <font style="color:black;">500000</font> | <font style="color:black;">2000000</font> | <font style="color:black;">800</font> |
| <font style="color:black;">3</font> | <font style="color:black;">5000000</font> | <font style="color:black;">20000000</font> | <font style="color:black;">200</font> |
| <font style="color:black;">4</font> | <font style="color:black;">1000</font> | <font style="color:black;">3000</font> | <font style="color:black;">10000</font> |
| <font style="color:black;">4</font> | <font style="color:black;">5000</font> | <font style="color:black;">20000</font> | <font style="color:black;">2000</font> |
| <font style="color:black;">4</font> | <font style="color:black;">50000</font> | <font style="color:black;">200000</font> | <font style="color:black;">400</font> |
| <font style="color:black;">4</font> | <font style="color:black;">500000</font> | <font style="color:black;">2000000</font> | <font style="color:black;">80</font> |
| <font style="color:black;">4</font> | <font style="color:black;">5000000</font> | <font style="color:black;">20000000</font> | <font style="color:black;">16</font> |
| <font style="color:black;">5</font> | <font style="color:black;">1000</font> | <font style="color:black;">3000</font> | <font style="color:black;">10000</font> |
| <font style="color:black;">5</font> | <font style="color:black;">5000</font> | <font style="color:black;">20000</font> | <font style="color:black;">2000</font> |
| <font style="color:black;">5</font> | <font style="color:black;">50000</font> | <font style="color:black;">200000</font> | <font style="color:black;">400</font> |
| <font style="color:black;">5</font> | <font style="color:black;">500000</font> | <font style="color:black;">2000000</font> | <font style="color:black;">80</font> |
| <font style="color:black;">5</font> | <font style="color:black;">5000000</font> | <font style="color:black;">20000000</font> | <font style="color:black;">16</font> |


## 2.3. 消费策略
生成每个用户的消费策略。

> 举例：如果一个用户通过充值或者礼金获得了100万金币，但是投入游戏后1金币都没有产出过（流水），任何玩家都不会再去充值了
>

| <font style="color:black;">用户类型</font> | <font style="color:black;">备注</font> | <font style="color:black;">首次消费</font><br/><font style="color:black;">（万分比）</font> | <font style="color:black;">静态心情值</font> | <font style="color:black;">心情值续费影响参数</font> | <font style="color:black;">冲动值影响参数</font> | <font style="color:black;">权重</font> |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">谨慎</font> | <font style="color:black;">50</font> | <font style="color:black;">5000</font> | <font style="color:black;">8000</font> | <font style="color:black;">3000</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">2</font> | <font style="color:black;">常规</font> | <font style="color:black;">100</font> | <font style="color:black;">5000</font> | <font style="color:black;">12000</font> | <font style="color:black;">5000</font> | <font style="color:black;">2000</font> |
| <font style="color:black;">3</font> | <font style="color:black;">冲动</font> | <font style="color:black;">200</font> | <font style="color:black;">5000</font> | <font style="color:black;">20000</font> | <font style="color:black;">8000</font> | <font style="color:black;">1000</font> |


首次消费是首次消费的比例。

续费参数是续费的时候相比上一次提升多少倍的首次充值，这个值会根据当前心情值和这个参数计算。

静态心情值是初始的心情值，以及不等于静态心情值后每天自然恢复或降低，最后变成静态心情值。

续费判断心跳，用户没有卸载游戏半流失状态下，多久判断一次是否续费继续玩游戏。

心情值续费影响参数，影响心情值效能计算曲线，可以得出玩家续费概率，休息概率和卸载概率。

冲动值影响参数和冲动值，和续费参数共同影响续费时的提升值。

<font style="color:black;">根据权重给现有的用户附加消费策略属性，当玩家破产时会根据消费策略属性，有的玩家继续充值，有的玩家停留或流失。</font>

## <font style="color:black;">2.4. 投注尺度</font>
然后生成每个用户的投注尺：

投注尺度代表用户每次投注会投注自由资金的百分比，投注目标百分之xx为boss，boss检索对应场次的鱼分，每次投注根据比例随机出是投注boss还是普通鱼。

| <font style="color:black;">投注尺度</font> | <font style="color:black;">单次投注比例</font> | <font style="color:black;">攻击boss比例加权</font> | <font style="color:black;">权重</font> |
| :---: | :---: | :---: | :---: |
| <font style="color:black;">低</font> | <font style="color:black;">0.05%</font> | <font style="color:black;">1</font> | <font style="color:black;">100</font> |
| <font style="color:black;">中</font> | <font style="color:black;">0.20%</font> | <font style="color:black;">2</font> | <font style="color:black;">100</font> |
| <font style="color:black;">高</font> | <font style="color:black;">0.60%</font> | <font style="color:black;">5</font> | <font style="color:black;">100</font> |


具体模拟时根据用户持有的剩余资金，以及投注尺度属性进行投注，随机投注boss或者精英，根据用户投注金额检索对应的渔场、以及鱼分计算概率进行一次投注，生成结果进行用户数值的改变。

## 2.5. 提现尺度
然后生成每个用户的提现尺度：

提现尺度代表玩家在什么条件下触发提现，当账户资金超过充值资金的几倍时，用户会触发提现。

提现会有两种行为：一种是提现继续玩，一种是提现跑路，体现跑路之后用户就全部提现不玩了。

| <font style="color:black;">提现尺度</font> | <font style="color:black;">几倍提现</font> | <font style="color:black;">提现跑路概率</font> | <font style="color:black;">权重</font> |
| :---: | :---: | :---: | :---: |
| <font style="color:black;">保守</font> | <font style="color:black;">2</font> | <font style="color:black;">50%</font> | <font style="color:black;">50</font> |
| <font style="color:black;">常规</font> | <font style="color:black;">4</font> | <font style="color:black;">20%</font> | <font style="color:black;">35</font> |
| <font style="color:black;">贪婪</font> | <font style="color:black;">10</font> | <font style="color:black;">5%</font> | <font style="color:black;">15</font> |


## 2.6. 心情值
**玩家首次开始游戏读取初始心情值，并载入为当前玩家心情值。**

读取玩家本轮初始资金，当用户资金低于这个值百分之xx(读取配置）每次攻击失败心情扣除xx万分比，具体数值读取每次攻击失败心情值降低。

玩家每次击中目标获得心情值，获得的心情值=击中倍率对应心情值参数*（鱼分/10）^1.5*（if扭亏为盈，1+扭亏为盈心情值加成/10000，1）。

玩家每天过天的时候会change一次心情值，change值=（静态值-当前值）*每天心情值变化参数/10000

玩家首次充值时如果时配额充值，玩家获得配额ton/充值美金*配额充值增加心情值参数的心情值。

| <font style="color:black;">鱼分</font> | <font style="color:black;">心情值</font> |
| --- | --- |
| <font style="color:black;">500</font> | <font style="color:black;">353.5533906</font> |
| <font style="color:black;">450</font> | <font style="color:black;">301.869177</font> |
| <font style="color:black;">400</font> | <font style="color:black;">252.9822128</font> |
| <font style="color:black;">350</font> | <font style="color:black;">207.0627924</font> |
| <font style="color:black;">300</font> | <font style="color:black;">164.3167673</font> |
| <font style="color:black;">250</font> | <font style="color:black;">125</font> |
| <font style="color:black;">200</font> | <font style="color:black;">89.4427191</font> |
| <font style="color:black;">150</font> | <font style="color:black;">58.09475019</font> |
| <font style="color:black;">100</font> | <font style="color:black;">31.6227766</font> |
| <font style="color:black;">50</font> | <font style="color:black;">11.18033989</font> |


| 行为 | 数值 |
| --- | --- |
| <font style="color:black;">低于初始资金比例开始降低心情值</font> | <font style="color:black;">8000</font> |
| <font style="color:black;">每次攻击失败心情值降低（万分比）</font> | 10（万分比） |
| <font style="color:black;">击中倍率对应心情值参数</font> | 50 |
| <font style="color:black;">扭亏为盈心情值加成（万分比）</font> | 5000）万分比 |
| <font style="color:black;">每天心情值变化参数</font> | 1800 |


## 2.7.留存推送
推送以心情值为线，以用户心情值从大于这个值降低到少于这个值为线触发

赠送金钱=玩家当前充值轮次初始金币的倍率

| <font style="color:black;">推送尺度</font> | <font style="color:black;">推送心情值</font> | <font style="color:black;">推送概率</font> | <font style="color:black;">赠送金钱基于初始金额倍率</font> | <font style="color:black;">单次储值对应轮次最大次数</font> |
| :---: | :---: | :---: | :---: | :---: |
| <font style="color:black;">保守</font> | <font style="color:black;">2500</font> | <font style="color:black;">30%</font> | <font style="color:black;">20%</font> | <font style="color:black;">1</font> |
| <font style="color:black;">中等</font> | <font style="color:black;">3000</font> | <font style="color:black;">40%</font> | <font style="color:black;">25%</font> | <font style="color:black;">1</font> |
| <font style="color:black;">激进</font> | <font style="color:black;">3500</font> | <font style="color:black;">50%</font> | <font style="color:black;">30%</font> | <font style="color:black;">2</font> |
| <font style="color:black;">超激进</font> | <font style="color:black;">4000</font> | <font style="color:black;">80%</font> | <font style="color:black;">40%</font> | <font style="color:black;">4</font> |


## 2.8. 冲动值
玩家初始保持一个冲动值，每次判断玩家冲动后对这个值进行改变，例如破产、充值、提现。

心跳心情是判断玩家破产后，玩家是暂时流失还是会继续游戏，暂时流失后会在一定时间内再次返回游戏。

当玩家的资金输到0，会进入续费和流失的判断

续费概率=（K/(k+1))*（心情值续费影响参数/1000）

如果玩家不续费则流失进入流失列表后续不会回到游戏中

如果玩家续费那么续费美金=上次充值美金*（1+(冲动值影响参数/10000)*冲动值/（冲动值+10000）

| 行为 | 数值 |
| --- | --- |
| <font style="color:black;">冲动值change参数</font> | 5000 |
| <font style="color:black;">每次提现冲动值衰减</font> | 5000 |
| <font style="color:black;">每天过天冲动值衰减</font> | 9000 |
| <font style="color:black;">心跳判断点心情值时间影响参数</font> | 500 |
| <font style="color:black;">心跳心情继续玩参数</font> | 10000 |
| <font style="color:black;">休息状态回来玩参数</font> | 10000 |




# 三、参数调节
## 3.1. 运营可调节参数
在用户生成后，可以调节配置游戏内各种参数，模拟用户进入游戏后的体验流程，并统计数据最终输出结果。

### 3.1.1. 每次模拟时各用户数量
| <font style="color:black;">用户类型</font> | <font style="color:black;">用户数量</font> |
| :---: | :---: |
| <font style="color:black;">参与分配用户数</font> | <font style="color:black;">100000</font> |
| <font style="color:black;">高活分配用户</font> | <font style="color:black;">20000</font> |
| <font style="color:black;">低活分配用户</font> | <font style="color:black;">80000</font> |
| <font style="color:black;">未参与分配赌博目标用户</font> | <font style="color:black;">20000</font> |


### 3.1.2. 生成运营基础参数
| 行为 | 数值 |
| --- | --- |
| <font style="color:black;">1治理币兑换金币</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">1治理币价值</font> | <font style="color:black;">10美分（暂定）</font> |
| <font style="color:black;">1美元兑换金币数量</font> | <font style="color:black;">10000</font> |
| 多少金币兑换1美元 | 10050 |
| <font style="color:black;">总共投放治理币</font> | <font style="color:black;">5000000</font> |


### <font style="color:black;">3.1.3. 治理币分配</font>
| <font style="color:black;">用户属性</font> | <font style="color:black;">加权</font> |
| :---: | :---: |
| <font style="color:black;">高活</font> | <font style="color:black;">5</font> |
| <font style="color:black;">低活</font> | <font style="color:black;">1</font> |


### 3.1.4. 充值时Buff参数
定义：Buff是一个面向研发和运营、玩家不可见，调控玩家实际游戏体验的工具，反映到游戏中是调控玩家当前捕鱼时在一定金币数量内命中的概率。

Buff倍率为此次获得的buff金币数量为充值的x倍；金额参数为这次获得金币倍率。

| <font style="color:black;">行为</font> | <font style="color:black;">BUFF倍率参数</font> | <font style="color:black;">BUFF金额参数</font> |
| :---: | :---: | :---: |
| <font style="color:black;">美元充值</font> | <font style="color:black;">0.1</font> | <font style="color:black;">1</font> |
| <font style="color:black;">治理比充值</font> | <font style="color:black;">-0.2</font> | <font style="color:black;">1.5</font> |


### 3.1.5. Buff触发时机和参数
除了上述充值行为外，游戏中实际可配置Buff获得的时机还有很多：

①登录时获取Buff（适用于所有玩家）

②充值或购买个礼包时获得Buff（提高充值玩家的短期体验）

③区间内损失获取Buff（适用于新手保护期或充值后损失保护）

④Buff结束时（适用于挽回用户负面体验或略微打压充值后用户）

⑤领取奖励时触发（打压免费玩家）

⑥升级炮台倍率后触发（鼓励玩家用更高炮倍进行游戏）

⑦Boss鱼被击杀后（鼓励玩家打指定鱼种）

⑧根据当前资金总量触发（鼓励玩家使用高炮倍）

⑨Vip等级提升时触发（鼓励玩家充值）

对于Buff具体数值可以配置：

①Buff的生效金币数

决定当前Buff给到玩家身上后，玩家会额外获得/亏损多少金币，

②Buff对命中概率的影响

配置一个正数概率，例如20%，即在当前Buff生效时，给玩家增加20%命中率；

反之配置一个负数概率即降低玩家的命中率。

表现为玩家会在Buff生效期间额外命中一条鱼，或者是本来可以命中实际未命中一条鱼。对应鱼分则在上述金币数内扣除。

③Buff的生效时间

决定当前Buff的保质期，让玩家不能去无限叠加Buff养号，避免工作室行为。

### 3.1.6. 各渔场相关参数
| <font style="color:black;">鱼场等级</font> | <font style="color:black;">单次最小投注</font> | <font style="color:black;">平均抽水率</font> | <font style="color:black;">1分钟最小投注（美金）</font> | <font style="color:black;">平均鱼分（普)</font> | <font style="color:black;">平均鱼分（boss)</font> |
| :---: | :---: | :---: | :---: | --- | --- |
| <font style="color:black;">1</font> | <font style="color:black;">10</font> | <font style="color:black;">5%</font> | <font style="color:black;">0.12</font> | <font style="color:black;">3</font> | <font style="color:black;">100</font> |
| <font style="color:black;">2</font> | <font style="color:black;">200</font> | <font style="color:black;">8%</font> | <font style="color:black;">2.4</font> | <font style="color:black;">4</font> | <font style="color:black;">200</font> |
| <font style="color:black;">3</font> | <font style="color:black;">5000</font> | <font style="color:black;">12%</font> | <font style="color:black;">60</font> | <font style="color:black;">6</font> | <font style="color:black;">500</font> |
| <font style="color:black;">4</font> | <font style="color:black;">100000</font> | <font style="color:black;">16%</font> | <font style="color:black;">1200</font> | <font style="color:black;">10</font> | <font style="color:black;">1000</font> |




### 3.1.7. 玩家行为相关参数
| 行为 | 数值 |
| --- | --- |
| 单日游戏时长 | 7200（秒） |
| 充值行为增加游戏时长 | 1800（秒） |
| 投注状态时间占比 | 50% |
| 每秒投注次数 | 4 |
| 总模拟天数 | 50 |


### 3.1.8. 命中率公式内参数
| 公式 | 参数范围 |
| --- | --- |
| 保底参数 | 0~10000（万分比） |
| 上限参数 | 大于保底参数，且和保底参数相加小于20000，推荐相加=20000（万分比） |
| 营收曲线参数 | -30000（万分比） |
| 运营全局额外抽水率 | 0~10000（万分比）<br/>推荐0~1000 |




### 3.1.9. 提现
| 行为 | 数值 |
| --- | --- |
| 1美金配额的ton币 | 7200（秒） |
| 配额充值所增加的心情值 | 5% |




## 3.2. 模拟计算
**鱼的概率计算公式**

:::danger
击中概率 =（1-抽水率）*（保底参数+（上限参数-保底参数）/（1+e^(营收曲线参数*(所有玩家消耗/所有玩家获取-1））*（1-运营全局额外抽水）*（1+BUFF倍率）/鱼分

:::

:::danger
击中收益 = 单次投注金额*鱼分

:::

（保底参数+（上限参数-保底参数）/（1+e^(营收曲线参数*(玩家消耗/玩家获取-1））为全局营收平衡抽水参数

运营可在工具上进行调整，并且会提供当下设置的参数，当全局玩家收益=玩家消耗时的额外抽水率，以及当额外抽税率为0就是玩家概率正常稳定态下，玩家消耗/玩家获取的值。



<details class="lake-collapse"><summary id="u6eb1d4b8"><strong><span class="ne-text" style="font-size: 16px">用户行为：	</span></strong><span class="ne-text" style="font-size: 16px">															</span></summary><p id="u84b00302" class="ne-p"><span class="ne-text" style="color: #DF2A3F; font-size: 16px">充值：首先行为是充值，用户账户金币为0触发充值行为，有治理币则充值所有治理币，没有治理币则进行美元充值。</span></p><p id="u97baebf6" class="ne-p"><span class="ne-text" style="font-size: 16px">										</span></p><p id="u1d30777f" class="ne-p"><span class="ne-text" style="font-size: 16px">美元充值会根据用户消费能力和消费心态计算获得								</span></p><p id="u049caac3" class="ne-p"><span class="ne-text" style="font-size: 16px">单次充值等于消费心态对应的消费比例*可支配资产								</span></p><p id="u0fb57b7a" class="ne-p"><span class="ne-text" style="font-size: 16px">首次充值没有条件，后续游戏内破产触发充值会根据用户消费心态判断流失还是充值具体规则破产阶段讲解								</span></p><p id="u537db409" class="ne-p"><span class="ne-text" style="font-size: 16px">充值金额时根据用户初始的可支配资产计算的也就是说他作为一个赌徒，并不会因为输的快破产了就一次只充几十美金（因为也没法翻本这样）								</span></p><p id="uea1d98cc" class="ne-p"><span class="ne-text" style="font-size: 16px">充值的美元的时候会扣除玩家的现有资产，如果扣光则玩家彻底破产流失				</span></p><p id="u758e5001" class="ne-p"><span class="ne-text" style="font-size: 16px">充值会给用户附加BUFF，降低和提高概率，BUFF上有值的参数，当玩家带着BUFF投注时，每次投注会根据BUFF修正概率，同时会给BUFF剩余影响金额扣除对应的投注值									</span></p><p id="u97b05672" class="ne-p"><span class="ne-text" style="color: #DF2A3F; font-size: 16px">依次模拟玩家每天的游戏行为</span><span class="ne-text" style="font-size: 16px">								</span></p><p id="ubdd62e83" class="ne-p"><span class="ne-text" style="font-size: 16px">投注：玩家进入游戏开始投注，根据持有筹码和投注尺度计算出单次投注金额（这个投注金额今天不会因为亏损而变低，但是会因为盈利而提高）								</span></p><p id="udfcb7bbc" class="ne-p"><span class="ne-text" style="font-size: 16px">根据投注金额检索用户所处渔场								</span></p><p id="ua5a0cf81" class="ne-p"><span class="ne-text" style="font-size: 16px">计算用户这次投注攻击的是普通鱼还是boss鱼，默认权重1比1，根据投注尺度和所处场次对boss鱼的被攻击权重做加权								</span></p><p id="u06778f37" class="ne-p"><span class="ne-text" style="font-size: 16px">根据攻击鱼的种类检索出这次攻击目标的平均鱼分								</span></p><p id="u7ce6c263" class="ne-p"><span class="ne-text" style="font-size: 16px">根据鱼分和各个抽水参数计算出玩家玩家本次攻击命中的概率，然后判断是否命中			</span></p><p id="u60151818" class="ne-p"><span class="ne-text" style="font-size: 16px">命中则给与玩家鱼分*投注金额的金币								</span></p><p id="u7cc37f22" class="ne-p"><span class="ne-text" style="font-size: 16px">根据结果change玩家持有的金币								</span></p><p id="u92fe42c2" class="ne-p"><span class="ne-text" style="font-size: 16px">记录玩家的当日剩余游戏时长（可以根据当日投注次数计算）有配置玩家的平均游戏时长，投注频率								</span></p><p id="u4848f532" class="ne-p"><span class="ne-text" style="font-size: 16px">当玩家持有的金币不够一次投注触发破产，破产会进入流失判断								</span></p><p id="udc9cee0b" class="ne-p"><span class="ne-text" style="font-size: 16px">流失判断：将玩家上次充值到破产阶段所有游戏内投注获得的流水金币/他的上次充值金额得出参数K								</span></p><p id="u721b4c66" class="ne-p"><span class="ne-text" style="font-size: 16px">检索出玩家消费心态对应的的充值条件*（1-累计提现金额/（累计提现金额+累计充值金额））得出参数M								</span></p><p id="u49729a9f" class="ne-p"><span class="ne-text" style="font-size: 16px">如果k&gt;=m则玩家继续充值，获得金币扣除玩家剩余资产，公司获得对应充值收入,玩家游戏剩余游戏时间恢复到7200（赌博游戏的常态进入翻本状态不会立马停止游戏的）				</span></p><p id="ud6798fae" class="ne-p"><span class="ne-text" style="font-size: 16px">如果k&lt;=m则玩家进入流失状态，流失状态的玩家在当前版本的模拟中就不会在游戏了，未来优化版本可以增加唤醒几率和活动送治理比唤醒的模拟，可以用一个流失列表把这些用户归类管理起来</span></p><p id="u0690eda8" class="ne-p"><span class="ne-text" style="font-size: 16px"></span></p><p id="u4ee672eb" class="ne-p"><span class="ne-text" style="font-size: 16px">提现：当玩家持有的金币大于其提现尺度对应的金额是进入提现行为状态					</span></p><p id="uff5730d4" class="ne-p"><span class="ne-text" style="font-size: 16px">根据提现尺度检索玩家提现跑路的概率，判断玩家这次提现是提现跑路还是提现留本金继续		</span></p><p id="u5e8558f2" class="ne-p"><span class="ne-text" style="font-size: 16px">提现跑路玩家提现金额是所有金币提现，根据玩家我们的提现比例玩家获得美元资产增加对应美元，公司资金减少xx美元/								</span></p><p id="u51d936a9" class="ne-p"><span class="ne-text" style="font-size: 16px">提现继续玩，玩家会留下本金其他提现，提现流程一样，本金会留在里面继续玩，游戏时间不恢复。</span></p><p id="uf1d0ed39" class="ne-p"><span class="ne-text" style="font-size: 16px">			</span></p><p id="u753f81ed" class="ne-p"><span class="ne-text" style="font-size: 16px">游戏时间：玩家剩余游戏归0当天游戏结束。								</span></p><p id="u6f1cd4da" class="ne-p"><span class="ne-text" style="font-size: 16px">新的一天游戏模拟开始需要清除全局营收平衡参数中的玩家消耗和玩家获取								</span></p><p id="ua5cd2e1f" class="ne-p"><span class="ne-text" style="font-size: 16px">根据运营选择的模拟天数，依次每天模拟玩家的游戏行为								</span></p><p id="ubbc57bbe" class="ne-p"><span class="ne-text" style="font-size: 16px"></span></p></details>
# 四、输出结果
目前输出简单结果，包含：

1、提现后跑路流失的玩家数量

2、因破产没续充的玩家流失的数量

3、资产全部充值的玩家数量

4、因心跳心情而暂时休息的玩家数量

5、模拟天数截止后，仍然留存的玩家数量

6、官方总充值收入是多少（美元）

7、玩家一共提现赚取的的收入是多少（美元）

8、官方净盈利数量（美元）

9、百分之xx的玩家赚到钱

10、百分之xx的玩家亏损

11、百分之xx的玩家累计赚到超过他单次充值5倍以上的钱

12、有多少持有治理币进来玩的玩家在没有充值的情况下赚到钱提现过

13、游戏内剩余金币总和和其对应的美元价值



后续会通过千人千面去追踪并且验证用户行为参数，持续完善行为参数和结果参数。



# 五、模拟器后续拓展方向
## 5.1. 真实游戏内数据模拟
 目前使用纯数学建模的形式模拟器，还存在部分调试不方便，数据和实际配置存在差异，模拟器计算卡顿等问题。

当用户参数、运营可调节参数持续增加扩展时，数学建模出的数字可能会失真，且在这种情况下排查bug的难度高。

计划把这套模拟器改为和实际游戏接轨，解决上述问题。

### 5.1.1. 数据精细化
模拟数据和游戏内接轨后，运营可调控的各数据基于游戏内实际配置表，与架空的填数据差异小、且数据更精细，可以针对细节调整。

例1：运营可以控制渔场内出现的每一条鱼的鱼分、捕获概率。（标绿为暂未制作的鱼，也可以填好预期的鱼分并配置）

| **<font style="color:black;">魚種類ID</font>** | **<font style="color:black;">魚名稱</font>** | **<font style="color:black;">對應魚分</font>** | **<font style="color:black;">魚分權重</font>** | **<font style="color:black;">概率   </font>****<font style="color:black;">十萬分比</font>** |
| :---: | :---: | :---: | :---: | :---: |
| <font style="color:black;">Fish_1001</font> | <font style="color:black;">小金魚</font> | <font style="color:black;">2</font> | <font style="color:black;">1</font> | <font style="color:black;">43480</font> |
| <font style="color:black;">Fish_1002</font> | <font style="color:black;">小藍魚</font> | <font style="color:black;">2</font> | <font style="color:black;">1</font> | <font style="color:black;">43480</font> |
| <font style="color:black;">Fish_1003</font> | <font style="color:black;">鱟</font> | <font style="color:black;">2</font> | <font style="color:black;">1</font> | <font style="color:black;">43480</font> |
| <font style="color:black;">Fish_1004</font> | <font style="color:black;">鸚鵡螺</font> | <font style="color:black;">2</font> | <font style="color:black;">1</font> | <font style="color:black;">43480</font> |
| <font style="color:black;">Fish_1005</font> | <font style="color:black;">熱帶魚</font> | <font style="color:black;">2</font> | <font style="color:black;">1</font> | <font style="color:black;">43480</font> |
| <font style="color:black;">Fish_1006</font> | <font style="color:black;">小丑魚</font> | <font style="color:black;">2</font> | <font style="color:black;">1</font> | <font style="color:black;">8700</font> |
| <font style="color:black;">Fish_1007</font> | <font style="color:black;">刺鱗魚</font> | <font style="color:black;">10</font> | <font style="color:black;">1</font> | <font style="color:black;">8700</font> |
| <font style="color:black;">Fish_1008</font> | <font style="color:black;">黑鰭金梭魚</font> | <font style="color:black;">10</font> | <font style="color:black;">1</font> | <font style="color:black;">8700</font> |
| <font style="color:black;">Fish_1009</font> | <font style="color:black;">梭子蟹</font> | <font style="color:black;">12</font> | <font style="color:black;">1</font> | <font style="color:black;">7250</font> |
| <font style="color:black;">Fish_1010</font> | <font style="color:black;">河豚魚</font> | <font style="color:black;">15</font> | <font style="color:black;">1</font> | <font style="color:black;">5660</font> |
| <font style="color:black;">Fish_1011</font> | <font style="color:black;">短鼻海馬</font> | <font style="color:black;">15</font> | <font style="color:black;">1</font> | <font style="color:black;">5660</font> |
| <font style="color:black;">Fish_1012</font> | <font style="color:black;">燈籠魚</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1013</font> | <font style="color:black;">水母</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1014</font> | <font style="color:black;">獅子魚</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1015</font> | <font style="color:black;">裙擺魚</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1016</font> | <font style="color:black;">蝴蝶魚</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1017</font> | <font style="color:black;">龍蝦</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1018</font> | <font style="color:black;">小海龜</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1019</font> | <font style="color:black;">烏賊魚</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1020_01</font> | <font style="color:black;">海星</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_1020_02</font> | <font style="color:black;">變異海星</font> | <font style="color:black;">6</font> | <font style="color:black;">1</font> | <font style="color:black;">14490</font> |
| <font style="color:black;">Fish_2001</font> | <font style="color:black;">熔岩龜</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_2002</font> | <font style="color:black;">鸚嘴魚</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2003</font> | <font style="color:black;">魔鬼魚</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_2004</font> | <font style="color:black;">虎鯨</font> | <font style="color:black;">25</font> | <font style="color:black;">1</font> | <font style="color:black;">3660</font> |
| <font style="color:black;">Fish_2005</font> | <font style="color:black;">鯨魚</font> | <font style="color:black;">30</font> | <font style="color:black;">1</font> | <font style="color:black;">3330</font> |
| <font style="color:black;">Fish_2006</font> | <font style="color:black;">錘頭鯊</font> | <font style="color:black;">35</font> | <font style="color:black;">1</font> | <font style="color:black;">2220</font> |
| <font style="color:black;">Fish_2007</font> | <font style="color:black;">八爪魚</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_2008</font> | <font style="color:black;">魔法水母</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2009</font> | <font style="color:black;">斑狀電鱔</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2010</font> | <font style="color:black;">冰鰩</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2011</font> | <font style="color:black;">旗魚</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2012</font> | <font style="color:black;">蟾蜍</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2013</font> | <font style="color:black;">黃金蟾蜍</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2014</font> | <font style="color:black;">黃金魔鬼魚</font> | <font style="color:black;">100</font> | <font style="color:black;">1</font> | <font style="color:black;">1430</font> |
| <font style="color:black;">Fish_2015</font> | <font style="color:black;">黃金虎鯨</font> | <font style="color:black;">150</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2016</font> | <font style="color:black;">黃金鯨魚</font> | <font style="color:black;">200</font> | <font style="color:black;">1</font> | <font style="color:black;">830</font> |
| <font style="color:black;">Fish_2017</font> | <font style="color:black;">黃金錘頭鯊</font> | <font style="color:black;">250</font> | <font style="color:black;">1</font> | <font style="color:black;">670</font> |
| <font style="color:black;">Fish_2018</font> | <font style="color:black;">黃金海龜</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_2019</font> | <font style="color:black;">黃金水母</font> | <font style="color:black;">100</font> | <font style="color:black;">1</font> | <font style="color:black;">1430</font> |
| <font style="color:black;">Fish_2020</font> | <font style="color:black;">黃金旗魚</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3101</font> | <font style="color:black;">維京之魂</font> | <font style="color:black;">300;600;1000</font> | <font style="color:black;">2;3;5</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3102</font> | <font style="color:black;">忍者蟹</font> | <font style="color:black;">300;600;1000</font> | <font style="color:black;">2;3;5</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3103</font> | <font style="color:black;">金剛電鱸</font> | <font style="color:black;">600;900;1500</font> | <font style="color:black;">2;3;5</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3104</font> | <font style="color:black;">金豬報福</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3105</font> | <font style="color:black;">招財貓</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3106</font> | <font style="color:black;">Mr. Bean</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3201</font> | <font style="color:black;">哥布林</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3202</font> | <font style="color:black;">寶箱小丑</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3203</font> | <font style="color:black;">鬼娃娃</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3204</font> | <font style="color:black;">搖錢樹</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3205</font> | <font style="color:black;">見習女巫</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3206</font> | <font style="color:black;">丘比特</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3207</font> | <font style="color:black;">資本大鱷</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3208</font> | <font style="color:black;">獨角獸</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3209</font> | <font style="color:black;">UFO</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3210</font> | <font style="color:black;">財神</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3211</font> | <font style="color:black;">福神</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3212</font> | <font style="color:black;">夢幻木馬</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3213</font> | <font style="color:black;">復活石像</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3214</font> | <font style="color:black;">怪奇寶箱</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3215</font> | <font style="color:black;">血紅咆哮</font> | <font style="color:black;">1000</font> | <font style="color:black;">1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3216</font> | <font style="color:black;">死海之殤</font> | <font style="color:black;">500;800;1100;1500</font> | <font style="color:black;">1;1;1;3</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3217</font> | <font style="color:black;">幽冥巨鯨</font> | <font style="color:black;">500;800;1500</font> | <font style="color:black;">1;1;1</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3301</font> | <font style="color:black;">黑寡婦</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3302</font> | <font style="color:black;">死亡浴缸</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3303</font> | <font style="color:black;">毒伯爵</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3304</font> | <font style="color:black;">兔女郎</font> | <font style="color:black;">500;1000;2000</font> | <font style="color:black;">2;3;5</font> | <font style="color:black;">1000</font> |
| <font style="color:black;">Fish_3305</font> | <font style="color:black;">龍的傳人</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3306</font> | <font style="color:black;">伊賀</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3307</font> | <font style="color:black;">火龍王</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3308</font> | <font style="color:black;">冰龍王</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3309</font> | <font style="color:black;">風龍王</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3310</font> | <font style="color:black;">金龍王</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3311</font> | <font style="color:black;">惡靈詛咒</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3312</font> | <font style="color:black;">電鋸狂魔</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3313</font> | <font style="color:black;">塞提一世</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3314</font> | <font style="color:black;">海盜船</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3401</font> | <font style="color:black;">霍頓伯爵</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3402</font> | <font style="color:black;">哈迪斯</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3403</font> | <font style="color:black;">凱西莫多</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3404</font> | <font style="color:black;">鋼鐵霸王龍</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |
| <font style="color:black;">Fish_3405</font> | <font style="color:black;">大天使</font> | <font style="color:black;">20</font> | <font style="color:black;">1</font> | <font style="color:black;">4330</font> |




例2：不同渔场内能出现的鱼的种类

| **<font style="color:black;">渔场名称</font>** | **<font style="color:black;">渔场对应刷新组id</font>** |
| :---: | :---: |
| <font style="color:black;">海洋爭霸</font> | <font style="color:black;">1;2;3;4;5</font> |
| <font style="color:black;">南海探險</font> | <font style="color:black;">6;7;8;9;10</font> |
| <font style="color:black;">加勒比海盜</font> | <font style="color:black;">1;2;3;4;5</font> |
| <font style="color:black;">深海迷航</font> | <font style="color:black;">6;7;8;9;10</font> |


### 5.1.2. 数据可靠性
因为模拟流程是实际在游戏中进行，游戏中都是后续上限运营时实际的参数，比起模拟器凭空设定更加稳定可靠。

同时数据运行流程各处有迹可查，便于bug查询。

### 5.1.3. 数据处理高效
模拟流程需要大量的用户数据，通过unity在游戏内实际模拟，多线程处理获取结果更快，便于运营快速调试各参数模拟不同情况。

### 5.1.4. 前台控制器
预期做一个单独的预输入程序界面，运营可视化的调整用户生成相关信息，来决定用户的属性，模拟用户的下注、充值、提现等行为。

同时可以设定用户是否使用“狂暴”等游戏内功能道具。

### 5.1.5. 后台控制器
预期做一个可以实时监控的后台界面，在模拟器运行过程中，可以随时暂停天数、延长天数、调整各运营数值等。

通过实时调整参数的方式，模拟真实的运营行为，调整抽水率、Buff等参数提升用户体验。

同时可以更加精细化运营，给某些来源的用户甚至某个单独的用户调整抽水率等。

















