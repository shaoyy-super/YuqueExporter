皮肤shader主要分为5个功能，次表面散射，皮肤高光，模糊法线，细节贴图，清漆图层。

## **<font style="color:rgb(25, 27, 31);">次表面散射	</font>**
次表面散射又分为：

预积分次表面散射（Texture-based Approximation）

物理上更精确的方法（如BSSRDF）

屏幕空间次表面散射（Screen-Space Subsurface Scattering, SSSS)

BSSRDF和SSSS方法没有第一种更可控，所以用第一种方法来实现次表面散射，BSSRDF和SSSS方法在后面会介绍原理怎么实现的。

### <font style="color:rgb(25, 27, 31);">预积分次表面散射</font>
<font style="color:rgb(25, 27, 31);">图像空间的方法和屏幕空间的方法都是通过对原图像模糊再混合达到类似次表面散射的结果的，而预积分的次表面散射这是将散射结果预计算到一张二分法查找表上，方便用户使用。以光照的变量作为索引（横轴竖轴），制成一个查找表（look-up table, LUT）。</font>

<font style="color:rgb(25, 27, 31);">次表面散射的效果主要发生在曲率较大的位置，而比较平坦的地方则不容易出现次表面散射的效果，比如说鼻子的散射效果肯定是要比脸颊高的。而从上图我们可以看到，随着y值的增大，颜色的变化越缓，说明越曲率将作为y值采样这张二分查找表。</font>

<font style="color:rgb(25, 27, 31);">  
</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724124519316-44f93b91-1e69-4f9a-8577-3975cd99e42a.webp)

<font style="color:rgb(25, 27, 31);">如上图所示，左侧是LUT，输入横轴纵轴的值，对应像素的颜色就是diffuse color（当然后续还要乘以物体颜色光颜色之类的）。横轴是N dot L的取值，他表示法线与光线夹角的cos值。图中的这些颜色怎么理解呢？我们忽略纵轴，只看NdotL对颜色值的影响，可以看到右侧NdotL>0时，N和L夹角小于90度，表明光照在物体表面，所以结果是白色；反之，NdotL<0，N和L夹角大于90度，意味着这个点在物体背面，所以光照结果是黑色的。</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724124519413-6a48c293-a7ed-49c0-bcd9-556e3eac8077.webp)

<font style="color:rgb(25, 27, 31);">纵轴1/r表示曲率，对应到右侧的人脸上就是额头、脸颊相对平整，曲率低，而鼻沟、眼皮和耳朵曲率高。曲率高的地方LUT的y值更大，此时NdotL在明暗交界处会偏红，暗部会更亮，也就是更透红光。</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">要注意采样这张LUT图的时候，UV的x值是NdotL*0.5+0.5，因为NdotL是-1到1的值，而y值是曲率图，但是改曲率图是曲率越大越接近1，曲率越小接近0.而正常的DDC软件如SP烘焙出的曲率图是越平坦越接近0.5，越凸越接近1，越凹越接近0。所以要处理一下（将abs(curvature - 0.5) * 2）。也有其他的方法算出曲率，通过以下的算法也能得到曲率，不过计算出的结果难看，还是采用采样曲率贴图的方式（曲率贴图放在mask贴图里，不用单独采样，所以更节省性能）。可以在曲率加上厚度值，让耳朵鼻尖等厚度比较薄的地方在背光的时候更通透。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724125716175-575e2d9e-264e-4f22-92ad-fc91eac687ce.png)

<font style="color:rgb(25, 27, 31);">  
</font><font style="color:rgb(25, 27, 31);"> </font>

得到的结果替换原来Lambert效果则实现此表面散射的效果了。

(如Lit shader，在lighting将lambert替换为SSS)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724127821708-defd190f-0ca8-422c-a3e7-2359a5c3aedc.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724127845200-faf767f0-309d-41d0-831d-6526e15a08c9.png)

### <font style="color:#000000;">BSSRDF 着色模型 </font>
<font style="color:rgb(49, 70, 89);">光线进入 shading point 周围的像素后经过在散射介质内部的随机游走（random walk），可能会从 shading point 射出。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724120160753-d82b0aae-879a-477b-92c9-59c65606baae.png)

<font style="color:rgb(49, 70, 89);">但 SSS 的光线在散射介质中的游走行为难以模拟，为了达成实时渲染的性能要求，便有了以下思路：</font>

