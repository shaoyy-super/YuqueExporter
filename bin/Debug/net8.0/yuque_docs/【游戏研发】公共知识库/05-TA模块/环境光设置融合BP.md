### BP使用说明：
**<font style="color:#DF2A3F;">1.安装插件LiveLink，否则无法在编辑器模式下实时更新蓝图</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721711437961-0e1205b5-a80b-4bd4-ad7d-45da964ca9c9.png)



2.将BP_EnvMerge拖入关卡中

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721712774441-4e9107eb-6ab2-4043-8ebf-8ffea97facd1.png)



3.蓝图使用说明

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721712750465-001d648e-2120-4af7-94ee-48596a9772d8.png)

(1)**TurnOn: **决定蓝图的启用与否

(2)**Cine Camera: **生效的摄像机，会根据这个摄像机的位置来选用对应的灯光预设

(3)**MainActors: **最终应用光照数据的Actor，可以使用默认那套，无需置入PostProcessVolume和ForwardVector

(4)**EnvActors: **环境光照预设

索引[0]为默认设置，复制MainActor置入，在全部Volume外时会从这里传值给MainActors，后面要改默认值就改这里的Actor参数，索引[0]无需置入PostProcessVolume和ForwardDirection，索引[1]无需填写ForwardDirection

后面的预设可以通过➕号添加，生效范围为其中PostProcessVolume的范围（暂不支持旋转）

ForwardDirection为摄像机进入该预设Volume范围时的主要运动方向，需手动填写，注意只能在一个轴向赋值，例：（1，0，0），考虑过根据摄像机速度自动计算，但在有大折角时可能会切换轴向产生跳变，所以还是手动输入吧





### 注意事项：
1. Volume最好按摄像机的进入顺序排列

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721639022228-85c6242c-eb42-4444-8e62-3b8ad7c14537.png)

2. 该蓝图只修改光源信息，无法控制自动曝光等后效问题



调整时：

预设建议放在Light关卡，建立不用文件夹管理

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721713972318-629ce780-2b49-445b-aebd-bddf48b3385b.png)

调整预设时让其他光源全部不显示，否则多个光源会抢主光源

或者只显示MainActors内的光源，将摄像机放到对应预设的Volume中，然后调预设的参数，相关修改也会反映到主光源中



### 可混合参数：
部分参数蓝图中不可读或不可写，以下为能做融合的所有参数



平行光：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721731274870-3a2c5160-0483-4f2c-bbb5-413a6adf649d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721731207462-b65c8d2a-c857-4bee-ada8-63e8d87eadbc.png)

高度雾：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721714353617-c53c294b-452d-4f79-8a87-eff63b3d2887.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721819987942-93f81a01-fc1c-4f75-a55c-fd8840113266.png)

天光：



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721714380806-5ff79c58-484e-4285-941c-ff198bb7219f.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721731644281-ec4183dc-fafb-427f-b6cb-45430f06be3d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721731680436-43259715-1040-4e40-b184-0711dafa0dec.png)

大气：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721714397393-5d67650d-29b2-47a7-95c3-a218ccbf35e6.png)![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721731898634-3dc9a510-cb5e-4d9b-a87f-b8e9bc709122.png)![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721731942465-a1d2bd42-4d7c-4d17-8082-ed092e8a7d4b.png)

