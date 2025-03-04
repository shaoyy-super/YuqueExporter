### 简介：
此轨道用来修改场景的环境光和雾效，我们可以在一个轨道上创建多个clip来使它们支持[<u>融合</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/609f62bd12d5ba000109ac61)（融合概念参考超链接中3.3融合介绍），或者修改某个clip的EaseInDuration和BlendOutDuration达到渐入渐出的效果。

案例A:

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942744784-a1ce0ae3-0dab-4dbe-873c-2f255b0eb4e1.png)

如上图所示，播放timeline时，当前场景的灯光和雾效将在A-B段渐变到右边的目标状态，在B-C段保持目标状态，在C-D段从目标状态渐变回原始状态。

案例B:

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942745090-aafc534e-39eb-42d7-ac06-b8ef39b8ba4e.png)

需要注意的是，本轨道只支持数值类型的变量融合，如下图为ClipA和ClipB的参数设置

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942745403-abc4aa75-9f20-4af1-83bd-fc8a7588d45f.png)

它们有四项参数不同，其中前三项是数值类型，会在A-B段里逐渐融合为ClipB的值。而最后一项UseFog，不是数值类型的变量，不支持融合，会在进入ClipB片段时，即A点，瞬间改变。

## 参数细节介绍
### 1.轨道参数
| 参数 | 说明 |
| :--- | :--- |
| LeaveAsIs | 环境光在轨道播放完后是否还原为播放前的状态，勾选则代表不还原，否则为还原 |


### 2.片段参数
点击想要调整的片段，显示右边的片段参数。其中大部分参数的含义和Unity的Lighting-->Enviroment选项卡中的同名参数相同，下面主要解释一下此轨道自定义的一些参数。

| 参数 | 说明 |
| :--- | :--- |
| UseDirectSunSource | 是否使用直接光源 |
| DirectSunSource | 勾选UseDirectSunSource后，可以直接拖动光源来赋值 |
| SunSource | 未勾选UseDirectSunSource时，使用[thoughts 文档](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a)的方式来选择光源 |
| UseEnvironmentLight | 是否开启环境光融合，勾选代表开启 |
| UseReflections | 是否开启反射融合，勾选代表开启 |


