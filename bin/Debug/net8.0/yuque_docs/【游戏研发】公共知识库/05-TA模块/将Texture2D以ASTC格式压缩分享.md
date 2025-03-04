本次分享是根据[ASTC格式剖析及其GPU编码实现 - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/659509891)该文章里的工程实现的

可以看完本篇文章再看看这篇文章[Compute ASTC | niedap’s notes (niepp.github.io)](https://niepp.github.io/2021/12/18/Compute-ASTC.html)，就几分钟能读完，但是能对ASTC有更好更深更专业的理解。

# 压缩原理
### 概念理解
<font style="color:rgb(25, 27, 31);">不管是ASTC_4x4还是ASTC_12x12，每个ASTC block都是128bits。</font>

<font style="color:rgb(25, 27, 31);">比如这是一张原图2048*2048，导入unity设置sizemax为32*32大小且设置压缩格式为ASTC8*8的图，可以看出他被分为了4*4（32/8）的块里，每个块有8*8个像素，</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727690846917-1df07711-9b19-4951-89b1-aa974b2b70c6.png)

<font style="color:rgb(25, 27, 31);">单个压缩块的大小是固定的 128 bits(16bytes)。 所以最后图片的大小为（4*4块）16*16bytes=256bytes。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727688262970-da7be2e0-6e7b-406d-80ea-32c3433fc1ab.png)



<font style="color:rgb(25, 27, 31);">因此ASTC_4x4是8bpp(</font>[<font style="color:rgb(25, 27, 31);">bits per pixel</font>](https://zhida.zhihu.com/search?content_id=234686071&content_type=Article&match_order=1&q=bits+per+pixel&zhida_source=entity)<font style="color:rgb(25, 27, 31);">)[128/(4x4)=8]，ASTC_12x12则是0.89bpp[128/(12x12)=0.89]。</font>

<font style="color:rgb(25, 27, 31);"></font>

<font style="color:rgb(25, 27, 31);">在128bits里分为元数据，颜色端点，权重网格，三大部分。图像长这样</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728731660189-63abe56a-e947-4fb0-8d66-6d05e3c144fa.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728728792595-00a0b4bf-0bda-4bad-aa1a-751bac06666c.png)

<u>关于元数据及ASTC格式编码规则的定义规则可以看</u>[<u>ASTC格式解析 - 知乎 (zhihu.com)</u>](https://zhuanlan.zhihu.com/p/673380622)<u>这篇文章；</u>

我们逐个把它拆解看，先讲比较容易懂的

`Color Endpoints`（颜色端点）和 `Weight Grid`（权重网格）。

他们的所占的bits长度和位置不是固定的。先直观展示下他们两个是什么样子的。<font style="color:rgb(25, 27, 31);">可以看出他被分为了4*4（32/8）的块里，每个块有8*8个像素。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727690846917-1df07711-9b19-4951-89b1-aa974b2b70c6.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727690820559-df982b4b-927f-45b4-bcb7-205b919d1715.png)

而一个块里有两个属性`Color Endpoints`（颜色端点）和 `Weight Grid`（权重网格）。

所以我们可以看到每个块里就只有两种颜色混合的样子。每个块又由更小的格子（权重网格）分开，每个权重网格里就只有一种颜色（截屏位置没对好，所以格子里不是纯色，实际是纯色），可以看出块和块的分界线比较明确，但是块里因为Weight Grid所以比较柔和。



这个是32*32大小的图片以ASTC4*4的方式压缩后的块和Weight Grid（权重网格）的样子。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727691330240-8cdcb0bf-431f-49ad-aac8-04b6f6f1f799.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727691340673-cf2b1125-bcb8-4ee6-9476-76a225c24052.png)

<font style="color:rgb(25, 27, 31);">Weight Grid（</font>权重网格）其实不一定等于<font style="color:rgb(25, 27, 31);">Block Size（</font>块）的大小，也可以小于<font style="color:rgb(25, 27, 31);">Block Size（</font>块）的大小的，通过双线性插值来拟合成<font style="color:rgb(25, 27, 31);">Block Size（</font>块）大小就行。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728031090320-ee7f310c-4592-44fb-a248-c547d1416b61.png)

**那****Color Endpoints（颜色端点）是怎么计算的，什么样子的呢。**

Color Endpoints（颜色端点）计算的方式有很多，我们找一个最简单的例子来说。所以的颜色都可以用rgb来表示，rgb是三个通道，也可以认为是三个维度，只要把当前块的所有颜色以点的形式都放在这个三维空间里，然后找出一条离所有点最近的一条线段，在把线段的两个端点记下来，就能得出Color Endpoints（颜色端点）了。



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728030613839-2eb044c0-2eb7-4e10-a8e1-9d9990cc77f9.png)

其他方法如下

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728033023696-70e71fd4-1a17-4007-8461-765dcc33b961.png)

上面说的Color Endpoints（颜色端点）是三维的情况。<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">如果像素的rgb通道中有任何通道要进行分开编码就要有多套endpoints和权重网格，就像是normal。如果要加上A通道则多一个维度来计算，或者再用一套endpoints和权重网格来储存信息。个人认为A通道只要多一个维度来计算就行了，不需要单独的一套endpoints和权重网格来存储信息。不过也有不好的地方就是看上去更糙了。</font>

<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);"></font>

**元数据**

