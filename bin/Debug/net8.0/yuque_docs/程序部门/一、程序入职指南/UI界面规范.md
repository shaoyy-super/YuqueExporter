

## 一、界面布局
### （一）复杂界面
比如游戏主界面、背包、商城、活动、战斗等界面，按照布局划分节点，如下图所示：

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714284541630-5cb25be6-5ade-4352-98b1-0fb6600a2d7d.png)

布局节点中心点与锚点信息：

+ 上 **Top**![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285264786-9d122888-461a-44e4-9be7-369a00545c42.png)
+ 下 **Bottom**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285287530-91a262f9-ba37-459d-96e4-63ed9416005f.png)
+ 左 **Left**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285313502-24e0cbf3-08a9-4309-ab3c-30277e053236.png)
+ 右 **Right**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285332737-ed3f19b1-823a-4b1e-905c-7bd3a01a4143.png)
+ 左上 **LeftTop**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285348219-87c975d0-edf2-4bb9-8173-7d6c938af530.png)
+ 左下 **LeftBottom**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285373336-6030c59f-0d6d-484a-a3a4-15f4bb988051.png)
+ 右上 **RightTop**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285395403-58a37ef8-aa45-45fc-847b-4aa21c22939f.png)
+ 右下 **RightBottom**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285410672-b47649ee-6d2e-4c3f-9b69-268d4f012573.png)
+ 中心 **Center**
+ ![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285426550-cad530cc-8cfa-4abc-b6bb-1a24ce36765c.png)

### （二）简单界面
比如`MessageBox`、道具详情等界面，分成`Bg`和`Content`两个节点，如下所示：

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714285003544-4ab736fa-917b-4191-a8e0-556161a405bd.png)

## 二、界面层级结构
原则是在界面布局清晰的情况下，尽可能减少节点层级。如下所示：

划分了`Bg`和`Content`节点，剩下的关闭、确定、取消按钮和标题、文本内容都直接放在`Content`节点下。

假如有其他的背景修饰图标，可以都放到`Bg`节点下

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714286067746-7fe21755-b442-4f20-9126-b4101d18502c.png)

## 三、节点命名
UI节点命名遵循以下几条规则：

+ 大驼峰命名法，比如 `BtnClose`
+ 命名要简洁且能表达出节点的含义，比如`TextItemName`：道具名字文本,`BtnLvUp`：升级按钮
+ 避免过长的命名，可以使用常见的缩写简化命名，比如 `Background->Bg，Button->Btn，Image->Img`
+ 不需要特别关注的的深层级的节点，可以不参照命名规范



<font style="color:#DF2A3F;">注意：</font>

+ <font style="color:#DF2A3F;">以上规则是对整体的界面制作做一定的约束，特殊界面可以根据使用情况做适当调整</font>





