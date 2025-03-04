# 问题
项目中存在大批量的植被等场景物体，GPUInstance单批次有最高数量限制，而SRP Batcher在处理巨量相同网格物体时表现不好，所以需要一种能处理巨量植被的方案。

我们先梳理一下面对的一些问题：

+ 场景GameObject包括了TransForm，MeshFilter，MeshRenderer三个组件，序列化属性比较多，面对巨量植被时，会占用较大的内存。
+ 同屏下植被数量太多，需要去除冗余，譬如视锥外的，被遮挡的以及远距离的。而这些包括LOD都涉及到了大量的计算，unity对这部分的处理都是在cpu完成的，在面对大量物体时其效率愈发低效，会占用大量cpu的时间并拉低帧率。

# 思路
1. 将场景物体分类并单独保存为序列化文件，只保留必要的一些数据，剔除冗余，从而减少内存占用。（这时候还可以进行分块处理，但我们项目场景都不是很大，可以将其整个当作一个块来看待）
2. 利用GPU并行计算的特性，用compute shader完成视锥剔除，遮挡剔除，LOD等复杂计算。需要注意的是，要避免数据回读，即数据从GPU传入CPU，会造成带宽阻塞。既然不能回读，那只能直接draw出来了，数量级在这，DrawMeshInstancedIndirects是个不错的选择，这就是GPU Driven。

# 实现
## 数据序列化
### 单位植被
为了让保存出的分块数据尽可能小，单位植被仅会保留渲染必要的一些数据，经过考量，最终保留了，position：float3，rotation：float3，scale：float，总共7个float，28字节，百万数量级也只需要26M的内存，其数据结构为：

```plain
public struct VegetationTransform
{
    public Vector3 postion;
    public Vector3 rotation;
    public float scale;
}
```

### 分块数据
<font style="color:rgb(25, 27, 31);">对于分块数据中的一种特定植被，通过VegtationPrefab定义，其中，VegtationLod记录该植被的lod层级对应的mesh、material等相关资源。</font>

```plain
public class VegetationPrefab
{
    public int _VegType;//0:common, 1:seagrass
    public uint _CastShadow;
    public VegetationLod[] _VegetationLodGroup;
    public VegetationTransform[] _ItemArray;
    // public Matrix4x4[] _ItemMatrixArray;
    public Bounds _BlockVegetationBounds;
}
```

```plain
public class VegetationLod
{
    public Mesh _LodMesh;
    public Material _LodMat;
    [Range(0, 1)] public float _ViewDisRatio;
    [HideInInspector] public Bounds _LodBounds;
}
```

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729151005651-512c0906-b21f-4d27-a186-183df47515d4.png)

## GPU裁剪和渲染
### 概念介绍
+ DrawMeshInstancedIndirect：用于渲染大量网格实例的函数，参数部分需要准备：
    - 网格数据：包括了顶点数据，索引数据，材质属性。
    - 实例数据：包括了实例的仿射变换矩阵。
    - 间接绘制数据：包括了实例数量，顶点数量等数据，存于Argsbuffer中。

```plain
Graphics.DrawMeshInstancedIndirect(
            info.mesh, 				//要画的mesh
            0, 						//要画的mesh的，submesh索引
            info.mat, 				//要使用什么材质求画
            info.mesh.bounds,		//画的东西的包围盒
            info.argBuffer,			//间接绘制数据buffer
            info.argOffset,			//这个是参数buffer的偏移
            info.materialPropertyBlock,				//画的材质的参数
            ShadowCastingMode.Off,	//释放阴影，也可以释放
            false,					//不接受阴影，也可以接受
            0,						//所属的层，是哪个层级
            null);					//为null，则画所有的相机
```

+   Argsbuffer：下方是单个预制的ArgsBuffer结构，主要记录了mesh索引数，实例数量，索引起始，索引便宜，实例偏移：

