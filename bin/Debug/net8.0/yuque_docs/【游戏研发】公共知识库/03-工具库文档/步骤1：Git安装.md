## Git安装
软件路径：**\\192.168.0.231\AI及Web3游戏研发中心\公共资源\04_软件\办公软件**

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712471263040-f5d04a3c-1981-4e78-9708-85f3cecfdb7c.png)

软件复制到本地，双击点击安装

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469777918-2236b39f-af8f-42a8-b10c-6b7e9b808cf5.png)

选择安装路径，点击[next]

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469778345-abc7a7c2-c541-4ec3-804e-407e194a7593.png)

在窗口中选择组件，点击[next]

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469778788-8d05d878-bafd-42c8-a9ee-efdc6947187b.png)

点击[next]

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469779307-040437e1-33cf-4657-a8e9-6affafa4d2ef.png)

选择默认文本编辑器，点击[next]

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469779686-e71cb3c7-da4d-4b35-bf44-b348481ef780.png)

[创建文件夹]界面，选项默认，点击[next]

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469780108-595b20f8-5993-40bf-bf10-89358841398b.png)

**[修改系统的环境变量]界面，默认勾选的第二个选项，点击[next]**

选项一，不会修改系统环境变量，但是Windows系统cmd命令行中无法使用git命令

选项二，会将git相关程序加入系统环境变量中，使得Windows系统cmd命令行中可以使用git命令

选项三，会将git相关程序以及一些其他的Unix工具加入系统环境变量，使得Windows系统cmd命令行中可以使用git以及Unix工具。要注意的是，这将覆盖Windows工具，如 “ find 和 sort ”。只有在了解其含义后才使用此选项。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469780652-9ab6160c-210f-49c3-a140-4f0c95c63c63.png)

选择SSH可执行文件，选择开源代码，点击[next]

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469781002-66f6977a-8eaa-4ba6-ac23-23d040e5118c.png)

**选择HTTPS传输后端，使用默认OpenSSL 库，点击[next]**

选项一，使用 OpenSSL 库

选项二，使用本地 Windows 安全通道库

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469781399-f00d01a4-e6e5-4deb-8d32-ca11de30c767.png)

下一步请注意,**一定要选Checkout as-is,commit as-is这一样**

指令为 git config --global core.autocrlf false

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469781991-a06c41b8-298c-4ead-9155-758cea13acb7.png)![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469782422-d8e8c2e9-fb05-4853-a1d7-11070bc23a60.png)![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469782822-c5b02719-26be-472b-89cb-1f15752d2428.png)![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469783847-6dc9cbe6-ba38-4e59-bbe1-35a725a4e795.png)![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469783216-9ef00321-0bb8-4520-9a9a-a4c1ecdcb19f.png)

**最后点击install**

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469784282-333d58eb-48e3-4944-b7f8-aa7a7bce4824.png)

## 安装完成后：
操作win+R，输入cmd，打开 cmd 软件

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469784714-2165d818-1a6f-49b9-bbcd-b11e7b861f58.png)

拷贝 _**<u>git config --global core.autocrlf false</u>**_

粘贴代码后回车

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712469785177-4f7b8a19-4175-4e02-a381-6469080e2cd3.png)

