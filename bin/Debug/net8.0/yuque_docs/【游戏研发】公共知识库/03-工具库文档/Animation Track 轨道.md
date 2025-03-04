动画轨道可以控制对应的对象进行动画播放

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803211268-7879f2ef-28c5-4ce4-b1d7-44909f7b0393.png)

首先我们先创建一个Animation轨道

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803235776-64e6ebe3-c7ad-4f95-9225-515e8f0cdbc7.png)

刚创建出的Animation轨道是没有任何引用对象的，因此我们需要添加一个对象进来

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803262677-1d862b2e-09e9-407d-a157-5c21d1063741.png)

在我们想要控制的对象上添加Animator组件，这样的话他就可以被我们的Animation轨道所引用。

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803282751-46a396f7-33e3-4bef-a020-9942295723dc.png)

这时我们就选中添加了该组件的对象



添加Clip的方式有两种

第一种右键轨道选择Add From Animation Clip,就可以看见该项目下所有的Clip动画了

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803303335-28a31c9b-427e-4e5e-bbc0-8b37b2fefebc.png)

可以看到本项目中已有的所有的动画的Clip

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803338905-b3068a55-e67c-4f4a-a669-d139d7dfde3c.png)

程序同学就可以使用美术同学做好的Clip来直接使用啦

第二种自行录制

点击动画轨道上的录制按钮

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803377242-87a5f960-4dae-4e9c-bb0f-659074947665.png)

可以看见该轨道已经开始录制了，这时只需要在一些关键帧修改该对象的状态，在添加完之后再次点击这个按钮我们就录制完了这条动画。

此时双击这条轨道我们就进入Animation窗口

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803403235-b188860c-4427-435a-894f-ff8b6bf9e9ba.png)

nimation的功能按钮和Timeline的按钮功能都是相同的，这里就不多做介绍了，我们介绍一下timeline里没有的功能

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803426699-0c933c46-0f5b-4450-97cc-a6609441016b.png)

Add Property点击这个按钮后可以看见这个引用对象的所有的属性，如Transfrom，Image等等，再点击属性后的 + 后就可以在Animation窗口中控制该属性的状态了。

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712803456591-ee9c0fa8-56ea-4b47-a6c5-d01890404c65.png)

在该轨道中还可以右键-Add Key来添加关键帧，这时我们就可以在这个关键帧对咱们添加属性进行控制啦。

同样的再上一波演示效果

![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712803667352-c979ee61-040f-4666-b3f2-d06be462c4ac.gif)

可以看到我的图片沿着一条直线的轨道位移，在Animation的面板上有许多的关键帧咱们只要在关键帧上修改这个物体的参数就可以实现各种各样的效果啦

