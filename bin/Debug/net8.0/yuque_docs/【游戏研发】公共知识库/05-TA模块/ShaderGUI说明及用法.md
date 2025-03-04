# 介绍
Unity 关于材质球在Inspector界面的显示提供了一系列易用的接口。其中就**用法**来说，主要分为两类。

+ 一是 仅使用**Drawer**和**Decorator**，通过修饰Shader里的Properties来为每个属性提供合适的显示界面，而不需要去写具体的GUI脚本。好处是简单方便，效果好，缺点是自定义程度不够高。
+ 二是 通过继承**ShaderGUI**并实现特定着色器的GUI来自定义需要的GUI效果。这个方法的优劣势和前者恰恰相反。两者互相补充。

一般来说，平常使用中，仅使用Drawer和Decorator已经是绰绰有余了。如果还有额外需求的话，比起给特定Shader实现特定的ShaderGUI，开发或引入ShaderGUI相关的插件来扩展Drawer和Decorator的功能显得更为合理。后面将要介绍的LWGUI正是对Drawer和Decorator做了不少扩展。

> + Drawer 主要用于改变字段的显示和交互方式：
>     - 可以完全自定义字段的显示方式，例如使用滑块、颜色选择器、下拉菜单等代替默认的文本框。
>     - 可以添加自定义的按钮、事件和逻辑。
>     - 可以用于显示和编辑复杂的数据类型，例如自定义结构体、列表和字典等。
>     - 一个属性只能有一个Drawer。
> + Decorator 主要用于增强 Inspector 面板的可读性和组织性：
>     - 添加标题、分隔线、空白间距等，使 Inspector 面板更易于阅读。
>     - 添加帮助信息、提示信息、警告信息等，帮助开发者理解和使用字段。
>     - 无法改变字段的显示和交互方式。
>     - 一个属性可以有多个Decorator。
>



## Builtin ShaderGUI
主要介绍一下内置Drawer和Decorator的用法

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721819599267-b73cce67-6889-4ad9-9091-558c78711504.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721819650110-fd5d9e05-f84f-4a0f-819e-b5750fd9e4c9.png)

```cpp
Shader "Test/test_shader_gui_builtin"
{
    Properties
    {
        [Header]
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        
        [Header(Extra Settings)]
        [Space]
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        [Space]
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Header(Toggle)]
        // 测试Toggle
        [Toggle] _TestTog ("Test Tog", Float) = 0
        [Toggle(TEST_TOGGLE_FEATURE)] _TestToggleFeature ("Test Toggle Feature", float) = 0
        
        [Header(ToggleOff)]
        // 测试ToggleOff
        [ToggleOff] _TestTogOff ("Test Tog", Float) = 0
        [ToggleOff(TEST_TOGGLEOFF_FEATURE)] _TestToggleOffFeature ("Test ToggleOff Feature", float) = 0
        
        [Header(Enum)]
        // 测试Enum
        [Enum] _TestEnum ("Test Enum", Float) = 0
        [Enum(Meta48.Rendering.Tests.EBlendModes)] _TestenumEnum ("Test enum Enum", Float) = 1
        [Enum(One, 1, SrcAlpha, 5)] _TestManualEnum ("Test manual Enum", Float) = 1
        
        [Header(KeywordEnum)]
        // 测试KeywordEnum
        [KeywordEnum] _TestKeywordEnum ("Test Keyword Enum", Float) = 0
        [KeywordEnum(None, Add, Multiply)] _TestKeywordEnumWith ("Test Keyword Enum With", Float) = 0
        
        [Header(PowerSlider)]
        // 测试PowerSlider
        [PowerSlider] _TestPowerSlider ("Test PowerSlider", Range(0.01, 1)) = 0.5
        [PowerSlider(3.0)] _TestPowerSliderWithCurve ("Test PowerSlider with curve", Range(0.01, 1)) = 0.5
        
        [Header(IntRange)]
        // 测试IntRange
        [IntRange] _TestIntRange ("Test IntRange", Range(0, 10)) = 0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        // test for Toggle, ToggleOff
        #pragma multi_compile _ _TESTTOG_ON
        #pragma multi_compile _ TEST_TOGGLE_FEATURE
        #pragma multi_compile _ _TESTTOGOFF_OFF
        #pragma multi_compile _ TEST_TOGGLEOFF_FEATURE

        // test for KeywordEnum
        #pragma multi_compile _ _TESTKEYWORDENUMWITH_NONE _TESTKEYWORDENUMWITH_ADD _TESTKEYWORDENUMWITH_MULTIPLY
        
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

```

```csharp
namespace Meta48.Rendering.Tests
{
    public enum EBlendModes
    {
        One = 1,
        SrcAlpha = 5
    }
    
    public class TestBuiltinShaderGUI
    {
    }
}
```



## LWGUI
### 优势
+ **轻量**: LWGUI使用简洁的Material Property Drawer语法，无需编写大量代码，即可实现功能强大的Shader GUI。
+ **灵活**: LWGUI提供了丰富的Drawer和Decorator，可以满足各种Shader GUI的需求。同时，开发者还可以自定义Header、Footer和Drawer，实现更高级的功能。
+ **强大**: LWGUI经过多个大型商业项目的验证，稳定可靠。它还提供了一些比Unity内置更强大的功能，例如Unreal风格的Ramp Map编辑器。

### 安装和使用
**安装**:

+ 打开Unity项目，通过Package Manager添加Git链接安装：[https://github.com/JasonMa0012/LWGUI.git](https://github.com/JasonMa0012/LWGUI.git)
+ 也可以手动下载Zip包，然后通过Package Manager导入本地包。

**使用**:

+ 在Shader代码最底部，最后一个大括号之前添加一行代码： CustomEditor "LWGUI.LWGUI"

### 核心组件
#### Drawers
Drawers用于绘制Shader属性，LWGUI提供了多种Drawer，例如：

+ **Main & Sub**: 用于创建折叠组，将相关的属性归类在一起。
+ **SubToggle**: 类似于Unity内置的Toggle，用于控制开关选项。
+ **SubPowerSlider**: 类似于Unity内置的PowerSlider，用于控制带有指数曲线的数值。
+ **SubIntRange**: 类似于Unity内置的IntRange。
+ **MinMaxSlider**: 用于同时控制两个数值的最小值和最大值。
+ **KWEnum & SubEnum/SubKeywordEnum**: 用于创建枚举下拉菜单，并可以与关键字关联。
+ **Tex & Color**: 用于绘制纹理属性和颜色属性。
+ **Image**: 用于在Shader GUI中显示只读的纹理预览图。
+ **Channel**: 用于选择纹理采样通道。
+ **Ramp**: 用于创建和编辑Unreal风格的Ramp Map。
+ **Preset**: 用于保存和加载Shader属性预设。

#### Decorators
Decorators用于装饰Shader属性，例如添加标题、提示信息等，LWGUI提供了一些常用的Decorator:

+ **Title & SubTitle**: 用于添加标题和子标题。
+ **Tooltip & Helpbox**: 用于添加工具提示和帮助信息。
+ **PassSwitch**: 用于配合Toggle控制Shader Pass的开关。
+ **Advanced & AdvancedHeaderProperty**: 用于将属性折叠到高级设置中。
+ **Hidden**: 用于隐藏属性，但可以通过显示模式按钮取消隐藏。
+ **ReadOnly**: 用于将属性设置为只读。
+ **ShowIf**: 用于根据条件控制属性的显示和隐藏。

#### **一些参数的惯例用法**
+ **keyword**："_" = ignore, none or "__" = Property Name +  "_ON", always Upper (Default: none)
+ **group**：father group name, support suffix keyword for conditional display (Default: none)

#### 自定义Shader GUI (除非有定制具体ShaderGUI相关需求，否则一般用不上)
LWGUI还支持自定义Shader GUI，开发者可以：

+ **自定义Header和Footer**: 在Shader GUI的顶部和底部添加自定义模块。
+ **自定义Drawer**: 创建新的Drawer来满足特定需求。



### 示例
#### Drawers
+ **Main & Sub**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888634091-4629e224-90fd-4af3-a320-df477012f749.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888644471-33b113fd-e2c7-4ea7-8d4b-b1c1dfdd799e.png)

