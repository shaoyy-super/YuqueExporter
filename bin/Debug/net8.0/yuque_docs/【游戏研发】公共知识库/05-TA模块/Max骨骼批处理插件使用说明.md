# 插件使用
## 在Max中打开所需要处理的文件
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726731011113-a60e0f54-3763-42ea-8632-0ff6444b0e49.png)

## 将脚本window.ms拖入Max中
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726731506259-08bf6774-d7bf-47bd-8ed8-41b477f547f1.png)

## 直接点击Batch按钮，执行指令
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726731544491-9791036d-d391-4360-b133-8a4e41b01449.png)

## 弹出窗口，处理成功，表示处理完毕
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726731767856-ee22e5b1-658f-431b-985b-49d190297679.png)

# 插件各部分功能解释
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726734577940-6de27868-5f6b-4d1c-869d-5ea7aff98249.png)

## Add Twist Bone
增加Twist骨骼，在四肢上分别添加16根twist骨骼

> 添加前：
>
> ![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726734908726-d18636eb-6ab2-4b99-96fe-b4d21e390098.png)
>

> 添加后：![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726734969579-886f1ca0-59d6-4294-9735-4c4869272a45.png)
>

## Transfer Skinning
这个功能是骨骼的蒙皮转换功能，将指定的Mesh骨骼上的蒙皮转换到另外一个指定骨骼上

在插件代码目录下会有一个version0-0-1.txt的配置文件，里面记录了所需要的配置信息，其中#mapping下的就是蒙皮转换所指定的骨骼：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726735200763-f1f5952d-96fa-4040-9fa6-e5aee64e8497.png)

例如：

> Bip001 Spine1: C_Spine1_01_K,C_Spine1_02_K,C_Spine1_04_K
>
> 就是将C_Spine1_01_K,C_Spine1_02_K,C_Spine1_04_K这三根骨骼的权重转换到Bip001 Spine1上去
>

:::color4
需要注意的是，执行Transfer Skinning需要选定至少一个Mesh，这个会对选中的Mesh进行蒙皮处理！

:::

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726735604814-8b0ab986-626b-4fde-9763-15e7686965f2.png)

## Delete Bone
删除不必要的骨骼

在配置文件中：#delete后就是要删除的骨骼

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726735825540-91ed34f7-d84a-4872-848d-61ce2ef91114.png)

:::color4
在蒙皮转换中，所需要转换的骨骼也会加入到删除的骨骼中

例如：

Bip001 Spine1: C_Spine1_01_K,C_Spine1_02_K,C_Spine1_04_K

C_Spine1_01_K,C_Spine1_02_K,C_Spine1_04_K这三根骨骼也会被删除

:::

执行后不必要的骨骼被删除

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726736147703-52fcb0f8-1f62-4012-be4d-c7225d3a12d0.png)

## LinkBone
将指定的骨骼链接到另外的骨骼上

在配置文件中：#link后的内容就是链接骨骼的内容

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726736282997-9084585b-be4d-48e6-9016-91bf46318d4b.png)

> 例如：
>
> Bip001 Spine2:Bone001,Bone002
>
> 就是将Bone001,Bone002链接到Bip001 Spine2上，成为Bip001 Spine2的子节点
>

## rootConnection
增加Root骨骼，将Bip001链接到Root骨骼上，成为子节点

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1726736500029-3adc8fb0-c49f-4776-8287-cacdbe8b1e64.png)

## Batch
Batch会依次执行上述所有的功能（在TransferSkinning的时候回遍历所有的Mesh）





