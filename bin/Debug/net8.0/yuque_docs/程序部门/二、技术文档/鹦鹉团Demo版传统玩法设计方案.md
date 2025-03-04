# 一、简介
鹦鹉团Demo版本传统玩法类似于劲舞团，在选择完音乐、角色、场景、玩法等内容后，进入游玩场景，在特定时间显示若干方向键，玩家需要在规定时间内连续按下正确的按键，并在特定时间按下击打键，根据按下的时机计算分数，最终根据总分进行结算。



# 二、结构设计
内容：

:::info
1. 新增Page_DanceMain_Ctrl.lua脚本，用来做跳舞玩法入口的跳转管理。
2. 新增Page_DancePlay_Ctrl.lua脚本，用来做跳舞玩法的游玩流程的跳转管理
3. 新增DanceMgr.lua脚本，用来存储全局信息及实现全局方法。
4. 新增DanceDefine.lua脚本，用来定义相关数据类型。
5. 新增DanceActor.lua脚本用来控制舞蹈场景内角色。
6. 新增DanceCamera.lua脚本来控制舞蹈场景内镜头。
7. 在ResRegistry.lua中新增处理音乐加载的相关方法：PreloadMusic(musicId)、LoadMusic(musicId)、ReleaseMusic(musicId)。
8. 新增界面相关脚本：
    1. 主界面：UI_DanceMain_Ctrl.lua和UI_DanceMain_Panel.lua。
    2. 配置选择界面：UI_DanceSelect_Ctrl.lua和UI_DanceSelect_Panel.lua。
    3. 歌曲信息界面：UI_DanceMusicShow_Ctrl.lua和UI_DanceMusicShow_Panel.lua。
    4. 传统玩法界面：UI_DanceKeyPressMode_Ctrl.lua和UI_DanceKeyPressMode_Panel.lua。
    5. 结算界面：UI_DanceResult_Ctrl.lua和UI_DanceResult_Panel.lua。
9. Demo阶段先实现传统玩法，后续会加入下落式等不同玩法，需要在设计架构的时候考虑。所以新增DanceModeBase，作为玩法的基类，新增KeyPressDanceMode，对应传统玩法，后续新增玩法时需要继承DanceModeBase。

:::





# 三、详细结构设计
内容：

:::info
1. 涉及到的需要加载的配置文件及数据解析：
    1. Music.xlsm表，用来存储音乐相关信息，如歌名、创作者、下载地址、谱面数据、舞步数据等。结构如下图：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719890038592-baa9a990-cde2-4796-95c0-8b4df7119e48.png)加载后转换为本地数据类型MusicData![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719828287012-b0db3695-8457-462b-87b6-90b29da0320a.png)
    2. 每首歌的舞步数据，需要将版署版的lua文件转为json，每个json文件对应一首歌，命名规则为歌曲ID+"_D.json"。数据结构如下![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719889739838-607a07a1-05ea-4d95-9a37-d3d3e676cdbe.png)

其中每一条数据结构为{Id，关键秒，事件类型，{参数列表}}，不同类型对应的不同参数列表。

后续需要支持AI工具生成的舞步数据，这里需要预留接口。

    3. 事件类型及需要的参数如下图：：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719830309724-cdba72c1-e14d-45ae-996c-642cdfb1ffd2.png)
    4. 每首歌的歌曲数据，采用如下结构的json文件：      ![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719889879115-09569321-0762-426a-8dc8-91ba4f438810.png)  ，存储文件名为歌曲ID+"_M.json"。
    5. 每首歌的谱面数据，采用如下结构的json文件：                      ![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719889922070-74eb21f2-8d52-4bc1-82c9-c6c2228f4720.png)          主要的信息为每个关键帧及其对应所需要生成的键位数量，同样为每首歌一个文件，以歌曲ID+"_B.json"命名。解析并存入MusicData中。
2. 类的依赖关系及重要方法

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719897929329-9e1a9564-4294-4364-b053-9f3ccefb958a.png)

:::



# 四、流程设计


1. 整个Demo版本流程图如下：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719814456690-4988df03-86c0-4d1b-b1ce-5c4da0164d25.png)
2. 资源的加载策略：
    1. 场景资源：进入游玩阶段后，根据玩家的选择进行加载，由于占用内存较大，不缓存，在加载界面加载。
    2. 角色资源：玩家在主界面进行角色切换时进行加载，Demo版本只有一套模型，在游戏开始前预加载
    3. 角色舞步及镜头动画：沿用版署版的数据结构，单个舞蹈存为一个json文件，包含一个关键帧列表，每个关键帧数据包括帧数、事件类型及每个类型所需的参数。
    4. 歌曲、谱面、舞步：先检查本地是否有资源，没有的话进行下载，下载后保存到本地。通过DanceMgr进行加载及释放。
3. 传统玩法的游玩流程如下：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1719891590332-a08515c4-423b-4ab0-83b5-3ea5be265560.png)
4. 传统玩法的界面信息：

![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689586446-0c654340-1df3-4af8-8452-0240aa2a7994.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_834%2Climit_0)

5. 判定点的显示时机：
    1. 根据谱面数据第一条的时间点，计算出对应的小节，再提前一个小节显示判定点。
6. 判定点每一小节滚动一个周期。
7. 谱面轨道上方的难度=当前显示的按键的数量。
8. 判定点为每小节第三拍，根据时间偏移值（偏移值=Abs(按下时间-配置时间)）判定结果（遗憾、很棒、优秀、完美），具体的偏移数据读配置。需要加个Debug界面，可以在游戏中动态进行调整。
9. 按键生成：
    1. PC端从四个方向键中随机，手机端从三个方向键中随机。
    2. 谱面配置中会给出每个时间点需要生成的按键数量，按照生成规则生成对应的按键。
    3. 拇指模式和三指模式：
        1. ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689587912-927618da-f104-499c-b4f5-b11a4fba73a0.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_240%2Climit_0)		![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689589935-a0beef64-48ec-4c59-905a-9d1cc5ceb948.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_237%2Climit_0)
        2. 区别在于最后一个按键是向下还是向右。
        3. 仅限于手机端，PC端四个键都显示
    4. 随机生成规则：
        1. 每相邻两个小节出现的第一个键不能是同方向。
        2. 四个及以上按键，不能出现连续四个同方向按键。
        3. 六个及以上按键，不能出现相邻按键方向都不相同的情况。
10. 击打键有两个音效，分别在击打成功和击打失败时播放
11. 谱面轨道中的按键需要有两个状态，已按下和未按下



# 五、提出问题
暂无