```cpp
        // 测试 Main & Sub
        [Title(Main and Sub)]
        
        [Main] _TestMainGroupEmpty ("Test Main Group Empty", Float) = 0
        
        [Main(Group1Empty)] _TestMainGroup1Empty ("Test Main Group1 Empty", Float) = 0
        
        [Main(Group2Empty, _GROUPKEY)] _TestMainGroup2KeyEmpty ("Test Main Group2 Empty with Keyword", Float) = 0
        
        [Main(Group3Empty, _, on, off)]
        _TestMainGroup3Empty ("Test Main Group3 Empty with folding and toggle", Float) = 0
        [Sub(Group3Empty)] _TestSubWithGroup3Empty ("Test Sub With Group3Empty", Float) = 0
        [Sub(Group3Empty_GROUPKEY)] _TestSubWithGroup3EmptyAndKey ("Test Sub With Group3EmptyAndKey", Float) = 0
        
        [Sub] _TestSubEmpty ("Test Sub Empty", Float) = 0
        [Sub(Group2Empty)] _TestSubWithGroup2Empty ("Test Sub With Group2Empty", Float) = 0
        [Sub(Group2Empty_GROUPKEY)] _TestSubWithGroup2EmptyAndKey ("Test Sub With Group2EmptyAndKey", Float) = 0
        
        [Sub(Group1Empty)] _TestSubWithGroup1Empty ("Test Sub With Group1Empty", Float) = 0
        [Sub(Group1Empty_GROUPKEY)] _TestSubWithGroup1EmptyAndKey ("Test Sub With Group1EmptyAndKey", Float) = 0
        
        [Sub(_)] _TestSubWithGroupNotExist ("Test Sub With GroupNotExist", Float) = 0
        [Sub(__GROUPKEY)] _TestSubWithGroupNotExistAndKey ("Test Sub With GroupNotExistAndKey", Float) = 0
```



+ **SubToggle**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888659232-2646c2be-1a91-4952-b38c-09c76e3b8983.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888669564-0f0c5114-5bca-432f-9cb4-60534b1de971.png)

```cpp
        // 测试 SubToggle
        [Title(SubToggle)]
        [Main(GroupSubToggle, _, off, off)] _TestSubToggleGroup ("Test SubToggle Group", Float) = 0
        
        [SubToggle] _TestSubToggle ("Test Sub Toggle", Float) = 0
        [SubToggle(GroupSubToggle)] _TestSubToggleInGroup ("Test Sub Toggle in Group", Float) = 0
        [SubToggle(GroupSubToggle, _)] _TestSubToggleWithoutKey ("Test Sub Toggle Without Key", Float) = 0
```



+ **SubPowerSlider**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888681513-7dcd0517-b6a7-4caa-bb5d-fa46dd17a7cd.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888692140-b008b581-7196-4740-9a5c-6c3f933070ca.png)

```cpp
        [Title(SubPowerSlider)]
        [Main(GroupSubPowerSlider, _, off, off)] _TestSubPowerSliderGroup ("Test SubPowerSlider Group", Float) = 0
        
        [SubPowerSlider] _TestSubPowerSlider ("Test Sub PowerSlider", Range(0.01, 1)) = 0.5
        [SubPowerSlider(0.3)] _TestSubPowerSliderWithCurve ("Test Sub PowerSlider With Curve", Range(0.01, 1)) = 0.5
        [SubPowerSlider(GroupSubPowerSlider)] _testSubPowerSliderInGroup ("Test Sub PowerSlider in Group", Range(0.01, 1)) = 0.5
        [SubPowerSlider(GroupSubPowerSlider, 9.0)] _testSubPowerSliderWithCurveInGroup ("Test Sub PowerSlider with Curve in Group", Range(0.01, 1)) = 0.5
```



+ **SubIntRange**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888753944-30d01c3a-fd6b-42db-a728-0bea0e3d032d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888762193-74d2a1f3-07fc-4c72-ab66-751152343610.png)

```cpp
        // 测试 SubIntRange
        [Title(SubIntRange)]
        [Main(GroupSubIntRange, _, off, off)] _TestSubIntRangeGroup ("Test SubIntRange Group", Float) = 0
        
        [SubIntRange] _TestIrregularSubIntRange ("Test Irregular Sub IntRange", Range(0.01, 4)) = 3
        [SubIntRange] _TestSubIntRange ("Test Sub IntRange", Range(1, 4)) = 3
        [SubIntRange(GroupSubIntRange)] _TestIrregularSubIntRangeInGroup ("Test Irregular Sub IntRange in Group", Range(1, 4)) = 3
        [SubIntRange(GroupSubIntRange)] _TestSubIntRangeInGroup ("Test Sub IntRange in Group", Range(0.01, 4)) = 3
```



+ **MinMaxSlider**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888768785-80281e4c-a26c-4502-909d-7be99648d714.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888775466-7ae7d1f1-e94a-46bd-a689-2301fa157b66.png)

```cpp
        // 测试 MinMaxSlider
        [Title(MinMaxSlider)]
        [Main(GroupMinMaxSlider, _, off, off)] _TestMinMaxSliderGroup ("Test MinMaxSlider Group", Float) = 0
        
        [MinMaxSlider(_TestMinMaxSliderRangeStart, _TestMinMaxSliderRangeEnd)] _TestMinMaxSlider ("Min Max Slider (0 - 1)", Range(0.0, 1.0)) = 1.0
        [Hidden] _TestMinMaxSliderRangeStart ("Range Start", Range(0.0, 0.5)) = 0.0
        [Hidden] _TestMinMaxSliderRangeEnd ("Range End", Range(0.5, 1.0)) = 1.0
        [MinMaxSlider(GroupMinMaxSlider, _TestMinMaxSliderRangeStartInGroup, _TestMinMaxSliderRangeEndInGroup)] _TestMinMaxSliderInGroup ("Min Max Slider (0 - 1) in Group", Range(0.0, 1.0)) = 1.0
        [Sub(GroupMinMaxSlider)] _TestMinMaxSliderRangeStartInGroup ("Range Start in Group", Range(0.0, 0.5)) = 0.5
        [Sub(GroupMinMaxSlider)] _TestMinMaxSliderRangeEndInGroup ("Range End in Group", Range(0.5, 1.0)) = 1.0
```



+ **KWEnum & SubEnum/SubKeywordEnum**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888785085-e6900417-86b3-4930-914c-2e18c608610b.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888796411-fe12c913-2d9c-460c-8e64-5e94858db95d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888835078-c813383e-f902-44c8-b556-94d7d81cd459.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888842001-c6a84d44-fc4a-4bd6-8d83-42ae28b6ed40.png)

> + SubEnum、SubKeywordEnum与Unity内置的Enum、KeywordEnum用法完全一致，除了多一个分组。
> + KWEnum的区别就是可以将显示在UI上的enum名字和实际添加的关键字解耦开，实现在ui上显示Add，关键字则添加ABBCD。比较类似Enum的用法二：One, 1, SrcAlpha, 5，显示One和SrcAlpha，但实际赋予变量的值是1或5。KeywordEnum关键字只能是变量后面追加显示名。
>

