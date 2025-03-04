### 一、问题现象
在Frame Debugger发现有些材质球是单独一个批次（同一个模型同一个材质球），调查改材质球发现材质球上的属性ZTest为Less，而Frame Debugger里显示该材质球的ZTest属性为LessEqual，把材质球的ZTest改为LessEqual就合批成功了。

### 二、调查问题
![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740462980313-6126babc-5c6f-4d73-b1dd-8eb7540a0a94.png)

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740462908462-d68729c3-1764-4fc6-8388-e967e6208add.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740462882284-594d09ee-c8d5-464d-9248-585624568c12.png)

首先_ZTest属性只在StandardLit Pass里使用，而SelfShadowCaster Pass恒为LessEqual，但是在材质球面板上修改ZTest属性确会影响合批

——————————————————————————————————————

条件：材质球上的ZTest设置为Less，SelfShadowCasterPass里的ZTest设置为LessEqual

结果：与其他的Batch未合批

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740463240524-deb18ae3-d443-4425-82a0-9687009ef8dd.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740463279213-6a0cc4e4-c153-44df-8832-c265535eb2c6.png)

————————————————————————————————————————————

条件：材质球上的ZTest设置为LessEqual，SelfShadowCasterPass里的ZTest设置为LessEqual

结果：合批成功了

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740463385532-f801f0ea-6bbc-4459-8310-357780a5c6fb.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740463405692-11ff66fc-5ecd-4433-b3e2-f3858d02f17e.png)

——————————————————————————————————————————————

条件：StandardLitPass属性直接改成Less

结果：self的ShadowCaster正常合批

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740464623184-1c29ab76-fcbb-4bf1-8a38-e10f91e0ebb9.png)

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740464589025-e3f38dd3-894a-4621-b90c-4bf5c5856ab3.png)

——————————————————————————————————————————————

条件：在其他的Pass里，如DepthOnlyPass里使用该属性，其他Pass不使用该属性

结果：与其他的Batch合批失败

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740465387116-7f0f9bd2-34ab-47ad-bddc-8ca14304166b.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740465423886-da4c598d-e973-4090-a59c-f3d01bf0dc65.png)

——————————————————————————————————————



得到结论一：Properties里的_ZTest属性，只要在Pass使用，就会导致其他Pass合批失败，一个Barch里的Ztest只有一种状态，而Pass里是否合批是先检查Properties里的_ZTest来判断，而不是检查实际Pass里的ZTest属性



联想到新条件：那如果Properties里没有_ZTest呢?

条件：将Properties里的_ZTest名字改成_ZTest01，且材质球上的ZTest还是设置为Less

结果：和其他Batch合批失败

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740466014126-ae8ad282-35e9-4d96-95d6-db1d4909bb5c.png)

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740465984001-7b1f54c8-3fba-4aba-94a0-28f0739a8e17.png)![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740541643900-2f30f490-1197-4600-aa11-fe9149ffecd9.png)

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740466116678-e85b00c3-334e-45a7-88ff-758ab048695a.png)



### 三、最终结论
在检查过程中发现，其他属性，如_Cull  ，Stencil的_Comp和_Pass的不同也会影响合批

**合批前，unity会判断这些变量值，他不在CBuffer里，但是会存在所有pass里，因为不在CBuffer才导致合批失败  **

![](https://cdn.nlark.com/yuque/0/2025/png/39137189/1740466313728-7e9b9298-d20e-44ea-ba8c-9102156e2fba.png)

