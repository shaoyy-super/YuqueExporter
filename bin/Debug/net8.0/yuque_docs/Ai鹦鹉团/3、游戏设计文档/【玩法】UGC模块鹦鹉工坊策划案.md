# 一、概述
### 1.1 文档说明
该文档为UGC模块-创作工坊部分策划案，包括用户创作、作品管理、作品分享等内容。

### 1.2 文档维护
| <font style="color:white;">版本</font> | <font style="color:white;">时间</font> | | <font style="color:white;">负责人</font> | <font style="color:white;">修改内容</font> |
| :---: | :---: | --- | :---: | :--- |
| <font style="color:black;">v1.0</font> | <font style="color:black;">2024/6/18</font> | | 王亚婷 | <font style="color:black;">文档创建</font> |


### 1.3 设计思路
本设计旨在为游戏玩家提供游戏内的ugc创作和分享体验，玩家可以导入音乐并由 AI 自动生成对应的音游谱面和舞蹈作品，为玩家带来了极大的创作便利性，除此之外，玩家还可以对 AI 生成的作品进行二次编辑来完成个性化创作，完成创作后玩家可以将作品发布至游戏社区供其他玩家体验和互动。这不仅丰富了游戏本身的内容，也营造了一个充满活力的 UGC 创作社区。

具体包含以下核心功能:

    1. 作品创作：导入音乐AI自动生成作品、高级编辑（初版不做）；
    2. 作品分享：私域分享、发布至ugc社区；
    3. 作品管理：分为已发布作品和草稿箱作品分别管理；
    4. 社区互动：点赞、评分、收藏、评论、打赏、订阅、举报。（朱梅迪负责）

### 1.4 系统框架图
![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1719301342198-ed60821d-c179-474b-a95b-f0fe52f06da2.png)

