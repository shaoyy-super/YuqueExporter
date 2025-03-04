# 一：软件规范
MAX版本：2016

帧频：30/S



# **<font style="color:#000000;">二：检查模型</font>**
<font style="color:#000000;">1. 朝向：确认模型自身坐标朝向，让角色面朝世界坐标的前方。</font>

<font style="color:#1a1a1a;">2. 比例：按R查看模型的缩放比例，保证为100。</font>

<font style="color:#1a1a1a;">3. 网格类型：必须保证模型类型为Poly可编辑多边形模式</font>

<font style="color:#1a1a1a;">4. 检查模型是否有破损面，光滑组是否有问题。模型是否居中等问题。</font>

****

# **<font style="color:#000000;">三. 骨骼及特效挂点</font>**
<font style="color:#000000;">创建</font><font style="color:#000000;">3</font><font style="color:#000000;">个虚拟体，分别命名为</font>

<font style="color:#000000;">Root（需勾选骨骼属性）</font>

<font style="color:#000000;">Rotate</font>

<font style="color:#000000;">RootBuff</font>

<font style="color:#000000;">把</font><font style="color:#000000;">Rotate</font><font style="color:#000000;">和</font><font style="color:#000000;">RootBuff</font><font style="color:#000000;">都链接给</font><font style="color:#000000;">Root</font><font style="color:#000000;">下面</font>

<font style="color:#000000;">Bip骨骼勾选如下选项（捕鱼项目通常情况下用不到BIP骨骼，如有用到的也一样进行如下操作。）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1722218367841-8c2b5d4b-2a68-40e7-bcc0-c5f320a7f01d.png)

<font style="color:#000000;">把</font><font style="color:#000000;">Bip001</font><font style="color:#000000;">骨骼链接在</font><font style="color:#000000;">Rotate</font><font style="color:#000000;">下</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1722218367975-e46a9539-2d53-4a72-a6d4-0a8c175fe831.png)

<font style="color:#ff0000;">注意：</font>

<font style="color:#ff0000;">1.ROOT点不能制作动画，否则会导致BUG。</font>

<font style="color:#ff0000;">2.所有动作，要让</font>**<font style="color:#ff0000;">RootBuff</font>**<font style="color:#ff0000;">始终跟随再角色脚下，尤其是角色脱离Root做位移的动作，尤其要注意这一点。</font>

<font style="color:#ff0000;">3.保证层级干净。以Root为主体的骨骼树下不允许链接任何不需要输出的骨骼和辅助物体。 </font>

<font style="color:#ff0000;">4.在制作过程中，用以辅助但最终不参与输出的骨骼，不能链接到主骨骼树上。如有必要，可使用链接约束来制作。</font>



**<font style="color:#1a1a1a;"></font>****<font style="color:#000000;"> 特效挂点</font>**

<font style="color:#000000;">1.顶部 P_Title_01  头部上方用于挂血条之类（链接给Root）</font>

<font style="color:#000000;">2.头部 P_Head_01 对齐到头部骨骼</font>

<font style="color:#000000;">3.胸部 P_Chest_01 挂在最上边一节胸骨上，放在放在胸口处用来挂受击特效，（Y轴朝前）</font>

<font style="color:#000000;">4.左手 P_L_Hand_01 放到手掌中心位置（紧贴手掌皮肤）</font>

<font style="color:#000000;">5.右手 P_R_Hand_01 放到手掌中心位置（紧贴手掌皮肤）</font>

<font style="color:#000000;">6.左脚 P_L_Foot_01 对齐到脚部位骨骼</font>

<font style="color:#000000;">7.右脚 P_R_Foot_01 对齐到脚部位骨骼</font>

<font style="color:#000000;">8.武器 P_Weapon_01 X轴朝前</font>

<font style="color:#000000;">备注：</font>

<font style="color:#000000;">1.同一部位如有多个挂点则编号叠加。</font>

<font style="color:#000000;">2.除P_Title_01需要挂到Root上之外其他挂点全部挂到对应骨骼上。</font>

<font style="color:#000000;">3.特效挂点命名规则为P_部位名称_编号。</font>

