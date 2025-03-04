## 简介：
FA战斗专用的摄像机聚焦功能.

在**时间点A** 到 **时间点B**开始转向(位置不变) 目标组的中心坐标，并且过渡到目标FOV。

**时间点C**到**时间点D**从目标状态恢复到原始状态。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943206463-0df4d98d-a32a-45c1-aafd-49f664ee9b9f.png)

## 参数细节介绍
### 1.轨道参数
用这个轨道，但是不同的片段

[thoughts 文档](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60d59881eaa11900017aa740)

### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943207041-7ce7b9b9-5a53-4e8c-94e7-add347a99cea.png)

| Only Work For Home | 只在我方角色为owner的时候生效 |
| :--- | :--- |
| FocusFOV | 目标FOV |
| UpdateAimPosPerFrame | 是否每帧更新瞄准位置，<br/>不勾上的话，只在进入的时候计算一次 |
| AimOffset | 瞄准偏移，Owner的本地坐标（例如，Z为Owner的前方） |
| Targets | 目标组（多个[thoughts 文档](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a)） |


