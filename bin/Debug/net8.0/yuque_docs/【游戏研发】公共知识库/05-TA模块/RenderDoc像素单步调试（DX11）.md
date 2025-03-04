# 准备步骤
## RenderDoc安装包：
[https://renderdoc.org/](https://renderdoc.org/)

## RenderDoc在Unity窗口抓帧：
安装RenderDoc后，unity会自动加载，在Scene或者Game的tab页上面右键会出现Load RenderDoc

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725335080933-8a26300c-5e9e-420d-8c7c-26a23bfec12f.png)

点击后：会出现RenderDoc抓帧的图标

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725335173586-11bc65a4-6730-4d01-b0b5-041acd98ed5c.png)

点击即可打开RenderDoc

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725335359750-6a5b1b54-a3d0-4132-ac5d-7950d8d9158d.png)

## Unity切换到DirectX平台
**File -> BuildSetting -> PlayerSettings -> Player**

把**Graphics APIs for Windows**中**Direct3D11**设置为最上面一层就可以

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725336101958-191870e3-1129-4db1-9ca2-b0416380c8cf.png)

:::info
注意：这里哪怕游戏平台是安卓等其他平台，只要将编辑器的平台改成DirectX就可以了！编辑器会默认用PC平台

:::

## 开启Shader debug（DriectX）
在Pass中插入**#pragma enable_d3d11_debug_symbols**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725343293631-276551b6-52e0-4f7c-9722-402c991a3b0d.png)

# 单步调试
## 定位
双击打开Capture，如果有多个capture想对比，可以**右键-> Open in new Instance**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725344484371-00b2a8b0-2604-4794-9d6d-2af26ce9fe84.png)

在根据Texture在左侧找到需要调试的步骤位置，在Texture中可以看到变化

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725345194702-196d23b2-3c23-43ec-95de-949a1c41cf4b.png)

由于DirectX中是翻转的，所以可以在RenderDoc中翻转过来

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725346686388-2be50091-1cab-4df6-b0aa-5230913f3c39.png)

## Fragment调试
可以把图像放大，找到想要调试的像素点，用鼠标右键点击，可以看到右下角可以看到像素的定位

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725347637244-8a459953-b404-477f-8564-2374ecc1ba3e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725347821415-5859304c-35c1-47cc-a040-2ead2d10a872.png)

History可以看到像素的历史，Debug可以开启调试

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725348246270-4dc67dcd-6680-40d0-b9de-3dfe090f7713.png)

在debug中，可以开启单步调试，调试过程中可以看到变量的值及变化（<font style="background-color:#DF2A3F;">注意必须要开启shader debug</font>）

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725348617544-e0538c63-2def-40a5-bbee-017242656da5.png)

甚至可以向后调试

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725349019064-890d654d-055b-4785-bcaf-1e26fb98d48b.png)

## Vertex调试
在MeshView里面可以看到顶点信息，在顶点上右键->Debug this Vertex可以调试顶点信息数据（<font style="background-color:#DF2A3F;">注意必须要开启shader debug</font>）

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725543619679-29a3c177-ec9f-4348-84d7-b7979a04ad2c.png)

