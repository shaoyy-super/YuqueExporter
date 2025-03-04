### 1. 初始化和配置
+ **加载依赖**UIMgr 模块首先加载了一些依赖库和模块，例如 UIBase、Stack、UIDefine、CSysConfig 等，这些模块提供了必要的功能和配置支持。
+ **定义本地变量** 定义了一些局部变量和表，如 ctrl_tbl、opened_ui、panel_stack、cur_scenario、has_full_screen 等，用于存储UI控制器、UI状态、堆栈、当前场景、全屏状态等信息。

### 2. 获取和加载配置
+ **获取配置**GetConfig(ui_name) 方法用于通过UI名称从通过CSysConfig从SysDefine 中获取UI的配置，如果找不到配置则记录错误日志。
+ **判断全屏**IsFull(ui_name) 方法通过配置判断UI是否为全屏界面。
+ **包含当前场景**ContainsCurScenario(config_scenarios) 方法用于判断当前场景是否包含在给定的配置场景中。

### 3. 加载和初始化UI
+ **判断UI是否已加载 **IsLoadOk(ui_name) 方法用于判断单个UI是否已经加载完成。
+ **加载UI脚本 **LoadScript(ui_name) 方法用于加载与UI相关的脚本，包括控制器和面板。它使用 pcall 和 require 动态加载脚本，并将控制器实例化后存储在 ctrl_tbl 中。
+ **异步截屏 **Snapshot(ui_name) 方法用于异步截屏，判断是否需要模糊背景，并处理截屏逻辑。
+ **UI加载完成的回调 **OnUILoaded(prefab) 方法是UI资源加载完成后的回调函数，它将加载完成的预制件存储在 prefabs 表中，并继续处理UI的显示逻辑。
+ **加载UI资源 **LoadUI(ui_name, param) 方法用于加载UI资源，它记录加载中的UI，调用 LoadScript 加载脚本，并使用 ResAdapter.LoadUI 加载预制件，设置加载完成的回调。

### 4. 显示和隐藏UI
+ **显示或隐藏UI **ShowOrHide(ui_name, is_show, param) 方法根据 is_show 参数决定显示或隐藏UI。它调用 ShowUI 方法显示UI，或者调用 HideUI 方法隐藏UI。
+ **刷新全屏UI状态 **RefreshFullScreen(is_show_full) 方法用于刷新全屏UI的状态，判断是否有全屏UI显示，并根据结果设置场景是否隐藏。
+ **协程显示UI **CoroutineShowUI(ui_name, params) 方法使用协程异步显示UI，处理UI堆栈操作，调用 ShowOrHide 显示UI，并刷新全屏状态。
+ **销毁或隐藏UI **DestroyOrHide(ui_name, skip_ani) 方法根据UI的配置决定销毁或隐藏UI。如果UI未加载，则不需要处理；如果UI在显示，则调用 HideUI 方法隐藏UI，并在必要时调用 CloseUI 销毁UI。
+ **协程关闭UI **CoroutineCloseUI(ui_name) 方法使用协程异步关闭UI，处理UI堆栈操作，调用 DestroyOrHide 销毁或隐藏UI，并刷新全屏状态。

### 5. 场景切换和清理
+ **场景切换清理 **OnChangeSceneClear() 方法在切换场景时清理UI，清空临时缓存，预加载必要的UI，并销毁或隐藏不需要的UI。
+ **场景切换前预加载 **BeforeChangeScene() 方法在场景切换前处理预加载操作，预加载需要的UI，并设置场景等待预加载完成。

