**简介：**

1. Impostors是一个优化插件，可以把离相机较远处的模型替换成一个片，且可以根据相机的实时位置，改变模型的渲染图，贴到片上。下图中绿色的物件就是转换成插片的模型：

![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1736750978560-3c4a3377-ebb4-4e03-a0b5-262c3131246e.png)![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1736750987275-d9baddc3-106a-4db9-863b-17ef4127556c.png)

2. 如果相机快速移动，且Impostors插片离camera较近的情况，变化还是能看到的，所以离camera较近处，相机位置快速变化的情况不适合使用此插件。适合相机位置不动，或者变化较慢的情况。

**Impostors使用步骤：**

1. ** **Tools/Impostors/Creat Scene Manager，场景中增加IMPOSTORS物体。
2. ** **选中需要优化的camera，Tools/Impostors/Creat Camera Manager，为此镜头添加 Camera Impostors，在步骤1中创建的IMPOSTORS物体下。（如果场景中只有一个camera，则执行步骤1时会自动创建Scene Manager和Camera Manager)
3. URP管线的Scene Render配置文件中，添加Imposter相关的参数:  
![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1737350943520-e5dc6d58-0f2d-4ab7-a2f4-3e5116e0e2d0.png)
4. ** **为每个物件上添加Impostor LOD Group：选中物体，Tools/Impostors/Set Impostor。注意：需要优化的场景物件必须原来都有unity的默认LOD Group设置。
5. 调节Edit/Project Settings/Quality/LOD Bias参数，来调整Impostors生效的和camera距离的远近，数值越小，Impostors生效距离越小。通过勾选Camera ** Impostors细节面板中的Debug Mode Enabled，运行后可以查看哪些物体已经被转换为Impostors插片。注意：IMPOSTORS物体的细节面板中的shader需要保持默认的ImpostorShader才能生效，替换为其他shader无效。

![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1736751013473-1efaf3ea-630e-4f04-8426-6852a1be2ef8.png)  
**Impostors问题汇总：**  
**1. **Impostors默认材质受场景unity默认雾效影响，需要关掉fog。  
**2.** Impostors替代面片无阴影，需要跟TA研究如何增加阴影。  
**3. **Impostors物件的材质光影会受到场景中 ![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1736751060930-f0848eff-9d13-4fbd-a535-dd08d808b577.png)这两个灯光的影响而变化，需要解决这个冲突。

 关闭这两个光源的效果： 

![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1736751071213-147a92ee-4285-44b5-9806-dbca2dee3187.png)

打开这两个光源后的效果：

![](https://cdn.nlark.com/yuque/0/2025/png/21386602/1736751078674-bbf55f49-c798-492f-a6dd-465d5a931542.png)

