简介

Tips系统是山海项目中用于弹出显示各类信息的窗口的管理系统，需要注意区分的是，Tips系统和Message系统存在区别，事实上，确认取消弹框属于Message系统，而物品Tips和宠物Tips等属于Tips系统。Tips系统基于的预制体是UI_Tips_Panel，这一部分所有的预制体在项目中的位置为Assets/Art/ModuleRes/Tips，lua代码所在位置为ExtAssets\Lua\Presentation\CSys\Tips



Tips系统数据传输流程

![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44684279/1727682643151-2c8e60eb-202e-4e9f-9349-9db739139bae.jpeg)

Tips系统Lua代码结构

![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44684279/1727684568383-d3626b71-02ea-4c82-a8f6-af4785101f3c.jpeg)

Tips预制体结构

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1727684250019-7bcd02c3-3526-42b4-958d-8cfa0ca292df.png)





系统结构

TipsVM所属功能

1. 功能入口，在需要弹出Tips的地方，调用TipsVM的最开始的3个方法，即可弹出Tips，如果需要添加新的Tips可以添加新的方法，目前存在的方法：ShowPropTip（支持点击通用物品\宠物头像弹出tips），ShowPetTipWithUid（支持通过宠物的Uid弹出宠物Tips），ShowItemTipWithItemId（支持通过item的item_id打开物品Tips）
2. 矫正Tips坐标

UI_Tips_Ctrl所属功能

这是承载所有Tips的界面的控制器，和其他UI一样，可以通过UIMgr打开，并存在和其他UI一样的生命周期函数，但是和制作其他UI不同的是，我们在扩展Tips功能的时候并不需要更改UI_Tips_Ctrl中的任何东西，所以下面也仅仅是简述它的功能

1. 打开一个Tip预制体（OpenPropTip）：这个方法会管理一个对象池，并通过传入的prop_base（一般来讲是UI_PropBase类型，但是实际上只需要传入prop_base.prop_type和prop_data就可以）获得一个对象池中的tips对象并打开



UI_TipBase所属功能

这是所有Tips控制类都需要继承的父类，其中定义了Tips的构造方法，注意，这个构造方法中存在两个c4l，其中root_c4l是用来控制tips统一处理的东西的，目前主要是处理tips出现的位置。而我们常用的那个c4l组件则被命名为c4l。

在这一脚本中，有几个生命周期函数被定义OnCreate，OnShow，OnHide使用方法和正常的UI一致



UI_XXXTip所属功能

这是可以被扩展的Tips控制类，实际处理Tips下逻辑的脚本。我们需要让这一脚本继承UI_TipBase，并在其中使用生命周期函数实现各种需求



UITipDefine所属功能

这个脚本定义了一些目前支持的Tips类型，如需扩展，可以在这里加入对应的内容，需要注意，在Unity中的预制体名称和Lua脚本名称应该在这里注册，控制这些的Key是UIPropType中的类型，目前仅有Item，Pet，Skill，BigSkill四类，如需扩展，需要在UIPropType中添加新的类型



预制体结构

请参考ItemTips结构

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1727608609132-fc7fa160-90e3-42df-8e08-1197aebdff94.png)

注意，需要区分箭头指着的这两个层级，UI_ItemTip需要在UITipDefine中注册，并挂载一个C4L组件，对应root_c4l，这个C4L组件下需要挂载以下内容

![](https://cdn.nlark.com/yuque/0/2024/png/44684279/1727608736686-857b7057-1bcc-42f0-8668-757a37017bf7.png)

在Root下需要再挂载一个C4L组件，这个C4L对应c4l，这个可以和其他的c4l一样正常使用



扩展流程

![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/44684279/1727685036664-d69bfd0b-a167-455a-a8f3-23d01872d011.jpeg)

