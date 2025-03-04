解决播放一个视频，美术希望让视频不受场景环境（灯光，后期等）影响的需求。

## 一、一次性的准备操作
1. 添加一个视频播放层，如下：

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715068424466-c28d6b44-916a-4158-8df3-5d1a87de27c1.png)

点击Layer----》点击Add Layer

在打开的视图里添加新层级“Video”。

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715068482476-3d31d8e2-6e8f-44d5-87db-b98d1650c7b3.png)

2. 在URP-HighFidelity-Render设置里去除对Video层的渲染。如下：

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715068620685-8f52c795-ba48-418d-907a-0a929db6aff2.png)

3. 在URP-HighFidelity-Render设置里点击Add Renderer Feature，新加一个Render Objects

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715068717817-4975650a-8251-4cd2-bb0d-2f4aa29d43f2.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715068772015-14f597c3-3637-4365-a27c-1f56c417ffe2.png)

然后按如下设置新建的特性

Name可以改成Video，Event改成AfterRenderingPostProcessing，Layer Mask设置video

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715093021697-1f4bd215-288d-43fa-83e0-e14eebec5bb6.png)

此时，后处理将不再作用于video层级

## 二、设置videoplayer
![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715069181612-1eeffa3d-279b-4e33-b28a-2a11a26028e0.png)

按上图先新建材质球和一个renderTexture，并设置RT的size和视频一致。

<font style="color:rgb(237, 235, 232);background-color:rgb(19, 21, 22);">并将 Render Texture 拖拽到 Base Map,同时也拖拽到Emissive Map,控制Emission Intensity的强度可以调节材质的亮度。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715069277932-74437359-367e-47b3-914b-960b1b8a40e6.png)

在场景中新建plane

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715069326791-8d07b33e-e444-455d-91db-e3bfe97beb28.png)

按下图修改层级，设置视频文件，设置rendermode和TargetTexture

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715069445975-cb6639e7-df14-4d0a-8ddf-9be004a61b9e.png)

去除灯光中对video层的影响。

![](https://cdn.nlark.com/yuque/0/2024/png/43475718/1715069495693-9f6e5fd9-28b5-4365-af33-edb84a297c98.png)



