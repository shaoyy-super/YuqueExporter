**0，SVG导出选项**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410375020-7a9e8711-ec3c-4337-b0eb-f5193b7ac880.png)

**1，进入网页**[**https://icomoon.io/app**](https://icomoon.io/app)**，点击** **Import Icons** **按钮。**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410375243-a8598f6b-ddb1-4606-a92c-48608b72db7f.png)

**2，先清空原有的图集，再选择需要导入的** **.json 文件，先点击** **Generate Font** **页签加载字符编码（*防止老编码被新增编码打乱），然后再点击** **Selection** **页签添加新的** **.svg** **文件**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410375525-77a6382f-dd23-425b-8fb9-7b2005180d0f.png)

**3，选中（橙色白底方框代表选中）所有需要的图标及新增的图标，点击** **Generate Font。**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410375777-69e6cd85-8f39-4871-9dbd-424196b093fd.png)

**4，点击** **Download** **按钮将字体文件下载到本地，记住最新的字符编码 e9xx（后续会用到）**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410376019-fdc6c505-306c-4922-aa20-4f82c04d313c.png)

**5，打开软件** **FontCreator，打开下载文件中的 iconmoon.ttf 文件**

**6，以 FA 项目为例，将保存的文件分别放入目录（client\Assets\ArtContent\Font）下的 cn 和 en 文件夹中。若更新则覆盖原有文件**

**7，在 Unity 中找到这些文件，点击** **Update Atlas Texture**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410377568-1838a5c9-54e5-4916-8e51-b63b9b168593.png)

**8，在** **Character Sequence（Hex）栏中修改字符范围：E900-最新的编码。点击** **Generate Font Atlas，完成后点击** **Save。（多语言上传时，每个语言字体都进行一次此操作）**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410377869-4c82d133-7f80-4f0f-ae52-ef5b2d6373d4.png)

**9，上传相应的文件**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1715410378165-02868094-25f7-4dec-82b3-d14dba4e6eac.png)

**Unity使用方式：选择icomoon字体，文本框中输入\ueXXX（对应图形的字形编码）**

[thoughts 文档](https://thoughts.teambition.com/workspaces/5ef1a751f60ea9001bd606cc/docs/609ba02412d5ba0001f8a2a2)

