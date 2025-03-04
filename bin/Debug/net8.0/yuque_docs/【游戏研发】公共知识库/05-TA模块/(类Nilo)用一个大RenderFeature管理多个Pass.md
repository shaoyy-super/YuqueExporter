

# 大致功能&用法介绍：
### RenderFeature eg.
 （太长了删了一部分，看个大概意思，后面有详细说明）

```plain
    [Serializable]
    public class NiloToonRendererFeatureSettings
    {
        [Header("Outline settings")]
        public NiloToonToonOutlinePass.Settings outlineSettings = new NiloToonToonOutlinePass.Settings();
        [Header("Misc settings")]
        public NiloToonSetToonParamPass.Settings MiscSettings = new NiloToonSetToonParamPass.Settings();
        [Header("Average URP shadow map")]
        public NiloToonAverageShadowTestRTPass.Settings sphereShadowTestSettings = new NiloToonAverageShadowTestRTPass.Settings();
        [Header("Anime PostProcess")]
        public NiloToonAnimePostProcessPass.Settings animePostProcessSettings = new NiloToonAnimePostProcessPass.Settings();
    }

    [DisallowMultipleRendererFeature]
    public class NiloToonAllInOneRendererFeature : ScriptableRendererFeature
    {
        public NiloToonRendererFeatureSettings settings = new NiloToonRendererFeatureSettings();

        NiloToonSetToonParamPass SetToonParamPass;
        NiloToonAverageShadowTestRTPass SphereShadowTestRTPass;
        NiloToonCharSelfShadowMapRTPass CharSelfShadowMapRTRenderPass;
        DrawSkyboxPass SkyboxRedrawBeforeOpaquePass;
        NiloToonToonOutlinePass ToonOutlinePass;


        public override void Create()
        {
            ReInitPassesIfNeeded();
        }

        private void ReInitPassesIfNeeded()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Create all passes
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (SetToonParamPass == null)
                SetToonParamPass = new NiloToonSetToonParamPass(settings);
            if (SphereShadowTestRTPass == null)
                SphereShadowTestRTPass = new NiloToonAverageShadowTestRTPass(settings);
            if (CharSelfShadowMapRTRenderPass == null)
                CharSelfShadowMapRTRenderPass = new NiloToonCharSelfShadowMapRTPass(settings);
            if (SkyboxRedrawBeforeOpaquePass == null)
                SkyboxRedrawBeforeOpaquePass = new DrawSkyboxPass(RenderPassEvent.BeforeRenderingOpaques);

            // RenderQueueRange.opaque = render queue(0-2500) materials
            if (ToonOutlinePass == null)
                ToonOutlinePass = new NiloToonToonOutlinePass(settings, RenderQueueRange.opaque, "NiloToonToonOutlinePass(Classic outline - OpaqueQueue)");
            if (ToonOutlinePass_RightAfterTransparent == null)
                ToonOutlinePass_RightAfterTransparent = new NiloToonToonOutlinePass(settings, RenderQueueRange.transparent, "NiloToonToonOutlinePass(Classic outline - TransparentQueue)");


            SetToonParamPass.renderPassEvent = RenderPassEvent.BeforeRenderingPrePasses;
            PrepassBufferRTPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques - 2;
            SphereShadowTestRTPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques - 1;
            CharSelfShadowMapRTRenderPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques - 1;
            ScreenSpaceOutlinePass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques - 1; // use BeforeRenderingOpaques because we need depth and normal texture, URP's SSAO is BeforeRenderingOpaques also
            // SkyboxRedrawBeforeOpaquePass's renderPassEvent is BeforeRenderingOpaques (defined when calling the pass's constuctor)

            ToonOutlinePass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox; // use AfterRenderingSkybox instead of BeforeRenderingSkybox, to make "semi-transparent(ZWrite) + outline" blend with skybox correctly

            ToonOutlinePass_RightAfterTransparent.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents + 1; // right after transparent materials finish drawing, draw this outline pass

            ExtraThickOutlinePass.renderPassEvent = settings.outlineSettings.extraThickOutlineRenderTiming; // default use AfterRenderingTransparents, because we want this outline not being blocked by transparent effects

            // AnimePostProcessPass's renderPassEvent will be decided by the pass itself

            UberPostProcessPass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            // a fix to any possible missing/null pass
            ReInitPassesIfNeeded();

            RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            RenderTargetHandle cameraTarget = new RenderTargetHandle();
            cameraTarget.Init("_CameraColorAttachmentA");
            UberPostProcessPass.Setup(cameraTargetDescriptor, cameraTarget);

            // the order of Enqueue matters if they have the same renderPassEvent
            renderer.EnqueuePass(SetToonParamPass);
            renderer.EnqueuePass(SphereShadowTestRTPass);
            renderer.EnqueuePass(CharSelfShadowMapRTRenderPass);
            renderer.EnqueuePass(ToonOutlinePass);

            if (settings.MiscSettings.EnableSkyboxDrawBeforeOpaque && renderingData.cameraData.camera.clearFlags == CameraClearFlags.Skybox)
                renderer.EnqueuePass(SkyboxRedrawBeforeOpaquePass);

            var tonemappingEffect = VolumeManager.instance.stack.GetComponent<NiloToonTonemappingVolume>();
            var bloomEffect = VolumeManager.instance.stack.GetComponent<NiloToonBloomVolume>();
            
            bool isAnyNiloPostEnabled = false;
            isAnyNiloPostEnabled |= tonemappingEffect.IsActive();
            isAnyNiloPostEnabled |= bloomEffect.IsActive() && settings.uberPostProcessSettings.allowRenderNiloToonBloom;

            // optimization when no character is using Color Fill feature
            bool isAnyNiloToonPerCharacterScriptRequiresPrepass = false;
            foreach (var characterRenderController in characterList)
            {
                if (!characterRenderController) continue;
                
                if (characterRenderController.isActiveAndEnabled && 
                    characterRenderController.gameObject.activeInHierarchy && 
                    characterRenderController.shouldRenderCharacterAreaColorFill)
                {
                    isAnyNiloToonPerCharacterScriptRequiresPrepass = true;
                    break;
                }
            }
            renderer.EnqueuePass(UberPostProcessPass);
        }

        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            UberPostProcessPass.Setup(renderingData.cameraData.cameraTargetDescriptor, renderer.cameraColorTargetHandle);
        }


        protected override void Dispose(bool disposing)
        {
            // call Dispose to passes that alloc RT
            PrepassBufferRTPass?.Dispose();
            CharSelfShadowMapRTRenderPass?.Dispose();

            
            // null all passes
            SetToonParamPass = null;
            SphereShadowTestRTPass = null;
            CharSelfShadowMapRTRenderPass = null;
            SkyboxRedrawBeforeOpaquePass = null;
            ToonOutlinePass = null;
            ToonOutlinePass_RightAfterTransparent = null;
        }
```



