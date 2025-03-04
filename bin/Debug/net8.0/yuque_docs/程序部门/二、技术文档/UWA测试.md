# 一.简介
**<font style="color:rgb(98, 103, 115);">UWA是一款从测试机型分布、引擎各模块开销、内存占用等方面剖析定位Unity手游性能瓶颈和趋势。协助寻找合适的性能定位和提升方案。</font>**

**<font style="color:rgb(98, 103, 115);">UWA SDK 与UWA资源检测工具已在Box项目接入.</font>**

**<font style="color:rgb(98, 103, 115);">UWA SDK接入后可</font>****<font style="color:rgb(102, 102, 102);">PlayerSettings.SetScriptingDefineSymbolsForGroup添加或删除DISABLE_UWA_SDK宏。控制</font>****<font style="color:rgb(98, 103, 115);">是否打包</font>**_**<font style="color:rgb(102, 102, 102);">屏蔽</font>**_**<font style="color:rgb(98, 103, 115);">UWA相关内容集成。  
</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721374518772-46f8b097-0493-449a-b2dc-ecfb95521a5b.png)



**<font style="color:rgb(102, 102, 102);">UWA GOT 详情模式只支持 Development Build，请勾选 Development Build；</font>**

**<font style="color:rgb(102, 102, 102);">如果在 </font>****<font style="color:rgb(51, 51, 51);">IL2CPP</font>****<font style="color:rgb(102, 102, 102);"> 下进行 </font>****<font style="color:rgb(51, 51, 51);">Android</font>****<font style="color:rgb(102, 102, 102);"> 的 </font>****<font style="color:rgb(51, 51, 51);">Mono</font>****<font style="color:rgb(102, 102, 102);"> 模式测试，需要勾选 </font>****<font style="color:rgb(51, 51, 51);">Script Debugging</font>****<font style="color:rgb(102, 102, 102);">。当通过 </font>****<font style="color:rgb(51, 51, 51);">BuildPlayer</font>****<font style="color:rgb(102, 102, 102);"> 接口发布时，请确保添加 </font>****<font style="color:rgb(51, 51, 51);">BuildOptions.AllowDebugging</font>****<font style="color:rgb(102, 102, 102);"> 选项。</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721374942732-1da91f38-65e7-4d1a-90f3-3e14742671f2.png)



通过Jenkins打包配置安卓包，默认关闭UWA测试![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721375474576-ebb8d461-74b9-456f-a88b-fc03f84c167f.png)



**本地资源检测工具使用入口**

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721375976688-f6a98d3d-6c6d-4a07-b7cf-3545f8dd9c1d.png)

本地检测设置完成，测试完成后，会在工程目录生成UwaScan目录![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721376193682-1c045a41-f7e7-4ed2-8731-b698c6948383.png)

在工程tools/ UwaDataUploader目录可在config文件配置挡墙用户名和密码，然后命令行运行UwaDataUploader.exe  生成的UwaScan目录  从而<font style="color:rgb(102, 102, 102);">上传至UWA官网即可查看本地资源检测结果。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721376312424-509f51cb-d943-48ca-8d5c-74ca8e101eb3.png)

命令上传成功后，测试结果如图

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721376593811-9c1c8276-75ae-4f65-8541-cac3e0e39b3a.png)



**AB包检测工具使用入口在对应工程UwaABCheck目录 **box\tools\UwaABCheck

<font style="color:rgb(102, 102, 102);">在 ABAnalyzer 工具所在目录进入命令行，调用 box\tools\UwaABCheck\ABAnalyzer_Win_1.0.3\ABAnalyzer_Win_1.0.3\ABAnalyzer.exe %AssetBundle_PATH% 命令即可，分析过程请耐心等待，</font>**<font style="color:rgb(51, 51, 51);">不要关闭命令行窗口</font>**<font style="color:rgb(102, 102, 102);">。</font>

<font style="color:rgb(102, 102, 102);">AB包检测工具分析完成后 点击项目目录 box\tools\UwaABCheck\UWATools_Windows\UWATools_Windows下UWATools.exe 上传文件，上传文件后，可在UWA界面查看。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721377400261-3e3995bd-2c18-4c76-a0ce-33c7c378e261.png)



Box项目初测不影响功能已优化的地方 

几个长背景音乐用的默认加载方式，改为Streaming模式加载。  
动画资源Anim Compression采用Optimal压缩模式。

TextMeshPro字体资源 采用Multi_Tex模式等。

Mono堆提示几个大的可能泄露的地方，一个是 GameRunner.Update,EventManger类

经功能测试lua中有功能Update注册没有移除监听，每次打开界面多个EventManger的监听没移除，发现UI已修复。



修复前峰值内存1.48G

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721378343186-33aaabb0-2578-4ae3-a435-97ebdfd71ca9.png)

修复后峰值内存1.25G

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721378425708-713d029c-e138-4fec-bf70-863538ee8e80.png)



后续UWA建议重点关注部分Shader性能，占用内存过大和冗余问题。

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721378828564-4602c036-9986-45db-be92-11ab2f161961.png)



字体模块优化，UI系统调用默认字体的地方删掉后，可删除Unity默认字体，是否有冗余加载问题。

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721378574681-bf4be7b1-709b-44f8-938a-fcaa19134c99.png)



AB打包策略配置优化减少冗余资源

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721379018499-b4c5e4f8-5cbd-4b08-b5e7-5fe71ec4ddd0.png)



堆内存增长主要函数关注

![](https://cdn.nlark.com/yuque/0/2024/png/43734381/1721379278591-3a91d966-dd74-40db-8b07-fd449d3da18b.png)











