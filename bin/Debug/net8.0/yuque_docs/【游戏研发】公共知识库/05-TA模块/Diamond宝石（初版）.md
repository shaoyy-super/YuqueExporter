### Shader: URP/UT/Scene/Diamond
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730785196131-61f92e29-ace4-46a2-879b-de7d33871185.png)



### 挂载脚本：
在宝石模型上挂载脚本 Diamond Renderer，**<font style="color:#DF2A3F;">有Mesh Filter和Mesh Renderer的这一级</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730785441917-82c2b04b-eb38-45c2-aeee-8b9f9e358ad2.png)

**参数&功能说明：**

**Color: **颜色

**Light Transmission: **视觉效果上看值越小高光颜色越接近Color中设置的颜色

**ColorByDepth: **值越大，模拟深度越深的位置颜色越接近Color中设置的颜色

**Max Reflection: **最大反射次数，值越大切的越碎，性能开销越高，建议不超过4

**Refraction Index: **折射率

**Scale: **脚本自动设置，无需更改

**Shape Tex Location: **形状贴图储存位置，默认储存在 Assets/Art/Textures/DiamondShapeTextures 路径，需确保文件夹存在，否则会储存失败，可自行更改（要改的话目前每个脚本都要改一遍）

**Shape Texture: **形状贴图

**CalculateMesh: **点击后会根据模型自动计算出一张Shape Texture形状贴图并根据设置的**Shape Tex** **Location**路径进行储存，shape Texture是shader计算切割效果的依据



**<font style="background-color:#FBDE28;">步骤：</font>**<font style="background-color:#FBDE28;">先设置</font>**<font style="background-color:#FBDE28;">Shape Tex Location，</font>**<font style="background-color:#FBDE28;">然后点击 </font>**<font style="background-color:#FBDE28;">CalculateMesh </font>**<font style="background-color:#FBDE28;">按钮，再调参，</font>**<font style="background-color:#FBDE28;">需确保Shape Tex Location路径存在</font>**

### 模型要求：
需要有棱有角，过渡柔和的出不来效果，面数尽量不超100，面数越高开销越高



### 材质说明：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730788042551-b1823f44-b101-4e3a-b97f-a47927e914af.png)

**ShapeTex:**

**SizeX:**

**SizeY:**

**PlaneCount:**

以上由脚本控制



**Environment: **环境Cubemap，用于折射效果采样计算（LDR）

**Mip Level: **Environment反射球采样的Mip等级，值越高图越糊

**RefractiveIndex: **脚本控制

**BaseReflection: **基础反射强度

**RefractionBiasAngle(Y):  **Environment反射球绕Y轴旋转的角度，部分角度下折射效果会不太好（基于cubemap），可以用此参数调整到一个合适的角度



**Dispersion: **色散率

**Dispersion(R): **R通道色散偏移量

**Dispersion(G):** G通道色散偏移量

**Dispersion(B): **B通道色散偏移量

**DispersionIntensity: **色散强度

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730791680879-a0e3db06-2b90-4ef4-a058-e0a4d43f3ada.png)



**TotalInternalReflection:  **总体内部反射范围

**specular**: 表面反射强度

**NdotLStrength: **基于表面法线朝向和主光光源的夹角添加一个压暗效果，增加体积感



**Brightness:** 整体亮度

**Power:** 用Power方法对整体进行压暗，亮度越高的地方压暗程度越小，亮度越低的地方压暗程度越大

**Enable Tonemapping:** 启用后期调色

**Contrast: **对比度

**_Disaturate:** 去色

**Min:** 最小亮度

**Max: **最大亮度

**PostExposure:** 整体曝光度 



**ReflectionCube:** 反射Cubemap（表面）（LDR）

**NormalMap:** 法线图

**SpecularMap: **高光颜色图（也可以当遮罩来用）

**ReflectionMask: **反射遮罩图