```plain
// LOD00
m_args[argsIndex + 0] = _LodMesh0.GetIndexCount(0);        // 0 - submesh的索引数 
m_args[argsIndex + 1] = 0;                                 // 1 - instance数量
m_args[argsIndex + 2] = 0;                                 // 2 - submesh的起始索引位置
m_args[argsIndex + 3] = 0;                                 // 3 - submesh的索引偏移值
m_args[argsIndex + 4] = 0;                                 // 4 - instance偏移值
// LOD1
m_args[argsIndex + 5] = _LodMesh1.GetIndexCount(0);         
m_args[argsIndex + 6] = 0;                                 
m_args[argsIndex + 7] = 0;                                 
m_args[argsIndex + 8] = 0;                                 
m_args[argsIndex + 9] = 0;                                 
// LOD
m_args[argsIndex + 10] = _LodMesh2.GetIndexCount(0);        
m_args[argsIndex + 11] = 0;                                
m_args[argsIndex + 12] = 0;                                
m_args[argsIndex + 13] = 0;                                
m_args[argsIndex + 14] = 0;             
```

### 初版
#### 流程总览
1. 剔除准备
2. 根据导入的序列化数据计算M矩阵（只初始化时执行一次）
3. 视锥剔除+distance剔除(这里借用了lod group cull)，得到剔除过后的矩阵StructuredBuffer
4. 将剔除过后的矩阵StructuredBuffer传入Vertex Shader，根据InstanceID获取M矩阵进行顶点计算
5. 使用DrawMeshInstancedIndirect函数进行渲染

#### 思路
这是一套基础的GPU Driven流程，先准备好矩阵等相关数据，然后传入compute shader进行GPU剔除，再将剔除过后的数据传入shader，最后调用DrawMeshInstancedIndirect函数渲染实例。

#### 步骤实现
##### 剔除准备
在脚本初始化时，需要准备一系列的参数。

如obj的transform属性，mesh的包围盒，lod的viewRatio，每帧的主相机VP矩阵等。

如sortingData，其记录了自身index，obj所属类型index，以及距相机的距离。

```plain
for (int j = 0; j < vegBlockData[i]._ItemArray.Length; j++)
{
    sortingData.Add(new SortingData()
    {
        drawCallInstanceIndex = ((((uint)i * NUMBER_OF_ARGS_PER_INSTANCE_TYPE) << 16) + ((uint) m_numberOfInstances)),
        distanceToCam = Vector3.Distance(vegBlockData[i]._ItemArray[j].postion, m_CamPosition)
    });
    m_numberOfInstances++;
}
```

##### M矩阵计算
在shader内构建M矩阵的耗费太大，所以需要提前在computeshader中构建，然后再传入shader直接使用。

之前我们序列化了单位植被的position，rotation，scale其中scale是个float，即为统一缩放系数，这样做一是为了节省内存占用；二是因为非统一缩放在normal localToWorld时需要用到逆M矩阵，也就是说，我们还得再向shader传入一份逆矩阵RT，还需对其进行采样获取矩阵，这样的话，消耗直接多了一倍。

该步骤只会在初始化的时候执行一次。

##### GPU Culling
剔除工作和lod计算都会在这一步完成，一个个来说。

+ 视锥剔除

原理是判断物体是否在相机视锥范围之内，判断方法有不少，这里选用了包围盒八个顶点分别与齐次空间进行判断。

```plain
inline uint IsVisibleAfterFrustumCulling(float4 clipPos)
{
    return (clipPos.z > clipPos.w 
            || clipPos.x < -clipPos.w 
            || clipPos.x > clipPos.w 
            || clipPos.y < -clipPos.w 
            || clipPos.y > clipPos.w) 
            ? 0 : 1;
}
```

+ Distance剔除

根据包围盒求出和相机的最小距离minDistanceToCamere，这里不需要那么精确，八个顶点距离对比一下获取最小的那个即可；然后和Lod Group Cull比较判断是否可见。

##### Shader
将矩阵StructuredBuffer传入vertex shader，根据InstanceID获取对应M矩阵进行计算。

```plain
StructuredBuffer<float4x4> objectToWorldMatrixBuffer;

v2f vert(appdata_full v, uint instanceID : SV_InstanceID)
{
    float4x4 matrix = objectToWorldMatrixBuffer[instanceID];
}
```

