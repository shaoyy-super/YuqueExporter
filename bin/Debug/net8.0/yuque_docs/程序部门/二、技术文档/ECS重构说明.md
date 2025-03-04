# 一、简介
内容：对现有ECS系统的重构，主要包括以下四个方面：

          1 逻辑层和表现层的分离

          2 通过事件缓冲队列，去除大量通过标记位检查执行所导致的空转

          3 破除不合理的继承树，改成组合的形式，插入到component和system。

          4 更为严格的代码规范。



目的：1 和性能和架构相关

          2 和性能相关

          3 4 和架构以及代码的可维护性相关

           

注意点：此方案只是理想终极设想，考虑项目需要平滑过渡，不必完全实现。具体实现的先后顺序，以及是否去实现，是否需要调整，需经过开会讨论再确定。



# 二、概要思路
**1 逻辑层和表现层的分离**

内容：希望Lua层只保留逻辑，表现层（Unity引擎相关）都回到C#底层。

目的：1 从设计上具有明显的逻辑和表现分层

          2 逻辑层System可以降频（部分甚至可以降很多）



**2 通过事件缓冲队列，去除大量通过标记位检查执行所导致的空转**

   目的： 降低性能消耗。

              根据目前测试结果，推测这应该是性能消耗的主要原因之一。



**3 破除不合理的继承树**

   	      具体表现在FIshBase、ItemSkillEntity 等继承树。

（这两个目前都是直接继承自EntityBase，违背了ECS的设计思想，和ECS的设计哲学严重相冲突）

              以类似AutoMoveSystem 中 route_updater（FishRouteBase） 的方式更为合理。

              但是route_updater 现在是以成员变量的形式存放于FishBase中。

              更为合理的方式应该是在各自对应的component里存放type以及对应需要存放的数据（type-data map），在对应的system里存放（对应的 type-function map）。在system update里驱动到对应逻辑的时候，根据对应type 获得对应function 去做分叉逻辑，在分叉逻辑中 到对应的component 去获得分叉数据。

              具体的做法我们会在下一节详细步骤里给出示例。可参见具体类图



    目的：遵照ECS的编程范式：

              1 保证不从Entity继承任何子类，一切都以组合的形式，组合到component里。

                 保证Entity component的纯净性。

              2 数据和函数确实有多态需求的，确保最小切口原则，做出一个极小的扁平的继承结构。

                 数据放置在component里，函数放置system里（当然，也可以放在system之外，做成静态函数。或者说把几个函数聚合在一起，做成模板模式。但是总之不能继承Entity）



**4 更为严格的代码规范**

              1> component里禁止放函数，

             2> system里禁止放除了update(以及类似fixupdate，加入有的话)之外的函数

                  除非是用于支撑update的子函数

                  外部不可以直接通过system来调用system的成员函数

             3> 应该编写统一的API接口供外部调用。比如ECSUtil，ECSAPI，ECSDataUtils等。

                   也即，外部有需要，应调用ECSAPI 的函数

                             API和system update 有公用的功能时，抽取到ECSUtil ECSDataUtils去提供支撑。

                             ECSUtil，ECSDataUtils 可以根据需要去分为不同系统，

                              比如AutoMoveUtils，AutoMoveDataUtils，SkillUtils，SkillDataUtils

             可参见具体类图

这部分工作量巨大，需要开会讨论是不是去做。

# 三、详细步骤
**1 逻辑层和表现层的分离**

:::info
1. todo

:::



**2 通过事件缓冲队列，去除大量通过标记位检查执行所导致的空转**

:::info
创建一个 ActionList (或者成为CmdList)

如下图中所示，CmdList内部成员变量 cmdList ，基类为cmdBase ，可以继承出各种Cmd。

cmdList的update会遍历list中的所有cmd ，并调用 execute 函数。

未来如果有时序问题或者优先级问题，可以分为多个队列，在不同时机处理。或者说根据 system type对应不同队列。

可以之后讨论，我过去经验里没有这个需要

===

但是未来如果logicWorld和displayWorld 则需要分别有对应的cmdList

:::

3 **破除不合理的继承树**

:::info
todo

:::

** 4 更为严格的代码规范**