# 二、功能说明
### 2.1 作品创作
![创作流程](https://cdn.nlark.com/yuque/0/2024/png/45413786/1719293488439-a8a1aa85-9f39-4821-8bc9-920d6bc9a1b0.png)

#### 2.1.1 导入音乐
导入音乐分为有四种方式，分别为官方曲库选择、本地导入、录音导入、视频提取，默认为官方曲库选择。

    - 官方曲库选择：显示官方歌曲的歌名、歌手、bpm信息，玩家可试听。
    - 本地导入：读取玩家设备本地mp3或ogg音频文件供玩家选择。
    - 录音导入：录制玩家设备播放的声音。
    - 视频提取：读取玩家设备本地视频文件（格式待定）供玩家选择，选择后提取该视频的音频。

后三种方式都有导入音频历史记录功能，便于玩家二次使用。

#### 2.1.2 选择玩法
选择谱面玩法种类，玩法种类与游戏主玩法的种类一致，目前只有传统玩法。

#### 2.1.3 选择难度
选择谱面难度，有简单、普通、困难三种难度。

#### 2.1.4 作品预览
试玩：试玩角色为玩家角色，背景为默认随机背景（可替换）。

二次编辑：可以进行谱面和舞蹈的二次编辑。（暂不开发）



### 2.2 作品分享
#### 2.2.1 作品发布
审核内容：需对发布作品的歌曲版权、歌名信息、封面图片进行审核，默认封面的不需要审核封面图。

审核中：审核期间，作品存在我的作品-草稿箱中，显示“审核中”标识。

审核通过：审核通过后作品存在我的作品-已发布中，且作品发布到社区中供其他玩家体验和互动。

审核失败：作品仍存在我的作品中，去掉“审核中”标识，且发邮件通知玩家该作品未审核通过。

#### 2.2.1 作品分享
游戏内好友：将作品发送给游戏内好友，好友在私聊对话框点击即可弹出该作品的游玩界面。

二维码：生成含有二维码和作品信息的图片，其他玩家打开游戏扫一扫该图片即可游玩该作品。

链接：生成分享链接，其他玩家复制该链接，打开游戏可以游玩该作品。

外部社交平台：可直接跳转至其他平台分享。（待定）



### 2.3 作品管理
#### 2.3.1已发布作品的管理
下载草稿：

    - 复制一份该作品到草稿箱中，草稿箱中该草稿有“已发布”标识与其他草稿区分。
    - 对该草稿进行编辑后可直接更新发布该作品。
    - 若已经对该草稿进行编辑改动，但未更新发布，将“已发布”标识改为“可更新”。	
    - 若已经下载草稿，再次点击下载草稿，则复制一份该作品覆盖原来的草稿数据，无论原草稿是否有改动。	



下架：将该作品从已发布中移除，存一份到草稿箱中，该作品所有互动数据清空。

#### 2.3.2草稿箱作品的管理
删除：从草稿箱中删除，清空所有数据。

复制：复制一份草稿到草稿箱最新，若被复制草稿为已发布作品的下载草稿，即有“已发布”或“可更新”标识，则复制出来的草稿为普通草稿，即不可对已发布作品进行更新发布。

# 三、界面说明
## 3.1入口说明
![游戏主界面](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720065595125-847aafb8-d475-4cf0-984c-ee33f43c6f06.png)

![UGC社区主界面](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720070497838-3f9d2f4a-ff84-4228-afd9-fae14bd6f9c2.png)

## **3.1【鹦鹉工坊】主界面**
![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720071734166-b3e4ef6d-7e5e-4221-b60a-6e50ae1d2ee1.png)

| **<font style="color:rgb(0,0,0);">编号</font>** | **<font style="color:rgb(0,0,0);">控件名称</font>** | **<font style="color:rgb(0,0,0);">点击效果</font>** | **<font style="color:rgb(0,0,0);">控件状态</font>** |
| :---: | --- | --- | --- |
| ① | 页签：已发布 | 点击切换显示已发布作品信息 | 1.选中态/未选中态<br/>2.为进入界面默认显示页签 |
| ② | 页签：草稿箱 | 点击切换显示草稿箱作品信息 | 选中态/未选中态 |
| ③ | 控件：搜索 | 1.点击调出玩家输入进入搜索状态，控件变为如下输入态，④或⑭作品预览栏显示实时搜索结果。<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720074727234-6739401c-1762-4b76-9eea-37616bd35cd8.png)<br/>2.点击右侧关闭按钮，控件恢复默认状态，⑤歌曲预览栏恢复默认显示。<br/>注：若玩家在搜索状态选中了某个作品，退出搜索后应仍选中该作品。<br/> | 1.常规态/输入态<br/>2. 无搜索结果时④或⑭作品预览栏显示为空即可。 |
| ④ | 控件：已发布作品预览栏 | 1.垂直方向滑动<br/>2.控件内部单个作品预览条为按钮，点击切换当前选中的作品，右侧⑤⑥作品详情也同步切换。 | 1.选中态/未选中态<br/>2.默认选中最方的作品（即最新发布/更新的作品）<br/>3.作品预览信息包括：作品封面、歌曲名字、歌手名字、谱面玩法、作品难度、谱面星级。 |
| ⑤ | 控件：已发布作品详情 | 无 | 详情信息包括：作品封面、发布时间、上次更新时间（具体到日期即可）、歌曲时长、歌曲BPM、作品标签。 |
| ⑥ | 文本：“热度=”+作品热度 | 无 | 单一状态 |
| ⑦ | 按钮：去分享 | 点击跳转到<font style="background-color:#FBDE28;">作品分享界面</font> | 常规态/按下态 |
| ⑧ | 按钮：下架 | 点击弹出二级弹窗“确认下架该作品吗？”<br/>1.点击确认则将该作品从【作品广场】（策划案待补充）下架，同时作品从已发布页签移到草稿箱页签中，点赞数等社交参数全部清空。弹窗关闭。<br/>2.点击取消或关闭按钮，直接关闭弹窗。<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720078189507-8be41bd2-77c9-44c5-bb47-a546c9c04081.png) | 常规态/按下态 |
| ⑨ | 按钮：下载草稿 | 1.初次点击将该作品复制一份到草稿箱页签，并飘窗提示“已下载至草稿箱，请前往查看”。<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720084667724-31e2465a-d99e-43d0-a632-dccfce031b9d.png)<br/>2.下载至草稿箱的作品，在预览条右下角需加上“已发布”标识，与其他作品区分；若玩家编辑过该草稿箱作品，则该标识变为“可更新”。![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720085021785-ba16650c-1059-41fb-afae-cea38b42ec8d.png)<br/>3.若玩家再次点击该按钮，则弹出二级弹窗，“新下载的草稿会覆盖这个作品已有的对应草稿，确认下载吗？” 点击确认则覆盖草稿，关闭弹窗。（因为每个已发布作品只能有一个对应草稿）<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720085451197-0bfe617f-7806-4e0e-b8a6-e5d390b5145d.png) | 常规态/按下态 |
| ⑩ | 按钮：游玩 | 点击进入<font style="background-color:#FBDE28;">游玩界面</font><br/> | 常规态/按下态 |
| ⑪ | 按钮：新建作品 | 点击进入<font style="background-color:#FBDE28;">音乐选择界面</font> | 常规态/按下态 |
| ⑫ | 按钮：返回 | 点击返回3.1游戏主界面 | 常规态/按下态 |
|  |  |  |  |
|  |  |  |  |
|  |  | 1.点击出现下拉框，下拉标识变化（倒立小三角变成正立小三角），当前选择在下拉框中有特殊表现。<br/>2.玩家点击下拉框中的选项进行选择，选择后收起下拉框，控件显示同步更新。（点击当前选项仍有效，控制显示难度不变即可）<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1719476778511-3fd6585c-a693-43f7-9673-0a5a8f15510d.png) | 1.下拉框中有简单、普通、困难三个选项，每个选项都有选中态/未选中态。<br/>2.下拉标识有打开下拉框态和关闭下拉框态。 |