### 6. UI管理接口
+ **尝试关闭UI **TryClose(ui_name) 方法尝试关闭指定的UI，如果UI未显示则不处理。
+ **预加载UI **Preload(ui_name) 方法预加载指定的UI资源。
+ **打开UI **Open(ui_name, params) 方法打开指定的UI，使用协程处理异步操作，调用 CoroutineShowUI 显示UI。
+ **关闭UI **Close(ui_name) 方法关闭指定的UI，使用协程处理异步操作，调用 CoroutineCloseUI 关闭UI。
+ **判断UI是否显示 **IsShowing(ui_name) 方法判断指定的UI是否正在显示。
+ **设置临时缓存状态 **CacheUI(ui_name, is_cache) 方法设置UI的临时缓存状态，控制UI在切换场景后的缓存。
+ **特殊隐藏所有UI **HideAll(is_hide, except) 方法隐藏或还原所有UI，不触发生命周期事件。
+ **获取UI控制器 **GetCtrl(ui_name, disable_hint) 方法获取指定UI的控制器。
+ **移出屏幕 **MoveOutScreen(ui_name, is_out) 方法将指定的UI移出屏幕或还原位置。

### 7. 协程管理
+ **初始化 **Init() 方法注册场景切换的全局事件。
+ **清空所有UI **Clear() 方法清空所有UI，释放资源。
+ **关闭所有二级UI **CloseAllSecondUI() 方法关闭所有二级UI。
+ **协程完成事件 **OnCoroutineFinish() 方法处理所有UI协程完成后的事件。
+ **注册所有UI协程完成事件 **RegAllCoroutineFinish() 方法注册所有UI协程完成事件。
+ **等待UI协程结束 **WaitUICoroutineFinish(co) 方法在协程中等待UI协程结束。

### 


**UIBase类方法说明**

```lua
function UIBase:Ctor(ui_name)
end
```

【用法】构造函数，创建Ctrl类对应的对象

【调用】通过New调用



```lua
function UIBase:OnCreate()
end
```

【用法】一切在Lua对象创建的时候应该进行的内容，在对象存在期间这个方法只会被调用一次

【调用】在UIBase的ShowUI中被调用



```lua
function UIBase:OnShowUI(param)
end
```

【用法】这件事第一次发生在OnCreate之后，处理显示UI时的工作，在每一次显示UI的时候都会使用当通过自己的Ctrl打开UI的时候会被调用。可用于注册按钮事件。

【调用】在UIBase的ShowUI中被调用



```lua
-- 隐藏UI的事件
function UIBase:OnHideUI()
end
```

【用法】和OnShowUI成对，如果Show注册了按钮事件，这里需要释放

【调用】和OnShowUI对应的Hide的地方



```lua
function UIBase:OnDestroyUI()
end
```

【用法】一般用来 RegistryMgr:Destroy

【调用】在UIBase中CloseUI中调用



```lua
function UIBase:OnLeaveAni()
end
```

【用法】用来播放UI消失的时候的动画

【调用】在UIBase中的PlayLeaveAni中被调用



```lua
function UIBase:OnClickBack()
end
```

【用法】点击返回的时候执行这个回调函数，UIBase会自动New一个返回的对象

【调用】在注册的通用返回按钮被按下时调用



```lua
function UIBase:RegisterCommonBack(c4l, back_type, path, title_cfg_id, title_id)
end
function UIBase:RegisterTitle(path, icon, title)
end
```

【用法】调用之前需要先注册，注册可以放在OnCreate中

【调用】暂无调用



```lua
function UIBase:Register(param, ...)
end


function UIBase:UnRegister(evtName)
end

function UIBase:UnRegisterAll()
end
```

【用法】注册事件和对应的响应函数，或者释放事件，释放左右事件  
【调用】在需要的位置调用注册和反注册



```lua
---@param no_warp boolean @ 不自动包装self给回调
function UIBase:AddClick(go, func, no_warp)
end
function UIBase:AddArgClick(go, func, ...)
end
function UIBase:AddLongClick(go, press_fun, click_fun, press_end_fn)
end
function UIBase:AddArgLongClick(go, press_fun, click_fun, press_end_fn, ...)
end
```

【用法】AddClick：添加按钮的点击事件，回调时只携带self+C#的参数 

AddArgClick：添加按钮的点击事件，回调时会携带self+额外参数，自己的参数在前面

AddLongClick：添加按钮的长按事件，回调时会携带self

AddArgLongClick：添加按钮的长按事件，回调时会携带self+额外参数

【调用】在需要的时候在Ctrl脚本中调用