<font style="color:rgb(25, 27, 31);">前11个bits称为Block Mode。主要声明以下信息：</font>

    - <font style="color:rgb(25, 27, 31);">Weight Grid的尺寸。（4x4;5x5;6x6 ...）</font>
    - <font style="color:rgb(25, 27, 31);">Weight Grid中每个元素的取值范围，称为Weight Range。</font>
    - <font style="color:rgb(25, 27, 31);">是否是Dual Plane，Dual Plane的话会包含两组Weight Grid。</font>[<font style="color:rgb(25, 27, 31);">双平面</font>](https://zhida.zhihu.com/search?content_id=234686071&content_type=Article&match_order=1&q=%E5%8F%8C%E5%B9%B3%E9%9D%A2&zhida_source=entity)<font style="color:rgb(25, 27, 31);">主要用于处理某个通道和其他通道的数据不存在相关性，不适合用同一个权重值来表示的情况。在ASTC中，是可以指定哪个通道使用单独的Weight Grid的，并不一定非得是</font>[<font style="color:rgb(25, 27, 31);">Alpha通道</font>](https://zhida.zhihu.com/search?content_id=234686071&content_type=Article&match_order=1&q=Alpha%E9%80%9A%E9%81%93&zhida_source=entity)<font style="color:rgb(25, 27, 31);">。</font>

<font style="color:rgb(25, 27, 31);">前11个bits的</font>声明方法如下

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729672510716-aea6a9c2-8db4-4713-af52-3bf6f37685d9.png)

<font style="color:rgb(145, 150, 161);">A：weight range encodings</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729672522582-cbb9f5e7-fef9-4581-a98f-1dafe6d5d4a7.png)

<font style="color:rgb(145, 150, 161);">B: 2D block mode layout, weight grid width and height</font>

<font style="color:rgb(145, 150, 161);"></font>

<font style="color:rgb(25, 27, 31);">ASTC的Bits[11, 12]表示Partition数量，在没有开启Dual Plane的情况下，最多允许4个Partition，开了Dual Plane由于Weight Grid占用太多内存，最多只允许3个Partition。在我们的实时ASTC压缩实现，只会有1个Partition，因为编码多个Partition涉及的计算非常复杂，不可能做到实时。</font>

<font style="color:rgb(25, 27, 31);">纯黑和纯白是插值不出红色的，可以得出：unity压缩有分区的作用可以存在2个以上颜色端点。</font>

自己压缩							原图						Unity压缩

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063607778-0c30d353-2ca9-4f2e-abc3-9a990ab5b3f6.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063622068-bcdec7f1-db15-4abf-ac9b-2df40f84abee.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063635846-d16c25b0-60d1-46fe-aad0-5a02488ec4fe.png)

