# Odin Inspector相关介绍
## **官网地址：**
[https://odininspector.com/](https://odininspector.com/)

## **主要功能：**
Odin Inspector主要用于扩展Unity编辑器，Odin提供了很多非常方便的attribute和一些方法可以很有效率的提高自定义编辑器的效率

## **Odin开启Editor Only Mode**
Odin的模块中，如果不希望使用Odin Serializer来进行序列化（会build进包里），可以在<font style="color:rgb(199, 37, 78);background-color:rgb(249, 242, 244);">Tools > Odin Inspector > Preferences > Editor Only Mode </font>中进行修改

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724068233323-207c3bdf-09b6-4b32-8cad-0eb593989140.png)

关于Odin Serializer：[https://odininspector.com/odin-serializer](https://odininspector.com/odin-serializer)

Odin Serializer可以序列化一些泛型，字典，多维数组等常用数据结构

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724811212252-115a2513-185c-419b-8895-fb66100ef5a2.png)

## Odin相关的示例
可以在<font style="color:rgb(199, 37, 78);background-color:rgb(249, 242, 244);">Tools > Odin Inspector > Getting Started </font>中找到

这里可以看到如果是EditorOnly，OdinSerializer被Disabled了

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724068450089-3957ec46-2f25-48d5-8971-c486a34f6f29.png)

点击Get started可以看到各种示例

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724068695607-a5e25f41-e175-4d8d-b007-417f6145587e.png)

其中最开始最常用的应该是Odin的各种Attributes

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724068767440-72a3a815-2e3b-46b3-b787-987a09faab06.png)

# Odin相关Attribute举例
## **<font style="color:#DF2A3F;">LabelText</font>****：**
:::info
**可以改变property的名字并且支持中文：**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724069271055-16a39b4f-c945-4ffd-a0cc-967004c2d3ea.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724069296048-7b07a151-ab0e-4dcf-89d0-5e697da52884.png)

**一些特殊用法：**

支持嵌入c#代码：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724069629252-ebe386eb-afbe-42f8-ba68-0cfd4b937ba0.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724069567297-45617a0d-be37-4eb9-a334-ad9950e3b6f6.png)

或者Icon

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724069672319-a6073ca2-444f-447c-98b1-0b939feab7ce.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724069683347-cf822e76-69f7-46e0-b7a4-b7782cc3634a.png)

:::

## **<font style="color:#DF2A3F;">Group相关：</font>**
:::info
**例如****<font style="color:#DF2A3F;">[HorizontalGroup]</font>****可以把多个property放到同一行**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070113710-eea6cb44-9d92-4ec3-bdbc-80633db83a84.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070132702-e56f6c98-1513-4344-9ba6-af99fe9159d2.png)

**<font style="color:#DF2A3F;">[ButtonGroup]</font>****可以对Button进行组织**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070382859-6e3ebe56-5451-479b-807b-42699c89cebd.png)

**<font style="color:#DF2A3F;">[TabGroup]</font>****可以分标签栏**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070507288-b294ebaa-7962-40ad-8880-e791b6b8f58d.png)

:::

**其他例如Conditionals当中****<font style="color:#DF2A3F;">ShowIf，HideIf，</font>****都是非常常用的attribute**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070695107-5e1e8aed-fc61-49c1-8d17-f6440a1f2bdc.png)

注意：Odin中都是支持复杂的C#表达式的

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070727028-3e1181a1-3ad1-4e6d-9936-94676951f907.png)



# Odin Editor Windows
Odin中可以继承不同的EditorWindow来扩展自己的编辑器，相比于Unity自身，会方便非常多

具体例子可以看：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724070968892-7dfc416a-c407-4ebc-94e7-9a2d327f7a0d.png)

另外官方文档中提供了了一个非常复杂的编辑器例子：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724071359362-ad744f52-e79e-41ce-936d-e0fb28a982e1.png)

# 一些比较有用的用法思路
## 自定义下拉框的内容
如果这个字段要选择一些ID（例如这个示例中选择加载的是对应的prefab场景），用Odin可以很简单的处理：

![](https://cdn.nlark.com/yuque/0/2024/jpeg/46024715/1724072093525-ad969b50-6b06-48db-b3ff-b6cce389423e.jpeg)

首先对property加上**<font style="color:#DF2A3F;">[ValueDropDown]</font>****的attribute，其中填上一个自定义的方法**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724072206890-ca030968-ec74-4277-a375-44d29aa1967d.png)

在自定义方法中返回一个ValueDropdownList即可

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724072310891-fdabe79d-fec7-4c1c-848d-984eea3f6f48.png)

## 自定义列表绘制和行为
如果有一个List，希望能够很方便的自定义这个List的绘制并获取一些事件，用Odin可以很方便的做到

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724073164215-72a8a5cf-57d2-4903-9063-2b66ffb87385.png)

可以给property加上如下Attrubute：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724073318043-79cb1644-c067-4487-9255-8d5ad6bd1348.png)

这里加上了两条Odin的Attribute，分别来解释一下：

**[HideReferenceObjectPicker] : 会隐藏掉ObjectField（为了美观和安全）**

**[ListDrawSettings]：会重写数组/列表的绘制**

例如：

**<font style="color:rgb(24, 24, 24);">[ListDrawerSettings(NumberOfItemsPerPage = 5)] 会让List里面的Item在呈现的时候进行分页</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724073732111-0e8e64dd-2c6f-4db8-b30f-237623e2d3df.png)

在上面的例子中，我获取了OnBeginListElementGUI事件，然后用GUI增加了List下元素的绘制方法

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724073959971-383765ba-38fe-4736-bf9a-612b8cce5a56.png)

并且支持undo：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724074155841-b77db10b-0f8e-4cc5-a114-580528ca9f38.png)