#### 问题
+ StructuredBuffer在移动端部分机型上vertex shader不支持，所以后续需要改为贴图来存矩阵信息。
+ 在GPU并行剔除后，将可视的矩阵通过append函数添入buffer，这时因并行计算，矩阵顺序会乱，instanceID无法与矩阵信息对应起来，所以该方法只能适用于单个drawcall。而一旦涉及LOD，单个模型会出现3个drawcall，所以无法直接使用该方法来处理。若每个LOD都用一个buffer来存储的话，那每个模型都需要申请三个矩阵buffer，且需每个模型都走一遍这套流程，这会导致很严重的计算和buffer冗余。

### 二版
1. 剔除准备
2. 根据导入的序列化数据计算M矩阵（只初始化时执行一次）
3. 视锥剔除+distance剔除(这里借用了lod group cull)，得到剔除过后的矩阵，通过InterlockedAdd函数对instance count进行累加，同时它也起到了计数器的作用，可使得通过的矩阵根据计数器index来存储入贴图中。
4. 将剔除过后的矩阵StructuredBuffer传入Vertex Shader，根据InstanceID获取M矩阵进行顶点计算
5. 使用DrawMeshInstancedIndirect函数进行渲染

#### 思路
这一版用贴图替换ssbo，使得其能兼容所有的移动端平台，矩阵最后一行可以忽略，实际所需存储的数据为12个float，为了减少在vertex shader对矩阵贴图的采样，单个矩阵需要在保证精度的情况下尽量减少对像素的占用，所以最后采用了float贴图进行存储，这时只需在vertex shader中采样三次即可获取对应的M矩阵。

#### 步骤实现
##### GPU Culling
这一版只有GPU Culiing这一步骤有所变化，剔除部分的计算不变，在存储前首先需要知道矩阵存储的贴图位置，这个位置由通过线程的index决定，该index可通过InterlockedAdd函数来获取，该函数可以对instance count进行累加，同时其也起到了计数器的作用。贴图的值为0-1，所以需要先对矩阵float进行映射，然后再根据index存入矩阵贴图。

```plain
float EncodeIntRGBAFloat(uint i)
{
    float val = float((i & 0xFFFFFFFF) / 4294967295.0);
    return val;
}

uint index;
InterlockedAdd(_ArgsBuffer[1], 1, index); // 获取当前索引并自增
//转换成xy
posID.x = index % 512;
posID.y = index / 512;
_VegDataMap[posID] = EncodeIntRGBAFloat(index); // 使用获取的索引写入数据
```

### 三版
#### 流程总览
1. 剔除准备
2. 根据导入的序列化数据计算M矩阵（只初始化时执行一次）
3. 视锥剔除+distance剔除(这里借用了lod group cull)+LOD计算，得出visibility数组
4. 对visibility数组进行并行reduction计算和并行prefix sum计算
5. 筛选出可见index，重新排序，将对应的矩阵写入矩阵RT
6. 将InstanceID偏移值写入IDOffset RT
7. 根据距相机距离远近进行双调排序（为了下一帧LOD的选取）

注：阴影部分的处理在步骤3时用了不同的剔除方案，然后则是重复了步骤4，5，6，后面不再单独说。

#### 难点思路
可能会有疑问为什么要写的这么复杂，毕竟已经在前3步的时候得到了矩阵信息，可见性，LOD层级，那是否可以直接输出呢？显然不是不行的，因为GPU读不懂。

DrawMeshInstanceIndiect必须要传入ArgsBuffer实例数据这个参数，这个buffer的参数构建已经在上面写了，我们需要填充每个lod的instance count以及start instance index，有了这些信息，就能构建出所有可见物体的连续性instanceID；若这时ObjectToWorldMatrixBuffer的结构也与其对应，那instanceID就可以作为index来获取其对应的矩阵。

而要将ObjectToWorldMatrixBuffer梳理成下方的结构，一是需要剔除掉数组中不可见的元素（步骤4），二是将元素根据距相机距离从近到远进行排列（步骤7）。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729134862369-bacd5cdd-fa0f-4621-8e70-43c8c2088e3a.png)

#### 步骤实现
##### GPU Culling
这一步剔除部分不变，增添了LOD的计算，也不再直接输出剔除过后的矩阵，代替的是输出visibility buffer，不可见为0，可见为1。而实际的剔除操作会在步骤5完成。

