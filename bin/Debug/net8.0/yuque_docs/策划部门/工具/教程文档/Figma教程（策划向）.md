# 概述
## 文档说明
1. 本文档为Figma教程，不再赘述基础的移动工具、形状工具、文字工具等，默认阅读者简单使用过至少一款设计软件。
2. 本文档主要有两部分内容：
    1. 针对策划UE工作流提出的**团队协作**建议
    2. Figma的一些特色功能与**实用技巧**

## 策划需求
![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/26927517/1719223406828-84299a67-e4ba-49e9-a339-2d3e2ba7d3ab.jpeg)

:::warning
前期制定以下规范，有利于提高策划之间 以及与UI沟通时的效率，降低试错成本。

1. **按钮、图标组件化**：将常用按钮与图标做成组件，方便策划重复调用，也方便UI一一对应
2. **页面、弹窗大小样式规范化**：提前定义大中小弹窗、二次确认弹窗等通用大小等，方便策划重复调用
3. **交互一致性**：提前约定通用交互，避免不同策划间设计不一致
    1. 关闭、跳转方式（关闭按钮、返回按钮、界面空白处、取消按钮等）
    2. 滑动交互方向
    3. ...
    4. （可通过参考游戏一致/及时查看其他策划的UE避免冲突）
4. ~~简单的布局约束~~

:::

## Figma简介
一款基于浏览器的全能型设计工具。

1. 全平台
2. 云端文件
3. 历史版本
4. 共享
5. 实时协作
6. 团队沟通----Comment
7. 组件库

# 实用功能
**Tips：多用组件（Component）、多用画框（Frame）**

## 组件（Component）
**<font style="color:rgba(0, 0, 0, 0.8);background-color:rgb(253, 253, 252);">组件是可以在你的设计中重复使用的UI元素。 它们可以帮助你跨项目创建和管理一致的设计。</font>**

可以将常用的按钮等做成可复用的**初始组件**，再由初始组件实例化为一个个**实例组件**。

按钮可以添加**变体（Variant）**：如将一个通用按钮做成**默认态、选中态、禁用态**

> 例：创建多种状态的按钮组件
>

1. 先创建一个文本框
2. 按下Shift+A，为文本框添加自动布局（也可右键选择）
3. （这样创建出来的按钮会有布局约束，拉伸时可以保持文本居中不变形）
4. 为按钮添加填充、边框、圆角等
5. 复制、制作多种状态的按钮



![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719207420191-f67cd31f-928c-442f-ab4a-0e610ae88f40.png)

6. 框选，在顶部导航栏选择【创建多个组件】
7. 框选，在右侧属性栏选择【合并为变体（Combine as Variant）】，此时按钮被合并成一个具有多个状态的组件
8. 对状态进行重命名：如Default、Disabled、Press等

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719207529174-b0966c6a-d9a2-4fa4-898e-f2c575ce4e49.png)

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719207573959-7cc85c18-b80d-442d-87bc-437ccc06c1fc.png)

9. 应用：从组件库中拖出组件，在右侧属性栏中可以快捷选择按钮状态

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719207783340-6a491a65-0615-47e6-807d-b664a8df1e9c.png)

## 组（Group）和画框（Frame）
Frame是Figma特有的一个概念，是更高级的组。

> 在拼UI时可以多进行分组和添加框架进行管理。
>

+ **Frame**

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719219901344-bf844f80-1e63-4953-a6b6-487255c3f482.png)

+ **Group**

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719219919799-7935f0d9-bcf6-4ebc-a1b2-3c3b5a3145fe.png)

## 布局（Layout）与自动布局（Auto Layout）
**布局**

+ 框选后，右下角会有自动整理按钮，可以快捷进行整理布局
+ 也可以在右侧属性栏进行进一步的布局需求

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719219194598-f6bd480f-f07d-44dc-8661-87a15939a814.png)![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719219375544-5f264d3a-f10f-495b-baae-04dd8623c68e.png)

**自动布局**

+ 可以在右侧属性栏添加自动布局进行更高级的布局约束
    - 适应内容
    - 固定宽度
    - 填充容器：根据外轮廓的大小变化，内部元素大小也进行对应变化

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719219473461-0504e8f0-f120-467c-969f-b7d35a3a094f.png)

# 原型（分享与预览）
+ 右侧属性栏可以从【设计】界面切换为【原型】界面
+ 常用的交互方式On Click，点击进行界面跳转
+ 可以直接拖拽界面上的小圆点新建交互（类似画流程图的方式）

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719215139233-f7337f60-58c1-4eac-a37a-4bcc4957b426.png)

+ 点击右上角运行按钮，可以预览各个交互起始点的原型

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719215292438-888ec3cd-b686-497a-a5f4-488e259eba4e.png)

+ 运行原型后，点击分享按钮即可复制当前原型的链接
+ 被分享者点开链接后可查看原型，并且可以在左侧查看该页面包含的所有交互

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719312613471-8b768623-ad5e-4906-b36c-bbf3511473a4.png)

