# 背景
1. 捕鱼项目的3104金猪boss Transform里面有个常驻的金币掉落特效，会随着金猪左右移动过程中，一直发出随重力掉落的金币。
2. 金猪左右移动是通过修改金猪根节点的Transform scale.x = 1/-1来实现的。
3. 因此在金猪左右移动过程中，金币特效实际上也会处于world scale.x = 1/-1的情况。而在这过程中，发现金币world scale.x = -1时，光照明显不正确。

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737425459723-0608ac61-7b9a-45a8-98c4-f6e09d94620e.png)

4. 如上图，此时金猪根节点scale.x = -1，同时因为金币本身也有自旋转，所以出现了部分金币world scale.x = 1，部分金币world scale.x = -1的情况。进而能发现两种情况下金币的光照效果差异很大，不符合期望。



# 调查
## 一、金币材质与设置
![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737425714315-982b1d18-ede3-4372-9999-497b7234fc1e.png)![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737425729470-a18e542f-9b63-41ab-bafc-313e1e474b30.png)![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737425759485-61367d83-4574-45d6-a6b1-a09b2920c4e3.png)

如上图可见，金币本身就是一个粒子系统加上捕鱼的特效材质，用的是半透明加双面，没有什么特别的设置。

首先试了下切换半透明成Cutout，以及切换双面显示来看光照是否有变化。结果并没有什么差别。

### ps. 这里要测试粒子系统可以将粒子固定下来，方便观察：
![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737426040030-81126ac6-057e-44e3-85db-8aaabd7ac907.png)![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737426078151-0fbccfeb-67a3-4007-9f16-ae2f63fcfb37.png)

将粒子系统里标记的这些关掉，然后相关数值设成0，就可以获得一个固定位置，不会变动的静态粒子，这样方便观察。



## 二、测试其他Shader
1. 保持粒子系统不变，将Effect_General shader改成FishStandard shader。结果没有差别。

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737426556306-2e9db72c-ea90-468d-9aa2-289615b608f6.png)



2. 再新建一个urplit的材质替换上去，发现urp lit的效果是符合预期的，scale.x = 1/-1时，光照始终是同一个效果。

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737426959476-475bb700-4945-4c18-b602-709f07fd75a9.png)



对比两个shader，可以发现光照部分比较可疑的点就在FishStandard会做 normalWS * facing，而urplit不会。

使用rendering debugger显示normalWS，也能发现问题确实在这里：

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737427388541-fa5a8dac-5eca-4604-88e8-e7fcf7ca6131.png)

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737427401725-3c3d47e1-1b58-411c-9fa9-9a1f6a0f3e47.png)

1/-1时，金猪和左边urplit的金币差别都不大，而右边FishStandard的金币则normalWS有明显差异。

将FishStandard normalWS * facing中的* facing去掉后，再用rendering debugger来显示，就会发现normalWS符合预期了：

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737427543017-dba7a380-1034-4599-8902-d5d62efd3bb4.png)![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737427552267-ae275d79-30a4-482d-b908-274c7b09b7bc.png)



## 三、直接原因总结
对比下来，直接的结论就是在粒子系统里，scale.x的变化影响了面朝向，导致对此敏感的FishStandard以及Fish_Effect_General（特效开启PBR模式时与FishStandard采用同一套光照计算）出现了明显变化，而对面朝向不敏感的urplit则没有影响。

要处理这个问题，还需要知道

1. 粒子系统镜像时，面朝向变化的根本原因
2. fishstandard计算光照时采取normalWS * facing的目的。



## 四、面朝向变化原因调查
如上所述，粒子系统里，scale.x的变化影响了面朝向，那么其他Renderer是否会有此差别？

### 测试MeshRenderer和SkinnedMeshRenderer
因为金猪本身就是SkinnedMeshRenderer，所以其实从一开始就应该注意到一个被忽视了的事：SkinnedMeshRenderer scale.x = 1/-1不会影响fishstandard的光照效果。

至于MeshRenderer，可以新建两个Cube，一个是urplit，另一个是fishstandard。观察scale.x = 1/-1的差别。

