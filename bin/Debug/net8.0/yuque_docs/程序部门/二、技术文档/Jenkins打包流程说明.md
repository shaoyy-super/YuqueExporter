### 1.打包配置说明


[BuildSetting.xlsm](https://snh48group.yuque.com/attachments/yuque/0/2024/xlsm/43297665/1721617092774-81e21aea-41b5-4d6d-8f1e-ebddaed0bece.xlsm)

配置文件的路径一般为：项目目录/config/client/BuildSetting.Xml

使用宏将设定的内容转为Json导入unity

常用的参数说明:

| 参数 | 说明 |
| --- | --- |
| id | 打包的配置ID |
| productName | 项目名称 例如:![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721617464843-e6a3583c-3e1f-4dde-b06f-22609f7a9d46.png) |
| bundleIdentifier | Unity的AppId(用于记录软件的唯一性) |
| icon | 项目的Icon (可见上图) |
| pkgCustomCfg/urls | 目录服地址(这个得找服务器同学要) |


基础的打包流程只需要修改上面的参数就可以了，其他的参数可以看表中的配置注释



### 2.groovy文件说明
首先得在git中将jenkinsbuild项目拉取到本地![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721618358919-01b792f7-7d61-4031-88d7-18c45db42020.png)

已元富翁为例

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721619475875-99b81c9f-09df-4941-b23e-7b556e3c54f7.png)

新建一个项目自己的分支,在自己的分支上修改自己项目中的参数

(Tips: 有同学拉取main分支下的东西 发现内容变动较大 建议从BoxAndroid分支拉一个分支出来 之前Box有改过一些打包参数)



首先是ToolParams.json文件

(Tips:目录中有两个ToolParam.json文件，我们需要修改的是ClientDist文件夹下的)

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721619610621-08609eb8-f852-4d01-b066-a4470c557eff.png)

需要我们关注的是FTP参数中的"ftp_dir"

这个是我们打包之后的文件地址

\\192.168.0.231\AI及Web3游戏研发中心\公共资源\06_BuildTarget\Richman\dev



各个项目如果需要新建目录 只需要将其中的Richman改成对应的文件目录即可。



groovy文件

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721626607934-5ab77a58-21f3-4ffb-b204-775c0a1ad219.png)

如果是直接从别的分支拉过来的话,直接修改对应的文件名就好(最好以项目名称命名)。

其中的大部分代码都有注释介绍 就不再过多赘述，主要说一下我们需要修改的部分

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721627020228-318bd05d-54e3-4aeb-840b-a0a6f5eeb4c1.png)

ServerJobName: 这个是服务器的任务名称对应了Jenkins中项目的服务器任务名![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721627082349-cf47ed4e-b0a6-48cb-a751-394d9b5d2d2f.png)

GitUrl：项目的git地址



![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721627136213-027f062e-dd15-4da1-96b1-46ef5a40330e.png)

FtpUrl：需和上面修改的FtpUrl的目录名称一致

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721627214395-fda01d31-1e9e-4de9-862a-a3fd3c996fb9.png)

对应的服务器的group配置(需找服务器同学确认)

其余的修改就按照项目的需求进行设置



### 3.新建Jenkins任务
Jenkins地址：[http://192.168.4.222:8080](http://192.168.4.222:8080)

用户名：dev

密码：123456

1.新建Item

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721627736971-a58f2322-f705-49a5-a487-5073e68bd261.png)

在Jenkins的主界面点击新建Item

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721627812102-7fb8e0d5-6be0-407f-a927-f651fe77fede.png)

在新建任务界面输入自己的任务名->选择Pipeline->确定

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721628009604-b9c2005a-2c20-4acd-aa7b-c21205c3c808.png)

在设置界面选择 **<font style="color:rgb(20, 20, 31);">This project is parameterized(参数化项目)</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721628131856-23f3415b-764b-46e2-b258-d2eaebd0aa08.png)

在点击添加参数后会出现以下几个选项



