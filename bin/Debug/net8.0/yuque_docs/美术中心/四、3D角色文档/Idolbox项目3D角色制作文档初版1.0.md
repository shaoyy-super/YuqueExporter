# <font style="background-color:#FFFF00;">制作软件版本需求：</font>
 3DMax_2020及以下版本

 Maya_2022及以下版本

 Zbrush_2022及以下版本

 MarvelousDesign_10及以下版本

 SubstancePainter_8.2及以下版本

uUnity2022.3.22 Urp管线

# <font style="background-color:#FFFF00;">制作前先建立角色对应的工程文件夹：</font>
  Max（max  fbx  导入引擎贴图文件）

  Sp（Substance 3D Painter  Substance 3D Designer）

  Tex （材质素材文件）

  ZB （ZBrush文件）

 MD （MD，Style3d文件） 

**<font style="color:red;">（参考目录如下：）</font>**

![](https://cdn.nlark.com/yuque/0/2024/png/43256863/1717408904102-60970c11-3585-4a3a-8cf6-af6fbc0de7bc.png)

# <font style="color:black;background-color:#FFFF00;">3Dmax</font><font style="color:black;background-color:#FFFF00;">模型单位设置需求：厘米</font>
  System Unit Setup :**M**etric=**<font style="color:#333333;background-color:#FFFFFF;">C</font>**<font style="color:#333333;background-color:#FFFFFF;">entimetres</font>

  **<font style="color:#333333;background-color:#FFFFFF;">1</font>**<font style="color:#333333;background-color:#FFFFFF;">Unit=</font>**<font style="color:#333333;background-color:#FFFFFF;">1.0C</font>**<font style="color:#333333;background-color:#FFFFFF;">entimetres</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256863/1717408935433-d316074c-35f8-4b4e-a569-8a75881dc793.png)

# <font style="color:black;background-color:#FFFF00;">制作规范：</font>
**<font style="color:#333333;background-color:#FFFFFF;">模型</font>****<font style="color:#333333;background-color:#FFFFFF;">/</font>****<font style="color:#333333;background-color:#FFFFFF;">拆分：</font>**

<font style="color:#333333;">1)</font><font style="color:#333333;">       </font>**<font style="color:#333333;background-color:#FFFFFF;">面数</font>**<font style="color:#333333;background-color:#FFFFFF;">：总面数控制在</font><font style="color:#333333;background-color:#FFFFFF;">8W-10</font><font style="color:#333333;background-color:#FFFFFF;">万左右（衣服</font><font style="color:#333333;background-color:#FFFFFF;">+</font><font style="color:#333333;background-color:#FFFFFF;">头发</font><font style="color:#333333;background-color:#FFFFFF;">+</font><font style="color:#333333;background-color:#FFFFFF;">角色裸模）。衣服遮挡住的裸模部分需要删除。</font>

<font style="color:#333333;">2)</font><font style="color:#333333;">      </font>**<font style="color:#333333;background-color:#FFFFFF;">拆分</font>**<font style="color:#333333;background-color:#FFFFFF;">：</font>

<font style="color:#333333;background-color:#FFFFFF;">裸模拆分：</font>