+ <font style="color:rgb(25, 27, 31);">1个Partition的话，将和BC1一样只包含2个color endpoints，N个Partition则会包含2*N个color endpoints。如果包含多个Partition，在我们解码Block内某个像素的值时，除了需要获取其Weight，还需要</font>**<font style="color:rgb(25, 27, 31);">计算</font>**<font style="color:rgb(25, 27, 31);">当前像素使用的到底是哪个Partition。之所以说</font>**<font style="color:rgb(25, 27, 31);">计算</font>**<font style="color:rgb(25, 27, 31);">，是因为Partition和像素的对应关系并没有存储在Block数据中，而是需要通过一个</font>[<font style="color:rgb(25, 27, 31);">Hash函数</font>](https://zhida.zhihu.com/search?content_id=234686071&content_type=Article&match_order=1&q=Hash%E5%87%BD%E6%95%B0&zhida_source=entity)<font style="color:rgb(25, 27, 31);">来计算这个对应关系：</font>`<font style="color:rgb(25, 27, 31);background-color:rgb(248, 248, 250);">Hash(PixelCoord, ParitionIndex, PartitionCount)</font>`<font style="color:rgb(25, 27, 31);">。 如果PartitionCount大于1，那么Block的Bits[13, 22]将存储Hash函数中用到的</font>`<font style="color:rgb(25, 27, 31);background-color:rgb(248, 248, 250);">PartitionIndex</font>`<font style="color:rgb(25, 27, 31);">。下面是ASTC_8x8格式中，2、3、4个Partition在不同PartitionIndex下的分布</font>

<font style="color:rgb(25, 27, 31);">（来源：</font>[<font style="color:#117CEE;">https://github.com/ARM-software/astc-encoder/blob/main/Docs/FormatOverview.md</font>](https://link.zhihu.com/?target=https%3A//github.com/ARM-software/astc-encoder/blob/main/Docs/FormatOverview.md)<font style="color:rgb(25, 27, 31);">）</font>

![](https://cdn.nlark.com/yuque/0/2024/webp/39137189/1728730531027-e15bd764-17db-4853-bdee-505417255503.webp)

最后CEM就是声明颜色是使用哪种模式<font style="color:rgb(25, 27, 31);">（Color Endpoint Mode，简称CEM）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729672483531-238d6d78-76fd-4c34-8239-89042aedbe45.png)



接下来就是怎么把原贴图的数据存储为ASTC格式了。

### 开始压缩！![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727693358471-3d1a5a60-1989-4141-9eb3-5a078a8c146b.png)
压缩步骤：

+ 将图片进行分块划分
+ <font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">计算每个</font><font style="color:rgb(25, 27, 31);">Block</font><font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">的endpoints</font>
+ <font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">endpoints量化，BISE编码</font>
+ <font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">将划分里的像素表示为endpoints的插值，计算插值权重weights。</font>
+ <font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">weights量化，BISE编码</font>
+ <font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">组装为128bit块</font>

 用ps的原因是原作者说<font style="color:rgb(55, 58, 64);">用ps是因为有的安卓机cs性能很差。</font>

<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">我们以Textureformat为ASTC4X4为例；</font>

<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">因为是4X4，定义PIXEL_COUNT_1D=4，PIXEL_COUNT_2D=4X4；</font>

<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">首先将图片分块，如一张32X32的图片，（每个像素用正方形框住了，每个正方形里就是一个像素，不是纯色是因为有双线性差值的原因）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728106656804-42cc8c6b-ca20-4788-97c3-f606e4dacb03.png)

先将采样的UV进行偏移，这样每个格就有4X4的像素了

然后再分别**对每个块进行采样**，将采样的值放在float3 Block[PIXEL_COUNT_2D]的数组里。

```properties
void ReadBlockRGB(Texture2D<float4> SourceTexture, SamplerState TextureSampler, int mipLevel, float2 UV, float2 TexelUVSize, out float3 Block[PIXEL_COUNT_2D])
{
    [unroll]
    for (int y = 0; y < 4; ++y)
    {
        [unroll]
        for (int x = 0; x < 4; ++x)
        {
            Block[4 * y + x] = SourceTexture.SampleLevel(TextureSampler, UV + float2(x, y) * TexelUVSize, mipLevel).rgb;        
        }
    }
}
```

分块后就要求Color Endpoints（颜色端点）了，在这里使用的方法是通过for循环遍历数组里的每个值，比较出RGB中最大值和最小值，作为Color Endpoints（颜色端点）；

```properties
void GetBlockMinMax(in float3 Block[PIXEL_COUNT_2D], out float3 OutMin, out float3 OutMax)
{
    OutMin = Block[0];
    OutMax = Block[0];
    //比较每个像素的RGB最小值
    for (int i = 1; i < PIXEL_COUNT_2D; ++i)
    {
        OutMin = min(OutMin, Block[i]);
        OutMax = max(OutMax, Block[i]);
    }
}
```

这里的操作是将Color Endpoints（颜色端点）进行量化操作，<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">量化就是用尽可能少的bit位去表示给定的数据。</font>

```properties
#if _COMPRESS_ASTC5x5
    #define ColorEndPointRange 63  // RGB666
#elif _COMPRESS_ASTC6x6
    #define ColorEndPointRange 79  // 4 Bits + 1 Quint(2^4*5)
#else
    #define ColorEndPointRange 255 // RGB888
#endif

#if _COMPRESS_ASTC6x6
    // 6x6由于有quint，需要特殊处理: https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#astc-endpoint-unquantization
    uint3 MinColorUint = (uint3)round(saturate(MinColor) * 255);
    uint3 MaxColorUint = (uint3)round(saturate(MaxColor) * 255);
    MinColorUint = uint3(color_quant_table[MinColorUint.x], color_quant_table[MinColorUint.y], color_quant_table[MinColorUint.z]);
    MaxColorUint = uint3(color_quant_table[MaxColorUint.x], color_quant_table[MaxColorUint.y], color_quant_table[MaxColorUint.z]);

    MinColor = round(saturate(MinColor) * ColorEndPointRange) / ColorEndPointRange;
    MaxColor = round(saturate(MaxColor) * ColorEndPointRange) / ColorEndPointRange;
#else
    uint3 MinColorUint = (uint3)round(saturate(MinColor) * ColorEndPointRange);
    uint3 MaxColorUint = (uint3)round(saturate(MaxColor) * ColorEndPointRange);
    MinColor = (float3)MinColorUint / ColorEndPointRange;
    MaxColor = (float3)MaxColorUint / ColorEndPointRange;
#endif
```

接下来就得把每个块的这些数据按照规范填入128Bits里了，先看ASTC4X4的编码中各个bits的含义

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729652654725-5cb9b7da-c477-40cc-a691-5c0d95596e68.png)

Blits[0,16]为元数据

Blits[17，64]为Color Endpoints（颜色端点）

Blits[80,127]为Weight Grid（权重网格）

元数据我们是固定的，<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">将</font>Color Endpoints（颜色端点）压缩到Blits[17，64]

```properties
 // ASTC 4x4
 uint4 PackedBlock = uint4(0x00010053, 0, 0, 0); 
 PackedBlock.x |= (MinColorUint.r << 17); 
 PackedBlock.x |= (MaxColorUint.r << 25); 
 PackedBlock.y |= (MaxColorUint.r >> 7);
 
 PackedBlock.y |= (MinColorUint.g << 1);
 PackedBlock.y |= (MaxColorUint.g << 9); 
 PackedBlock.y |= (MinColorUint.b << 17); 
 
 PackedBlock.y |= (MaxColorUint.b << 25);
 PackedBlock.z |= (MaxColorUint.b >> 7);
```

uint4 PackedBlock长这样子:

   x:  00000000 00000001 00000000 01010011

   y:  00000000 00000000 00000000 00000000

   z:  00000000  00000000 00000000 00000000

   w:  00000000 00000000 00000000 00000000





00000000 00000000 00000000 00000000

填充后长这样子

![](https://cdn.nlark.com/yuque/0/2024/jpeg/39137189/1729653211494-ffd9e479-5ca1-49e3-9eac-57066a967a35.jpeg)

通过位移和位操作，将颜色信息从 MinColorUint 和 MaxColorUint 打包到 PackedBlock 中。



最后就是对<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">权重weights计算插值，并且</font>压缩到Blits[80，127]了。

（这一步的操作就是将16个像素颜色范围通过从mincolor到maxcolor的范围通过点乘映射到到0~7的范围，小数点部分四舍五入，并且分别把每个权重网格的数据放在（0-7=2*3）3Bits里，最后从127bits倒过来开始填入权重网格信息）

```properties
#if _COMPRESS_ASTC6x6
static const uint REV_BITS[4] = { 0, 2, 1, 3 };
#define SCALE_RANGE 3
#define SCALE_RANGE_F 3.0
#else
static const uint REV_BITS[8] = { 0, 4, 2, 6, 1, 5, 3, 7 };
#define SCALE_RANGE 7
#define SCALE_RANGE_F 7.0
#endif

float3 Range = MaxColor - MinColor;
float Scale = SCALE_RANGE_F / max(1e-5, dot(Range, Range)); // 正常的最小值是(1/255.0)^2，约等于1.53e-5
float3 ScaledRange = Range * Scale;
float Bias = (dot(MinColor, MinColor) - dot(MaxColor, MinColor)) * Scale;
[unroll]
for (int i = 0; i < PIXEL_COUNT_2D; ++i)
{
    // Compute the distance index for this element
    uint Index = clamp(round(dot(Block[i], ScaledRange) + Bias), 0, SCALE_RANGE);

    uint bitRevIndex = REV_BITS[Index];

    if (i < 10)
    {
        // [96 + 2, 128) 
        PackedBlock.w |= (bitRevIndex << (29 - i * 3));
    }
    else if (i == 10)
    {
        // [95, 96 + 2)
        PackedBlock.w |= (bitRevIndex >> 1);
        PackedBlock.z |= (bitRevIndex << 31);
    }
    else if (i < 21)
    {
        // [64+1, 64+28+3)
        PackedBlock.z |= (bitRevIndex << (28 - (i - 11) * 3));
    }
    else if (i == 21)
    {
        // [62, 65)
        PackedBlock.z |= (bitRevIndex >> 2);
        PackedBlock.y |= (bitRevIndex << 30);
    }
    else
    {
        // [53, 62) -> [32 + 21, 32 + 27 + 3)
        PackedBlock.y |= (bitRevIndex << (27 - (i - 22) * 3));
    }
```

Bits[0,16]为元数据【17Bits】

Bits[17，64]为Color Endpoints（颜色端点）【2*R8G8B8=48Bits】

Bits[80,127]为Weight Grid（权重网格）【4*4*3=48Bits】

这样17+48+48=113Bits 还有15bits没信息

最后输出PackedBlock里面就是我们压缩后的ASTC纹理了。

### BISE方法
但是如果使用ASTC5X5的话，17+2*8*3+5*5*3=140bits>128bits,位数不够了，使用要将里面的颜色端点或者权重网格进行压缩，ASTC采用的是BISE方法压缩。

具体压缩逻辑可以看这个文章介绍：[Compute ASTC | niedap’s notes](https://niepp.github.io/2021/12/18/Compute-ASTC.html)



BISE编码可以抄这个

[UEViewer/libs/astc/astc_integer_sequence.cpp at master · gildor2/UEViewer · GitHub](https://github.com/gildor2/UEViewer/blob/master/libs/astc/astc_integer_sequence.cpp)



工程里的ASTC5*5采用的是将颜色端点使用<font style="color:rgb(25, 27, 31);">R6G6B6</font>的方式储存，然后解码的时候映射到<font style="color:rgb(25, 27, 31);">R8G8B8。</font>但是也可以<font style="color:rgb(25, 27, 31);">ASTC规定的BISE规则用</font><u><font style="color:rgb(25, 27, 31);">Base3</font></u><font style="color:rgb(25, 27, 31);">将其编码</font>储存颜色端点。如将颜色端点<font style="color:rgb(25, 27, 31);">编码成QUANT192</font>具体逻辑就是

将5个8位数分为后面的6位为低位，前面的2位为高位，低位正常存储，高位则是只考虑00，01，10三种情况，只有5个高位数的组合就只有3<sup>5</sup>=243种情况，比2<sup>8</sup>=256小，只有原来要10bits来表示高位，现在只要8bits表示了。我们也可以使用在权重网格上，下列就是<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">最优的endpoints和weights量化级别</font>参考

| <font style="color:rgb(63, 63, 63);">No Alpha：CEM_LDR_RGB_DIRECT</font> | <font style="color:rgb(63, 63, 63);"> </font> | <font style="color:rgb(63, 63, 63);">Has Alpha：CEM_LDR_RGBA_DIRECT</font> | <font style="color:rgb(63, 63, 63);"> </font> |
| :--- | :--- | :--- | :--- |
| <font style="color:rgb(63, 63, 63);">权重范围</font> | <font style="color:rgb(63, 63, 63);">Endpoints范围</font> | <font style="color:rgb(63, 63, 63);">权重范围</font> | <font style="color:rgb(63, 63, 63);">Endpoints范围</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_3</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> | <font style="color:rgb(63, 63, 63);">QUANT_3</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_4</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> | <font style="color:rgb(63, 63, 63);">QUANT_4</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_5</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> | <font style="color:rgb(63, 63, 63);">QUANT_5</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_6</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> | <font style="color:rgb(63, 63, 63);">QUANT_6</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_8</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> | <font style="color:rgb(63, 63, 63);">QUANT_8</font> | <font style="color:rgb(63, 63, 63);">QUANT_192</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_12</font> | <font style="color:rgb(63, 63, 63);">QUANT_256</font> | <font style="color:rgb(63, 63, 63);">QUANT_12</font> | <font style="color:rgb(63, 63, 63);">QUANT_96</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_16</font> | <font style="color:rgb(63, 63, 63);">QUANT_192</font> | <font style="color:rgb(63, 63, 63);">QUANT_16</font> | <font style="color:rgb(63, 63, 63);">QUANT_48</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_20</font> | <font style="color:rgb(63, 63, 63);">QUANT_96</font> | <font style="color:rgb(63, 63, 63);">QUANT_20</font> | <font style="color:rgb(63, 63, 63);">QUANT_32</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_24</font> | <font style="color:rgb(63, 63, 63);">QUANT_64</font> | <font style="color:rgb(63, 63, 63);">QUANT_24</font> | <font style="color:rgb(63, 63, 63);">QUANT_24</font> |
| <font style="color:rgb(63, 63, 63);">QUANT_32</font> | <font style="color:rgb(63, 63, 63);">QUANT_32</font> | <font style="color:rgb(63, 63, 63);">QUANT_32</font> | <font style="color:rgb(63, 63, 63);">QUANT_12</font> |




工程里ASTC6*6使用的就是<font style="color:rgb(25, 27, 31);">ASTC规定的BISE规则用</font><u><font style="color:rgb(25, 27, 31);">Base5</font></u><font style="color:rgb(25, 27, 31);">将颜色端点编码成QUANT80</font>储存颜色端点。



总结一下ASTC的压缩逻辑，首先将图片分取切成一个一个Block，然后读取每个Block的颜色值记录到数组里，求出数组每个通道的最大值和最小值作为Color Endpoints，通过Color Endpoints计算Weight Grid，将这些Block的值用位操作塞入uint4 里，最后将每个Block的uint4 都写入到目标纹理中。



# 优化部分
### 不支持A通道压缩的解决方法
首先之前的方法是不支持将A通道进行压缩的，将A通道一起压缩的方法看上去也不难，只需将A通道一起计算Color Endpoints（颜色端点）和Weight Grid（权重网格）并且按照ASTC编码组成规范填入就行了。

如果正常选择ASTC 4*4,R8G8B8A8<font style="color:rgb(25, 27, 31);">，Weight为3bits，</font>就会出现只剩下128-17-4*8*2=44<4*4*3=48的情况。

如果选择压缩Color Endpoints（颜色端点），选择ASTC 4*4,R6G6B6A6<font style="color:rgb(25, 27, 31);">，Weight为3bits，</font>就会出现128-17-（5*6（低位）+8（高位）+3*8（剩下的3个通道））-4*4*3=1bits,只剩下1bits空间

如果选择压缩Weight Grid（权重网格），选择ASTC 4*4,R8G8B8A8<font style="color:rgb(25, 27, 31);">，Weight为2bits， </font>就会出现128-17-4*8*2-4*4*2=15bits,也是还有15bits空间；

一开始鉴于第二种方式实现也比较方便实现也比较方便，所以采用第二种方式，既选择更少的<font style="color:rgb(25, 27, 31);">Weight</font>。

首先重新定义uint4 PackedBlock = uint4(0x00018042, 0, 0, 0);作用是更改元数据，声明图像是ASTC 4*4,R8G8B8A8<font style="color:rgb(25, 27, 31);">，Weight为2bits，分区数为1个分区的类型；</font>

0x00018042换成二进制位00000000 00000001 10000000 01000010 

**00000000 00000001 10000000 01000010**。  

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728471664216-7ee08bfa-7337-42bf-a31b-4e181264a8ac.png)

红色部分对应

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729672662937-c03aaa2b-5b99-49e6-8536-00e887325f82.png)

黄色对应

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728471827238-c97e739b-059b-42d0-ab7a-1314cbe2ffa2.png)

蓝色部分对应![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729672779966-1cf98f5c-026f-4f6f-9ff8-25ce5aeef1d0.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729672802209-95c8935c-3837-463d-b8e8-9844bd309563.png)

<font style="color:#E7E9E8;">（元数据花了一天时间才看懂）</font>

      <font style="color:rgb(25, 27, 31);">将之前RGB求</font>Color Endpoints（颜色端点）<font style="color:rgb(25, 27, 31);">的操作加上A通道一起求；</font>

<font style="color:rgb(25, 27, 31);">然后将Color Endpoints（颜色端点）用位运算放入PackedBlock；</font>

```properties
 uint4 PackedBlock = uint4(0x00018042, 0, 0, 0);
 PackedBlock.x |= (MinColorUint.r << 17);
 
 PackedBlock.x |= (MaxColorUint.r << 25);
 PackedBlock.y |= (MaxColorUint.r >> 7);
 
 PackedBlock.y |= (MinColorUint.g << 1); 
 PackedBlock.y |= (MaxColorUint.g << 9); 
 PackedBlock.y |= (MinColorUint.b << 17);
 
 PackedBlock.y |= (MaxColorUint.b << 25);
 PackedBlock.z |= (MaxColorUint.b >> 7);
 PackedBlock.z |= (MinColorUint.a << 1);  
 PackedBlock.z |= (MaxColorUint.a << 9);  
```

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728383685587-72354a10-bf70-44fb-adbf-67418013bd8a.png)

