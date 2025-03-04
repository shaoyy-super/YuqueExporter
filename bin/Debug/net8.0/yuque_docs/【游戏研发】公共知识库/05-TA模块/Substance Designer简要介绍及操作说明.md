# **一、定义**
Substance Designer是用来生成程序化纹理的。Substance文件根据参数和输入生成一幅或多幅 2D 图像。当进行更改时，Substance Engine会不断计算结果，可以实时看到修改结果。



# **二、基本介绍**
+ **界面**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563657108-a58e9a9f-474b-4aa3-acaf-edeae8454b10.png)



+ **浏览器**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563711350-f4ae84bf-e477-4193-bdf0-485577a16231.png)

管理包、图形、其他资源的地方，可用于发布、导出图形。

函数图是Substance Designer 中最重要的概念之一，它通过节点和连接线构成了材质的生成流程。



**Substance图形**：SD原生创建的材质球。

**MDL图形**：Nvidia开发的一种基于物理的材质描述语言，允许其他支持MDL的软件(如SP、NVIDIA Iray、Chaos的V-Ray、Megascans资产库)识别或使用，跨平台性更好。在SD中创建，然后在工作流的其他所有程序中使用该材质。但目前比较新，有待验证，2022年全面开源，包括glsl后端。



+ **图形**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563759796-72300f6b-ba59-44e0-a280-730745475a2f.png)

    - 数据从左向右流动;
    - 可以使用将任何图形用作另一个图形内的“图形实例”。通过将图形拖动到另一个图形上来创建图形的实例。此工作流程允许您重复使用常用的节点链;
    - 公开参数;



+ **属性面板**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563786832-35996a33-4b17-4aeb-b5e4-5bbe8d2ccd36.png)

调节各种属性，设置通道、名字的地方



+ **2D视口**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563820084-cb881ba0-2d27-49c6-a28a-be01c52b451a.png)

查看当前节点的输出贴图、保存成图片、查看不同通道内容



+ **3D视口**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563838175-752e904b-e418-43b1-b2b3-ac02027e0d99.png)

预览材质在一些基础模型上的效果，类似于Unity中的材质球预览窗口。

可以调整预览窗口的一些环境设置，如灯光、相机、背景（天空盒）、使用的模型。也可以调节使用的着色器设置。



+ **库面板**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563849495-548d3058-95c6-46b9-8672-5bdf5c1e3ce4.png)

存放全部的资源。



# **三、重要节点**
节点库包含了 Substance Designer 中所有可用的节点，根据功能可以分为以下几类：

**输出节点**: 输出最终的材质结果。

**生成器节点****:** 生成各种程序化纹理。制作纹理基本结构、噪点、图案、平铺等。

**滤镜节点**: 对纹理应用各种滤镜效果，如颜色调整、变形、混合等。

**工具节点**: 提供各种工具函数。

#### **1.**** ****形状****节点**
**Shape、Fibers等**: 生成各种几何形状，例如圆形、矩形、多边形等。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563874586-6cc3300c-835b-4569-ab18-3528381db580.png)



**各种Gradient**: 生成线性、径向或角度渐变。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563883294-2f002c61-49ec-4824-8236-e5d17ec71612.png)



**各种Noise**: 生成各种类型的噪声纹理。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563892364-ee7a823c-ec20-4dc0-ae9f-dd71ffd29e47.png)



**Tile Sampler(Generator)**: 生成各种类型的平铺纹理。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563897828-c49147ba-76d8-49c5-93e0-c0f361b4e89c.png)



#### **2.**** ****颜色节点**
**均一颜色Uniform Color**: 生成单一颜色或灰度的纹理，常作为灰度配合混合节点用来调整其他贴图的强度。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563906253-6cc04add-c9bc-47b8-aa28-d9fc4d675bd9.png)



**RGBA Split/Merge**: 用于拆分或合并贴图通道。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563933269-109df2d3-4d28-4400-9843-2f6ba5c09bdf.png)



**色阶Levels**: 调整纹理的色阶。



**Histogram Scan/Range**: 直方图扫描，常用于重映射输入灰度图像的对比度和亮度。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563944526-d205fa98-febc-4c59-9cc5-b516077fc699.png)