![](https://cdn.nlark.com/yuque/0/2024/jpeg/43256863/1718865446676-66ee7c64-e2a1-45cd-aed5-2d78690ec436.jpeg)![](https://cdn.nlark.com/yuque/0/2024/jpeg/43256863/1718865457219-6bb2d059-a820-40fe-b928-1618e495cb3c.jpeg)<font style="color:#333333;background-color:#FFFFFF;">  
  
</font><font style="color:#333333;background-color:#FFFFFF;">换装部位拆分：</font>

<font style="color:#333333;background-color:#FFFFFF;">Hair</font><font style="color:#333333;background-color:#FFFFFF;">  </font><font style="color:#333333;background-color:#FFFFFF;">(</font><font style="color:#333333;background-color:#FFFFFF;">头发</font><font style="color:#333333;background-color:#FFFFFF;">) </font>

<font style="color:#333333;background-color:#FFFFFF;">Top</font><font style="color:#333333;background-color:#FFFFFF;">（头饰）</font>

<font style="color:#333333;background-color:#FFFFFF;">Back</font><font style="color:#333333;background-color:#FFFFFF;">（背饰）</font>

<font style="color:#333333;background-color:#FFFFFF;">Upper(</font><font style="color:#333333;background-color:#FFFFFF;">上衣</font><font style="color:#333333;background-color:#FFFFFF;">)</font>

<font style="color:#333333;background-color:#FFFFFF;">Lower(</font><font style="color:#333333;background-color:#FFFFFF;">裤子类</font><font style="color:#333333;background-color:#FFFFFF;">)</font>

<font style="color:#333333;background-color:#FFFFFF;">Glove</font><font style="color:#333333;background-color:#FFFFFF;">（手饰）</font>

<font style="color:#333333;background-color:#FFFFFF;">Prop</font><font style="color:#333333;background-color:#FFFFFF;">（手持）</font>

<font style="color:#333333;background-color:#FFFFFF;">Shoes</font><font style="color:#333333;background-color:#FFFFFF;">（鞋子）</font>

<font style="color:#333333;background-color:#FFFFFF;">Stage(</font><font style="color:#333333;background-color:#FFFFFF;">舞台装饰</font><font style="color:#333333;background-color:#FFFFFF;">)</font>

<font style="color:#333333;background-color:#FFFFFF;"></font>

      根据项目需求：<font style="color:#333333;background-color:#FFFFFF;">所有衣服，道具部件都</font>需要自由搭配(<font style="background-color:#FFFFFF;">如果有帽子头饰归为Hair)</font>

<font style="background-color:#FFFFFF;">身体模型为方便换装已经进行了相应的拆分，服装制作完成后需要添加在相对应的身体模型上</font><font style="color:#333333;background-color:#FFFFFF;">拆分部件中有暴露在外的皮肤时，皮肤部分沿用裸模贴图，建立多维材质皮肤部分为</font><font style="color:#333333;background-color:#FFFFFF;">ID1</font><font style="color:#333333;background-color:#FFFFFF;">，服装为</font><font style="color:#333333;background-color:#FFFFFF;">ID2</font><font style="color:#333333;background-color:#FFFFFF;">，（被衣服遮挡的部分裸模需要删除）</font>

<font style="color:#333333;background-color:#FFFFFF;">如图：</font>![](https://cdn.nlark.com/yuque/0/2024/png/43256863/1717409025503-3f6cb68c-34b6-4133-9385-7c94141877b2.png)

<font style="color:#333333;background-color:#FFFFFF;">材质属性上分为透明和非透明</font><font style="color:#333333;background-color:#FFFFFF;">2</font><font style="color:#333333;background-color:#FFFFFF;">种</font><font style="color:#333333;background-color:#FFFFFF;">Shader,</font><font style="color:#333333;background-color:#FFFFFF;">模型也做相对应的拆分。</font>

**<font style="color:#333333;background-color:#FFFFFF;">命名：</font>**

<font style="color:#333333;background-color:#FFFFFF;">模型材质贴图命名规则：</font>

<font style="color:#333333;background-color:#FFFFFF;">非透明部分：</font>

<font style="color:#333333;background-color:#FFFFFF;">模型：</font><font style="color:#333333;background-color:#FFFFFF;">F/M(</font><font style="color:#333333;background-color:#FFFFFF;">性别</font><font style="color:#333333;background-color:#FFFFFF;">)_</font><font style="color:#333333;background-color:#FFFFFF;">部件</font><font style="color:#333333;background-color:#FFFFFF;">_</font><font style="color:#333333;background-color:#FFFFFF;">编号</font>

<font style="color:#333333;background-color:#FFFFFF;">材质球：</font><font style="color:#333333;background-color:#FFFFFF;">Mat_F/M(</font><font style="color:#333333;background-color:#FFFFFF;">性别</font><font style="color:#333333;background-color:#FFFFFF;">)_</font><font style="color:#333333;background-color:#FFFFFF;">部件</font><font style="color:#333333;background-color:#FFFFFF;">_</font><font style="color:#333333;background-color:#FFFFFF;">编号</font>

<font style="color:#333333;background-color:#FFFFFF;">贴图：</font><font style="color:#333333;background-color:#FFFFFF;">Tex_CH_F/M(</font><font style="color:#333333;background-color:#FFFFFF;">性别</font><font style="color:#333333;background-color:#FFFFFF;">)_</font><font style="color:#333333;background-color:#FFFFFF;">部件</font><font style="color:#333333;background-color:#FFFFFF;">_</font><font style="color:#333333;background-color:#FFFFFF;">编号</font>

<font style="color:#333333;background-color:#FFFFFF;">透明部分：</font>

<font style="color:#333333;background-color:#FFFFFF;"></font><font style="color:#333333;background-color:#FFFFFF;">模型：</font><font style="color:#333333;background-color:#FFFFFF;">F/M(</font><font style="color:#333333;background-color:#FFFFFF;">性别</font><font style="color:#333333;background-color:#FFFFFF;">)_</font><font style="color:#333333;background-color:#FFFFFF;">部件</font><font style="color:#333333;background-color:#FFFFFF;">_A__</font><font style="color:#333333;background-color:#FFFFFF;">编号</font>

<font style="color:#333333;background-color:#FFFFFF;">材质球：</font><font style="color:#333333;background-color:#FFFFFF;">Mat_F/M(</font><font style="color:#333333;background-color:#FFFFFF;">性别</font><font style="color:#333333;background-color:#FFFFFF;">)_</font><font style="color:#333333;background-color:#FFFFFF;">部件</font><font style="color:#333333;background-color:#FFFFFF;">_A_</font><font style="color:#333333;background-color:#FFFFFF;">编号</font>

<font style="color:#333333;background-color:#FFFFFF;">贴图：</font><font style="color:#333333;background-color:#FFFFFF;">Tex_CH_F/M(</font><font style="color:#333333;background-color:#FFFFFF;">性别</font><font style="color:#333333;background-color:#FFFFFF;">)_</font><font style="color:#333333;background-color:#FFFFFF;">部件</font><font style="color:#333333;background-color:#FFFFFF;">_A_</font><font style="color:#333333;background-color:#FFFFFF;">编号</font>

<font style="color:#333333;background-color:#FFFFFF;">以</font><font style="color:#333333;background-color:#FFFFFF;">F_001</font><font style="color:#333333;background-color:#FFFFFF;">上衣为例</font>

<font style="color:#333333;background-color:#FFFFFF;">非透明材质模型命名为：</font><font style="color:#333333;background-color:#FFFFFF;">F_Upper_001</font>

<font style="color:#333333;background-color:#FFFFFF;"></font><font style="color:#333333;background-color:#FFFFFF;">材质球命名为：</font><font style="color:#333333;background-color:#FFFFFF;">Mat_F_Upper_001</font>

<font style="color:#333333;background-color:#FFFFFF;">贴图命名为：</font><font style="color:#333333;background-color:#FFFFFF;">Tex_CH_ F_Upper_001</font>

<font style="color:#333333;background-color:#FFFFFF;">透明材质模型命名为</font><font style="color:#333333;background-color:#FFFFFF;">:</font><font style="color:#333333;background-color:#FFFFFF;">  </font><font style="color:#333333;background-color:#FFFFFF;">F_Upper_A_001  
</font><font style="color:#333333;background-color:#FFFFFF;">材质球命名为</font><font style="color:#333333;background-color:#FFFFFF;">:</font><font style="color:#333333;background-color:#FFFFFF;">  </font><font style="color:#333333;background-color:#FFFFFF;">Mat_F_Upper_A_001</font>

<font style="color:#333333;background-color:#FFFFFF;">贴图命名为：</font><font style="color:#333333;background-color:#FFFFFF;">Tex_CH_ Upper_A_001</font>

<font style="color:#333333;background-color:#FFFFFF;"></font>

<font style="color:#333333;background-color:#FFFFFF;">模型</font><font style="color:#333333;background-color:#FFFFFF;">uv</font><font style="color:#333333;background-color:#FFFFFF;">贴图制作注意事项：</font>

<font style="color:#333333;">1) </font><font style="color:#333333;background-color:#FFFFFF;">低模拓扑的布线保持顺畅，有骨骼环切线的预留。</font>

<font style="color:#333333;">2) </font><font style="color:#333333;background-color:#FFFFFF;">拓扑阶段考虑好</font><font style="color:#333333;background-color:#FFFFFF;">UV</font><font style="color:#333333;background-color:#FFFFFF;">的切口位置，把切口线尽量隐藏在模型内侧或布缝中。</font>

