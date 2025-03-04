URP/UT/Character/EyeShader

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730113124682-0cc5dacf-3d9a-47fd-8017-bc5bdbdff77d.png)

在第一次创建的时候面板如上图![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730113763113-3b85a15e-12db-4be7-a763-68ceb2efa30a.png)

可以参考的面板

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730113815472-22d494b6-a0ab-459e-85fa-8c08d8737eb7.png)

大小：控制除了巩膜，整体的缩放（不能缩为0，会出现问题，最多大概缩到0.0018左右）

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114077713-0cec3853-a11d-4786-ac1d-fd4f1ed9b835.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114104685-73cb8c08-c8b2-401d-a840-1e3dfb6e548d.png)

大小为0.0018								大小为0.42

明暗控制虹膜对比度

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114248039-ff9f0bf0-6fe0-474c-91f2-eeb7286628a1.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114256941-77e92d07-4ffe-4d44-b982-2fc92dccdf12.png)

明暗为0								明暗为1

瞳孔控制虹膜中心缩放

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114403056-0eefa592-8249-4d73-b29d-fb32d9201cd5.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114414440-18c48889-f37f-477e-8ff4-7d8c664b7e7f.png)

瞳孔为0							瞳孔为1.75

虹膜边缘虚实是控制虹膜边缘和巩膜过渡的颜色范围

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114544696-2771b3e5-d743-4f16-89f4-d94a37e5f49a.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114553136-f303dd1b-4777-4fb8-b9cc-60074ce931c3.png)

虹膜边缘虚实为0						虹膜边缘虚实为0.8



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114646814-561e7041-f10c-4cd2-a6a3-3e8eefdef661.png)

这些颜色则是空着对应区域的颜色

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114689030-144f4316-0726-4378-ba18-c1082f2f9ba7.png)

虹膜贴图RGB通道放Diffuse贴图，A通道无需处理

巩膜贴图同上



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730114869339-03347d7d-c91e-48c4-a916-2c3159954b70.png)

法线贴图则是巩膜的法线



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730115223261-5a5b710e-a16c-46a8-89e0-52276a41d9fd.png)

因为眼睛有折射效果，所以有Mask贴图和虹膜平面法线图，这两张图是对应的眼睛模型烘焙的，如果眼睛模型没改变的话就不用单独烘。

EyeMask贴图R通道放的是虹膜边缘的遮罩，G通道放的是高度图，如果Mask贴图的R通道没有和虹膜边缘位置匹配上可以通过虹膜位置对其这个属性来调整



虹膜平面法线图则是折射效果折射平面的法线图



虹膜视差强度则是调节眼睛视差强度的参数，条太高，视差效果会出现错误



最终颜色强度就是最终给眼睛整体乘上一个亮度，默认为1就行