然后就是Weight Grid（权重网格）放入

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1731322267127-c2513e41-1fdc-48f9-a3b6-c4fc85ac7bc7.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728385725711-b18fcaa0-1afe-4f9a-afe7-6891f0e812cb.png)

至此，ASTC4X4支持RGBA的压缩大功告成！

### 压缩表现效果优化
<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">参考的github作者说过ASTC压缩的精度损失主要来自下面三个方面</font>

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728982199512-6e46e3cb-7fa9-498a-8d66-952ff0254747.png)

因为我们在颜色端点和权重网格上没有进行量化的操作，所有主要的误差就是来自计算颜色端点和权重网格的时候出现的问题。

在计算颜色端点的时候，我们采用的是求取最大值的方法，而不是求这个块的主要颜色是什么，这样带来的坏处就是，假设我一个块里有16个像素，其中14个都是（1，0，0，1），剩下两个为（0.1，1，1，1）和（0，0，0，0），那么颜色端点也是（1，1，1，1）和（0，0，0，0）。这样的话权重网格在做差值时虽然会把范围内的颜色都包括进来，但是因为误差太大导致效果很差。

（中间的大图为压缩后的图，右边的图上边为原图，下边为使用unity的ASTC4x4压缩的图，可以看出使用Unity压缩的图的效果和原图差别不大，但是使用知乎作者的方法压缩的图在细节上就糊了很多）![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728982926411-3b790a62-ef7e-4918-9798-b83c46efa881.png)

