## 整体思路
### 一、控制方式
将原先自定义脚本控制转换为Global volume控制

eg.

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718677494650-c00de766-5ac9-4ade-8761-b3b72bf4289a.png)

由自定义脚本控制转换为

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718677813492-88560188-4094-4604-b047-58f222b6db98.png)

由通过Global Volume中Add Override的方式添加自定义控制Volume

添加完成后效果示意：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718678170150-3a3fa29b-d476-42d8-9cc3-2f6f5890fb13.png)

实际操作其实差不多，只是挪了个位置。并且该项改**只是使用到了unity后处理模块的相应功能，不涉及对URP管线的改动**。



## 二、具体实现
### 2.1 Volume相关
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718680589309-5ba21f38-6acd-487b-8906-64b87812c88f.png)

通过继承VolumeComponent类和IPostProcessComponent接口实现在Global Volume中调用

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718680876027-28394523-02b8-40d1-9245-3ea3a6dd6c24.png)

在其中填充需要使用的属性



**设想：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718681319550-0762547f-0ab1-4167-9db3-06b08eb2ccb7.png)

将目前的Environment Volume和Light Setting Volume重新拆分成Character~~, PostProcess~~和Environment 2个Volume（暂定），每个Volume统一管理和自己相关的所有参数，~~每个Volume在场景中唯一（？）~~，







**远期：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718681673867-2fd21330-baa9-42dc-ac33-c2cedd9afee9.png)

~~Volume与RenderFeature & RenderPass联动，控制高中低配相关设置~~  暂不考虑



### 2.2 RenderFeature相关
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718676309375-760bd624-ad98-403e-8fa0-411a1353aeaf.png)

目前做法为每个效果Feature制作对应的Render Feature并附加在管线上

新做法为建立一个大的RenderFeature，统一管理其中的RenderPass，eg.

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718682898190-af26d2d7-5e21-4366-b89c-293d2110615c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718682935614-16c04ce9-c96f-457b-ace8-62494615d1fa.png)

该做法对于RenderPass的启停、排序与高中低配设置管理会有更好的表现



**待定：**~~目前倾向于基于2.1对Volume的分类建立3个大的RenderFeature（或者直接就全部共用一个？）~~

          分为CharacterVolume及EnvironmentVolume



### 2.3 RenderPass相关
<font style="color:#000000;">由于RenderFeature改为了一管多RenderPass的模式，需要保持简洁，代码尽可能只涉及对RenderPass的处理部分（启停、排序、销毁等）；因此参数部分尽可能全部在Pass中声明和处理</font>

**<font style="color:#000000;"></font>**

**<font style="color:#000000;">远期：</font>**<font style="color:#000000;">RenderPass相关也看到了一套相关的管理方法，目前还没理解透，暂还是按照原有方式制作，除了与Feature对应关系由一对一变为多对一</font>



### 2.4 Environment Volume 与 Light Setting Volume 的改动
设想是按2.1拆分成2个Volume，然后因为原Environmnet Volume和Light Setting Volume会涉及到复数Volume融合的情况，因此首先需要明确要移植的新项目中对Volume融合的使用情景，然后据此拆分出其中会涉及融合的融合参数与常量参数。



~~因为拆分后的三大Volume在场景中唯一（2.1设计）~~，所以对具有不确定性的复数融合参数建立Manager进行管理，经由Manager计算出最终值后传递给对应Volume**(待定)**



融合参数的设置方法：根据实际需求决定（eg.按类似原Environment Volume的方式脚本控制？）

## 三、后续功能与注意事项
1.后效层级、以及多摄像机情况

2.Volume优先级，重载相关设置

