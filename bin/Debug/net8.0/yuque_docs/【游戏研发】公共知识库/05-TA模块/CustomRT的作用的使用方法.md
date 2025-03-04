**<font style="color:rgb(0,0,0);">CustomRT介绍</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1720780139833-d2d1348b-020f-468a-a8b2-fde22cb34194.png)<font style="color:rgb(0,0,0);"> </font>

<font style="color:rgb(0,0,0);">根据官方文档的介绍，简单来说CustomRT是一张贴图（RT贴图），可以将分配的材质球（纹理合成材质）的信息输入成一张RT贴图，供其他材质球使用这张贴图，wustomRT最大优点就是可以根据分配的材质球（纹理合成材质）参数变化实时改变。</font>

<font style="color:rgb(0,0,0);"></font>

<font style="color:rgb(255,0,0);">CustomRT</font><font style="color:rgb(255,0,0);"> </font><font style="color:rgb(255,0,0);">的创建注意事项</font>

右键创建Create-Custom Render Texture



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1720780256528-078346cf-ca5f-4b03-85ff-f6f1bf449605.png)

Custom RT默认面板

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1720780276000-d0c4c766-a821-45be-96e6-095f659c7b62.png)		

Custom RT使用参考面板



Size根据项目贴图大小要求设置。Color Format改成R8G8B8A8_SRGB。上面的Material放要用的材质球（脸部纹理合成材质球）。当Source设置为Material时，下面的Material也放一样的的材质球（脸部纹理合成材质球）。把Initialization Mode改成Onload,Update Mode改成Realtime。

<font style="color:rgb(0,0,0);"></font>

<font style="color:rgb(0,0,0);"></font>

<font style="color:rgb(0,0,0);">CustomRT </font>使用过程演示

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1720780435503-94df2deb-a59e-4e4a-af79-a642b2557b9d.png)

角色的材质球用默认材质，主帖图用CustomRT作为贴图的情况下

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1720780467039-eaf05cf1-3a68-424d-85a1-7491aba7c12f.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1720780483275-75bd36e0-4d24-4866-a4dd-082e1cf81a45.png)

修改脸部纹理合成材质球的信息，角色脸上的妆容也会跟着改变



总结：CustomRT是一张贴图，可以将一个材质球的材质信息作为一张贴图实时传入另一个材质球。在捏人项目中，要调节当前角色的脸部妆容位置颜色，要去CustomRT里的材质球里修改材质信息。

