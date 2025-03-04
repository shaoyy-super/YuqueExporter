

| 时间 | 说明 |
| --- | --- |
| 2024/12/10 17:24 | 完成轨道与片段的文档初版编写 |
| 2024/12/10 20:21 | 更新:创建轨道最左标签快与创建片段的颜色块 |
| 2024/12/11 19:30 | 更新:演出UI预制体相关 |
| 2024/12/11 19:30 | 修改:数值片段的颜色相关设定 |
| 2024/12/12 10:51 | 修改:动画片段0帧片段的显示调整   |
| 2024/12/12 17:21 | 修改:数值片段的色彩区分 |


## 1.创建UI
在使用演出UI跳字轨道之前我们需要创建演出UI，演出UI通过轨道创建

![右键空白区域展开的菜单](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733796392191-3930b422-8775-4d19-94e3-0be989248bd2.png)

**操作步骤：**

1._在Timeline窗口空白区域右键打开菜单_

2._菜单中选择_<u>Game.Timeline</u>_展开二级菜单_

3._二级菜单中选择_<u>CreateSettlement-创建小结算UI</u>



![演出UI创建轨道设置正确](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733833368512-7931d5c7-b0d9-4787-9282-07824d00e670.png)

![演出UI轨道设置错误](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733833394674-e5910a1d-cb4a-44ba-90d8-e4885ab5774b.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">最左的红绿标签色块：</font>_轨道是否设置正确的颜色标签，绿色为正常，红色为设置错误_
+ <font style="background-color:#FBDFEF;">Create Settlement Track Asset：</font>_轨道名字，选中可随意修改_



至此创建<font style="background-color:#DF2A3F;">【演出UI创建轨道】</font>成功，但光有轨道并不能在运行的过程中生成出UI来，接下来通过创建<font style="background-color:#ECAA04;">【演出UI创建片段】</font>，我们可以在运行的过程中生成出UI



![右键【演出UI创建轨道】展开的菜单](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733796504176-dc858499-fe64-4b16-8a95-b1ac1d9f4436.png)

**操作步骤：**

1._在Timeline窗口右键_<font style="background-color:#DF2A3F;">【演出UI创建轨道】</font>_打开菜单_

2._选择_<u>Add Create Settlement Clip Asset</u>_在_<font style="background-color:#DF2A3F;">【演出UI创建轨道】</font>_上创建_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>

3._选中创建出的_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>_在Inspector面板中将UI预制体拖入Prefab栏_

__

![演出UI创建片段](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733833638400-fa02646f-0c54-4a48-85cf-e8c409638c59.png)  
![选中【演出UI创建片段】后Inspector面板展示的信息](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733796457641-cf6c2120-b467-4c21-990a-208c4e64a6e0.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">创建片段底部红绿颜色条：</font>_片段是否设置正确(拖入了预制体)的颜色标签，绿色为正常，红色为设置错误或未设置_



**注意事项：**

+ _在运行的过程中，当时间线进入_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>_时，拖入_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>_的预制体会被生成创建出来，当时间线离开_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>_时创建出来的UI会被隐藏，当这个轨道所在的Timeline播放完毕时UI会被销毁。_
+ _一般在我们Boss的组装演出中，或是其他演出，这个_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>_会是最长的，这是因为当时间线离开_<font style="background-color:#ECAA04;">【演出UI创建片段】</font>_时创建出来的UI会被隐藏，那么其他操作这个UI的轨道的操作结果也就显示不出来了，所以在组装演出中，这个轨道会被放在全局Timeline，并且是从头占到尾的最长的片段。_



通过轨道运行时创建UI的流程完毕，接下来进行详细讲解

## 2.操作UI
我们在运行时播放Timeline的过程中，生成了演出的UI，但是光生成和销毁并不够，我们可能需要对这个UI进行一些操作，比如说控制运动，更改数字图片等，接下来讲解如何通过Timeline的轨道片段操作这个创建出来的UI

![右键空白区域展开的菜单](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733797822115-7d28f8f0-ac3d-45fe-a511-c1f283e50412.png)

**操作步骤：**

1._在Timeline窗口空白区域右键打开菜单_

2._菜单中选择_<u>Game.Timeline</u>_展开二级菜单_

3._二级菜单中选择_<u>FishSettle-Boss结算动画</u>



![演出UI操作轨道](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733797881037-f3eedc69-23ed-4229-a371-248d20ce7573.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">最左的黄色标签：</font>_为轨道的固定颜色标签，一般情况下不会变成其他颜色，后续可能会为其添加功能_
+ <font style="background-color:#FBDFEF;">[操作节点]Element_0：</font>_轨道名字，不可随意修改，会有以下几种显示情况：_
    - <font style="background-color:#E6DCF9;">[错误]没找到预制体：</font>_该Boss演出Timeline还没有建立对应的BossUI预制体，一般是因为没有在目录_<u>Assets/Art/ModuleRes/SettlementUI</u>_下找到关联的BossUI预制体的文件夹和预制体。_
    - <font style="background-color:#E6DCF9;">[错误]预制体上没挂UIFishSettlement脚本：</font>_该演出UI预制体根节点上没有挂载_<u>UIFishSettlement</u>_脚本。_
    - <font style="background-color:#E6DCF9;">[错误]UIFishSettlement组件没有拖入控制节点：</font>_该演出UI预制体根节点脚本上没有拖入任何可控制的节点。_
    - <font style="background-color:#E6DCF9;">[错误]UIFishSettlement组件找不到节点：</font>_一般是因为删除过脚本上拖入的节点，导致找不到之前的节点。_
    - <font style="background-color:#E6DCF9;">[操作节点]Element_0：</font>_固定显示_<u>[操作节点]</u>_，_<u>Element_0</u>_则是会动态的显示这个轨道会操作修改的对应UI预制体根节点脚本上的节点名_



![选择轨道上所有片段要操作的节点](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733810824319-1f9e552e-3161-4c94-9f37-8a9fd51bfd7b.png)![UI预制体根节点脚本上要拖入期望控制的节点](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733810798957-883748bd-0a15-4787-a58a-b5daf41deb1b.png)

当鼠标选中轨道时在Inspector面板中可以选择这个<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>上所有<font style="background-color:#ECAA04;">【演出UI操作片段】</font>所要操作的节点，这个下拉列表和脚本中所有节点列表一一对应。



至此创建<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>成功，但同上光是有轨道是没有用的，并不能起到运行过程中对UI进行操作更改的目的，接下来通过创建三种不同的<font style="background-color:#ECAA04;">【演出UI操作片段】</font>，我们就可以预设一些修改操作，在时间线进入这些片段时对演出UI进行修改操作。

### 2.1.动画片段
![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733810569772-306abf67-487d-422e-8a16-7203fa4f829d.png)

**操作步骤：**

1._在Timeline窗口右键_<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>_打开菜单_

2._选择_<u>Add 动画片段</u>_在_<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>_上创建_<font style="background-color:#ECAA04;">【演出UI动画片段】</font>

__

![动画片段没设置动画状态](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733811928311-896a83be-1091-472b-9c13-0d62e6878f0b.png)![动画片段正确的设置了动画状态](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733811698415-65716cd6-504c-4ec7-aae9-bd6af40296ff.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">动画片段橙色背景：</font>_为片段的固定颜色背景，用以显眼的区分不同的片段。_
+ <font style="background-color:#FBDFEF;">动画片段底部红绿颜色条：</font>_为片段是否正确的设置的快速判断辨别区，正确的设置会显示绿色，不正确的设置会显示红色。_
+ <font style="background-color:#FBDFEF;">动画片段中间文字：</font>_动画片段的信息展示，有以下几种情况：_
    - <font style="background-color:#E6DCF9;">[错误]没设置动画状态：</font>_没有设置动画状态。_
    - <font style="background-color:#E6DCF9;">[错误]节点没有Animator组件：</font>_该轨道操作的节点没有挂载_<u>Animator</u>_组件。_
    - <font style="background-color:#E6DCF9;">[错误]未找到状态name：</font>_该动画片段设置的动画状态名，在对应的操作节点Animator的状态机内没有找到同名状态。_
    - <font style="background-color:#E6DCF9;">[错误]状态中没有clip动画：</font>_该动画片段设置的动画状态，在对应的操作节点Animator的状态机内的状态中，没有设置_<u>clip动画</u>_。_
    - <font style="background-color:#E6DCF9;">[动画]name：</font>_固定显示_<u>[动画]</u>_，_<u>name</u>_则是会动态的显示这个动画片段会播放的动画状态名。_



![动画片段的Inspector面板](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733810626964-8d152fbb-0fa3-4b34-a4ea-e07059113447.png)![某个Boss演出UI节点下的动画状态](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733810636353-76d8360f-bc2e-43c3-94a5-e9973d10acf1.png)![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733813804313-79e279ff-7221-4355-8001-cb180bd12f20.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">是否只播一次：</font>_为设置动画状态是否在整个组装演出中只会播放一次的设置，因为可能会遇到这个Timeline多次播放，但这个动画只需要播放一次的需求。_
+ <font style="background-color:#FBDFEF;">选择动画：</font>_这里会有几种不同的显示：_
    - <font style="background-color:#E6DCF9;">[错误]没有挂Animator组件：</font>_一般是因为轨道操作节点上没有挂载_<u>Animator</u>_组件_<u></u>_。_
    - <font style="background-color:#E6DCF9;">[错误]Animator里面没有设置状态机：</font>_一般是因为轨道操作节点的_<u>Animator</u>_组件上没有设置_<u>动画状态机</u>_。_
    - <font style="background-color:#E6DCF9;">[错误]Animator的动画状态机中没有动画状态：</font>_一般是因为轨道操作节点的_<u>Animator</u>_组件上的_<u>动画状态机</u>_中没有创建状态，所以不显示下拉列表。_
    - <font style="background-color:#E6DCF9;">选择动画：</font>_下拉列表中会显示轨道操作的节点上的_<u>Animator</u>_组件所设置的_<u>动画状态机</u>_中的所有动画状态。_



### 2.2.数值片段
![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733814137543-e2acf404-a555-4f9d-beca-fdde9c8c77fe.png)**操作步骤：**

1._在Timeline窗口右键_<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>_打开菜单_

2._选择_<u>Add 动画片段</u>_在_<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>_上创建_<font style="background-color:#ECAA04;">【演出UI数值片段】</font>

<font style="background-color:#ECAA04;"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733917592855-887426ca-52de-4079-b7ee-bca25c82f683.png)![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733917580892-a2d549d3-3a3f-4148-8d41-aaa0dd1a7687.png)![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1734081810958-c72fad2e-9bc6-4d87-938c-0985adbd16c7.png)**元素讲解：**

+ <font style="background-color:#FBDFEF;">动画片段橙蓝绿背景：</font>_根据片段数值是否带有过渡数值动画_
    - <font style="background-color:#E6DCF9;">绿色背景：</font>_不带数值过渡动画___
    - <font style="background-color:#E6DCF9;">蓝绿渐变背景：</font>_带数值过渡动画的飘字___
    - <font style="background-color:#E6DCF9;">橙绿渐变背景：</font>_带数值过渡动画的总分_
+ <font style="background-color:#FBDFEF;">动画片段底部红绿颜色条：</font>_为片段所在的轨道控制的节点中是否存在可以_<u>设置数值的组件</u>_的快速判断辨别区，绿色为存在，红色为不存在。_
+ <font style="background-color:#FBDFEF;">动画片段中间文字：</font>_动画片段的信息展示，会将片段的信息组合文字展示出来。_



![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733814972034-ecefcc80-243b-4890-b099-68bf051296b5.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">显示数值的百分比过渡：</font>_为设置数值片段的数值显示占比区间，要显示的数字会乘以这个百分比，进行最终的显示，这个进度条是最小值最大值进度条，如果最小值和最大值一样，那么就是没有数值过渡动画，如果最小值和最大值不一样，那么就会有过渡动画，是从最小值过渡到最大值。_
+ <font style="background-color:#FBDFEF;">数值类型：</font>_这里会有几种不同的显示：_
    - <font style="background-color:#E6DCF9;">下方总分：</font>_一般演出UI的形式会比较固定，下方有一个合计得分，这个总分是根据之前的步骤累加的总分，这个分数的来源会配在表里。_
    - <font style="background-color:#E6DCF9;">飘字分数：</font>_一般是表里配的一个固定数值，比如+300，x50这样。_
    - <font style="background-color:#E6DCF9;">特殊数值：</font>_目前还没有功能。_

### 2.3.位移片段
![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733817991313-4e2291df-e95d-40a6-b9b4-c47c776163c7.png)

**操作步骤：**

1._在Timeline窗口右键_<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>_打开菜单_

2._选择_<u>Add 自适应位移片段</u>_在_<font style="background-color:#DF2A3F;">【演出UI操作轨道】</font>_上创建_<font style="background-color:#ECAA04;">【演出UI位移片段】</font>

<font style="background-color:#ECAA04;"></font>

![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733821701684-0abdf067-b0be-4d5f-9dac-31241842f4b5.png)**元素讲解：**

+ <font style="background-color:#FBDFEF;">动画片段曲线背景：</font>_为片段设置的移动曲线。_
+ <font style="background-color:#FBDFEF;">动画片段中间文字：</font>_显示的是该片段移动的最终目标名。_



![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733821837332-51c47321-1667-474a-9219-e1a95673c1bc.png)

![UI预制体根节点脚本上要拖入期望控制的节点](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733810798957-883748bd-0a15-4787-a58a-b5daf41deb1b.png)

**元素讲解：**

+ <font style="background-color:#FBDFEF;">移动曲线：</font>_为设置轨道节点移动到目标节点位置时的速度曲线。_
+ <font style="background-color:#FBDFEF;">移动到目标：</font>_这里会有几种不同的显示：_
    - <font style="background-color:#E6DCF9;">[错误]没找到预制体：</font>_该Boss演出Timeline还没有建立对应的BossUI预制体，一般是因为没有在目录_<u>Assets/Art/ModuleRes/SettlementUI</u>_下找到关联的BossUI预制体的文件夹和预制体。_
    - <font style="background-color:#E6DCF9;">[错误]预制体上没挂UIFishSettlement脚本：</font>_该演出UI预制体根节点上没有挂载_<u>UIFishSettlement</u>_脚本。_
    - <font style="background-color:#E6DCF9;">[错误]UIFishSettlement组件没有拖入控制节点：</font>_该演出UI预制体根节点脚本上没有拖入任何可控制的节点。_
    - <font style="background-color:#E6DCF9;">[错误]UIFishSettlement组件找不到节点：</font>_一般是因为删除过脚本上拖入的节点，导致找不到之前的节点。_
    - <font style="background-color:#E6DCF9;">下拉列表：</font>_显示的是对应的演出UI根节点UIFishSettlement脚本上的所有节点列表_

**注意事项：**

+ 在使用过程中如果遇到某些面板显示错误，先选择其他gameobject再选择要修改的Timeline对应的gameobject用以手动刷新面板。

## 3.UI预制体


![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733835479547-1f01cd55-5f3e-4303-9a40-e6f44dcba021.png)

_Boss组装演出的UI预制体统一在_<u>Assets/Art/ModuleRes/SettlementUI</u>_目录下，规范为：_

+ <font style="background-color:#FBDFEF;">文件夹名：</font>Boss_**BossID**
+ <font style="background-color:#FBDFEF;">预制体名：</font>UI_Settlement_**BossID**

预制体用到的<u>动画状态机</u>与<u>动画片段</u>统一放在对应的目录下



_Boss组装演出的_<u>UI预制体</u>_由_<u>公用预制体</u>_**组装**__而成_，_**UI**__需要改样式只需要改_<u>公用预制体</u>_就可以了，_<u>公用预制体</u>_放在同目录的_**Common**_文件夹下_

![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733911709117-afe62f40-e74a-4c90-a665-a07363ae7483.png)

_目前分别有以下几种：_

+ <font style="background-color:#FBDFEF;">Character：</font>_头像背景图，类似于李小龙的小半身ICon，这个图一般在小结算也会用到_
+ <font style="background-color:#FBDFEF;">Combo：</font>_连击数，一个连击的图片字，和一个数字_
+ <font style="background-color:#FBDFEF;">FishCoin：</font>_金币数，带一个背景图做底板，一般是框_
+ <font style="background-color:#FBDFEF;">FishRate：</font>_鱼倍数，带一个背景图做底板，一般是框_
+ <font style="background-color:#FBDFEF;">Number：</font>_次数，带一个背景图做底板，一般是框_
+ <font style="background-color:#FBDFEF;">Rate：</font>_系数（翻倍数），带一个背景图做底板，一般是框_

不同后缀的定义：

+ <font style="background-color:#FBDFEF;">ElementCommon：</font>_一般用在下方总分，大部分会带一个背景图做为底板，一般是框_
+ <font style="background-color:#FBDFEF;">FloatCommon：</font>_一般用在跳字飘字，基本大部分都是纯数字，不带其他图，会覆盖基础数字预制体的一些细节参数，比如数字的前缀+、x_
+ <font style="background-color:#FBDFEF;">Num：</font>_基础数字预制体，下方总分或者飘字的数字都会用这个预制体，一般不会去用，主要是用来留作做统一修改的基础_



![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733912334288-49d7930e-0f7d-4564-ba07-627c6f232a68.png)

_一般实际Boss演出时用到的UI预制体会由多个_<u>公用预制体</u>_**组装**__而成_



![](https://cdn.nlark.com/yuque/0/2024/png/1580229/1733914997698-fb83ae79-e084-42f1-86c6-6391573ab120.png)

一般情况下：

+ _由_**程序**_控制像_**UI_FishRate_ElementCommon**_这类根层级_
+ _由_**特效**_控制像_**Node、BGNode、UI_FishRate_Num**_这类没有设置图片或文字组件的空层级_
+ _由_**UI**_控制_**FishRate_BG、NumberConvertImage**_这类设置了图片或文字之类的层级，UI统一通过这些层级修改显示样式_

