### 美术ui示意图标注规则
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733716240540-a562e11b-172b-438a-b819-55e07c11838c.png)

**美术字体标注：**

4 4012 24px 居中 #fece0c 栏宽504 行高48

3 e995 #fece0c 24px 位置跟随

整体宽度504

1.2.3.4分别对应四种字体，<font style="color:#DF2A3F;">注意界面上选择字体时，需要选择cn路径下的字体</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733717023362-e34f416c-52fe-4a11-a597-8cd73975692f.png)



<font style="color:#DF2A3F;">4012：对应字体的材质效果，有描边阴影，渐变等，不同材质对应效果不同，以美术标注为准</font>

24px：字体大小为24像素

居中居左居右：是字体的布局

#fece0c：字体的颜色

<font style="color:#DF2A3F;">栏宽，行高：字体组件的宽高设置，以美术标注为准，如果没有标注，栏宽则自适应；高度默认是字体大小的两倍</font>

3 e995：是图片文字，e995是对应字体的unicode码位



![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733726254763-96299afb-e057-4326-b4d7-8d55c6b7cd0d.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_852%2Climit_0)

<font style="color:#DF2A3F;">4 60px 上#ffe065 下#ffffff 居中：当文字设置了上下或者左右渐变的时候，需要把对应的参数设置为line</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733728020326-865c6577-59d5-4eae-af58-1e91b1017348.png)

### 特殊情况
##### 1.<font style="color:#DF2A3F;">图文混排的文字</font>，如果共用同一个text，则字体以文字标注字体为准，图片文字会以callback字体去寻找；如果不共用，则图片文字用icomoon_fishing SDF，文字字体用标注的字体。防止出现图片显示正常，文字效果不对的情况。
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733727236769-9bbbb1f4-f963-42f0-90a6-18774c74bb63.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733727263415-53b1d583-42b2-4b6e-a47f-2733d39f0a11.png)

##### 2.<font style="color:#DF2A3F;">文字，图片位置跟随</font>的效果，会用到布局组件控制子节点的宽或者高度，里面的子节点的宽或者高度是自适应的，美术会标注限制整体宽度，防止多语言文本过长，整体布局超出最大宽度范围
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733722050948-7ae5d6b8-cde4-4dc8-b666-1fd26c0ef932.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_564%2Climit_0)

##### 3.当文本遇到标注字体为4：Ethnocentric Light SDF，需要居中时，要选择MidLine居中方式，不然字体是偏下的
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733722409876-4a69b795-cbea-405a-994d-6d3dda479513.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1125%2Climit_0)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733722460976-2111928a-2a97-4a70-ba2d-ce1acf7c0cf6.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1125%2Climit_0)

##### 4.对于文本多语言适配策略：
I.单行不换行，限定栏宽，文字大小自适应，提供最大最小字号范围

II.换行，一般情况下默认是两行，美术提供文字栏的宽和高，最大最小字号范围，行间距line为-50

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733723783211-71b41d90-ceda-4c46-9cee-b48ca6deb785.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1125%2Climit_0)



### 字体使用场合
##### SourceHanSansCN-Medium SDF：标注为1
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733731979719-3714c31a-8e81-4478-b481-3c0e6d418d35.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733732327090-3d92f1a5-8f38-4667-bfa8-04868f9db9c2.png)

最常用字体，描述，标题，普通文本，提示tip，功能入口按钮文字等基本用的都是1字体，通过不同的材质设置，描边，渐变达到效果

##### SourceHanSansCN-Bold SDF：标注为2
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733731687849-9b3e6e3b-80af-4a94-9089-2a211ae00d82.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733732648131-da8f36c7-4fe1-4233-b163-5372bfe8ea0c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733732703782-37e17300-f695-4331-bd8a-9a8ed4ea633c.png)

普通文本的粗体，玩家名字，道具名字，弹窗界面（不是全屏界面）界面文字较少时，需要用到粗体，让玩家更能注意到

##### icomoon_fishing SDF：标注为3
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733731188598-e2d5ed14-6a3d-4f9f-89ab-45239f82a24c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733731285353-1f79b801-f06a-410d-931b-b72c7cc52caf.png)

图片文字，界面上一些小图标，功能标识按钮等使用

##### Ethnocentric Light SDF：标注为4
![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733731487406-9748e1f3-5f2b-4c60-a9bb-53efbbfba391.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733731547760-34dbe70b-f06c-4034-b6c3-68d0e1cb76e9.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1733732141874-84fb926f-1d7b-4aa3-89a4-36a080362e3d.png)

货币栏，通用按钮标签文字，界面上特殊显示需要玩家着重注意的文字或者数字等使用

### 目前项目中字体出现不统一的原因
同一个界面内，字体基本没问题，但是如果存在几个界面都具有相同的元素，会出现不同的界面，不同的字体。

1.有的是用的通用的，美术没有标注，程序在拼界面的时候就用了默认的，应该<font style="color:#DF2A3F;">在拼界面的时候，如果对比效果图有明显差异，直接找美术沟通确认</font>

<font style="color:#000000;">2.有的是美术修改了标注，但是是之前原有界面基础上优化，但是标注上面优化的东西不明显，程序没有get到，</font><font style="color:#DF2A3F;">这个建议美术每次优化的时候，把本次优化修改的东西着重标识出来</font>

<font style="color:#000000;">3.有的也确实是程序的问题，字体用的就是错的，</font><font style="color:#DF2A3F;">大概率是复制过来的，没修改，直接拿来用了</font>

<font style="color:#000000;">4.就是出现了上面提到的特殊情况，程序没有根据图文区分文字字体，使用的同一种字体</font>

<font style="color:#000000;">5.程序在自测的时候，也要注意多语言问题，切换多语言自测界面</font>



****