| Boolean Parameter | 布尔类型的参数(true or false)，有默认值的选项 | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721628688049-07fafcc8-0fd4-49c2-b05d-0288f7be84ce.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721630982158-95213eb5-6049-4f8f-94ab-62c3361bc926.png) |
| --- | --- | --- | --- |
| Choice Paramter | 选项类型的参数，每行都是一个不同的选项类型 | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721628709539-4ea18150-6b34-4b45-bb42-372e8f84c6a9.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721630964836-148f8eb6-efbc-4c5f-b9e8-904e4c6c452e.png) |
| **<font style="color:rgb(20, 20, 31);">Credentials Parameter</font>** | 凭据参数 可以选择凭据类型(file、key等等)，按需设置即可 | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721628732575-d560dfa6-1fe5-45fb-a7ac-a4750cc832ba.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631001981-f219e3f1-f9b8-44f6-9339-8e032d69c7e6.png) |
| **<font style="color:rgb(20, 20, 31);">File Parameter</font>** | 文件参数 参数为对应的文件，上传的文件会拷贝一份进入我们定义的文件路径下 | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721629608698-95b1d838-494a-4026-bb44-7ae4aea0ddd5.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631117293-33df5c1f-ff47-4ea6-ac35-dacbdff330e9.png) |
| **<font style="color:rgb(20, 20, 31);">Password Parameter</font>** | 密码参数 可以设定默认的密码参数 | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721630577596-aefa3092-47c5-4461-a0bf-b860a7af6151.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631134136-69bd6c58-86bd-45b6-ab1c-db366bf12ef2.png) |
| Multi-line String Parameter | 多行文本参数 和Chioce不同换行也会作为一段文本一起显示 | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721630886264-55ad8ab3-5377-4fe8-9f18-2403200528af.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631147980-09ef8289-51f8-4407-a031-15afd4d2ef69.png) |
| **<font style="color:rgb(20, 20, 31);">String Parameter</font>** | 字符串文本参数 里面放置的就是一段string | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721630898535-bc7ced53-ae4e-439c-be09-cdb00368da30.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631160165-6ce222f6-05a9-46b5-b66a-f9612d676d2a.png) |


项目中的具体的打包参数的说明可以看下面这个文档

[客户端打包参数说明](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/am20lgutlzbg80if)

再贴一些元富翁的打包参数作为参照

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631427916-467f4cb6-c1cc-4dd9-9867-6a35e742f9d9.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631455535-950ae526-2f16-448f-8ac2-bb726c5d8c25.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631476042-341a380a-9a1e-43ce-87b7-e466f90cb076.png)

流水线设置：

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721631670124-cce95312-714a-427e-a374-1c135b18f2c1.png)

在流水线选项中定义选择Pipeline script from SCM->SCM选择Git->Repository 填写Jenkinsbuild的git地址->

Branches to build 填写*/ + 自己项目拉出的jenkinsbuild的分支名称->脚本路径填写上面修改的groovy文件的文件名称->点击保存。

然后就会在看板视图看到刚刚创建的任务了

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721632235165-8a178fb5-feeb-4655-8551-900b8d8348b6.png)

<font style="color:rgb(20, 20, 31);">点击任务进入build视图</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721632635428-7545852e-5c57-4305-925a-3662608779ca.png)



接下来，需要参照RichMan-服务器补丁添加自己的服务器补丁Item，需要更改以下内容

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722252740832-57f40ee2-3e30-47ea-afae-15397fac90d5.png)

此为git地址

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722252769456-1fbbd19d-005e-43a4-8d8c-d33de1415f3a.png)

需重点关注上面这张图，对应以下图中的FtpUrl中的路径，并将dev改为server

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722252868278-8de4a89a-efe6-4af8-a8fb-70e988cfcb24.png)

最后，我们需要改动groovy文件中最上方的这一部分

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722252929352-2ee0774a-4030-4fac-980f-81907c2e883e.png)

SlaveNode需要自己配置，以下为配置的方法

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722253009096-9580fcdd-f023-44b6-b086-503648b38ad0.png)

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722253049184-75543936-50c8-459b-a150-b7a49e2484c2.png)

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1722253117693-65f13f7e-6d66-4cf7-b976-c86667c119b1.png)

再完善我们需要打包的配置以及参数点击build就可以打包了

打包成功的话就可以在对应的路径找到我们的apk文件了

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721632799245-fcf843c3-0e4b-4ddd-975f-f48435cc2661.png)

如果打包失败的话

可以win+R 执行mstsc命令 远程链接打包机

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721632872376-e93f677f-8972-492d-9bf0-073d0f18cfcf.png)

打包机用户： dabaoji1  

密码：234werSDF

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1721633119555-2493a4ff-e14e-478e-83ad-c829329589ae.png)

在打包机的D:/Jenkins/workspace/RichMan-Android/Project/client/BuildTarget/Upload(RichMan-Android为对应的任务名称）这个路径下可以找到html文件 双击即可查看报错信息。