<font style="color:#000000;">4.蒙皮时与特效确认除以上基础挂点外是否需要额外添加挂点。</font>

<font style="color:#000000;">在动画制作的时候所有动作的所有挂载点再动作的开始和结束部分需要K上关键帧。</font>

<font style="color:#DF2A3F;">（捕鱼项目根据实际情况添加对应位置的挂点）</font>

<font style="color:#DF2A3F;"></font>

# **<font style="color:#000000;">四. 蒙皮规范</font>**
<font style="color:#000000;">①. </font><font style="color:#000000;">蒙皮设置</font>

<font style="color:#000000;">蒙皮设置中的骨骼影响限制值必须设置为4。</font>

<font style="color:#000000;">②. </font><font style="color:#000000;">蒙皮质量检查规范</font>

<font style="color:#000000;">检查蒙皮需要将每个关节旋转到最大极限，保证形态符合肌肉的正常过渡。</font>

<font style="color:#000000;"></font>

# **<font style="color:#000000;">五. 摄像机规范</font>**
<font style="color:#000000;">①. </font><font style="color:#000000;">摄像机类型</font>

<font style="color:#000000;">摄像机类型必须为自由体类型的</font>

<font style="color:#000000;">②. </font><font style="color:#000000;">摄像机骨骼朝向</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1722218823782-e39950bc-6dde-4c86-bf9c-56025922c4ab.png)

<font style="color:#000000;">③. </font><font style="color:#000000;">技能摄像机参数</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1722220771964-4be77366-6989-42a5-a77b-de041664604b.png)摄像机的分辨率和FOV会根据项目和应用场景的不同儿有些许变化，制作前先确认清楚分辨率和FOV等重要信息。

<font style="color:#000000;">注意：摄像机动画需要单独输出</font>

<font style="color:#000000;"></font>

# **<font style="color:#000000;">六.</font>****<font style="color:#000000;">动画文件的命名及提交</font>**
<font style="color:#000000;">蒙皮文件命名规范：</font>**<font style="color:#000000;">角色编号_Skin_编号.max</font>**

<font style="color:#000000;">动画文件命名规则：</font>**<font style="color:#000000;">@Ani_角色编号_动作名_编号.max</font>**

<font style="color:#000000;">角色名需要跟策划文档中的要求一致.</font>

FBX文件命名格式与max文件保持一致。



<font style="color:#000000;">导出前要检查确认Root的“启用骨骼”参数是否勾上</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1722218986990-2c0633b9-7893-470c-af17-a7de77f0ff29.png)

蒙皮文件需要导出模型加角色骨骼。动画文件仅导出对应骨骼即可。

<font style="color:#000000;">导出时仅选择需要的物体导出,多余的骨骼,比如CS的末节点就可以不导出。</font><font style="color:#ff0000;">不用的骨骼就不要导出!</font>  
提交文件：资产源文件+ 输出FBX文件+动作录屏文件  

动画列表

| <font style="color:#000;">动作名</font> | <font style="color:#000;">中文名</font> | <font style="color:#000;">帧数备注</font><br/><font style="color:#000;"></font> | |
| :---: | :---: | :---: | --- |
| <font style="color:#000;">@Ani_XXX_Run_01 </font> | <font style="color:#000;">位移</font> | <font style="color:#000;">20-80帧根据角色体型而定</font> | <font style="color:#000;">所有鱼都需要做</font> |
| <font style="color:#000;">@Ani_XXX_Born_01</font> | <font style="color:#000;">出场</font> | 60-120帧<font style="color:#000;">角色体型和角色特点而定</font> | |


# 七.命名规则
![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1724401518215-c9a6aac4-797c-467d-9063-c156c9911191.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1724401569901-b07e9a49-9334-4351-a54b-02afa11b74b3.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1724401601617-1da355a2-5123-401d-a384-7466ccf039c0.png)![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1724401657321-9715641f-1a8f-49b4-9405-e10ee27c754d.png)

# 八.资源路径
![](https://cdn.nlark.com/yuque/0/2024/png/43256883/1724401673533-8d0392f3-003a-4280-b793-aa8c8fa12a32.png)