+ 计算LOD

这里LOD分级计算没有直接暴力的给定一个数值，而是走了LOD Group那套屏占比的算法。LOD分级的屏占比数值是预存的，这个数值可以直接转换成物体距离相机的距离进行计算。计算原理是简单的三角函数。如下图，若GameObject的size=View Distance，则viewRatio为1，若GameObject在视锥内size为0，则viewRatio为0。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729075258761-68b53b74-4e6b-43cf-9bc6-8311412e9bf5.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729169414693-c2d2d866-10c3-4e6c-b969-936dde56dd7e.png)

```plain
float3 boundSize2 = boundMax2 - boundMin2;
float maxSize = max(max(boundSize2.x, boundSize2.y), boundSize2.z);
float camAngle = radians(_CamFieldOfView * 0.5);
float camRatio = maxSize * 0.5 / tan(camAngle);

float lod0Range = camRatio / lodsViewRatio.lod0Range;
float lod1Range = camRatio / lodsViewRatio.lod1Range;
float lod2Range = camRatio / lodsViewRatio.lod2Range;
```

+ 信息存入buffer

至此，我们得到了可见性判断，存入_IsVisibleBuffer，不可见存0，可见存1；InterlockedAdd函数对instance count进行累加，存入_ArgsBuffer，但该buffer中lod有三层，我们需要给对应lod的instance count+1，所以需要计算一下该lod对应的argsIndex，代码为

```plain
uint argsIndex = drawCall + 1;
argsIndex += 5 * (minDistanceToCamera > lod0Range);
argsIndex += 5 * (minDistanceToCamera > lod1Range);
```

```plain
InterlockedAdd(_ArgsBuffer[argsIndex], isVisible);
```

+ 阴影

阴影的绘制考虑到会涉及屏幕外的物体，所以只用了distance剔除，distance值会读取QualitySetting配置的

shadowDistance参数。

##### 并行Reduction和Prefix Sum计算
这时我们已经获得了_IsVisibleBuffer，它的内部结构，举个例子是0，0，1，1，0，1...，这样的一数组，可以通过前缀和来计算每个1对应的index。这些数据会被分到不同Group中进行计算，获得某个物体的index时，应该是这样的一个计算：

```plain
index = GroupPrefixSum + GroupThreadPrefixSum
```

GroupThreadPrefixSum：对Group内数据做前缀和

GroupPrefixSum：计算每个Group的总和，再对所有Group做前缀和

###### Reduction归约
对N个数值<font style="color:rgb(25, 27, 31);">求其总和这一操作，在CPU很好做，循环遍历一边即可，但现在是在GPU上，需要利用多线程来完成这计算。并行计算的话，其计算方式要与顺序无关，对此有一个朴素解法。</font>

<font style="color:rgb(25, 27, 31);">我们从叶子节点到根节点进行遍历计算树的内部节点的部分和，根节点保存数组中所有节点的和。如下图所示。</font>

![](https://cdn.nlark.com/yuque/0/2024/jpeg/46064633/1732079644939-d104477a-9068-4bd1-bf3a-6e3f5f350f04.jpeg)

```plain
int tID = (int) _dispatchThreadID.x;
int groupTID = (int) _groupThreadID.x;
int offset = 1;
temp[2 * groupTID] = _IsVisibleBuffer[2 * tID];
temp[2 * groupTID + 1] = _IsVisibleBuffer[2 * tID + 1];
int d;
const int NoofElements = 2 * THREAD_GROUP_SIZE_X;
//reduction
for (d = NoofElements >> 1; d > 0; d >>= 1)
{
    GroupMemoryBarrierWithGroupSync();
    if (groupTID < d)
    {
        int ai = offset * (2 * groupTID + 1) - 1;
        int bi = offset * (2 * groupTID + 2) - 1;
        temp[bi] += temp[ai];
    }
    offset *= 2;
}
```

###### Prefix Sum前缀和
先定义一下这个问题，<font style="color:rgb(25, 27, 31);">输入一个数组input[n], 计算新数组output[n], 使得对于任意元素output[i]都满足</font>

<font style="color:rgb(25, 27, 31);">output[i] = input[0] + input[1] + ... input[i-1]</font>

| 输入 | 0 | 1 | 0 | 0 | 1 | 0 |
| --- | --- | --- | --- | --- | --- | --- |
| 输出 | 0 | 0 | 1 | 1 | 1 | 2 |


该问题每个元素的计算都依赖之前的值，我们需要解决如何并行。

大概思路为每一片段的最后存储上一个片段的数值和，然后将上一个片段的数值和和该片段的数值和相加得到前缀和。

实际做法为，将从根节点开始向下遍历， 使用归约阶段的部分和数组上进行向上扫描。我们首先在树的根部插入0，在每一步中，当前层次的每个节点将其自己的值传递给其左子节点，其值与其左子树的前值之和传递给其右子节点。

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1730971965552-f01bf4f3-2482-4bc1-86f9-3adf25071f08.png)

