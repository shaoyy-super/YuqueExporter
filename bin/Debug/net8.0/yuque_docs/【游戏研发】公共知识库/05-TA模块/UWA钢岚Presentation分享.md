

:::info
💡UWA钢岚项目的技术视频内容分享

:::



| **项目名** | 钢岚 |
| --- | --- |
| **演讲者** | BlackJack Studio Tech Director：李志洋 |
| **状态** |  <font style="background:#DBF1B7;color:#2A4200">已读完</font> |
| **简介** | 钢岚Project: Shading,Lighting,Material, RenderPipeline Iteration, Common Convinent Tools |


# OutLine：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727590990531-c13bc03a-f323-41d3-924e-b69b0d57bbb8.png)

1. shading，lighting，material
2. 渲染管线
3. 工具

### 钢岚项目图形渲染工作目标：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727591175810-93a75671-30f0-4bd3-828b-fa7cd41acf95.png)

### 目标执行：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727592857925-7ca8eef4-cecd-495b-834f-18eeee1f0011.png)

1. 写实画风更容易达成画面的统一性
2. 确定shading model，lighting model和material model
3. 渲染管线选择增量开发，因为PC和移动的管线大的框架一致通过新增feature来做差异，目的是为了减少维护成本
4. 通过自动化工具来减少美术在效果和性能上的迭代成本



# Shading Model：
### Shader直接光照Direct Lighting：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727591889382-95934903-1069-4a9c-b940-0dd623e3c7c7.png)

+ 普通直接光的漫反射使用Disney Diffuse
+ 非金属直接光的漫反射使用优化过的Disney Diffuse, 目的是为了降低指令数
+ 地形的漫反射使用预积分的Disney Diffuse, 目的是为了降低指令数
+ 普通直接光的镜面反射使用GGX
+ 非金属直接光的镜面反射使用优化过的GGX, 目的是为了降低指令数

### 直接光照的优化Optimized：
目的：降低指令数

优化方法：

![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727592518034-2edf8385-4c81-4f80-a5a5-6b65dc0ab126.png)

1. Dielectric非金属的metallic和F0为常量值，因为项目特性的原因
+ F0 描述了表面在正常入射情况下的反射特性，决定了光在表面上的反射量
+ 对于金属和非金属材质，F0 的值会有所不同
+ 对于金属材质，F0 通常较高（接近 1），表示大部分入射光会被反射。
+ 对于非金属材质，F0 较低（通常在 0 到 1 之间），并且依赖于表面的颜色和材质。
+ 在许多渲染场景中，尤其是静态或预设的材质，可以将 F0 设置为常量值。这样可以简化计算，尤其是在没有动态变化的情况下，例如，对于大多数非金属材料，F0 可能被设置为一个固定值，如 0.04（代表 4% 的反射率）



![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727599641961-cdc07cba-32ed-4830-b2b1-b64a6c22fdca.png)

2. 用拟合公式来优化Schlick公式来降低指令数
+ 使用球面高斯函数来替代BRDF中Schlick（Fresnel）公式
+ 在计算光照和反射时，使用 _球面高斯（Spherical Gaussian）_ 来代替 _Schlick 近似 _是一种可行的做法，特别是在实现 BRDF 时。球面高斯能够提供更平滑的反射分布，而 Schlick 近似通常用于快速计算菲涅尔反射
+ 两个公式的曲线很接近



![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727599990305-0c0904cc-13b2-4167-99d9-10ac4c16f703.png)



![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727600078338-c5db28c2-a9f6-4a87-9334-6698501f9e32.png)

3. 在Terrain中使用预积分的方法来代替Diffuse BRDF，地形大部分法线朝上
+ 预积分得到的图中横轴是Roughness纵轴是cos(theta)，在法线基本朝上的情况下效果很近似
+ 使用Metalab来做3dmodel确定近似的情况，下图是一个正午的情况下，此图中x是法线和光线的夹角，y是Roughness = 1，z值是BRDF项，0-50的区间内z值比较接近



![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727602325475-7ab8b054-1a64-45ae-8f55-801fdb3564ca.png)

+ 下图是灯光处于黄昏时间的值的变化，在0-50区间只有斜率角很大的情况下值才会有大的差异

![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727664411717-f13abfb6-015f-45ef-8ae0-e1c9978b39e9.png)

+ MatLab的介绍