如上，RenderFeature内只执行最低限度的必要函数，如：Create(), AddRenderPasses(), Dispose()等，以及只传递Pass内需要且只能从RenderFeature内获得的参数，其他的参数与运算全部在具体的Pass内实现



### Volume eg.
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719390062803-23c8551d-0162-4d6c-b905-4c129ea0c65f.png)

继承VolumeComponent类和IPostProcessComponent接口后就能在GlobalVolume - Override添加该Volume

注：还需要在VolumeComponentMenu中添加“路径/名称”才能在GlobalVolume - Override中找到该Volume；以及使用IPostProcessComponent接口后需加入接口所需的IsActive()和IsTileCompatible()函数

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719388677059-f90d1145-285c-4781-addd-4130942ba88a.png)

成功加入GlobalVolume图示



### RenderPass
写法差别不大，就是尽可能把要用的参数和函数写在Pass内，RenderFeature仅调用和传值







# 详细说明：
**<font style="color:#DF2A3F;">RenderFeature, RenderPass 和 Volume保持在同一个namespace下</font>**

### Volume：
#### 基础说明：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719393851918-d4ce7da3-6937-49af-b29b-af4dd74eba75.png)

       Volume类需要序列化，并通过VolumeComponentMenu设置“路径/名称”才能在GlobalVolume添加时找到该Volume（路径不是必需但还是要有，不然全在最外面容易乱）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719394808941-4412c204-bb97-41dc-b957-0b81168269a9.png)

       需要继承VolumeComponent类和IPostProcessComponent接口

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719395045922-0fd494a7-0348-44b8-ae14-b37565eed727.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719395176470-50d809b9-82a3-4f96-af0c-3cd944232087.png)

       因为继承了接口，因此需要实现接口内的成员

       C#接口介绍：[https://blog.csdn.net/sinat_40003796/article/details/125520392](https://blog.csdn.net/sinat_40003796/article/details/125520392)

       IsActive()返回Volume激活状态，可自定义返回值，例图中用useSSAO的开关状态返回组件激活情况，IsActive()可以作为RenderFeature内是否将该Pass加入渲染管线的判断条件

       IsTileCompaatible(）返回对Forward+的支持情况（？），一般直接返回false

       因为目前Volume中只有CustomSSAO相关内容，因此IsActive()中只判断useSSAO的开关情况，后面功能加的多了就需要将每个功能的开关情况都加进来做判断



#### 参数设置：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396090746-f0dd3e4c-10da-4116-8b1d-da87152bb1ed.png)

       Volume内的参数都需要是Parameter类型，例：bool -> BoolParameter，float -> FloatParameter



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396292008-c3a00d01-150b-4bbe-bb8e-7f35272525ad.png)

       像ClampedFloatParamter在编辑器面板内对应的就是滑条，ClampedFloatParamter(默认值，最小值，最大值)



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396471515-6304e1d2-8d45-43e0-ab89-f7daabced080.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396516958-0022e3dd-95e9-41fa-82c9-d8ec694ea77a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396532350-e46b15f8-231d-4bfc-8c8b-99388735f1a2.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396545612-12e4400e-e073-49a3-9bb1-8b5bc0a5d52d.png)

       类似选项框的情况，除了建立对应的Enum类外还需要如上图方法建立对应的sealed类，每个sealed类都需要序列化，否则会报错（后面看看有没有更好的方法）



