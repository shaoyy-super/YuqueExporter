

## 1、用途
提供多种常用的资源管理，比如Sprite、Actor、Prefab、Timeline等，较为重度的资源管理

## 2、用法


（1）Sprite相关接口与SpriteRegistry一致

（2）Actor相关接口与ActorRegistry一致

（3）Prefab相关接口

`ResRegistry:PreloadPrefab(prefab_name, callback)`

这里只会预加载对应名字的预制体，不会自动创建对象池

一般需要使用者自己去实例化和销毁对象

```lua
local res_registry = RegistryMgr:GetRes(PageEnum.Main)
res_registry:PreloadPrefab("xxx_prefab", function(asset)
    local go = BasicEE.Instantiate(asset)
    self.go = go
    --	TODO 其他操作
end)

--...其他逻辑

-- 销毁
if self.go then
  BasicEE.Destroy(self.go, true)
  self.go = nil
end
```



（4）Timeline相关接口

+ 预加载Timeline

`ResRegistry:PreloadTml(prefab_name, callback)`

与预加载prefab类似，不一样的地方是tml预加载完毕会自动创建对象池

+ 播放Timeline

   	`ResRegistry:PlayTml(prefab_name, owner, target)`

 `ResRegistry:PlayTmlById(prefab_name, owner_id, target_id)`

两种方式播放tml，传目标对象或者目标对象的唯一id都可以，都是返回tml唯一id，后续可以根据这个id暂停、监听事件等

+ 暂停Timeline

`ResRegistry:StopTml(tml_id)`

参数是播放tml返回的唯一id

+ 监听Timeline暂停或完成事件

`ResRegistry:ListenTmlComplete(callback, cb_self)`

`ResRegistry:ListenTmlStop(callback, cb_self)`

暂停事件是一定会触发的，完成事件不一定（比如手动调用Stop的时候）

```lua

--	tml完成事件
local function _OnTimelineComplete(tml_id)
    --	TODO 根据tml_id处理自己的逻辑
end

--	tml暂停事件
local function _OnTimelineStop(tml_id)
    --	TODO 根据tml_id处理自己的逻辑
end

local res_registry = RegistryMgr:GetRes(PageEnum.Main)
res_registry:ListenTmlComplete(_OnTimelineComplete)
res_registry:ListenTmlStop(_OnTimelineStop)
```















