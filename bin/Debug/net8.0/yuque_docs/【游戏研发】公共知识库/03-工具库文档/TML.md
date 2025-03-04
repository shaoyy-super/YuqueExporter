# 创建Timeline
首先在Project界面中右键菜单-Create-Timeline,就可以创建一个Timeline资源了(格式为.playable)

![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712745738401-b97f3f37-8190-4cf3-96bb-a4a9c72bc12a.gif)

这创建出来的Tmieline是没有被引用的需要通过Playable Director组件来引用这个Timeline资源是未被引用的，需要通过Playable Director组件来引用这个Timeline资源。我们可以给某个GameObject手动添加Playable Director然后引用该Timeline资源。![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712746201038-a14e9db7-2df9-447d-ad9e-06b173c5464f.gif)



# Timeline面板介绍
![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712801027917-1fd3a046-713f-4203-a51b-8724ddf8bda6.png)



| 按钮 | 功能 |
| --- | --- |
| 1 移动到起始位置按钮 | 点击这个按钮可以将时间轴移动到最开始的位置 |
| 2 移动到前一帧按钮 | 点击这个按钮可以将时间轴移动到前一帧 |
| 3 播放按钮 | 点击播放按钮后，会播放Timeline，可以在场景中预览动画效果。快捷键是空格。进入播放模式后，会从当前时间轴所处的位置开始播放，直到最后。如果范围播放按钮启动的话，播放会限定在指定的时间区域内。时间轴播放时会跟着移动，时间轴的区域会显示当前的时间或者帧数，取决于Timeline的设置 |
| 4 移动到下一帧按钮 | 点击这个按钮可以将时间轴移动到下一帧 |
| 5 移动到最后位置按钮 | 点击这个按钮可以将时间轴移动到最后的位置 |
| 6 范围播放按钮 | 启动范围播放按钮后，可以在指定范围内进行预览播放。注意只能在Timeline窗口中预览时生效，在运行模式下会忽略范围设置。<br/>时间轴上会显示开始和结束的位置，可以拖动开始和结束的图标来修改范围。 |


![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712801361121-181c858c-fdf8-458f-ab64-f774e751ce42.png)

preview按钮可以启用或者禁用对你在场景中选中的Timeline实例的预览效果

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712801480499-2f966716-d77f-441b-b396-da4d39482733.png)

如图我录制了一个动画在第0帧时将这个图片的位置移到左下角（此时Preview是关闭的）![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712801656767-661f7728-3791-403b-937e-3df66e6c54fa.gif)

可以看见当我开启Preview的时候图片的第0帧就应用了轨道上的Timeline，当我关闭Preview时图片就又回到了之前的位置

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712801788859-b6bb1a4b-c0d7-4fbd-b93e-6f813e7aa1cd.png)

Maskers按钮

点击Maskers按钮后就会打开Maskers轨道Maskers轨道属于一种特殊的Signal轨道，使用方法可以参照后续Signal轨道的介绍

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712801846238-c76a3764-c79f-47f1-b53d-df1d84679bcb.png)



|    名称 | 功能 |
| --- | --- |
| 1 Hide curves view | 开启该按钮后就可以看见该轨道上的数值的变化 |
| 2 lock | 点击该按钮之后，就无法再对该轨道进行编辑，但是可以隐藏掉 |
| 3 Mute | 点击该按钮后就可以隐藏该条轨道，当你点击运行时该轨道不参与其中PS：虽然轨道不参与Timeline运行但是可以修改该轨道上的参数，建议与Lock按钮一起使用 |


在Timeline界面的右上角还有两个比较重要的按钮

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712801951493-c88c3419-8178-4427-ad03-76950cb8b794.png)

图上的锁按钮可以锁定当前Timeline的状态。点击锁按钮之后，此时选择其他的对象也不会修改Timeline窗口里的参数，即使你选的是另一个Timeline对象也不会改变。这点在制作单个Timeline的时候会比较方便。而另一个设置按钮里的Frame Rate则是另一个重点 在这个选项卡里，我们可以选择许多不同的帧率，比如我现在选中的帧率就是60帧每秒的帧率。

# 时间轴
Timeline窗口中的时间轴显示了当前预览的位置。时间轴上显示了当前的帧数或者时间。此外滚动条上也有一个小的白竖线，代表了时间轴的位置。

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712802046133-89cf30a1-536e-493d-b1eb-9c8d5f60af93.png)

#  轨道的种类
![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712802091524-3a03b5af-70eb-454f-91f2-358c44d9354f.png)

在Timeline窗口右键菜单或者点击加号，就可以创建多种轨道，下面我们用表格的形式来了解一下它们



|    轨道 |   描述 |
| --- | --- |
| Track Group | 它可以将不同的轨道进行分类，相当于文件夹的功能。 |
| Activation Track | 该轨道是用来控制物体的显示与隐藏。<br/>[Activation Track 轨道](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/dq1l1h5a2qyfcwqg)  |
| Animation Track | 该轨道可以为物体加入动画，它既可以在场景中方便的录制动画，也可以直接使用已经制作好的Animation Clip<br/>[Animation Track 轨道](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/dq6oo6vx5z0otk3v)  |
| Audio Track | 该轨道为音频轨道，可以为动画添加音效，也可以对音效进行简单的裁剪和操作<br/>[Audio Track轨道](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/wreisy0aapvldl2e)  |
| Control Track | 在该轨道上可以添加粒子效果，同时也可以添加子Timeline进行嵌套<br/>[Control Track轨道](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/ldpm6p8xpmtgyzot)  |
| Signal Track | 该轨道为信号轨道，可以发送信号，触发响应信号的函数调用<br/>[Signal轨道](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/lhgd8csrdfwkl0wl)  |
| Playable Track | 在该轨道中用户可以添加自定义的播放功能<br/>[Playable Track轨道](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/urzneu2bymqb83eu)  |