#### Interface相关：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719396955948-d26e400d-f73e-4bcf-9d49-8872deab66ed.png)

[Header]: 在Interface面板上加一个标题

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719397019060-e3718738-1f86-42ce-ad1e-781168448a44.png)

[ToolTip]：当鼠标移至参数上时的提示信息

[OverrideDisplayName]：显示在面板上的自定义命名，替代参数本来的名称



#### 自定义Volume面板：
新建脚本重写interface面板界面，一般命名规则为”Volume名称 + Editor”

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719397738000-090621c5-6abf-44f0-9823-55fb34a98ad3.png)

示例代码:

```plain
#if UNITY_EDITOR
using UnityEditor.Rendering;
using UnityEditor;


[CustomEditor(typeof(ExampleComponent))]
class ExampleComponentEditor : VolumeComponentEditor
{
    SerializedDataParameter m_Intensity;

    public override void OnEnable()
    {
        var o = new PropertyFetcher<ExampleComponent>(serializedObject);
        m_Intensity = Unpack(o.Find(x => x.intensity));
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("具体属性", EditorStyles.largeLabel);

        PropertyField(m_Intensity);
    }
}
#endif
```

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1720504188838-2547e220-b83b-4b8c-822c-d5699eaf91e6.png)

typeof(XXX)就是需要被重写面板的Volume名

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1720504183436-8490de8a-92a1-46c6-bd5e-dcc1bcccb15c.png)

SerializedDataParameter  XXX 对应需要重写面板的Volume内的可编辑的参数，两边命名需一致

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1720504177302-daaa83e4-f1c0-4896-b745-72a76f6cd3b7.png)

在OnEnable方法中，使用 PropertyFetcher 获取组件中的属性，Unpack(o.Find(x => x.intensity))将两边参数对应上

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1720504169969-921cd80c-fde5-43d1-9592-be9777aead0e.png)

EditorGUILayout.LabelField("具体属性", EditorStyles.largeLabel)：加一个名为“具体属性”的标题

PropertyField(m_SampleParam)：面板上显示对应参数



自定义面板一般用来控制参数的显示与否，例如当useSSAO开关关闭时，SSAO相关参数全部不显示；或是在切换选项时，用来控制选项相关的参数的显示与否

eg.

```plain
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("体积光", EditorStyles.largeLabel);

            PropertyField(m_VolumeLight);
            PropertyField(m_ColorChange);
            PropertyField(m_LightIntensity);
            PropertyField(m_StepSize);
            PropertyField(m_MaxDistance);
            PropertyField(m_MaxStep);
            PropertyField(m_Loop);
            PropertyField(m_Mode);

            BlurMode mode = (BlurMode)m_Mode.value.enumValueIndex;
            if (mode == BlurMode.GaussianBlur)
            {
                EditorGUILayout.LabelField("高斯模糊", EditorStyles.boldLabel);
                PropertyField(m_BlurInt);
                PropertyField(m_Brightness);
            }
            else if (mode == BlurMode.BilateralFilter)
            {
                EditorGUILayout.LabelField("双边滤波", EditorStyles.boldLabel);
                PropertyField(m_Space_S);
                PropertyField(m_Space_R);
                PropertyField(m_KernelSize);
            } 
```

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719398962679-29813c1a-0fe5-4a5f-b234-3f50ded058e7.png)

切换Mode就可以显示不同模式下的参数



### RenderFeature：
#### 目的说明：
在一个RenderFeature内管理多个相关的RenderPass，eg.

