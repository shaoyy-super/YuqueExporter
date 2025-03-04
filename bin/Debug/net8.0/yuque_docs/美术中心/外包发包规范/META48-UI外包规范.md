c

# 一、制作规范
### 01.适配
![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1721989410263-e61fb382-7156-4ba1-b4db-f7de52b73415.png)

 文档提及的具体像素基于1500Px×750Px的逻辑分辨率  
  
红色框——1500×750（18∶9）标准制作分辨率<font style="color:#DF2A3F;">（制作尺寸）</font>  
绿色框——1624×750（21∶9.7）iPhone  
紫色框——1750×750（21∶9）  
蓝色框——1500×844（16∶9）模拟器  
黄色框——1500×1124（4∶3）iPad  
橙色框——1500×1500（1∶1）折叠设备  
  
详见视频：[适配_横屏_动画示意.mp4](https://snh48group.yuque.com/attachments/yuque/0/2024/mp4/43256889/1721989764685-0c2c41ad-9495-466a-8dcd-ed16d4b14258.mp4)  
  
<font style="color:#DF2A3F;">注：界面元素应当根据功能或视觉需求设置对应的锚点，随着设备分辨率变化而动态调整相应的位置。  
</font><font style="color:#DF2A3F;">作为背景图的UI资源规格统一为1624×1000</font>





### 02.网格&排版
![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1721990376633-f233cf86-c27c-4b47-8605-1b6fe0aa8757.png)

主网格

网格基于一格24px，上下间隔12px（36点行距）进行设置。

界面元素（按钮，控件等）应该以占格或占行的思路来进行排版，保证界面的一致性，节奏感和对齐。



界面中相对固定的元素，比如弹出框，应该尽可能地对齐主网格。而位置较为灵活的元素，比如提示框或者活动入口，应该基于自身坐标建立网格。



<font style="color:#DF2A3F;">注：网格是一种参考线，单位可以是半格，四分之一格。实际运用中务必以视觉感受为优先，不要被网格和参数束缚。</font>

<font style="color:#DF2A3F;"></font>

字号  
常用字号为18，24，30，36，48，60，72点，为了保证多字号情况下的对齐问题，尽量使用常用字号来进行排版。避免使用18点以下的字号以保证文字易认性。  
  
栏宽  
在设计软件和引擎的排版器中，栏宽都要设为字号的整数倍。  
Unity中的文字要设置合理的栏宽和缩略方案，避免多语言情况下的文字溢出，内容冲突和视觉混乱。  
  
行距  
18点字号的行距为30点，24点字体的行距为36点等等……这些基本设置会加入到Unity的预设里，方便前端调用。  
  
富文本说明  
[http://digitalnativestudios.com/textmeshpro/docs/rich-text/](about:blank)<font style="color:#DF2A3F;">  
</font><font style="color:#DF2A3F;"> </font>

### 03.字体


1. SourceHanSansCN-Medium
2. SourceHanSansCN-Bold
3. icomoon_fishing
4. Ethnocentric Light
5. SourceHanSansCN-Bold-High

****

**icomoon 码位对照表**

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1726826658243-8543bef5-8f3f-48d3-a0cd-8ff6cf937115.png)

现阶段尽量使用已有图标，后期统一替换。相同名词要用同一个图标表意。临时替用的图标需要做好记录，以免造成混乱  
需要添加的图标存放目录：通用组件\icomoon（Ai文件，512x512）

图形文字制作文档：[https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/dvn5ql4u2kdganai  ](about:blank)



字体材质

| 材质名称 | 效果预览 | 备 |
| --- | --- | --- |
| 1001 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736936736001-7f88990c-1f35-4de3-a6f8-cb4aff67bb67.png) | |
| 1002 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736936861454-1999f5b0-ae78-4417-962f-6e318cb5cdf8.png) |  |
| 1003 |  |  |
| 1004 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937087083-e28f036d-54ab-42c0-a9fa-5c090a737910.png) |  |
| 1005 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937097419-ff5badb2-6062-4204-8fa3-fbf2239625da.png) |  |
| 1011 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937107186-5dff416a-ebf8-408c-be29-d89a80b98d67.png) |  |
| 1012 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937119667-1dfe6874-3825-4afe-9a9b-a6820c7440b4.png) |  |
| 1013 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937136683-2c26e1f7-51af-41a0-acb5-23841737165f.png) |  |
| 2001 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937445484-a5e922d2-b5e5-4224-96db-853e004ede7a.png) |  |
| 2002 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937454624-8d513555-b60c-40fb-bb7b-1c979511d2bc.png) |  |
| 2011 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937463660-a12b95f0-0ebc-4ba5-b984-b876cfd3e257.png) |  |
| 2012 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937473688-ffc8e893-052a-4e20-8cc3-b505f1697791.png) |  |
| 2013 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937482760-0297ba6c-2cdd-4f2b-bd09-7dd05256a151.png) |  |
| 2014 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937494991-c43de319-5a05-4c98-bf6b-9d9b0aadc85c.png) |  |
| 2015 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937503969-6e4d42c2-dea5-4968-82df-fdd1b55dffc3.png) |  |
| 2016 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937515366-6c07f26a-ca5c-4027-aa73-6184af133641.png) |  |
| 2017 |  |  |
| 2018 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736937527667-2d2b98df-14fb-4fd9-993d-c0c95c52f088.png) |  |
| 3001 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938144805-39dc2ef5-c374-4b7f-8da2-f5b3914c8120.png) |  |
| 3011 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938130514-691a56f5-1847-4569-bcd4-38eb1bbdceea.png) |  |
| 3012 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938162634-fbe1274b-eda1-4478-baa2-41199d4d5683.png) |  |
| 3013 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938172041-adcf512c-551a-4273-b83b-4d6505306557.png) |  |
| 4001 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938200829-6eb1b26c-419d-445e-a4dc-6df1d6a63ad4.png) |  |
| 4002 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938219449-a140639e-7871-42c7-80db-6ed569af9d4b.png) |  |
| 4011 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938252548-d8fc18cb-12a7-4701-b898-5de003570143.png) |  |
| 4012 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938264563-3cae9c0f-6d34-4ea2-af16-2e438bd4079c.png) |  |
| 4013 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938272470-b4157959-7b01-492c-a655-876a7aafb151.png) |  |
| 4014 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938285597-b9c8aae4-ade3-494e-9834-e8288b2416cf.png) |  |
| 4021 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938299392-97ab8d8a-177d-42cb-93e5-3081903592d7.png) |  |
| 4031 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736938312123-72dae73f-4f25-437b-9219-0fe2b09ccc11.png) |  |
|  |  |  |
|  |  |  |
| 5001 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736995699618-8218f129-4bd7-40b3-b7da-8a0e20be9fb1.png) |  |
| 5002 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736995711528-de4e49d0-010b-4c60-8817-4d94a83f0f0b.png) |  |
| 5003 | ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736995749200-a1400ddf-bec7-433f-9598-840cf62a4ede.png) |  |
| 5004 |  |  |
|  |  |  |