### Shader间接光照Indirect Lighting：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727593993657-6ee70b7d-97bc-4d00-9ce9-043e27fbeccf.png)



# Lighting Model：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727666903345-40c89627-3851-4f30-add7-810d53849051.png)

### Lighting Direct 直接光照：
+ shadow分动态和静态分开处理
+ 静态shadow是在第一帧或者在场景有变化的时候绘制，绘制后存起来，除此之外不会再绘制，比如场景被破坏了
+ 正常手动绘制动态shadow

### Lighting Indirect 间接光照
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727667598478-859bb6da-0777-440b-af97-b00028483025.png)

![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727667856324-252b85d7-1f54-4299-be85-91ff11a394c3.png)

+ 间接光照使用PRT（ Precomputed Radiance Transfer ）重新构建光照
+ 用light Probe的的烘培将PRT数据放到Probe中（PRT数据是Albedo，WorldPos, WorldNormal，Sky Visiblility），存储方式是HL2（6个方向上的光照信息）,每次光照变化后编辑器重新构建光照，再存入3D texture中，runtime中采这张3D texture，场景大小是250x250而纹理是64*64*16的所以一定会滤波处理，间接光照的精度不需要很高所以处理后是能满足需求的(效果可以参考untiy现在版本中默认管线下的LPPV+Sky Visibilty或者unity6中的APV)

![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727685887591-21734fd2-ee80-4592-84d7-faccf7caa7b2.png)

+ 上图是间接光照relighting的来源，可以relighting的shader中做计算
+ 这套方案是再编辑器下做的relighting所有性能瓶颈应该再runtime采样3D texture（单场景6张），如果是runtime下relighting再移动端下读写3D texture基本不太可能，需要用其他的方法
+ PRT参考文章[Microsoft PowerPoint - 09-PRT.ppt (jankautz.com)](https://jankautz.com/courses/ShadowCourse/09-RadianceTransfer.pdf)，[GDC 2016 (mrakobes.com)](https://mrakobes.com/Nikolay.Stefanov.GDC.2016.pdf)



### 物理光照Physical Lighting
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727686268861-b25edbd6-0b5d-4e62-aa6e-2e97436176ca.png)

+ 物理灯光的单位是基于实际光线测量值，例如您在灯泡包装或摄影测光表上看到的测量值，使用物理光照是为了规范打灯是的标准规范
+ Candela：国际单位制中发光强度的基本单位，作为参考，普通的蜡蜡烛发出的光强度约为 1 Candela
+ Lumen：描述光源向各个方向发射的可见光总量。使用此装置时，可见光量与光源的大小无关，这意味着场景的照明水平不会根据光源的大小而变化，但是，光源产生的高光会随着光源面积的增加而变暗，这是因为相同的功率分布在更大的区域
+ Lux：在 1 平方米的面积上发射 1 Lumen光通量的光源的照度为 1 Lux
+ Nits：描述可见光源的表面强度。当您使用本机时，光源的总功率取决于光源的大小，这意味着场景的照明级别会根据光源的大小而变化，光源产生的高光保持其强度，而不管表面的大小如何，在 1 平方米的面积上发射 1 candela发光强度的光源，其亮度为每平方米 1 candela
+ Exposure Value：表示相机的快门速度和 f 值的组合的值。它本质上是一种曝光测量，因此产生相同曝光水平的所有快门速度和 f 值组合具有相同的 EV，物理光源可以使用 EV100，即具有 100 国际标准组织 （ISO） 胶片的 EV，钢岚选择是再物体shader中处理自动曝光计算并没有再后处理中做计算
+ 物理灯光参考文档：[浅谈游戏中物理灯光的应用 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/660928058)

### HDRI
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727686581873-24778828-23df-4f67-8afd-fd9d1836ae86.png)



# Material Model：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727686957647-c168c903-f45e-467d-a615-ce57ca17d7d5.png)

### 角色（机甲）Material Model：
+ Dye Mask Material 调色是预设的非玩家自定义
+ Clearcoat车漆材质，直接光Specular2层通过coatMask来Scale在进行叠加，Diffuse根据coatMask的值来缩放，为了进行能量守恒，间接光制作了Specular的处理

       直射光公式：![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727712864482-0170913b-eb3b-4d4a-af8a-ab3b9bce4d1c.png)

