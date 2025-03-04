

# **实现原理**
SSAO [https://learnopengl-cn.github.io/05%20Advanced%20Lighting/09%20SSAO/](https://learnopengl-cn.github.io/05%20Advanced%20Lighting/09%20SSAO/)

一种间接光照的模拟叫做**环境光遮蔽****(Ambient Occlusion)**，它的原理是通过将褶皱、孔洞和非常靠近的墙面变暗的方法近似模拟出间接光照。这些区域很大程度上是被周围的几何体遮蔽的，光线会很难流失，所以这些地方看起来会更暗一些。

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718697152040-8c336885-3967-4712-9bf7-d2d1b84c7b36.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718697155052-6446c885-a1ab-4c47-be00-d8d0d8be28b6.png)

主要计算该位置AO(环境遮挡因子)减弱环境光的影响

使用深度图重建世界坐标，重建法向，用随机噪声获取采样点，计算法向半球内像素对带当前位置AO(环境遮挡因子)的贡献

基于法线的双边滤波对AO降噪

滤波生成备用的AO贴图





# **资源消耗**
参数解释：

• **DownSample：**开启后采样数会从1降到0.5

• **AfterSample：**开启后会在不透明物体之后渲染，最终绘制到ColorRT上

### **RT****需求**
升级2021后的SSAO多了一张RT_SSAO_OcclusionTexture

| **名称** | **分辨率** | **格式** |
| :--- | :--- | :--- |
| _SSAO_OcclusionTexture1 | 全屏 | R8G8B8A8_UNorm |
| _SSAO_OcclusionTexture2 | 全屏 | R8G8B8A8_UNorm |
| _SSAO_OcclusionTexture3 | 全屏 | R8G8B8A8_UNorm |
| _SSAO_OcclusionTexture | 全屏 | R8G8B8A8_UNorm，不支持则为ARGB32 |


• **开启****AfterOpaque:**

SSAO在不透明物体之后渲染，会最后渲染到颜色rt上

| **名称** | **分辨率** | **格式** |
| :--- | :--- | :--- |
| _SSAO_OcclusionTexture1 | 全屏 | R8G8B8A8_UNorm |
| _SSAO_OcclusionTexture2 | 全屏 | R8G8B8A8_UNorm |
| _SSAO_OcclusionTexture3 | 全屏 | R8G8B8A8_UNorm |
| _SSAO_OcclusionTexture | 全屏 | R8G8B8A8_UNorm，不支持则为ARGB32 |
| _CameraColorAttachmentA | 全屏 | B10G11R11_UFloatPack32 |


### **DrawCall****需求**
| **名称** | **Pass** | **RT** |
| :--- | :--- | :--- |
| DC1 | SSAO_Occlusion | _CameraDepthTexture ->_SSAO_OcclusionTexture1 |
| DC2 | SSAO_HorizontalBlur  | _CameraDepthTexture,_SSAO_OcclusionTexture1->_SSAO_OcclusionTexture2 |
| DC3 | SSAO_VerticalBlur | _SSAO_OcclusionTexture2->_SSAO_OcclusionTexture3 |
| DC4 | SSAO_FinalBlur | _SSAO_OcclusionTexture3->_SSAO_OcclusionTexture |


• **开启****AfterOpaque:**

SSAO会DrawCall加一，但是去掉了PreDepthPass的渲染

| **名称** | **Pass** | **RT** |
| :--- | :--- | :--- |
| DC1 | SSAO_Occlusion | _CameraDepthTexture ->_SSAO_OcclusionTexture1 |
| DC2 | SSAO_HorizontalBlur  | _CameraDepthTexture,_SSAO_OcclusionTexture1->_SSAO_OcclusionTexture2 |
| DC3 | SSAO_VerticalBlur | _SSAO_OcclusionTexture2->_SSAO_OcclusionTexture3 |
| DC4 | SSAO_FinalBlur | _SSAO_OcclusionTexture3->_SSAO_OcclusionTexture |
| DC5 | SSAO_AfterOpaque | _SSAO_OcclusionTexture **->** _CameraColorAttachmentA |


### **复杂度**
| **Pass** | **Mali-G72(970)** | **Mali-G78(9000)** | **Bound** |
| :--- | :--- | --- | :--- |
| SSAO_Occlusion | 11.67 | 4.26 | A |
| SSAO_HorizontalBlur | 9.0 | 3.21 | T(14) |
| SSAO_VerticalBlur | 4.1 | 1.27 | T(5) |
| SSAO_FinalBlur | 3.93 | 1.19 | T(5) |
| SSAO_AfterOpaque | 1 |  | |






# **画面对比（包含帧率）**


| ![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696535430-8f3d4037-f249-4f7f-b951-d722e739dc78.png) | ![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696539394-0c9a7ee9-f935-4528-9976-2160adc51598.png) |
| :--- | :--- |


开SSAO

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696568146-998ecebe-ddab-45ae-971f-eceec0a861af.png)

关SSAO

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696570792-ac156c33-1f2c-4ac2-8163-50071f15654f.png)

