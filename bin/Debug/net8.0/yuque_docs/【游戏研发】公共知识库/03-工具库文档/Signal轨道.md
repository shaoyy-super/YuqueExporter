_Si_gnal Track信号轨道，可以发送信号，用来触发信号的函数调用。

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805758156-cf4b151d-e3a0-419b-839f-76baf41700dd.png)

首先创建一个GameObjcet，再创建一个c#脚本用来响应信号

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805789147-ec540976-4435-4f8b-a464-6f64be9beb4e.png)

再在Timeline中创建Signal Track信号轨道![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805812775-dcca4da2-4592-4409-8d9a-c5b3652b3655.png)

再创建一个信号发射器资源

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805841921-44bf4ca7-e564-4467-86ea-03c9043585a7.png)

这时就可以将信号发射器拖到轨道上

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805869438-6260ed4c-cf45-47c5-b9ed-1a9a614ec1bd.png)

这时就让这个组件响应这个轨道所发出的信号

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805893179-62a925a6-fa05-4170-86b5-ae36784aec42.png)

这时就可以看见每当Timeline运行到信号发射器时都会执行这个方法来输出一段文字。

![](https://cdn.nlark.com/yuque/0/2024/png/43297665/1712805915611-52470cb1-bf4d-4183-a6c2-a98a90495562.png)

