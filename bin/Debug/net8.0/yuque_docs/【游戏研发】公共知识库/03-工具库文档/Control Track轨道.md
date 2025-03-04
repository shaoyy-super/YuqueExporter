Control Track轨道可以添加粒子效果，同时也可以添加子Timeline进行嵌套

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712804867353-4f46535e-ad6f-4eab-8182-0765b33086ed.png)

首先我们创建一个粒子

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712804904299-773ec296-dada-423f-b3c9-622c34ff7a34.png)

就可以直接将这个粒子拖到我们创建好的Control Track轨道中，这时点击播放就可以看到粒子的效果了

上演示

![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712804964168-5da00465-1e2c-4b17-aaa6-af10e0bb334d.gif)

可以看见在播放Timeline的时候，粒子的效果也确实开始播放了



嵌套Timeline

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805291686-76c2b3d8-b446-43e7-b7fe-89f05f9e136a.png)

如图我又创建了一个Timeline，并且添加了一个Animation Track轨道来控制Image2的状态

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805386427-743ad3d9-17b9-4e33-990c-df7864a99af1.png)

此时我们在创建一个Control Track轨道，再将刚才的SubTimeline拖到该轨道中，就可以实现Timeline的嵌套啦

话不多说 看效果

![](https://cdn.nlark.com/yuque/0/2024/gif/43297665/1712805502522-de02f219-1a0f-4988-a598-d635e95473b7.gif)

可以看到添加的这条Timeline即使没有打开他的面板也是可以正常运行的

