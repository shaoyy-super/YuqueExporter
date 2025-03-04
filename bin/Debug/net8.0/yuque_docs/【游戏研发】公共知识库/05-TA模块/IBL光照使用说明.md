用于间接光部分光照计算

![仅IBL间接光效果（含SSAO）](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738992570617-4d58b58b-f019-4a21-8d53-66c01f823f38.png)



## 使用说明
![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738992744959-ccf8d245-24f4-424a-bf29-4f76d1483a3d.png)

在Volume中加载Environment Volume，开启 useEnvironmentLighting总控开关

**设置天空盒：**

开启改写天空盒材质开关，置入自定义天空盒材质

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738993202547-d217285c-766d-40cd-89ec-ade241afd3d8.png)

注：

用于天空盒的Cubemap，**Texture Shape **需设置为** Cube**, **<font style="color:#DF2A3F;">Convolution Type</font>**** **需设置为 **<font style="color:#DF2A3F;">Specular</font>**

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738993001749-5dd459a8-a201-4bb2-8d7b-74ef25c631b3.png)



**设置间接光漫反射：**

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738993193522-655b4e51-334b-4b88-977c-41886e3e11e4.png)

开启改写环境光开关

**环境光来源**为Skybox时，会基于天空盒Cubemap预计算辐照度，作为环境光漫反射；来源为Gradient和Color时，使用自定义颜色作为环境光漫反射；一般推荐使用Skybox模式，有更好的美术效果

**强度倍增**参数可以调节环境光漫反射的强度

注：

首次切换天空盒材质球或调整漫反射强度时会触发预处理漫反射球谐矩阵计算，右下角出现图示读条时说明正在预计算，计算完成后会应用回正确参数

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738993610652-392121f4-043f-470a-828e-f90f2155e39d.png)



**设置间接光镜面反射：**

![](https://cdn.nlark.com/yuque/0/2025/png/45354151/1738993901972-4d0576bc-6102-4769-a8cb-ecc01be9ddaa.png)

开启使用反射开关

**反射来源**为Skybox时会使用天空盒Cubemap作为环境光镜面反射来源，决议（翻译问题，实际是分辨率）可以选择使用天空盒cubemap的最大分辨率，不能超过贴图本身大小，不能超过1024，在此基础上能满足美术效果的前提下越小越好；反射来源为Custom时，可以置入自定义Cubemap作为环境光镜面反射来源；**当场景内烘焙了反射球时，反射球影响范围内的物体会使用反射球作为镜面反射来源**

**反射强度倍增**参数可以调整镜面反射的强度