**灰度转换**：将颜色贴图转换成灰度图。



**混合Blend**: 将两张纹理进行混合，支持多种混合模式。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563952614-9994d8bf-1486-4b60-b975-45c179506955.png)



**反向、colorspace转换**等等

#### **3.**** ****变换节点**
**Transform 2D**: 对纹理进行平移、旋转、缩放等变换。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563959882-a3003f2c-a546-48d6-b128-dff4eb53c14f.png)



**Crop Color/Grayscale**: 裁剪贴图。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563979441-a4568536-a197-45b4-904b-a63bef094881.png)



**Non-Uniform Dir.Warp Color/Grayscale**: 方向扭曲。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563982549-c2af4cff-0e2e-4e6e-b919-6d3a9ed97e39.png)



**Make It Tile Patch**: 通过边缘模糊将一张图强行修成四方连续。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722563991904-f0fe4cd5-0e39-421f-8c6f-7425992f6e93.png)



#### **4.**** ****法线相关****节点**
**Normal Intensity**: 对法线强度进行调整。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564008035-d39c1e87-aaa4-45a9-b228-d013927baa12.png)



**Normal Invert**: 法线某个通道反转。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564014501-cbe49ccb-625a-4e25-b220-60ba4e2fb71a.png)



#### **5.**** ****高级节点**
**像素处理器**：函数节点，可以自定义逻辑计算，修改图形。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564027432-8f8279d3-a0e0-4520-a46a-cbf386cfde83.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564034115-b06bc234-d933-4bce-a7b5-98feaf7e96e9.png)



**值处理器**：函数节点，生成值，可用于计算变量，并绑定节点的参数。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564057721-649c4a88-cdde-4667-9a97-a20e60647167.png)



# **四、重要操作**
### **- 新建材质**
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564079310-3c9261a3-e5ac-4fa8-8e1e-87911e587058.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564086773-a9e55a0f-5a79-450e-8fd7-c75505f9f2ca.png)



### **- 新建SP用的filter**
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564105525-e63e5c66-c5d1-4b02-a0b6-7b495dd4d03f.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564106569-397ca021-5dfc-45a3-8011-42f475775a48.png)

通道命名得和SP中的通道名严格对应，对于SP中的用户通道来说，得使用通道名，而非自己自定义的Label通道标签。SP提供了重命名通道的功能，但那改的其实只是标签，而非通道名。

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564108653-524d4690-ae53-48a5-9640-511cdf1abc14.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564115295-0939bb12-9a09-4ba3-9231-be0269488b83.png)



### **- 编辑像素处理器**
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564128496-4e0dd7b1-7714-43ff-bbd0-8d09161e7e09.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564135663-0da6d366-73c4-4e14-8d49-98b2ddbba1b2.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564139777-2abd1029-f483-49aa-9b3c-25f3a6af1efe.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564143956-5cec988f-75ae-4831-8eac-5323bbee7fff.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564146874-eab18ca0-1439-4a36-a154-b661a0fb8117.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564149131-87bfc776-8d8b-4a8f-8661-27d678c62023.png)



### **- 公开参数**
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564168156-1644ad3e-e6e4-48c5-b2ef-ed92a7bc1e83.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564173170-e8b6698f-6ad1-4b48-9aa1-3a2a65a95b38.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564189205-0dde4c2b-5803-4ce2-9b77-35659607dc6a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564201517-ddda8690-bc80-4cc9-b05b-95c10ef84268.png)

### **- 预览调整参数**
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564198996-ae78a5ec-3f00-4c9f-b01d-18c1e4682700.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564200859-b2bbb154-2775-41b2-af58-8149f4d4a6b9.png)



### **- 发布、导出**
1. **Substance 文件 (SBS)**

Substance 文件是 Designer 的主要来源文件。当您打开 Substance 文件时，您可以查看和编辑图形中的所有节点。它们表示为可以包含任意数目的资源（例如图形、函数、位图和网格）的包。

> 属于生产文件，拥有此文件即可对材质做二次修改、发布，使用、修改方便，但是没有安全性。
>



2. **Substance 存档 (SBSAR)**

Substance 存档是经过编译和优化的 Substance 文件。它们的计算速度比 SBS 文件快得多，并且可以跨应用程序使用，而不会出现引用问题。您仍然可以调整参数，但是不能编辑图形。

