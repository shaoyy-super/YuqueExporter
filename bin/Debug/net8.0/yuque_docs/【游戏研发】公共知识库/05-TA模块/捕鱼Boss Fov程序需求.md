美术有新增给Boss单独调整Fov的需求，目前使用了RenderObject来实现

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1728627594820-a6abdc56-2c56-4bdf-805b-d5c8570ca894.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1728627335895-97a60aa5-174e-47a3-9390-6c94752e2d03.png)



# 需求：
1. 新建控制脚本，物体Layer设置为CharacterFov时自动挂上，目前版本手动挂也行

       脚本功能：

（1）控制上述RenderObject（LayerMask为CharacterFov）中Camera的启停，以及Field Of View和			Position Offset的值，Editor模式下ExcuteAlways，游戏中OnEnable时执行一次

（2）记录创建时的位置与旋转角度

2. 挂载上述脚本的物体基于脚本内的自定义位置与旋转角度进行创建（游戏&美术测试场景）



**测试分支：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1728628728598-f027c381-8051-4493-8165-4903a0ad2351.png)