![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1720072982427-879bf984-7244-45e6-98ee-e11a69b7734b.png)



①【鹦鹉工坊】：鹦鹉工坊页签，选中态、非选中态；

②【已发布】：已发布页签，选中态、非选中态；

③【草稿箱】：草稿箱页签（草稿箱界面见3.2），选中态、非选中态；

④【搜索】：点击调出玩家输入键盘，输入内容实时模糊搜索。

⑤【游玩】：点击弹出作品游玩界面（见3.3）。

⑦【下架】：点击弹出二次确认弹窗“是否确认下架作品？”，点击确认将该作品从已发布中移除，保存至草稿箱。

![确认下架弹窗](https://cdn.nlark.com/yuque/0/2024/png/45413786/1718876018816-053021c9-c96d-41b9-b35a-82cc5d6b0000.png)

⑧【点赞】：点赞状态和未点赞状态，点击切换；下方显示总点赞数。

⑨【收藏】：收藏状态和未收藏状态，点击切换；下方显示总收藏数；<font style="background-color:rgb(255,255,0);">已收藏作品显示在【我的】</font><font style="background-color:rgb(255,255,0);">-【收藏】页签</font>。

⑩【转发】：点击进入作品分享界面（见3.6）；下方显示总转发数。

⑪【赞赏值】：点击弹窗提示玩家不能赞赏自己；下方显示总赞赏值。

⑫【一键生成】：点击进入创作界面（见3.7）。

⑬、⑭分别为【作品预览区域】和【作品详情区域】，【作品详情区域】显示当前选中作品的详情，默认选中最上方的作品。

## **3.2【鹦鹉工坊】-【草稿】界面**
![](https://cdn.nlark.com/yuque/0/2024/png/45413786/1718884166676-5dccbeeb-99ad-4e71-90b8-c0df0607a05a.png)

①【删除】：点击弹出二级弹窗“是否确认将该作品从草稿箱中删除？”  
	选择确认则删除该草稿；  
    	选择取消弹窗消失。

![确认删除弹窗](https://cdn.nlark.com/yuque/0/2024/png/45413786/1718872025545-3f44a031-a199-42e7-acd0-343c5b606f07.png)

②【编辑】：点击进入作品试玩界面（见3.4）。

③④⑤【草稿预览】：

③为普通类型的作品草稿，作品名为草稿创建日期，选中后右边【作品详情区域】直接显示草稿详情。

④为已发布作品的草稿版本管理，为一个草稿集，左上角有“已发布”标签，右上角有合集标签；点击弹出版本选择界面，选择某个版本后退出弹窗，【作品详情区域】显示该版本的信息，可对该版本进行编辑和删除操作。

⑤为已下架作品的草稿备份，左上角显示“已下架”标签，选中后右边【作品详情区域】直接显示草稿详情。

![版本选择](https://cdn.nlark.com/yuque/0/2024/png/45413786/1718872450892-c2daaba7-7887-4756-9ba9-9c3bd6877262.png)

# 四、配置表说明（必备）
[表格说明](https://snh48group.yuque.com/sup7t9/ugxnhr/sezy0teu6zm7kuoi)

# 五、功能需求清单
程序实现功能此处进行汇总，包含数据统计和GM指令需求

| **<font style="color:white;">编号</font>** | **<font style="color:white;">分类</font>** | **<font style="color:white;">优先级</font>** | **<font style="color:white;">需求描述</font>** | **<font style="color:white;">自检</font>** | **<font style="color:white;">确认</font>** | **<font style="color:white;">备注</font>** |
| :---: | :---: | :---: | --- | :---: | :---: | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">登录</font> | <font style="color:black;">1</font> | <font style="color:black;">版本号显示</font> | <font style="color:black;">1</font> | <font style="color:black;">0</font> | |
| <font style="color:black;">2</font> | | <font style="color:black;">1</font> | <font style="color:black;">清除缓存</font> | <font style="color:black;">1</font> | <font style="color:black;">1</font> | |
| <font style="color:black;">3</font> | | <font style="color:black;">1</font> | <font style="color:black;">通过配置修改登录闪屏</font> | | | |
| <font style="color:black;">4</font> | | <font style="color:black;">1</font> | <font style="color:black;">通过配置修改登录logo</font> | | | |
| <font style="color:black;">5</font> | | <font style="color:black;">2</font> | <font style="color:black;">通过服务器更改客户端配置版本和语言版本</font> | | | |
| <font style="color:black;">6</font> | | <font style="color:black;">3</font> | <font style="color:black;">区服选择</font> | | | |
| <font style="color:black;">7</font> | | <font style="color:black;">4</font> | <font style="color:black;">区服选择列表推荐服务器</font> | | | |
| <font style="color:black;">8</font> | | <font style="color:black;">1</font> | <font style="color:black;">区服选择列表最后登录服务器</font> | | | |
| <font style="color:black;">9</font> | | <font style="color:black;">1</font> | <font style="color:black;">服务器状态：新、火爆、爆满、维护、排队</font> | | | |
| <font style="color:black;">10</font> | | <font style="color:black;">1</font> | <font style="color:black;">登录排队</font> | | | |
| <font style="color:black;">11</font> | | <font style="color:black;">1</font> | <font style="color:black;">推荐服务器</font> | | | |
| <font style="color:black;">12</font> | | <font style="color:black;">1</font> | <font style="color:black;">记住最后登录的服务器</font> | | | |
| <font style="color:black;">13</font> | | <font style="color:black;">1</font> | <font style="color:black;">清除缓存</font> | | | |
| <font style="color:black;">14</font> | | <font style="color:black;">1</font> | <font style="color:black;">登录重连</font> | | | |
| <font style="color:black;">15</font> | | <font style="color:black;">1</font> | <font style="color:black;">版本号显示</font> | | | |
| <font style="color:black;">16</font> | | <font style="color:black;">1</font> | <font style="color:black;">选择登录服务器流程</font> | | | |
| <font style="color:black;">17</font> | <font style="color:black;">注册</font> | <font style="color:black;">1</font> | <font style="color:black;">账号注册流程</font> | | | |
| <font style="color:black;">18</font> | <font style="color:black;">排队</font> | <font style="color:black;">1</font> | <font style="color:black;">排队登录流程</font> | | | |
| <font style="color:black;">19</font> | | <font style="color:black;">1</font> | <font style="color:black;">动态更新排队人数</font> | | | |
| <font style="color:black;">20</font> | | <font style="color:black;">1</font> | <font style="color:black;">排队可进入游戏后直接进入游戏</font> | | | |
| <font style="color:black;">21</font> | <font style="color:black;">更新</font> | <font style="color:black;">1</font> | <font style="color:black;">没有提示框的补丁更新流程</font> | | | |
| <font style="color:black;">22</font> | | <font style="color:black;">1</font> | <font style="color:black;">有提示框的补丁更新流程</font> | | | |
| <font style="color:black;">23</font> | | <font style="color:black;">1</font> | <font style="color:black;">强制更新流程</font> | | | |
| <font style="color:black;">24</font> | <font style="color:black;">公告</font> | <font style="color:black;">1</font> | <font style="color:black;">维护公告</font> | | | |
| <font style="color:black;">25</font> | | <font style="color:black;">1</font> | <font style="color:black;">登录公告</font> | | | |
| <font style="color:black;">26</font> | | <font style="color:black;">1</font> | <font style="color:black;">强更公告</font> | | | |
| <font style="color:black;">27</font> | | <font style="color:black;">1</font> | <font style="color:black;">每次开启公告界面刷新公告状态</font> | | | |
| <font style="color:black;">28</font> | <font style="color:black;">更新</font> | <font style="color:black;">1</font> | <font style="color:black;">没有提示框的补丁更新流程</font> | | | |
| <font style="color:black;">29</font> | | <font style="color:black;">1</font> | <font style="color:black;">有提示框的补丁更新流程</font> | | | |
| <font style="color:black;">30</font> | | <font style="color:black;">1</font> | <font style="color:black;">强制更新流程</font> | | | |
| <font style="color:black;">31</font> | <font style="color:red;">数据统计</font> | <font style="color:black;">1</font> | <font style="color:black;">实现数据统计需求</font> | | | |
| <font style="color:black;">32</font> | | <font style="color:black;">1</font> | <font style="color:black;">实现GM指令需求</font> | | | |


# 六、美术需求清单（必备）
### 6.1 2D需求
| **<font style="color:white;">编号</font>** | **<font style="color:white;">分类</font>** | **<font style="color:white;">名称</font>** | **<font style="color:white;">大小-美术</font>** | **<font style="color:white;">资源命名</font>** | | **<font style="color:white;">美术需求</font>** | | | **<font style="color:white;">特殊说明（逻辑说明）-预留</font>** | **<font style="color:white;">参考</font>** | **<font style="color:white;">成品缩略图</font>** |
| :---: | :---: | :---: | :---: | :---: | --- | --- | --- | --- | :---: | :---: | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">装备</font> | <font style="color:black;">木槌</font> | | <font style="color:black;">Icon_xxx_xxx_01</font> | | <font style="color:black;">装备图标，残破的木锤</font> | | | | | |
| <font style="color:black;">2</font> | <font style="color:black;">功能图标</font> | <font style="color:black;">背包</font> | | <font style="color:black;">Icon_xxx_xxx_01</font> | | <font style="color:black;">主界面功能图标，皮质背包</font> | | | | | |


### 6.2 界面需求
<font style="color:#DF2A3F;">界面性质包括：一级界面、二级界面、弹框界面、弹出框</font>

| **<font style="color:white;">编号</font>** | **<font style="color:white;">界面</font>** | | | **<font style="color:white;">性质</font>** |
| :---: | --- | --- | --- | :---: |
| <font style="color:black;">1</font> | <font style="color:black;">更新界面</font> | | | <font style="color:black;">二级界面</font> |
| <font style="color:black;">2</font> | <font style="color:black;">登录界面</font> | | | <font style="color:black;">一级界面</font> |
| <font style="color:black;">3</font> | <font style="color:black;">开发登录界面</font> | | | <font style="color:black;">弹出框</font> |
| <font style="color:black;">4</font> | <font style="color:black;">区服登录界面</font> | | | <font style="color:black;">弹出框</font> |
| <font style="color:black;">5</font> | <font style="color:black;">选服界面</font> | | | <font style="color:black;">二级界面</font> |
| <font style="color:black;">6</font> | <font style="color:black;">公告界面</font> | | | <font style="color:black;">弹框界面</font> |


### 6.3 动效需求
使用通用的界面动效需求表格

备注此系统需求的动效ID

参考项目：

[https://snh48group.yuque.com/mwyfd0/nx3vv2/ahfzy98v2wyz3mp4](https://snh48group.yuque.com/mwyfd0/nx3vv2/ahfzy98v2wyz3mp4)

### 6.4 场景需求
| **<font style="color:white;">所属系统</font>** | **<font style="color:white;">场景类型</font>** | **<font style="color:white;">场景名称-美术</font>** | | **<font style="color:white;">场景用途</font>** | | | **<font style="color:white;">描述</font>** |
| :---: | :---: | :---: | --- | :---: | --- | --- | :---: |
| <font style="color:black;">公会</font> | <font style="color:black;">战斗地图</font> | | | <font style="color:black;">公会战专属战斗地图</font> | | | <font style="color:black;">四周需要能体现多人战争气氛</font> |
| <font style="color:black;">公会</font> | <font style="color:black;">UI地图</font> | | | <font style="color:black;">公会大厅展示用地图</font> | | | <font style="color:black;">详见参考文档</font> |
| <font style="color:black;">主城</font> | <font style="color:black;">展示地图</font> | | | <font style="color:black;">精灵展示用UI场景</font> | | | <font style="color:black;">详见参考文档</font> |


### 6.5 动态物件需求
| **<font style="color:white;">分类</font>** | **<font style="color:white;">名称</font>** | **<font style="color:white;">用途</font>** | **<font style="color:white;">美术需求</font>** | | | **<font style="color:white;">动作需求</font>** | | **<font style="color:white;">动作名</font>** | **<font style="color:white;">特效需求</font>** |
| :---: | :---: | :---: | :---: | --- | --- | :---: | --- | :---: | :---: |
| <font style="color:black;">场景物件</font> | <font style="color:black;">Box</font> | <font style="color:black;">副本宝箱</font> | <font style="color:black;">探索地图上出现的随机低阶宝箱</font> | | | <font style="color:black;">宝箱出现动画</font> | | <font style="color:black;">@Ani_Box_Idle_01</font> | <font style="color:black;">宝箱出现动画</font> |
| | | | | | | <font style="color:black;">宝箱打开动画</font> | | <font style="color:black;">@Ani_Box_Atk_01</font> | <font style="color:black;">宝箱打开动画</font> |


# 六、string需求
| **<font style="color:white;">类别</font>** | **<font style="color:white;">目标操作</font>** | **<font style="color:white;">触发条件</font>** | **<font style="color:white;">提示形式</font>** | **<font style="color:white;">提示string</font>** | **<font style="color:white;">stringID</font>** |
| :---: | --- | --- | :---: | --- | :---: |
| <font style="color:black;">登录流程</font> | <font style="color:black;">启动游戏</font> | <font style="color:black;">加载服务器列表失败</font> | <font style="color:black;">确定框</font> | <font style="color:black;">加载服务器列表失败，请重新尝试!</font> | |
| | <font style="color:black;">启动游戏</font> | <font style="color:black;">客户端版本不匹配</font> | <font style="color:black;">确定框</font> | <font style="color:black;">客户端版本号不匹配!</font> | |
| | <font style="color:black;">启动游戏</font> | <font style="color:black;">找不到渠道号</font> | <font style="color:black;">确定框</font> | <font style="color:black;">找不到渠道号!</font> | |
| | <font style="color:black;">启动游戏</font> | <font style="color:black;">加载配置文件失败</font> | <font style="color:black;">确定框</font> | <font style="color:black;">加载文件失败,请尝试重新下载客户端!</font> | |
| | <font style="color:black;">游戏更新</font> | <font style="color:black;">无法更新</font> | <font style="color:black;">确定取消框</font> | <font style="color:black;">不能更新资源，是否重试?</font> | |
| | <font style="color:black;">登录服务器</font> | <font style="color:black;">服务器爆满</font> | <font style="color:black;">确定框</font> | <font style="color:black;">当前服务器爆满！请选择其它服务器。</font> | |
| | <font style="color:black;">登录服务器</font> | <font style="color:black;">服务器维护中</font> | <font style="color:black;">确定框</font> | <font style="color:black;">该服务器紧急维护中，请稍后尝试登录！</font> | |
| <font style="color:black;">更新流程</font> | <font style="color:black;">x</font> | <font style="color:black;">连接服务器</font> | <font style="color:black;">文字</font> | <font style="color:black;">连接服务器中…</font> | |
| | <font style="color:black;">x</font> | <font style="color:black;">获取更新信息</font> | <font style="color:black;">文字</font> | <font style="color:black;">获取更新信息中…</font> | |
| | <font style="color:black;">x</font> | <font style="color:black;">大于1m的补丁更新，显示进度条</font> | <font style="color:black;">文字</font> | <font style="color:black;">更新配置文件（xxMB/xxMB）</font> | |
| | <font style="color:black;">x</font> | <font style="color:black;">更新后的配置加载，显示进度条</font> | <font style="color:black;">文字</font> | <font style="color:black;">资源加载中…</font> | |
| <font style="color:black;">公告流程</font> | <font style="color:black;">点击公告按钮</font> | <font style="color:black;">没有配置公告内容</font> | <font style="color:black;">错误文字</font> | <font style="color:black;">当前没有公告内容。</font> | |
| <font style="color:black;">特殊流程</font> | <font style="color:black;">点击登录</font> | <font style="color:black;">当前选择登录服务器关闭</font> | <font style="color:black;">确定框</font> | <font style="color:black;">当前登录服务器没有开启，请选择其它的登录服务器。</font> | |




> 原始Excel文档
>
> [系统策划案范例.xlsx](https://snh48group.yuque.com/attachments/yuque/0/2024/xlsx/45413786/1718871528257-c383a2be-d062-4b45-b51c-48899a2ddb36.xlsx)
>

