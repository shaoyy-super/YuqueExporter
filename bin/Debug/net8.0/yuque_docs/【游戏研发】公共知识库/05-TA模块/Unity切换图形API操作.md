![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736833231195-77e4424e-5cb4-40d8-81ca-e65f48f18e55.png)点击左上角的Flie里的Build Settings![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736833270441-485017a2-0582-44e4-85fb-3aa077816384.png)

在跳出的Build Settings窗口选择PlayerSettings

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736833530272-f97a5102-1282-435e-85bb-49b64709d681.png)

在PlayerSettings窗口先选择Player，然后点击那个电脑图标，把Auto Graphics APl for Windows的勾勾去掉，就会跳出Graphics APls for Windows一栏，点击加号

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736833702072-6613b0eb-bb1c-454a-a147-ecdab72d730f.png)

把DirectD11选上，然后把5里的拖动DirectD11移到最前面

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736833811309-5b41d8bd-fb72-4a50-a455-a8c8fa6bac39.png)

最后跳出这个窗口选择Restart Editor，他就会自动重启unity，然后，就会切换平台API了



（检查完记得切换来，有时候切换API会导致效果不对）

fork也不要提交这个修改文件

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736834003734-54d4be2c-ec79-4363-9147-b789c4664a39.png)







通过命令行强制切换为DX11

 -force-d3d11  



![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736923320008-de663e01-a59f-46fa-b283-ef50e60ecf70.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736923333983-9cb7d499-7c7e-4bd1-991c-ea731d403bad.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736923352339-bee82e39-80e7-40aa-b6df-fa9b04a31b0d.png)

保存后重新打开该项目就一直是DX了



![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1736923545930-c53c8b1c-cb8d-45f9-81f0-98b92f8adbb4.png)

不过这里显示的还是之前的，其实已经是DX了

