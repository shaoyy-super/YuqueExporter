## 1. UWA SDK 工具 1.1. SDK 下载 1. 打开 UWA 网站，登录 UWA 账号；
## 2. 打开下载页面：https://www.uwa4d.com/index.html#download，可以看到本地测试产品 –UWA SDK 的下载链接，可免费下载。
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739207765-c5b08681-946e-4ace-b7c3-a04b28b155ab.png)

## 3. 下载包为 ZIP 文件，其中包含了 5 个安装文件和 1 个文档。
需要集成对应发布平台的UWA_SDK_XXX.unitypackage 文件到项目中；在真机测试设备上安装 UWATools。注：已购买并安装使用“本地测试产品 – GOT”的会员，无需重复安装“本地测试产品 – UWA SDK”。 

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739207791-1527bd1e-223b-46f2-969a-afbecd2ccde8.png)

## 1.2.集成打包
## 1. 将“UWA_SDK”文件夹中对应平台的 UWA_SDK_XXX.unitypackage 文件拖入项目中，并点击“Import”按钮进行导入。
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739207759-4bc94251-2804-4862-ae1b-d180741e40fb.png)

## 2. 在 Unity Editor 中将 UWA/Prefabs 文件夹下的 Prefab 文件拖入到项目的首场景中，且确保不会被强制 Destroy，如下图所示。
## 3. 如在Game视图的右上角出现如下图所示的UI界面，且无报错信息，说明工具集成完成。
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739207788-4eb7494b-f806-40e9-875c-e254cc382919.png)

## 4. 点击菜单栏“Tools -> UWA SDK”，打开 UWA 工具栏。
注意：若不能拖动，鼠标右键点击show in Explorer,本地拖入文件

## 5. 在“配置”界面上选择需要使用的 UWA GOT 和/或 UWA GPM 工具（建议两者同时勾选），按照指引指导配置直到两个按钮变成绿色且显示“完成”则表示配置成功。
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208137-0e84fd6e-c9cd-4ae9-b41d-9f65dd2c96a3.png)

注：UWA GOT 只支持 Development Build，请确保配置勾选 Development Build。UWA GPM 支持Development Build 或者 Release。

## 6.点此发布前需手动打AB包并替换
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208262-94cf48d5-0760-442d-a41a-835d138f6f6e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208266-fa360eb1-d7a6-4dc3-9551-663fc9ae1b3e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208301-9c478ee3-28b0-47de-8efc-4e28df8a1a0e.png)

## 7.需关闭下载补丁包
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208358-35ff6443-1b4e-4f03-8abe-d69287843e9f.png)

## 8.进游戏场景后右上角有UWA的SDK即为成功
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208669-9268daee-da3f-423f-a82a-d9ca22e78a5a.png)

## 9.以FA为例，需以一款机型设定一套完整稳定的测试流程
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208700-b3c0b359-962b-4e54-b95d-aff3c8fed5a3.png)

## 10.自动上传的测试数据填入对应的表格里
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712739208758-550e8b78-9de8-431a-9d50-d6ce96ef1b1c.png)



