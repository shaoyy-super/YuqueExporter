# 概述
## 文档说明
<font style="background-color:rgb(253, 253, 254);">NFT兑换功能是游戏内为玩家提供的一种奖励机制，允许玩家通过捕鱼获得的金币来兑换NFT，再通过在市场上售卖NFT将NFT卡牌兑换成真金白银。</font>

## <font style="color:rgb(5, 7, 59);background-color:rgb(253, 253, 254);">文档维护</font>
| <font style="color:white;">版本</font> | <font style="color:white;">时间</font> | | <font style="color:white;">负责人</font> | <font style="color:white;">修改内容</font> |
| :---: | :---: | --- | :---: | --- |
| <font style="color:black;">v1.0</font> | <font style="color:black;">2024/8/4</font> | | <font style="color:rgb(5, 7, 59);background-color:rgb(253, 253, 254);">徐翰文</font> | <font style="color:black;">文档创建</font> |
| v.1.1 | 2024/8/23 | | 徐翰文 | 新增播放动画，恭喜获得 |
| v1.2 | 2024年10月09日 | | [@王敏涵](undefined/cookie-ylrqq) | 修改，删除多余内容、优化界面 |
|  | 2024年10月10日 | | [@王敏涵](undefined/cookie-ylrqq) | 增加nft兑换弹窗，支持单次兑换多个 |


## <font style="color:rgb(5, 7, 59);background-color:rgb(253, 253, 254);">设计思路</font>
<font style="background-color:rgb(253, 253, 254);">页面设计需注重用户体验，明确展示兑换规则、物品信息及所需条件，简化操作流程，确保界面直观易懂，同时融入吸引用户的视觉元素，以提升兑换意愿和满意度。</font>

# 功能说明
+ 玩家在游戏内成功兑换NFT卡牌后，能在我的藏品界面查看兑换的NFT
+ **仅捕鱼所获得的金币可以用来兑换NFT**
+ 每张nft卡牌有自己唯一id

# 配置表
[Exchange.xlsm](https://snh48group.yuque.com/attachments/yuque/0/2024/xlsm/26927517/1728443282892-10f1ba15-4bf2-4b9a-b4eb-0ddf50721f22.xlsm)

# 界面
系统入口：点击主界面NFT按钮，进入NFT兑换界面

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1728442524468-f8e9148f-8428-4878-b988-75d32960fa46.png)

## NFT-兑换界面
![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1728562978829-1ef6f30b-fbe3-4cb4-8cfc-03423676c0e7.png)

+ 每次默认为“兑换商店”页签
+ 左上角为【返回】按钮，点击后返回主界面
+ 点击【问号】按钮弹通用帮助弹窗
+ **右上角为资源栏，仅展示玩家捕鱼所获金币**
+ 平铺展示不同面值的NFT卡牌，共有6种面值：1，10，50，100，500，1000
    - 按面值由小到大排列
+ 点击【兑换】按钮，打开兑换弹窗

## NFT-兑换弹窗
![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1728562936248-4d0816bd-74bf-4b8c-bef1-060714604ecc.png)

+ 兑换时默认兑换数量为1
+ 点击【加号】【减号】按钮进行增减，最小数量为1，最大为当前可兑换的最大数量，达到最小/最大数量后【加号】【减号】按钮置灰不可点击
+ 展示玩家当前已拥有的该NFT数量
+ 点击【确认】进行购买，播放NFT兑换的动画
+ 点击【取消】关闭兑换弹窗

## NFT-我的收藏界面
![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1728548590553-59c3e828-3cbd-464f-9484-0f2b192eda34.png)

+ 界面支持左右滑动，展示每张nft卡牌的拥有数量

## 界面参考
（兑换界面类似商店界面）

![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1728439448333-df7ac98b-dd95-4845-87e5-09e068ed8721.png)![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1728439450384-f794e5ba-e28d-49cf-823c-a932b1ed80f1.png)

我的收藏界面参考

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1728455257485-6b31a0ae-2b66-441a-8513-fa8e023972b5.png)

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1728455266780-bc4f5d96-708e-4fbd-8ed6-7cc89b381e84.png)

