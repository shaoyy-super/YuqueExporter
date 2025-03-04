### 对角色播放TML
```lua
local res = RegistryMgr:GetRes('Snippet')
local tml_name = 'TML_GM_Test'
local unit_load_id = 101
-- 预加载角色
res:PreloadActor(unit_load_id, function(ul_id)
    -- 获取角色实例
    local actor_id = res:GetActor(ul_id, Vector3.zero, { x = 1, z = 1 }, 100)
    -- 预加载TML
    res:PreloadTml(tml_name, function(prefab, name)
        -- 对角色播放TML
        res:PlayTml(name, actor_id, nil, nil, nil)
    end)
end)
```



### 播放带视频的TML
```lua
local res = RegistryMgr:GetRes('Snippet')
local tml_name = 'TML_GM_Test'
res:PreloadTml(tml_name, function(prefab, name)
    -- 播带视频的TML，最后一个参数一定传true，需要预处理
    res:PlayTml(name, nil, nil, nil, nil, true)
end)
```



### Lua中强制访问C#的私有成员
```lua
-- 传入一个类，标记可以访问其私有成员
xlua.private_accessible(CS.Game.LogSetting)

-- 私有方法调用示例
CS.Game.LogSetting._Setting4Editor()
```

