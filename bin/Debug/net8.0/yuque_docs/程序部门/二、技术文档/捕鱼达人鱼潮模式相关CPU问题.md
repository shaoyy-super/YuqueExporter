

## 热点函数 GetFishAroundByRadius 问题
调用次数太多，用了遍历查找，这里需要优化

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1739873561115-4536edd2-a870-43de-a3be-58a500ce98e7.png)

方案1，直接用Unity自带的 Physics.OverlapSphere  

方案2. 自行采用数据接口，降低查询开销 



ChatGPT的参考

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1739960008643-515a5a94-2ec7-45f6-8467-5154ea1ab18c.png)



```lua
--- 根据鱼的半径获得鱼群 平面距离
function GunUtils:GetFishAroundByRadius(com,radius,target_fish_base)
    local ret = {}
    if target_fish_base:IsDie() then
        return ret
    end
    local count = FishMgr.SortFishInCamera:Count()
    for i = 1, count do
        local fish_base = FishMgr.SortFishInCamera:GetByIndex(i)
        if gunFuncSelectTarget:CheckTargetOk(com, fish_base, true) then
            local dis = gunFuncSelectTarget:GetFishDistance(target_fish_base, fish_base)
            if dis <= radius then
                table.insert(ret, fish_base)
            end
        end
    end
    return ret
end
```



其他问题

代码中GetFishDistance 跨语言调用Vector3 

CheckTargetOk 这个函数考虑不需要在大奖模式中调用



结论：这里先改成Unity自带的碰撞系统测试一下

修改前

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740382686516-bc9cf8d8-3b41-4f2c-95d9-c8a75a8a7cf4.png)

修改后

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740382484051-d698f84b-c19b-4b0b-a96b-8715cbc391b2.png)







## FishSystem.DoUpdate 关于视野的开销较高 
![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1739961524220-54ac19a0-e907-4632-b167-8330a89f0407.png)

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1739960503458-22d64b05-600e-4ec3-8114-3b738bc30e6f.png)

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1739960614653-6dbf94c1-5123-48de-a249-c5bf79c021a8.png)

```lua
  public bool FishInCamera(int uid, bool useIn)
  {
      if (uid == 0)
      {
          return false;
      }
      var fishObject = _fishObjectDic[uid];
      if (fishObject == null || fishObject.fishGo == null)
          return false;

      if (fishObject.fishCollider == null)
      {
          fishObject.fishCollider = fishObject.fishGo.GetComponentInChildren<BoxCollider>(true);
      }
      var pos = useIn ? ConvertFishInPosition(fishObject.fishCollider) : ConvertFishOutPosition(fishObject.fishCollider);
      var distance = FishTools.CameraDistance - fishObject.fishGo.transform.position.y;
      return  FishTools.IsInCamera(pos, distance);
  }

  private Vector3 ConvertFishInPosition(BoxCollider box)
  {
      Vector3 targetPos = box.transform.TransformPoint(new Vector3(box.center.x, box.center.y,
          box.center.z + box.size.z / 2));
      return targetPos;
  }

  // 计算鱼出去的临界点
  private Vector3 ConvertFishOutPosition(BoxCollider box)
  {
      Vector3 targetPos = box.transform.TransformPoint(new Vector3(box.center.x, box.center.y,
          box.center.z - box.size.z / 2));
      return targetPos;
  }
```

这个函数主要用途是用来检测鱼是否进入和离开屏幕空间了

这里有矩阵相关的计算，因为我们游戏是平面的，考虑AABB碰撞，并降低远处的更新频率



ChatGpt参考

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1739961072323-77ffe9c8-2500-44da-88c6-8cb0b97169a0.png)

5. Variable Time-Step AI（变步长 AI 更新）  
概念：让 AI 在不同距离下使用不同的时间步长：  
近距离（10m 内） → 每帧更新  
中距离（10m~50m） → 每 0.5s 更新  
远距离（>50m） → 每 2s 更新

核心代码

