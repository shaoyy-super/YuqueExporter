原来的做法扩展性太差，每一个Boss入场都对应一个Panel，Boss属性、名字、icon等其他信息都是在界面里拼死的，一旦美术需要修改效果，有多少boss入场界面就要修改多少遍，所以删除了原来的做法。

现在的做法如下：

1、在出场TML预制体上添加脚本 **ToLuaReceiver**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942470750-87fcf548-8fa3-4211-94b0-31b967b7b161.png)

2、预览时还是用**预制体生成UI节点MoleUIEffectTrack**创建**UI_Battle_BossAppear_Panel**，调整好位置和效果(**调好效果后要删除这个片段，仅仅是用于编辑时方便预览**）

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942470981-564db5b2-fb4b-45ca-af22-7ac13d02f0f2.png)

3、在开始展示Boss属性的位置，添加**ToLuaMarker**，设置ParamList参数为**BossAppear**

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942471293-bd5ffbb2-7e57-4288-9c20-d4e05cbe6e98.png)![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942471508-ceaa0b63-8790-4135-8976-90efc993134f.png)

4、在结束展示Boss属性的位置，添加**ToLuaMarker**，设置ParamList参数为**BossDisappear**

如果属性展示和tml同时结束，第二个ToLuaMarker可以不加，会自动在TML结束时隐藏展示界面

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942471837-c06d6234-5fcb-436f-a0a4-90dd4adbbd17.png)

注意：

### **一定要确保，最后要把第二步用于预览的生成UI节点的轨道删掉**
