

# 一、创建导轨与电影摄像机Actor
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723612674953-90a33d53-0e61-4df5-a50c-49a54c6bbff3.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723612732342-f3ef93c3-7997-4b46-8027-f57094ece82d.png)

如上图所示创建摄像机导轨与电影摄像机Actor

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723612810117-a8dfddea-7f11-4f93-9c72-236abec39e9a.png)

大纲视图中将CineCameraActor拖到CameraRig_Rail上，将摄像机设为导轨子级

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615827434-347955e4-52cd-4151-b21e-2e8f2c9237e2.png)

选中摄像机导轨，开启选项“将朝向锁定到滑轨”，此时摄像机的朝向会根据所在滑轨位置自行调整



# 二、编辑导轨
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723613022038-519e8083-397a-415a-b385-4f53d53b333e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723612987028-a0742f69-dea0-4ae4-8af2-c1cf05b949e9.png)

将摄像机移动到导轨的滑块上，摄像机会跟随滑块移动（不移过去的话摄像机会保持和滑块的相对位置进行移动）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723614027510-3e43bd27-50c3-4761-b050-faa73ffa5dfd.png)

选中导轨上的白点，可以移动当前段的导轨位置

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723614053017-553b7337-351a-4bf2-9e55-584432aa9a1a.png)

同样选中导轨上的白点，按住Alt键移动时会创建一段新的导轨

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615172250-779fa0e4-991d-4df5-86ae-ffc6e9a98eb5.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615233381-2ab955b7-7241-40f6-aea1-a7bb16ae3e03.png)

如果对弯道处的角度不满意，可以按上图步骤手动调整角度



# 三、加入Sequence
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723617199797-6656bd6a-d062-435d-9285-afcb5ae28c01.png)

如果场景内没有Sequence，在红框内择一创建Sequence; 如果需要在已有的Sequence中进行编辑，在黄框位置打开需要编辑的Sequence

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615348065-caf4881c-2de8-44a0-826a-8ebc41172846.png)

将大纲视图中的导轨和摄像机拖入Sequence（两个都要）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615381954-1cd98b5c-98ae-47c8-82a5-2ae6069d8f80.png)

拖完以后主视图会切换到创建的电影摄像机Actor视角，**<font style="color:#DF2A3F;">此时不要移动视角！</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615440365-f2712d51-cd25-4dbb-8302-da90fcff8692.png)

按上图所示先切换到过场动画视口，再切回默认视口就能回到预览摄像机视角（默认从Sequence出来也是在默认视口，有点bug)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615473570-eb1fb168-0350-47d9-9742-846d90bddafa.png)

创建第二个Viewport用于查看电影摄像机Actor视角

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615557672-1e301534-0e2e-4100-9126-418d087b56ca.png)

在刚刚创建的视口二中点击一下，然后进入Sequence面板，点击电影摄像机Actor边上的摄像机图标，此时视口二的视角就会切换到电影摄像机的视角

此时，视口一：预览摄像机，进行日常操作

视口二：电影相机，仅预览

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723615609173-caa143f2-1832-48ca-bd65-7bdf0dfce5a9.png)

如上图所示给导轨添加关键帧，首尾打0和1，当中按需求K帧，用于控制摄像机的运动速度

（如果需要摄像机在某个时间段进行旋转等也可以在摄像机的Transform里K帧）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723616004835-d937ee52-b399-44ac-acbc-69ec5f032b63.png)

上图红框位置可以打开曲线编辑器，对Sequence内的各项K过帧的数据进行编辑

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1723616489914-90a144d8-06f5-47b2-9e59-ab0fc00bd400.png)

主要会调过渡等设置