```cpp
        // 测试 KWEnum，主要用于激活Keywords
        [Title(KWEnum)]
        [Main(GroupKWEnum, _, off, off)] _TestKWEnumGroup ("Test KWEnum Group", Float) = 0
        
        [KWEnum] _TestKWEnum ("Test KWEnum", Float) = 0
        [KWEnum(none, _TESTKWENUM2)] _TestKWEnum2 ("Test KWEnum2", Float) = 0
        [KWEnum(none, _TESTKWENUM3_NONE, add, _TESTKWENUM3_ADD)] _TestKWEnum3 ("Test KWEnum3", Float) = 0
        [KWEnum(GroupKWEnum)] _TestKWEnumInGroup ("Test KWEnum in Group", Float) = 0
        [KWEnum(GroupKWEnum, none, _TESTKWENUM2INGROUP)] _TestKWEnum2InGroup ("Test KWEnum2 in Group", Float) = 0
        [KWEnum(GroupKWEnum, none, _TESTKWENUM3INGROUP_NONE, add, _TESTKWENUM3INGROUP_ADD)] _TestKWEnum3InGroup ("Test KWEnum3 in Group", Float) = 0
        
        // 测试 SubEnum & SubKeywordEnum
        [Title(SubEnum and SubKeywordEnum)]
        [Main(GroupSubEnumAndSubKeywordEnum, _, off, off)] _TestSubEnumAndSubKeywordEnumGroup ("Test SubEnum and SubKeywordEnum Group", Float) = 0
        
        [SubEnum(GroupSubEnumAndSubKeywordEnum)] _TestSubEnum ("Test SubEnum", Float) = 0
        [SubEnum(GroupSubEnumAndSubKeywordEnum, Meta48.Rendering.Tests.EBlendModes)] _TestenumSubEnum ("Test enum SubEnum", Float) = 1
        [SubEnum(GroupSubEnumAndSubKeywordEnum, One, 1, SrcAlpha, 5)] _TestManualSubEnum ("Test manual SubEnum", Float) = 1
        
        [SubKeywordEnum] _TestKeywordEnum ("Test Keyword Enum", Float) = 0
        [SubKeywordEnum(GroupSubEnumAndSubKeywordEnum, None, Add, Multiply)] _TestKeywordEnumWith ("Test Keyword Enum With Keywords", Float) = 0
```



+ **Tex & Color**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888848043-59e00370-1f4b-4bcb-98c3-e42d15aeca6d.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888854194-4864520a-cc31-44d8-97e7-415d69d82231.png)

```cpp
        // 测试 Tex & Color
        [Title(Tex and Color)]
        [Main(GroupTexAndColor, _, off, off)] _TestTexAndColorGroup ("Test Tex and Color Group", Float) = 0
        
        [Tex] _TestTex ("Test Tex", 2D) = "white" {}
        [Tex(GroupTexAndColor)] _TestTexInGroup ("Test Tex in Group", 2D) = "white" {}
        [Tex(GroupTexAndColor, _TestColorInTex)] _TestTex_Color ("Test Tex with Color", 2D) = "white" {}
        [Hidden] _TestColorInTex (" ", Color) = (1, 0, 0, 1)
        [Tex(GroupTexAndColor, _TestVectorInTex)] _TestTex_Vector ("Test Tex with Vector", 2D) = "white" {}
        [Hidden] _TestVectorInTex (" ", Vector) = (0, 0, 0, 1)
        
        [Color(GroupTexAndColor, _TestColor2, _TestColor3, _TestColor4)]
        _TestColor ("Multi Color", Color) = (1, 1, 1, 1)
        [Hidden] _TestColor2 (" ", Color) = (1, 0, 0, 1)
        [Hidden] _TestColor3 (" ", Color) = (0, 1, 0, 1)
        [Hidden] _TestColor4 (" ", Color) = (0, 0, 1, 1)
```



+ **Image**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888867352-dacda830-f3ea-47c2-8827-5a4654376e35.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888876407-6c4640c5-b6d3-4ead-a6fb-c586dd68cceb.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888893918-548c4b0f-5e5f-4bdb-aa0d-15779b87ad6a.png)

```cpp
        // 测试 Image，不会参与build
        [Title(Image)]
        [Main(GroupImage, _, off, off)] _TestImageGroup ("Test Image Group", Float) = 0
        
        [Image] _TestImage ("Test Image", 2D) = "black" {}
        [Image(GroupImage)] _TestImageInGroup ("Test Image in Group", 2D) = "yellow" {}
```



+ **Channel**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888902518-b99b5e5a-5f99-4e8f-90b0-e34401db4164.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888910083-229a4437-5ae0-4aca-a36b-6e8bac3c33b5.png)

```cpp
        // 测试 Channel
        [Title(Channel)]
        [Main(GroupChannel, _, off, off)] _TestChannelGroup ("Test Channel Group", Float) = 0
        
        [Channel] _TestChannel ("Test Channel", Vector) = (0, 1, 0, 0)
        [Channel(GroupChannel)] _TestChannelInGroup ("Test Channel in Group (Default G)", Vector) = (0, 1, 0, 0)
```



+ **Ramp**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888917528-44ffc342-845f-4506-8913-8035bbafd2c4.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888924220-52e55c27-d3dd-453a-bcf2-39b527fcdf98.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888976643-817999dd-50e9-4e00-bd41-2d73614af5f5.png)

> 上面一个只有黑白颜色变化的是Alpha通道，下面有多种颜色的是RGB通道。可以在Channels选项里设置想要调节的通道。
>

```cpp
        // 测试 Ramp，Ramp图需要手动点击GUI里的按钮来创建及保存
        [Title(Ramp)]
        [Main(GroupRamp, _, off, off)] _TestRampGroup ("Test Ramp Group", Float) = 0
        
        [Ramp] _TestRamp ("Test Ramp", 2D) = "white" {}
        [Ramp(GroupRamp)] _TestRampInGroup ("Test Ramp in Group", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap)] _TestRampNameInGroup ("Test Ramp in Group with name", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, 256)] _TestRampWidthInGroup ("Test Ramp in Group with width", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets.Textures.Ramp, 256)] _TestRampRootInGroup ("Test Ramp in Group with root", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets, Linear, 256)] _TestRampColorSpaceInGroup ("Test Ramp in Group with colorspace", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets, sRGB, 256, RGB)] _TestRampChannelInGroup ("Test Ramp in Group with channel", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets, sRGB, 256, RGBA, 24)] _TestRampTimeInGroup ("Test Ramp in Group with time", 2D) = "white" {}
```



+ **Preset**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721888987135-6a870c7e-0c54-4c44-85b8-fb6884649add.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889000865-15cc90dd-a1a7-4bac-b123-1846415e3c60.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889010330-62db925b-1c8c-446a-a3e8-41ecfc39016f.png)

```cpp
        // 测试 Preset
        [Title(Preset)]
        [Main(GroupPreset, _, off, off)] _TestPresetGroup ("Test Preset Group", Float) = 0
        
        [Preset(GroupPreset, TestPreset)] _BlendMode ("Blend Mode Preset", Float) = 0
        [SubEnum(GroupPreset, UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
        [SubEnum(GroupPreset, UnityEngine.Rendering.BlendMode)] _SrcBlend ("SrcBlend", Float) = 1
        [SubEnum(GroupPreset, UnityEngine.Rendering.BlendMode)] _DstBlend ("DstBlend", Float) = 0
        [SubToggle(GroupPreset)] _ZWrite ("ZWrite", Float) = 1
        [SubEnum(GroupPreset, UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
        [SubEnum(GroupPreset, RGBA, 15, RGB, 14)] _ColorMask ("ColorMask", Float) = 15
```



