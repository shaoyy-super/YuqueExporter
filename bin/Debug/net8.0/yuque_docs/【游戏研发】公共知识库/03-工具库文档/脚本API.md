### MagicaManager
系统管理类，对这里面参数的设置，会影响整个系统

访问示例：

```csharp
MagicaManager.SetGlobalTimeScale(magicaGlobalTimeScale);
```

#### 属性
```csharp
public static Action OnPreSimulation;
```



```csharp
public static Action OnPostSimulation;
```

#### 方法
```csharp
public static void SetGlobalTimeScale(float timeScale)
```

```csharp
public static float GetGlobalTimeScale()
```

```csharp
public static void SetSimulationFrequency(int freq)
```

```csharp
public static int GetSimulationFrequency()
```

```csharp
public static void SetMaxSimulationCountPerFrame(int count)
```

```csharp
public static int GetMaxSimulationCountPerFrame()
```

```csharp
public static void UnloadUnusedData();
```

```csharp
public static void SetInitializationLocation(InitializationLocation initLocation)
```

### MagicaCloth
<font style="color:rgb(51, 51, 51);">操控所有布组件的主要 MonoBehaviour 类</font>

<font style="color:rgb(51, 51, 51);">此类主要由脚本操作，保存可以在运行时操作的参数</font>

#### <font style="color:rgb(51, 51, 51);">属性</font>
```csharp
[SerializeField]
private ClothSerializeData serializeData = new ClothSerializeData();
public ClothSerializeData SerializeData => serializeData;
```

```csharp
public Action<bool> OnBuildComplete;
```

#### 方法
```csharp
public bool IsValid()
```

```csharp
public void Initialize()
```

```csharp
public void DisableAutoBuild()
```

```csharp
public bool BuildAndRun()
```

```csharp
public void ReplaceTransform(Dictionary<string, Transform> targetTransformDict)
```

```csharp
public void SetParameterChange()
```

```csharp
public void SetTimeScale(float timeScale)
```

```csharp
public float GetTimeScale()
```

```csharp
public void ResetCloth(bool keepPose = false)
```

```csharp
public Vector3 GetCenterPosition()
```

<font style="color:#DF2A3F;">SerializeData2类是系统使用的参数类，包含</font><font style="color:#DF2A3F;">系统引用的私有参数，</font><font style="color:#DF2A3F;">不建议修改</font>

```csharp
public ClothSerializeData2 GetSerializeData2()
```

```csharp
public void AddForce(Vector3 forceDirection, float forceVelocity, ClothForceMode fmode = ClothForceMode.VelocityAdd)
```

```csharp
public void SetSkipWriting(bool sw)
{
  if (IsValid())
  {
    Process.SetSkipWriting(sw);
  }
}
```

### <font style="color:rgb(51, 51, 51);">ClothSerializeData </font>
此类包含所有参数，<font style="color:rgb(51, 51, 51);">也可以在运行时操作此类以更改参数。</font><font style="color:rgb(51, 51, 51);">但是，某些参数在运行时无法更改</font>

<font style="color:rgb(51, 51, 51);">此类的成员可以作为 JSON 导出和导入。这些通常用作预设功能。</font>

<font style="color:#DF2A3F;">是否可以在运行时进行更改，是否可以导出为预设，以注释的形式描述如下</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1722231635547-2ab7cc2a-abae-4b3f-a36c-2019b4f89125.png)

<font style="color:#DF2A3F;">若要在运行时更改参数，请先操作 ClothSerializeData 类的相应成员。</font><font style="color:#DF2A3F;">  
</font><font style="color:#DF2A3F;">然后调用 SetParameterChange（） 以通知系统更改。</font><font style="color:#DF2A3F;">  
</font><font style="color:#DF2A3F;">此操作对所有参数都是通用的。</font>

#### 属性
```csharp
public ClothProcess.ClothType clothType = ClothProcess.ClothType.MeshCloth;
```

```csharp
public List<Renderer> sourceRenderers = new List<Renderer>();
```

