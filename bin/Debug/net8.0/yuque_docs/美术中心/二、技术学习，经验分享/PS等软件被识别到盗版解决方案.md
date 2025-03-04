**方法一：修改hosts文件**

1. 打开**C:\Windows\System32\drivers\etc**路径，使用记事本打开其中的**hosts**文件

![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1733974561832-91f707d9-b961-47a4-bdf8-0ee0d42761ce.png)

2. 在文本的最下方黏贴内容 

**127.0.0.1 ic.adobe.io  
****#Mac  
****127.0.0.1 1b9khekel6.adobe.io  
****#Win  
****127.0.0.1 7g2gzgk9g1.adobe.io  **![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1733974771657-58bad849-faeb-42bb-8943-60055d21550c.png)

3. 保存后即可恢复PS的使用

<font style="color:#DF2A3F;">无法保存时，先存到桌面，删除后缀.txt后拖进文件夹中替换</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1733975370788-b0858bd6-0917-4401-a615-6bf688b90627.png)

****

**方法二：禁网**

此方案为通过防火墙禁网的形式规避掉被联网识别的风险，不过会导致Design Mirror等软件连接不稳定

<font style="color:#DF2A3F;">此方式有出现仍被检测到的情况</font>

1. 重装出问题的软件
2. 打开防火墙

![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482063709-86a56f04-3d07-4bd7-9737-0b484d32838a.png)



3. 点击高级设置![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482145408-62a0279c-6dd9-4170-8063-7da069529352.png)



4. 点击出站规则，然后点击新建规则![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482187020-4881b2a1-211b-4bbe-8fea-e4d46d1fe3cd.png)



5. 选择程序，下一步![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482383753-d9a5a527-4942-451d-9234-70e0eb8a0cb1.png)
6. 通过浏览选择到对应软件的.exe文件,然后下一步![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482395419-32674c7b-05c3-42ad-82e7-c4aafa69de19.png)![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482506264-a833e11e-87e1-4962-a7b1-ef1e3c0ef69b.png)
7. 选择阻止连接，下一步![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482531733-13918ced-64a5-4602-aa42-279b28ecb8f3.png)
8. 如图，下一步![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482588259-beade262-2e14-41a5-916d-452167fff756.png)
9. 填上命名，完成![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1729482631445-5927ee56-f20c-4326-a63f-0f484c2b79b6.png)











