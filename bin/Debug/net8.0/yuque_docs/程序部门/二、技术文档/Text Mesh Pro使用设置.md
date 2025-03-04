# 1、创建Font Asset
![](https://cdn.nlark.com/yuque/0/2024/png/43256857/1727408137236-4da5c845-6cc5-46d2-a20c-7eb50bfcf4dd.png)

**Sampling Point Size：**通常[40, 50]就可以得到比较不错的效果；要等到更好的效果，设置在[60, 70]也够了

**Padding：**使用SDF[x]模式时，padding决定材质的效果的影响范围（如softness、outline thickness、shadow offset等）。padding值是像素为单位的，它影响的结果是相对于sampling point size算百分比。通常留10%的padding足够使用。

padding过小，会出现两个问题

    - 字周围有半透明的色块；
    - 字的边缘出现其他字的图像（多个字的图形混到1个字的显示范围了）

**Render Mode：**<font style="color:rgb(51, 51, 51);">SDF系列，如SDFAA、SDFAA_HINTED</font>





# 2、字体材质设置
[Distance Field / Distance Field Overlay Mobile Shaders | TextMeshPro | 4.0.0-pre.2](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/ShadersDistanceFieldMobile.html)

### 创建Material Presets
将Font Asset下的材质球，复制一份出来，保证材质的文件名中包含Font Asset的名字即可



##### Debug Settings
1）Gradient Scale：用途未知，值测试下来跟Padding保持一致比较合适

2）Scale X / Scale Y / Sharpness：测试下来，都会影响半透方块的问题。（效果类似锐化）

![Game窗口字周围出现半透色块；Scene里字缩的比较小也会出现](https://cdn.nlark.com/yuque/0/2024/png/43256857/1727429710468-ff7acd3a-c486-4e8a-aa2c-736b39a01788.png)