```csharp
public PaintMode paintMode = PaintMode.Manual;
```

```csharp
public List<Texture2D> paintMaps = new List<Texture2D>();
```

```csharp
public List<Transform> rootBones = new List<Transform>();
```

```csharp
public RenderSetupData.BoneConnectionMode connectionMode = RenderSetupData.BoneConnectionMode.Line;
```

```csharp
[Range(0.0f, 1.0f)]
public float rotationalInterpolation = 0.5f;
```

```csharp
[Range(0.0f, 1.0f)]
public float rootRotation = 0.5f;
```

```csharp
public ClothUpdateMode updateMode = ClothUpdateMode.AnimatorLinkage;
```

```csharp
[Range(0.0f, 1.0f)]
public float animationPoseRatio = 0.0f;
```

```csharp
public ReductionSettings reductionSetting = new ReductionSettings();
[System.Serializable]
public class ReductionSettings : IDataValidate
{
  /// <summary>
  /// Simple distance reduction (% of AABB maximum distance) (0.0 ~ 1.0).
  /// 単純な距離による削減(AABB最大距離の%)(0.0 ~ 1.0)
  /// [NG] Runtime changes.
  /// [NG] Export/Import with Presets
  /// </summary>
  [Range(0.0f, 0.1f)]
  public float simpleDistance = 0.0f;

  /// <summary>
  /// Reduction by distance considering geometry (% of AABB maximum distance) (0.0 ~ 1.0).
  /// 形状を考慮した距離による削減(AABB最大距離の%)(0.0 ~ 1.0)
  /// [NG] Runtime changes.
  /// [NG] Export/Import with Presets
  /// </summary>
  [Range(0.0f, 0.1f)]
  public float shapeDistance = 0.0f;
}
```

```csharp
public CullingSettings cullingSettings = new CullingSettings();
```

```csharp
public NormalAlignmentSettings normalAlignmentSetting = new NormalAlignmentSettings();
```

```csharp
[Range(0.0f, 10.0f)]
public float gravity = 5.0f;
```

```csharp
[Range(0.0f, 1.0f)]
public float gravityFalloff = 0.0f;
```

```csharp
public CurveSerializeData damping = new CurveSerializeData(0.05f);
```

```csharp
public CurveSerializeData radius = new CurveSerializeData(0.02f);
```

```csharp
public InertiaConstraint.SerializeData inertiaConstraint = new InertiaConstraint.SerializeData();
```

```csharp
public TetherConstraint.SerializeData tetherConstraint = new TetherConstraint.SerializeData();
```

```csharp
public DistanceConstraint.SerializeData distanceConstraint = new DistanceConstraint.SerializeData();
```

```csharp
public TriangleBendingConstraint.SerializeData triangleBendingConstraint = new TriangleBendingConstraint.SerializeData();
```

```csharp
public AngleConstraint.RestorationSerializeData angleRestorationConstraint = new AngleConstraint.RestorationSerializeData();
```

```csharp
public AngleConstraint.LimitSerializeData angleLimitConstraint = new AngleConstraint.LimitSerializeData();
```

```csharp
public MotionConstraint.SerializeData motionConstraint = new MotionConstraint.SerializeData();
```

```csharp
public ColliderCollisionConstraint.SerializeData colliderCollisionConstraint = new ColliderCollisionConstraint.SerializeData();
```

```csharp
public SelfCollisionConstraint.SerializeData selfCollisionConstraint = new SelfCollisionConstraint.SerializeData();
```

```csharp
public WindSettings wind = new WindSettings();
```

```csharp
public SpringConstraint.SerializeData springConstraint = new SpringConstraint.SerializeData();
```

#### 方法
```csharp
public bool IsValid()
```

```csharp
public string ExportJson()
```

```csharp
public bool ImportJson(string json)
```

```csharp
public void Import(ClothSerializeData sdata, bool deepCopy = false)
public void Import(MagicaCloth src, bool deepCopy = false)
```