```lua
float interval = distance < 10 ? 0.1f : (distance < 50 ? 0.5f : 2f);
if (Time.time > lastUpdate + interval)
{
    lastUpdate = Time.time;
    FindTargets();
}
```



结论：视野相关的计算放在CSharp 不在Lua代码中轮询遍历获取结果

计算采用二维Rect 简单计算

根据离屏幕中心位置的距离，降低更新位置和计算的频率

修改前

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740449407848-e09a2d6d-b27f-4102-9883-4ed9a5416e73.png)

修改后

1

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740449247031-3ef4f048-dca0-4aad-b669-26b1c63206ba.png)

2.事件改成Unity通知

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740450792470-e683950d-56ce-4c95-aa0c-f469b0880e83.png)

3.不再轮询

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740455415284-4f984d1c-b580-4c68-a041-23bcf59d43be.png)

4.JobSystem 之前

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740464232049-660fa55a-d42b-410e-b89c-20240aae3370.png)

JobSystem 之后

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740481183595-6c964119-6740-4286-9806-55aec917879a.png)



最开始

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740628710825-d89d9f23-768b-4c00-87fb-e1469d7c6be5.png)

计算距离替换成JobSystem 影响几乎可以忽略

之前

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740626286065-99d8004b-d848-4529-9a9c-b69dc91ee2cf.png)

之后

测试场景，鱼潮模式，一次鱼潮

