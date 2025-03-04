#### Unity插件制作
 ![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824064937-8ae53b88-3608-44fb-877c-2bd8699224de.png)

(创建一个这样的结构, 这里只演示发布, 不加入具体逻辑功能, 故直接用个空的)

文件夹名字视具体情况确定, 这里为了后面演示显眼在前面加了一个_

Editor是否需要以及其中包含的文件视插件具体用途, 不必强求一样

CHANGELOG是版本变更记录, 推荐有相关的说明文档去记录版本变化

<font style="color:#DF2A3F;">package是记录插件信息的, 必须要有</font>

README类似插件功能介绍之类的, 在Verdaccio上能直接浏览到该文件内容

 ![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824146155-fe99993a-c8ff-4d3e-9ca4-af22752e2342.png)

(对Editor文件夹创建Assembly Definition文件)

 	![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824185142-37839e02-eabf-44a9-a067-cf270f039201.png)

红线圈出部分为本插件需要的宏定义约束, 例如UNITY_EDITOR之类, 按需修改

蓝线圈出部分为本插件需要依赖的插件, 例如图上本插件依赖于CodeCoverage插件, 按需修改

黄线圈出部分为本插件使用的平台, 按需修改即可

紫线圈出部分为本插件需要根据资源版本号变更的宏定义设计, 具体按需使用(一般应该用不上)

 	![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824198019-91e5b210-b178-47f6-a369-d00eeba1928e.png)

(此图为package.json的配置文本)

name为本插件使用的包名

displayName为本插件在Package Manager内显示的名字

version为本插件当前版本号, 每次更新需要增加该数值

Keywords为本插件实现功能的相关关键字

category为该插件的类别

description为该插件的简要描述

dependences为该插件需要的依赖以及对应的版本号(填写包名)

#### **Unity插件发布**
##### 2.1 Verdaccio用户的注册与登录(<font style="color:#DF2A3F;">该步骤只需要使用一次, 如果之前已经有过可跳过2.1步骤</font>)
 ![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824420080-e8c9cf8c-a058-481d-8c96-3b68b39007b2.png)

(使用 npm adduser --registry http://ip:端口 命令添加用户, 具体使用的ip和端口看Verdaccio搭建的位置)

 		![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824429078-7bb05dd7-8758-441e-a63b-b363b0570dad.png)

(需要输入用户名, 密码, 邮箱, 邮箱未发现具体用途, 可以先考虑填公司邮箱)

 		![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824438600-914bc175-1496-4a5f-a0d6-25c1ad63b548.png)

(使用 npm login --registry http://ip:端口 命令登录Verdaccio)

##### 2.2	发布插件
 ![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824496392-89bf0014-612a-408e-b86d-3c5188004174.png)

(先<font style="color:#DF2A3F;">用cd命令移到要发布的插件目录</font>, 例如本次演示的是 cd "C:\Users\jialiang_li\Package Manager Test Project\Assets\_one_package")

 ![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824516115-586b1100-58d8-4713-b7eb-4c5eb37f83c5.png)

(使用 npm publish --registry http://ip:端口 命令发布插件, IP和端口看Verdaccio的位置, 例如本次演示的为npm publish --registry http://192.168.5.148:4873)

 		![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824525139-928e5f10-0136-40d7-bda6-44760a7b1d72.png)

(此时在Verdaccio上已经出现了本次演示发布的插件)

#### Unity插件的更新
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824635465-6ecf37b9-3142-4c4d-bb82-847b9e051149.png)

(修改插件的CHANGELOG, 非必须但尽量)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824647428-75e9a97e-03bf-45f1-8d83-8fb265930657.png)

(<font style="color:red;">增加版本号</font><font style="color:red;">, </font><font style="color:red;">必须要的操作</font>, 此外尽量不要修改name, 修改name可能会导致Verdaccio上出现多个同名的插件(貌似是bug, 重新启动Vredaccio时也会有报错, 不过不影响正常运作))

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824658301-85cd39ad-b1eb-456e-b8b1-9b5a99fba8dd.png)

(再次使用发布命令发布该插件, <font style="color:red;">同理需要先</font><font style="color:red;">cd</font><font style="color:red;">到对应的插件目录</font>, npm publish --registry http://ip:端口 )

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824666122-44e80b54-ce88-4631-b7d4-3095176d1f39.png)

(插件更新完毕)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824675238-2bfdd26f-e729-45a4-a482-9b8b0cda18c5.png)

(使用了该插件的项目里可以刷新一下然后更新到相应版本)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712824681984-7e57cf21-2023-4e9f-b6cb-5b6f78254ac7.png)

(项目内也更新完成了)





