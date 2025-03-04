| 轨道名 | MoleTransformTweenTrack |
| :--- | :--- |
| 用途简介 | 从A的位置移动到B的位置，同时支持从A的旋转融合到B的旋转 |
| 融合说明 | 支持融合，支持过渡 |
| Track/Clip 结束说明 | clip结束时还原初始位置、旋转 |
| 要点描述 | 见文末Tips |


## 参数细节介绍
### 1.轨道参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942974978-e0c98aef-6fa0-4401-adbe-bdb2ec6d91e4.png)

| **参数** | **说明** |
| :--- | :--- |
| [<u>TargetSelect</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a) | 选择要被控制的目标 |


### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942975219-147e378d-8f8d-45b4-9dd4-8a117428c2f8.png)

| **参数** | **说明** |
| :--- | :--- |
| ConstraintPositionX | 锁定x轴位移，即x轴位置不变 |
| ConstraintPositionY | 锁定y轴位移，即y轴位置不变 |
| ConstraintPositionZ | 锁定z轴位移，即z轴位置不变 |
| TweenRotation | 控制物体旋转 |
| TweenType | 移动曲线<br/>Linear：线性匀速移动<br/>Deceleration：（待考证）<br/>Harmonic：缓动缓停<br/>Custom：自定义曲线 |
| StartSelect | 起点 |
| EndSelect | 终点 |


### Tips
如下图，改轨道可在播放Clip时，将目标的位置和旋转，依据指定的曲线从起点移动到终点。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942975467-6a45f735-8542-4407-b400-a87449e4ed05.png)

因为该轨道支持融合，所以我们还可以这样做，也能达成同样的效果。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942975687-a1845ef7-e927-4b92-bb40-23851b3b484a.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942976014-7e0e91c2-d1de-4729-b31c-2ffe5aa4b256.png)

Clip1的起点和终点都设置为self，Clip2的起点和终点都设置成Target，然后让Clip1和Clip2融合。就能实现让被控制的物体从self平滑移动到Target。此时，TweenType字段是失效的。

以此类推，下图的用法是将目标从自身位置移动到Self的位置。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942976260-df4ebdd6-30cb-4c4d-8f53-d9e8b04ab111.png)