所以对于块里的颜色变化比较大，即细节比较多的图片应该采用其他的方法来计算颜色端点和权重网格。

本文使用的方法就是<font style="color:rgb(17, 17, 17);background-color:rgb(253, 253, 253);">github作者说的PAC方法</font>![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728983461753-2f5ba7d5-1c6e-4f6c-a0e1-864e71561d1e.png)



```properties
void PCAEndpoints(in float4 Block[PIXEL_COUNT_2D], out float4 OutMin, out float4 OutMax)
{
     // 1. 计算均值
    float4 mean = CalculateMean(Block);

    // 2. 去均值化并计算协方差矩阵
    float4x4 covarianceMatrix = CalculateCovarianceMatrix(Block,mean);

    // 3. 选择初始向量并使用幂次法计算主方向
    float4 principalAxis = MaxAccum(covarianceMatrix, 4);  // 4次迭代

    // 4. 计算端点
    CalculateEndpoints(Block, principalAxis,mean, OutMin, OutMax);
    
 }
 float4 CalculateMean(float4 colors[16]) 
{
    float4 mean = float4(0, 0, 0, 0);
    
    // 计算均值
    for (int i = 0; i < 16; i++) {
        mean += colors[i];
    }
    mean /= 16.0;

    return mean;
}
float4x4 CalculateCovarianceMatrix(float4 colors[16], float4 mean) 
{
    float4x4 covariance = float4x4(0, 0, 0, 0,
                                   0, 0, 0, 0,
                                   0, 0, 0, 0,
                                   0, 0, 0, 0);

    // 2. 去均值化并计算协方差矩阵
    for (int i = 0; i < 16; i++) {
        float4 centeredColor = colors[i].rgba - mean;

        covariance[0][0] += centeredColor.r * centeredColor.r;
        covariance[0][1] += centeredColor.r * centeredColor.g;
        covariance[0][2] += centeredColor.r * centeredColor.b;
        covariance[0][3] += centeredColor.r * centeredColor.a;

        covariance[1][0] += centeredColor.g * centeredColor.r;
        covariance[1][1] += centeredColor.g * centeredColor.g;
        covariance[1][2] += centeredColor.g * centeredColor.b;
        covariance[1][3] += centeredColor.g * centeredColor.a;

        covariance[2][0] += centeredColor.b * centeredColor.r;
        covariance[2][1] += centeredColor.b * centeredColor.g;
        covariance[2][2] += centeredColor.b * centeredColor.b;
        covariance[2][3] += centeredColor.b * centeredColor.a;

        covariance[3][0] += centeredColor.a * centeredColor.r;
        covariance[3][1] += centeredColor.a * centeredColor.g;
        covariance[3][2] += centeredColor.a * centeredColor.b;
        covariance[3][3] += centeredColor.a * centeredColor.a;
    }

    covariance /= 16; // 计算样本协方差矩阵

    return covariance;
}
float4 MaxAccum(float4x4 covarianceMatrix, int iterations) 
{
    float4 initialVector = float4(1, 1, 1, 1);  // 初始向量

    for (int i = 0; i < iterations; i++) {
        initialVector = mul(covarianceMatrix, initialVector);
        
        initialVector /= length(initialVector);
    }
    initialVector=normalize(initialVector);

    return initialVector;
}
void CalculateEndpoints(float4 colors[16], float4 principalAxis,float4 mean, out float4 minEndpoint, out float4 maxEndpoint) 
{
    float minProjection = dot(colors[0]- mean, principalAxis);
    float maxProjection = minProjection;
    minEndpoint = colors[0];
    maxEndpoint = colors[0];
    // 投影到主方向并找出最小和最大投影点
    for (int i = 1; i < 15; i++) {
        float projection = dot(colors[i] - mean, principalAxis);
        
        if (projection < minProjection) {
            minProjection = projection;
            minEndpoint = colors[i];
        }

        if (projection > maxProjection) {
            maxProjection = projection;
            maxEndpoint = colors[i];
        }
    }

    //解码时会将最小值和最大值的RGb相加作比较，如果最小值的rgb相加大于最大值，会调换，事先做比较
    if ((minEndpoint.x+minEndpoint.y+minEndpoint.z)>(maxEndpoint.x+maxEndpoint.y+maxEndpoint.z))
    {
        float4 a = maxEndpoint;
        maxEndpoint=minEndpoint;
        minEndpoint=a;
    }
 
}
```



