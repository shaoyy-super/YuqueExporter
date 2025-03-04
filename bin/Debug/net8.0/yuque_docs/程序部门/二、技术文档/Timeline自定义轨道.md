# 一、认识Timeline
## 1、Timeline内部结构
![](https://cdn.nlark.com/yuque/0/2024/png/43256857/1714469221669-03182edf-2b0e-4c3f-b13d-4d1f19f1cf3d.png)

Timeline的结构是一个树状图， 它的数据流向是自下向上。以动画为例：

1. 3个Animation Clip将信息输入到Animation Mixer中
2. Animation Mixer根据时间顺序，决定最终的动画播放顺序及播放什么动画。并且将处理后的信息输入给Timeline
3. 最后Timeline将信息输入到Animator组件中，让Animator播放最终的动作



## 2、使用Playable拓展Timeline轨道
自定义个一个完整的Timeline轨道需要4个脚本

1. Data：为TML提供控制数据的对象或者参数，如：Animation Clip、Audio Clip，它们都是Unity的资源但其本质都是多媒体的数据。
2. Clip：放在TML时间轴上的对象，它承载了Data和时间排布的信息，同时它也是一个Asset资源
3. Mixer：它决定一条轨道最终输出的结果是什么
4. Track：它包含Mixer，在Mixer完成任务后，将结果传递给场景中受影响的对象



结合部分Playable的概念，下面会对4个脚本做进一步的说明及示例代码展示

### 2.1 Data：Clip的PlayableBehaviour
从Playable的设定上讲，Data是一个行为脚本，它是可以负责逻辑处理的。

从Unity的视频教程里看，他期望Data主要作为一个数据类，用来记录输入的信息。



这个脚本需要下面的要素：

1. 要继承自PlayableBehaviour
2. 要使用[Serializable]特性标注，作为数据类时需要保存用户设置的参数

```csharp
[Serializable]
public class VideoPlayerClip : PlayableBehaviour
{
    public VideoPlayer Player;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Player.Play();
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Player.Pause();
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (null != Player)
        {
            double targetTime = playable.GetTime();
            Player.externalReferenceTime = targetTime;
        }
    }
}
```



### 2.2 Clip：用于存储时间、Clip数据的Asset（PlayableAsset）
Clip没有二义性，它就两个主要职责 1）序列化保存片段数据，2）运行时创建Playable对象



这个脚本需要下面的要素：

1. 要继承自PlayableAsset，并实现ITimelineClipAsset
2. 要使用[Serializable]特性标注，Asset都需要可序列化
3. 重载CreatePlayable方法，来创建Clip的Playable对象
4. 实现ClipCaps，设定Clip是否支持融合

```csharp
[Serializable]
public class VideoPlayerClipAsset : PlayableAsset, ITimelineClipAsset
{
    // 这里定义Data的对象，建议的变量名就是Template
    // 它作为创建Playable的模板数据使用
    public VideoPlayerClip Template;

    // 控制是否支持融合、外插值等特征
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // 用模板创建Clip的Playable对象
        ScriptPlayable<VideoPlayerClip> playable = ScriptPlayable<VideoPlayerClip>.Create(graph, Template);
        return playable;
    }
}
```



### 2.3 Mixer：融合多个Clip并输出最终结果的行为类（PlayableBehaviour）
Mixer跟Data都是行为脚本（继承自PlayableBehaviour），它的职责更加明确且唯一。

Mixer只负责处理行为，并且它是对整条轨道做全局控制的，所以它可以在多个Clip间做协调



这个脚本需要下面的要素：

1. 要继承自PlayableBehaviour
2. 实现ProcessFrame方法，对Clip进行融合、结果输出

```csharp
public class VideoPlayerMixer : PlayableBehaviour
{
    public VideoPlayer Player;
    private Playable _mixerPlayable;

    public override void OnGraphStart(Playable playable)
    {
        _mixerPlayable = playable;
        for (int i = 0; i < playable.GetInputCount(); ++i)
        {
            VideoPlayerClip clip = ((ScriptPlayable<VideoPlayerClip>)playable.GetInput(i)).GetBehaviour();
            clip.Player = Player;
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        for (int i = 0; i < _mixerPlayable.GetInputCount(); ++i)
        {
            if (_mixerPlayable.GetInputWeight(i) > 0)
            {
                double targetTime = playable.GetTime();
                VideoPlayerClip clip = ((ScriptPlayable<VideoPlayerClip>)playable.GetInput(i)).GetBehaviour();
                clip.Player.externalReferenceTime = targetTime;
            }
        }
    }
}
```



### 2.4 Track：用于存储用于存储整体轨道数据的Asset
Track本质上也是一个PlayableAsset（同Clip），但是它的职责更多，所以拓展出了专属的TrackAsset类。

Track处理要序列化我们自定义的一些参数，还要管理它下面的所有Clip，如：根据Clip创建TimelineClip



这个脚本需要下面的要素：

1. 要继承自TrackAsset
2. 要添加[TrackClipType]特性，明确制定可以轨道使用的Clip
3. 添加[Serializable]，序列化资源的通用要求
4. 实现CreateTrackMixer方法，创建Mixer的Playable

```csharp
// 设置Track可以接受的Clip
[TrackClipType(typeof(VideoPlayerClipAsset))]
[Serializable]
public class VideoPlayerTrack : TrackAsset
{
    public ExposedReference<VideoPlayer> Player;

    // 创建Mixer的Playable
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        ScriptPlayable<VideoPlayerMixer> playable = ScriptPlayable<VideoPlayerMixer>.Create(graph, inputCount);
        VideoPlayerMixer mixer = playable.GetBehaviour();
        mixer.Player = Player.Resolve(graph.GetResolver());
        mixer.Player.timeReference = VideoTimeReference.ExternalTime;
        return playable;
    }
}
```



# 二、轨道拓展设计
## 1、框架中脚本的命名及职责
为了让开发人员更容易理解Playable的设计，在脚本命名上不采用上面的划分方式，而是用更贴近Playable本身的命名，包括职责也是。对应关系如下：

1. Data：脚本命名采用Clip，原意是使用ClipBehaviour，为了简化省去Behaviour。职责是存储Clip数据，并且负责单个Clip范围内的行为
2. Clip：脚本命名采用ClipAsset，指明它是Asset。职责就是创建Clip的Playable，及设置Clip是否可以融合。
3. Mixer：命名还是Mixer，完整名是MixerBehavour，为了简化省去Behaviour。职责是协调Clip间的关系，如效果融合。
4. Track：脚本命名为TrackAsset。职责是创建Mixer的Playable。

所以在我们自己的框架下，轨道的4个脚本分别是：Clip、ClipAsset、Mixer、TrackAsset



## 2、框架接口设计
### 2.1 Clip的基类
```csharp
/// <summary>
/// 跟随Clip的行为基类
/// 用来存储Clip的序列化数据，书写单个Clip范围内的逻辑
/// </summary>
public class ClipBehaviourBase : PlayableBehaviour
{
    [NonSerialized]
    public TimelineCtrl TmlCtrl;

    public sealed override void OnPlayableCreate(Playable playable)
    {
        //...
    }
}
```

核心点：

1. 在Clip内提供直接访问TimelineCtrl的字段，方便实现业务逻辑
2. 禁止子类重载OnPlayableCreate方法。一方面是防止子类重载破坏掉框架的逻辑；另一方面虽然Clip会用来实现行为逻辑，但是不建议做非常复杂的逻辑，所以减少可用的API



### 2.2 ClipAsset的基类
```csharp
/// <summary>
/// Timeline Clip Asset的基类
/// 
/// ClipAsset最主要的职责是创建ClipBehaviour的Playable，通过实现：public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
/// </summary>
[Serializable]
public abstract class ClipAssetBase : PlayableAsset, ITimelineClipAsset
{
    public virtual ClipCaps clipCaps { get; }
}
```

功能非常简单，没有特殊的设计。



### 2.3 Mixer的基类
```csharp
/// <summary>
/// 负责Clip融合行为的基类
/// </summary>
public class MixerBehaviourBase : PlayableBehaviour
{
    public TimelineCtrl TmlCtrl;

    protected bool IsPaused = false;
    protected bool IsValid = false;

    /// <summary>
    /// 自身所属的Playable
    /// </summary>
    protected ScriptPlayable<MixerBehaviourBase> MixerPlayable;

    /// <summary>
    /// 属于当前Track的全部Clip
    /// </summary>
    public ScriptPlayable<ClipBehaviourBase>[] ClipPlayables
    {
        get{}
    }


    #region ========= 自定义生命周期 =========

    protected virtual void OnFirstFrame(Playable playable, FrameData info, object playerData)
    {
    }

    /// <summary>
    /// 初始化自身
    /// 注意：该方法内，不允许对外部对象（如Actor）做写操作！！！！！
    /// </summary>
    protected virtual void OnInit()
    {
    }

    /// <summary>
    /// 被外部调用
    /// </summary>
    protected virtual void OnCall(string key, params object[] args)
    {
    }

    /// <summary>
    /// 每TML帧更新轨道的内容
    /// </summary>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    protected virtual void OnUpdate(FrameData info, object playerData)
    {
    }

    /// <summary>
    /// 让子类处理暂停的接口
    /// </summary>
    protected virtual void OnPause(bool isPause)
    {
    }

    /// <summary>
    /// 销毁
    /// </summary>
    protected virtual void OnDestroy()
    {
    }


    #region ========= Unity生命周期 =========

    public sealed override void OnPlayableCreate(Playable playable)

    public sealed override void PrepareFrame(Playable playable, FrameData info)

    public sealed override void ProcessFrame(Playable playable, FrameData info, object playerData)

    public sealed override void OnPlayableDestroy(Playable playable)

    #endregion
}
```

核心点：

1）自定义了一组生命周期方法，不依赖于Unity原生的管理流程。因为它原始的流程有问题，会让业务侧很容易出错。

如：PlayableDirector的onStopped消息会早于Mixer的OnPlayableDestroy，这就导致业务侧认为TML停止之后，它还有逻辑在执行。

2）禁止子类重载OnPlayableCreate、PrepareFrame、ProcessFrame、OnPlayableDestroy等方法，目的也是两个：1）防止破坏框架；2）强制使用我们自定义的生命周期

3）为了方便子类使用，提供TmlCtrl、MixerPlayable、ClipPlayables等字段、属性



### 2.4 Track的基类
```csharp
/// <summary>
/// 轨道资源的基类
/// 指定轨道可用的Clip需要添加Attribute标注：[TrackClipType(typeof(XxxClipAsset))]
/// </summary>
public abstract class TrackAssetBase : TrackAsset
{
    /// <summary>
    /// TrackAsset最主要的职责就是创建Mixer Playable
    /// </summary>
    public abstract override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount);

    /// <summary>
    /// 收集轨道需要的动态资源
    /// </summary>
    public virtual void CollectDynamicAssets(TimelineCtrl tmlCtrl, List<TmlDynamicAsset> assetList)
    {
    }
}
```

核心点：

1. 将CreateTrackMixer改完抽象类，可以让子类清楚的知道自己要做什么
2. 虚方法CollectDynamicAssets为以后做TML的资源预加载做预留



# 三、参考资料
1. [Extending Timeline: A Practical Guide | Unity Blog](https://blog.unity.com/engine-platform/extending-timeline-practical-guide)
2. Unite Europe 2017 - Extending Timeline with your own playables：`\\192.168.0.231\AI及Web3游戏研发中心\公共资源\08_学习资料\Unity\【Unity官方教程】Timeline 自定义轨道 [BV1Rq4y1D7Nv].mp4`	