### 04.安全区
![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1721990561503-a94b3d81-e325-4f78-b6e8-3b805e126604.png)

 目的  
安全区的设置为了避开某些设备的圆角，曲面区域，Home Indicator等软硬件限制，更好地实现交互和响应，提升UI设计的包容性。  
  
参数  
左右预留30，上15，下27。  
  
原则  
UI中的主要元素都应保证在安全区以内，比如文字和交互按钮。装饰性元素或面板不受安全区限制。  
  
<font style="color:#DF2A3F;">注：实际效果根据Unity中各设备的安全区域数据进行二次修正后得到</font> 

### 05.通用标注
:::warning
### **真金版**
### ![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1733304170955-f9d595e8-c5a7-46d9-9940-216f54e2b412.png)
### ![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1732601706654-53b42504-65df-45bb-a952-a8acb92f0326.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1739427114621-bee0098c-a0a1-4475-8f4a-d99d2d842ff4.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736935133661-a14fb0c9-609a-48a1-a2e8-85f1ae2c006d.png)![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736935133134-4ed060db-fa04-48f9-bac8-041ad2533a2a.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736935151695-a2dbea16-c938-4ad4-a2cb-95557e1a084d.png)
### ![](https://cdn.nlark.com/yuque/0/2024/png/47687966/1733897516206-a3aaeb0d-ec76-4b89-91fa-8a7d917a2e6d.png)
### ![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1734931800666-214e360e-cca4-4b60-b217-8b0a1d537bd7.png)
:::