### **参数对比：**
以主城为例，对比DownSample和AfterOpaque开启/关闭的情况

机型参数：小米11（2400*1080， 30fps， fsr:0.67）

• 30帧满帧

| 参数 | **DownSample****开启** | **DownSample****关闭** |
| :--- | :--- | :--- |
| **AfterOpaque****开启** | FPS：偶尔掉到28，基本维持在29<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696631818-104afb20-dc56-4e3d-b253-342b63ec68fb.png) | FPS：偶尔掉到28，基本维持在29<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696637418-44cf9b17-d9d8-4230-a45a-05c470ef0461.png) |
| **AfterOpaque****关闭** | FPS：基本维持在29<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696672022-5dbe9d0e-0dc6-4b9f-b2a5-724d8a739426.png) | FPS：基本维持在29<br/>![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696675505-ac3aea16-8fe0-4bad-9c0f-8c067f3c7abf.png) |


• 60帧满帧

****

****

**画面对比：**

• **SSAO****关闭****/****开启：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696716573-4915568a-aba3-4965-9483-9c7cec530785.png)





• **AfterOpaque开启/对比:**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696721419-4ca22b56-49e6-4949-be9f-bc4d9c6de99b.png)





• **DownSample开启/关闭（AfterOpaque已开启）：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696729546-bc20383a-877e-4df5-b823-20502cb61342.png)



# **帧率对比****(****测试场景****)**
### **小米****11**
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696911330-89691da1-d1eb-4411-8c60-8cd81b5d4430.png)

|  | FPS（1min,avg） | 帧率时间（ms） | 增加耗时（ms） |
| --- | :--- | :--- | --- |
| 默认 | 49.957 | 20 |  |
| ssao开 | 24.076 | 41.5 | 21.5 |
| 体积光开 | 34.273 | 29.1 | 9.1 |
| 全开 | 19.432 | 51.4 | 31.4 |


****

**SSAO参数耗时对比（主城夜晚）60FPS：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1718696920392-ec34bf37-851f-404b-afb9-e57b58f44b64.png)

关闭SSAO，平均帧率是36.90596，耗时是：27.10

| 参数 | **DownSample****开启** | **DownSample****关闭** |
| :--- | :--- | :--- |
| **AfterOpaque****开启** | FPS（1min, avg）：35.36373<br/>帧率时间（ms）：28.28<br/>增加耗时（ms）：1.18 | FPS（1min, avg）：32.48364<br/>帧率时间（ms）：30.78<br/>增加耗时（ms）：3.68 |
| **AfterOpaque****关闭** | FPS（1min, avg）：33.05249<br/>帧率时间（ms）：30.25<br/>增加耗时（ms）：3.15 | FPS（1min, avg）：31.81801<br/>帧率时间（ms）：31.43<br/>增加耗时（ms）：4.33 |


**结论：**

关闭SSAO帧率会增加，能提高到37帧左右；

开启SSAO后帧率下降，开启DownSample或AfterOpaque都能提升帧率，两个功能都开启效果更佳明显，接近35帧

****

****

**Depth Pre Pass耗时：**

|  | **关闭** | **开启** |
| --- | :--- | :--- |
| FPS（1min, avg） | 37.79826 | 36.21852 |
|  | 37.58 | 35.61887 |


**结论：**测试两组数据，关闭Depth Pre Pass和开启，相差约为1帧





### **索尼xz1**
|  | FPS（1min,avg） | 帧率时间（ms） | 增加耗时（ms） |
| --- | :--- | :--- | --- |
| 默认 | 56.596 | 17.6 |  |
| ssao开 | 8.328 | 120 | 102.4 |
| 体积光开 | 27.957 | 35.7 | 18.1 |
| 全开 | 7.134 | 140.1 | 122.5 |


**SSAO****参数耗时对比（主城夜晚）****60FPS****：**

关闭SSAO，平均帧率是23.23463， 耗时：43.04

| 参数 | **DownSample****开启** | **DownSample****关闭** |
| :--- | :--- | :--- |
| **AfterOpaque****开启** | FPS（1min, avg）：23.22287<br/>帧率时间（ms）：43.06<br/>增加耗时（ms）：0.02 | FPS（1min, avg）：22.22627<br/>帧率时间（ms）：45.00<br/>增加耗时（ms）：1.96 |
| **AfterOpaque****关闭** | FPS（1min, avg）：21.13873<br/>帧率时间（ms）：47.31<br/>增加耗时（ms）：4.27 | FPS（1min, avg）：20.47703<br/>帧率时间（ms）：48.84<br/>增加耗时（ms）：5.8 |


**结论：**

开启SSAO会掉帧增加耗时，从23到20帧左后，耗时增加约5.8ms

开启DownSample或AfterOpaque会提升帧率，两个都开启后帧率提升接近于关闭SSAO的帧率



**Depth Pre Pass耗时：**

|  | **关闭** | **开启** |
| --- | :--- | :--- |
| FPS（1min, avg） | 20.78989 | 19.5401 |
|  | 20.31298 | 19.04733 |


**结论：**测试两组数据，关闭Depth Pre Pass和开启，相差约为1帧

