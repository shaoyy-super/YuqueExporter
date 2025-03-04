Activation Track这个轨道的功能是用来控制物体的显示和隐藏

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712802791862-ac199e1f-bd50-477c-abd3-149bf14cb12f.png)

该轨道左侧的即为该轨道所控制的对象，可以看到在该轨道中有部分轨道状态为Active，当Timeline播放到这部分的时候，该对象就会被显示出来，而在空白的部分时，这个对象就会被隐藏。

下面上演示效果![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712802945589-cae2377d-c9b2-4e29-92f4-2af140357845.gif)

可以看到当Timeline运行到Active的时候，image就被显示出来了，当Timeline移出Active时image就消失了。这样的话这条轨道的用法我们就了解的差不多了。![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803048926-bd613af4-256c-45ef-9c57-bf703715f0b4.png)

在点击这条轨道的时候我们会发现这条轨道的Inspector窗口中有Post-playback state属性



|  名称 | 功能 |
| --- | --- |
| Active | 当Timeline资源播放完后设置该轨道控制的GameObjcet状态为显示 |
| Incative | 当Timeline资源播放完后设置该轨道控制的GameObjcet状态为隐藏 |
| Revert | 当Timeline资源播放完后设置该轨道控制的GameObjcet状态为GameObject播放前的取反值 |
| Leave As Is | 当Timeline资源播放完后GameObject的状态保持不变 |