#### Decorators
+ **Title & SubTitle**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889023295-270d2139-a34a-40b0-9648-ee78bb894b83.png)

> ? 这里Title结合Group时显示上似乎有些bug
>

```cpp
        // 测试 Title & SubTitle, 源码中显示SubTitle只是对Title的封装，没有任何额外的具体实现
        [Title(Title and SubTitle)]
        [Main(GroupTitle, _, off, off)] _TestTitleGroup ("Test Title Group", Float) = 0
        
        [Title(GroupTitle, Title)] _TestTitle1 ("Test Title 1", Float) = 0
        [Title(GroupTitle, Title with height, 32)] _TestTitle2 ("Test Title 2", Float) = 0
        [Title(GroupTitle, _, 32)] _TestTitle3 ("Test Title 3", Float) = 0
```



+ **Tooltip & Helpbox**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889031293-bd2f30ea-24ec-4581-906f-d8837fb3b80a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889045048-4cb66c27-057d-46d0-9930-a02ad5920e4d.png)

```cpp
        // 测试 Tooltip & Helpbox
        [Title(Tooltip and Helpbox)]
        [Main(GroupTooltipAndHelpbox, _, off, off)] _TestTooltipAndHelpboxGroup ("Test Tooltip and Helpbox Group", Float) = 0
        
        [Tooltip] [Sub(GroupTooltipAndHelpbox)] _TestTooltip ("Test Tooltip", Float) = 0
        [Tooltip(here is a tooltip)] [Sub(GroupTooltipAndHelpbox)] _TestTooltip2 ("Test Tooltip2", Float) = 0
        [Tooltip(here is a tooltip, a, single line, can have, maximum 4 commas)]
        [Tooltip()]
        [Tooltip(line 3)]
        [Tooltip(line 4)]
        [Sub(GroupTooltipAndHelpbox)] _TestTooltip3 ("Test Tooltip3 #this is a tooltip inside name", Float) = 0
        
        [Helpbox] [Sub(GroupTooltipAndHelpbox)] _TestHelpbox ("Test Helpbox", Float) = 0
        [Helpbox(here is a helpbox)] [Sub(GroupTooltipAndHelpbox)] _TestHelpbox2 ("Test Helpbox2", Float) = 0
        [Helpbox(here is a helpbox, a, single line, can have, maximum 4 commas)]
        [Helpbox()]
        [Helpbox(line 3)]
        [Helpbox(line 4)]
        [Sub(GroupTooltipAndHelpbox)] _TestHelpbox3 ("Test Helpbox3 #this is a helpbox inside name", Float) = 0
```



+ **PassSwitch**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889069825-6bd63668-8393-4d04-a3c8-c72dd36f2960.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889079926-aa4c4345-5de5-41ef-90e7-5789757bd732.png)

```cpp
        // 测试 PassSwitch
        [Title(PassSwitch)]
        [Main(GroupPassSwitch, _, off, off)] _TestPassSwitchGroup ("Test PassSwitch Group", Float) = 0
        
        [PassSwitch(ForwardBase)] [SubToggle(GroupPassSwitch)] _TestPassSwitch ("Test PassSwitch", Float) = 0
```

> 注意这里的Pass填的是LightMode，不是pass name。LightMode在URP中是预定义的，具体可查询[URP ShaderLab Pass tags | Universal RP | 14.0.11 (unity3d.com)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@14.0/manual/urp-shaders/urp-shaderlab-pass-tags.html)
>



+ **Advanced & AdvancedHeaderProperty**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889089101-ea5c3150-a65c-4ad6-bf46-d84898765755.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889098243-6bd9131c-11d2-4396-a904-21df3ee479c2.png)

```cpp
        // 测试 Advanced & AdvancedHeaderProperty，折叠时会采取就近折叠的原则
        [Title(Advanced and AdvancedHeaderProperty)]
        [Main(GroupAdvanced, _, off, off)] _TestAdvancedGroup ("Test Advanced Group", Float) = 0
        
        [Sub(GroupAdvanced)] _TestAdvancedFloat ("Test Advanced Float", Float) = 1
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat2 ("Test Advanced Float2", Float) = 2
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat3 ("Test Advanced Float3", Float) = 3
        [Advanced(Advanced Header Test)][Sub(GroupAdvanced)] _TestAdvancedFloat4 ("Test Advanced Float4", Float) = 4
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat5 ("Test Advanced Float5", Float) = 5
        [AdvancedHeaderProperty][Sub(GroupAdvanced)] _TestAdvancedFloat6 ("Test Advanced Float6", Float) = 6
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat7 ("Test Advanced Float7", Float) = 7
```



+ **Hidden**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889107063-2da7dfb9-faf4-4460-87f7-af38aedf3a09.png)

```cpp
        // 测试 Hidden
        [Title(Hidden)]
        [Main(GroupHidden, _, off, off)] _TestHiddenGroup ("Test Hidden Group", Float) = 0
        
        [Hidden][Sub(GroupHidden)] _TestHiddenFloat ("Test Hidden Float", Float) = 0
```



+ **ReadOnly**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889115938-b5e33d31-a528-42ab-85a9-523f4dc9a4db.png)

```cpp
        // 测试 ReadOnly
        [Title(ReadOnly)]
        [Main(GroupReadOnly, _, off, off)] _TestReadOnlyGroup ("Test ReadOnly Group", Float) = 0
        
        [ReadOnly][Sub(GroupReadOnly)] _TestReadOnlyFloat ("Test ReadOnly Float", Float) = 0
```



+ **ShowIf**

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889124037-b90bbb76-74b7-479b-bca3-a095e296345f.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889133519-4dfe489e-d276-4ca2-ad2b-55e768e90512.png)

![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1721889146164-53dee83e-4e85-48ee-b94a-7752e6e55974.png)

```cpp
        // 测试 ShowIf
        [Title(ShowIf)]
        [Main(GroupShowIf, _, off, off)] _TestShowIfGroup ("Test ShowIf Group", Float) = 0
        
        [Sub(GroupShowIf)] _TestShowIfConditionFloat ("Test ShowIf Condition Float", Float) = 1
        [ShowIf(_TestShowIfConditionFloat, Greater, 1)]
        [ReadOnly]
        [Sub(GroupShowIf)] _TestShowIfShowValue ("Test ShowIf Show Value", Float) = 100
        
        [SubToggle(GroupShowIf)] _TestShowIfForceShow ("Test ShowIf Force Show", Float) = 0
        [ShowIf(_TestShowIfConditionFloat, Greater, 1)]
        [ShowIf(Or, _TestShowIfForceShow, Equal, 1)]
        [ReadOnly]
        [Sub(GroupShowIf)] _TestShowIfShowValue2 ("Test ShowIf Force Show Value", Float) = 200
```