+ <font style="color:rgb(49, 70, 89);">提供一个类似于 BRDF 的函数（也就是 BSSRDF），但输入参数从4D（入射方向、出射方向）变成了8D（入射点位置、入射方向、出射点位置、出射方向）</font>
+ <font style="color:rgb(49, 70, 89);">太远的像素对 shading point 的散射光贡献极小，因此可以忽略它们，只对一定范围内的表面 A 进行积分</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724120160735-a18e4f44-c601-4a7e-b88f-be029c0d2a5b.png)

<font style="color:rgb(49, 70, 89);">图左是 BRDF 的行为，图右是 BSSRDF 的行为</font>

<font style="color:rgb(49, 70, 89);">于是便有了基于 BSSRDF 的渲染方程，相当于在 BRDF 渲染方程的基础上增加了对表面的积分：</font>

<font style="color:rgb(49, 70, 89);">L</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">=</font><font style="color:rgb(49, 70, 89);">∫</font><font style="color:rgb(49, 70, 89);">A</font><font style="color:rgb(49, 70, 89);">∫</font><font style="color:rgb(49, 70, 89);">2</font><font style="color:rgb(49, 70, 89);">π</font><font style="color:rgb(49, 70, 89);">S</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">L</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">|</font><font style="color:rgb(49, 70, 89);">cos</font><font style="color:rgb(49, 70, 89);">θ</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">|</font><font style="color:rgb(49, 70, 89);">d</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">d</font><font style="color:rgb(49, 70, 89);">A</font><font style="color:rgb(49, 70, 89);">L</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">=</font><font style="color:rgb(49, 70, 89);">∫</font><font style="color:rgb(49, 70, 89);">A</font><font style="color:rgb(49, 70, 89);">∫</font><font style="color:rgb(49, 70, 89);">2</font><font style="color:rgb(49, 70, 89);">π</font><font style="color:rgb(49, 70, 89);">S</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">L</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">|</font><font style="color:rgb(49, 70, 89);">cos</font><font style="color:rgb(49, 70, 89);">⁡</font><font style="color:rgb(49, 70, 89);">θ</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">|</font><font style="color:rgb(49, 70, 89);">d</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">d</font><font style="color:rgb(49, 70, 89);">A</font>

<font style="color:rgb(49, 70, 89);">然后又基于下列假设：</font>

+ <font style="color:rgb(49, 70, 89);">次表面散射的物体是一个曲率为零的平面</font>
+ <font style="color:rgb(49, 70, 89);">这个平面的厚度，大小都是无限</font>
+ <font style="color:rgb(49, 70, 89);">平面内部的介质参数是均匀的</font>
+ <font style="color:rgb(49, 70, 89);">光线永远是从垂直的方向入射表面</font>

<font style="color:rgb(49, 70, 89);">得出 BSSRDF 函数的形式：</font>

