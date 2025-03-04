# 一.简述
考虑到捏脸/换装相关美术资源及配置资产，在多个项目间进行共享，或者更进一步在多个开发团队之间共享，特做出以下方案设计。

# 二.ID段划分方案
基于ID数字位数尽量短的原则，同时考虑到资源类型及应用场景和处理方式的不同，现将资源分为两个大块，即服装资源和捏脸资源，二者之间的ID互不相关。

即一件衣服的ID，允许和一个眉毛的ID相同。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727258626105-5ecfb60c-2e35-4228-ba73-72d8512720b1.png)

## 捏脸资源ID段划分
首先捏脸资源ID，先按大类区分，大类数量占两位，即预留99个。

类型ID需要新增/删除/修改时，同步所有使用方。

每个类型的实例ID占用三位，即每个类型预留1000个实例ID。

由程序导出数据时自动根据后三位的序号（美术设置后三位），自动生成ID。即从美术制作的视角，只需关注后三位即可:

| <font style="color:#000000;">类型</font> | <font style="color:#000000;">类型ID</font> | <font style="color:#000000;">实例ID下限</font> | <font style="color:#000000;">实例ID上限</font> |
| :---: | :---: | :---: | :---: |
| <font style="color:#000000;">眼妆</font> | <font style="color:#000000;">1</font> | <font style="color:#000000;">1000</font> | <font style="color:#000000;">1999</font> |
| <font style="color:#000000;">眉毛</font> | <font style="color:#000000;">2</font> | <font style="color:#000000;">2000</font> | <font style="color:#000000;">2999</font> |
| <font style="color:#000000;">腮红</font> | <font style="color:#000000;">3</font> | <font style="color:#000000;">3000</font> | <font style="color:#000000;">3999</font> |
| <font style="color:#000000;">唇妆</font> | <font style="color:#000000;">4</font> | <font style="color:#000000;">4000</font> | <font style="color:#000000;">4999</font> |
| <font style="color:#000000;">面纹</font> | <font style="color:#000000;">5</font> | <font style="color:#000000;">5000</font> | <font style="color:#000000;">5999</font> |
| <font style="color:#000000;">睫毛</font> | <font style="color:#000000;">6</font> | <font style="color:#000000;">6000</font> | <font style="color:#000000;">6999</font> |
| <font style="color:#000000;">眼睛</font> | <font style="color:#000000;">7</font> | <font style="color:#000000;">7000</font> | <font style="color:#000000;">7999</font> |
| <font style="color:#000000;">皮肤</font> | <font style="color:#000000;">8</font> | <font style="color:#000000;">8000</font> | <font style="color:#000000;">8999</font> |
| <font style="color:#000000;">眼影</font> | <font style="color:#000000;">9</font> | <font style="color:#000000;">9000</font> | <font style="color:#000000;">9999</font> |
| <font style="color:#000000;">眼线</font> | <font style="color:#000000;">10</font> | <font style="color:#000000;">10000</font> | <font style="color:#000000;">10999</font> |
| <font style="color:#000000;">眼睑</font> | <font style="color:#000000;">11</font> | <font style="color:#000000;">11000</font> | <font style="color:#000000;">11999</font> |
| <font style="color:#000000;">...</font> | <font style="color:#000000;">99</font> | <font style="color:#000000;">99000</font> | <font style="color:#000000;">99999</font> |




多个开发团队进行共享配置资产时，需确保ID唯一，因此，加入开发团队ID作为表示。

开发团队ID在交付CustomizerPackage前确定，并写入代码内部，无法从外部更改。

开发团队ID占用1位，即预留9个开发团队进行资产数据共享：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727254496533-c0a50662-a1a8-4fb5-b3f5-194eb6c8f949.png)



## 换装资源ID段划分
同捏脸资源ID段划分，采用相同的规则。不同的是，考虑到将来可能制作的数量级，为每个类型，预留10000个ID

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727262500919-3bf974db-aa0e-49ef-bca0-09e2e22380dc.png)

# 三.多个开发团队共享资源ID方案
在此明确一个概念，一个资源ID并不等同于一个png或者一个预制体。

如下图，一个资源ID表示了对一组文件的引用，以及一些相关配置资产。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727255479638-7e18beb6-956f-4ca1-af03-f386bc3f766c.png)

## 开发流程
以下为文件及配置资产的开发流程：

step1.搭建一个文件服务器，以及一个供预览和管理的网页。

step2.为每个开发团队分配一个团队ID，用来影响资源ID

step3.制作原始资源，如眉毛贴图，衣服的FBX/贴图

step4.导入进DIYEditor，进行数据配置。如眉毛的初始位置/旋转/缩放，衣服的染色配置

step5.导出数据json，美术手动填充依赖的文件，以及预览图片。其中json形如：

```plain
{
    "metaData":
    {
        "version":"1.0",
        "id":"210001",
        "type":"1",
        "icon":"210001icon.png",
        "chineseName":"一个眼妆",
        "updateTime":"19456353424355",
        "descArray":[
            {
                "desc":"正面截图",
                "textureName":"正面截图.png"
            },
            {
                "desc":"侧面截图",
                "textureName":"侧面截图.png"
            }
        ]
    },
    "assetData":
    {
        "texturePath":"1.png",
        "offset":"0;0",
        "xxx":"xxxxx"
    }
}
```

step6.将json及配套的预览图片，依赖文件，一起上传到文件服务器

step7.使用的游戏项目挑选，下载某个资源包，并导入进游戏项目，使用工具解析Json，将数据配置合入

step8.项目可能会修改优化资源包，如果是大的修改，可以复制一份进行修改，命名为新的ID并上传



![网页预览示意图](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727346995049-8d038687-6d3f-4d3c-8c63-688d50253a1a.png)



![资源详情示意图](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727347104122-ebde5e6b-7946-477f-96df-07a8cd794864.png)



![资源流程](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727343519249-bde58de2-20e1-4d73-81c8-70908a9dae10.png)

## 版本维护
如下图，代码包会率先发布新版本，新版本可能新增或者优化了功能。

于此同时，美术制作标准（ArtVersion）会跟着同步变化。

所以ArtServer上的资源会存在版本管理问题，此时有两种做法：



一是<font style="background-color:#FBDE28;">服务器上尽量保留每个版本下的资源，游戏项目仅能下载和自己版本一致的资源并导入</font>。

同时我们会<font style="background-color:#FBDE28;">使用工具，尽可能的自动的根据一个原始版本资源，生成所有的版本</font>，此过程发生在文件服务器上。

如下：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727351195858-91ca42a8-9960-44e1-aff7-5333e33fc360.png)



二是<font style="background-color:#74B602;">文件服务器上只保留最新版本的资源，游戏项目方下载后，自行使用工具进行转化</font>：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727351156698-b7f83c79-2e72-4683-b1bc-06e39b7c13da.png)



# 四.单个开发团队多项目方案
去除掉"开发团队ID"这个概念，生成资源ID时不再考虑该项。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1727345941769-5dfeb782-b08e-4ef0-bd7b-f96670fe6f96.png)

