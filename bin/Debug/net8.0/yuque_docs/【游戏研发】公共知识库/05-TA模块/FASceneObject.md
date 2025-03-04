URP/MoleGame/Scene/FASceneObject



**基础参数：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730183532998-39af7a04-e815-4936-bd1d-3ae50f84c4f5.png)

**RenderingMode:   **混合模式，选择不透，AlphaClip、或是半透

**Fog Mode:**   雾效模式，HeightFog：完全受EnvironmentVolume雾效参数控制；CustomHeightFog: 允许为材质单独调整雾效强度

**Cull Mode:**   剔除模式：剔除背面，剔除正面或是双面显示

**ZTest:**   深度检测模式，一般不用调整

**Color:   **主帖图颜色调整

**MainTexture:   **主帖图（颜色贴图），RGB: 颜色；A: Alpha

**MainTextureUVType:   **主帖图使用UV选择（UV1,UV2,世界空间坐标采样，默认UV1）

**Cutoff:   **RenderMode为cutout时，Alpha低于该值的部分认为是全透，高于该值的部分认为是不透

**Comp:   **Stencil测试模式，一般不用调整

**Pass:**   Stencil测试通过后对Stencil的写入逻辑，一般不用调整

**Stencil Ref:**   Stencil值

**AO_Metal_Smoothness Texture:**   Mask贴图，R: AO; G: 金属度; B: 光滑度

**Smoothness Texture UV Type:   **Mask贴图使用UV选择（UV1,UV2,世界空间坐标采样，默认UV1）

**Smoothness Tilling And Offset:   **Mask贴图UV平铺合偏移值调整，R: U方向Tilling; G: V方向Tilling; B: U方向偏移; A: V方向偏移

**AO_Metal_Smoothness Scaled:**   Mask各通道参数倍率调整；R: AO; G: 金属度; B: 光滑度

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730871940929-4ee7ff2d-bdd2-4235-84f6-4c3631e140c3.png)

**Light Map Intersity：**接受BakeMap的强度

**Use Character Ambient Color:**   开启后环境光计算会替换为CharacterVolume中的角色环境色

**自定义环境球： **启用后使用自定义Cubemap作为反射球

**反射球贴图:   **自定义Cubemap

**Environment Reflection Intensity(环境球反射强度）：**环境球反射强度

**Ambient Map Rotation Angle(环境球绕Y轴旋转角度)：**环境球绕Y轴旋转角度，1等于1°，360旋转一周

**Main Texture As Emission Texture:   **启用后使用主帖图作为自发光贴图

**Emissive Intensity:   **自发光强度

**Emission Color:   **自发光颜色

**自发光/Mask调色： **启用后使用自定义自发光图片来实现自发光效果

**Emission Texture:    **自定义自发光贴图，RGB: 颜色； A：自发光遮罩

**Use Emission Alpha Mask:    **启用后根据自发光贴图A通道的自发光遮罩选取自发光区域

**Emission Mask Color:    **自发光颜色

**Emission Mask Color Intensity:    **自发光强度

**Normal Map:**    法线贴图

**Normal Scale:   **法线强度

****

**细节法线：**

** **![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730184111672-fcce9808-0831-4b95-b5a1-2652eed7ecb4.png)

**Normal2 Map:**    细节法线贴图

**Normal2 Scale: **   细节法线强度



**脏渍：**

叠加一层layer在当前效果之上

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730184247228-221e975b-64d9-4d8e-b7dc-ceab40cc070f.png)

**Dirt Color:   **脏渍颜色

**Dirt Texture:   **脏渍贴图，RGB: 颜色； A: Alpha

**Dirt Metal:   **脏渍金属度

**Dirt Smoothness:   **脏渍光滑度

**Dirt Normal Map:   **脏渍法线贴图

**Dirt Normal Scale:   **脏渍法线强度



**顶部混合：**

在物体Y轴朝上的地方混合另一层Layer，eg.物体上的积雪

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730184663559-51cf78a2-5933-45a7-aee4-82d0adc64b7f.png)

Top Color:   顶部混合Layer层的颜色

Top Texture:   顶部混合Layer层的颜色贴图

Top Metal:    顶部混合Layer层的金属度

Top Smoothness:    顶部混合Layer层的光滑度

Top Normal Map:    顶部混合Layer层的法线贴图

Top Normal Scale:     顶部混合Layer层的法线

Top Mask Texture 2 With UV2:    启用后使用Mask图UV采样Top Texture，默认关闭时使用主贴图UV采样

Top Intensity:    顶部混合Layer层的强度（类似Alpha）

Top Power:    顶部混合Layer层的过渡强度，值越大越只有法线朝向越接近y轴正方向的部分才会有混合效果

Top Offset:     保留过渡强度效果，扩大混合范围



**溶解：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1730203918366-5bf8c35f-47e5-4c96-9e64-8245ca30b903.png)

**Dissolve Percent:    **溶解百分比，0为全溶，1为不溶

**Dissolve Mask:    **溶解遮罩图

**Dissolve Texture Channel:   **  选择使用溶解通道图的哪一个通道作为溶解Mask

**Dissolve Mask UV Type：   **选择溶解遮罩图使用的UV类型（UV1,UV2,世界空间坐标作为UV）

**Main Texture Sample Direction:     **仅使用世界空间坐标作为UV采样时有效，下4参数同，选择采样平面

**Dissolve Mask Texture Rande X Mask:     **U方向上的缩放

**Dissolve Mask Texture Rande Y Mask: 	    **实测同上

**Dissolve Mask Texture Rande Z Mask:     **V方向上的缩放

**Dissolve Mask Texture Rande W Mask:     **实测同上

**Ramp Mask:     **溶解边缘渐变遮罩

**Ramp Texture Channel Mask:   **溶解边缘遮罩使用通道选择

**EdgeColor Length: **   溶解边缘宽度

**Edge Color: **   溶解边缘颜色