# 插件
**插件入口：**

+ 搜索插件名，点击插件即可运行
+ 可将常用插件加入收藏

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1718873169594-0ce466c1-341a-4e7b-909f-5ce346092840.png)

Figma有丰富的插件库，以下是策划做UE时较为实用的插件。

+ Icons8/Feather Icons：比较全的图标库，对标常用的阿里巴巴矢量图标库 www.iconfont.cn
+ AutoFlow：自动连线工具，用来制作流程图/框架图
+ Content ReelL：自动填充内容（图标、文本），制作好友、聊天等系统时可以填充内容进行占位
+ Deck：将figma转换成ppt的工具，会保留所有可交互组件

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1718877043227-1066c309-ef99-4633-a9e8-c6f5c5da913f.png)

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719195175108-06172f5c-5598-4047-a637-71c897ca243e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1718877103502-0306307c-69b5-44df-ada4-3648501c277a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1718877140430-9cb15279-76d2-429d-a8ff-698663b91ae7.png)

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1718877269647-d42b1994-c4a2-4a6e-8693-61b7ebe1824f.png)

# 快捷键
**<font style="color:rgba(0, 0, 0, 0.8);background-color:#E7E9E8;">Ctrl + Shift + ？</font>**  查看快捷键帮助

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1718872926372-9c2bd539-0d54-4f76-91f8-989a34dfa169.png)

**<font style="color:rgba(0, 0, 0, 0.8);">一些常用的快捷键/快捷操作：</font>**

**<font style="color:rgba(0, 0, 0, 0.8);background-color:#E7E9E8;">Shift + 0/1  </font>****<font style="color:rgba(0, 0, 0, 0.8);">缩放到100%/适合显示</font>**

**<font style="color:rgba(0, 0, 0, 0.8);background-color:#E7E9E8;">Ctrl + R </font>****<font style="color:rgba(0, 0, 0, 0.8);">重命名</font>**

**<font style="color:rgba(0, 0, 0, 0.8);background-color:#E7E9E8;">Ctrl + Alt + K </font>****<font style="color:rgba(0, 0, 0, 0.8);">添加组件</font>**

**<font style="background-color:#D8DAD9;">Ctrl+Shift+C</font>**<font style="background-color:#D8DAD9;"> </font>**<font style="color:rgba(0, 0, 0, 0.8);">快捷将所选内容复制为PNG</font>**

属性栏数值调整：按住Alt再拖动鼠标进行快捷调整

加超链接：选中文字，粘贴链接

# 协作
Figma的库和评论功能方便了团队协作。

## 组件库
+ 建议将所有组件等规范单独放在同一个页面中，供团队成员共同调用
+ 可以将常用图标也单独创建

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719208376358-316bf106-c621-4e9c-9d94-a782a92229f3.png)

## 样式库
> 两种方式：
>
> 1. 单独创建页面存放，应用时复制属性
> 2. 在右侧属性栏中创建对应的样式
>

+ **文本样式**
+ **颜色样式**

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719210405874-f849e7fb-0360-4507-b87b-db28c79933ed.png)

## 评论
类似文档评论的功能，可以在画布的任何位置添加评论，也可以向原型和设计文件添加评论。

+ <font style="color:rgba(0, 0, 0, 0.8);background-color:rgb(253, 253, 252);">单击工具栏中的评论图标进入评论模式，或者使用键盘快捷键</font><font style="color:rgba(0, 0, 0, 0.8);background-color:#D8DAD9;"> </font>**<font style="color:rgba(0, 0, 0, 0.8);background-color:#D8DAD9;">C</font>**<font style="color:rgba(0, 0, 0, 0.8);background-color:#D8DAD9;"> </font>

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719211078694-e1fe5def-7d7b-4102-b834-205fe50bc6f6.png)

---

Ps：免费版限制

1. 团队只提供一个项目坑位；
2. 团队的项目文件只能创建3个；
3. 团队的每个文件最多三个页面；
4. 文件历史恢复只能恢复最近三十天；
5. 不能跨文件共享做好的组件库、只能共享样式库

# 导出
+ 框选后，可以在右侧属性栏进行导出设置
+ **<font style="background-color:#D8DAD9;">Ctrl+Shift+C</font>**<font style="background-color:#D8DAD9;"> </font>快捷将所选内容复制为PNG

![](https://cdn.nlark.com/yuque/0/2024/png/26927517/1719211257235-2fe7f993-4ba5-4de7-b421-4e7c3448b510.png)

# 附
> Figma中文指南：
>

[开始使用 | FigmaChina](https://figmachina.com/guide)

> Figma插件社区：
>

[Figma插件组件推荐-插件库安装- Figma 中文社区](https://www.figma.cool/plugins)

> 比较简洁易懂的b站系列快速入门教程：
>

[001_Figma开篇简介_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV1wa411V7mU?p=1&vd_source=58cf2b2864e3420e375d9ee01ae8c572)

