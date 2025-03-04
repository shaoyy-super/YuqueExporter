# 一、简介
游玩界面：

![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689870911-e19a068c-b987-4148-af8d-d6ac0e155f99.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_852%2Climit_0)

流星玩法在界面下方会显示四个按键区域。

开始播放舞蹈和音乐后，按键会从屏幕上方飘向判定区域，当按键正好飘到判定区域时，点击对应按键得分。

按键一共有四种类型：

    1. 单音符：![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689877796-097e2bbd-1de2-4256-b307-122858f837cd.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_138%2Climit_0) 一个按键图标，只需点击即可。
    2. 滑动音符：![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689889846-9e4569c3-fab5-48c4-b700-02e1233eca1e.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_256%2Climit_0)按住按键之后滑向指定方向，只要滑出当前的判定框就判定成功。
    3. 多短音符：![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689886553-a4350351-4b4c-454e-95e5-f42aa84023eb.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_480%2Climit_0)，多个单音符同时下落，中间以线连接，需要同时点击多个音符。
    4. 长音符：![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689881062-0386a5e5-ac79-47b8-a3bd-488c45508e53.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_147%2Climit_0)![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689882912-0aa597f8-db01-47e9-8ea7-54d2efa2d08a.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_222%2Climit_0)多个相连的按键图标，在第一个按键按下，之后按照轨道进行长按、滑动。





# 二、结构设计
内容：

:::info
1. **新增流程控制脚本**
    1. MeteorDanceMode.lua，用来控制整个流星玩法的流程。
2. **拆分UI_DanceKeyPressMode**
    1. 因为游玩阶段的UI有一部分和传统模式的可以公用，所以将原本传统模式的游玩UI：UI_DanceKeyPressMode(_Ctrl|_Panel)拆分成通用的UI_DancePlay(_Ctrl|_Panel)和UI_DanceKeyPressMode(_Ctrl|_Panel)，前者作为通用UI，后者为传统模式的独有UI。
3. **新增界面相关脚本**
    1. UI_DanceMeteorMode_Ctrl.lua和UI_DanceMeteorMode_Panel.lua，作为流星玩法的游玩UI。
4. **新增VM脚本**
    1. DanceMeteorVM.lua，用来存储流星玩法游玩过程中的数据，并提供获取数据的接口
5. **拆分DanceMgr.lua中数据部分**
    1. DanceVM.lua:存储跳舞玩法对局外数据，并提供获取数据的接口
    2. DanceKeyPressVM.lua:存储传统模式游玩过程中的数据，并提供获取数据的接口

:::



# 三、详细结构设计
内容：

:::info
1. **谱面文件命名规则**

谱面配置为了与传统模式的谱面配置进行区分，传统模式的谱面命名规则改为歌曲ID+"_KD.json"，流星玩法的谱面命名规则为歌曲ID+"_MD.json"

2. **流星玩法谱面结构**

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722252345720-04439285-6a5f-40fc-b498-2a03f68f96ca.png)

    1.  id：从1开始依次递增，不可重复
    2. time：按键按下时机，按键出现的时机为time-800毫秒（800毫秒走配置）。
    3. type：按键类型，需要结合下面的"key"、"parameter"判断，对应类型为：
        1. 1：单点，key为对应的按键判定区域（1-4），偏差3毫秒（走配置）内的单点算作多短音符。
        2. 2：长按，key为对应的按键判定区域，parameter为按下的时长（毫秒）
        3. 3：滑动，key为对应的按键判定区域，parameter为滑动的终点判定区域，如”1“代表滑向第一格，”4“代表滑向第四格。
        4. 4：折起点，同长按或滑动
        5. 5：折中间点，同长按或滑动
    4. 合并规则：
        1. type：4、5合并为一条
        2. type：1需要合并±3毫秒内同type的数据，作为多短音符
3. **类的依赖关系及重要方法**

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722253281997-21ea1c31-d0b1-4e99-9e52-60ea77d01752.png)

:::



# 四、流程设计
1. **流星玩法流程：**

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722243988766-5ecb3e8f-dbb6-4342-9bd9-465694ebb666.png)

2. **按键结构：**
    1. 单音符：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722250120429-a8200cac-bdb2-4d0e-bee5-97de6716fb47.png)单个按键，显示一张Sprite，另外有一个特效挂点。根节点通过Button接收点击事件
    2. 多短音符：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722252160877-673383a1-7c15-4091-8cb1-52eab2ff7f1f.png)，由多个单音符和一个连线组成
    3. 滑动音符：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722252217071-3da29743-cc18-43f0-90c4-170eb96dc539.png)，由一个单音符和一个连线及一个箭头组成，根据滑动距离调整Line长度，根据滑动方向调整Line和Arrow的位置及旋转
    4. 长音符：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722252256916-3679e1cb-2e0f-419c-9675-fbe8a96b7cb0.png)，使用多短音符和滑动音符进行组合，所有子音符挂载在Root节点下
3. **按键的移动曲线**

确定好起点、中间点、终点和移动时间后，根据贝塞尔曲线算出给定时间点的位置。

每个判定框分别有两个对应的空物体，作为中间点和起点，方便后面动态调整下落路径。

4. **按键移动过程中的缩放**

根据当前移动时间和总的移动时间进行插值计算，呈现出由远到近的视觉效果。

5. **音效播放**
    1. 四种类型的音符有各自的点击音效
    2. 当前的连击数>=50（走配置）时，如果出现失败判定，播放失败判定音效。
6. **计分**

音符基础分值：