![](https://cdn.nlark.com/yuque/0/2025/png/49784329/1740623668215-df00c171-5a31-4068-a59b-13415cfc15dd.png)



## TimelineSystem 问题
实例化问题

1.音频

```basic
SoundManager.GetHandle
UnityEngine.AudioModule.dll!UnityEngine::AudioSource.PlayHelper()
UnityEngine.AudioModule.dll!UnityEngine::AudioSource.Play()
Assembly-CSharp.dll!::<_PlayClip>d__24.MoveNext()
UnityEngine.CoreModule.dll!UnityEngine::SetupCoroutine.InvokeMoveNext()
UnityEngine.CoreModule.dll!UnityEngine::MonoBehaviour.StartCoroutineManaged2()
UnityEngine.CoreModule.dll!UnityEngine::MonoBehaviour.StartCoroutine()
Assembly-CSharp.dll!RS::AudioMgr._Play()
Assembly-CSharp.dll!RS::AudioMgr.PlayByTml()
Assembly-CSharp.dll!Game.Timeline::MetaAudioMixer.OnEnterClip()
UT.Timeline.dll!UT.Timeline::StateMixerBehaviour.OnUpdate()
UT.Timeline.dll!UT.Timeline::MixerBehaviourBase.ProcessFrame()
UnityEngine.DirectorModule.dll!UnityEngine.Playables::PlayableDirector.Evaluate()
TimelineCtrl._Evaluate
UT.Timeline.dll!UT.Timeline::TimelineCtrl._Evaluate()
UT.Timeline.dll!UT.Timeline::TimelineCtrl.Play()
Assembly-CSharp.dll!RS::TmlMgr.InitAndPlay()
Assembly-CSharp.dll!RS::TmlMgr.PlayTml()
Assembly-CSharp.dll!XLua.CSObjectWrap::RSTmlMgrWrap._m_PlayTml()
Assembly-CSharp.dll!XLua.CSObjectWrap::RSTmlMgrWrap._m_PlayTml()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp6()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!RS::LuaManager.OnUpdate()
Game.Meta.dll!Game.Meta::MgrCollection.EachUpdate()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop

```

2.鱼魂

P_FX_UI_FishFide_yuhunkuang_income_01

P_FX_UI_FishFide_yuhunkuang_income_02

3.彩排 GoColorWheel

```basic
GameObject.ActivateAwakeRecursively
GameObject.Activate
UnityEngine.CoreModule.dll!UnityEngine::GameObject.SetActive()
UT.RS.dll!UT.RS.Res::GameObjectPool.SetActive()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
Assembly-CSharp.dll!Game::PrefabMgr.CreateIn()
Assembly-CSharp.dll!XLua.CSObjectWrap::GamePrefabMgrWrap._m_CreateIn()
Assembly-CSharp.dll!XLua.CSObjectWrap::GamePrefabMgrWrap._m_CreateIn()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp20()
UT.Timeline.dll!UT.Timeline::TimelineCtrl.UnityEngine.Playables.INotificationReceiver.OnNotify()
UnityEngine.DirectorModule.dll!UnityEngine.Playables::PlayableDirector.Evaluate()
TimelineCtrl._Evaluate
UT.Timeline.dll!UT.Timeline::TimelineCtrl._Evaluate()
TimelineCtrl.ManualUpdate
UT.Timeline.dll!UT.Timeline::TimelineCtrl.ManualUpdate()
mscorlib.dll!System::Action`1.invoke_void_T()
Assembly-CSharp.dll!::TmlGame._OnUpdate()
UT.Toolkit.dll!UT.Toolkit::Messenger`1.Broadcast()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop

```

4.鱼本体

```basic
Instantiate
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent_Injected()
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
Assembly-CSharp.dll!Game::PrefabMgr.CreateAt()
Assembly-CSharp.dll!Game::ActorResHandler.Assemble()
Game.Meta.dll!Game.Meta::BatchLoader.Get()
Game.Meta.dll!Game.Meta::LoadManager.Get()
Assembly-CSharp.dll!::LoadMgr.GetActorAt()
Assembly-CSharp.dll!XLua.CSObjectWrap::LoadMgrWrap._m_GetActorAt()
Assembly-CSharp.dll!XLua.CSObjectWrap::LoadMgrWrap._m_GetActorAt()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp6()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!RS::LuaManager.OnUpdate()
Game.Meta.dll!Game.Meta::MgrCollection.EachUpdate()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop

```

5.死亡金币效果

P_FX_Coin_xishou_05

```basic
Instantiate
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent_Injected()
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
Assembly-CSharp.dll!Game::PrefabMgr.CreateIn()
Assembly-CSharp.dll!Game::ActorTools.PlayCoinAbsorbEffect()
Assembly-CSharp.dll!XLua.CSObjectWrap::GameActorToolsWrap._m_PlayCoinAbsorbEffect_xlua_st_()
Assembly-CSharp.dll!XLua.CSObjectWrap::GameActorToolsWrap._m_PlayCoinAbsorbEffect_xlua_st_()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp20()
UT.Timeline.dll!UT.Timeline::TimelineCtrl.UnityEngine.Playables.INotificationReceiver.OnNotify()
UnityEngine.DirectorModule.dll!UnityEngine.Playables::PlayableDirector.Evaluate()
TimelineCtrl._Evaluate
UT.Timeline.dll!UT.Timeline::TimelineCtrl._Evaluate()
TimelineCtrl.ManualUpdate
UT.Timeline.dll!UT.Timeline::TimelineCtrl.ManualUpdate()
mscorlib.dll!System::Action`1.invoke_void_T()
Assembly-CSharp.dll!::TmlGame._OnUpdate()
UT.Toolkit.dll!UT.Toolkit::Messenger`1.Broadcast()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop



```

6.鱼的死亡效果特效

P_FX_Fish_die_boom_01_yuchao

P_FX_Fish_die_boom_02

TML_DieEff_Big_1_01_001

```basic
Instantiate
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent_Injected()
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
Assembly-CSharp.dll!Game::PrefabMgr.CreateAt()
Assembly-CSharp.dll!::TimelineResLoader.Generate()
Assembly-CSharp.dll!Game.Timeline::PlayFishDieEffectMixer.OnEnterClip()
UT.Timeline.dll!UT.Timeline::TimingMixerBehaviour.OnUpdate()
UT.Timeline.dll!UT.Timeline::MixerBehaviourBase.ProcessFrame()
UnityEngine.DirectorModule.dll!UnityEngine.Playables::PlayableDirector.Evaluate()
TimelineCtrl._Evaluate
UT.Timeline.dll!UT.Timeline::TimelineCtrl._Evaluate()
UT.Timeline.dll!UT.Timeline::TimelineCtrl.Play()
Assembly-CSharp.dll!RS::TmlMgr.InitAndPlay()
Assembly-CSharp.dll!RS::TmlMgr.PlayTml()
Assembly-CSharp.dll!XLua.CSObjectWrap::RSTmlMgrWrap._m_PlayTml()
Assembly-CSharp.dll!XLua.CSObjectWrap::RSTmlMgrWrap._m_PlayTml()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp6()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!RS::LuaManager.OnUpdate()
Game.Meta.dll!Game.Meta::MgrCollection.EachUpdate()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop

```

7.炮弹Boom 

预制体 P_FX_dajiangyu_paodan_boom

```basic
Instantiate
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent_Injected()
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
UT.RS.dll!UT.RS.Res::GameObjectPool.Get()
Assembly-CSharp.dll!Game::PrefabMgr.CreateIn()
Assembly-CSharp.dll!XLua.CSObjectWrap::GamePrefabMgrWrap._m_CreateIn()
Assembly-CSharp.dll!XLua.CSObjectWrap::GamePrefabMgrWrap._m_CreateIn()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp6()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!::Lua4CS.CallUpdate()
Assembly-CSharp.dll!RS::LuaManager.OnUpdate()
Game.Meta.dll!Game.Meta::MgrCollection.EachUpdate()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop

```

8.数字预制体

```basic
Instantiate
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent_Injected()
UnityEngine.CoreModule.dll!UnityEngine::Object.Internal_InstantiateSingleWithParent()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
UnityEngine.CoreModule.dll!UnityEngine::Object.Instantiate()
Assembly-CSharp.dll!UT.RS.Res::GameObjectCountPool.Get()
Assembly-CSharp.dll!UT.RS.Res::GameObjectCountPool.Get()
Assembly-CSharp.dll!RS::GameObjectCountPoolMgr.CreateIn()
Assembly-CSharp.dll!::UIPrefabHelper.Init()
Assembly-CSharp.dll!::UIPrefabHelper.InitByPrefabName()
Assembly-CSharp.dll!::NumberConvertImage.InitImageListByNum()
Assembly-CSharp.dll!::NumberConvertImage.SetShowNum()
Assembly-CSharp.dll!RS::Component4Lua.SetNumberConvertImageNum()
Assembly-CSharp.dll!XLua.CSObjectWrap::RSComponent4LuaWrap._m_SetNumberConvertImageNum()
Assembly-CSharp.dll!XLua.CSObjectWrap::RSComponent4LuaWrap._m_SetNumberConvertImageNum()
Assembly-CSharp.dll!XLua.LuaDLL::Lua.lua_pcall()
Assembly-CSharp.dll!XLua::DelegateBridge.PCall()
Assembly-CSharp.dll!XLua::DelegateBridge.__Gen_Delegate_Imp20()
UT.Timeline.dll!UT.Timeline::TimelineCtrl.UnityEngine.Playables.INotificationReceiver.OnNotify()
UnityEngine.DirectorModule.dll!UnityEngine.Playables::PlayableDirector.Evaluate()
TimelineCtrl._Evaluate
UT.Timeline.dll!UT.Timeline::TimelineCtrl._Evaluate()
TimelineCtrl.ManualUpdate
UT.Timeline.dll!UT.Timeline::TimelineCtrl.ManualUpdate()
mscorlib.dll!System::Action`1.invoke_void_T()
Assembly-CSharp.dll!::TmlGame._OnUpdate()
UT.Toolkit.dll!UT.Toolkit::Messenger`1.Broadcast()
Game.Meta.dll!Game.Meta::GameRunner.Update()
BehaviourUpdate
Update.ScriptRunBehaviourUpdate
PlayerLoop

```

