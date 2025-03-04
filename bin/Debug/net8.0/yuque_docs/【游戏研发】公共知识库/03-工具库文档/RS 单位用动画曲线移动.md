| 轨道名 | RSActorTransCurve |
| :--- | --- |
| 用途简介 | 用额外的动画(旋转、位移)控制Actor移动，不占用Actor的Animator |
| 融合说明 | 不支持融合 |
| Track/Clip 结束说明 | Clip结束时会还原到起始位置 |
| 要点描述 |  |


## 动画曲线生成
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943467534-ddbf726c-abad-431e-93a5-36b00573262b.png)

在动作的FBX文件上右键，选择 【RS -> 动作 -> 提取[Rotate]位移旋转曲线】

生成的曲线资源(TransAnimCurve的asset)会放到Art下同目录的位置

## 参数细节介绍
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943467832-e7b4c1d7-d1a8-4ed6-a2fd-e80c2a81bf7d.png)

### 1.轨道参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943468038-e6156953-7d11-4f37-ba3c-ea34e3592941.png)

| **参数** | **说明** |
| :--- | :--- |
| Target | 选择轨道要控制的Actor |


### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943468245-e8f2b2f8-c75d-4f4e-bc73-5137756e4ad6.png)

点击想要调整的片段，显示右边的片段参数

| **参数** | **说明** |
| :--- | :--- |
| Trans Anim | 选择这里要使用的TransAnimCurve资源 |


