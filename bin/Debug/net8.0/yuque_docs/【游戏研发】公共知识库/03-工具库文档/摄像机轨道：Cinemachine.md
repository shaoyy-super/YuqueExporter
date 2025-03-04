**简介：**

Unity插件Cinemachine的扩展控制轨道。控制虚拟摄像机的多种行为。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942866589-a9e6f6c8-f13d-4322-9b3c-084b1bd6aa32.png)

**参数细节介绍**

**1.轨道参数**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942866801-cc5002e8-f9dc-4568-9af4-57f975911ece.png)

点击左边红框选中轨道，在右边就会显示轨道参数

| 参数 | 说明 |
| :--- | :--- |
| useCameraModule | 是否自动获取游戏中的摄像机 |


**2.片段参数**

**(1).MoleCinemachineAnimation片段(摄像机动画)**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942867086-f846f73b-f36b-4ac2-92ea-c423b17b09eb.png)

点击想要调整的片段，显示右边的片段参数

| 参数 | 说明 |
| :--- | --- |
| StartPos | 相<u>机父节点初始位置,默认为我方位置且不旋转</u> |
| 动画片段(新版动画)(AnimClip) | 需要播放的动画片段 |
| 对敌播放是否镜像(MirrorCamera) | 对敌方播放时是否镜像，默认是镜像 |
| lerpDuration | 摄像机父节点偏移权重 |
| 自动设置相机视野(autoFieldOfView) | 自动设置相机视野，勾选后沿用摄像机原本的视野范围而不使用fieldOfView的值 |
| 相机视野(fieldOfView) |  |