#### **Full test_shader_gui_lwgui.shader**
```cpp
Shader "Test/test_shader_gui_lwgui"
{
    Properties
    {
        // 测试 Drawer
        // 测试 Main & Sub
        [Title(Main and Sub)]
        
        [Main] _TestMainGroupEmpty ("Test Main Group Empty", Float) = 0
        
        [Main(Group1Empty)] _TestMainGroup1Empty ("Test Main Group1 Empty", Float) = 0
        
        [Main(Group2Empty, _GROUPKEY)] _TestMainGroup2KeyEmpty ("Test Main Group2 Empty with Keyword", Float) = 0
        
        [Main(Group3Empty, _, on, off)]
        _TestMainGroup3Empty ("Test Main Group3 Empty with folding and toggle", Float) = 0
        [Sub(Group3Empty)] _TestSubWithGroup3Empty ("Test Sub With Group3Empty", Float) = 0
        [Sub(Group3Empty_GROUPKEY)] _TestSubWithGroup3EmptyAndKey ("Test Sub With Group3EmptyAndKey", Float) = 0
        
        [Sub] _TestSubEmpty ("Test Sub Empty", Float) = 0
        [Sub(Group2Empty)] _TestSubWithGroup2Empty ("Test Sub With Group2Empty", Float) = 0
        [Sub(Group2Empty_GROUPKEY)] _TestSubWithGroup2EmptyAndKey ("Test Sub With Group2EmptyAndKey", Float) = 0
        
        [Sub(Group1Empty)] _TestSubWithGroup1Empty ("Test Sub With Group1Empty", Float) = 0
        [Sub(Group1Empty_GROUPKEY)] _TestSubWithGroup1EmptyAndKey ("Test Sub With Group1EmptyAndKey", Float) = 0
        
        [Sub(_)] _TestSubWithGroupNotExist ("Test Sub With GroupNotExist", Float) = 0
        [Sub(__GROUPKEY)] _TestSubWithGroupNotExistAndKey ("Test Sub With GroupNotExistAndKey", Float) = 0
        
        // 测试 SubToggle
        [Title(SubToggle)]
        [Main(GroupSubToggle, _, off, off)] _TestSubToggleGroup ("Test SubToggle Group", Float) = 0
        
        [SubToggle] _TestSubToggle ("Test Sub Toggle", Float) = 0
        [SubToggle(GroupSubToggle)] _TestSubToggleInGroup ("Test Sub Toggle in Group", Float) = 0
        [SubToggle(GroupSubToggle, _)] _TestSubToggleWithoutKey ("Test Sub Toggle Without Key", Float) = 0
        
        // 测试 SubPowerSlider
        [Title(SubPowerSlider)]
        [Main(GroupSubPowerSlider, _, off, off)] _TestSubPowerSliderGroup ("Test SubPowerSlider Group", Float) = 0
        
        [SubPowerSlider] _TestSubPowerSlider ("Test Sub PowerSlider", Range(0.01, 1)) = 0.5
        [SubPowerSlider(0.3)] _TestSubPowerSliderWithCurve ("Test Sub PowerSlider With Curve", Range(0.01, 1)) = 0.5
        [SubPowerSlider(GroupSubPowerSlider)] _testSubPowerSliderInGroup ("Test Sub PowerSlider in Group", Range(0.01, 1)) = 0.5
        [SubPowerSlider(GroupSubPowerSlider, 9.0)] _testSubPowerSliderWithCurveInGroup ("Test Sub PowerSlider with Curve in Group", Range(0.01, 1)) = 0.5
        
        // 测试 SubIntRange
        [Title(SubIntRange)]
        [Main(GroupSubIntRange, _, off, off)] _TestSubIntRangeGroup ("Test SubIntRange Group", Float) = 0
        
        [SubIntRange] _TestIrregularSubIntRange ("Test Irregular Sub IntRange", Range(0.01, 4)) = 3
        [SubIntRange] _TestSubIntRange ("Test Sub IntRange", Range(1, 4)) = 3
        [SubIntRange(GroupSubIntRange)] _TestIrregularSubIntRangeInGroup ("Test Irregular Sub IntRange in Group", Range(1, 4)) = 3
        [SubIntRange(GroupSubIntRange)] _TestSubIntRangeInGroup ("Test Sub IntRange in Group", Range(0.01, 4)) = 3
        
        // 测试 MinMaxSlider
        [Title(MinMaxSlider)]
        [Main(GroupMinMaxSlider, _, off, off)] _TestMinMaxSliderGroup ("Test MinMaxSlider Group", Float) = 0
        
        [MinMaxSlider(_TestMinMaxSliderRangeStart, _TestMinMaxSliderRangeEnd)] _TestMinMaxSlider ("Min Max Slider (0 - 1)", Range(0.0, 1.0)) = 1.0
        [Hidden] _TestMinMaxSliderRangeStart ("Range Start", Range(0.0, 0.5)) = 0.0
        [Hidden] _TestMinMaxSliderRangeEnd ("Range End", Range(0.5, 1.0)) = 1.0
        [MinMaxSlider(GroupMinMaxSlider, _TestMinMaxSliderRangeStartInGroup, _TestMinMaxSliderRangeEndInGroup)] _TestMinMaxSliderInGroup ("Min Max Slider (0 - 1) in Group", Range(0.0, 1.0)) = 1.0
        [Sub(GroupMinMaxSlider)] _TestMinMaxSliderRangeStartInGroup ("Range Start in Group", Range(0.0, 0.5)) = 0.5
        [Sub(GroupMinMaxSlider)] _TestMinMaxSliderRangeEndInGroup ("Range End in Group", Range(0.5, 1.0)) = 1.0
        
        // 测试 KWEnum，主要用于激活Keywords
        [Title(KWEnum)]
        [Main(GroupKWEnum, _, off, off)] _TestKWEnumGroup ("Test KWEnum Group", Float) = 0
        
        [KWEnum] _TestKWEnum ("Test KWEnum", Float) = 0
        [KWEnum(none, _TESTKWENUM2)] _TestKWEnum2 ("Test KWEnum2", Float) = 0
        [KWEnum(none, _TESTKWENUM3_NONE, add, _TESTKWENUM3_ADD)] _TestKWEnum3 ("Test KWEnum3", Float) = 0
        [KWEnum(GroupKWEnum)] _TestKWEnumInGroup ("Test KWEnum in Group", Float) = 0
        [KWEnum(GroupKWEnum, none, _TESTKWENUM2INGROUP)] _TestKWEnum2InGroup ("Test KWEnum2 in Group", Float) = 0
        [KWEnum(GroupKWEnum, none, _TESTKWENUM3INGROUP_NONE, add, _TESTKWENUM3INGROUP_ADD)] _TestKWEnum3InGroup ("Test KWEnum3 in Group", Float) = 0
        
        // 测试 SubEnum & SubKeywordEnum
        [Title(SubEnum and SubKeywordEnum)]
        [Main(GroupSubEnumAndSubKeywordEnum, _, off, off)] _TestSubEnumAndSubKeywordEnumGroup ("Test SubEnum and SubKeywordEnum Group", Float) = 0
        
        [SubEnum(GroupSubEnumAndSubKeywordEnum)] _TestSubEnum ("Test SubEnum", Float) = 0
        [SubEnum(GroupSubEnumAndSubKeywordEnum, Meta48.Rendering.Tests.EBlendModes)] _TestenumSubEnum ("Test enum SubEnum", Float) = 1
        [SubEnum(GroupSubEnumAndSubKeywordEnum, One, 1, SrcAlpha, 5)] _TestManualSubEnum ("Test manual SubEnum", Float) = 1
        
        [SubKeywordEnum] _TestKeywordEnum ("Test Keyword Enum", Float) = 0
        [SubKeywordEnum(GroupSubEnumAndSubKeywordEnum, None, Add, Multiply)] _TestKeywordEnumWith ("Test Keyword Enum With Keywords", Float) = 0
        
        // 测试 Tex & Color
        [Title(Tex and Color)]
        [Main(GroupTexAndColor, _, off, off)] _TestTexAndColorGroup ("Test Tex and Color Group", Float) = 0
        
        [Tex] _TestTex ("Test Tex", 2D) = "white" {}
        [Tex(GroupTexAndColor)] _TestTexInGroup ("Test Tex in Group", 2D) = "white" {}
        [Tex(GroupTexAndColor, _TestColorInTex)] _TestTex_Color ("Test Tex with Color", 2D) = "white" {}
        [Hidden] _TestColorInTex (" ", Color) = (1, 0, 0, 1)
        [Tex(GroupTexAndColor, _TestVectorInTex)] _TestTex_Vector ("Test Tex with Vector", 2D) = "white" {}
        [Hidden] _TestVectorInTex (" ", Vector) = (0, 0, 0, 1)
        
        [Color(GroupTexAndColor, _TestColor2, _TestColor3, _TestColor4)]
        _TestColor ("Multi Color", Color) = (1, 1, 1, 1)
        [Hidden] _TestColor2 (" ", Color) = (1, 0, 0, 1)
        [Hidden] _TestColor3 (" ", Color) = (0, 1, 0, 1)
        [Hidden] _TestColor4 (" ", Color) = (0, 0, 1, 1)
        
        // 测试 Image，不会参与build
        [Title(Image)]
        [Main(GroupImage, _, off, off)] _TestImageGroup ("Test Image Group", Float) = 0
        
        [Image] _TestImage ("Test Image", 2D) = "black" {}
        [Image(GroupImage)] _TestImageInGroup ("Test Image in Group", 2D) = "yellow" {}
        
        // 测试 Channel
        [Title(Channel)]
        [Main(GroupChannel, _, off, off)] _TestChannelGroup ("Test Channel Group", Float) = 0
        
        [Channel] _TestChannel ("Test Channel", Vector) = (0, 1, 0, 0)
        [Channel(GroupChannel)] _TestChannelInGroup ("Test Channel in Group (Default G)", Vector) = (0, 1, 0, 0)
        
        // 测试 Ramp，Ramp图需要手动点击GUI里的按钮来创建及保存
        [Title(Ramp)]
        [Main(GroupRamp, _, off, off)] _TestRampGroup ("Test Ramp Group", Float) = 0
        
        [Ramp] _TestRamp ("Test Ramp", 2D) = "white" {}
        [Ramp(GroupRamp)] _TestRampInGroup ("Test Ramp in Group", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap)] _TestRampNameInGroup ("Test Ramp in Group with name", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, 256)] _TestRampWidthInGroup ("Test Ramp in Group with width", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets.Textures.Ramp, 256)] _TestRampRootInGroup ("Test Ramp in Group with root", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets, Linear, 256)] _TestRampColorSpaceInGroup ("Test Ramp in Group with colorspace", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets, sRGB, 256, RGB)] _TestRampChannelInGroup ("Test Ramp in Group with channel", 2D) = "white" {}
        [Ramp(GroupRamp, TempRampMap, Assets, sRGB, 256, RGBA, 24)] _TestRampTimeInGroup ("Test Ramp in Group with time", 2D) = "white" {}
        
        // 测试 Preset
        [Title(Preset)]
        [Main(GroupPreset, _, off, off)] _TestPresetGroup ("Test Preset Group", Float) = 0
        
        [Preset(GroupPreset, TestPreset)] _BlendMode ("Blend Mode Preset", Float) = 0
        [SubEnum(GroupPreset, UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
        [SubEnum(GroupPreset, UnityEngine.Rendering.BlendMode)] _SrcBlend ("SrcBlend", Float) = 1
        [SubEnum(GroupPreset, UnityEngine.Rendering.BlendMode)] _DstBlend ("DstBlend", Float) = 0
        [SubToggle(GroupPreset)] _ZWrite ("ZWrite", Float) = 1
        [SubEnum(GroupPreset, UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
        [SubEnum(GroupPreset, RGBA, 15, RGB, 14)] _ColorMask ("ColorMask", Float) = 15
        
        // 测试 Decorators
        // 测试 Title & SubTitle, 源码中显示SubTitle只是对Title的封装，没有任何额外的具体实现
        [Title(Title and SubTitle)]
        [Main(GroupTitle, _, off, off)] _TestTitleGroup ("Test Title Group", Float) = 0
        
        [Title(GroupTitle, Title)] _TestTitle1 ("Test Title 1", Float) = 0
        [Title(GroupTitle, Title with height, 32)] _TestTitle2 ("Test Title 2", Float) = 0
        [Title(GroupTitle, _, 32)] _TestTitle3 ("Test Title 3", Float) = 0
        
        // 测试 Tooltip & Helpbox
        [Title(Tooltip and Helpbox)]
        [Main(GroupTooltipAndHelpbox, _, off, off)] _TestTooltipAndHelpboxGroup ("Test Tooltip and Helpbox Group", Float) = 0
        
        [Tooltip] [Sub(GroupTooltipAndHelpbox)] _TestTooltip ("Test Tooltip", Float) = 0
        [Tooltip(here is a tooltip)] [Sub(GroupTooltipAndHelpbox)] _TestTooltip2 ("Test Tooltip2", Float) = 0
        [Tooltip(here is a tooltip, a, single line, can have, maximum 4 commas)]
        [Tooltip()]
        [Tooltip(line 3)]
        [Tooltip(line 4)]
        [Sub(GroupTooltipAndHelpbox)] _TestTooltip3 ("Test Tooltip3 #this is a tooltip inside name", Float) = 0
        
        [Helpbox] [Sub(GroupTooltipAndHelpbox)] _TestHelpbox ("Test Helpbox", Float) = 0
        [Helpbox(here is a helpbox)] [Sub(GroupTooltipAndHelpbox)] _TestHelpbox2 ("Test Helpbox2", Float) = 0
        [Helpbox(here is a helpbox, a, single line, can have, maximum 4 commas)]
        [Helpbox()]
        [Helpbox(line 3)]
        [Helpbox(line 4)]
        [Sub(GroupTooltipAndHelpbox)] _TestHelpbox3 ("Test Helpbox3 #this is a helpbox inside name", Float) = 0
        
        // 测试 PassSwitch
        [Title(PassSwitch)]
        [Main(GroupPassSwitch, _, off, off)] _TestPassSwitchGroup ("Test PassSwitch Group", Float) = 0
        
        [PassSwitch(ForwardBase)] [SubToggle(GroupPassSwitch)] _TestPassSwitch ("Test PassSwitch", Float) = 0
        
        // 测试 Advanced & AdvancedHeaderProperty，折叠时会采取就近折叠的原则
        [Title(Advanced and AdvancedHeaderProperty)]
        [Main(GroupAdvanced, _, off, off)] _TestAdvancedGroup ("Test Advanced Group", Float) = 0
        
        [Sub(GroupAdvanced)] _TestAdvancedFloat ("Test Advanced Float", Float) = 1
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat2 ("Test Advanced Float2", Float) = 2
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat3 ("Test Advanced Float3", Float) = 3
        [Advanced(Advanced Header Test)][Sub(GroupAdvanced)] _TestAdvancedFloat4 ("Test Advanced Float4", Float) = 4
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat5 ("Test Advanced Float5", Float) = 5
        [AdvancedHeaderProperty][Sub(GroupAdvanced)] _TestAdvancedFloat6 ("Test Advanced Float6", Float) = 6
        [Advanced][Sub(GroupAdvanced)] _TestAdvancedFloat7 ("Test Advanced Float7", Float) = 7
        
        // 测试 Hidden
        [Title(Hidden)]
        [Main(GroupHidden, _, off, off)] _TestHiddenGroup ("Test Hidden Group", Float) = 0
        
        [Hidden][Sub(GroupHidden)] _TestHiddenFloat ("Test Hidden Float", Float) = 0
        
        // 测试 ReadOnly
        [Title(ReadOnly)]
        [Main(GroupReadOnly, _, off, off)] _TestReadOnlyGroup ("Test ReadOnly Group", Float) = 0
        
        [ReadOnly][Sub(GroupReadOnly)] _TestReadOnlyFloat ("Test ReadOnly Float", Float) = 0
        
        // 测试 ShowIf
        [Title(ShowIf)]
        [Main(GroupShowIf, _, off, off)] _TestShowIfGroup ("Test ShowIf Group", Float) = 0
        
        [Sub(GroupShowIf)] _TestShowIfConditionFloat ("Test ShowIf Condition Float", Float) = 1
        [ShowIf(_TestShowIfConditionFloat, Greater, 1)]
        [ReadOnly]
        [Sub(GroupShowIf)] _TestShowIfShowValue ("Test ShowIf Show Value", Float) = 100
        
        [SubToggle(GroupShowIf)] _TestShowIfForceShow ("Test ShowIf Force Show", Float) = 0
        [ShowIf(_TestShowIfConditionFloat, Greater, 1)]
        [ShowIf(Or, _TestShowIfForceShow, Equal, 1)]
        [ReadOnly]
        [Sub(GroupShowIf)] _TestShowIfShowValue2 ("Test ShowIf Force Show Value", Float) = 200
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        // // SubToggle 关键字
        // #pragma multi_compile _ _TOGGLE_KEYWORD
        //
        // // KWEnum 关键字
        // #pragma multi_compile _ _OPT1 _OPT2 _OPT3
        //
        // // SubKeywordEnum 关键字
        // #pragma multi_compile _ _KEY1 _KEY2 _KEY3

        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
    CustomEditor "LWGUI.LWGUI"
}
```



