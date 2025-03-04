

# 初期设置：
## 1.材质设置：
shader使用FishStandard

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729576268952-e52398bc-fc9d-4f70-acaa-e00a2da5ee08.png)

勾选启用Flatten和Enable Custom Fov

（临时）如果是手拖Prefab进场景的话，Flatten World Origin Pos 的Y轴和 Flatten Plane Offset的值需要手动设置下，需保持一致



Flatten Factor会影响模型移动时的透视程度，值越小移动时越不受透视影响，但可能引起ZFighting，默认0.1



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729576346511-f239490f-0672-4cbd-aa37-76f96a0b3b36.png)

半透物体不显示的话检查下Render Queue有没有改成Transparent，替换shader的时候可能没有继承到



## 2.Prefab设置：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729576171117-6d79f038-eecc-49b0-a9c6-442fcd686228.png)

Layer使用Character_Fov(应用到所有子物体)



模型位置与Prefab根节点位置尽量保持一致（不一致的话可能会导致一些预期外的问题）



根节点挂载脚本FovController

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729576943317-48213731-4969-48c5-ae56-a7197e704289.png)

# 效果调整：
### step1.
进入美术测试场景运行游戏，生成模式为固定节点生成出对应模型，（不进也行，只要相机设置和游戏相机保持一致，然后将Prefab根节点transform置零，scale置1），调整Prefab次一级节点的 Position，Rotation以及Scale到一个合适的值**（此时不要勾选 Fov Controller 里的Override Camera Fov开关，调整效果时也不要动根节点的Transform）**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729649856091-b4d2b69e-cf3f-41ba-93fd-706ec104e728.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729577594662-4e336833-6e61-42e0-971f-e7f363a1bf28.png)

### step2.
勾选 Fov Controller 里的Override Camera Fov开关，调整TargetFov, ScaleFov和Transform的Rotation三个参数，使模型Fov和大小达到预期效果

**Override Camera Fov：**控制Fov功能的启用与否

**TargetFov：**镜头Fov大小，Fov越大物体屏占比越小，因为Fov相关的计算逻辑在shader里改掉了，这个参数可以简单理解为缩放值

**ScaleFov：**用于控制模型近大远小的缩放比例，同时会影响模型显示的大小和角度，因此需要配合TargetFov和Transform的Rotation一起使用

### step3.
取消勾选 Override Camera Fov开关，查看调整后的模型大小与位置与未调整时是否有较大差异，有的话：调大小重复step2，调位置改PositionBias参数，X左右，Y上下，**Z不要动，否则模型显示位置和实际位置会对不上**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1729578377027-80d0dabf-8fe5-4cec-858d-e39e1b78209a.png)



### step4.
如果是在游戏模式下调整的参数，记录调整后的参数，退出游戏模式重新赋值

