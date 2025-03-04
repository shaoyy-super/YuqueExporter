### 简介：
播放TML时，会使用Clip上指定的[<u>environmentVolume</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60e6e23541cef6000174099d)来代替当前的环境参数，播放完成后会还原当前环境参数。暂不支持插值功能，有需求可以提。

~~**时间点A**~~ ~~生成环境参数对象A并应用对应的环境参数值进行自身过渡~~

~~**时间点B**~~ ~~生成环境参数对象B,由A过渡到B~~

~~**时间点C**~~ ~~删除环境参数对象A完全过渡为环境参数对象B~~

~~**时间点D**~~ ~~删除环境参数对象B应用默认值。~~

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943218027-954a37f2-fe43-4220-aea4-e31a400c3767.png)

参数细节介绍:

### 1.轨道参数（选择目标）
无

### 2.片段参数（选择目标位置）
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943218255-109c86ed-87f9-4b87-aa26-5ba5d9431594.png)

| 参数 | 说明 |
| :--- | :--- |
| 环境参数预制体 | 带有[<u>environmentVolume</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60e6e23541cef6000174099d)脚本的预制体（注意：**这个预制体不能直接放在TML下面**，否则会在TML刚开始播放的时候就生效。要做成Perfab，拖到上图红框处） |


