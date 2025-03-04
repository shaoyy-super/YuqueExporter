```lua
function SceneBase:Ctor(scene_name)
end
```

scene_name场景名，获取枚举名

_destroyed，这个场景是否存在

调用OnCreate



```lua
function SceneBase:Show()
end
```

调用可以在子类中实现的方法OnShow

暂无参数

_destroyed设为false因为这里正式开始显示场景了



```lua
function SceneBase:Destroy()
end
```

上面的内容同上两部分，最后调用子类中实现的OnDestroy



```lua
-- 脚本对象创建的消息【一个对象只走一次】
function SceneBase:OnCreate()
end
```

【调用】在构造方法中调用

【用法】这个函数并不常用，但是可以用来进行一些基础的设置，完成一些一个对象只进行一次的功能



```lua
-- 场景资源开始加载前的消息【每次加载场景资源执行】
function SceneBase:OnInit()
end
```

【调用】在SceneMgr中的SceneMgr:_Goto被调用，在自己的Ctrl中被实现后就可以用

【用法】用于加载一些场景资源



```lua
-- 进入场景前执行预加载，此时场景资源加载完了，可以加载一些逻辑资源【每次进入场景执行一次】
function SceneBase:DoPreload()
end
```

【调用】场景加载完成后的回调 SceneMgr:_AfterSceneLoaded()中被调用，注意和场景显示的回调SceneMgr:_AfterSceneShown()进行区分

【用法】加载逻辑资源



```lua
-- 场景显示【每次进入场景执行一次】
function SceneBase:OnShow()
end
```

如上，不再多赘述

【调用】在SceneBase中的Show()中被调用

【用法】可以放置进入场景的特效等显示时需要的操作



```lua
-- 场景隐藏【每次离开场景执行一次】
function SceneBase:OnHide()
end
```

【调用】在SceneMgr中有两个地方调用

首先在加载回调中SceneMgr:_AfterSceneLoaded()

除此之外，在SceneMgr:_DestroyScene(scene_enum)中也会调用

【用法】用于处理场景隐藏时需要做的事情，例如关闭音乐，释放注册的资源，总之OnShow做的事情的反向处理等

