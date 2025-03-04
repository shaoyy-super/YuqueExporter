

## 1、用途
只处理Sprite相关的操作，比较轻量级的资源管理

## 2、用法
```lua
---@param image Image
---@param sprite_name string 
---@param callback Func 设置成功后回调参数，默认为空，参数为Image
SpriteRegistry:SetSprite(image, sprite_name, callback)


-- 例子,设置图片并矫正图片原始大小
local sprite_res = RegistryMgr:GetSprite(UIEnum.Main)
local img = UI_Main_Panel:Get(CompIndex.Image)
sprite_res:SetSprite(img, "xxx_icon", function(image)
  image:SetNativeSize()
end)
```