<font style="color:rgb(49, 70, 89);">S</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">=</font><font style="color:rgb(49, 70, 89);">1</font><font style="color:rgb(49, 70, 89);">π</font><font style="color:rgb(49, 70, 89);">F</font><font style="color:rgb(49, 70, 89);">t</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">η</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">R</font><font style="color:rgb(49, 70, 89);">d</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">∥</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">−</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">∥</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">F</font><font style="color:rgb(49, 70, 89);">t</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">η</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">S</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">=</font><font style="color:rgb(49, 70, 89);">1</font><font style="color:rgb(49, 70, 89);">π</font><font style="color:rgb(49, 70, 89);">F</font><font style="color:rgb(49, 70, 89);">t</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">η</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">R</font><font style="color:rgb(49, 70, 89);">d</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">‖</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">−</font><font style="color:rgb(49, 70, 89);">p</font><font style="color:rgb(49, 70, 89);">o</font><font style="color:rgb(49, 70, 89);">‖</font><font style="color:rgb(49, 70, 89);">)</font><font style="color:rgb(49, 70, 89);">F</font><font style="color:rgb(49, 70, 89);">t</font><font style="color:rgb(49, 70, 89);">(</font><font style="color:rgb(49, 70, 89);">η</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">,</font><font style="color:rgb(49, 70, 89);">ω</font><font style="color:rgb(49, 70, 89);">i</font><font style="color:rgb(49, 70, 89);">)</font>

![](https://cdn.nlark.com/yuque/0/2024/jpeg/39137189/1724120160796-9f325824-f580-401b-ab02-29de95acf990.jpeg)

（总结下来就是入射点Po和出射点Pi的亮度关系只跟距离，灯光方向，观察方向，介质的衰减系数有关，这个BSSRDF只是算出距离的衰减系数。可以看下图，出射点距离入射点的距离半径r越大则越暗。）

（颜色关系则光线的波长有关，红光蓝光波长不一样。）

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724121095531-f396d039-0a8c-4b5c-82a9-c4f53be09c79.png)

  
 

### <font style="color:#000000;">屏幕空间次表面散射的 SSS 方法</font>
<font style="color:rgb(49, 70, 89);">重温一遍上面得到的基于 BSSRDF 渲染方程：</font>

<font style="color:rgb(49, 70, 89);">就会发现对表面积分和卷积（模糊）基本就是一回儿事，只不过每个 sample 的权重基本上取决于 diffusion profile，因此我们可以通过 Blur 方法来实现 BSSRDF 的渲染。</font>

#### <font style="color:rgb(49, 70, 89);">纹理空间模糊（Texture Space Blur）</font>
![](https://cdn.nlark.com/yuque/0/2024/jpeg/39137189/1724121851948-0978a1a6-c12f-49d6-9887-98316be07e3a.jpeg)

<font style="color:rgb(49, 70, 89);">总体流程：</font>

1. <font style="color:rgb(49, 70, 89);">（可预计算）计算出一张 stretch texture（拉伸校正贴图），表示每个 texel 应该进行多大范围的 blur</font>
2. <font style="color:rgb(49, 70, 89);">渲染出一张 irradiance texture</font>
3. <font style="color:rgb(49, 70, 89);">纹理空间模糊：</font>
    - <font style="color:rgb(49, 70, 89);">卷积核的权重：由高斯和拟合的 diffusion profile 确定</font>
    - <font style="color:rgb(49, 70, 89);">卷积核的半径：由 stretch texture 提供半径拉伸系数</font>
    - <font style="color:rgb(49, 70, 89);">根据每个高斯核来对 irradiance texture 进行 Blur 得到一些模糊好的 textures 并保存起来</font>
4. <font style="color:rgb(49, 70, 89);">对皮肤 Mesh 进行渲染时：</font>
    - <font style="color:rgb(49, 70, 89);">根据 texcoord 分别采样这些模糊好的 textures 以给定的权重混合起来得到 diffuse 结果</font>
    - <font style="color:rgb(49, 70, 89);">根据每个光源给添加 specular 结果</font>

#### <font style="color:rgb(49, 70, 89);">屏幕空间模糊（Screen Space Blur）</font>
![](https://cdn.nlark.com/yuque/0/2024/jpeg/39137189/1724121852084-66f9df9e-53ba-4d74-bf3c-8e83a9dc3387.jpeg)

<font style="color:rgb(49, 70, 89);">核心思路：</font>

+ <font style="color:rgb(49, 70, 89);">只需要对屏幕中 Stencil 标记过的 Skin 像素进行若干卷积操作，极大地降低了 Blur 的像素数目</font>
+ <font style="color:rgb(49, 70, 89);">卷积核的大小：根据当前像素的深度 z(x,y) 及其深度两个方向的导数来确定</font>



## 皮肤shader的其他功能
因为其他的功能比较细节，远看有没有差别不大，所以都没有实现。简单说一下它们的效果和实现方法。

### 皮肤高光
人们经过测量发现只有很少一部分的光在接触到皮肤表面后会被镜面反射，这种反射光具有若干性质，可以归纳如下：

+ 反射光不带有任何颜色信息，这是电介质作为反射层的固有特性；
+ 反射光中的大部分来自于皮肤最表面那很薄的一层油脂层(Thin Oily Layer)
+ 反射光遵循菲尼尔效应，掠射角处反射量增大

<font style="color:rgb(25, 27, 31);">计算高光项最常用的是phong或者blinn-phong模型。学者通过对皮肤油脂层的建模，设计出了Kelemen/Szirmay-Kalos高光模型，是专门给皮肤的模型。我们从下图可以看到，ks模型在视线和法线夹角更大的时候高光更明显（下边两张图）。根据</font>[<font style="color:rgb(25, 27, 31);">这篇博客</font>](https://zhuanlan.zhihu.com/p/35628106)<font style="color:rgb(25, 27, 31);">的实际体验，使用ks模型，“油腻的师姐”感太强，参数调低效果又不明显。所以phong模型不是全输的，使用哪种模型看自己的选择。  
</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724149696331-3e830cfb-008d-43f6-b6f8-553c7774c85c.webp)



PBR高光模型的一般实现形式，它由直接入射光强度(可能被遮蔽影响)，BRDF项，还有一个余弦函数（ 控制入射光能的衰减强度）组成，具体形式参考如下代码：

```plain
specularLight += lightColor[i] * lightShadow[i] * rho_s * specBRDF( N, V, L[i], eta, m) * saturate( dot( N, L[i] ) );
```

上述代码中的`rho_s`是一个引入的“非物理”因子用于控制整体强度，而`specBRDF`一般是由菲尼尔项`F`，几何遮蔽项`G`，还有法线分布项`D`构成的，需要用到的入参有：法线方向`N`， 视方向`V`，光方向`L`，菲尼尔系数`F0`，以及反应粗糙度的`m`项。

首先是菲尼尔项F，和Disney BRDF一样《GPU Gem 3》也采用了Schlick菲尼尔近似，它有如下代码形式，简单理解就是当视方向`V`越接近掠射角（与微表面法线`H`垂直）所形成的反射光照强度越高。

```csharp
float fresnelReflectance( float3 H, float3 V, float F0 ) 
{   
    float base = 1.0 - dot( V, H );   
    float exponential = pow( base, 5.0 );   
    return exponential + F0 * ( 1.0 - exponential ); 
}
```

另外代码中的F0表示当皮肤遇到垂直入射光照时的反射率，《GPU Gem 3》中使用的是测量常数0.028。

下面是`G`项和`D`项，结合论文 《A Microface Based Coupled Specular-Matte BRDF Model with Importance Sampling》所提出的一种简化版 Cook-Torrance模型BRDF：<font style="color:rgb(64, 64, 64);">  
</font>![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150135157-eeb0a9e8-107a-42b1-9d37-89f41682f3c3.webp)

<font style="color:rgb(153, 153, 153);">Cook-Torrance BRDF</font>

公式中的 `P` 项代表微表面法线分布的概率密度，也就是传统意义上的`D`项。而右侧分子上的`F`就是菲尼尔项，余下的部分是简化后的`G`项，没错，整个Cook-Torrance模型中的`G`项连同 `1/(4(NdotL)(NdotV))`因子被合并简化成了 `1/(hdoth)`，而h代表的是`(V+L)`，既半角向量未被归一化前的样子。

我们现在已经明确`G`项和`F`项的公式，这部分计算可以放在PS阶段处理，一来是因为计算消耗不大，而来也是因为涉及多项入参，特别是光照方向`L`和物体表面的菲尼尔系数`F0`，这些量变化范围广，很难压缩到速查表里。为啥要强调这些呢？那是因为我们希望尽可能预计算公式中的复杂部分，以供实时快速查询和取用，比如余下我们还没说得`P`项（`D`项），因为只涉及到微表面法线`H`一个控制变量，非常有利于我们创建速查表。举个例子（实际处理并非如此）：我们有一张2D的纹理，让`uv`各自代表球面坐标系中的天顶角和方位角，就能模拟出`H`的朝向，然后以`uv`组合为索引，采样纹理获得预计算好的概率密度值。

再来看Kelemen/Szirmay-Kalos在论文中引用的的P项表述，其本质是Beckmann distribution：

<font style="color:rgb(64, 64, 64);">  
</font>![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150152931-7a579648-7523-4c81-8521-a7775224dcc0.webp)

<font style="color:rgb(153, 153, 153);">Beckmann distribution</font>

式中`α`表示宏观法线`N`与微表面法线H的夹角。符号`m`代表当前面元的粗糙度，可以通过对各个方向梯度值求取均方根（root mean square）来得到，但在实际工程中一般是由导入的粗糙度贴图采样获得。所以上式是一个由夹角`α`以及粗糙度`m`一共2个维度变量控制的函数。

We employ a similar approach to efficiently compute the Kelemen/Szirmay-Kalos specular BRDF, but instead we precompute a single texture (the Beckmann distribution function) and use the Schlick Fresnel approximation for a fairly efficient specular reflectance calculation that allows m, the roughness parameter, to vary over the object.

上述片段来自于《GPU Gem 3》，表述了作者对Beckmann分布函数预处理以便生成速查纹理的设想，如下代码同样摘录自书中，是对编码纹理的实现：

```cpp
float PHBeckmann( float ndoth, float m ) 
{   
    float alpha = acos( ndoth );   
    float ta = tan( alpha );   
    float val = 1.0/(m*m*pow(ndoth,4.0))*exp(-(ta*ta)/(m*m));   
    return val; 
} 

// Render a screen-aligned quad to precompute a 512x512 texture.    
float KSTextureCompute(float2 tex : TEXCOORD0) 
{   
    // Scale the value to fit within [0,1] – invert upon lookup.    
    return 0.5 * pow( PHBeckmann( tex.x, tex.y ), 0.1 ); 
}
```

很简单，PHBeckmann方法负责计算Beckmann分布，注意其入参为`ndoth`和`m`，其中`m`不必多说，`ndoth`是宏观法线`N`与微表面法线`H`的夹角的余弦值，具体计算时需要使用反余弦函数`acos`处理出真实`α`夹角。方法KSTextureCompute是对纹理的编码，主要利用了`uv`轴[0,1]区间的特性，将`y=cosα`以及`y=m`在这个区间上展开，带入PHBeckmann方法进行计算。有2点需要注意，其一是粗糙度m需要确保提前转化到[0,1]区间；其二是我们需要将计算结果同样压缩到[0,1]区间中，具体方法参考代码。

实时取用的方法参考如下代码，同样很简单，共分为3个步骤：

+ 首先是准备诸如`h`，`H`以及`ndoth`等中间变量；
+ 其次是采样预计算纹理，注意采样`uv`是`float2(ndoth，m)`，同时注意要反压缩采样获得的数据；
+ 最后是参考Kelemen/Szirmay-Kalos提出的简化版BRDF，装配上`G`项`F`项和`P`项。

```cpp
float KS_Skin_Specular( 
    float3 N,     // Bumped surface normal    
    float3 L,     // Points to light    
    float3 V,     // Points to eye    
    float m,      // Roughness    
    float rho_s,  // Specular brightness    
    uniform texobj2D beckmannTex ) 
{   
    float result = 0.0;   
    float ndotl = dot( N, L ); 
    if( ndotl > 0.0 ) 
    {    
        float3 h = L + V; // Unnormalized half-way vector    
        float3 H = normalize( h );    
        float ndoth = dot( N, H );    
        float PH = pow( 2.0*f1tex2D(beckmannTex,float2(ndoth,m)), 10.0 );    
        float F = fresnelReflectance( H, V, 0.028 );    
        float frSpec = max( PH * F / dot( h, h ), 0 );    
        result = ndotl * rho_s * frSpec; // BRDF * dot(N,L) * rho_s  
    }  
    return result; 
}
```

下图展示了预计算速查纹理（右），以及对人像采样该图后直接输出的效果（左）

<font style="color:rgb(64, 64, 64);">  
</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150208309-3d54c92d-2e2e-4ca0-99ae-a3b138f6dd0b.webp)

<font style="color:rgb(153, 153, 153);">precomputed p term</font>



<font style="color:rgb(25, 27, 31);">除了高光模型的选择，我们还要介绍高光的第二件事，这算是一种从视觉效果出发的渲染技巧了：计算多次高光。如下图所示，计算一遍低光滑度的高光，再计算一遍高光滑度的高光，加在一起。</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724149696307-e901ec22-49c4-48db-8fd8-8a0c965a7f1f.webp)

  
  


### 模糊法线
<font style="color:rgb(25, 27, 31);">关于兰伯特公式：Dot(Normal,Light)</font>

<font style="color:rgb(25, 27, 31);">直接将法线和光线方向代入公式就可以得出结果</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150477219-d8af6162-f291-4ca1-b884-f31f539dfdfd.webp)

