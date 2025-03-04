# 简介
**DT（Deformation Transfer）**是一个比较具有一般性的网格变形迁移算法，不要**source mesh**和**target mesh**之间有相同的顶点数量和相同的连通性

**原文链接**：[https://people.csail.mit.edu/sumner/research/deftransfer/Sumner2004DTF.pdf](https://people.csail.mit.edu/sumner/research/deftransfer/Sumner2004DTF.pdf)

**基础概念：**

1. **拓扑一致**：如果两个模型的**点的数量**，**点序**，以及**连通性**是一致的，那么可以称为拓扑一致
2. **BlendShape（混合形变）**: 一个模型在保证拓扑一致的情况下，点的位置改变后所产生的新的模型

**DT算法的优点：**

1. 利用顶点标记来构建目标关系
2. 求解线性
3. 速度比较快（目前算法350组blendshape大约需要一个小时）

**数据准备：**

1. 要有一个**source mesh**和**target mesh**，source和target之间可以拓扑不一致
2. 一系列基于**source mesh**的变形体，变形体和source之间拓扑必须一致

**工作流程：**

1. 为**source mesh**和**target mesh**之间建立手动关联关系，即选择一部分的两两匹配的**语义顶点**
2. 构建**source mesh**和**target mesh**之间面的**correspondence mapping**,并保存下来
3. 利用**correspondence mapping**来计算**target mesh**的变形体

![](https://cdn.nlark.com/yuque/0/2025/png/46024715/1739954770641-12648bfb-b431-4ca2-9ef9-2262caa30244.png)

# Deformation Transfer原理
Deformation Transfer的目标是将source mesh的变形传递到target mesh上，我们将source mesh的deformation表示为每一个三角形的affine transformation（因为每一个affine transformation包含了一个三角形方向比例和偏斜的变化）：

## Affine Transformation（仿射变换）
**平面上的仿射变换：**

$ \begin{bmatrix}  
X\\  
Y  
\end{bmatrix} = A
\begin{bmatrix}  
x\\  
y  
\end{bmatrix} + 
\begin{bmatrix}  
t_x\\  
t_y  
\end{bmatrix}  $$ |A| \neq 0 $

**仿射变换的齐次表示：（线性形式）**

$ \begin{bmatrix}  
X\\  
Y\\
1
\end{bmatrix} =
\begin{bmatrix}  
A & b\\  
0 & 1  
\end{bmatrix}
\begin{bmatrix}  
x\\  
y\\
1
\end{bmatrix}  $

仿射变换的线性表示意味着我们可以在更高的维度通过线性变换来完成低维度的仿射变换

## 构建三角形的仿射变换
由于三个点无法完全构建一个三角形的仿射变换，要引入第四个点

$ v_4 = v_1 + \frac{(v_2 - v_1) \times (v_3 - v_1)}{\sqrt{|(v_2 - v_1) \times (v_3 - v_1)|}} $

仿射变换

$ Qv_i + d = \widetilde{v_i} $

如果我们减去第一行，可以消去$ d $ ，可以得到$ QV = \widetilde{V} $

$ \widetilde{V} = [\widetilde{v_2} - \widetilde{v_1} \quad \widetilde{v_3} - \widetilde{v_1} \quad \widetilde{v_4} - \widetilde{v_1}]
 $

$ V = [v_2 - v_1 \quad v_3 - v_1 \quad v_4 - v_1] $

最终我们可以得到如下形式

$ Q = \widetilde{V}V^{-1} $

## DT的优化目标
有了上面得到的形式以后，我们可以构建**source mesh**的**Deformation**:$ S_1,\ldots,S_{|s|} $

![](https://cdn.nlark.com/yuque/0/2025/png/46024715/1739958293756-b6170b79-530c-4f21-be5d-e25f723f79fd.png)

如果假设我们有一个source和target之间三角形的对应关系

$ M = \{(s_1,t_1),(s_2,t_2),\ldots,(s_{|M|},t_{|M|})\} $

这里的$ s_i $和$ t_i $分别对应着在一个对应关系$ M $中所代表的**source mesh**和**target mesh**的**deformation**

这里我们可以直接将$ s_i $应用到**target mesh**上么？

**答案是不能！！**

**如果我们直接这么做了，会导致面片是相互分离的状态，因为这其中有一个非常隐含的约束，那就是每一个三角面上存在共享的点，我们必须要保证共享的点位置是一样的**

### 构建目标函数
也就是说，我们除了要求source的变形和target的变形尽可能的一致以外，我们还要求对于三角形**共享的顶点**，位置是一致的

**最终target function：**

$ \begin{aligned}
\min _{\mathbf{T}_1+\mathbf{d}_1 \ldots \mathbf{T}_{|T|}+\mathbf{d}_{|T|}} \quad & \sum_{j=1}^{|M|}\left\|\mathbf{S}_{s_j}-\mathbf{T}_{t_j}\right\|_F^2 \\
\text { subject to } & \mathbf{T}_j \mathbf{v}_i+\mathbf{d}_j=\mathbf{T}_k \mathbf{v}_i+\mathbf{d}_k, \quad \forall i, \forall j, k \in p\left(v_i\right) .
\end{aligned} $

 其中$ p\left(v_i\right) $代表包含点$ v_i $的所有三角形

### 目标函数形式的优化
对于上面的目标函数，我们是可以通过**二次规划**来去求解的，但是求解的难度大，并且耗时长，我们可以通过转化为线性的方式来优化，**这是整个方法中最重要的地方**

对于target mesh：

$ T = \widetilde{V}V^{-1} $

为了方便看，我们把$ \widetilde{V} $写成$ U $

$ T = UV^{-1} $

$ U = [u_2 - u_1,u_3 - u_1,u_4 - u_1] $

我们把$ u_{4x} - u_1 $简写成$ u_4 $

$ UV^{-1} = 
\begin{bmatrix}  
u_{2x} - u_{1x} & u_{3x} - u_{1x} & u_{4x} \\
u_{2y} - u_{1y} & u_{3y} - u_{1y} & u_{4y} \\
u_{2y} - u_{1z} & u_{3z} - u_{1z} & u_{4z}
\end{bmatrix}
\begin{bmatrix}  
v_{11} & v_{21} & v_{31} \\
v_{21} & v_{22} & v_{32} \\
v_{31} & v_{23} & v_{33}
\end{bmatrix} $

我们看这个结果的第一行（同理其第二和第三行），经过简化以后：

$ \begin{bmatrix}  
-(v_{11} + v_{21} + v_{31})u_{1x} + v_{11}u_{2x} + v_{21}u_{3x} + v_{31}u_{4x}\\
-(v_{11} + v_{21} + v_{31})u_{1y} + v_{11}u_{2y} + v_{21}u_{3y} + v_{31}u_{4y}\\
-(v_{11} + v_{21} + v_{31})u_{1z} + v_{11}u_{2z} + v_{21}u_{3z} + v_{31}u_{4z}\\
\end{bmatrix}^T $

观察后我们提取矩阵

$ span =
\begin{bmatrix}  
-(v_{11} + v_{21} + v_{31}) & v_{11} & v_{21} & v_{31}\\
-(v_{12} + v_{22} + v_{32}) & v_{12} & v_{22} & v_{32}\\
-(v_{13} + v_{23} + v_{33}) & v_{13} & v_{23} & v_{33}\\
\end{bmatrix} $

最终$ ||S_{s_j}-T_{t_j}||_F^2 $优化结果可以写成：

$ \begin{bmatrix}  
span & 0 & 0\\
0 & span & 0\\
0 & 0 & span\\
\end{bmatrix}
\begin{bmatrix}  
u_{1x}\\
u_{2x}\\
u_{3x}\\
u_{4x}\\
\ldots \\
u_{1z}\\
u_{2z}\\
u_{3z}\\
u_{4z}\\
\end{bmatrix} =
\begin{bmatrix}  
r_{1x}\\
r_{2x}\\
r_{3x}\\
r_{4x}\\
\ldots \\
r_{1z}\\
r_{2z}\\
r_{3z}\\
r_{4z}\\
\end{bmatrix} $

这里就直接转换成了线性的方式，**而且直接包含了点的约束！**

### 优化后的目标函数
$ \min _{\tilde{\mathbf{v}}_1 \ldots \tilde{\mathbf{v}}_n}\|\mathbf{c}-\mathbf{A} \tilde{\mathbf{x}}\|_2^2 $

## 建立correspondence
建立correspondence的关键就是我们要想办法把target mesh给变形成source mesh的样子

### 可选方案
建立correspondence的方式有很多，例如：

#### [Non-Rigid Registration](https://www.cs.cmu.edu/~galeotti/methods_course/DeformableRegistration.pdf)
#### [Laplacian Mesh Editing](https://people.eecs.berkeley.edu/~jrs/meshpapers/SCOLARS.pdf)
但是Non-rigid Registration的速度过慢，而且如果source的点密度不高结果并不一定好，比较适用于**匹配点云**  
Laplacian Mesh Editing在整体结构上可以做的比较好，但是在局部不一定理想**不能完整的充分考虑mesh的结构**

### 基于先验语义mapping的mesh deformation
#### 先验语义的标注
先验语义的意思是用人工的方式做一些标记一些**语义点**，在source和target之间做标记，这些标记点被指定为**源和目标顶点索引的配对**。每对表示源顶点在变形后应与目标顶点的位置相匹配，这些标记作为约束条件被强制执行在最小化过程中

**关于语义点选取的注意事项**

1. 语义点的标记尽量覆盖不同的部位
2. 语义点不用非常多，但是要尽量精准
3. 要充分考虑到模型的布线

### 构建优化目标
优化目标分为三项：**Deformation Smoothess**，**Deformation Identity**，**Closest Valid Point **

#### Deformation Smoothess
Smoothess项尽量保证变形的平滑性，让与其临近的三角形尽量保证是相同的deformation

$ E_S\left(\mathbf{v}_1 \ldots \mathbf{v}_n\right)=\sum_{i=1}^{|T|} \sum_{j \in \operatorname{adj}(i)}\left\|\mathbf{T}_i-\mathbf{T}_j\right\|_F^2 $

#### Deformation Identify
Identify项可以看作是正则项，让deformation不要过度平滑

$ E_I\left(\mathbf{v}_1 \ldots \mathbf{v}_n\right)=\sum_{i=1}^{|T|}\left\|\mathbf{T}_i-\mathbf{I}\right\|_F^2 $

#### Closest Valid Point
Closest Valid Point项用于约束语义点的位置

$ E_C\left(\mathbf{v}_1 \ldots \mathbf{v}_n, \mathbf{c}_1 \ldots \mathbf{c}_n\right)=\sum_{i=1}^n\left\|\mathbf{v}_i-\mathbf{c}_i\right\|^2 $

**什么情况下是Valid Point**

比较**source mesh**的顶点法线与**target mesh**的三角形法线，法线方向差异**小于 90°** 表示是一个Valid Point

#### 最终优化目标
$ \begin{gathered}
\min _{\tilde{\mathbf{v}}_1 \ldots \tilde{\mathbf{v}}_n} E\left(\mathbf{v}_1 \ldots \mathbf{v}_n, \mathbf{c}_1 \ldots \mathbf{c}_n\right)=w_S E_S+w_I E_I+w_C E_C\\
\text { subject to } \quad
\tilde{\mathbf{v_{sk}}}=\mathbf{m}_k, \quad k \in 1 \ldots m
\end{gathered} $

同样，这也是一个线性的

### 最终利用icp构建面的对应关系
在我的算法中，最终选择了双向icp，**因为双向icp可以保证在一边面比较稀疏的情况下，稀疏的一边可以同时考虑到更多面的影响**

![](https://cdn.nlark.com/yuque/0/2025/png/46024715/1739960415006-1ea99946-8fd6-4563-babd-da621a6b9fbf.png)

# 在项目中Blendshape上应用的诸多问题和解决办法
## 问题
### Mesh点的输出结果不能匹配原始位置
![](https://cdn.nlark.com/yuque/0/2025/png/46024715/1739960715881-96d817d1-5142-4730-bb16-379796b010e6.png)

### Mesh结构并不连续
在我们的模型中，Mesh的结构并不是一个连续的，相当于一个Mesh实际包含几个分割出来的组件：头部，眼皮，牙，睫毛，舌头

![](https://cdn.nlark.com/yuque/0/2025/png/46024715/1739960737104-b219921e-2c4a-473b-b887-5cb325240a64.png)

但是这些组件相互之间存在一定的关联性，例如眼皮需要贴着眼睛，睫毛需要贴在眼睛上部，牙齿和舌头在口腔内部

### 存在固定点
固定点有两种情况：

#### 全局固定的点
例如后脑勺的点，要被固定住，不能移动，外边缘线的点要被固定住不能移动

#### 不同表情下存在不同需要固定的点
例如张嘴的时候眼睛的点可能是不动的

### 模型结构存在缺陷
嘴角的模型结构存在

![](https://cdn.nlark.com/yuque/0/2025/png/46024715/1739960801754-324807c4-187f-4572-8894-b5c471aa6d85.png)

## 解决思路
对于Mesh点的输出结果不能匹配原始位置的问题，和Mesh中存在固定点的问题，这两个问题可以合并，在最终目标函数中加上固定点的约束：  
对于不同表情下需要被固定的点，由于我们只有面的关联关系，并没有点的关联关系，因此我们有两种选择：

1. 利用icp重新构建点的关联关系
2. 利用先验的语义点来构建约束（我的程序中用的方法）

最终我们可以得到固定点集合$ P
 $来调整我们的优化函数：

$ \min _{\tilde{\mathbf{v}}_1 \ldots \tilde{\mathbf{v}}_n}\|\mathbf{c}-\mathbf{A} \tilde{\mathbf{x}}\|_2^2 + \sum_{i=1}^m\left\|\mathbf{v}_i-\mathbf{p}_i\right\|^2 $

对于组件关联性的问题，我们可以主动构建这种关联性，同样通过选点的方式，构建relationship集合，同时修改优化函数：

$ \min _{\tilde{\mathbf{v}}_1 \ldots \tilde{\mathbf{v}}_n}\|\mathbf{c}-\mathbf{A} \tilde{\mathbf{x}}\|_2^2 + \sum_{i=1}^m\left\|\mathbf{v}_i-\mathbf{p}_i\right\|^2 + \sum_{j \in \operatorname{relation}(i)}\left\|\mathbf{v}_i-\mathbf{v}_{j}\right\|^2 $

**但是对于口腔内上下牙和舌头，我们无法找到关联性的点，只能通过固定点的方式来解决！**

**对于模型结构存在缺陷的问题：目前只能通过修改原始模型来解决！**