```plain
//prefix sum
for (d = 1; d < _NumOfGroups; d *= 2)
{
    offset >>= 1;

    GroupMemoryBarrierWithGroupSync();

    if (tID < d)
    {
        int ai = offset * (2 * groupTID + 1) - 1;
        int bi = offset * (2 * groupTID + 2) - 1;
        int t = temp[ai];
        temp[ai] = temp[bi];
        temp[bi] += t;
    }
}
```

[https://www.cnblogs.com/Ligo-Z/p/16254628.html](https://www.cnblogs.com/Ligo-Z/p/16254628.html)

[https://developer.nvidia.com/gpugems/gpugems3/part-vi-gpu-computing/chapter-39-parallel-prefix-sum-scan-cuda](https://developer.nvidia.com/gpugems/gpugems3/part-vi-gpu-computing/chapter-39-parallel-prefix-sum-scan-cuda)

##### 重构Index和矩阵输出
通过上一步，我们获得了GroupPrefixSum和GroupThreadPrefixSum，两者相加之下即可得出所有可见物体的index，该index是连续的，这样我们就可以重建出一个以该index为下标的可见矩阵buffer，从而剔除掉冗余矩阵信息；若是用StructuredBuffer来存重构矩阵的话，就可以极大缩小Buffer的大小；RT存储形式的话，也可以因此省去index RT，但存矩阵的RT是提前申请的，其大小没法缩小了。

为了减少后面在shader中的采样次数，RT是RGBA_Float精度的贴图，矩阵最后一行没什么信息，可以不用存，这样我们只需存3个像素共12个float数据。

当然像素是0-1的值，我们还需将数据压缩一下才能存进去，后面再在shader中解压。

```plain
float4 EncodeFloatRGBAFloat(float4 v)
{
    uint4 i = asuint(v);
    float4 val = (i & 0xFFFFFFFF) / 4294967295.0;
    return val;
}

float4 DecodeFloatRGBAFloat(float4 rgba)
{
    uint4 value = uint4(rgba * 4294967295.0);
    return asfloat(value);
}
```

##### InstanceID偏移输出
我们在之前申请了argsbuffer，一个dc对应了一个args，每个args都需记录相应的instance count以及偏移值，由此才能组合出最终的InstanceID。

这就要说到vulkan和opengles在<font style="color:rgb(64, 64, 64);">DrawMeshInstancedIndirect函数对InstanceID处理上的差异了；对于走同一个ArgsBuffer所有实例，vulkan输出的InstanceID会自动加上偏移值，我们在shader中获取到的instanceID就是连续的；但opengles不会，你需要手动在shader中加上这个偏移值才行。</font>

##### <font style="color:rgb(64, 64, 64);">双调排序</font>
先了解下双调序列，<font style="color:rgb(51, 51, 51);">双调序列是一个先单调递增后单调递减（或者先单调递减后单调递增）的序列。</font>

<font style="color:rgb(51, 51, 51);">再了解下Batcher定理，</font><font style="color:rgb(51, 51, 51);">将任意一个长为2n的双调序列A分为等长的两半X和Y，将X中的元素与Y中的元素一一按原序比较，即ai与ai+n比较，将较大者放入MAX序列，较小者放入MIN序列。则得到的MAX和MIN序列仍然是双调序列，并且MAX序列中的任意一个元素不小于MIN序列中的任意一个元素2。</font>

<font style="color:rgb(51, 51, 51);">双调排序，则是对一个双调序列，根据Batcher定理划分成2个双调序列，然后继续对每个双调序列递归划分，得到更短的双调序列，直到得到的子序列长度为1为止。这时的输出序列按单调递增顺序排列。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729157626615-159d6ab7-1ba3-4ec1-b26f-b000ee5810fd.png)

双调序列怎么排序了解了，那如何将一个任意序列变成一个双调序列呢？

<font style="color:rgb(51, 51, 51);">这个过程叫Bitonic merge, 实际上也是divide and conquer的思路。 和前面sort的思路正相反， 是一个bottom up的过程——将两个相邻的，单调性相反的单调序列看作一个双调序列， 每次将这两个相邻的，单调性相反的单调序列merge生成一个新的双调序列， 然后排序（同3、双调排序）。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729157605878-44c91496-4681-463e-a47e-383a4ee8a467.png)