> 属于发布用文件，拥有此文件可以使用该材质，但是没办法对材质做二次修改、发布，只能调整其公开参数来实现微调材质效果。一般对外发布的就是这种文件。
>



3. **标准图像格式（TGA、PNG...）**

Designer 支持导出为许多标准图像格式。导出到标准格式以将资产引入到支持它们的任何应用程序中。图像是静态的，因此当您从 Designer 中导出图像时，会失去任何程序化、参数化、非破坏性功能。



+ 发布为.sbsar文件
+ 发送至Substance Painter

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564241552-703478d6-cd5c-4689-96dc-8e2454976352.png)



# **五、工作流示例**
### 简单制作一个织物SD材质
**新建材质**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564315108-f2cd608f-5c7c-480e-adbd-c3805931425c.png)



**制作基础纹理结构**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564302177-18c1e0fb-eff0-43dd-b992-8287aa6d2d03.png)



**给纹理加点噪声、污渍等细节**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564305127-b297041d-a7d5-4fd0-8aae-7162ff96a928.png)



**合成基础结构和细节**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564312245-b90fb5ce-8f5e-4a07-8129-66f6ce7b18b3.png)



**平铺后，准备处理成灰度图用于生成高度、粗糙度、颜色等贴图**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564321992-e403a2db-a691-45ca-88de-ea84ff7b2789.png)



**生成能染色的贴图和偏白的贴图**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564311463-e74baec7-6f1e-4474-a2b2-70774616101d.png)



**生成粗糙度、金属度（因为织物没有金属度，所以直接提供一张公开参数的灰度图，来实现让用户手调数值来控制整体金属度）**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564315278-cf33296b-b481-4949-9516-a4527d3a5e56.png)



**法线、高度、AO均由高度图生成**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564319571-b49cf16b-b1b0-4a88-88ab-723aa9e4a0dc.png)



**这就是一个偏小型但完整的SD材质制作**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564327211-fefd48ef-0407-45bb-9c4d-5fa9922503ed.png)



### 将一张细节法线贴图转换成SD材质
**导入细节法线贴图，拆分出有用通道，重新拼成真的法线**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564400699-c51d373f-75a5-4b0f-a3e3-499ca4a9d5e9.png)



**由于金属度、粗糙度、颜色均是由用户手调数值控制，且没有贴图槽位，所以那几个通道无需写入内容，直接给张公开了参数的均一颜色节点即可。作为SD材质来说，只有当Unity的Shader提供了对应内容的贴图槽位时，SD往该通道写入内容才是有意义的，否则直接给张纯色图，然后公开参数即可。**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564401232-fde9b1c7-f0e2-4866-88e0-85e42c9cf0e2.png)



**发送到SP后，可以看到如下效果**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564408655-81275062-61b9-45ac-9abb-c01030d61813.png)

**之前输出颜色时，分别提供了一张支持染色的base color，一张不支持染色base map。在sp中默认会使用支持染色的base color，如果要使用base map，则需要在通道映射功能中，将SP接受的Base Color通道映射到材质的basemap通道上，这样sp中这层材质就会使用basemap的内容，所以可以看到下面的Color设置失效了。**



![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722564408902-a97eb518-580d-4088-90aa-308df6e9b0a5.png)

**将Base Color通道改回成base color通道后，就会发现下面的Color设置又起效了。至于金属度、粗糙度、法线强度则一直都有效，除非像base color一样，强行修改对应通道的通道映射。**



# **六、参考**
1. [https://creativecloud.adobe.com/learn/substance-3d-designer/web/substance-designer-for-beginners?locale=en](https://creativecloud.adobe.com/learn/substance-3d-designer/web/substance-designer-for-beginners?locale=en)
2. [https://creativecloud.adobe.com/learn/substance-3d-designer/web/substance-3d-designer-keyboard-shortcuts](https://creativecloud.adobe.com/learn/substance-3d-designer/web/substance-3d-designer-keyboard-shortcuts)
3. [https://creativecloud.adobe.com/learn/substance-3d-designer/web/intro-to-functions](https://creativecloud.adobe.com/learn/substance-3d-designer/web/intro-to-functions)