![scale.x = 1](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737429232148-bf18f9b4-c5ff-4b55-9b7f-df64f24e6bd9.png)

![scale.x = -1](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737429266839-2f1bbfcf-702e-49e6-a801-cdddc32487b6.png)![scale.x = 1 normalWS](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737429351542-db86b1ae-0120-4f83-8d23-3b2f27eeab03.png)

![scale.x = -1 normalWS](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737429324525-8613436b-25f6-41c6-b25e-73d5f1228bca.png)

![scale.x = 1 (facing, 0, 0, 1)](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737429963868-f433b7a5-8b37-4cd7-bcf6-9180abebe5bc.png)

![scale.x = -1 (facing, 0, 0, 1)](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737430008100-afe1bb05-821b-4253-95a5-4c892fd013af.png)



到目前为止可以发现：**只有粒子系统ParticleSystemRenderer会在scale.x = -1时，改变面朝向，MeshRenderer和SkinnedMeshRenderer都不会有此影响**。



### 抓帧分析ParticleSystemRenderer面朝向如何改变
准备MeshRenderer、ParticleSystemRenderer各两个，且都采用Cube，每一对都是一个scale.x = 1，且采用urplit，另一个scale.x = -1，且采用fishstandard。尽量确保一次抓帧能多一些结论。![抓帧用例](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737431258098-74c9a7cf-c7e3-4f9a-84d8-89bd091686fb.png)



**RenderDoc数据记录**：

1. urplit MeshRenderer scale.x = 1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737430810807-005da8d0-a9b3-47cf-9ddf-1f4ca25ebafc.png)

2. fishstandard MeshRenderer scale.x = -1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737430892049-4600b9dc-62a8-488c-a69a-a9cc8b4bfb22.png)

3. urplit ParticleSystemRenderer scale.x = 1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737431317288-34c5cd36-f3cb-4183-9e04-633a4d7e415d.png)

4. fishstandard ParticleSystemRenderer scale.x = -1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737431334373-7a383fa5-bf28-420f-a020-a2be43ff9ef4.png)

**对比RenderDoc数据，发现:**

1. **MeshRenderer**时，Cube的数据都是正常的模型，positionOS、normal，Vertex和IDX都是符合预期的。能从posOS看出长宽高都是1.  
将顶点0, 1, 2, 3那面当做正面的话，顶点索引顺序是按顺时针排布的。  
且无论scale.x是1还是-1，**顶点索引顺序是一致的，换句话说这里不会修改模型数据，所以任何情况下模型数据就是同一个。**
2. **ParticleSystemRenderer**时，Cube模型像是被重组然后重新计算了一样，postionOS、normal，Vertex和IDX都变了。从positionOS看，长宽高像是变成了13左右。normal也产生了精度损失，且能看出来坐标系的轴变了，所以才会有0.1,0.9之类的值。而顶点顺序更是被重新排序了。  
这里还有一点特别的，**scale.x = -1时，Cube模型数据中的顶点排序变了。将case4中的顶点1440, 1441, 1442, 1443当做正面的话，顶点索引顺序是按逆时针排布的**。  
而参考scale.x = 1时的模型，将case3中的顶点1464, 1465, 1466, 1467当做正面，顶点索引顺序依然是按顺时针排布的。  
这里就是因为ParticleSystemRenderer会重组模型数据，而scale.x的调整也成了重组数据的一个重要影响参数。  
**模型顶点索引顺序，决定了面的朝向，由此就解释了为什么ParticleSystemRenderer下，当scale.x = -1时，模型的面朝向会改变。**
3. 上面观察normal数据时能发现Normal数据对于各自顶点还是正常的，只是坐标系变了所以会有些微偏差。



下面为了进一步准确验证上述结论，将2个MeshRenderer以及2个ParticleSystemRenderer都摆在同一位置，确保没有任何旋转，且放缩一致，再重新抓一帧分析。因为只看vertex shader阶段的数据，所以这里记录shader类型其实没什么意义，只需要关注是MeshRenderer还是Particle，以及scale.x是否为-1。

**RenderDoc数据记录：**