![画板](https://cdn.nlark.com/yuque/0/2025/jpeg/38384792/1736307016174-f866374c-b90c-4d46-b74c-28f93b4b114f.jpeg)





终极情况下的UML图



![](https://cdn.nlark.com/yuque/0/2025/png/38384792/1736402778831-1bbf1d75-cee6-4a81-a5f7-eba192190031.png)

说明：CmdList 承载了取出现有系统里，大量flag触发下一帧调用事件的作用。

目前阶段可以只有一个，未来如果logicWorld和DisplayWorld真正分离，则需要两个分别对应Logic和DisPlay.

需要注意的是，严格的原教旨主义的ECS里，只允许logicWorld往DisplayCmdList塞入cmd，而不可以反过来。

因为应该遵循逻辑驱动表现的原则。











# 四、实际计划
和柴猛讨论之后，觉得可以先给出一个折中的可执行的计划。

**1 除AutoMove外，其他系统考虑直接降频**

   因为其他系统大部分实际上都是逻辑层

   只有automove 掺杂了逻辑层和表现层

   注意：这么做之后 需要看一下其他系统是否因为这个改动而出现小问题



**2 AutoMove 的Apply 考虑不是直接操作transform.position 而是写入到一个Array**

**   然后C#去读取刷新实际的transform**

****

**以上两点为第一批需求**

****

**3 剥离flag空转， 通过actionList (cmdList)的形式 剥离绝大部分system里的逻辑空转**

****

**为第二批需求**

**==**

****

**以上三点为必做内容**

****

**4 对于继承树的破除，但是我个人认为SKill Fish 的继承树重构工作量巨大 收益可能不够大**

**   但是未来如果有扩展，应该按照这样的思路去架构，不应该继续去尝试继承Entity**

**   要不要做，看柴猛的想法。**

****

**5 关于ECSAPI层， ECSLogicUtils 、ECSDataUtils层，以及可能的各个系统的Utils层。**

**    同上，重构工作量巨大，但是至少未来继续扩展，应该按照这个架构来比较合理，否则很多时候会不知道代码放在哪里更为合理，以及会破坏Component 和 system的纯粹性**

****

**6 逻辑层和表现层的绝对分离**

**    **

**7 更为严格的代码规范，比如system里**

**       **

**4-7的必要性有待讨论。**





# 4.2、具体示例
我们通过几个系统的具体分析，来阐述：



**1 ActorSystem LoadSystem**

 1》 lifecheck

 ActorSystem 只依赖于 Actor

```lua
function ActorSystem:Matches(entity)
    if not entity:HasComponent(ComponentType.Actor) then
        return false
    end
    
    return true
end
```

   但是在ActorSystem Update的时候 OnLifeCheck 中明显依赖于 FishComponent

>    	 local com = entity:GetComponent(ComponentType.Fish)
>

 

  这是因为 目前正好只有创建鱼的时候 才会有actor 

     要么在 Match的地方补充 对 FishComponent 的 match 

     要么在 ActorSystem:OnLifeCheck 中 对 FishComponent取回的结果进行检查 if not com

      而我认为最合理的做法 应该是把 这个功能独立出来

      做一个  LifeCheckSystem  和对应的 LifeComponent

      这样功能就会比较单一 不至于把本该属于生命周期检查的代码 和数据 分别放在 Actor 里 和 Fish 里

      同时这个部分也可以做成逻辑帧率

      当然 这么做 很多系统都要拆 可能工作量过于巨大

2》 再比如entity



3》 OnDestroyFish

       依赖于一个 fish_destroy_flag 的

       这里岔开去研究一下鱼死亡的问题 看注释  这里用flag就是为了延迟一帧

```lua
function FishMgr:_OnShadowColliderOut(fish_uid, fish_type)
    local fish = self:GetFish(fish_uid, fish_type)
    if not fish then
        return
    end
    --  直接销毁单位，如果是Boss，要检测是否要再次出场
    if fish:ShadowColliderBorder() then
        fish:GetComponent(ComponentType.Actor).fish_destroy_flag = true
    end
end
```



```lua
-- 超过边界或者超时销毁的延一帧, 当前Timeline系统在AutoMove上面, 会出现AutoMove销毁鱼之后Timeline系统无法正常回收的情况, 故延迟一帧
    self.fish_destroy_flag = nil
```



看上去就是不能直接销毁 而是先打标记 然后下一帧 （因为ActorSystem 是第一个 或者说 在Auto之前）



至于

```lua
function FishCreateSystem:RemoveFish(fish)
    if self.big_reward_mode ~= FishCreateMode.BigReward then
        return
    end
    local fish_com = fish:GetComponent(ComponentType.Fish)
    if fish_com.fish_matrix_index then
        if self.matrix_fish_list[fish_com.fish_matrix_index] == nil then
            self.matrix_fish_list[fish_com.fish_matrix_index] = {}
        end
        self.matrix_fish_list[fish_com.fish_matrix_index][fish.uid] = nil
        self.matrix_fish_entity[fish_com.fish_matrix_index].go:GetComponent(typeof(CS.FishMatrix)):RemoveFish(fish.cs_actor_id)
        fish:GetComponent(ComponentType.AutoMove).disable_move = false
        Debug.LogWarning('remove matrix fish uid '.. tostring(fish.uid) .. ' matrix_index ' .. tostring(fish_com.fish_matrix_index))
    end
end
```

这段代码在干嘛 暂时不是很清楚

但是总而言之 让lifecheck 和死亡 之类  放在actor里是不合适的 PS 什么是 fish matrix？  



3> OnCreateActor 

不需要通过     if not com.cs_actor_id then 这个标记轮询来创建这个 

 而是直接在创建 初始化的时机 去塞一个cmd 去做这件事 （比如 在 add Actor Component 这条鱼之后）

这部分后续代码我不能准确看懂



```lua
function ActorSystem:OnCreateActor(entity)
    local com = entity:GetComponent(ComponentType.Actor)
    if not com.cs_actor_id then
        local load_com = entity:GetComponent(ComponentType.Load)
        local res = BattleRes.res
        local cs_actor_id = res:GetActor(load_com.unit_load_id, Vector3(999, 0, 0), Vector3(0,0,0), com.scale)
        com.cs_actor_id = cs_actor_id
        -- TODO 临时赋值，很多系统在使用，释放暂时交由FishBase:Release管理，后续修改
        entity.cs_actor_id = cs_actor_id

        local factor = ActorTools.GetActorFlattenFactor(cs_actor_id)
        factor = factor * 10
        factor = math.ceil(factor)
        entity.flatten_factor = factor
        --TODO 可能 uid fish_type is_boss 的拆分
        ActorTools.EquipActorInfo(cs_actor_id, entity.uid, entity.fish_type, entity.is_boss)
        self:OnDisableActorAnimator(entity, com)
        self:OnFishBaseShow(entity, com)
        local actor = PrefabMgr:GetComponent(cs_actor_id, "UnityActorCtrl")
        local fish_com = entity:GetComponent(ComponentType.Fish)
        if fish_com.bind_group_id then
            local parent_group = FishMgr:GetFish(fish_com.bind_group_id)
            SetupFishMoveComp(parent_group, fish_com.fish_cfg, actor, true)
            parent_group.blackboard:Set(BlackboardComponent.Type.AutoMoveSystem_OnFishReadyMove, true)
        else
            SetupFishMoveComp(entity, fish_com.fish_cfg, actor, false)
            entity.blackboard:Set(BlackboardComponent.Type.AutoMoveSystem_OnFishReadyMove, true)
        end
        ECS:GetSystem(SystemType.FishCreateSystem):AddFish(entity)
    end
end
```



4 > OnReActiveActor

       类似 去掉flag 貌似是鱼组创完鱼之后  要重新激活鱼 可以直接调用 而不是用标记绕一道



5> OnPlayAnimate 

     OnDisableActorAnimator 等...

     一样

 或者可以简单的说 如果只是一个bool值的flag 驱动下一帧做事 都可以直接变成事件 

这里除了life 而life这样的持续性的检查才帧的需要update 

但是也可以剥离出来 然后降频处理

反过来说这里到底有哪些系统真的需要unity 渲染帧的update呢？

好像其实没有





**2     AutoMoveSystem**

       1 . 从性能来说，仅仅需要把 self:Apply() 这个作用于模型的 提出来 就好了 

           1.1 因为频繁单步地从lua层穿透C#层，修改gameObject.transform的位置会非常消耗。

                 应写入一个arr 并且上层批量去设置transform

           1.2 未来更进一步，可以对automove降频，计算路径，而在unity层去实现拟合。

                 因为贝塞尔之类，并不需要去每个渲染帧都计算，尤其是在低端机上。



      2 update的时候 通过

```lua
if not entity.blackboard:IsEmpty() then
        self:_OnEvent(entity)
```

```lua
--- @param entity EntityBase
function AutoMoveSystem:_OnEvent(entity)
    self:_OnEvent_OnFishReadyMove(entity)
end
```

```lua
function AutoMoveSystem:_OnEvent_OnFishReadyMove(entity)
    local value = entity.blackboard:Get(BlackboardComponent.Type.AutoMoveSystem_OnFishReadyMove, true)
    if value then
        self:_SetFishRoute(entity)
    end
end
```

	

	标记位触发事件，来_SetFishRoute 转换为CmdList驱动会更好。



       





====



**3 BulletSystem**









**4 GunSystem**

**   SkillSystem**



这里有些内容是需要讨论的 因为似乎不改也没关系...

比如严格来讲

1  **is_me 是应该从逻辑层杜绝  **

    原因本来在于isMe 本来应该只是表现层关于表现的不同... 或者说 和输入输出相关



**2  SkillSystem 存在 player_skill_entity  / room_skill_entity**

**    GunSystem 里存在 player_guns_pos/ player_guns_entity /player_guns_pool 等system级别变量。**

   

```lua
self.player_guns_pos = {}
    self.player_guns_entity = {}
    self.player_guns_pool = {}
    self._list_normal_shot_audio_id = { 202, 208, 209, 210 }
    self._list_aim_shot_audio_id = { 223, 224, 225, 225 }
    self._bullet_uid = 10000
    self._select_randoms = {}
    self._big_reward_radius = 20

    self.ROOT_GO = ResAdapter.BulletRoot
    self.ROOT_TRANSFORM = self.ROOT_GO.transform
```

似乎都不应该存在于此处 （system中）

但是好像如果现在动 又会产生巨大影响... 不如保留现状









# 五、提出问题




---

# 六、总结整理（开发完成后撰写）
开发过程中，很可能产生跟上面方案不一致的地方。可能补充了更多的细节内容，可能由于某些未想到的原因推翻了方案中的一些设定。

所有开发完成后有价值的内容，都可以记录在这里。

