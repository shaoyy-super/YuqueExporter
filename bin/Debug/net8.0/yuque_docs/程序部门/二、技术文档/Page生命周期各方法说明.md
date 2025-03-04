1. **文字描述PageMgr：**

**页面管理器是UI控制中最重要的一个结构，通过它不但可以调动各个Scene的资源，也提供给更上曾的UIMgr一些常用的功能，进而实现了对整个游戏UI系统的管理，以下是详细说明**

**页面管理器的基本结构**

PageMgr 通过维护一个内部字段 _field 来跟踪页面管理器的状态。这个字段包括当前页面、即将前往的新页面、页面参数、协程对象等。PageMgr 还使用一个堆栈 page_stack 来管理多层页面（Page）。

**页面注册与记录**

PageMgr 提供了 Register 函数，用于注册页面信息。每个页面信息包括页面控制器（ctrl）、页面枚举值（page_enum）以及页面参数（params）。注册后的页面信息存储在 _field.page_records 中，用于后续的页面切换和管理。

**页面切换逻辑**

页面切换主要通过 Goto 函数实现。Goto 函数接受页面枚举值、参数和进入模式等参数，首先检查是否可以切换到目标页面。然后，检查当前页面是否允许进入新的页面，如果允许，则保存当前页面的参数并将其压入堆栈中。

Goto 函数会启动一个协程 CoroutineGo 来处理页面切换的具体逻辑。根据需要，协程会决定是否直接打开新页面或者切换场景。如果不需要切换场景，协程会直接关闭旧页面并预加载新页面资源，然后显示新页面。

**页面返回与堆栈管理**

页面管理器使用堆栈 page_stack 来管理多层页面。每次切换页面时，当前页面的状态（包括场景、情景和参数）都会被压入堆栈中，以便后续可以返回到之前的页面。

GoBack 函数用于返回到前一个页面。如果没有指定返回的页面枚举值，则返回到堆栈顶部的页面。如果指定了页面枚举值，则在堆栈中查找该页面并返回。返回时，会弹出堆栈中的页面状态并恢复相应的场景和参数。

**特殊页面管理**

PageMgr 提供了 GoHome 函数，用于返回到主页面。这个函数会清空堆栈并返回到主页面，同时可以传递额外的参数，确保在返回主页面后立即执行某些操作。

**协程与异步处理**

PageMgr 使用 Lua 的协程来处理页面切换中的异步操作，例如资源加载和动画过渡。通过协程，页面管理器可以在切换页面时挂起当前操作，等待异步任务完成后再继续执行，从而实现流畅的页面切换体验。



2. **对于 PageBase代码的解释**

```lua
function PageBase:Ctor()
end
```



```lua
---@return PageInfo
function PageBase.GetPageInfo()
end
```

【用法】设置这个功能的默认场景、可存在场景等

【调用】在PageMgr：RegisterCtrls()中调用



```lua
---@param params table @ 进入系统的参数
---@param disable_notice boolean @ 不给出提示文字
function PageBase:CanEnter(params, disable_notice)
    return true
end
```

【用法】进入系统的条件检查，检查是否可以进入系统

【调用】PageMgr:Goto中检查时能否进入时调用



```lua
---@param page_enum PageEnum @ 当前要进入的Page名
function PageBase:OnBeforeEnterPage(page_enum, params)
end
```

【用法】进入功能前被调用（此时，功能状态还停留在上一个功能处），用于设置进入的一些状态，比如调用PageMgr:SetOverrideInfo()设置动态场景信息

【调用】在PageMgr中的function CoroutineGo中使用



```lua
function PageBase:DoPreload(param)
end
```

【用法】切场景开始时执行的方法，切场景开始时的自定义行为，可以预加载一些资源

【调用】在私有的CoroutineGo中的回调中调用，在PageMgr:Init()中当Scene被加载的时候作为回调调用

```lua
function PageBase:OnShow(param)
end
```

【用法】显示时的自定义行为，显示这个功能需要的界面等

【调用】在PageMgr的 OnLoaded()中调用



```lua
function PageBase:OnCloseAni()
end
```

【用法】播放关闭Page的动效表现（协程函数），在Loading开之前执行，协程resume后再开Loading

【调用】PageMgr中CoroutineGo调用关闭多层UI时调用



```lua
function PageBase:OnClose()
end
```

【用法】关闭，功能只是逻辑上的，不像界面和场景有隐藏而不关闭的需求，关闭时的自定义行为，关闭界面等



```lua
function PageBase:SaveParam()
end
```

【用法】功能序列化，压栈的时候会调用到，这里返回的参数在出栈时会传给DoPreload OnShow方法

【调用】保存Param时在Page:Goto中调用

