

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
  
详见视频：[适配_横屏_动画示意.mp4](https://snh48group.yuque.com/attachments/yuque/0/2025/mp4/43256889/1736406684657-50ab9d3c-852a-4e3c-a43e-69dddf44788b.mp4)  
  
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
3. <font style="color:rgb(0,0,0);">icomoon_</font><font style="color:rgb(4,4,4);">monopoly run</font>

**字体常用颜色**

通用文本  #272727   #474747  #717171  

****

**icomoon 码位对照表**

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736932134370-a2f7907c-9aec-458a-8848-66d4cc41ff14.png)

现阶段尽量使用已有图标，后期统一替换。相同名词要用同一个图标表意。临时替用的图标需要做好记录，以免造成混乱  
需要添加的图标存放目录：通用组件\icomoon（Ai文件，512x512）

图形文字制作文档：[https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/dvn5ql4u2kdganai  ](about:blank)

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
![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736931801816-270d0738-6be1-4b5b-891c-fd12fb6ffb38.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737368177470-fbe24070-f1b0-4598-a105-9d049bfc6f85.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737368101173-4cf6f70f-6428-45e8-88b9-f7d987b262e1.png)![](https://cdn.nlark.com/yuque/0/2025/jpeg/47215391/1736931934188-662ddefe-8fb0-4894-8d18-fe6920ea29bc.jpeg)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737358069503-c05021d0-219d-4b12-965a-4f4467ee00e9.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737358075014-18d76541-38c8-4596-9d0b-3ba2a38a8d05.png)

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736931928650-15a3dd8a-98f1-4a31-b0ad-5b9ca0ac6ffa.png)

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736931977578-6d12653e-bea4-4b45-a69b-d4e0d8761e1d.png)（标注例）



字体标注：

（例）1 24px #000000 居左 栏宽240

1表示1号字体；24表示字号；#为色值



（例）2 e994 24px #ffffff

图形字体要标注unicode码位



# 二、美术风格要求
### 01.设计
**<font style="color:rgb(4,4,4);">风格关键词：简洁，平面，潮流，图形化，弥散光设计。 </font>**

<font style="color:rgb(4,4,4);">视觉包装整体考虑凸显游戏性和玩法沉浸感为主。UI层体现功能提示、界面引导、 信息传达为主旨。因此设计上采用轻量简洁的色块设计元素为主，避免冗杂花哨的 </font>

<font style="color:rgb(4,4,4);">设计，喧宾夺主。 </font>

<font style="color:rgb(4,4,4);">整体包装以亮白色为基调，加以蓝粉色的潮流撞色配色和部分高饱和图形元素点 缀，体现时尚感和现代感，体现元宇宙的多元化和多彩化概念。 </font>

<font style="color:rgb(4,4,4);">整体UI包装区别于传统，轻量化的设计服务于游戏场景，人物，功能玩法，表现上倾 向于加入更丰富的动效细节视觉呈现，增加活泼感和趣味性体验。 </font>

<font style="color:rgb(4,4,4);">活动类界面则追求更多的视觉刺激，紧密结合主题，更偏向于场景化和生活化的包</font>



参考目录（SVN）：UI\正式资源\6. 参考图

横版

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935340174-ae7b544a-80ab-493f-821b-e775146497de.png)

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935387854-1c8d3c86-a094-439b-afcd-13f85d5a1e7f.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737094803395-85b36884-8077-4f88-a714-3de83e1ec095.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935411148-b901feaa-6941-430e-854b-6249f98ca2b0.png)

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935477692-74fb29dd-f9e9-4403-aaff-5c496f62c75c.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935507702-abb86bd3-77c3-4484-927a-e573c1ea587d.png)

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935674752-ff18c370-eade-4c8c-b4f9-30ebe39e2477.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1736935698746-d4e25989-d566-4bb6-95d7-1ad2f1df24db.png)

竖版

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737094852063-89fa40c6-b497-4100-a0f3-3f682cc5ab01.png)

![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737094888446-dc765353-f27a-41ac-a6ef-76cda8c84af0.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737094907827-85a39d37-367a-4e09-991c-ae5165727e12.png)![](https://cdn.nlark.com/yuque/0/2025/png/47215391/1737094946703-a5deb7ae-d959-49e6-9c14-25b284b0c625.png)









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

