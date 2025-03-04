头发shader比较特殊的地方就是光照模型和半透问题的处理上。

目前使用的是Kajiya-Kay模型的漫反射和YYDhair的高光计算。

## 光照模型
### Kajiya-Kay模型
Kajiya-Kay是一个各向异性的线/纤维光照模型，它使用了头发的切线，而不是法线来进行光照计算。假设头发的法线位于切线和视线范围内的平面上。

Kajiya-Kay模型的原始论文：https://www.cs.drexel.edu/~david/Classes/CS586/Papers/p271-kajiya.pdf论文的内容主要是使用3D纹理渲染毛发，关于Kajiya-Kay模型，只需要看Lighting model for hair这一节即可。

Kajiya-Kay中，头发丝被视作圆柱体。光照模型包含两个部分，分别是**漫反射项**和**镜面反射项**。漫反射项本质是Lambert模型在微小的圆柱体的应用，镜面反射项则是与Phong镜面反射模型相似的，同样为圆柱体做调整的特殊模型。

如下图所示，介绍Kajiya-Kay模型中用到的数学表示，向量均为单位向量。

t——切线

l——光照向量

e——观察向量，应该用v更合适

x0——圆柱上的任意一点，也就是发丝上的任意一点

P——垂直于t的屏幕，也是l‘的投影面

l'——l在P上的投影

b——与t，l'垂直的向量

t，l'，b为基向量(basis vector)，他们互相垂直

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724308173189-c5a694d0-9895-42e3-b0a0-7b37bf5f9dbb.png)

计算三个基向量，用于之后的计算。

t——模型切线，资源给出。

l'——normalize(l-dot(t,l)*t) ，l减去l在t上的投影向量，并进行单位化

b——cross(t,l')，t和l’的叉积

之后，我们给出P在x0处圆柱的切面，用于分析漫反射项的计算。

n(θ)——角度为θ的法线向量

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724308173370-98bc80aa-9285-4932-8b08-6a266f0cdf15.png)

**漫反射项的计算**

我们知道，光在平面上的一点产生漫反射，方向范围是一个半球，在切面上，就是一个半圆。也就是说，我们只需要在图中0到π的角度范围中计算漫反射项。

首先，根据Figure7，我们能得到半圆上的法线分布方程：

n(θ)=bcos(θ)+l'sin(θ)

假设漫反射系数为kd，那么漫反射项就是l·n在0到π的定积分*kd。经过数学计算，我们可以得到漫反射项的计算结果：

Kd*sin(t,l)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724308173265-74ed601e-c08a-4f2b-be32-9962ff479f72.png)

**镜面反射项的计算**

Phong的原始公式是镜面反射系数*pow(cos(V,R),镜面反射指数)。在Kajiya-Kay模型的镜面反射公式中，将反射向量R用e'来代替。

这是因为在Kajiya-Kay模型中，光照击中头发之后，反射方向是沿着切线而不是法线以镜面反射角度射出的，这样所有的反射光线均位于以切线为轴线，角度为θ（光照与切线之间的夹角）的圆锥体上。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724308174665-2d34a0e0-bbee-4f49-be3a-8c1199b1320b.png)

e'是被包含在圆锥体中的最接近观察向量的镜面反射向量，如下图所示。

可以理解为e'是e在圆锥体上投影

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724308173215-a858dcb6-56c3-4aff-9de7-1ae16de269fc.png)

### Marschner模型
Marschner是一个基于头发散射属性测量而得到的模型，模型本身比较复杂。这里采用了Marschner模型的两个观察结果，主要考虑了头发生长方向对高光的影响，Paragon的头发采用的更复杂。

1.主要的镜面反射高光向发梢偏移。

2.次要的镜面反射高光向发根偏移，且有色。

这是因为：

主要的镜面反射高光是R（反射）光路的表现，直接反射了光照。

次要的镜面反射高光是TRT（透射-反射-透射）光路的表现，它透射进头发内部（折射+吸收），在头发内部又反射（反射多次，这里只考虑一次）到空气中。在这个过程中，光线从头发透射出去的位置已经与光线击中头发透射进的位置产生了偏移，同时因为光线在头发内部散射而产生了颜色。

