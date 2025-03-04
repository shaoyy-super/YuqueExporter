Unlit/FaceDiffuseMapBlend

Unlit/FaceNormaMapBlend

Unlit/FaceMaskMapBlend

合成shader有三个，分别是Diffuse，Normal，Mask合成

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730172088763-7e086446-28da-4514-a739-0b0cab4f222b.png)

其中染色部分和皮肤是完全一样的





比较特殊的部分介绍眼妆如果要使用独立的贴图则要勾选![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730172197536-b745aff4-cbb6-4ae1-b4b6-b136d003adec.png)

勾选后，使用的眼线眼影眼睑贴图的<u>A通道必须有信息</u>。因为要保留原贴图的颜色信息，使用采用的是眼妆重合部分是取代的关系而不是叠加的关系，如第一层为红色，第二层为蓝色，表现结果是蓝色而不是紫色。取代关系为眼睑>眼线>眼影.



主帖图RGB为Diffuse，A为厚度图。

眼妆使用独立纹理后，眼妆贴图RGB为Diffuse，A为透明度。

眉毛腮红唇妆都是R通道为透明度

面纹RGB为Diffuse，A为透明度。



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730172843185-636817b4-4219-4308-9686-e9dff6f79b01.png)

FaceMaskMapBlend暂时只用整体光泽度和唇妆光泽度的调整



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730172902769-555241c6-a222-499d-b5cd-99be82428aa2.png)

FaceNormalMapBlend有主法线，眉毛、眼皮和疤痕法线的合成



合成后的材质球放入[Custom RenderTexture](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/wxfe39/cmeh7hpitxkyqy8f)就可以得到一张RT贴图，最后用这个RT图就能当成合成后的贴图，放入Skin材质球使用了

因为这个customRT是未压缩的，大小比较大，后面等运行时压缩的功能落地后就会将这个RT给压缩。