因为ASTC解码的时候会将mincolor和maxcolor的RGB相加，来判断要不要将他们对调![](https://cdn.nlark.com/yuque/0/2024/jpeg/39137189/1729489393615-20a547f8-a709-43d9-a423-c528890f5b34.jpeg)

所以最后我们还要手动的做一次比较，防止解码的时候颜色不对



优化压缩算法后的效果对比

PAC

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729049137475-78783138-b203-4f5c-94f5-ba7510cb3263.png)

原图

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729049174206-213f17d3-f088-4a5b-9248-41531887b8f3.png)

求最大最小值

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729049099088-15b620cf-f125-4ffe-a2af-3254c28b46be.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729049102742-40f27268-3295-4792-9a49-3ec86e20d79f.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729049105600-9aef7fb8-f0d0-49ca-a6dd-4ede9bbe749c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729049107859-9e2cd9ed-aa23-482c-9ea7-219ff9fffbd8.png)



### 使用BISE编码压缩
在之前我们了解了BISE编码是怎么将一组数进行压缩的，接下来就是实际操作将颜色端点<font style="color:rgb(25, 27, 31);">使用ASTC规定的BISE规则编码成</font>QUANT192，然后使用Base3的方式压缩。

首先就是所有的颜色通道都要编码为QUANT192，不能有些编码为QUANT192，有些使用其他方式编码。

```properties
uint4 MinColorUint = (uint4)round(saturate(MinColor) * 255);
uint4 MaxColorUint = (uint4)round(saturate(MaxColor) * 255);
MinColorUint = uint4(color_quant_table[MinColorUint.x], color_quant_table[MinColorUint.y], color_quant_table[MinColorUint.z], color_quant_table[MinColorUint.w]);
MaxColorUint = uint4(color_quant_table[MaxColorUint.x], color_quant_table[MaxColorUint.y], color_quant_table[MaxColorUint.z], color_quant_table[MaxColorUint.w]);
 ...
float color_quant_table[256] ；
```

