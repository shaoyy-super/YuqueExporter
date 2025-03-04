### 序言: 
设计上希望各个模块注册各自的资源注册器, 但在实际使用中会出现多个代码文件同属于同一功能但彼此没有直接参数交互, 导致最终一个模块内注册了多个资源注册器并重复注册销毁. 故使用了RegistryMgr这样一个全局的管理器, 其他功能需要资源注册器时向RegistryMgr获取, 在对应功能退出时用RegistryMgr销毁. 

### RegistryMgr的使用
Res资源注册器内含有Sprite资源注册器相关功能, 一般的UI界面使用SpriteRegistry足以满足功能, 则使用GetSprite和DestroySprite即可. 如果需要其他资源则直接使用GetRes和DestoryRes去获得ResRegistry, <font style="color:#DF2A3F;">且不再需要SpriteRegistry</font>. 

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1714119104321-7159031d-1757-47fa-9032-ace761b60d90.png)

RegistryMgr提供了3个对外接口. 

GetRes方法传入对应的模块名字, 会获得Res资源注册器(如果没有会自动创建并返回)

GetSprite方法传入对应的模块名字, 会获得Sprite资源注册器(如果没有会自动创建并返回)

Destory方法传入对应的模块名字, 会销毁对应模块的资源注册器. 

### 示例
在主线副本界面注册和销毁Sprite资源注册器

```lua
function UI_Adventure_Ctrl:OnShowUI(params)
    self.sprite_registry = RegistryMgr:GetSprite(UIEnum.Adventure)
    self:RefreshData(true)
    self:RefreshUI(true)
end

function UI_Adventure_Ctrl:OnDestroyUI()
    RegistryMgr:Destroy(UIEnum.Adventure)
end
```

在主线副本二级UI下直接获得该Sprite注册器, 不需要销毁

```lua
function UI_AdventureReward_Ctrl:OnShowUI(params)
    self.sprite_registry = RegistryMgr:GetSprite(UIEnum.Adventure)
    self:RefreshData(true)
    self:RefreshUI(true)
    
end

function UI_AdventureReward_Ctrl:OnDestroyUI()
    
end
```



### 注意事项：
+ 资源管理的名字要唯一，一般使用UIEnum或者PageEnum
+ 各自模块使用各自的资源管理，比如不要在背包里使用主城的资源管理
+ 只有设置图片资源的模块，使用 RegistryMgr:GetSprite即可
+ 创建和销毁的生命周期要成对，比如OnCreate创建，OnDestroy销毁，不要OnShow创建，OnDestroy销毁(上边的示例就是错误的示范)