<font style="color:#333333;">3)  </font><font style="color:#333333;background-color:#FFFFFF;">各个角度检查模型部件，注意封口，防止模型穿帮（衣领，袖口等）。</font>

<font style="color:#333333;">4) </font><font style="color:#333333;background-color:#FFFFFF;">低模转折接近或小于90度的地方，法线及模型容易发黑，模型需要切开UV给到光滑组设置。</font>![](https://cdn.nlark.com/yuque/0/2024/png/43256863/1717409061303-6e6754cd-fb43-4cb3-a8ca-9667b553868a.png)

<font style="color:#333333;">5) </font><font style="color:#333333;background-color:#FFFFFF;">模型如果有内外分层，请确保内外分层的模型布线尽可能一致。</font>

<font style="color:#333333;">6) </font><font style="color:#333333;background-color:#FFFFFF;">UV边缘扩边4-6个像素格，UV边缘尽可能打直，尤其是UV切口的地方（优势在于贴图精度低，模型减面过程中不会出现接缝现象）。</font>

<font style="color:#333333;">7) </font><font style="color:#333333;background-color:#FFFFFF;">UV合理饱满，能做一体的低模尽量做成一体，UV放置在第一象限。</font>

<font style="color:#333333;">8) </font><font style="color:#333333;background-color:#FFFFFF;">角色需要换装，按照一个模型一套贴图的原则，Upper，Lower输出尺寸为2048其他部位输出 1024*1024</font>