### UI风格
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1722847862299-d6952a46-8236-4d0f-9ef0-062d09cc6a6f.png)

#### Full_test_uistyle.shader
```cpp
Shader "Test/UI_Style"
{
    Properties
    {
        // Surface Options
        // Write Pass settings parameters here (except stencil), 
        // e.g. Cull, ZClip, ZTest, ZWrite, Offset, Blend, ColorMask, Conservative
        [Main(GroupSurfaceOptions, _, on, off)] _EnableGroupSurfaceOptions("基础设置(Surface Options)", Float) = 1
        [Preset(GroupSurfaceOptions, Test_SurfacePreset)] _Surface("表面类型(Surface Type)", Float) = 0.0
        
        // Blending state
        [ShowIf(_Surface, Equal, 1.0)]
        [Preset(GroupSurfaceOptions, Test_BlendingPreset)] _Blend("混合模式(Blending Mode)", Float) = 0.0
        
        [SubToggle(GroupSurfaceOptions, _ALPHAPREMULTIPLY_ON)] [Hidden] _BlendModePreserveSpecular("    Preserve Specular", Float) = 1.0
        
        [ShowIf(_Surface, Equal, 1.0)]
        [ShowIf(And, _Blend, Equal, 6)]
        [SubEnum(GroupSurfaceOptions, UnityEngine.Rendering.BlendMode)] _SrcBlend("    Src", Float) = 1.0
        
        [ShowIf(_Surface, Equal, 1.0)]
        [ShowIf(And, _Blend, Equal, 6)]
        [SubEnum(GroupSurfaceOptions, UnityEngine.Rendering.BlendMode)] _DstBlend("    Dst", Float) = 0.0
        
        [ShowIf(_Surface, Equal, 1.0)]
        [ShowIf(And, _Blend, Equal, 6)]
        [SubEnum(GroupSurfaceOptions, UnityEngine.Rendering.BlendMode)] _SrcBlendAlpha("    Src Alpha", Float) = 0.0
        
        [ShowIf(_Surface, Equal, 1.0)]
        [ShowIf(And, _Blend, Equal, 6)]
        [SubEnum(GroupSurfaceOptions, UnityEngine.Rendering.BlendMode)] _DstBlendAlpha("    Dst Alpha", Float) = 0.0
        
        [ShowIf(_Surface, Equal, 1.0)]
        [ShowIf(And, _Blend, Equal, 6)]
        [SubToggle(GroupSurfaceOptions, _)] _ZWrite("    Depth Write", Float) = 1.0
        
        // Alpha Clipping: 0, 1
        [ShowIf(_Surface, Greater, 0)]
        [SubToggle(GroupSurfaceOptions, _ALPHATEST_ON)] _AlphaClipping("透明裁切(Alpha Clipping)", Float) = 0.0
        //     Alpha Cutoff: 0 ~ 1
        [ShowIf(_Surface, Greater, 0)]
        [ShowIf(And, _AlphaClipping, Equal, 1)]
        [Sub(GroupSurfaceOptions)] _Cutoff ("    裁切阈值(Alpha Cutoff)", Range(0.0, 1.0)) = 0.5
        //     Alpha To Mask
        [ShowIf(_Surface, Equal, 2.0)]
        [ShowIf(And, _AlphaClipping, Equal, 1)]
        [SubToggle(GroupSurfaceOptions, _)] _AlphaToMask("    AlphaToMask", Float) = 1.0
        
        // Render Face: Both, Back, Front
        [SubEnum(GroupSurfaceOptions, Both, 0, Back, 1, Front, 2)] _Cull("Render Face", Float) = 2.0
        
        // Cast Shadows
        [SubToggle(GroupSurfaceOptions, _)] _CastShadows("投射阴影(Cast Shadows)", Float) = 1.0
        
        // Surface Inputs
        [Main(GroupSurfaceInputs, _, on, off)] _EnableGroupSurfaceInputs("贴图设置(Surface Inputs)", Float) = 1
        [AdvancedHeaderProperty]
        [Tex(GroupSurfaceInputs, _Color)] _MainTex("主贴图(Albedo)", 2D) = "white" {}
        [Advanced]
        [Hidden] _Color(" ", Color) = (1, 1, 1, 1)
        [Advanced]
        [Sub(GroupSurfaceInputs)] _MainTexUV("UV Tiling and Offset", Vector) = (1, 1, 0, 0)
        
        [Space]
        [AdvancedHeaderProperty]
        [Tex(GroupSurfaceInputs)] [Normal] _BumpMap("法线(Bump Map)", 2D) = "bump" {}
        [Advanced]
        [Sub(GroupSurfaceInputs)] _BumpScale("法线强度(Bump Scale)", Float) = 1.0

        [Space]
        [AdvancedHeaderProperty]
        [Tex(GroupSurfaceInputs)] _MetallicGlossMap("金属度(Metallic)", 2D) = "white" {}
        [Advanced]
        [Channel(GroupSurfaceInputs)] _SmoothnessTextureChannel("光滑度来源通道", Vector) = (0, 0, 0, 1)
        [Advanced]
        [Sub(GroupSurfaceInputs)] _Metallic("金属度强度(Metallic)", Range(0.0, 1.0)) = 0.0
        [Advanced]
        [Sub(GroupSurfaceInputs)] _Smoothness("光滑度(Smoothness)", Range(0.0, 1.0)) = 0.5

        [Space]
        [AdvancedHeaderProperty]
        [Tex(GroupSurfaceInputs)] _ParallaxMap("高度(Parallax Map)", 2D) = "black" {}
        [Advanced]
        [Sub(GroupSurfaceInputs)] _Parallax("高度强度(Parallax)", Range(0.005, 0.08)) = 0.005

        [Space]
        [AdvancedHeaderProperty]
        [Tex(GroupSurfaceInputs)] _OcclusionMap("Ambient Occlusion", 2D) = "white" {}
        [Advanced]
        [Sub(GroupSurfaceInputs)] _OcclusionStrength("AO强度(Occlusion Strength)", Range(0.0, 1.0)) = 1.0

        [Space]
        [AdvancedHeaderProperty]
        [Tex(GroupSurfaceInputs, _EmissionColor)] _EmissionMap("自发光(Emission)", 2D) = "white" {}
        [Advanced]
        [Hidden] [HDR] _EmissionColor(" ", Color) = (0,0,0)
        [Advanced]
        [Sub(GroupSurfaceInputs)] _EmissionStrength("自发光强度(Emission Strength)", Range(0.0, 1.0)) = 1.0
        
        // [?] Detail Options. Write Other Extra Features like this.
        [Main(GroupDetailInputs, _ENABLE_DETAIL_INPUTS, off, on)] _EnableGroupDetailInputs("Detail Options", Float) = 0
        [Tex(GroupDetailInputs)] _DetailMask("Detail Mask", 2D) = "white" {}
        [Tex(GroupDetailInputs)] _DetailAlbedoMap("Detail Albedo", 2D) = "linearGrey" {}
        [Sub(GroupDetailInputs)] _DetailAlbedoMapScale("Detail Scale", Range(0.0, 2.0)) = 1.0
        [Tex(GroupDetailInputs)] [Normal] _DetailNormalMap("Detail Normal Map", 2D) = "bump" {}
        [Sub(GroupDetailInputs)] _DetailNormalMapScale("Normal Strength", Range(0.0, 2.0)) = 1.0
        
        // [?] Shadow Options (if Receive Shadows has many child options)
        [Main(GroupShadowOptions, _ENABLE_SHADOW_OPTIONS, off, on)] _EnableGroupShadowOptions("阴影设置(Shadow Options)", Float) = 0
        // Receive Shadows
        [SubToggle(GroupShadowOptions, _)] _ReceiveShadows("接受阴影(Receive Shadows)", Float) = 1.0
    	[Ramp(GroupShadowOptions, ShadowRampMap, Assets.Tests.LWGUI Demo, 256)] _ShadowRamp ("Shadow Ramp", 2D) = "white" {}
        
        // [?] Stencil Options
        [Main(GroupStencilOptions, _, off, on)] _EnableGroupStencilOptions("模版设置(Stencil Options)", Float) = 0
		[Sub(GroupStencilOptions)] _stencilRef("Stencil Ref", Float) = 0
		[Sub(GroupStencilOptions)] _stencilReadMask("Stencil ReadMask", Float) = 255
		[Sub(GroupStencilOptions)] _stencilWriteMask("Stencil WriteMask", Float) = 255
		[SubEnum(GroupStencilOptions, UnityEngine.Rendering.CompareFunction)] _StencilComp("Stencil Comparison", Float) = 8
		[SubEnum(GroupStencilOptions, UnityEngine.Rendering.StencilOp)] _StencilPassOp("Stencil Pass Op", Float) = 0
		[SubEnum(GroupStencilOptions, UnityEngine.Rendering.StencilOp)] _StencilFailOp("Stencil Fail Op", Float) = 0
		[SubEnum(GroupStencilOptions, UnityEngine.Rendering.StencilOp)] _StencilZFailOp("Stencil ZFail Op", Float) = 0
        
        // [?] Advanced Options
        [Main(GroupAdvancedOptions, _, off, on)] _EnableGroupAdvancedOptions("进阶设置(Advanced Options)", Float) = 0
        [SubToggle(GroupAdvancedOptions, _)] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [SubToggle(GroupAdvancedOptions, _)] _EnvironmentReflections("Environment Reflections", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "UniversalMaterialType" = "Lit"
            "IgnoreProjector" = "True"
        }
        LOD 100

		Pass
		{
            Name "ForwardLit"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            Blend [_SrcBlend][_DstBlend], [_SrcBlendAlpha][_DstBlendAlpha]
			ZWrite [_ZWrite]
			Cull [_Cull]
			AlphaToMask [_AlphaToMask]
			Stencil
			{
				Ref [_stencilRef]
				ReadMask  [_stencilReadMask]
				WriteMask [_stencilWriteMask]
				Comp [_StencilComp]
				Pass [_StencilPassOp]
				Fail [_StencilFailOp]
				ZFail [_StencilFailOp]
			}
			HLSLPROGRAM

			// 需要standard SRP library编译gles 2.0
			// 所有shader必须用HLSLcc编译，默认情况下只有gles不使用HLSLcc
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			#pragma vertex LitPassVertex
			#pragma fragment LitPassFragment
			
            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _SURFACE_TYPE_TRANSPARENT
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Unlit.hlsl"

			CBUFFER_START(UnityPerMaterial)
			half4 _Color;
			half _Surface;
			half _Cutoff;
			float4 _MainTexUV;
			float4 _MainTex_ST;
			CBUFFER_END

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float2 uv : TEXCOORD0;
				float4 positionCS : SV_POSITION;
			};

			void InitializeInputData(out InputData inputData)
			{
				inputData = (InputData)0;
				inputData.positionWS = float3(0, 0, 0);
				inputData.normalWS = half3(0, 0, 1);
				inputData.viewDirectionWS = half3(0, 0, 1);
				inputData.shadowCoord = 0;
				inputData.fogCoord = 0;
				inputData.vertexLighting = half3(0, 0, 0);
				inputData.bakedGI = half3(0, 0, 0);
				inputData.normalizedScreenSpaceUV = 0;
				inputData.shadowMask = half4(1, 1, 1, 1);
			}

			Varyings LitPassVertex(Attributes input)
			{
				_MainTex_ST = _MainTexUV;
				Varyings output = (Varyings)0;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);

				output.positionCS = vertexInput.positionCS;
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);

				return output;
			}

			void LitPassFragment(Varyings input, out half4 outColor : SV_Target0)
			{
				half2 uv = input.uv;
				half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
				half3 color = texColor.rgb * _Color.rgb;
				half alpha = texColor.a * _Color.a;

				alpha = AlphaDiscard(alpha, _Cutoff);
				color = AlphaModulate(color, alpha);

				InputData inputData;
				InitializeInputData(inputData);

				half4 finalColor = UniversalFragmentUnlit(inputData, color, alpha);
				finalColor.a = OutputAlpha(finalColor.a, IsSurfaceTypeTransparent(_Surface));

				outColor = finalColor;
			}
			
			ENDHLSL
		}
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
    CustomEditor "LWGUI.LWGUI"
}
```





### 注意
1. LWGUI不提供Override Tag的功能，因此没法切换Tag RenderType到Transparent，或者切回到Opaque。

> 经测试，RenderQueue正确的情况下，RenderType不切也可以。
>

2. LWGUI目前Preset功能存在一定缺陷。当只有一层Preset，即Preset内只包含基础类型数值时，Preset工作正常。但是当Preset嵌套存在时，即一级Preset的预设值，涉及到一个变量A，且A也是一个Preset。这个时候，A的Preset会不进行初始化。不过当A可见后，再直接切换A的Preset时，A还是工作正常的。

> 一般来说最好不要在Preset里再嵌套Preset类型的变量。如果有的话，就需要在初始化时让外层的Preset手动给里层Preset涉及到的变量赋值，以规避从不可见切换到可见时里层Preset无任何动作的问题。
>



# 参考
[LWGUI/README_CN.md at 1.x · JasonMa0012/LWGUI](https://github.com/JasonMa0012/LWGUI/blob/1.x/README_CN.md#subtoggle)

