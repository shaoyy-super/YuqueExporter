## 简介:
让**指定角色**在在**时间点A**，对Animator发出命令。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942850237-45a6c4b0-409e-42e2-b48e-d4969a1d2d96.png)

**命令类型1**：播放指定动画状态(可控制渐变时间)

**命令类型2**：改变动画状态机的参数

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942850462-c300cfd7-09eb-4342-bc09-f51e959fe56c.png)

## 参数细节介绍
### 1.轨道参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942850772-168c6f15-cadc-4d0e-858c-71fe7c4001ba.png)

点击左边红框选中轨道，在右边就会显示轨道参数

| 参数 | 说明 |
| :--- | :--- |
| 目标选择(TargetSelect) | [thoughts 文档](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a) |


### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942851230-a6a0de2f-56f8-4325-a9ce-f5ba36d391a6.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942851435-60409606-f2ba-4c26-b66b-e2d8e3751a11.png)

点击想要调整的片段，显示右边的片段参数

| 参数 | 说明 |
| :--- | --- |
| 操控方式 | CrossFade/SetParameter,选择不同的操控方式，面板显示的参数会发生变化。 |
| 当操控方式为CrossFade时 |  |
| 动画状态 | 当**指定目标**在MoleTimelineCtrl中找得到时，可以通过动画列表辅助填写。<br/>若没找到，也可以自己手动填写。 |
| 手动设置渐变时间 | 若没勾上，则自动设置。若两个片段重叠，渐变时间则会重叠时间。<br/>若勾上，则可以手动填写渐变时间 |
| 渐变时间 | 渐变时间 |
| 当操控方式为SetParameter时 |  |
| ParameterType | 参数类型 |
| Parameter | 参数名称 |
| Int/Bool/Float Value | 参数值 |
| 离开Clip时的操作 | 重置/设为False 或者 0/保持修改 |