<font style="color:rgb(145, 150, 161);">NdotL的结果</font>

<font style="color:rgb(25, 27, 31);">但是结果的暗部细节太明显，像是脸上很多凸起的东西，一般来说，漫反射这一部分低频信息，不需要太多凸起的细节，可以看到下面这张真实照片里的脸并不会有很多凹凸起伏的明暗细节。</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150477261-e5cfea90-587a-4a4b-aab0-3ce91131ef92.webp)

<font style="color:rgb(145, 150, 161);">参照真实照片里的亮暗部分</font>

<font style="color:rgb(25, 27, 31);">其实就是只保留低频信息，不需要那么多凹凸起伏的细节，那就直接用一张模糊过的法线贴图来计算NdotL就好了</font>

<font style="color:rgb(25, 27, 31);">关于模糊的法线，可以在shader里面计算，也可以采样法线贴图的Mipmap高层级，就行了。</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150477276-6c3b82fa-db2d-458e-9a59-f5308ac98174.webp)

<font style="color:rgb(145, 150, 161);">记得打开MipMap选项</font>

```plain
half3 normalMapBlur =UnpackNormalScale(SAMPLE_TEXTURE2D_LOD(_NormalMap, sampler_NormalMap, i.uv.xy,6), _NormalInt);
```

<font style="color:rgb(25, 27, 31);">然后用这个模糊的法线去代入NdotL的计算就如下</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1724150477252-6636a0a8-0559-42a8-a2d4-8f78de022b3c.webp)