### <font style="color:rgb(51, 51, 51);">CurveSerializeData</font>
<font style="color:rgb(51, 51, 51);">CurveSerializeData 是一个用于根据深度设置参数值的类</font>

```csharp
[System.Serializable]
public class CurveSerializeData
{
  /// <summary>
  /// Basic value.
  /// </summary>
  public float value;

  /// <summary>
  /// Use of curves.
  /// </summary>
  public bool useCurve;

  /// <summary>
  /// Animation curve.
  /// </summary>
  public AnimationCurve curve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f);

  public CurveSerializeData();

  public CurveSerializeData(float value);

  public CurveSerializeData(float value, float curveStart, float curveEnd, bool useCurve = true);

  public CurveSerializeData(float value, AnimationCurve curve);

  public void SetValue(float value);

  public void SetValue(float value, float curveStart, float curveEnd, bool useCurve = true);

  public void SetValue(float value, AnimationCurve curve);

  /// <summary>
  /// Get the current value of Time(0.0 ~ 1.0).
  /// </summary>
  /// <param name="time"></param>
  /// <returns></returns>
  public float Evaluate(float time);
}
```

### <font style="color:rgb(51, 51, 51);">MagicaSphereCollider</font>
#### 属性
```csharp
public Vector3 center;
```

#### 方法
```csharp
public void SetSize(float radius)
```

```csharp
public virtual Vector3 GetSize()
```

```csharp
public void UpdateParameters()
```



### <font style="color:rgb(51, 51, 51);">MagicaCapsuleCollider</font>
#### 属性
```csharp
public Direction direction = Direction.X;
```

```csharp
public bool reverseDirection = false;
```

```csharp
public bool alignedOnCenter = true;
```

```csharp
public bool radiusSeparation = false;
```

```csharp
public Vector3 center;
```

#### 方法
```csharp
public void SetSize(float startRadius, float endRadius, float length)
```

```csharp
public override Vector3 GetSize()
```

```csharp
public void UpdateParameters()
```



### <font style="color:rgb(51, 51, 51);">MagicaSettings</font>
#### 属性
```csharp
public RefreshMode refreshMode = RefreshMode.OnAwake;
```

```csharp
[Range(Define.System.SimulationFrequency_Low, Define.System.SimulationFrequency_Hi)]
public int simulationFrequency = Define.System.DefaultSimulationFrequency;
```

```csharp
[Range(Define.System.MaxSimulationCountPerFrame_Low, Define.System.MaxSimulationCountPerFrame_Hi)]
public int maxSimulationCountPerFrame = Define.System.DefaultMaxSimulationCountPerFrame;
```

```csharp
public TimeManager.UpdateLocation updateLocation = TimeManager.UpdateLocation.AfterLateUpdate;
```

```csharp
public MagicaManager.InitializationLocation initializationLocation = MagicaManager.InitializationLocation.Start;
```

#### 方法
```csharp
public void Refresh()
```



### <font style="color:rgb(51, 51, 51);">PreBuildScriptableObject</font>
![](https://cdn.nlark.com/yuque/0/2024/jpeg/46334471/1722237326923-0c83fc3e-e916-42ae-b3a2-8d4b0cd0cbf4.jpeg)

<font style="color:rgb(51, 51, 51);">这是由 pre-build 函数生成的布料数据资产。</font>  
<font style="color:rgb(51, 51, 51);">它与 MagicaCloth 组件分开管理。</font>

```csharp
public void Warmup();
```



### <font style="color:rgb(51, 51, 51);">PreBuildDataCreation</font>
<font style="color:rgb(51, 51, 51);">PreBuildDataCreation 类是一个 Static 类，用于支持预构造。</font>  
<font style="color:rgb(51, 51, 51);">只有在 Unity 编辑器中进行编辑时，才能访问此类</font>

```csharp
public static ResultCode CreatePreBuildData(MagicaCloth cloth, bool useNewSaveDialog = true);
```

