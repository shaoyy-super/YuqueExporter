:::info
图例：

<font style="background-color:#FBDE28;">待讨论</font>

其余：待制作

:::



cc：

## 概述
已完成

待确认

未完成

## 策划
### 攻击武器设计
区分普通攻击与技能攻击

+ 原先普通攻击方式保留，特殊攻击方式概念修改，各类攻击方式无需从商店购买，直接作为玩家的技能。
+ 默认一次只装备一个技能，技能列表设置多种技能，玩家可以从技能列表中进行技能切换。
+ 玩家攻击回合满足对应技能使用条件时，玩家可以通过消耗金钱释放该技能。

攻击流程

+ 原先的每回合攻击两次流程修改为一次普通攻击，一次技能攻击。
+ 进入玩家回合，首先进行普通攻击阶段，攻击结束后，进入技能攻击阶段，若无法进行技能攻击，则直接跳过该阶段。

攻击选择表现

![](https://cdn.nlark.com/yuque/0/2025/png/43554293/1739942924060-375250c6-973d-493e-b722-0d257e0e30d0.png)

### 设计优化：界面上要展示各阵营人数分布和阵营目标（胜利条件）
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1740723280684-332c7055-963b-4a61-9fce-5b983a335b8a.png)

## UI
### 四个阵营的图标
[https://snh48group.yuque.com/zdlwma/kxyozs/sd8vcf9dgg0gk86g?singleDoc#](https://snh48group.yuque.com/zdlwma/kxyozs/sd8vcf9dgg0gk86g?singleDoc#) 《城市争霸-阵营包装》

### 国家的图标设计
[https://snh48group.yuque.com/zdlwma/kxyozs/ienm88yrf2u5xalz?singleDoc#](https://snh48group.yuque.com/zdlwma/kxyozs/ienm88yrf2u5xalz?singleDoc#) 《城市争霸-国家包装》

### 四种资源的图标设计、血量
食物、人口、科技、金钱

区分四种类型的建筑卡，现在四种卡的辨识度不强

![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1740633356848-c739cf5f-35b6-4e11-b712-3f3538290cf4.png)

参考：

![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1740722279349-686f23f8-03c5-4d0b-9214-1f3540f123a0.png)

### 缺查看放大效果
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1740735679616-92fa25c0-cf9e-482b-b9e2-4f3ab2a4ae99.png)

### 缺局内查看阵营信息的效果


## 场景美术
### 参照vision pro玩王者荣耀的效果，模糊的一个个人空间场景，游戏台中间浮空，游戏台上展示整个玩法场景，需要提供一个场景和棋盘生成动画模拟
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1740567285580-48aac9ee-b385-4ba6-8b78-5c24693a7212.png)

[通过Apple Vison Pro 观看足球比赛是怎么样的？_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV1gC411b7dS/?spm_id_from=333.788.recommend_more_video.-1&vd_source=58cf2b2864e3420e375d9ee01ae8c572)

[腾讯视频适配Vision Proᯅ，看王者荣耀比赛实时面板、沙盘数据一目了然！据说咪咕VP版还有欧洲杯沙盘…_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV1x3VtejEFS/?spm_id_from=333.337.search-card.all.click&vd_source=58cf2b2864e3420e375d9ee01ae8c572)

### 一批1x1、1x2、2x2尺寸的建筑
[https://snh48group.yuque.com/zdlwma/kxyozs/beza9d41sgz0bn5g?singleDoc#8pXI](https://snh48group.yuque.com/zdlwma/kxyozs/beza9d41sgz0bn5g?singleDoc#8pXI) 《城市争霸-建筑包装》

### 与国家匹配的主城模型
[https://snh48group.yuque.com/zdlwma/kxyozs/ienm88yrf2u5xalz?singleDoc#](https://snh48group.yuque.com/zdlwma/kxyozs/ienm88yrf2u5xalz?singleDoc#) 《城市争霸-国家包装》

### 切换玩家的场景表现
1. 玩家选择其他玩家，进入其他玩家场景时的转场表现。
+  切换玩家场景表现：我方建筑溶解，生成另一玩家的建筑 。





## 特效、动效
![](https://cdn.nlark.com/yuque/0/2024/png/43554293/1734773220881-9b6f60bf-de4f-4fb3-8be3-b39524d8e0c9.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1125%2Climit_0)

1. 随机阵营动画表现
2. 确定阵营表现
3. 查看各玩家阵营表现
4. 切换城市的过场效果（时长不超过3s

![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1740735784144-9a4d1126-a1a8-41db-80e3-bdaa3b93bf8f.png)



## 程序
1. 建筑建造表现接入（复用）
2. 攻击建筑表现 攻击特效（复用）



