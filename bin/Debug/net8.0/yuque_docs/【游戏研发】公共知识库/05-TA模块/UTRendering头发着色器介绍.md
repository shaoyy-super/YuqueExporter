URP/UT/Charater/Hair

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730109204380-bcbfc439-b5da-4579-a14b-bf0510fbfbcc.png)

第一次创建默认面板如图![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730113693304-210fa7f0-e57c-4040-a10b-a7673823ab7c.png)

可以参考的面板



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730109296102-56e45337-de63-4efd-8a65-f57ac760cc53.png)

Render Face因为继承了lit的材质使用是只显示桌面，要将Front换成Both才能双面显示



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730109340704-74ab3c76-fabb-4107-b1b0-6d68d83a9a1d.png)

Rendering Mode默认为TransParnet，默认是半透效果



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730109621940-e6ee2573-703b-4ed1-a4bb-8c98d7961b2f.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730109635724-0a88e141-18fd-404b-ade3-35b04a322258.png)

勾上是否开启渐变后，头发颜色面板由6个属性控制，使用渐变的前提是头发要有两套UV，且第二套UV必须按照规范摆放![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730110403627-b963c30e-595e-4dfa-b1ae-05049737e95c.png) 	具体[头发制作规范](https://snh48group.yuque.com/dpatt3/ztaf9f/ct5zfowlnknlqmrh)在此链接可看



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730110559421-b6b84d4b-ffe7-4c1c-bed4-1156aba89a3c.png)

主帖图RGB通道为带有AO效果的白色，A通道则是放Alpha；

法线就是正常法线贴图；

各项异性噪声贴图是默认给了一张2048*2048的高精度的贴图，可以在后面裁剪一下，调节这两个参数来获取相同的效果![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730110626047-877fe8bb-dd06-4aed-a2e5-ae762abe5827.png)

打乱各项异性高光就是使用各项异性高光的强度

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730110941854-4cf1a3f6-1ceb-43ba-85bc-d4a0c044a37f.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730110925168-8e3191d0-3996-40f2-a4f2-d2086c2af179.png)

各项异性高光强度0							各项异性高光强度0



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730111053238-6ad9e804-aac4-44cc-920b-5f5dda314bb5.png)

渐变效果的调节方式

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730111539018-4834b125-2a5a-4720-9cae-8449713c83b6.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730111562613-9eb8d519-fb7f-43ba-a20b-55f4f9b80fff.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730111588244-609596c6-d315-4779-a435-dcc65003881e.png)

图一						图二						图三

图一： 发中范围0.5	发梢范围0.5	渐变过渡0.5

图二： 发中范围1	发梢范围1	渐变过渡0.5

图三： 发中范围1	发梢范围1	渐变过渡0





![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730111848525-ed2931fe-7cde-43a7-ba2b-5a3d4b67b8ec.png)

高光分为两层高光，且高光只受主光或者开启characterVolume时指定的主光影响，其他的灯光不会产生高光；

高光的颜色，强度范围偏移都可以手动调整



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730112267432-37a4407d-9806-4880-9565-ec9783450c18.png)

SSS强度为了给头发增加通透感

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730112414730-db5fb85f-e8dc-4730-83c6-f19683931089.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730112424596-ea01f619-22a9-4bf2-a4f5-acfba19252ae.png)

SSS强度为0					SSS强度为1



SSS效果是光照不做任何处理和做了SSS效果处理的调整过渡参数

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730112627740-9896c665-59f0-4452-a85b-ff8909457b81.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730112636836-4c679bb6-6660-4f4d-80d9-aa716f1449fc.png)

           SSS效果为0					SSS效果为1





![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730112727101-e676cbec-d0c0-4d25-8277-2faf0839c6c8.png)

接受阴影强度是头发受到其他物体的阴影的明暗调节参数

自阴影则是头发增加的阴影明度调节参数

这两个参数默认为1就行了



模板设置和进阶设置是在特定情况下才使用的，使用情况很少可不说







