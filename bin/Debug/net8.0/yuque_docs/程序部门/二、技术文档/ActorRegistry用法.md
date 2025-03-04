

## 1、用途
管理单位相关的资源加载

## 2、用法


业务层一般不需要直接创建ActorRegistry，而是由ResRegistry来统一管理，因为一个模块很少只使用Actor资源，还会牵涉到Sprite、Prefab、Timeline等



（1）预加载单位

`ActorRegistry:Preload(unit_load_id, load_callback) `

根据单位加载id，预加载单位的预制体和状态机

（2）异步获取单位

`ActorRegistry:LoadActor(unit_load_id)`

如果单位已经加载好了，则直接返回（预制体和状态机组装好的单位）

没有预加载则会先预加载，加载完成后再组装返回

（3）同步获取单位

`ActorRegistry:Get(unit_load_id)`

使用之前，必须保证预加载好

（4）释放一个单位  
	`ActorRegistry:Release(actor_id)`

 只是将单位预制体还回池里，并不会立马卸载

（5）卸载一个单位

`ActorRegistry:UnloadActor(actor_id)`

会先释放单位，并将单位资源引用计数-1，当计数等于0，则尝试卸载

（6）释放、卸载所有单位

`ActorRegistry:ReleaseAll()`、`ActorRegistry:UnloadAll()`

一般在离开模块时，会释放并卸载模块加载的所有单位

### 3、注意事项
一般在进入模块前，提前预加载好用到的单位资源，进入到模块后，再调用同步创建的方法

如果用到的单位不确定，也可以调用异步创建单位的方法











