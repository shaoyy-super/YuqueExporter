### 1.使用方法：
**Amplify Imposters：**

要使用的Amplify Imposters的物体都挂载对应的脚本，且进行烘焙

**Runtime Imposters：**

[优化插件RuntimeImposters使用说明](https://snh48group.yuque.com/dpatt3/eb0114/qkpz70gqlh8us80p?singleDoc#)

### 2.shader兼容性：
**Amplify Imposters：**

对自定义shader不支持，需要类似延迟渲染管线的数据，将Albedo、Normal、Specular、Smoothness、Emission、Alpha、<u>Depth</u>等信息烘焙在烘焙在4张图上保存下来（对默认的Lit Shader支持，想要使用得修改Shader）



**Runtime Imposters**：

兼容所有的Shader



### 3.功能差异：
**Amplify Imposters：**



![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739166376415-f9d83bbd-3d09-47dd-bb16-008a0836890f.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739166399488-fc3ea654-a4c0-42b7-8001-ebebe95fa96a.png)

                             原本			   			   Amplify Imposters

深度：有正确的深度信息

mesh绘制方法：（没有变更）SRP Batch

烘焙方式：预烘焙，图片保存本地

信息来源：通过BakingURP.shader将Albedo，Normal，Specular，Smoothness，Emission，Alpha，<u>Depth</u>等信息烘焙下来

根据距离分配纹素精度：通过预烘焙的贴图开启Mipmap完成



**Runtime Imposters：**

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739164417693-297f4bcc-7125-46a7-8a28-e050a8ab30d3.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739164400181-ecdfcded-7eda-4cd2-b924-62a62df5c74a.png)

                  原本			   Runtime Imposters

深度：深度信息错误（遮挡关系与后期效果错误）

mesh绘制方法： Graphics.DrawMesh

烘焙方式：实时烘焙，图片运行时生成RT，不保存本地

信息来源：获取相机的位置和投影矩阵，然后通过AddCommandBufferCommands模仿真实的拍摄过程  

根据距离分配纹素精度：根据距离远近将对象画在（尺寸相同的）不同的RT上。（可动态调整）

    纹素64  			       		   纹素32			      		  纹素16

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739155289777-f02bf155-7639-4775-8256-373972bced3d.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739155301480-43d17ecd-c7dd-43b2-bc46-4d7b6422cb8b.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739155314967-c568fc49-5005-41d6-a0fb-23e5c0954960.png)



### 4.性能比较
测试配置：Oppo 	骁龙670



![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739014884534-b5bb74fb-8e0a-4f26-ae65-f79b78bc680c.png)

| | **LOD Group** | **Amplify Imposters** | **Runtime Imposters** |
| --- | --- | --- | --- |
| 400颗树 | 21FPS | 24~25FPS | 29~30FPS |
| 600颗树 | 17FPS | 22~23FPS | 26~27FPS |


由于Amplify Imposters使用的Shader复杂度比较高，所以反而帧率没Runtime Imposters高

————————————————————————————————

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1739187068748-14db7cf8-96b8-4a64-8826-8804885dc9a7.png)

将Amplify Imposters使用的Shader换成FASceneObject后Amplify Imposters帧率更高

| | **Amplify Imposters** | **Runtime Imposters** |
| --- | --- | --- |
| 1000颗岩石 | 17~18FPS | 5FPS |
| 500颗岩石 | 20~21FPS | 9~10FPS |
| 100颗岩石 | 34~35FPS | 23~24FPS |


### 5.优缺点
| | **Amplify Imposters** | **Runtime Imposters** |
| --- | --- | --- |
| 优点 | 预烘焙；深度信息正确；要兼容shader的话，改造方便； | 对自定义Shader友好；不需要保存预先烘焙的数据； |
| 缺点 | 要将烘焙信息保存下来，增加包体大小 | 运行时烘生成RT；没有深度信息，对有穿插的物体效果很差，如果要有正确的深度，就得多生成一张新的深度RT |






