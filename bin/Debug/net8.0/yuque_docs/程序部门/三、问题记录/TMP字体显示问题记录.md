## 符号字符集运行时有白色块
### 表现：
![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1721731872607-f33d9394-ce1c-4f8e-86fd-33c0607465df.png)

该现象只有在运行时才会出现，Scene界面中查看正常。

### 解决办法：
将Shader中的Gradient Scale从2改为3后表现正常

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1721732045887-35518df0-2dbc-47b8-b500-811363fc793e.png)



## 创建描边材质后，会将周边文字的一部分也绘制出来
### 表现：
![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1721732140400-ba8f7447-b06e-44e8-bb1d-35533dd3231f.png)

描边的Shader设置：

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1721732194158-9b4ac872-841a-4d8b-b95d-381066fa6cfe.png)



### 解决办法：
生成字体时，将Padding从1改为2，增加字之间的间距。

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1721732269898-876072c6-f634-4d57-8cbb-48e6b524e672.png)

