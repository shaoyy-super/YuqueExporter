# 一.简介
一款配合播放的音乐，来驱动场景内各个组件变换的插件工具。

# 二.目标
插件通过编辑轨道(Track)打点事件(Event)，通过监听音乐流，执行轨道事件，获取触发事件(Event)配置的事件参数，来修改场景内组件的变化，例如

| 事件类型 | 获取参数类型 | 备注 | 调用函数 |
| --- | --- | --- | --- |
| Asset | Object | unity资源文件 | KoreographyEvent.GetAssetValue |
| Color | Color | 颜色值(r,g,b,a) | KoreographyEvent.GetColorValue |
| Curve | float | 曲线渐变 | KoreographyEvent.GetCurveValue |
| Float | float | 浮点数 | KoreographyEvent.GetFloatValue |
| Int | int | 整型 | KoreographyEvent.GetIntValue |
| <font style="color:rgb(64, 64, 64);">Gradient</font> | <font style="color:rgb(64, 64, 64);">Gradient</font> | 颜色渐变 | KoreographyEvent.GetGradientData |
| Text | string | 文字 | KoreographyEvent.GetTextValue |


# 三.使用
### 1.koreographer.1.6.1插件导入
通过Unity商店购买插件，也可通过某宝(学习用)，获取插件package，直接拖入unity(2022.3.22f1)，会提示Api更新，选择Yes即可。

### 2.创建并唤起koreographer编辑界面
 	导入插件后，右键在Create中最下方选中Koreography，创建一个Koreography组件(ScriptableObject)。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720061381025-a7512b5c-0894-483d-ad52-a5fe73af604e.png)

选中创建的Koreography选择一个AudioClip。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720061681616-f7ce96e0-dc58-4bbd-8834-b37ad0768fe8.png)

创建需要编辑的轨道(Track)用来K事件。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720062391127-6d3c4ee4-4ee3-427a-b996-04d61ae902c3.png)

设置每个轨道(Track)对应的事件ID

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720062446001-068a797d-663e-44d6-8524-d691da10efeb.png)

将创建好的轨道拖入Koreography组件

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720062535316-52aef2b7-7e31-4f7f-a808-11dd3c07bc88.png)

选中Koreography组件，并点击Open In Koreography Editor。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720062658320-cad9d8eb-5209-4093-a7e5-2252720e5ade.png)

### 3.编辑事件
选择某一跟轨道，即选择轨道配置的EventID。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720062744688-ce1b233d-9a81-4163-b884-734fbe923c74.png)

双击音频轨道打事件点。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720063377364-f6e1a427-ffde-4894-9a8a-d4eeccfbdb61.png)

选择事件需要传递的参数类型以及参数。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720063446187-7b9654a1-5542-4c59-ae75-e577ae0bd306.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720083982703-392a9b59-093a-413a-85e3-69ee4f1a8c4c.png)

### 4.监听事件并获取值。
创建一个MusicPlayer的GameObject，添加脚本Koreographer(Mono单例)，添加脚本SimpleMusicPlayer。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720064022418-ef8c2607-4130-4030-b2e5-5d1c2b9c0384.png)

把编辑的Koreography组件挂在上来。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720065462083-5328cb90-f926-4a69-8ebb-33c9f6da8d10.png)

添加自定义mono脚本TestAudio，监听轨道事件，获取事件配置参数。

```plain
public class TestAudio : MonoBehaviour
{
    // 匹配轨道的事件名 
    string _eventId = "FloatEvent";
    // Start is called before the first frame update
    void Start()
    {      
        Koreographer.Instance.RegisterForEvents(_eventId, _TestCallBack);
        Koreographer.Instance.RegisterForEventsWithTime(_eventId, _TestCallBack);
    }

    private void OnDestroy()
    {
        Koreographer.Instance.UnregisterForAllEvents(this);
    }


    private void _TestCallBack(KoreographyEvent koreoEvent)
    {     
        float floatValue = koreoEvent.GetFloatValue();           
        Debug.Log("FloatEventValue" + floatValue);
    }

    private void _TestCallBack(KoreographyEvent koreoEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {      
        float floatValue = koreoEvent.GetFloatValue();
        Debug.Log("FloatEventValue" + floatValue);      
    } 
}
```

# 四.数据结构
![画板](https://cdn.nlark.com/yuque/0/2024/jpeg/46026893/1720089249253-977e3af8-9a51-4180-916f-544444411624.jpeg)

# 五.引用相关文献地址
[https://blog.csdn.net/a123667/article/details/135047917](https://blog.csdn.net/a123667/article/details/135047917)   Unity-Koreography音游插件的介绍和使用

[https://blog.csdn.net/u014361280/article/details/120633304](https://blog.csdn.net/u014361280/article/details/120633304) Unity音乐插件制作一

[https://blog.csdn.net/u014361280/article/details/120659333](https://blog.csdn.net/u014361280/article/details/120659333) Unity音乐插件制作二

[https://www.bilibili.com/read/cv10422759/](https://www.bilibili.com/read/cv10422759/)  Unity知识记录--音游开发插件koreographer1

# 六.当前问题（后续根据最新的情况更新当前问题）
### 1.玩法方案未明确
未知需要变更的场景组件。

### 2.场景未知
未知场景是静态搭建，还是动态创建场景资源。

### 3.支持的加载方式
Koreography是ScriptableObject资源类型，是否支持动态加载和热更。

### 4.场景歌曲播放形式未知
场景歌曲音乐是有自己的独立播放模块吗，后续需要匹配插件播放。

### 5.插件集成到项目形式
插件如何集成到项目中

# 