![](https://cdn.nlark.com/yuque/0/2024/jpeg/39137189/1724308548712-6afd85df-a604-46b9-a189-b1e462f80528.jpeg)<font style="color:rgb(0, 0, 0);background-color:rgb(245, 245, 245);"> </font>

<font style="color:rgb(0, 0, 0);background-color:rgb(245, 245, 245);"></font>

——Shader拆解

顶点着色器：将切线，法线，观察向量，光照向量，环境遮蔽项传递给片段着色器。追求效果可以考虑逐像素计算部分向量。

片段着色器：

1.漫反射照明：在没有适合的自投影下，Kajiya-Kay模型的漫反射项sin(T,L)看起来过亮，这里使用调整的N·L项。

2.两个偏移的镜面反射高光：主要镜面反射高光和次要镜面反射高光。

2.将以上各项合并到颜色输出。



——偏移镜面反射高光

为了沿着头发长度偏移镜面反射高光，我们将切线向着法线的方向微移。

假设切线T是从发根指向发梢的向量：

—正的微移值向发梢移动高光。

—负的微移值向发尖移动高光。

从Specular Shift Texture中查找偏移值，避免头发面片上一致的表现，增加随机表现。

使用半角向量来计算发丝的镜面反射高光：

—使用反射向量和观察向量会增加一点着色器的复杂度。（使用IBL可以完全无视）

两个高光有不同的颜色，镜面反射指数和不同的偏移的切线。

使用Specular Noise Texture来调整次要高光。



<font style="color:rgb(0, 0, 0);background-color:rgb(245, 245, 245);"></font>

### 光照模型代码
```csharp
float3 KajiyaKayDiffuseAttenuation(float3 BaseColor,float Scatter, float3 L, float3 V, half3 N, float Shadow)
{
	// Use soft Kajiya Kay diffuse attenuation
	float KajiyaDiffuse = 1 - abs(dot(N, L));

	float3 FakeNormal = normalize(V - N * dot(V, N));
	//N = normalize( DiffuseN + FakeNormal * 2 );
	N = FakeNormal;
	// Hack approximation for multiple scattering.
	float NoL = saturate((dot(N, L) + 1)*0.25);
	float DiffuseScatter = (1 / PI) * lerp(NoL, KajiyaDiffuse, 0.33) * Scatter;
	float Luma = max(Luminance(BaseColor),0.01);
	float3 ScatterTint = pow(BaseColor / Luma, max(1 - Shadow,0.01));
	return sqrt(BaseColor) * DiffuseScatter * ScatterTint;
}


half3 HairSpecularShading(half3 BiTangentWS, half3 V, half3 L, half3 NormalWS,half3 SpecularColor, half LightStrength, half LightExponent, half LightPosition, half lightAttenuation)
{
    half3 L0 = half3 (normalize(L).x , ( normalize(L).y + ( LightPosition) ) , normalize(L).z);
    half3 V0 = normalize(V+L0);
    half VdotB = dot(V0,BiTangentWS);
    half Lambert = dot(NormalWS,normalize(L));
    half Specular = saturate(pow(sqrt(1-VdotB*VdotB),LightExponent))*LightStrength;
    half3 FianlSpecularColor = SpecularColor * Specular * smoothstep(0-1,1,VdotB)*saturate(Lambert*Lambert*Lambert)*lightAttenuation;

	return FianlSpecularColor; 
}
```





## 半透问题处理
两个pass操作，第一个只写入深度不写颜色，第二个正常半透明不写深度

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724402320455-52aa492a-64cd-4ecc-ab52-0b85fabff97c.png)

<font style="color:rgb(77, 77, 77);">关闭深入写入后会引发错误的排序情况。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724318595037-3338a74a-a99a-4620-900a-098b1ddd9408.png)

<font style="color:rgb(77, 77, 77);">开启深度会导致当前drawcall只会渲染深度最近的片元</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724320360017-43649125-40a9-490f-bcc6-5934b410a0e4.png)



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724320021176-9df00557-a0b5-44bd-aacf-a033bd8dd5a6.png)

通过alphaClip让深度信息尽可能写对，表现效果差强人意，会出现深度信息没对，或者锯齿感太严重和不透效果一样的问题。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724320591322-cdf0b513-09bf-4497-abdf-3078a7388c07.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724320601663-4cc0d783-9212-4177-96fd-64d6400bbfc3.png)

采样预写入深度的效果

先写入进行alphaClip后的深度，再写入颜色信息，因为半透明pass不写入深度，所以所有的片元都会和预先写入的深度进行深度测试。错误的<font style="color:rgb(77, 77, 77);">排序因为深度测试没过就不会被写入颜色里。只要alphaClip的值处理好就不会出现上述的问题。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724320937119-a33fea65-2db2-4633-8afd-d4e7f6fac6e0.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724320951810-626293ba-a73b-4e9f-a288-185f8c500b95.png)

过渡的样子也非常的完美（两个pass！！只能在特别近处可以使用）

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1724321388919-d02f5d80-6831-4144-82ed-5e27724db7da.png)



