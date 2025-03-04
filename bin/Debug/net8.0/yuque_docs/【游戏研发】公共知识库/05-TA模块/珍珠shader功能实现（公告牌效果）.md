珍珠材质是逆向还原以闪亮之名的珍珠效果实现的。



## 顶点着色器
从模型可以看出以闪亮之名的珍珠效果是通过面片做的，也就是公告牌效果，让面片一直朝向摄像机。![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732786849269-58ae82be-0623-4d6c-9624-7b85efd80ed4.png)

公告牌效果网上有很多种做法，大部分都是在世界空间下操作的；大致逻辑是构建一个新的坐标系，通过在世界空间下Camera减去Object的位置作为新坐标的z轴，Camera的向上方向作为y轴，再叉积出新的x轴，通过这个坐标系构建一个旋转矩阵，将世界空间的物体转到新的坐标系上，这样就能使Object时刻朝向摄像机了。

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732792132001-06601af6-a413-44a2-b450-f7ef072db908.png)

但是通过新坐标构建旋转矩阵的话计算量太多了。要做很多sin，cos这些计算。

然后第二种方法就是利用物体在object space空间始终朝向相机的原理，让object的m矩阵的旋转变换换成v矩阵的旋转，从而始终朝向相机。

```properties
Varyings output = (Varyings)0;
 //用v矩阵的旋转 构建新m矩阵的旋转矩阵
 float3 upCamVec = normalize(UNITY_MATRIX_V._m10_m11_m12);
 float3 forwardCamVec = -normalize(UNITY_MATRIX_V._m20_m21_m22);
 float3 rightCamVec = normalize(UNITY_MATRIX_V._m00_m01_m02);
 float4x4 rotationCamMatrix = float4x4(rightCamVec, 0, upCamVec, 0, forwardCamVec, 0, 0, 0, 0, 1);

 //乘上m矩阵的缩放和平移属性
 //求出缩放值,前三行三列的每一列的向量长度分别对应X,Y,Z三个轴向的缩放值
 float4 positionWS = input.positionOS;
 positionWS.x *= length(UNITY_MATRIX_M._m00_m10_m20);
 positionWS.y *= length(UNITY_MATRIX_M._m01_m11_m21);
 positionWS.z *= length(UNITY_MATRIX_M._m02_m12_m22);
 //在固定坐标系下面，我们用左乘的方法；在非固定的坐标系下面，用右乘。
 positionWS = mul(positionWS, rotationCamMatrix);
 //input.positionOS = input.positionOS.x * rotationCamMatrix._m00_m01_m02_m03 + input.positionOS.y * rotationCamMatrix._m10_m11_m12_m13
 //+ input.positionOS.z * rotationCamMatrix._m20_m21_m22_m23 + input.positionOS.w * rotationCamMatrix._m30_m31_m32_m33;
 //最后一列是模型中心的世界坐标,加上的是齐次坐标的偏移值,不加都会在原点
 positionWS.xyz += UNITY_MATRIX_M._m03_m13_m23;
 output.positionCS = TransformWorldToHClip(positionWS);

```

处理完positionOS后，normalOS和tangentOS也要乘上该旋转矩阵，然后将他原本的m矩阵变换去掉，法线的m矩阵在等比缩放和非等比缩放的时候，乘上的矩阵是不一样的，不过由于我们使用的面片模型法线的朝向都是一个方向，所以直接乘上，效果也没错

```properties
input.normalOS = normalize(mul(float4(input.normalOS, 0), rotationCamMatrix)).xyz;
input.tangentOS.xyz = normalize(mul(float4(input.tangentOS.xyz, 0), rotationCamMatrix)).xyz;

VertexPositionInputs vertexInput = PearlGetVertexPositionInputs(positionWS);
VertexNormalInputs normalInput = PearlGetVertexNormalInputs(input.normalOS, input.tangentOS);
...

VertexNormalInputs PearlGetVertexNormalInputs(float3 normalOS, float4 tangentOS)
{
    VertexNormalInputs tbn;

    // mikkts space compliant. only normalize when extracting normal at frag.
    real sign = real(tangentOS.w) * GetOddNegativeScale();
    //tbn.normalWS = TransformObjectToWorldNormal(normalOS);
    tbn.normalWS = normalOS;
    //tbn.tangentWS = real3(TransformObjectToWorldDir(tangentOS.xyz));
    tbn.tangentWS = tangentOS;//real3(TransformObjectToWorldDir(tangentOS.xyz));
    tbn.bitangentWS = real3(cross(tbn.normalWS, float3(tbn.tangentWS))) * sign;
    return tbn;
}

```

这样公告牌效果就完成了，且正确保留m矩阵的缩放和位移。

## 片元着色器
以闪亮之名的珍珠材质主要分为3个效果分别是

                采样matcap；            使用uv偏移生成高度图采样HDRCubeMap；以及前面算好的Fresnel效果叠加	

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732794008750-07e02a80-b09c-46c2-a150-63813ecf56f8.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732794057201-e2c4b26e-da2b-4dc4-a17a-57b21e90ae8f.png)![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732794090227-054aad12-a756-454f-bedb-25620f2885a2.png)

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732794218149-eadec124-d0f9-446c-b0eb-df7579810680.png)

上图是在线性空间下的最终效果。



**为什么确认是使用matcap方法采样和使用的是HdrCubeMap**：

因为他采样的贴图长这样子，然后和采样出的效果可以猜出是使用matcap方法采样，所以还原效果的的时候，采样的主帖图也是使用matcap方法

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732794503062-76259c55-cc93-410d-8ff0-2dfe564cb89f.png)

而采样CubeMap的时候，有一步是使用采样出的贴图的a通道乘16再乘rgb的过程，这个就是hdr贴图使用RGBM编码的时候的解码过程

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732794388374-30a6a658-30d4-4c72-96aa-eb8b61a9196d.png)



**最后还原他的shader（不能盲目还原他的shader，而是要知道他在做什么）。**

（他是使用高度图转换法线效果，而我们是直接使用采样法线贴图的法线）

首先main贴图使用matcap采样作为diffuse

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732797920760-da2b6789-8345-40e3-a074-a836dd4687b3.png)

然后使用自定义的HDRCubeMap替换GIColor里的高光采样的Cubemap，且采样方式换成和他一样的利用世界空间法线采样

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732797941846-739fccb1-366d-476f-ac69-ba022006727e.png)



由于他的fresnel效果没法参考，使用直接使用NdotV的Fresnel效果，最后叠加起来，效果就完成了

![](https://cdn.nlark.com/yuque/0/2024/png/39137189/1732798012623-e02798eb-b79f-4582-805e-9a3405597887.png)