<font style="color:#333333;">9) </font><font style="color:#333333;background-color:#FFFFFF;">Normal烘焙 正确烘焙裸模和装备的Normal贴图。检查Normal贴上低模后没有产生扭曲，拉伸，黑化及其他显示不正确的法线信息。</font>

<font style="color:#333333;background-color:#FFFFFF;"></font>

<font style="color:#333333;background-color:#FFFFFF;"></font>

# <font style="background-color:#FFFF00;">输出检查：</font>
1.   轮廓造型准确，布线均匀，没有多边面，软硬边正确

模型面不得大于 4 边面

2.   模型 ID 正确分配，只有一个材质球的情况统一为ID1，有多维材质的话皮肤统一为ID1，服装为ID2

3.   清除多余材质球

4.   Reset XForm 重置变换（清除历史，冻结变换，坐标归0，）

5.    正确的顶点法线

6.    无闪面、漏光、发黑、峭立面、 废线、开放边，无大于 4 边的面

7.    移除孤立点

8.     不得存在多套UV；所有UV必须都在UV框里（不要使用多象限UV)

9.     无破口、破面，无重合面、废点

10.  贴图，模型，材质球名字统一

<font style="color:#333333;background-color:#FFFFFF;"></font>

# <font style="background-color:#FFFF00;"></font>
