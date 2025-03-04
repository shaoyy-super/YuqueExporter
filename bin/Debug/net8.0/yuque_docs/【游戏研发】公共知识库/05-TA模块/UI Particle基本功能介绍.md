# UI Particle参数说明
**UI粒子特效需要在Canvas下制作！！！**

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733376250675-334cb6bb-2d56-4baf-afec-bb433aa205af.png)

+ **Maskable：**该部分的UI粒子渲染可被遮罩
+ **Scale：**UI粒子缩放。当选中**3D**时，scale拓展为三维参数(x, y, z)
+ **Animatable Properties：**可在动画中控制材质参数，例如_MainTex_ST，_Color
+ **Mesh Sharing：**开启后能使得同group内的粒子共享同一个粒子结果**。**若面板中存在很多相同的ui粒子，则该参数可选为**Auto**，然后对各ui粒子进行手动分组，也可使用**Random**进行随机分组。
+ **Position Mode：**粒子发射器位置模型。
    - **Absolute：**基于particleSystem的世界位置。
    - **Relative：**基于particleSystem的缩放位置。
+ **Auto Scaling Mode：**若选中**Transform**，则可以根据canvas的scale进行自动适配，避免其缩放导致的影响。
+ **Rendering Order：**可以在此修改particleSystem和对应的material。

# 常规用法
## 层级关系
eg：图片-粒子-图片

用UIParticle来做UI特效的话，其层级关系和UI一样，由Hierachy前后关系决定

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733387040532-c3668a25-be81-4b90-885d-33725137e3a8.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733387051469-4f309a8b-04fc-4b62-8899-03c8ed28efc8.png)

## Mask
1. 创建一个Mask作为父物体（Image挂载Mask组件）

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733387816095-7b7bd83f-b844-4ddf-8507-6a09c683c8be.png)

2. 创建UI Particle置于Mask下作为子物体
3. UI Particle组件Maskable参数✔

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733388372042-fe09563d-85da-43aa-a205-b6f811033acf.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733388445166-30cdc552-1c78-46b8-9db8-c1e7f6c3b414.png)

## Particle System参数和Effect材质参数统一问题
在排查问题的时候遇到不少同一个问题，特效在做粒子时，其Particle System和Effect材质参数不统一的问题，导致改为ui particle时效果不一且产生了问题。

如：Effect材质CustomData1,2开启了，但Particle System中的Custom Data却未开启。

特效在制作时需要注意这种错误情况。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733475192418-a2287ec5-0f3e-454e-abbd-0aaaa3059233.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733475250612-36f9099a-e645-426d-948e-33802c7e01d8.png)

## 使用UI Particle后Render问题
在ui particle后，会发现Particle System里Renderer自动取消勾选了。这是因为ui particle有自己的一套renderer，需要将其bake为ui mesh；当然，其会读取Particle System里的参数进行同步。如果挂载ui particle后再开启Particle System里Renderer会导致粒子被渲染了两遍，ui和world。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733722295770-0cc27cca-6316-4dd1-aa91-549d6602f346.png)

所以我们正常的流程做法该是，创建ui particle system后，如有对Renderer里参数有修改需求，则开启Particle System里的Renderer对其参数进行修改，修改后关闭Renderer或者UI Particle里点一下Refresh（点击后会自动关闭）。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1733722768998-8f24b3f8-780f-44fc-85f8-7197cbfc0246.png)