```plain
for (uint j = _Level >> 1; j > 0; j >>= 1)
{
    SortingData result = _SharedData[GI];
    uint t1 = GI & ~j;
    uint t2 = GI | j;
    SortingData inst1 = _SharedData[t1];//较小值
    SortingData inst2 = _SharedData[t2];//较大值
    
    float dist1 = (inst1.drawCallInstanceIndex >> 16) * 5000 + inst1.distanceToCam;
    float dist2 = (inst2.drawCallInstanceIndex >> 16) * 5000 + inst2.distanceToCam;
    if ((dist1 <= dist2) == ((bool)(_LevelMask & tID)))//满足升序列小于，降序列大于
    {
        result = _SharedData[GI ^ j];//互换数值
    }
    GroupMemoryBarrierWithGroupSync();
    _SharedData[GI] = result;
    GroupMemoryBarrierWithGroupSync();
    _Data[tID] = _SharedData[GI];
}
```

但<font style="color:rgb(51, 51, 51);">这样的双调排序算法只能应付长度为2的幂的数组。我这边选择拓展了原函数，使其长度符合2次幂，再进行排序。</font>

<font style="color:rgb(51, 51, 51);">不过这种方法会使用额外的空间，有些浪费。</font>

##### <font style="color:rgb(51, 51, 51);">Shader</font>
资源已经处理完了，剩下的就是shader如何获取矩阵信息。在shader中通过instanceID转uv采样矩阵贴图，采样三个像素，并对三个color进行解压还原。

```plain
float4x4 GetUTMatrixM(uint instanceID)
{
	float2 uv;
	float4 row1, row2, row3;
#if defined(SHADER_API_METAL) || defined(SHADER_API_VULKAN)	
	uint index = instanceID;
#else
	uv.x = (_DrawCallIndex % _InstanceIDOffsetMapRes) / _InstanceIDOffsetMapRes;
	uv.y = (_DrawCallIndex / _InstanceIDOffsetMapRes) / _InstanceIDOffsetMapRes;
	uint index = instanceID + DecodeIntRGBAFloat(SAMPLE_TEXTURE2D_LOD(_InstanceIDOffsetMap, sampler_InstanceIDOffsetMap, uv, 0).r);
#endif

	uv.x = (index * 3 % _VegDataMapRes) / _VegDataMapRes;
	uv.y = (index * 3 / _VegDataMapRes) / _VegDataMapRes;
	row1 = DecodeFloatRGBAFloat(SAMPLE_TEXTURE2D_LOD(_VegDataMap, sampler_VegDataMap, uv, 0));
		
	uv.x = ((index * 3 + 1) % _VegDataMapRes) / _VegDataMapRes;
	uv.y = ((index * 3 + 1) / _VegDataMapRes) / _VegDataMapRes;
	row2 = DecodeFloatRGBAFloat(SAMPLE_TEXTURE2D_LOD(_VegDataMap, sampler_VegDataMap, uv, 0));
		
	uv.x = ((index * 3 + 2) % _VegDataMapRes) / _VegDataMapRes;
	uv.y = ((index * 3 + 2) / _VegDataMapRes) / _VegDataMapRes;
	row3 = DecodeFloatRGBAFloat(SAMPLE_TEXTURE2D_LOD(_VegDataMap, sampler_VegDataMap, uv, 0));
		
	float4x4 objToWorldMatrix = float4x4(row1, row2, row3, float4(0,0,0,1));
	return objToWorldMatrix;
}
```

