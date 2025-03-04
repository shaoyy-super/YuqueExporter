# 生成
### 1. UI提供的素材
UI会生成一个(.ttf)的字体文件，放到ArtContent\Font\cn目录下。打开 [<u>https://icomoon.io/app/#/select</u>](https://icomoon.io/app/#/select)链接，打开 <u>dolbox48_artwork\UI\字体\Json</u> 工程文件下的 <u>selection.json</u> 文件，点击Generate Font即可看到图形对应的编码，类似下图：

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359355340-f74f3488-e3e5-48c4-88a7-3ac9f59a4467.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359355589-34b02991-9ab9-4b17-bdba-58ed3afe89c7.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359355920-118e61d5-3e74-40df-875a-3a39bb5ba411.png)

### 2. 生成TMP_Font_Asset
通过Window/TextMeshPro/FontAssetCreator打开窗口。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359356124-6e0868e5-7fde-46c0-a532-38bfd20db849.png)

然后按照下图设置好参数，并生成、保存FontAsset。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359356363-3217ffbd-cd6a-4eb2-b2a2-68f704bd316a.png)

然后找到该字体文件同名的material，将其shader改成TextMeshPro/Mobile/Distabce Field。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359356652-fabf8ae7-3a50-41c8-a37a-10b3c41ff0d5.png)

### 3.使用规则
找到名为PreloadTMPAssets的预制体，在ChachedFontAssets列表里加上刚刚生成的FontAsset，并将其打个ab包。在实际UI中用<font="icomoon SDF">\uxxxx(ue900)</font> 的格式插入图形，如果需要插入其他字体的文字，如下图。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359356875-e4c1faf7-e30e-49d4-81c2-ecad486bf43a.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359357070-d9aeeafa-6408-4c42-a939-efb22f177b3b.png)

游戏中实际效果：

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359357573-48fa4834-9ef9-427c-a18d-05954a84259c.png)

# 更新
当我们因为某些需求需要更新字体参数，或者增加字符集合时，可以在现有的基础上更新字体文件，如下：

1. 选中对应字体Asset，在Inspector面板点击图中按钮；

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359357923-849994aa-cc04-4b4e-81e6-cdde222ac681.png)

1. 接下来在设置面板中，先修改自己想要的设置，具体参考上文设置面板说明。
2. 再点击GenerateFontAtlas重新生成字体贴图，生成好之后点击Save即可完成更新。

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1714359358171-efdd059a-0b56-4705-af52-c02ef9aa92d7.png)

注意：不要点击Save as... 那样会新创建一个字体文件，而不是更新现有文件。

