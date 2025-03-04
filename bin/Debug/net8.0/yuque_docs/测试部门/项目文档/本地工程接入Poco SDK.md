## 一.下载<font style="color:rgb(0, 0, 0);">集成 POCO SDK</font>
<font style="color:rgb(0, 0, 0);"></font><font style="color:rgb(0, 0, 0);">Poco SDK 下载：</font><font style="color:rgb(0, 0, 255);">https://github.com/AirtestProject/Poco-SDK</font>

<font style="color:rgb(0, 0, 0);">Poco SDK 集成</font><u><font style="color:rgb(0, 0, 0);">说明</font></u><font style="color:rgb(0, 0, 0);">：</font><u><font style="color:rgb(0, 0, 255);">https://poco.readthedocs.io/en/latest/source/doc/integration.html</font></u><font style="color:rgb(0, 0, 0);">   
</font><font style="color:rgb(0, 0, 0);"></font><font style="color:rgb(0, 0, 0);">UWA 优化版本 Poco SDK： </font><font style="color:rgb(0, 0, 255);">https://github.com/UWAMakeItSimple/PocoSDK/tree/uwa_master/Unity3D</font>

[Poco-SDK-uwa_master.zip](https://snh48group.yuque.com/attachments/yuque/0/2024/zip/43256946/1712545631971-47dbb063-f2be-48ff-acab-57cb93388338.zip)

## 二.清除工程缓存
1.关闭服务器和打开的文件，关闭Unity

2.打开git,输入指令git clean -dn; git clean -df;git reset --hard;git pull

3.确保拉到最新并无任何冲突和差异

## 三**.拖入工程**
1.<font style="color:rgb(0, 0, 0);">下载 Poco SDK，将其中的 PocoSDK-U3D 拖入 Unity 工程的Assests当中。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712545572602-81858cc5-682c-4faa-a09a-f3c173f017bd.png)

2.<font style="color:rgb(0, 0, 0);">PocoSDK/Dumper 目录下有四个文件夹：FGUINode，NGUINode，UGUINode和TMPUGUINode根据游戏项目所使用的 GUI 方案，选择保留 Dumper 目录下的 UGUI，删除其他所有文件夹。如下图所示，FA项目使用的 GUI 方案为 UGUI，则只保留文件夹 UGUINode 即可。 </font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712545572715-a1cc1811-1208-4d14-8ed5-206ae51900f9.png)

3.<font style="color:rgb(0, 0, 0);">将其中的组件 PocoManager.cs 拖入游戏首场景中的物体上，box的</font>首场景名称为：  Launcher  

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1716346884552-dddeab53-fb3d-44cd-a145-135b9632831b.png)

4.点击StartUp页签，查看游戏首场景组件是否勾选<font style="color:rgb(0, 0, 0);"> PocoManager.cs</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712545572600-0b3ed79c-7709-485b-b3bd-3567b61c7733.png)