我建立了UT-SpaceTransforms.hlsl，里面提供了一些空间转换函数，适用于可能要走GPU Instance的shader替换unity原生的空间转换函数，其能在GPU Instance和正常状态切换。

# <font style="color:rgb(51, 51, 51);">性能</font>
之前做过一版DrawMeshInstanceIndirect和SRP Batcher之间的性能对比。

## 测试环境
机型：华为mate20pro

GPU：Mali-G76 OpenGLES 3.2

分辨率：1560*720

## 测试内容
SRPBatcher vs DrawMeshInstanceIndirect

数量级：6498

Frustum Culling：开

LOD Culling：开

LOD Level：关

## 测试结果
![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729165341095-b2e49b60-06ac-4a94-8649-6fa00298cb67.png)

SRP Batcher

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1725528029396-67a74ac5-3e09-415d-b263-7c63b3e054eb.png)

DrawMeshInstanceIndirect

![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1725528051162-e3f85fcd-78fb-44df-8e18-126c4406242c.png)

真机帧率从40到61，有了显著的提升，其余各方面的数据也有不错的优化。

# 效果
![](https://cdn.nlark.com/yuque/0/2024/png/46064633/1729159847681-3355169e-c06a-471f-bcaf-3a1f5c10da1b.png)

# 其他思路
除Texture外，还考虑过SSBO，UBO和Vertex Attribute的方案，接下来会说一下思路和各自的特点。

## SSBO
1. ssbo存储大小可以达到128mb
2. ssbo具有可变存储
3. ssbo访问速度比cbuffer慢
4. 对移动端致命的一点在于，大部分opengles3.0和mali处理器，它们的vertex shader不支持SSBO

这里有一份StructuredBuffer的兼容性测试报告：[https://zhuanlan.zhihu.com/p/68888365](https://zhuanlan.zhihu.com/p/68888365)

## UBO
1. UBO大小限制一般为64KB或128KB
2. UBO需声明大小

一个矩阵16个float，以64KB来算，最多只能存1024个矩阵，而项目同屏植被可能远超这个数量限制，所以不适合。

## Vertex Attribute
**VAO(顶点数组对象)：**VAO有一个属性列表，可以为属性指定数据，其中属性可以是顶点位置，颜色，法线，uv等数据，其中的每一个属性对应的数据就是VBO。

**VBO(顶点缓冲对象)：**顶点缓冲数据对象VBO是在显卡存储空间中开辟出的一块内存缓存区，用于存储顶点的各类属性信息，如顶点坐标，顶点法向量，顶点颜色等。

这个方案需要涉及源码的修改，需要重新bind buffer，技术限制，无法实现，这里主要是说下大概思路。

离线生成一个mesh，这个mesh用于存储所有的矩阵信息，之后叫它meshA。mesh中的uv3，uv4，uv5属性指向新的buffer，该buffer存储的矩阵数据由解析meshA获得。后面可以直接在shader中，通过uv3，uv4，uv5来获取矩阵信息，这样能省去贴图方案的3次采样。

# 参考资料
[https://developer.nvidia.com/gpugems/gpugems3/part-vi-gpu-computing/chapter-39-parallel-prefix-sum-scan-cuda](https://developer.nvidia.com/gpugems/gpugems3/part-vi-gpu-computing/chapter-39-parallel-prefix-sum-scan-cuda)

[https://zhuanlan.zhihu.com/p/430878486](https://zhuanlan.zhihu.com/p/430878486)

[https://cloud.tencent.com/developer/article/1381182](https://cloud.tencent.com/developer/article/1381182)

[https://zhuanlan.zhihu.com/p/416959273](https://zhuanlan.zhihu.com/p/416959273)

[https://research.nvidia.com/sites/default/files/pubs/2016-03_Single-pass-Parallel-Prefix/nvr-2016-002.pdf](https://research.nvidia.com/sites/default/files/pubs/2016-03_Single-pass-Parallel-Prefix/nvr-2016-002.pdf)



