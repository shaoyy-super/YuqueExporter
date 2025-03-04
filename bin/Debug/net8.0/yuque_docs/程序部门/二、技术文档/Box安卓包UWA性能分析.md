# 一、总体性能分析
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722498167433-2d7b1c53-912d-4814-9c15-042b66a50c4c.png)



## （一）程序上可以优化的主要有以下几个地方：
### 1、Animator.Initialize
我们的数据是 **156.93/千帧**，UWA建议是 **< 20/千帧 ；**

这个函数触发的时机是在含有**Animator**的组件**Active或Instantate**时触发，所以针对这个时机可以做以下几个优化：

+ 只是隐藏GameObject时，可以**disable** Animator组件，而不是直接SetActive
+ UI上能用Dotween实现的动画就尽量不要用Animator



当然目前项目这块还不属于性能瓶颈，优化后的效果不会对总体的性能造成很大的改变，只是作为可以优化的点，有必要的时候再考虑优化



### 2、Physics.Simulate
我们的数据是 **0.83ms**，UWA建议是 **< 0.4ms**（项目运行过程中Physics.Simulate每帧的平均CPU时间）

物理相关有以下几点可以优化

+ 如果项目没有物理模拟需求，可以直接关闭物理模拟，包括3D(Physics.simulationMode)和2D(Physics2D.simulationMode)，在各自模块需要的时候自己打开，离开模块的关闭

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722666853504-09b67402-f3b4-40ea-a0fc-424f86b8a883.png)

+ 打开ReuseCollisionCallbacks，这个是减少GC，因为碰撞信息会同步给一对碰撞体，Collision需要分配堆内存，打开这个选项后可以复用同一个对象
+ 合理定义碰撞层级 LayerCollisionMarix，不会碰撞的层级就取消勾选
+ 尽量使用基础的碰撞盒，优先使用的顺序：Box > Sphere > Caosule > 组合 > MeshCollider



![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722500597341-42163be6-5557-410a-9ad3-a44672669379.png)



关闭物理模拟后，对应的Physics.Simulate消耗就没了



![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722666695504-07b02e30-e46f-4c61-aa3d-afdc91fb5abc.png)



## (二)渲染相关数据
### 1、DrawCall峰值过高
我们数据 **780** UWA建议 < 700

### 2、UGUI DrawCall数过高
我们数据 **54** UWA建议 < 50

这里找了一个DrawCall较高的**战斗信息界面**做分析

通过Profiler看了战斗界面（战报信息展示），主要有以下几个问题：  
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722825002031-aa0789ec-37c3-41bb-b51f-4439638ed788.png)



1、ScrollView使用了Mask，替换成RectMak2D后战斗界面，从116降到46

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722825468543-51a81a8e-2a7b-4540-883b-12e21039e241.png)



![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722825055807-22c75efd-37d9-4dd0-bd22-5b7b06280d30.png)



2、每条战报信息预制体下也有遮罩，使用的是RectMask2D，然后ScrollView下，多个Item不同的RectMask2D区域无法合批，这里可以使用Mask，就能正常的合批，由46减到37



![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722839533450-498c11a4-b93e-45fe-9d78-5b5fe24ec92d.png)



3、战报PropBattleItemTemp预制体，里边的字体有的使用了TMP默认的字体材质，额外多了一个BatchCount

4、还有一些是遮挡关系，造成使用相同材质的文字，无法合批



除此之外，战报信息列表，没有复用逻辑，有多少战报数据，就创建多少PropBattleItemTemp实例



### 3、同屏渲染面片数过高
我们数据 **880690** UWA建议 < 800000

# 二、Lua性能分析
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722422434497-07db2f79-d523-4f6f-9f9b-7968f67f61d4.png)

Lua堆内存分配较高（每万帧堆内存分配超过10M）的函数



## 1、大量创建Table造成堆内存分配
 比如`Attribute:GeneratePlayerAttribute()`方法

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722424081806-8f5b27f9-019f-4596-9122-caebcd12b3e3.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722424093998-aab08542-ecc5-4e6f-b7fa-e1844fd6f97c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722424418301-e363c21f-ea68-4fbb-91e8-2464cdad7c3c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722424114467-279b8c4f-ad37-4a6b-a01e-2314e67b55db.png)

这里获取玩家属性，创建了大量的table用来合并玩家属性，比如等级属性、装备属性、宝石属性、角色卡属性、头像属性，每一个方法里还有一个table的遍历，会分配大量的堆内存

建议是优化一下MergeAttribute方法，不要创建额外的table，直接把src_attr合并到tar_attr即可



## 2、在lua里不合理的注册Update事件，并在Update里调用C#
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722424947487-950d4cf6-69dc-4c5e-84ea-40e1de2f47c4.png)

这两个都是在Update里刷新transform属性，完全可以使用tween代替。



## 3、lua里协程的创建，属于正常的调用，只是内存分配排名比较靠前


# 三、Mono性能分析
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722495799993-c30c151e-bc8f-4164-b843-70b4a514eaaf.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722497866127-a70d76a2-ece8-45ed-be7f-db18ab9f024e.png)



## 1、SetupCoroutine.InvokeMoveNext
主要是预制体实例化

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722497536263-6a604b74-5960-4ab7-9ffc-0aa0f3d150d8.png)



## 2、NetMgr.Update
主要是日志打印造成堆内存分配

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722497353190-50cf9162-8e40-4878-88cb-9eeb3d667bb9.png)



## 3、[Thread] ThreadHelper.ThreadStart
主要也是日志打印

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722497746019-0d24c829-54b0-46a5-8b47-9a48c5aa8f24.png)



其他的都是正常的实例化、日志打印、列表刷新、Canvas重绘造成的内存分配

# 四、GPU性能分析


![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1722495663867-f3129d1a-e6d3-4458-afba-d64ba1f350fd.png)

主要是两个性能指标需要优化

##   
1、<font style="color:rgb(46, 56, 71);">Fragment shaded</font>
## <font style="color:rgb(46, 56, 71);">2、OverDraw</font>


