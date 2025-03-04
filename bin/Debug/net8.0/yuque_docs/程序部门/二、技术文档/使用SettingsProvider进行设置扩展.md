### 1.问题背景：使用ScriptableObject对象作为项目配置时，会因为焦点切换，资源路径等因素造成混乱。
![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1712736032738-8ea650dd-9a1e-4d5b-91d3-a3126d4c9652.png)



### 2.解决方案：使用SettingsProvider扩展ProjectSetting和Preferences窗口。
如果没有特别的绘制需求或者分组需求，在打了[SettingsProvider]标签的静态方法内，调用SettingsProviderUtils.GetSettingsProvider返回一个默认的SettingsProvider对象即可完成扩展。(使用时先检查项目内是否已经实现了SettingsProviderUtils.GetSettingsProvider，别写多遍重复代码)



举例：



```csharp
class MyCustomDemoProjectSettings : ScriptableObject
{
    //配置对象存放路径，根据配置类型存放在合适路径
    public const string k_MyCustomSettingsPath = "Assets/Editor/MyCustomDemoSettings.asset";

    [SerializeField]
    private int m_Number;

    [SerializeField]
    private string m_SomeString;

    [SerializeField]
    private bool m_Check;

    [SerializeField]
    private List<int> m_IntList;

    [SerializeField]
    private int[] m_Dic;

    [SerializeField]
    private TextAsset m_Txt;

    [SerializeField]
    private Color m_Color;


    static MyCustomDemoProjectSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<MyCustomDemoProjectSettings>(k_MyCustomSettingsPath);
        if (settings == null)
        {
            settings = CreateInstance<MyCustomDemoProjectSettings>();
            AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    //设置扩展标签
    [SettingsProvider]
    static SettingsProvider CreateMyCustomSettingsProvider()
    {   
        //在ProjectSettings窗口进行扩展，返回绘制对象
        return SettingsProviderUtils.GetSettingsProvider(GetOrCreateSettings(), "CustomDemoTitle");
    }
}

//绘制实现，可以不用看
public class SettingsProviderUtils
{
    /// <summary>
    /// 得到默认的ProjectSettings窗口SettingsProvider
    /// </summary>
    /// <param name="serializedObject">要显示的资源对象</param>
    /// <param name="tabName">分组名</param>
    /// <param name="settingGroupPath">页签分组路径</param>
    /// <returns></returns>
    public static SettingsProvider GetSettingsProvider(ScriptableObject serializedObject, string tabName, string settingGroupPath = "Project/Meta48/")
    {
        //用于搜索时显示并高亮的关键字
        var keys = new HashSet<string>();
        //注意SettingsScope枚举参数，决定扩展设置在Preferences显示还是显示在ProjectSettings，使用ScriptableObject的一般时项目设置，所以这里写死SettingsScope.Project
        var provider = new SettingsProvider(settingGroupPath + tabName, SettingsScope.Project)
        {
            label = tabName,
            keywords = keys,
        };

        provider.guiHandler = (searchContext) =>
        {
            var settings = new SerializedObject(serializedObject);
            var pop = settings.GetIterator();
            while (pop.NextVisible(!pop.isArray))
            {
                if (pop.displayName != "Script")
                {
                    EditorGUILayout.PropertyField(settings.FindProperty(pop.propertyPath), new GUIContent(pop.displayName));
                    if (!keys.Contains(pop.displayName))
                    {
                        keys.Add(pop.displayName);
                        provider.keywords = keys;
                    }
                }
            }
            settings.ApplyModifiedPropertiesWithoutUndo();
        };
        return provider;
    }
}
```

效果如下图：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1712736332012-3b6d9fc1-9ee7-4489-99fe-f95a7b9c00cf.png)



### 3.注意事项：
对Unity来说，ProjectSettings是项目相关设置，每个项目的设置是互不影响的，对应设置的序列化文件在与Assets文件夹同级的ProjectSettings文件夹下。

而Preferences是用户偏好设置，修改后，所有Unity项目都会同步修改（可能需要重新启动unity才会同步）。

在使用SettingsProvider扩展这两个设置窗口时，SettingsScope参数，仅控制在哪个窗口显示，和设置的生效范围没有任何关系。

使用ScriptableObject来存储设置时，因为资源路径在项目内，所以一般情况下，设置的作用域只在项目内生效，不管是显示在哪个窗口。

如果想要实现跨项目设置的话，可以使用SettingsProvider + EditorPrefs（EditorPrefs是跨项目的偏好设置，重启Unity后即可同步）。如下是一个简单的举例：

```csharp
public enum LanguageType
{
    English,
    中文,
    双语,
    日本语,
}
public class LGameArtSettingGUI
{
    const string LGAMEMATERIAL_LANGUAGE_KEY = "LGAMEMATERIAL_LANGUAGE";
    public static LanguageType Language = LanguageType.English;
    static void LoadPreferences()
    {
        Language = (LanguageType)EditorPrefs.GetInt(LGAMEMATERIAL_LANGUAGE_KEY, 0);
    }

    static bool preferencesLoaded = false;
    [SettingsProvider]
    public static SettingsProvider BaseSetting()
    {
        var provider = new SettingsProvider("Preferences/Meta48/LGameArtSetting", SettingsScope.User)
        {
            label = "LGameArtSetting",
            guiHandler = (searchContext) =>
            {
                if (!preferencesLoaded)
                    LoadPreferences();
                EditorGUI.BeginChangeCheck();
                Language = (LanguageType)EditorGUILayout.EnumPopup("Language", Language);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorPrefs.SetInt(LGAMEMATERIAL_LANGUAGE_KEY, (int)Language);
                }
            },
        };
        return provider;
    }
}
```

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1712736411053-d7aacd4a-1547-4722-850f-1687ce9421a9.png)

当然，如果要在另一个项目里看到这个设置选项，也需要在那个项目里有相同的SettingsProvider相关代码。



总之，当需要扩展的‘设置’从设计和实现上都跨项目生效时，才使用SettingsScope.User，否则使用SettingsScope.Project。

