#### **进入Unity中Scoped Registries设置界面, 设置相关的Scoped Registries**
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712820728948-f5865135-5636-4189-b4f1-32316a83020a.png)

(在要使用的项目中打开Edit-Project Settings)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712820748106-a7d474d3-4a2b-46eb-b898-675927c56142.png)

(点进Package Manager, 黄圈部分为使用插件库需要的信息)

Name可以自定义

URL为Verdaccio的地址

Scopes为使用的该插件库中检索的包名, 可填多个, 通过右边+-按钮控制

<font style="color:red;">完成之后需要save一下</font>

#### 进入Package Manager管理相关插件
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712820876841-96c563a9-13c0-432d-9cfd-d595d419e5ed.png)

(进入Window-Package Manager)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712820891853-990992e4-c1e3-4ece-9318-e0e0b58c09c3.png)

(选择My Registries)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712820901131-3d257376-0012-4884-a153-aff502620864.png)

(此为刚刚加入的Scoped Registries发布的插件, 右侧install就可以加进项目)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712820935240-a1e4e273-301d-4f04-8166-d08891b62edf.png)

(插件已加入项目)

#### 下载插件的相关问题
##### 3.1 下载失败情形
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822002580-362fd2a9-bfcb-485a-af54-36f6d3e5c3aa.png)

(此插件存在依赖关系且依赖的插件在该第三方库中不存在, 联系插件开发者解决此问题)

##### 3.2 没有找到相关插件
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822267520-b0309532-655f-47d9-807d-a788c104a7d0.png)

(使用红圈处的刷新按钮<font style="color:#DF2A3F;">手动刷新一次</font>, Package Manager不会自动刷新, 包括关掉窗口重新打开, 如果需要的是近期上传的务必手动刷新一次)





