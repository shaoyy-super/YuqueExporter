<font style="color:black;">需要使相关object受雾效影响，需要调用相关类并改写shader</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299615863-868388fc-9cf7-4220-8625-85959bbb8be8.png)

1.调用<font style="color:black;">EnviromentFogParameter类，获取参数列表和混合计算</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299463430-ba71868a-81a0-40c6-b733-83bb279460f6.png)![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299466571-593fc1f9-ca36-4c8d-b5bf-5cbe022dcc7f.png)

2.用<font style="color:black;">LightSettingVolume类，将EnviromentFogParameter类实例化fogParameter并将参数传入公共列表paramList</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299489917-641630cd-ec88-4b56-9c00-e4f64a60ff01.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299494546-5f52beb1-914c-409d-9b95-cc2ea8aef9c2.png)

3.调用<font style="color:black;">MoleVolumeRenderEvent类，将EnviromentFogParameter类实例化fogVolume并进行全局声明</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299515131-49fd3d71-fad1-41a7-bcbc-a44b487672f9.png)

4.<font style="color:black;">调用FACustomFogLib.hlsl，引用全局声明的参数进行雾效计算，返回fogColor和fogFactor变量</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299534159-20fa935e-b686-4c5d-b9bf-aa5a47d865b7.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299539724-0ddf5788-4e3a-4e29-a39d-690243f8cc2d.png)

5.<font style="color:black;">根据需要在shader片元着色器输出阶段对最后输出的颜色加入变量fogcolor进行插值计算</font>

以FASceneObject.shader举例

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299566069-59405761-696f-40ee-a14f-7e117350d88e.png)

全流程图

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719299576402-f1ed6fee-b958-4482-a429-bff73b0317a0.png)