[https://github.com/gildor2/UEViewer/blob/master/libs/astc/astc_quantization.cpp](https://github.com/gildor2/UEViewer/blob/master/libs/astc/astc_quantization.cpp)有各种对应的QUANT量化数组。

量化完后就将高低位分开，因为使用的是Base3方法，所以高位是两位，低位是六位。

```properties
ASTC4x4_SplitHighLow(uint4(MinColorUint.r, MaxColorUint.r, MinColorUint.g, MaxColorUint.g), q0, m0);
ASTC4x4_SplitHighLow(uint4(MinColorUint.b, 0, 0, 0), q1, m1);
 ...
 #define ASTC4x4_COLOR_BIT_COUNT 6
void ASTC4x4_SplitHighLow(uint4 n, out uint4 high, out uint4 low)
{
    uint lowMask = (1 << ASTC4x4_COLOR_BIT_COUNT) - 1;
    low = n & lowMask;
    high = n >> ASTC4x4_COLOR_BIT_COUNT;
}
```

然后将5个高位用Base3的方式编码为8bits

[https://github.com/gildor2/UEViewer/blob/master/libs/astc/astc_integer_sequence.cpp](https://github.com/gildor2/UEViewer/blob/master/libs/astc/astc_integer_sequence.cpp)有各种对应的数组。

```properties
uint packhigh = (uint)floor(integer_from_quints[q1.x * 81 + q0.w * 27 + q0.z * 9 + q0.y * 3 + q0.x]);

float integer_from_quints[243] ；
```

最后按照官方规定的这个顺序将得到的值填入128bits就行了，图中m0这些低位所占的是4bits，其实无论是6bits还是3bits都可以填。



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729854307318-c57b669e-c5cd-4279-b2e5-366cc9de50c4.png)

```properties
uint packhigh1 = packhigh & (uint)3;//取后面两位            
uint packhigh2 = (packhigh>>2) & (uint)3;//取后面第三第四位
uint packhigh3 = (packhigh>>4) & (uint)1;//取后面第五位
uint packhigh4 = (packhigh>>5) & (uint)3;//取后面第六第七位
uint packhigh5 = (packhigh>>7) & (uint)1;//取后面第八位

PackedBlock.x |= (m0.r << 17);
PackedBlock.x |= (packhigh1 << 23);
PackedBlock.x |= (m0.g << 25);
PackedBlock.x |= (packhigh2 << 31);

PackedBlock.y |= (packhigh2 >> 1);
PackedBlock.y |= (m0.b << 1);
PackedBlock.y |= (packhigh3 << 7); 
PackedBlock.y |= (m0.a << 8); 
PackedBlock.y |= (packhigh4 << 14);
PackedBlock.y |= (m1.r << 16);
PackedBlock.y |= (packhigh5 << 22);
PackedBlock.y |= (MaxColorUint.b << 23); 
PackedBlock.y |= (MinColorUint.a << 31); 

PackedBlock.z |= (MinColorUint.a >> 1); 
PackedBlock.z |= (MaxColorUint.a << 7);  
```



顺便说一下Base5的填写规则如下

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729854513123-72734811-4b34-4d0a-953c-3df9c5e1b011.png)



最后看BISE的效果![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730102801158-31659f16-7fb4-4a36-87bc-4d2b4406f9a6.png)

可以明显看出4*4的块里超过4种颜色，但是出现了新的问题![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730102768250-e19ef795-21b4-41dd-ad8e-bb53844fb336.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730102849790-d447bae6-ea5f-48fe-8f4e-fa00c8a0e282.png)

可以看到会出现有些块里出现奇怪的颜色，看端点颜色发现是两个端点的RG通道都有问题，B和A通道正常，查了一些资料，大概是因为量化时出现的错误。 当两个颜色端点很接近时，BISE 的量化精度不足以精确表示这种细微差别，BISE 编码倾向于拉大端点之间的距离，这样可以更好地生成中间渐变，但在端点非常接近时，这种误差会更明显 。解决方案就是手动拉大端点距离，或者使用 YCoCg 颜色空间。  

YCoCg 是后面优化再做的内容，先手动拉开端点颜色

```properties
minEndpoint = round(saturate(minEndpoint) * 255)/255;
maxEndpoint = round(saturate(maxEndpoint) * 255)/255;

if(abs(maxEndpoint.x - minEndpoint.x) < 0.02)
{
    minEndpoint.x = max(minEndpoint.x - 0.012, 0);
    maxEndpoint.x = max(maxEndpoint.x + 0.012, 0);
}

if(abs(maxEndpoint.y - minEndpoint.y) < 0.02)
{
    minEndpoint.y = max(minEndpoint.y - 0.012, 0);
    maxEndpoint.y = max(maxEndpoint.y + 0.012, 0);
}
```

再看手机端的效果，奇怪的颜色就消失了.

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730103488985-61c5a6ff-85f0-4e98-834a-08afe5b4276b.png)

# 跟Unity ASTC4x4对比
本次对比只看像素的值，所以将图片的Filter Mode都改成Point，不使用任何差值的方式，只显示像素颜色的原本颜色来对比。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063358398-0b173fef-e672-4f5b-b62b-07c91cb31acd.png)



**对比颜色效果**

首先是在块里的颜色为如下情况下

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063472328-6dc58ed5-cda7-4853-ba7f-827926f3ca8e.png)

整体效果如下，其中中间的大图为中间压缩的效果![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063551276-40d522d7-e128-463a-8937-4cfce2257332.png)

自己压缩							原图						Unity压缩

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730103659621-59b856c2-9bec-46fd-be1d-07a2dcee7f5e.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063622068-bcdec7f1-db15-4abf-ac9b-2df40f84abee.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729063635846-d16c25b0-60d1-46fe-aad0-5a02488ec4fe.png)

可以明显发现，自己压缩的一个块里就只有黑白两种端点颜色，因为自己的<font style="color:rgb(25, 27, 31);">Weight Range为4</font>，而Unity却能够有十几种颜色，这是因为unity压缩时，如果块里的颜色较多，或者差异较大的时候就会使用了<font style="color:rgb(25, 27, 31);">Partition（</font>分区)概念，这个因为不能实时计算，所以我们是没有这种功能的

但采样块里的颜色细节的时候，可以发现unity颜色也是有一点不同的，如第二排第二个，原图颜色为（169，144，144），而压缩后的颜色为（173，120，120）.这是因为颜色端点量化的误差导致的。



**对比细节保留量：**

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729065051185-c42e907f-2a2b-45a8-8688-97e249044180.png)

