1.Foliage

解决LOD切换问题方法

1.控制台命令 foliage.forcelod 0 （耗，简单粗暴）

2.开启Nanite （相对较省，但也仅输出视频用）（透贴草禁用，反向优化）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889096606-1195b685-4b1e-45bf-8346-9ee4edc2e43c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889028611-a0bf0058-1036-4b57-9b56-0c2d517003c5.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889138117-c296fdaa-c93a-4399-89c5-83b727764c9f.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889161004-1db4bc00-368c-4d32-ae7c-be2302574239.png)

参考：[https://dev.epicgames.com/community/learning/tutorials/96Lz/unreal-engine-ultimate-foliage-guide-in-ue5](https://dev.epicgames.com/community/learning/tutorials/96Lz/unreal-engine-ultimate-foliage-guide-in-ue5)



2.Foliage剔除设置

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889430197-bb83f133-b47d-4023-99eb-9f52fe28e30d.png)

min&max为0时不剔除





优化相关（如果电脑hold不住）

开启VT

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889714541-a94dad7b-e4b0-4b87-8141-f7c28f2a43e1.png)

选中贴图转换为VT

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889809110-8a02b4ed-e0f4-464b-ba2c-7838c7e4e732.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889880012-6277fb43-a0d1-491c-80b8-ebac9d310773.png)

然后切换材质贴图采样方式

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719889901507-b67f20a5-6d09-42ba-94f0-7dd6a9671391.png)



Others

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719890043758-32fa01c4-f504-4733-9e25-5523c78636b6.png)

Shadow Cache Invalidation Behavior 改为Rigid优化阴影 （UE5.3+）



检查资产面数是否过高，过高的话降LOD

