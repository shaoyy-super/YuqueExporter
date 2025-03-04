# 背景概述
该方案是由于项目Animator消耗过高，为解决此问题而提出的方案。

# 适用范围
相机分辨率：1920*1080

1. 在该相机分辨率下所占像素较小的鱼。现限制：分辨率256*256以下，最好是细长的鱼（这样light cookie影响不大）。
2. 动画帧数不能过多。TexWidth * TexHeight * FrameCount <= 2048 * 2048
3. 材质效果不要太复杂，譬如存在薄膜干涉之类的。（怕前后帧衔接不上，会存在跳变。）

# 制作流程
1. 打开<font style="background-color:#FBDE28;">Sce_SequenceAnim</font>场景
2. 找到要处理的鱼Prefab A，复制一份出来B，去除B上的脚本，将B拖入场景，确认B的position为（0，0，0），确认<font style="background-color:#FBDE28;">鱼头朝向X轴正方向</font>。（不然Recorder时会材质miss）

处理完的Prefab B可以存入Assets/ArtNoBuild/Scenes/Sce_SequenceAnim/Prefabs中

3. 根据下面Fish拍摄参数参考表，确认Camera Position和分辨率，根据分辨率调整Game视图和CaptureMapRT图的Size。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735007742930-fd7720c3-558d-49c9-935c-fc955bd0c093.png)![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735007762921-2d4a47e8-a645-40ce-b173-0ccce8bfed4d.png)

4. 检查确保CaptureMapRT图挂载在Camera的Output中

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735007927479-abeb3c08-1bd3-4b09-8c4d-23d9f3a0ebb4.png)

5. Window->General->Recorder->Recorder Window打开窗口并调整参数至如下图

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735008295569-8f64f70b-b14c-4bc4-976a-07ec77197bae.png)

6. 点击START RECORDING开始录制，完成后在Assets/ArtNobuild/Scenes/Sce_SequenceAnim路径下新建文件夹，将录制生成的帧贴图放入该文件夹。
7. Tools->TA_Tools->MergeToSequenceMap打开窗口，将步骤6新建的文件夹拖入“帧贴图文件输入路径”，在拖入后会对贴图规范进行自动检查；拖入后记得查看Console是否出现报错日志，若有，需根据提示进行解决（后缀一定需是_xxxx的数字，避免混入奇怪的东西）。
8. 点击Generate，若未进行进程，查看Console是否出现报错日志，根据提示进行问题处理。若进程完毕，则会生成一张序列帧贴图，如下图；除此外，Console会有相应日志输出，该日志的参数会在步骤9中材质部分应用到，所以请<font style="background-color:#FBDE28;">务必截图记录</font>。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735008994761-e4bd6816-d53c-49fd-a694-10820adeff5c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735012559744-308c0fc8-b9b3-4dee-90ea-673c51b0f34f.png)

9. 制作相应Fish的帧动画Prefab。
    1. <font style="background-color:#FBDE28;">Mesh</font>用Fish Plane（该Plane Mesh是特别处理的，符合常规uv布局，unity的plane uv是反的）；
    2. 新建材质，<font style="background-color:#FBDE28;">材质</font>shader使用"URP/Aquaman/FishSquenceAnim"，将步骤8日志里 的参数填入；
    3. 挂载SequenceAnim.cs<font style="background-color:#FBDE28;">脚本</font>组件；
    4. 添加BoxCollider组件并对collider size进行修改；
    5. 根据步骤8日志里的SingleWidth和SingleHeight比例对Fish_xxxx进行缩放。如128：64对应

Scale 1：0.5（这么做是为了uv匹配贴图长宽比，避免拉伸）。

这里提供了一个模板例子进行参考：Example_SequenceAnim.Prefab

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735022544194-24949017-d4f5-4ff9-9de6-8bb39c90ba22.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1735022559835-149a9857-7909-4ec5-9291-358f0d9b8401.png)

10. 再跟原Prefab A的fish模型进行对比，进一步调整缩放，使得两者大小看起来差不多。

