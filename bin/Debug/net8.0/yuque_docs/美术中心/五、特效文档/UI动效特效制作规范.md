UI动效制作-流程-规范  
流程  
1.接到任务或TB单，需要找策划确认下需要制作的内容，UI动效？UI特效？还是两者都有。  
2.确认需要对接的程序同学是哪一位。  
3.找joy确认下对特效动效有没有特殊表现需求，或者做了大体后确认下效果。  
4.效果做好给我看下。程序功能和效果完成后，最终是要再确认一遍的。  
规范  
1.动效及特效资源存放：在对应功能模块文件夹中创建FX文件夹来存放动效文件和UI特效预制体

![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1733226075113-6e6423af-a583-4a04-86d9-957199c72354.png)

2.资源命名：  
   动效UI特效   P_FX_UI_界面功能说明_01  
   动画控制器    Ani_FX_人名缩写_界面名_01  
动画片段      Ani_FX_人名缩写_界面名_01_说明   （说明部分，进入动画用in，出去动画用out，刷新用refresh，）  
   动画控制器可以双击打开，把其中的片段名用简写代替  
如图例![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1733226089355-a7e4706d-2164-470d-b9a2-1a7c94202c20.png)

命名示例![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1733226097404-672ee498-e86d-4588-ba14-8d908e4c0013.png)



  注意：动画片段默认是循环的，所以根据需求一般要去掉，如图![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1733226120892-7319ce72-b9c4-471f-a563-fabeda5b4393.png)





3. UI特效制作，特效是要存成预制体的，一般不要直接做到界面预制体上，最好由程序去挂载，程序根据我们需求去调节层级，如果有裁切需求，也需要程序帮忙挂一下UIparticle组件  
特效预制体示例![](https://cdn.nlark.com/yuque/0/2024/png/43256850/1733226134783-240c39c8-1087-4076-b558-03d955ab0ed3.png)

更新：用UIparticle组件后，这里不用100的缩放了，在组件里可以填缩放

目前我们UI特效没有后期辉光效果，突出光感需要做假。UI特效材质球可根据情况勾选UI选项，会有写颜色和Alpha的差别，当然别把已有的改坏掉。

特效，注意不要有空粒子系统。



4.做动效的时候，K透明度，是要加Canvas Group组件来K，一般不要直接对Image图的color做动效。  
我们对预制体做更改，要和程序沟通能不能改，加CanvasGroup可以不告知。更改预制体进行提交，如果有冲突，要和程序对一下。

[https://snh48group.yuque.com/g/org-wiki-snh48group-ec9yge/wxfe39/rzhsyyn8fekk34z6/collaborator/join?token=LxDuIoI2gMYvdvDx&source=doc_collaborator#](https://snh48group.yuque.com/g/org-wiki-snh48group-ec9yge/wxfe39/rzhsyyn8fekk34z6/collaborator/join?token=LxDuIoI2gMYvdvDx&source=doc_collaborator#) 《UI Particle基本功能介绍》