:::success
### **绿色版**
### ![](https://cdn.nlark.com/yuque/0/2025/png/43892379/1740385103940-dc256de2-9960-4412-b19a-72815dda7899.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43892379/1740385091365-6bd39271-846c-4ac3-90f1-5d06dd9e3181.png)
### ![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1732601706654-53b42504-65df-45bb-a952-a8acb92f0326.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43892379/1740385122217-59a0f764-bbe5-433a-b244-684f0b10d388.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43892379/1740385125258-160e637c-596e-4bc7-96ca-143c5b02cffd.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736935133661-a14fb0c9-609a-48a1-a2e8-85f1ae2c006d.png)![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736935133134-4ed060db-fa04-48f9-bac8-041ad2533a2a.png)
### ![](https://cdn.nlark.com/yuque/0/2025/png/43256889/1736935151695-a2dbea16-c938-4ad4-a2cb-95557e1a084d.png)
### ![](https://cdn.nlark.com/yuque/0/2024/png/47687966/1733897516206-a3aaeb0d-ec76-4b89-91fa-8a7d917a2e6d.png)
### ![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1734931800666-214e360e-cca4-4b60-b217-8b0a1d537bd7.png)
:::





（标注例）



字体标注：

（例）1 24px #000000 居左 栏宽240

1表示1号字体；24表示字号；#为色值



（例）2 e994 24px #ffffff

图形字体要标注unicode码位



# 二、美术风格要求
### 01.设计
**风格关键词：赌场元素，偏厚重，偏写实，质感**

细节结构应小而精，避免过于粗犷。界面的层级需合理且清晰，交互合理。



通用元素（面板，衬底）应该以蓝灰色/深色为主，以适应不同的背景、场景、 图标等视觉元素。

常规系统的设计应尽量保持克制，以功能为优先，用细节、装饰、动效来体现质感与完成度。同时为需要表现力的场景、特殊界面留出充足的层级空间。

商店、主题活动等界面可以在保持基本设计语言的范围里和满足功能需求前提下，尽可能增加变化和表现力来丰富设计元素。



参考目录（SVN）：UI\正式资源\6. 参考图

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115209600-1298a4ad-cd3a-498a-8f43-5d0493119458.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115253207-28718174-0034-4c31-9433-f2d9e568ac34.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115269241-ae7dafad-301e-4d92-b68b-00d54c0f0896.png)



![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115309452-9dbb5fbb-005d-4520-94b9-9f28613b68d4.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115381520-fe5bd5d4-a23c-4ea2-80dd-8f6920f021c5.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115421182-75c4019b-6c85-418f-8b5b-18960872b269.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115487008-3f7513c3-44d6-4411-b9b2-ed7a7ad91966.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1731115557588-17cd6894-d0d6-41bc-b182-48c04a036eff.png)











### 02.层级
界面层级由低到高：  
启动、主界面  
各系统界面  
弹窗  
提示框（tips）  
提示（文字提示、战斗力变化等）



为了区分各级界面的层级，在视觉上应该有对应清晰的体现。

全屏的系统界面……需要点击返回或关闭按钮来退出界面。

弹出框背景为蒙黑处理，点击关闭按钮后关闭弹出框。  
  
 

# 三、提交规范
### 01.资源命名
![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1722218390682-5285b5b9-e80d-4ce6-bd88-0d06be23f3a8.png)

 文件夹  
前缀统一为 <font style="color:#DF2A3F;">ui</font>  
中缀为<font style="color:#DF2A3F;">系统名称</font>，公用资源、按钮、物品图标等。  
后缀为<font style="color:#DF2A3F;">编号</font>

  
![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1722218417202-581579c1-eca7-4ad9-bb8c-83c38e97947e.png)  
图片资源（png）  
前缀为文件夹<font style="color:#DF2A3F;">中缀的缩略</font>，取前3个字母  
中缀为图片<font style="color:#DF2A3F;">本身名词</font>  
后缀为<font style="color:#DF2A3F;">编号</font>，可以表示类型、状态、规格等。按钮后缀个位数表示状态，十位数表示类型  


![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1732602112770-29b3cdbd-da9e-4161-8eba-6313f19fe927.png)

背景资源（png）