EnvironmentAllInOneFeature内管理所有场景相关的Pass，如：SSAO，环境光，雾效等

CharacterAllInOneFeature内管理所有角色相关的Pass，如：角色自阴影，Outline等



#### 基础框架：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719455019383-5fb82896-aae4-40c0-a94e-773326877f9d.png)

使用与Volume和Pass相同的namespace

创建一个参数类，eg.EnvironmentAllInOneSettings，用以获取Feature下管理的所有的Pass的参数

Feature类下new一个上面创建的参数类，声明所有需要管理的Pass

**必要的函数:**

Create(): 创建时会执行几次，后续不再执行

AddRenderPass(): 每帧执行，负责管理Pass的添加与否以及Pass插入的时机

Dispose(): 每帧执行，用于在所有流程执行完毕后释放资源

其他可能用到的函数根据实际情况按需添加



#### 具体设置：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719399328307-01a8f247-d225-4792-bd3a-409e7414bb8f.png)

RenderFeature内建立一个类用来获取所管理Pass内的所有可编辑参数，**序列化后**各项参数在RenderingData内可见，对于各项Pass参数需建立标题以作分隔



Pass内参数类eg.

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719453661289-132abbbc-5777-4d3e-b447-3d621ac11e74.png)

**命名规范：**Pass内参数类的命名统一设置为Settings

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719454726372-8a65c2eb-6cf8-4f31-9dd7-453df61236dd.png)

**命名规范：**Feature内对获取的Pass内Settings类的命名为“Pass全称或简称”+“Settings”



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719456371674-d0fc6e26-b095-45c1-81aa-04a807a5891f.png)

Feature类下new一个上面创建的参数类，用于获取所有Pass内的参数

然后声明所有需要管理的RenderPass



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719456738614-aa1486b6-2a26-4ab7-b928-4303baa5bc97.png)

Create(）函数只在RenderFeature创建时<font style="color:#585A5A;">(一般等同于游戏开始或切换Render Data)</font>执行几次，后续不再执行，因此只在其中**加入仅需在创建时执行的逻辑**

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720064537118-849f2191-2247-4fed-aa12-cd278afedbd6.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1720064554097-cf9e9bfe-fb39-4f79-9fee-0d477be29311.png)



当然，创建时也需要初始化Pass

有时会碰到需要获取Volume参数的情况，如判断XXX功能是否开启等，可以用VolumeManager.instance.stack.GetComponent<XXXVolume>()获取到相应Volume



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719457069441-a6ad41e3-f2d6-4b6d-847a-225eae9b4d09.png)

AddRenderPasses()函数每帧执行，用于管理各Pass的添加情况以及插入的时机

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719457552379-d1b323e6-aa5a-4eb5-8471-79a1cd8e19a1.png)

当Pass的renderPassEvent相同时通过renderer.EnqueuePass(）的先后情况进行排序



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719457676470-e6ab6f99-23d8-4bb3-842c-123187448aad.png)

Dispose(）函数每帧执行，用于在所有流程执行完毕后释放资源

如果Pass内申请了RT，material等资源，则需要此处调用Pass内的Dispose函数进行释放（此处仅调用，具体逻辑在Pass的Dispose函数内实现）

然后将所有管理的Pass置空，下一帧要用时再根据情况进行创建（否则Pass一但创建就会一直存在）



### RenderPass：
Renderpass的大致逻辑与之前还是一样的，只是需要尽量将要用的参数和函数都写在Pass里，只通过RenderFeature传递必要的参数，简言之就是让Pass具有更高的独立性

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719458696002-8ce1b211-8d92-4e44-8338-a351fe227cb3.png)

Settings内添加Pass内会用到的需要暴露出来的可编辑参数，一般与Volume中对应功能的参数一一对应（除非明确部分参数不希望由Volume控制），且双方默认值应保持相同

![Volume中CustomSSAO相关参数](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719459067288-eec1690e-d0c8-4f78-b473-f2d299d12bc6.png)



必要逻辑：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719459659641-2618bb2a-e220-41de-93a6-0df8101359e2.png)

用处：获取RenderFeature内的对应参数

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719459944104-1c8d1d1f-8508-44b4-8a75-0a6e09616db0.png)

大致流程：Pass内建立了一个参数类（只有默认值）-> Feature内拿到了这个类，并且能在Render Data内调参  ->  把调过参的值拿过来备用（上述代码作用）



有时会碰到需要获取Volume参数的情况，可以用VolumeManager.instance.stack.GetComponent<XXXVolume>()获取到相应Volume



其余部分与普通RenderPass做法相同，不再赘述

