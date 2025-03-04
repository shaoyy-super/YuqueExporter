## 使用方法：
### 样条线：
1. Hierarchy下右键 -> Spline -> Draw Splines Tool 创建样条线生成工具

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734607066843-737d1d86-4173-4c04-aba5-50db2f6981bf.png)



2. 在场景内点击创建样条线，双击回车完成编辑

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734607235323-30b46721-620b-4d5b-b8c7-b3d64a32f8b2.png)



样条线各项编辑功能说明：



选中待编辑的样条线，点击左上角红框图标进入样条线编辑模式

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734608444590-45395464-6cc6-47eb-bda8-0ce70e5de616.png)



点选需要编辑的节点

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734608570896-c5a5fe87-7567-4220-9b10-9f7a8b14f1aa.png)

直接拖动可以改变节点在**XZ轴**平面的位置，也可以按w使用移动工具改变位置

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734607340146-f93bbc12-93bf-498d-b5db-31ab91751a73.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734608939333-a67223bd-8f22-4ca1-8d0e-8c8a238c09d2.png)

右下角可以选择样条线的生成方式：线性，自动或者贝塞尔曲线，一般自动或贝塞尔

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734608972764-d9dac497-4b5c-48d3-9f63-2144de0d920b.png)

使用贝塞尔模式时可以使用摇杆更自由地编辑曲线

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734609577689-1b1df210-2146-4cae-9dc8-92f4999abd51.png)

选中节点，点击Split可以断开节点

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734609876742-654e8222-4ccf-4276-ad4b-cd2c0a5f63ce.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734609922694-f20e5617-2755-4bf6-b81b-b8c4c35535f1.png)

**同一曲线**上断开的点可以通过join重新连接

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734609936907-e989c60e-1323-451f-aa3b-b654559911c4.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734609996479-e41a22b8-1fc3-4343-9ada-40cd6a4a7d3b.png)

**同一Spline组件**内的不同曲线可以通过Link功能建立连接，但生成mesh时还是不可避免会产生穿插问题，Unlink可以取消连接

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734610096306-e9eb0180-d0fc-4d7a-a406-0926e2996cf1.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734610107085-ddbef206-0ce9-4e48-a0e1-ac84be426f38.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734610133475-37b0dc38-8de7-477b-a287-4ccd076796f1.png)



### 样条线生成Mesh: 
Spline上添加脚本 LoftRoadBehaviour，会自动生成Mesh

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734665787197-80206a39-3636-48e5-b86e-d7f2bc230d38.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734665802753-88dc7d7e-6d57-405d-9c8d-ed609ec4db91.png)

MeshRenderer中替换材质（编辑中Ctrl Z撤销操作会把材质重置回默认）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734665821261-6622a29e-4b3e-46bf-bc6b-a0c167e79d83.png)

Mesh宽度调整：

Widths中点击加号创建节点宽度列表

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734666040000-2c9e89c6-bf32-4a65-821c-830053bdc754.png)

Default Value控制默认宽度

自定义节点宽度在Data Points中点加号添加元素控制

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734666230989-a9d87305-fcd4-4a7c-8af8-25a14ff2ea22.png)

控制模式可以选距离或者节点，一般选节点（Knot Index）

Data Points中Data Index与要控制的Spline节点（Knot）对应，Data Value是该节点的宽度，可以在这里输入，也可以在外部调整

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734666454399-e00c8529-3d30-438f-89ae-1091a4614122.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734666526814-08a6dd58-22fe-4b59-8ae3-1de752269b50.png)

外部调整时，选中Spline，进入Spline编辑模式，再点击下图红框位置进入节点宽度调整模式，只有在脚本Data points里添加过了的节点才能编辑

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734666631697-2cd9bbd7-99fd-47b4-b229-d2e345ecd3c8.png)

拖拽摇杆即可进行缩放

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1734666811074-0d6392b7-1975-4ef1-a5d3-5305f997efd8.png)