| <font style="color:rgb(0,0,0);">难度等级</font> | <font style="color:rgb(0,0,0);">基础分值</font> |
| :---: | :---: |
| <font style="color:rgb(0,0,0);">短音符</font> | <font style="color:rgb(0,0,0);">1000</font> |
| <font style="color:rgb(0,0,0);">长音符</font> | <font style="color:rgb(0,0,0);">1000</font> |
| 多短音符 | <font style="color:rgb(0,0,0);">2000</font> |
| 滑动音符 | <font style="color:rgb(0,0,0);">1000</font> |


判定倍率：

| <font style="color:rgb(0,0,0);">打击结果</font> | <font style="color:rgb(0,0,0);">判定倍率</font> |
| :---: | :---: |
| <font style="color:rgb(0,0,0);">遗憾</font> | <font style="color:rgb(0,0,0);">0</font> |
| <font style="color:rgb(0,0,0);">很棒</font> | <font style="color:rgb(0,0,0);">1</font> |
| <font style="color:rgb(0,0,0);">优秀</font> | <font style="color:rgb(0,0,0);">1.1</font> |
| <font style="color:rgb(0,0,0);">完美</font> | <font style="color:rgb(0,0,0);">1.2</font> |
| <font style="color:rgb(0,0,0);">超完美</font> | <font style="color:rgb(0,0,0);">1.3</font> |


连击倍率：

| <font style="color:rgb(0,0,0);">连击数（含）</font> | 连击倍率 |
| :---: | :---: |
| <font style="color:rgb(0,0,0);">1-49</font> | <font style="color:rgb(0,0,0);">1.0</font> |
| <font style="color:rgb(0,0,0);">50-99</font> | <font style="color:rgb(0,0,0);">1.1</font> |
| <font style="color:rgb(0,0,0);">100-199</font> | <font style="color:rgb(0,0,0);">1.2</font> |
| <font style="color:rgb(0,0,0);">200-499</font> | <font style="color:rgb(0,0,0);">1.5</font> |
| <font style="color:rgb(0,0,0);">500-999</font> | <font style="color:rgb(0,0,0);">2.0</font> |
| <font style="color:rgb(0,0,0);">1000及以上</font> | <font style="color:rgb(0,0,0);">2.5</font> |


    1. 单次实际得分=音符基础分值 * 判定倍率 * 连击倍率
    2. 实际总分=所有单次得分相加
    3. 最终显示总分：所有歌曲的总分是1000000分，需要将上一步计算出的总分通过公式：**显示总分=（总分/满分）*1000000**计算出最终得分。所有界面上显示的是最终显示总分。
    4. 超完美+得分：在超完美之上还有一个超完美+的判定，除了按超完美判定计入实际总分外，在计算完最终显示总分之后，还需要再加上一次超完美得分，使显示总分上限突破1000000分。
7. **判定失败时角色播放失败动画**

在播放失败动画期间再次判定失败，不会打断当前动画的播放。

8. **其他细节处理**
    1. 当玩家按住长音符时，玩家的连击数不增加，闪烁判定结果，闪烁的时间为（60/BPM/8）秒
    2. 不同连击数显示的数字颜色不同，通过配表实现。

| <font style="color:rgb(0,0,0);">连击数（含）</font> | <font style="color:rgb(0,0,0);">连击数字颜色（暂定，以美术终稿为准）</font> | <font style="color:rgb(0,0,0);">例图</font> |
| :---: | :---: | :---: |
| <font style="color:rgb(0,0,0);">1-49</font> | <font style="color:rgb(0,0,0);">蓝</font> | ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689898058-09aaabb0-dd88-47eb-9db1-301022477048.png) |
| <font style="color:rgb(0,0,0);">50-99</font> | <font style="color:rgb(0,0,0);">浅绿</font> | ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689900132-fe34c6c3-8af3-476a-8de0-ba7f62835b18.png) |
| <font style="color:rgb(0,0,0);">100-199</font> | <font style="color:rgb(0,0,0);">绿</font> | ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689902314-994e4b61-907a-4103-b2f2-ee6786440758.png) |
| <font style="color:rgb(0,0,0);">200-499</font> | <font style="color:rgb(0,0,0);">紫</font> | ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689904302-3a0b5f49-19fd-412f-a2a4-3b6a7246e940.png) |
| <font style="color:rgb(0,0,0);">500-999</font> | <font style="color:rgb(0,0,0);">红</font> | ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689909087-c89bf04b-9838-468c-8855-38e23068be6d.png) |
| <font style="color:rgb(0,0,0);">1000及以上</font> | <font style="color:rgb(0,0,0);">橙</font> | ![](https://cdn.nlark.com/yuque/0/2024/png/44683805/1718689911420-43445efc-a7b9-48c2-a0de-0a43731d8b17.png) |


    3. 当玩家达到50、100、200、500、1000这5个特殊连击数时，除连击数字颜色变化外，还有外扩放大的特效，以及特殊语音提示突出效果。这里需要等特效做完后处理，开发过程中先预留对应的处理接口。
    4. 结算界面比传统模式多了一个超完美数量的显示。![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1722244637571-a156a56d-adcd-4b11-ac01-69549484938b.png)
    5. 右上角增加一个开关，切换场景是否显示。



---

# 六、总结整理（开发完成后撰写）
开发过程中，很可能产生跟上面方案不一致的地方。可能补充了更多的细节内容，可能由于某些未想到的原因推翻了方案中的一些设定。

所有开发完成后有价值的内容，都可以记录在这里。

