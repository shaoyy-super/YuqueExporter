<font style="color:black;">BlueNoise方案</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394601092-8e0adb78-66e5-4492-b72e-b57024a7c54d.png)

统计

每帧一个<font style="color:black;">Texture2D</font><font style="color:black;">函数传递贴图给</font><font style="color:black;">shader</font><font style="color:black;">，两个</font><font style="color:black;">Vector4</font><font style="color:black;">函数传递参数和贴图尺寸</font>

<font style="color:black;">_BlueNoiseTexture，_SSAOBlueNoiseParams，_SSAOParams</font>

<font style="color:black;"></font>

<font style="color:black;">InterleavedGradient方案</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394638854-d37195be-9454-48ca-99cf-455282e6e640.png)

每帧一个<font style="color:black;">Vector4函数传递参数给shader</font>

<font style="color:black;">_SSAOParams</font>

<font style="color:black;"></font>

<font style="color:black;">_SSAOParams参数属于公共参数，拆解xyzw为INTENSITY，RADIUS，DOWNSAMPLE，FALLOFF参与计算，两种方案具体差异化实现在PickSamplePoint（）函数</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394660045-2c8a83db-1849-4110-8a80-3336855936c1.png)

<font style="color:black;">PickSamplePoint</font><font style="color:black;">（）进行</font><font style="color:black;">noise</font><font style="color:black;">生成</font>

<font style="color:black;">走BlueNoise</font>方案：

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394675963-f107ea98-d5c9-4739-93cb-411cbe41d617.png)

<font style="color:black;">采样</font><font style="color:black;">_BlueNoiseTexture</font><font style="color:black;">贴图并使用</font><font style="color:black;">_SSAOBlueNoiseParams</font><font style="color:black;">传参</font><font style="color:black;">xyzw</font><font style="color:black;">拆分成</font>

<font style="color:black;">_SSAOBlueNoiseParams.xy和_SSAOBlueNoiseParams.zw控制贴图的缩放和偏移</font>

<font style="color:black;"></font>

<font style="color:black;">走InterleavedGradient方案：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394693496-e49e8a87-531a-420b-b5e9-343e50c73f04.png)

<font style="color:black;">InterleavedGradientNoise（）函数计算生成noise</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394713600-8772a5e4-b62a-43bd-ab5c-e831f72acc50.png)





<font style="color:black;">效果对比</font>

<font style="color:black;">BlueNoise</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394725056-47ea411e-b64b-4b5c-a5a6-435d06c10ee7.png)

<font style="color:black;">InterleavedGradient</font>

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1719394734165-b4d5388a-f951-4a54-b481-90ebc5225950.png)

<font style="color:black;">综上，其他参数相同，效果对比BlueNoise更明显，噪点更大</font>

<font style="color:black;">开销对比BlueNoise方案多采样一张贴图和对应参数，开销大于InterleavedGradient</font>

