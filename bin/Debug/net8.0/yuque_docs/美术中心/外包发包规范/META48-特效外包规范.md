# **外包特效制作规范**
引擎版本：Unity2022.3.22f1

特效风格为半写实、偏写实。

## **一：特效资源规范：**
**1.****特效预制体**

存放\Assets\Art\Particle\Prefab\

特效预制体命名方式，根据具体需求不同有不同命名方式（提外包需求时，我们这边可能会提前命名好）

boss鱼技能特效：P_FX_Fish鱼编号_skill_序号

炮台相关特效：P_FX_炮台编号_相关说明_序号

其他特效：P_FX_说明_序号

**2.****特效贴图**

存放\Assets\Art\Particle\Texture\

贴图格式png,tga，贴图大小2的整数次幂，尽量使用小尺寸，通常256*256，最大不超过512*512工程中已存在的贴图直接使用

贴图尽量使用填充率高的，

带有透明通道的贴图请勾选Alpha is Transparency

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243823709-5c32e575-638e-438a-a837-ea5d1453b117.png)

贴图命名方式Tex_FX_wb_描述_序号，例如Tex_FX_wb_glow_01

贴图warp mode修改成clamp，请在贴图命名后缀加_clamp

**3.****特效模型**

存放\Assets\Art\Particle\Mesh

模型导入引擎格式为FBX2014/2015，单个模型面数少于600，从max导出时关闭不需要的选项，比如动画，灯光等

模型命名方式Fbx_FX_wb_描述_序号，例如Fbx_FX_wb_tiaodai_01，

**4.****特效材质**

存放\Assets\Art\Particle\Material

材质球命名方式Mat_FX_wb_描述_序号，例如Mat_FX_wb_kuosan_01

**5.****特效动画**

存放\Assets\Art\Particle\Ani

动画命名方式Ani_FX_wb_描述_序号，例如Ani_FX_wb_xuanzhuan_01

**6.UI****特效的预制体和动画文件**

在界面预制体文件夹中，建文件夹FX来存放，例如\Assets\Art\ModuleRes\GameMain\FX

存放目录示意图

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243834116-e1c860c5-cf60-4d0c-9dfe-8410246334ff.png)

## **二：特效制作流程规范**
特效编辑场景：打开工程，打开菜单栏RSTools,选择打开场景，选择Timeline编辑场景

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243851294-df946772-3012-489a-aad7-d383025cdbd4.png)

在资源管理器中搜索Main场景并拖入Hierarchy中并双击。运行一下，这样就可以在这里进行游戏特效的制作了。根据需求拖入需要作为参照的模型、TML等。

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243860065-6ba98eba-0fb1-432f-80ba-acd3851cd9b5.png)



## **三：材质球使用说明**
特效通常使用的材质

1. 通用特效材质

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722059872124-dd6d5012-9498-4ccb-a032-fec3d41b7210.png)

2. 空气扭曲材质

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722059876914-ef65c43f-fac8-4de6-b69d-12cfdc29ed39.png)

外包同学可以直接复制已有材质球，重新命名后修改。



**注意事项**，材质球中不用的开关不需要打开，不用的页签中不要有冗余的贴图存在



通用特效材质球截图说明：

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243883366-a8d7f227-ca8f-46b6-b188-87e2b79805a5.png)![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243903198-571fbe17-f824-4ceb-950e-98b73ea32893.png)

空气扭曲材质球截图说明：

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243917931-8768a45e-1394-49cb-8c76-7c5dfcd7b6a1.png)



## **四：关于特效预制体及粒子系统的一些说明**
1. 特效预制体所有Layer层选择为EffectSkill层。

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243962346-99276d07-5d41-4f66-b85d-8bb73c0c9c6d.png)

2. 预制体父级Transform值应对归零，缩放规1。

3. 粒子系统Scaling Mode模式选择Hierarchy。

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1722243972295-f7859eeb-e853-4342-a72f-a60da63675e2.png)

4. Max Particle Size调至100。

5. Order in Layer 统一30，可使用Sorting Fudge调节粒子之间层级。

6. 预制体内粒子系统命名用易懂的拼音，或者英文单词说明，不能简单用1234，abcd，不能是默认的Particle System。

7. 关于性能，发射器数量控制在20以内，粒子系统中用不到的选项不要开启，尽量复用材质球，减少粒子发射器发射mesh

8. 特效预制体要进行拆分，例如施法前段，施法中，施法受击，飞行子弹等，合理的拆分使得特效组成部分清晰明了。



## **五：关于特效需求**
特效需求将会以表格的形式发出，其中会描述具体需要的特效形式，特效类型，会提供特效所依附的单位模型动作等，特效提交也将体现。

有的特效不需要使用timeline，比如鱼本身的常驻特效。

有的特效需求，比如技能类，表演类，是通过Timeline来实现的，我们会提供包含动作在内的Timeline，特效制做也需要直接配在上面，提交资源时，将包含特效的Timeline文件导出并提交。Timeline的轨道使用方式具体再沟通。



## **六：特效资源的导出与提交**
同一个特效需求的所有特效预制体导出成一个Package，命名说明特效内容。

导出时需要检查特效资源的命名，存放路径等是否正确。

