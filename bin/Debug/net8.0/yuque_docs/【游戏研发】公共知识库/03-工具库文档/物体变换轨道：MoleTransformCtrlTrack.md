**简介：**

物体Transform的控制轨道。可以控制物体的位移、旋转，支持Path，支持多种类片段融合。

**参数细节介绍**

**1.轨道参数**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942728749-ef8f47d0-49e7-497f-bf46-cd570df26bfb.png)

点击左边的红色框，选中整个轨道，然后在右边的参数中**选择目标**

| 参数 | 说明 |
| :--- | :--- |
| 目标选择（TargetSelect） | [thoughts 文档](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a) |


**2.片段参数**

**(1).** **MoleObjectBattleMove片段(单纯位移控制)**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942728955-d3d8fd30-1337-4f21-99fb-bec88c5e9b74.png)

| 参数 | 说明 |
| :--- | :--- |
| 偏移目标类型 | **偏移目标类型**有以下几种选项<br/>+ 相对于自己<br/>+ 相对于敌方(己方与敌方的连线方向为Z轴方向)<br/>+ 相对于敌方阵型中心<br/>+ 相对于敌人(敌方面朝方向为Z轴方向) |
| 偏移向量 | 偏移向量 |


**(2).** **MoleObjectTurnTo片段(单纯旋转控制)**

旋转控制方式为选择其需要朝向的目标

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942729246-bfd60fda-0e23-4638-ad8b-8f10ef8dfacc.png)

| 参数 | 说明 |
| :--- | :--- |
| 目标选择（TargetSelect） | [thoughts 文档](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a) |


**(3).** **MoleObjectPath片段(路径控制，可控制位移、旋转)**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942729541-ee31b834-36a0-4438-83ca-6029fe03a314.png)

| 参数 | 说明 |
| :--- | :--- |
| 坐标空间选择 | **TMLSpace**：场景空间，路径位置固定<br/>**ActorSpace**：物体局部空间，路径位置跟随角色当前位置 |
| 同步位置 | 开始播放片段时根据“坐标空间选择”同步Object的初始位置 |
| 同步旋转 | 开始播放片段时根据“坐标空间选择”同步Object的初始旋转 |
| 是否控制旋转 | 是否根据路径朝向实时控制物体朝向 |
| PositionUnits | 控制下方Position参数代表的含义 |
| Position | 根据PositionUnits类型表示不同含义<br/>**Normalized**：标准化位置，0为路径起始点，1为路径终点<br/>**Distance**：固定长度，值为路径上对应长度的值<br/>**PathUnits**：路径点的位置，起始点不变，终点取路径第Position个点的位置 |


**(4).** **MoleObjectTransition片段(可控制物体位移，旋转，缩放)**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942729806-146eddbf-0f2d-40f4-b9cb-ca367c43164a.png)

| 参数 | 说明 |
| :--- | :--- |
| 位置：<br/>TransitionType<br/>**·** 相对于自身<br/>**·** 相对于目标<br/>**·** 开启跟随<br/>**·** 绝对值 | **相对于自身：**代表所填写的Value是一个相对于自己的偏移，即“最终位置 = 被控制物体原有位置 + Value”；<br/>**相对于目标：**代表所填写的Value是一个相对于目标的偏移，即“最终位置 = 目标位置 + Value”，其中目标参考[<u>目标选择</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a)。此时若勾选**开启跟随**，则被控制的物体将在clip运行期间始终与父物体保持大小为Value的偏移值；<br/>**绝对值：**代表所填写的Value是一个绝对值，即“最终位置 = Value” |
| 旋转：<br/>TransitionType<br/>**·** 相对于自身<br/>**·** 相对于目标<br/>**·** 开启跟随<br/>**·** 绝对值 | **相对于自身：**代表所填写的Value是一个相对于自己的偏移，即“最终旋转 = 被控制物体原有旋转+ Value”；<br/>**相对于目标：**代表所填写的Value是一个相对于目标的偏移，即“最终旋转 = 目标旋转 + Value”，其中目标参考[<u>目标选择</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a)。此时若勾选**开启跟随**，则被控制的物体将在clip运行期间始终与父物体保持大小为Value的偏移值；<br/>**绝对值：**代表所填写的Value是一个绝对值，即“最终旋转 = Value” |
| 缩放：<br/>TransitionType<br/>**·** 相对于自身<br/>**·** 相对于目标<br/>**·** 开启跟随<br/>**·** 绝对值 | **相对于自身：**代表所填写的Value是一个相对于自己的偏移，即“最终缩放 = 被控制物体原有缩放 + Value”；<br/>**相对于目标：**代表所填写的Value是一个相对于目标的偏移，即“最终缩放 = 目标缩放 + Value”，其中目标参考[<u>目标选择</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a)。此时若勾选**开启跟随**，则被控制的物体将在clip运行期间始终与父物体保持大小为Value的偏移值；<br/>**绝对值：**代表所填写的Value是一个绝对值，即“最终缩放 = Value” |


