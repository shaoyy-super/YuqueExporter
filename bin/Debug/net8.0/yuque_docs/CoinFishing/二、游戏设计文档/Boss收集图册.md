# **概述**
## **文档说明**
+ 本文档说明了捕获boss的收集功能

## **文档维护**
| <font style="color:white;">版本</font> | <font style="color:white;">时间</font> | | <font style="color:white;">负责人</font> | <font style="color:white;">修改内容</font> |
| :---: | :---: | --- | :---: | :--- |
| <font style="color:black;">v1.0</font> | 01月14日 | | [@王敏涵](undefined/cookie-ylrqq) | <font style="color:black;">文档创建</font> |
| <font style="color:black;"></font> | 01月20日 | | [@王敏涵](undefined/cookie-ylrqq) | <font style="color:black;">单独设立图册入口，将多次捕获boss的奖励也放在收集图册中</font> |


## **设计思路**
+ 给玩家捕获boss强收集的目标感
+ 纯文字形式展示捕获类成就表现力不足，需要加强，给予单独展示窗口

# **功能说明**
+ 玩家首次捕获boss后，视为收集了该boss，在boss收集图册中可激活该boss，领取收集奖励
+ 图册内对应boss卡片变为“待激活状态”，提示”点击激活“，点击后弹出通用物品获得，获得奖励
+ 该boss被激活后，每一级需要捕获该boss相应次数，每一级有对应奖励
    - 若一次性从2升到5级，奖励一次性获取
+ 记录玩家历史捕获该boss鱼的次数与捕获的历史最高倍率

红点逻辑：

+ 卡牌红点：若达成当前捕获次数可领取奖励，卡牌上显示红点
+ 页签红点：若有可领取奖励的boss，显示红点；都领奖后红点消失
+ 任一页签有红点，则外部图册入口图标有红点

# 界面
系统入口：主界面添加【海洋图册】按钮

## 图册界面
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1737344046317-d4bda388-ad0c-4fb4-89c3-994d4f66edb8.png)

1. 左侧按boss类型分页签，默认”全部“
2. 右上角展示收集进度：捕获过的该类boss鱼/所有该类boss鱼
3. 内容区
    1. 支持上下滑动
    2. 按FishType表id排序
    3. boss卡片：共三种状态
    - 未激活状态：显示boss剪影
    - 待激活状态：显示“点击激活”动效
    - 已收集状态：显示收集等级和收集进度条

【交互逻辑】

+ 点击boss卡片，打开详情弹窗
+ 详情弹窗点击关闭按钮或空白处关闭弹窗

## 详情弹窗
### 已激活boss的详情
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1737343884220-ec6eaedc-349a-4474-8cdf-1a336042722a.png)

1. BOSS展示区：展示boss名、立绘、boss收集等级、收集进度条
    1. 若该boss未解锁，隐藏收集等级和收集进度条
2. BOSS介绍：展示boss的文字介绍，中文约3行
3. 展示出现的渔场，中文约3行
4. 显示历史捕获次数和最高捕获倍率
    1. 若该boss未解锁，该行不显示
5. 下一级奖励预览：仅做奖励预览，道具数量不定
    1. 若该boss 未解锁，文字显示为”收集奖励“

### 未激活boss的详情
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1737343651904-8ba00ce8-358e-4730-876f-6accd41cdfdb.png)

# 配置表
新增Fishbook配置表

[Fishbook.xlsm](https://snh48group.yuque.com/attachments/yuque/0/2025/xlsm/26927517/1737350884450-2cd0a059-4645-42c3-903a-6c371dbfb5e5.xlsm)

# 参考 
![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1736845687549-0108e377-8322-4b58-ae59-7eb5400b5a91.png)

![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1736845698875-bf05efef-f27b-49cb-be09-0ad5d5339dd5.png)

![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1736845711012-e1e78096-6fac-4060-b9f5-a56d582c0df7.png)

![](https://cdn.nlark.com/yuque/0/2025/png/26927517/1736845705884-a5fe3715-2249-4063-a528-770675e41871.png)