# Fish拍摄参数参考表
| **Fish Name** | **Camera Position** | **CaptureMapRT Resolution** | **Shrink**<br/>**Resolution** | **Sequence Resolution** | **Animation Frame** | **Sequence Frame** | **拉伸矫正**<br/>**初始Scale** | **缩放** |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| <font style="color:#000000;">Fish_1001</font> | <font style="color:#000000;">(-1.66，10.77，0)</font> | <font style="color:#000000;">128*64</font> | <font style="color:#000000;">128*64</font> | <font style="color:#000000;">512*256</font> | <font style="color:#000000;">20</font> | <font style="color:#000000;">15</font> | <font style="color:#000000;">0.5，1，1</font> | <font style="color:#000000;">1.25</font> |
| <font style="color:#000000;">Fish_1002</font> | <font style="color:#000000;">(-1.66，11.6，0)</font> | <font style="color:#000000;">128*64</font> | <font style="color:#000000;">128*64</font> | <font style="color:#000000;">1024*512</font> | <font style="color:#000000;">60</font> | <font style="color:#000000;">60</font> | <font style="color:#000000;">0.5，1，1</font> | <font style="color:#000000;">1.4</font> |
| Fish_1003 | (-4.8，15.22，0) | 204*113 | 168*93 | 1024*1024 | 60 | 60 | 0.55，1，1 | 1.8 |
| <font style="color:#000000;">Fish_1004</font> | <font style="color:#000000;">(-3.8，14.39，0)</font> | <font style="color:#000000;">210*100</font> | <font style="color:#000000;">204*97</font> | <font style="color:#000000;">1024*1024</font> | <font style="color:#000000;">60</font> | <font style="color:#000000;">49</font> | <font style="color:#000000;">0.48，1，1</font> | <font style="color:#000000;">2</font> |
| Fish_1005 | (-1.8，33.4，0) | 169*255 | 135*204 | 1024*2048 | 80 | 69 | 1，1，0.66 | 2.1 |
| Fish_1006 | (0，11，0) | 151*82 | 146*79 | 1024*512 | 60 | 41 | 0.54，1，1 | 1.4 |
| Fish_1007 | (-0.9，10.7，2.64) | 146*78 | 119*64 | 512*512 | 40 | 31 | 0.54，1，1 | 1.35 |
| Fish_1008 | (-0.34，12.25，1.48) | 229*84 | 229*84 | 1024*1024 | 60 | 47 | 0.37，1，1 | 2.2 |
| <font style="color:#000000;">Fish_1009</font> | <font style="color:#000000;">(0，24.15，-2.6)</font> | <font style="color:#000000;">207*175</font> | <font style="color:#000000;">170*144</font> | <font style="color:#000000;">1024*512</font> | <font style="color:#000000;">20</font> | <font style="color:#000000;">17</font> | <font style="color:#000000;">0.85，1，1</font> | <font style="color:#000000;">1.8</font> |
| Fish_1010 | (-4，20，1.89) | 196*150 | 167*128 | 1024*512 | 28 | 23 | 0.77，1，1 | 1.7 |
| Fish_1011 | (-0.22，19.03，-0.4) | 87*137 | 72*113 | 512*1024 | 80 | 62 | 1，1，0.64 | 1.25 |
| Fish_1012 | (0.7，25.13，3.84) | 271*170 | 204*128 | 1024*1024 | 40 | 39 | 0.63，1，1 | 2.65 |
| <font style="color:#000000;">Fish_1013</font> | <font style="color:#000000;">(-0.65，31.85，0)</font> | <font style="color:#000000;">168*218</font> | <font style="color:#000000;">143*186</font> | <font style="color:#000000;">1024*2048</font> | <font style="color:#000000;">100</font> | <font style="color:#000000;">76</font> | <font style="color:#000000;">1，1，0.77</font> | <font style="color:#000000;">2.2</font> |
| Fish_1014 | (1.4，30，-0.42) | 185*207 | 185*207 | 1024*2048 | 60 | 44 | 1，1，0.89 | 2 |
| Fish_1015 | (-2.5，19.78，-0.68) | 213*140 | 170*112 | 1024*1024 | 80 | 53 | 0.66，1，1 | 2 |
| Fish_1016 | (-3.4，20.37，-0.38) | 192*140 | 156*113 | 1024*1024 | 80 | 53 | 0.72，1，1 | 1.8 |
| Fish_1017 | (2.3，23.41，0) | 225*165 | 204*150 | 1024*1024 | 40 | 29 | 0.74，1，1 | 2.1 |
| Fish_1018 | (1.1，29，0) | 188*195 | 170*177 | 2048*1024 | 80 | 59 | 1，1，0.96 | 1.9 |
| Fish_1019 | (-1.2，31.17，1.6) | 332*220 | 257*170 | 2048*1024 | 60 | 41 | 0.66，1，1 | 3 |
| Fish_1020_01 | (0，19.5，0.7) | 137*137 | 136*136 | 2048*1024 | 160 | 104 | 1，1，1 | 1.3 |
| Fish_1020_02 | (0.7，19.5，0) | 137*137 | 136*136 | 2048*1024 | 160 | 104 | 1，1，1 | 1.3 |




## 记录流程
流程都在Sce_SequenceAnim场景中进行。

1. 复原Camera
    1. camera position复原至（0，150，0）
    2. camera组件Output里的Output Texture设为None
2. 运行场景，Game视图分辨率设为1920*1080 Landscape。
3. 将流程制作步骤2里新建的Prefab丢入场景。
4. 使用截屏软件，确认涵盖动画范围的最小分辨率，对该分辨率向上取最近2次幂，32，64，128，256，512，1024这几个数，方便后期序列帧贴图制作和压缩。

![](https://cdn.nlark.com/yuque/0/2024/jpeg/46064633/1735021105962-c6036a55-311c-4ba6-839f-b2799fe41231.jpeg)

5. 将Game视图分辨率设为步骤4得到的分辨率数值。
6. 调整Camera position，使视图要涵盖整个fish动画周期，满足该条件的同时还要尽量使得fish填充满画面。调整好后记录下Camera position。

# 优化成果
测试环境：小米6x 第二渔场

原本动画性能：

![](https://cdn.nlark.com/yuque/0/2025/png/46064633/1738732534674-e2f5444e-e38f-4c19-8f4e-e941122ba8fb.png)

序列帧level1：

![](https://cdn.nlark.com/yuque/0/2025/png/46064633/1738732568957-2401d564-caa0-4e34-98d5-d02dc007f947.png)

序列帧level2：

![](https://cdn.nlark.com/yuque/0/2025/png/46064633/1738732586972-14f80a72-0367-48ca-bfe7-51197ee629c5.png)

