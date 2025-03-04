## **使用**
### **General Usage (Substance Painter****插件通用安装方法****)**
**要求**：Substance Painter Version 10.0.0及以上



1. 将SubstancePainterPythonPlugins -> plugins目录内的所有文件复制到Substance Painter用户文档目录的python -> plugins目录下。

> Substance Painter用户插件目录完整路径一般为：`C:\Users\{User}\Documents\Adobe\Adobe Substance 3D Painter\python\plugins`
>

> _**{User}**_是电脑的用户名，不同电脑，用户名各不相同。
>



2. 打开Substance Painter，点击界面左上方工具栏中的 **Python** 栏目，点击想要打开的插件名字，确保勾选上即可。

> 如果找不到对应插件名，可以点击Python栏目最下面的"**重载插件目录(Reload Plugins Folder)**"。
>

> 插件名字和plugins目录下的python文件同名，且Substance Painter加载插件时，只会加载plugins目录下一级内的python文件，不会递归加载子级文件夹中的python文件。
>



### **export_materials.py (SP****材质及贴图导出插件****)**
1. 使用插件提供的“**自动设置项目初始配置**”设置好通道及图层。

2. 给图层添加内容。

3. 点击界面左上方工具栏中的**File(****文件****)** 栏目，再点击**导出贴图及材质信息（****Export textures and save material settings to json****）** 即可。

4. 观察Substance Painter界面下方的 **Log(****日志****)** 窗口，找到导出文件所在目录。

> 一般导出的文件和打开的sp项目文件在同一目录下。假如sp项目文件名为meetmat.spp，那么导出的文件就在同级目录的meetmat_export文件夹内。
>

5. 导出文件夹内的目录结构说明如下:

+ DyeingTextures/ 存放可染色配置的贴图
+ GeneralTextures/ 存放不可染色配置的贴图
+ URPTextures/ 存放给一般着色器如URP/Lit使用的贴图（即配合一般着色器使用，而不是项目指定的着色器） 
+ xxx_export_material.json 存储mesh、材质、uv等信息，用于Unity获取相关材质信息。