前缀<font style="color:#DF2A3F;">bg</font>为背景简写

中缀<font style="color:#DF2A3F;">invite</font>为所用到的系统

后缀为<font style="color:#DF2A3F;">编号</font>，此编号与文件夹编号保持一致

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1722218508771-6e1befa0-19bf-4262-b378-e04315820d6e.png)  
图集文件（tps）和导出资源（png）(tpsheet)  
图集文件和导出资源命名与文件夹保持一致  
公共图集导出资源命名后加[F4]，提高质量

TexturePacker使用文档：[https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/tl6ofis7blgh6995](about:blank)  
  
<font style="color:#DF2A3F;">注：  
</font><font style="color:#DF2A3F;">命名统一小写，图集超出2048新建02图集后要注意重命名问题  
</font><font style="color:#DF2A3F;">项目内系统名词要保持统一  
</font><font style="color:#DF2A3F;">需要配置的资源（物品、头像、技能等）命名与策划部门协商决定</font>



名词对照：

图片资源

公共图集——public

按钮——button

面板——panel

物品——item

红点——badge

遮罩——mask

背景——bg

按钮——button

标签——tab

复选——checkbox

单选——radio

筛选——menu

箭头——arrow

图案——pattern

装饰——deco

线——line

框——frame



系统

玩家头像——avatar

战斗——battle

通用——public

邮件——email

装备——equip

主界面——main

物品——item

登录——login

提示——hint

弹出框——popup

奖励——reward

商店——shop

任务——task

提示框——tips

转圈——waiting

加载——loading

设置——setting

帮助——help

排名——rank

活动——activity





### 02.资源储存
 系统界面psd（SVN）：\UI\正式资源\2. 系统界面psd\系统名称  
  
组件Psd（SVN）：D:\组件_aquaman  
组件为外部链接文件，为方便多人合作使用需要放到统一的目录下，所以需要在D盘单独Checkout一个目录，确保打开psd文件时能够找到对应目录  
  
切图文件（Git）：aquaman_artwork\UI\正式资源\切图文件  
  
效果图（Git）：aquaman_artwork\UI\正式资源\效果图\系统名  
  
示意图（Git）：aquaman_artwork\UI\正式资源\示意图\系统名  
  
导出文件（工程目录）：client\Assets\Arts\UI\Atlas  
  
<font style="color:#DF2A3F;">注：</font>

<font style="color:#DF2A3F;">对应的效果图和示意图务必保持统一的命名  
</font><font style="color:#DF2A3F;">通用组件的标注要加到本文档内，避免出现多处重复标注，以便修改</font>





# 四、审核规范    
 
### **01.时间预估**
递交制作时间规划，时间可以根据需求拆分为不同阶段，这个时间包括预留的反馈修改时间与切图标注时间。时间一旦定下来尽量不要延期，如有特殊情况提前反馈。



### **02.设计阶段**  
**草稿阶段**

应提供大致的交互方案，大致的设计方向/思路/草稿。

<font style="color:#DF2A3F;">注：需求案仅仅提供功能需求，最终的交互排版应结合视觉效果与功能需求进行调整。</font>



**细化阶段**

根据敲定的草案提升界面上各个模块的细节，整体界面的完成度以及补齐界面中的各种状态。界面上的元素应具备可复用性，可编辑性（界面上的模块追求以矢量为主，效果尽量以图层样式来实现）。



### **03.PSD图层整理**
![](https://cdn.nlark.com/yuque/0/2024/png/43892379/1722245102170-1f1c1051-857c-4490-8a4f-84a8b79937cc.png)

psd文件内应该保持精简整洁，所有组件都应使用外部链接的文件，文件应该包含标注内容。

进行具体界面设计前，务必要了解当时的通用组件情况（按钮的层级，页签的层级，控件的使用逻辑等），相同的组件应当通用，保证界面视觉和逻辑上的一致性。需要增加组件时，提前做好沟通工作。

**组件定义**

当一个界面元素跨系统出现时，作为组件，例如通用按钮、资源栏、弹出框背景等等。



### **04.切图&标注**
资源的命名与示意图的标注应符合规范

<font style="color:#DF2A3F;">参照通用标注、资源命名</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256889/1728359586771-515e7efa-be14-486b-9d40-3549036e896e.png)