自己压缩							原图						Unity压缩

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729064512194-e40a7256-c1bb-4f7b-a556-d922939e3ba9.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729064497711-cba04cd8-82cb-4101-96a6-249db4663df3.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729064648031-7146243a-ab41-4d8b-8da4-b095ddbaa79e.png)



<font style="color:rgb(25, 27, 31);">再用PSNR的方式对比</font>![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729495264271-050ca142-f768-4003-8144-8e69e49fd7f5.png)

```properties
public static float CalculatePSNR(Texture2D originalTexture, Texture2D noisyTexture)
{
    if (originalTexture.width != noisyTexture.width || originalTexture.height != noisyTexture.height)
    {
        Debug.LogError("两个图像必须具有相同的尺寸！");
        return -1;
    }

    float maxPixelValue = 255f;

    // 计算三个通道的MSE
    float mse = CalculateMSE(originalTexture, noisyTexture);
    if (mse <= 1) return 20 * Mathf.Log10(maxPixelValue); // 完全相同的图像PSNR为无穷大


    // 计算PSNR
    float psnr = 20 * Mathf.Log10(maxPixelValue) - 10 * Mathf.Log10(mse);
    return psnr;
}

private static float CalculateMSE(Texture2D originalTexture, Texture2D noisyTexture)
{
    Color[] originalPixels = originalTexture.GetPixels();
    Color[] noisyPixels = noisyTexture.GetPixels();

    float mse = 0f;
    int pixelCount = originalPixels.Length;

    // 对每个像素计算RGB通道的差值平方和
    for (int i = 0; i < pixelCount; i++)
    {
        float diffR = originalPixels[i].r - noisyPixels[i].r;
        float diffG = originalPixels[i].g - noisyPixels[i].g;
        float diffB = originalPixels[i].b - noisyPixels[i].b;
        float diffA = originalPixels[i].a - noisyPixels[i].a;

        mse += (diffR * diffR + diffG * diffG + diffB * diffB + diffA * diffA) * 65025; // 255*255=65025
    }

    mse /= pixelCount; // 求平均误差
    return mse;
}
```



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729495868430-b33ad964-2556-4967-8e8c-f9db4102e4ba.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729496349566-478d2e2e-1095-4759-b2e9-6b134ba4f194.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729495886270-fc929741-71cf-4836-a91d-c86e5e81daf3.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1729495896455-35a0fcab-af60-43f5-b872-9a9f1e337a0e.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730103488985-61c5a6ff-85f0-4e98-834a-08afe5b4276b.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730103932751-acdd91ac-3873-4590-8d22-1ad2f0457a54.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730103949224-2119df08-301b-464c-8d78-528dbbc274e5.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730103961679-aa35eedc-fbe8-4295-ae6e-087732dd6126.png)

用手动拉开端点颜色的方法大概会比不使用Bise编码的方法低2分左右



![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730343555240-0b73acf3-87ef-4db9-9a46-3aacd794928a.png)

将元富翁拿出140张贴图做比较（DiffuseTexture）

其中第46、67~69、118、121得分差距有点大

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730343747723-57e45868-414c-4831-9d00-644353750eff.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730343755660-e2c6bb3c-b369-48f2-b3cf-eaeccc2b6822.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730343762155-e914f76c-8187-4e98-b4b6-623dc2526c92.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730343772009-1fe64fde-771c-4019-81d0-9bd1f1b31dcf.png)

第46是因为它是512*512且有Alpha的原因，67~69是图集，118是渐变，121又是图集

# 性能测试
机型：Redmi K20 Pro 

脚本后台：  IL2CPP  

GPU：高通骁龙855Plus

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728722694596-913cfe1f-92d7-4059-9878-32a20d2d3da8.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728722711240-69b81f50-0b70-456c-8662-13a57f448987.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728722720955-ab9a67b5-ef36-4fed-8249-a505a62a7705.png)

在第一次运行时CPU耗时是12.70ms，后面在1.2ms左右，其中![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728722974188-41104881-dba3-47a2-86c4-dad4e94b590d.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1728722952182-315619ec-8d5b-4878-95cd-545333600933.png)	第一次创建Texture和预热Shader花费6ms。

(如果Texture尺寸修改则会重新创建texture)。



连续压20张不同的贴图（大小，内容均不同），对比CPU耗时均在1左右上下波动

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730345271861-d5475654-d7ae-4798-890b-7a54213de904.png)

连续压2048大小的贴图cpu耗时为1ms左右

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730429714127-b3278b04-7576-42ab-978e-041ed5f7564e.png)连续压1024大小的贴图cpu耗时为0.6ms左右![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730429677870-17c015f2-a67c-43b4-8ea2-4de14d055bb1.png)

连续压512大小的贴图cpu耗时为0.4ms左右![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730429649723-349ffb80-b718-4c12-9cb2-d82cf21a5d5b.png)



[捕鱼 - GOT Online GPU性能分析](https://www.uwa4d.com/u/got/gpu.html/report?dataKey=20241101104319nit7232gpu917&project=9912&engine=1) 测ASTC开销的UWA报告

每帧都进行一次压缩对比gpu开销

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1730429402048-5776b6bb-2087-48e0-9472-e307a9354b7f.png)

去除固定消耗，压缩各种大小的贴图GPUClocks

2048*2048：216万

1024*1024：54万

512*512    ：10万

他们大小约为4倍的关系，也符合逻辑



# 注意事项
**看平台是否支持读取ASTC格式**

Windows上是不能通过创建format为ASTC格式的Texture2D，包括在编辑器上也是。

编辑器上能在图片导入的时候压缩为ASTC格式，但是却不能在脚本上动态创建一张ASTC格式的图片。

Texture2D TargetTexture=new Texture2D(TextureWidth, TextureHeight, <u>GraphicsFormat.RGBA_ASTC5X5_UFloat</u>, mipCount, flags)；

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1727675700450-511d82a4-faa3-4840-98c8-971122b93f43.png)

所以要判断目前平台支不支持ASTC格式来创建对应得Texture2D来存储压缩后的图片信息。