1. MeshRenderer scale.x = 1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737439534049-95da0f1b-181f-4947-84a3-3693d7db249c.png)

2. MeshRenderer scale.x = -1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737439569427-a172ae04-381e-4850-a5d3-97bd531c1d4d.png)

3. ParticleSystemRenderer scale.x = 1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737439619957-2def3f4e-8b98-49e4-a536-eba69c6ef0af.png)

4. ParticleSystemRenderer scale.x = -1

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737439639094-afb3a9b7-2ce0-4184-80ea-124d33a36332.png)



对比数据后，可以再次验证前述结论：

1. **MeshRenderer**时，**顶点索引顺序是一致的，换句话说这里不会修改模型数据，所以任何情况下模型数据就是同一个。**
2. **ParticleSystemRenderer**时，这次根据positionOS就能明确看出来长宽高是15，符合设置，且没有旋转，所以normal数据都是很完整的数。**且当选择同样四个点作为正面时，其他所有点的顶点法线也是都符合对应坐标系预期的，也就是正确的**。  
scale.x = -1后，模型中的顶点排序确实也从顺时针变成了逆时针。由此，**确定了ParticleSystemRenderer下，scale.x为负数时，会改变模型顶点排序，继而造成VFace的改变。**
3. 这里后来还检查了scale.y = -1的情况，确定scale.y = -1的影响和scale.x = -1的影响一致，scale.z也是。同时这里有一点，当出现了两个轴 = -1时，反而会突然出现VFace正确的情况，这里就单纯是两次反向排列后，负负得正了。不知道是不是考虑了这种情况，Shader里也提供了一个API，用于检测当前是不是奇数个scale -1。![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737441023474-ba4789b2-d699-46e2-b8a5-a5425133faf1.png)![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737441031612-428a9c7f-faf5-4c09-981b-8d540f7511f7.png)  


## 五、光照计算 normalWS * facing 的目的
分析完了面朝向改变的根本原因，再确认下normalWS * facing的必要性，毕竟不能简单地直接去掉 * facing。



此必要性主要存在于在面片的情况下，所以下面以面片举例

![左侧：urplit，右侧：fishstandard](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737441799591-62e834c6-7ea8-4755-bdd0-c3afead9cf6f.png)

![左侧：normalWS，右侧：normalWS * VFace](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737441991050-80092d72-9c18-459f-a2f8-6a5489b1ced7.png)

![左侧：normalWS，右侧：normalWS * VFace](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737441983411-f12c5a0f-2524-4a58-a189-01fb488306c0.png)

图中，左侧是urplit，右侧是fishstandard，两者都是plane面片，且开启了双面显示。



在这种情况下，因为面片正反面模型法线一致，如果不乘上VFace，就会导致无从判断哪一面应该接受光照，哪一面不用接受光照。因此 * facing依然是个必要的操作。



## 六、解决方法
因为VFace = 1/-1，GetOddNegativeScale() = 1/-1，所以直接用 VFace * GetOddNegativeScale()就能得到符合预期的值用于判断当前面是否需要接受光照。



这里也考虑了另一种方法，就是在粒子系统里，让使用者通过开关手动禁止该材质 * facing，不过后来测试时发现：因为粒子系统大多是半透明材质，禁止 * facing后，会导致模型里面的面透出来，从而又形成类似之前反面光照不对的问题。

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737443008462-384852e2-9924-4b61-bac7-945096a1c450.png)

因此，后来没有采用这种方法，而是选择让所有面都能使用正确的光照，这样至少避免因为半透明而看见内部明显差异的光照效果。

![](https://cdn.nlark.com/yuque/0/2025/png/1660870/1737443334495-3b56e65f-2a6f-4657-af5f-b5efe2bd82de.png)

# 总结
**ParticleSystemRenderer**会对Mesh进行重组操作，如果此时又修改了scale为负数，很有可能会造成模型数据的反向排列，从而引起各种不明确的问题。

因此对于适用于粒子系统的shader，需要考虑scale为负数造成的反向排列问题，且可以使用GetOddNegativeScale来判断当前是否反向排列。

至于**MeshRenderer以及SkinnedMeshRenderer**则不需要考虑此问题。

