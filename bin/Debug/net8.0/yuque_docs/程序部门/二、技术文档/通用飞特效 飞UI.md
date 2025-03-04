# 一、简介
内容：UI节点飞向UI节点，3D物体飞向3D物体、3D物体飞向UI节点、3D飞3D（UI映射的点）

目的：将项目中会出现的飞特效、飞金币等表现功能进行封装，简化使用



# 二、结构设计
内容：

新增C#脚本 <font style="color:#d0d0d0;background-color:#262626;">MovePathUtil </font>提供移动功能接口

:::tips
**1.UI飞UI功能:**

**2.3D物体飞向3D物体功能：**

**3.3D物体飞向UI节点**

以上3种移动方式支持的功能:

		1.position 到 position 固定点移动

  2.transform 到 transform移动（移动过程中 起点或者终点的Transform有变化的话，需要移动的transform也会追踪该变化）

		以上两点支持路径移动和直线运动 （移动过程支持Ease曲线，现支持4种曲线，如有新需求可额外   提出）

**4.动态镜头3D物体飞向UI节点**

	注:随着相机运动，原来在视野中的3D物体已经不在相机视野中了，该3D物体需要飞向固定的UI节点

	支持的功能：

		同上

:::

# 三、详细结构设计
**在 MovePathUtil脚本中使用以下方法**

**方法1：****<font style="color:#d0d0d0;background-color:#262626;">StartSimpleMove</font>**

		3d到3d移动，UI到UI移动

**1.1 Position -- Position**

```csharp
/// <param name="origin">需要移动的物体</param>
/// <param name="start">起点position</param>
/// <param name="end">终点position</param>
/// <param name="time">移动的时间</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartSimpleMove(Transform origin, Vector3 start, Vector3 end, 
                            float time, Action moveFinish = null)
    
```

**1.2 Transform -- Transform**

```csharp
/// <param name="origin">需要移动的物体</param>
/// <param name="start">起点transform</param>
/// <param name="end">终点transform</param>
/// <param name="time">移动的时间</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartSimpleMove(Transform origin, Transform start, Transform end,
                                float time, Action moveFinish = null)
        
```



1.3 新增需求 Position -- 屏幕中心 UI移动到屏幕中心

```csharp
/// <param name="origin">需要移动的物体</param>
/// <param name="start">起点的世界坐标</param>
/// <param name="time">移动事件</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartSimpleMove(Transform origin, Vector3 start, 
                            float time, Action moveFinish = null)
        
```





**方法2：****<font style="color:#d0d0d0;background-color:#262626;">StartW2UMove</font>**

		3D物体移动到UI位置

:::color4
<font style="color:#DF2A3F;">注意：如果UI使用相机渲染，需要传入相应的UICamera，如果UI使用Overlay方式 UICamera可为null</font>

:::

**2.1 Position -- Position**

```csharp
/// <param name="uiCamera">ui相机</param>
/// <param name="origin">需要移动的3D物体</param>
/// <param name="start">起始点</param>
/// <param name="end">终点UI</param>
/// <param name="time">移动时间</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartW2UMove(Camera uiCamera, Transform origin, Vector3 start, Vector3 end, 
                         float time,Action moveFinish = null)
        
```

**2.2 Transform -- Transform**

```csharp
/// <param name="uiCamera">ui相机</param>
/// <param name="origin">需要移动的3D物体</param>
/// <param name="start">起始点</param>
/// <param name="end">终点UI</param>
/// <param name="time">移动时间</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartW2UMove(Camera uiCamera, Transform origin, Transform start, Transform end, 
                         float time, Action moveFinish = null)
        
```



**方法3：****<font style="color:#d0d0d0;background-color:#262626;">StartW2UMoveSpecial</font>**

 3D相机在运动的过程中 3d物体移动到UI

:::color4
<font style="color:#DF2A3F;">注意：如果UI使用相机渲染，需要传入相应的UICamera，如果UI使用Overlay方式 UICamera可为null</font>

:::

**3.1 Position -- Position**

```csharp
/// <param name="moveCamera">在运动的相机</param>
/// <param name="uiCamera">ui相机</param>
/// <param name="origin">需要移动的3D物体</param>
/// <param name="start">起始点</param>
/// <param name="end">终点UI</param>
/// <param name="time">移动时间</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartW2UMoveSpecial(Camera moveCamera, Camera uiCamera, 
        Transform origin, Vector3 start, Vector3 end, float time, 
                                Action moveFinish = null)   

```

**3.2 Transform -- Transform**

```csharp
/// <param name="moveCamera">在运动的相机</param>
/// <param name="uiCamera">ui相机</param>
/// <param name="origin">需要移动的3D物体</param>
/// <param name="start">起始点</param>
/// <param name="end">终点UI</param>
/// <param name="time">移动时间</param>
/// <param name="moveFinish">移动结束事件</param>
public void StartW2UMoveSpecial(Camera moveCamera, Camera uiCamera,Transform origin,
                                Transform start, Transform end, float time, 
                                Action moveFinish = null)
```



# 四、流程设计
使用流程

1.在需要使用路径移动的功能模块处添加组件 MovePathUtil

![](https://cdn.nlark.com/yuque/0/2025/png/51144106/1739764259351-7a8e0e50-4490-4c58-8dc1-4fd139eda1ed.png)2.移动类型选择

 MoveType.Line：起始点线性移动，此类型下可以不创建路径

MoveType.Path：起始路径移动，在此类型下需要编辑一条路径模板

3.Eaae曲线，暂时只是以下四种，

InOutSine

InOutCubic

InQuad

OutCubic

当EaseType=Custom时会使用AnimCurve来进行缓动

当EaseType=None时 不进行缓动



**如果使用Path模式 需要Hierachy面板下创建一条贝塞尔曲线**

![](https://cdn.nlark.com/yuque/0/2024/png/51144106/1734521531231-a35f8e6d-1689-4ef4-bc0f-69342aad3f88.png)

![](https://cdn.nlark.com/yuque/0/2024/png/51144106/1734521967160-ab5a3f58-1078-440f-bc97-fba7e9da6d13.png)可自行根据需求编辑路径

注意1： 第一个节点的localposition需要是0，0，0；

![](https://cdn.nlark.com/yuque/0/2024/png/51144106/1734522065010-2428fb09-6ebc-4ff2-9c12-5abd6ef550e5.png)注意2： 如果是UI到UI的移动，保持路径在xy平面上，即 所有节点的z坐标为0

![](https://cdn.nlark.com/yuque/0/2024/png/51144106/1734522179582-6558aae6-5b35-444d-ba26-8f065877822e.png)最后将该路径拖至MovePathUtil的Path中

![](https://cdn.nlark.com/yuque/0/2025/png/51144106/1739449003798-5201cc26-8053-43fa-be3f-2dcd1b99173f.png)

# 五、提出问题
如有额外需求，请另外提出。

---

# 六、总结整理
目前支持4大类型移动 UI——UI，3D——3D，3D——UI，3D——UI动态

每一类型支持两种移动方式，单点移动方式，路径移动方式