<font style="color:rgb(145, 150, 161);">模糊法线代入的NdotL结果</font>

<font style="color:rgb(25, 27, 31);">结果还不错，保留了明暗部分的低频信息，没有那么多凹凸起伏的细节，那就达到我们的效果了。</font>

### 细节贴图
直接用litshader的方法采样细节法线和细节纹理，通过遮罩图分区采样

### 清漆图层
脸上的油脂或者嘴唇的口红会有一点清漆效果

## 皮肤的染色功能
在对永劫无间抓帧分析的时候发现皮肤的BaseMap是常规的BaseMap没有做任何处理，但是在染色的时候能够染得特别白，接近纯白但是还保留了细节信息。

所以猜测皮肤染色的流程为

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724151081132-58273fa0-7309-4e45-9548-44b312f8b753.png)

皮肤并非纯白色（在老化脸中更为明显）。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724151137910-afc922ee-3c6e-4ae5-aa3e-f1a4586d0f6b.png)

实现的逻辑大致是将BaseMap进行灰度转换，然后用Remap进行映射，提取出BaseMap的比较暗沉的地方（色斑等）。作为混合的lerp值，

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724151204600-91e7052c-c9da-4821-ac4b-2476bc1b8189.png)

具体计算如下图

。

```csharp
fixed4 col = tex2D(_MainTex, i.uv);

fixed thickness = col.a;
thickness = thickness*0.5 ;
//将颜色转换为灰度图
half grayscale = saturate(SmoothStep(0.0, _Spot * 0.4 + 0.6,dot(col.rgb, float3(0.299, 0.587, 0.114))));
//将 原图 和 灰度图+原图(使得暗处区域不至于因为灰度不足导致偏色) 进行lerp(灰度与厚度)，颜色越暗沉就越保留,最后将得到带有原图信息的白图进行染色
col.rgb = lerp(col.rgb, saturate(grayscale.rrr+col.rgb*_SpotSaturation), saturate(grayscale-thickness))*_Color;
```

**染色方案2.0**

第一版的染色方法是舍弃掉某些像素，且斑痕不染色，类似低频信息和高频信息取代和保留

研究永劫无间的染色算法，例如染成纯白的方法

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730189816362-fadffbf4-2383-4a68-b616-386766bd6f06.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730189824167-ab23fb9c-9e76-4849-ac5b-fa8ddaf1fad8.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730189836844-c675b304-4316-4228-b796-54f96fd65f51.png)

将diffuse贴图颜色直接乘上CBuffer里的颜色值然后再乘上4.651876

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730189846472-98cf0c33-b6a0-4d21-821b-b6b0e922bd95.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730189842753-fb20b466-749b-44ed-94b7-d2ff10287855.png)

然后将这个4.651876换成1，可以发现输入的纯白的数组并不是让他染成纯白，而是灰色，然后再成上4.651876，将其染色成纯白。

而对于男性的贴图他们的颜色映射关系又有不同了

也就是说我们要将显示给玩家面板的颜色和实际传入shader的颜色进行映射，且对于不同主帖图，不同通道分别用不同的映射关系。第二版的染色方案就是通过曲线工具使用3条曲线rgb分别进行映射，这样的好处就是对美术更友好，相对于公式，曲线的方法比较直观。

