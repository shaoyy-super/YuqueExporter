**简介**：

在Timeline中用**嵌套Timeline轨道**去控制**另一个Timeline(这里简称SubTML)**的播放。

SubTML在**时间点A**开始播放

若SubTML的**时长大于等于(时间点B - 时间点A）**,则SubTML在**时间点B**停止播放。

若SubTML的**时长小于 (时间点B - 时间点A)**，假设**(时间点C - 时间点A)等于SubTML的时长**,则SubTML在**时间点C**停止播放

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942986993-c363e33f-11c9-415c-a4d3-3a7e13cb505e.png)

## 参数细节介绍
### 1.轨道参数
无

### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942987355-d7d4ae31-8d1a-4092-b45a-15662b25a2b9.png)

点击想要调整的片段，显示右边的片段参数

| 参数 | 说明 |
| :--- | :--- |
| TimelinePrototype | 要播放的Timeline预制体或者场景中存在的Timeline |
| 是否从父级Timeline继承目标 | 若勾上,生成的timeline的owner,target和actors会继承父Timeline.<br/>若不勾上，则不继承父Timeline的(使用场景是播放原本就在场景中的timeline，且有自己的目标) |