**    **`**FC**` (Fresnel Coefficient):

    Fresnel Coefficient（Fresnel 系数）描述了光线在入射到不同折射率的表面时，反射和透射的比例

    Fc=F0+(1−F0)*pow(1−cos⁡(θ), 5)，其中 F0是反射率（在垂直入射下的反射率），θ 是光线与表面法线的角度

  

      ![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727713148156-f01dd1a8-b40a-4e2c-9f71-8f7fda6005dc.png)

+ Frosting磨砂使用Reoriented Normal Mapping进行细节Normal Map的纹理混合
+ 参考文章[Blending in Detail (selfshadow.com)](https://blog.selfshadow.com/publications/blending-in-detail/)

       ![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727713806126-bacdd43f-5863-43d5-a309-71fd2df6cf25.png)

+ Iridescence 是一种光学现象，表现为表面颜色在不同角度观察时变化的效果，常见于肥皂泡、翅膀、鱼鳞等自然界中。这种现象的产生通常与光的干涉和衍射有关。  其实就是对F0（Fresnel）的修改

       ![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727714779059-5a620b7c-46ba-40db-9e04-18d3c457f88f.png)



### 场景Material Model：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1727687688735-81ef86ed-7b0e-4eea-8d62-d0f767719d0d.png)

+ _LookDev Level_<font style="color:#8CCF17;"> </font>在不同环境下查看资源的材质和光照，会把所有资源放入此场景中同样也便于收集大多数的shader变体



### _Rain效果_，由多个Feature组成：
图1![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728528072757-3e0bb4e0-55ae-4c5a-9b11-4ae3ad52608c.png)

图2![](https://cdn.nlark.com/yuque/0/2024/webp/8418382/1728528742241-00063b02-6f41-4ba2-bb0d-73ceb4fb12f9.webp)

图3![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728527797384-edd7a039-e06b-4e8d-b559-e47fae5655e0.png)

图4![](https://cdn.nlark.com/yuque/0/2024/gif/8418382/1728531466238-c674862f-ba50-4b83-86d4-45a9f44b6242.gif)

图5![](https://cdn.nlark.com/yuque/0/2024/webp/8418382/1728531513137-9e22459e-355e-4687-929b-4291a750c7ed.webp)

+ 雨水材质：
+ 雨水涟漪可以在shader中用法线Y绝对值和(0,1,0)做dot做为mask和XZ与YZ平面的混合结果做lerp，再用XY世界空间的坐标采样一张Flipbook的涟漪normal map（见图1）或者计算涟漪的灰度图（见图2）来做表面向上的雨滴的法线并blend原材质的normal map
+ 雨水侧面的雨滴可以用法线的X和(1,0,0)做dot作为mask来lerp XZ和YZ平面，再用世界空间XZ和ZY来采一张水滴的灰度贴图（见图3）并做一次Flow map（见图4）最终的效果（见图5），最终的效果（见图5），使用全局wetness值可作为权重值，雨水的Roughness值可以是Constant值，最终的效果（见图5）
+ 地面积水可以通过water layer和地形材质的混合，混合权重是顶点色和高度信息（也可以使用一张mask贴图来lerp积水和非积水的区域）

图6A：![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728532008283-1513313c-69f1-45a3-abc5-ed4684a60751.png)

图6B： ![](https://cdn.nlark.com/yuque/0/2024/gif/8418382/1728624285928-cf520da8-5918-4c9e-a65e-980bbd8b671a.gif)



+ 近景的雨是挂在摄像机上的粒子，远景雨是一个随着摄像机转的个plane并且垂直于XZ平面，在上面做UV动画（见图6A,6B）

图7：![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728532318748-6cfc6d93-a2d8-4d1a-b940-be456ba51ecf.png)

+ 水汽是画在远景雨的plane上，通过找到屏幕空间雨下降方向的反方向在计算深度差，利用深度差做mask（mask区域见图7）在上面做noise闪烁的效果



### _海水_，由多个Feature组成：
海浪示意：![](https://cdn.nlark.com/yuque/0/2024/gif/8418382/1728537435245-25b39bd2-c192-42cc-9332-fffca792c3fa.gif)

+ 海水海浪是用FFT的原理使用Computer Shader计算频谱信息，用三组不同的FFT的顶点偏移最后叠加起来

上岸波浪示意： ![](https://cdn.nlark.com/yuque/0/2024/gif/8418382/1728537711648-c51f039c-9de1-4510-af9e-571bb23705ee.gif)

+ 上岸波浪是用一个plane通过vertex shader来拉扯这个plane
+ 海浪浸湿沙滩是利用Topdown Camera对上岸波浪的plane绘制一张局部的mask图来表示浸湿的区域然后在地面的材质里读取
+ FFT参考文档[coursenotes2004.pdf (clemson.edu)](https://people.computing.clemson.edu/~jtessen/reports/papers_files/coursenotes2004.pdf)，[Ocean simulation part one: using the discrete Fourier transform – www.keithlantz.net](https://www.keithlantz.net/2011/10/ocean-simulation-part-one-using-the-discrete-fourier-transform/)



# Render Pipeline：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728023639845-0980e02a-3701-4ea6-a120-b33bb290f05e.png)

### VRS（Variable Rate Shading）和VRR（Variable Rasterization Rate）：
![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728035507877-c1c8fe0c-c95c-461b-b358-1f1a421a55a4.png)

+ VRS是一种局部的反向MSAA在屏幕上不需要有高频信息的地方用低频来绘制，可以认为是屏幕上别分割成固定大小Tile，可以通过程序去绘制每个Tile分辨率，_Full rate shading (1x1)：_每个像素独立进行着色计算，_2x2 rate shading：_每4个像素共享一次着色计算，_4x4 rate shading_：每16个像素共享一次着色计算 
+ VRS有三种方式Per DrawCall，Per Primitive和Per Attachment
+ _Per Draw Call_ 方式意味着一次 Draw Call 中的所有像素使用相同的着色率，这是 VRS 的最基础模式
+ _Per Attachment_ 模式允许对不同的渲染目标（Render Targets）或附件（Attachment）设置不同的着色率。每个渲染目标通常代表图像的不同组成部分，如颜色缓冲区、深度缓冲区等  
+ _Per Primitive_ 模式是 VRS 的最细粒度应用方式，允许开发者对每个图元（例如三角形、四边形）独立设置着色率。这种方法极大地提升了对场景中局部区域的控制，可以根据图元的重要性或视距来动态调整着色率
+ 钢岚使用的是Per Attachment接口是一张texture2D，VRS_Gen Pass就是生成所需要的Per Attchment的图的信息，标出屏幕上哪个区域的Shading分辨率要降低， VRS 需要支持该技术的 GPU 才能发挥作用 
+ _VRS_Gen Pass__<font style="color:#DF2A3F;"> </font>_是生成所需要的Per Attchment的图的信息，标出屏幕上哪个区域的Shading分辨率要降低
+ VRS 参考文档[VRWorks - Variable Rate Shading (VRS) | NVIDIA Developer](https://developer.nvidia.com/vrworks/graphics/variablerateshading)

**VRR**![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728038218031-b16c6f10-7739-4b53-96e2-f52c4fccf9a7.png)

+ VRR是Metal版本的类似技术，实现方式和VRS完全不同，VRS是减少着色的计算，VRR可以正真降低RT的带宽，VRR需要手动划分屏幕上纵向和横向的Slice，两者相乘得到最终的Tile，用一个二维数组在存取每个Tile纵向和横向的Rate Value，在下图中可以看到接口返回是一个Vector2而不是Void，原因是在输入Rasterization时并不知道最终RT Size的大小，Metal会自动计算插值

![](https://cdn.nlark.com/yuque/0/2024/png/8418382/1728037712997-29efcf4a-8e03-416c-b6e5-a590c9d45c04.png)

_**以上VRR和VRS的接口都是用Unity源码的修改方式增加了Command Buffer的接口**_

### Shadow：
+ 静态 Main Light Shadow 只绘制一次
+ 动态 Main Light Shadow 每帧额外绘制
+ 对于 Local Light（Spot，Point）的Shadow 进行 Cache，运行时会每帧Blit一张新的Shadow

### Opaque：
+ Opaque Pass One 正常绘制不透明物体
+ Skybox Pass 正常绘制天空球
+ Opaque Pass Two 需要天空球深度的不透明物体

### Depth Blit， Exposure，FX Depth：
+ Downsample成1/4在SSAO和Fog中使用
+ Exposure是给一下一帧的 Opaque和Transparent使用
+ FX Depth是为了在绘制DOF是让一些不想被模糊的特效标上深度对于太小像素的半透明需要在After DOF Transparent Pass中去绘制

### 




