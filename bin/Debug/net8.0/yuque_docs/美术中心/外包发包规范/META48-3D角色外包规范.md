# **一、软件规范**
1. 3DMax_2016及以下版本

2. Zbrush_2022及以下版本

3. SubstancePainter_9.1及以下版本

4. Unity 2022.3.22f1管线

# **二、制作规范**
### **<u><font style="color:rgb(112,48,160);">01.3Dmax模型单位设置需求：</font></u>****<u><font style="color:rgb(112,48,160);background-color:rgb(255,255,0);">厘米</font></u>**
System Unit Setup :Metric=Centimetres

1Unit=1.0Centimetres

![](https://cdn.nlark.com/yuque/0/2024/png/44787879/1722247073596-7f7758c4-f7d5-4f8e-9420-254e32e4d9f5.png)

### **<u><font style="color:rgb(112,48,160);">02.模型尺寸及面数规范</font></u>**
**模型的大致尺寸比例参考下图，具体制作时会提供相应比例的MAX文件作为参考**

![](https://cdn.nlark.com/yuque/0/2024/jpeg/44787879/1722252250307-ec452fdb-a49f-4cdb-b6bc-38e62040995d.jpeg)

**四种鱼按照体型大小制作相应面数（根据效果面数可以减少一些，尽量不要增加面数）**

<font style="color:rgb(0,0,0);">1.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">普通鱼（小）：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">500</font><font style="color:rgb(0,0,0);">三角面以内。</font>

<font style="color:rgb(0,0,0);">2.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">精英鱼（中）：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">2000</font><font style="color:rgb(0,0,0);">三角面以内。</font>

<font style="color:rgb(0,0,0);">3.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">大鱼：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">4000</font><font style="color:rgb(0,0,0);">三角面以内。</font>

<font style="color:rgb(0,0,0);">4. Boss鱼:</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">15000</font><font style="color:rgb(0,0,0);">三角面以内。  
</font>

**<u><font style="color:rgb(112,48,160);">03.贴图尺寸规范</font></u>**

根据不同体型大小的鱼输出相应的贴图尺寸贴图格式<font style="background-color:rgb(255,255,0);">TGA</font>

（制作SP贴图时可以放大贴图尺寸制作，输出规定尺寸）

<font style="color:rgb(0,0,0);">1.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">普通鱼（小）：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">256*256</font><font style="color:rgb(0,0,0);">像素。</font>

<font style="color:rgb(0,0,0);">2.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">精英鱼（中）：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">512*512</font><font style="color:rgb(0,0,0);">像素。</font>

<font style="color:rgb(0,0,0);">3.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">大鱼：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">512*512</font><font style="color:rgb(0,0,0);">像素。</font>

<font style="color:rgb(0,0,0);">4.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">Boss鱼:</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">1024*1024</font><font style="color:rgb(0,0,0);">像素。</font>

### **<u><font style="color:rgb(112,48,160);">04.低模拆分规则</font></u>**
1.无特殊说明的情况下低模一个mash就可以。

2.模型如果有部分需要用到透贴或者半透明贴图，这部分不要跟其他部分做成一体的，透明部分模型需要拆出来，引擎会赋予单独的透明材质球。（命名参考下面的命名规范）

3.如果模型有换装需求的，例如原画指出这条鱼的头或者尾巴可以换装（换成其他鱼的头或者尾巴），那么模型要分为三个mash,贴图也分成三张，由于一张贴图变成3张贴图，贴图尺寸也要缩小，例如原来一张<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">1024*1024</font><font style="color:rgb(0,0,0);">的贴图，现在则做成3张</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">512*512</font><font style="color:rgb(0,0,0);">的贴图。</font>（命名参考下面的命名规范）

### **<u><font style="color:rgb(112,48,160);">05.命名规范</font></u>**
以编号Fish_<font style="background-color:#FCE75A;">2</font><font style="background-color:#81DFE4;">003</font>为例：

（命名中<font style="background-color:#FCE75A;">1</font>代表<font style="color:rgb(0,0,0);">普通鱼，</font><font style="color:rgb(0,0,0);background-color:#FCE75A;">2</font><font style="color:rgb(0,0,0);">代表精英鱼和大鱼，</font><font style="color:rgb(0,0,0);background-color:#FCE75A;">3</font><font style="color:rgb(0,0,0);">代表Boss鱼，</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">003</font><font style="color:rgb(0,0,0);">是鱼的编号）</font>

<font style="color:rgb(0,0,0);">1. max文件命名：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">Fish_2003</font>

<font style="color:rgb(0,0,0);">2. max文件内模型命名：模型只有一个部件的情况下命名为：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">Fish_2003</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">_01</font><font style="color:rgb(0,0,0);">，如果模型有多个部件组成则后缀</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">01</font><font style="color:rgb(0,0,0);">修改为</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">_02，_03</font><font style="color:rgb(0,0,0);">~~~~~~依次类推。</font>

<font style="color:rgb(0,0,0);">材质球命名：</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">Mat_Fish_2003</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">_01</font><font style="color:rgb(0,0,0);">，如果有多张贴图材质球的情况下则后缀</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">01</font><font style="color:rgb(0,0,0);">修改为</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">_02，_03</font><font style="color:rgb(0,0,0);">~~~~~~依次类推。</font>

<font style="color:rgb(0,0,0);">3. 贴图命名规则：贴图后缀为</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">_[D]</font><font style="color:rgb(0,0,0);">的是颜色贴图，</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">_[N]</font><font style="color:rgb(0,0,0);">是法线贴图，</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">_[M]</font><font style="color:rgb(0,0,0);">是高光贴图</font>

<font style="color:rgb(0,0,0);">颜色Tex_CH_</font><font style="color:rgb(0,0,0);background-color:#FBDE28;">Fish_2003</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">_01</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">_[D]</font>

<font style="color:rgb(0,0,0);">法线Tex_CH_</font><font style="color:rgb(0,0,0);background-color:#FBDE28;">Fish_2003</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">_01</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">_[N]</font>

<font style="color:rgb(0,0,0);">高光Tex_CH_</font><font style="color:rgb(0,0,0);background-color:#FBDE28;">Fish_2003</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">_01</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">_[M</font><font style="color:rgb(0,0,0);">]</font>

<font style="color:rgb(0,0,0);">后缀</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">_01</font><font style="color:rgb(0,0,0);">是模型只有一张贴图的后缀编号，如果遇到多张贴图的情况修改后缀为</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">_02,_03~~~~~</font><font style="color:rgb(0,0,0);">以此类推。</font>

<font style="color:rgb(0,0,0);">4.</font><font style="color:rgb(0,0,0);background-color:#FCE75A;">如果遇到同一个模型只需要做贴图换色的情况</font><font style="color:rgb(0,0,0);">,那么在原有编号基础上加字母</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">a,b,c~~~~~</font><font style="color:rgb(0,0,0);">依次类推。</font>

<font style="color:rgb(0,0,0);">例如Fish_2003换色材质球命名为：Mat_Fish_2003</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">a</font><font style="color:rgb(0,0,0);">_01、</font>

<font style="color:rgb(0,0,0);">贴图命名为：</font>

<font style="color:rgb(0,0,0);">颜色Tex_CH_</font><font style="color:rgb(0,0,0);background-color:#FBDE28;">Fish_2003</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">a</font><font style="color:rgb(0,0,0);">_01_[D]</font>

<font style="color:rgb(0,0,0);">法线Tex_CH_</font><font style="color:rgb(0,0,0);background-color:#FBDE28;">Fish_2003</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">a</font><font style="color:rgb(0,0,0);">_01_[N]</font>

<font style="color:rgb(0,0,0);">高光Tex_CH_</font><font style="color:rgb(0,0,0);background-color:#FBDE28;">Fish_2003</font><font style="color:rgb(0,0,0);background-color:#81DFE4;">a</font><font style="color:rgb(0,0,0);">_01_[M]     </font>

### **<u><font style="color:rgb(112,48,160);">06.贴图输出通道说明</font></u>**
<font style="color:rgb(0,0,0);">1.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">有透明贴图的情况下，透明贴图放入，颜色贴图的</font><font style="color:rgb(0,0,0);">alpha通道里</font>

![](https://cdn.nlark.com/yuque/0/2024/png/44787879/1722248574153-5657fd94-f959-4232-b7db-522480af790c.png)

<font style="color:rgb(0,0,0);">2.</font><font style="color:rgb(0,0,0);"> </font><font style="color:rgb(0,0,0);">高光贴图通道说明：</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">R</font><font style="color:rgb(0,0,0);">通道为</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">AO</font><font style="color:rgb(0,0,0);">贴图，</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">G</font><font style="color:rgb(0,0,0);">通道为金属度</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">metallic</font><font style="color:rgb(0,0,0);">贴图，</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">B</font><font style="color:rgb(0,0,0);">通道为</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">roughness </font><font style="color:rgb(0,0,0);">粗糙度贴图，</font><font style="color:rgb(0,0,0);background-color:rgb(0,255,255);">alpha</font><font style="color:rgb(0,0,0);">通道为</font><font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">ems</font><font style="color:rgb(0,0,0);">自发光贴图</font>

![](https://cdn.nlark.com/yuque/0/2024/png/44787879/1722248586117-8bc48f04-ca77-4c87-b710-036f4564f44e.png)

3. SP贴图输出设置说明（参考下图设置，法线用<font style="background-color:#81DFE4;">OpenGL</font>模式）：

![](https://cdn.nlark.com/yuque/0/2024/jpeg/44787879/1722306854514-4d0605ed-3e18-4c45-9dd0-247114be6982.jpeg)

### **<u><font style="color:rgb(112,48,160);">07.文件提交整理规范</font></u>**
**参考以下文件层级格式提交文件（每次提交都要提前整理好文件夹再提交审核）**

![](https://cdn.nlark.com/yuque/0/2024/jpeg/44787879/1722248902836-618ecba1-ece3-493f-8c03-b1181047666e.jpeg)

# **三、美术制作风格参考**
**1.模型比例轮廓可以参考一下真实海洋生物，轮廓可以整体一些不要太细碎，比例偏写实一些，不要太卡通。**

**2.**** ****材质可以参考一些真实的海洋生物，偏写实一些，细节比例适中，不能太小（出现噪点），也不要太大（太卡通）。**

**3. 贴图颜色需要叠加一定的AO和细节明暗效果上去，在只有颜色贴图显示的情况下有结构和明暗关系。AO不要太重造成暗部死黑。明暗部分增一定的颜色变化和冷暖变化，颜色明快不要脏，具体以引擎效果为准。****<font style="background-color:#FBDE28;">贴图制作可以参考下图</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/44787879/1722307225846-8af1fc66-cb63-4920-8dc1-f74035d991de.png)

**<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">风格可以参考下图：</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/44787879/1722251877559-9ac42fdc-fa55-4369-8a18-25806a659910.png)

![](https://cdn.nlark.com/yuque/0/2024/png/44787879/1722251889122-0ce72620-4137-4fb6-bd40-1adfb30b8323.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43298529/1722147241073-2cae854f-52fb-4d54-a400-0ae2fbea137a.png)



# **四、****提交审核流程与****<font style="background-color:rgb(255,255,0);">审核重点</font>**
**<font style="color:rgb(0,0,0);">1. 中模大型提交审核 。    </font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">（大型，结构，比例，轮廓剪影）</font>**

**<font style="color:rgb(0,0,0);">2. 高模完成提交审核   。  </font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">（细节比例，材质质感，整体完成度）</font>**

**<font style="color:rgb(0,0,0);">3. 低模拓扑UV提交审核 。</font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">（低模匹配度，</font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">UV利用率，布线合理注意关节动画位置）</font>**

**<font style="color:rgb(0,0,0);">4. 法线贴图烘焙提交审核 。</font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">（光滑组分配合理，法线显示正确与修复）</font>**

**<font style="color:rgb(0,0,0);">5. SP贴图输出提交审核  。 </font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">（颜色质感正确，适当的</font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">AO光影效果）</font>**

**<font style="color:rgb(0,0,0);">6. 最终文件提交。          </font>****<font style="color:rgb(0,0,0);background-color:rgb(255,255,0);">（文件命名规范等，参考下面的提交检查项）</font>**



# **五、****最终检查提交规范**
**<font style="color:rgb(0,0,0);">1.</font>****<font style="color:rgb(0,0,0);"> </font>****<font style="color:rgb(0,0,0);">文件夹，文件，模型贴图命，名是否正确。</font>**

**<font style="color:rgb(0,0,0);">2.</font>****<font style="color:rgb(0,0,0);"> </font>****<font style="color:rgb(0,0,0);">模型</font>****<font style="color:rgb(0,0,0);">ID 正确分配，只有一个材质球的情况统一为ID1，清除多余材质球。</font>**

**<font style="color:rgb(0,0,0);">3.</font>****<font style="color:rgb(0,0,0);"> </font>****<font style="color:rgb(0,0,0);">Reset XForm 重置变换（清除历史，冻结变换，坐标归0，）。</font>**

**<font style="color:rgb(0,0,0);">4.</font>****<font style="color:rgb(0,0,0);"> </font>****<font style="color:rgb(0,0,0);">无破口、破面，无重合面、废点，正确的顶点法线，移除孤立点。</font>**

**<font style="color:rgb(0,0,0);">5.</font>****<font style="color:rgb(0,0,0);"> </font>****<font style="color:rgb(0,0,0);">无闪面、漏光、发黑、峭立面、</font>****<font style="color:rgb(0,0,0);">废线、开放边，无大于</font>****<font style="color:rgb(0,0,0);">4 边的面。</font>**

**<font style="color:rgb(0,0,0);">6. 不得存在多套UV；所有UV必须都在UV框里（不要使用多象限UV)。</font>**

