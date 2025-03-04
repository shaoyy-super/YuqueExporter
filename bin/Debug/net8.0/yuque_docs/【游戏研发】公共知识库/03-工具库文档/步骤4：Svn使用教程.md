# 一、修改文件update
  1. 找到我们修改好的文件，<font style="background-color:#FBDE28;">[这里如果我们的文件被修改了，在前面会出现一个感叹号]</font>，就是代表我们这个文件进行了修改

2. 接下来我们先点击 SVN Update（中文版：svn更新），进行一个更新，以免冲突；
3. 然后点击SVN Commit进行提交。

具体步骤如下：

      1，  完成资源修改后出现了红色感叹号，代表已修改；

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712477975502-96e30942-6b37-4255-ae94-df2d223adb44.png)

         2，选择我们修改的文件，点击右键，此时在选择栏中，先选择 SVN Update，进行一个更新；

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478002565-5e64bb6b-61ff-4bb7-ae35-986fabf30dcc.png)

       

         3，然后再选择 SVN Commit进行提交；

                第一个红框，填写我们要提交的信息；

                第二个红框，选择我们所修改的文件；

                最后点击ok。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478032728-d7ef20ce-e0a1-4431-9b2e-416b418cc87d.png)

        然后出现这个弹窗；

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478053219-635fa689-9899-4dff-9b11-a953200dce62.png)

        等待加载完成点击ok。

当我们操作到这里，修改的文件就被推送到svn仓库当中了。

# 二、添加文件add
（1）将需要增加的新文件放入到本地迁出的文件夹目录的相应位置中，鼠标选中新文件右键选择“Tortoise SVN”的“Add/添加”项，如下图所示：

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478378081-28ef3c3a-47d4-467e-ad86-03c90ac91ac8.png)

 （2）鼠标选中项目文件夹右键选择“SVN Commit/提交…”，将新文件上传配置库对应文件夹中（若只上传单个文件，只需点中单个文件上传也可）。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478390548-16b6d7e5-b956-4402-b3c5-a31d6d6082f2.png)

如下图所示，确认提交

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478530305-77398a8d-2056-4953-ba0b-97edc39c11d8.png)

 然后出现这个弹窗；

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712478053219-635fa689-9899-4dff-9b11-a953200dce62.png)

        等待加载完成点击ok。

# 三、删除文件delete
<font style="color:rgb(77, 77, 77);">选中要被删除的文件，右键选择“Tortoise SVN”的“Delete”项，如下：</font>

![](https://cdn.nlark.com/yuque/0/2024/jpeg/12926950/1712478653403-456269ae-e25b-4a37-a12f-1cc465db16cf.jpeg)

<font style="color:rgb(77, 77, 77);">（2）删除文件后，鼠标选中项目文件夹右键选择“SVN Commit…”项进行提交，提交方式同增加文件的提交方式，提交后则将新文件从配置库中删除。</font>

---

 

原文链接：[https://blog.csdn.net/weixin_40130409/article/details/134013066](https://blog.csdn.net/weixin_40130409/article/details/134013066)



