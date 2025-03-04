# 一.简介
Dotfuscator是一款对dll进行混淆的软件，经由该软件混淆过的dll，再使用ILSpy等反编译工具打开，会发现反编译后的代码，对于人类而言，可读性大幅降低。



在如图路径中获取软件安装包，及本文第四步中的使用的工具。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724842518672-4baf60c0-7482-4459-9f27-85a817a6fb5f.png)

破解Dotfuscator：

安装成功后，替换下图中的压缩包内的两个dll到Dotfuscator安装目录。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724842621316-9b5478e4-3872-486b-80bf-98e60b2ac9a0.png)

# 二.软件的基本配置及概念
## 输入输出：
对于Dotfuscator来说，有一个叫做混淆工程的概念，实际上就是一个记录了混淆配置的xml文件。

如下，点击File-》New创建新的混淆工程。然后Ctrl+S将该xml保存在合适位置，同时混淆后的dll将会默认放在该目录/Dotfuscated下。也可以从Settings->BuildSettings->Destination Directory处更改输出目录。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724742997249-266f0bb9-0e10-42e4-bf18-67f5fcd376a8.png)

在Input处，点击文件夹图标，选择要混淆的dll：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724747757245-1eb1e074-b5b8-4592-9d01-5bc9340cf52d.png)



## 重命名（Rename）：
将类，方法，和字段重命名为无意义的名称，在逻辑不被破坏的前提下，提高人类理解代码的难度。



Rename->Options->Use <font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">Enhanced Overload Induction：使用增强重载归纳。</font>

<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">重载归纳</font><font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">指将尽可能多的方法/字段重命名为相同的名称，如下图，很多方法被重命名为a，通过重载的方式，即根据参数数量和类型的不同，来尽量使用同一个命名:</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724749154356-1c999ba3-fb71-4336-b45a-b5a8746c78bb.png)

<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">增强重载归纳：</font>

<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">在过载感应的基础上，把字段类型，和方法返回值的不同，也作为唯一区分，c#默认是不支持这种写法的，但是在这里可以实现，以增加干扰度。如下图是开启加强后的，类型不一样的时候，可以使用相同名字来作为干扰：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724749376382-10263e2c-28aa-4e2f-94e7-c46f58d739ac.png)

Rename->Options->Renaming Scheme<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">：重命名方案。决定了使用什么字符来替换原命名，可以选大写字母，小写字母，数组，不可打印符号。建议无特殊需求，使用“不可打印符号”选项，以最大层度的混淆命名。</font>

<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;"></font>

Rename->Options->Include serializable type<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">：包含可序列化类型。一定要取消勾选，可序列化的类，通常不可重命名。</font>

<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;"></font>

Rename->Options->Exclude<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">：排除。排除对某些类/方法/字段的重命名。如下，左侧，手动选择。右侧，添加规则，可以使用正则。（后续会介绍更简单的规则添加模式）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724750424280-734f6b74-e373-4b17-8f62-5a553884454a.png)



## 库模式（Library）：
在非库模式下，Dotfuscator会对所有访问权限的类/方法/字段进行重命名

在库模式下，Dotfuscator只会对非Public的类/方法/字段进行重命名。<font style="background-color:#FBDE28;">有的dll会使用友元特性，来访问另一个dll中的非public变量，注意，此时一定要将两个dll都添加到input中，一起进行混淆，同时重命名调用处和定义处。否则可能会导致混淆后的dll不可用。</font>



根据自己的需求，选择是否使用库模式，这是十分重要且基础的配置。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724750196766-207d1124-9aab-4fff-997e-62cc08a8fe3c.png)



## 字符串加密（String Encryption）：
可以选择对某些方法内的字符串字面量进行加密。

如下图，对Function1进行内定义的字符串字面量进行加密，Function2不做处理。可以看到“Function1”变为不可读，并在运行时调用生成的解密函数，进行恢复。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724751138498-edd8b0b9-a771-43a8-bc88-3f8cd68cc7c0.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724751190502-46c79450-4555-4147-ad95-6a6d5a99dfca.png)

需要注意到解密函数通常是有消耗的，所以应该仅对敏感字符串进行加密（如密码等）。

如下图，在左侧手动选择需要加密的方法，或者在右侧添加规则。<font style="color:rgb(34, 34, 34);background-color:#FFFFFF;">（后续会介绍更简单的规则添加模式）</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724752091312-a92adab0-88df-421f-ae15-9433f6c3c5b9.png)





## 控制流（Control Flow）：
在代码中添加会执行的，但不影响原方法执行效果的代码，即通过添加垃圾代码的方式，使逻辑在正确运行的前提下，丧失可读性。

如下，尽管方法和参数名被重命名了，但仍能明白这个方法做了什么，只是稍微麻烦了些。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724752455419-052f8579-b3b3-4a1f-bd20-937da11d74d1.png)



现在，我们打开控制流，再次加密。如下图，代码中注入了大量的分支/迭代/条件结构，已经近乎彻底失去了可读性：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724752596197-083b8f4d-87ab-4303-b86e-4238597e5113.png)



同时需要注意到，这些代码是会真实执行的，也就是会在一定程度上增加方法执行的CPU时间（很能会影响比较大）。

如下图，选择混淆等级，在等级为高时，可能会产生错误代码。引用官方文档所说：“**<font style="color:rgb(34, 34, 34);background-color:rgb(249, 249, 249);">High</font>**<font style="color:rgb(34, 34, 34);background-color:rgb(249, 249, 249);"> 的唯一宗旨在于击败自动反编译器的级别</font>”。<font style="background-color:#FBDE28;">建议不开启控制流，或者使用低等级，同时充分测试，保证注入的代码不大幅度影响效率，不产生错误</font>。：



![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724752804570-899a4041-fcd5-4f29-b2b8-527e3b234cc3.png)

排除不需要控制流干扰的类或方法

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724752831005-b53395c6-a0d8-4f47-a7c8-a043bf2fb7ed.png)



## 依赖路径:
当加密a.dll时，Dotfuscator需要访问a.dll所依赖的所有dll，以避免混淆到对其他dll的调用。

为此，需要在如下图所示位置，添加这些依赖的dll所在的路径，当缺少时，混淆失败。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724753339226-a53b8431-d6de-4fb9-9aca-0c65f5aa1692.png)



## 功能开关：


![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724755587967-3795c89b-6855-47ef-942a-a112881efc50.png)



## 保质期检查（Shelf Life Check）：
通过为dll设置一个日期上的有效期截止日期，Dotfuscator将会在选定的方法中注入判断是否过期的代码，在该方法执行后，可以选择通过布尔值字段来承接是否过期的信息，然后自行决定过期后的表现。



在Checks->Add Shelf Life Check处打开配置窗口，左侧需要配置5处，右侧需要配置1处。

ActivationKeyFile：证书文件，该文件通过软件官方邮件申请，正常流程沟通，可能会在1个月内得到证书。根据官方邮件答复所述，A账号申请的证书，不能给B账号使用；社区版申请的证书，不能在专业版上使用；A版本软件申请的证书，不能在B版本软件上使用。

但实测下来，我社区版5.x申请的证书，在pro破解版6.x上可以正常工作。



ExpirationDate：过期时间

ExpirationNotificationSkinElement：通知的类型。可选字段，属性，方法等，此处使用字段（Field）

ExpirationNotificationSkinName：承接检查结果的字段名，需要是bool类型。当为True时表示已过期。

ExpirationNotificationSkinOwner：承接检查结果的字段的所属类型



右侧Locations：指定Dotfuscator需要在哪个方法内进行字段修改的逻辑注入。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1728373421219-331d2908-6c7f-4bc1-8ef0-ca9c24e18495.png)

以上图的配置为例，对应下图代码，尤其过期时间设置为例2024/10/07，而当前测试时间为2024/10/08，所以applicationHasExpired会在LoadDataFiles执行后变为True，表示已过期。

![使用示例](https://cdn.nlark.com/yuque/0/2024/png/43256925/1728373404628-52950683-d46c-42fd-b6f6-462ae122d4e1.png)

![检查结果](https://cdn.nlark.com/yuque/0/2024/png/43256925/1728374454217-d6f32e77-2464-4814-91dd-c4a48ec47b21.png)



现在，我们将过期时间设置为2024/10/09，再次进行测试。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1728374617394-71fc17ac-34c9-4413-9e58-d7046e53cbd5.png)

如下图，可以看到，applicationHasExpired这个值为False, 表示尚未过期。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1728374655089-99182093-8350-4f7b-a430-f80a1d6f93ae.png)



# 三.UnityPackages使用Dotfuscator导出
## DLL从哪里来:
可以选择在vs里创建类库工程，添加现有项目等一系列操作，将一个Unity项目转为一个类库项目。但由于Unity项目的项目通常依赖关系都比较复杂，单单对于UnityEngine.dll的依赖链就很难处理，在此并不推荐这种做法，否则就需要一个一个的找到几十个依赖，并添加进去。



EditorDll可以再ProjectRoot\Library\ScriptAssemblies下找到并直接使用它来作为加密源Dll

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724754357425-bd6621c0-4b06-4b28-9fb9-0534128fd3de.png)

Library\ScriptAssemblies下的RuntimeDll会引用UnityEditor.dll，出包会失败。

所以RuntimeDll则需要通过unity提供的PlayerBuildInterface.CompilePlayerScripts接口来生成所有dll到指定路径，生成时需指定目标平台，此处可选择为win。

同时需要注意的时，由于指定了平台，会对代码做宏裁剪，为了保证再编辑器和真机上能够使用同一份DLL，Runtime的代码里，不能使用EditorAPI。

即RuntimeDll对应的程序集，不能使用 #if UNITY_EDITOR  #endif代码块。

如需访问，需通过反射来实现。



## 常见的依赖路径：
与unity版本和安装路径有关：

C:\Program Files\Unity\Hub\Editor\2022.3.22f1\Editor\Data\Managed\UnityEngine

C:\Program Files\Unity\Hub\Editor\2022.3.22f1\Editor\Data\UnityReferenceAssemblies\unity-4.8-api\Facades

C:\Program Files\Unity\Hub\Editor\2022.3.22f1\Editor\Data\UnityReferenceAssemblies\unity-4.8-api



所在项目的ScriptAssemblies

D:\Dev\DIY\DIYBody\DIYBody\Library\ScriptAssemblies



项目内的Plugins路径

D:\Dev\DIY\DIYBody\DIYBody\Assets\Customizer\Plugins

## 友元特性：
作为unity工具dll，通常会有customRuntime.dll和customEditor.dll。

经常的，由于customEditor做界面扩展，不得不将customRuntime中的某些类型设置为Public。对于此种需求，或可考虑使用c#的友元特性，以增加库模式下混淆的程度。

# 四.使用json配置重命名/字符串加密
记录一个类的哪些字段和方法需不需要重命名，应该不依赖具体的混淆工具。同时为操作更加高效便捷，在此提供一个exe工具，它将json格式配置的“重命名忽略”和 "开启字符串加密"，覆盖覆写到Dotfuscator工程xml的对应节点。



## json配置
分为三个大块，提供三种粒度的设置模式，从上至下，分别是：

针对所有类的x字段/方法

针对x类的所有字段/方法

针对x类的y字段/方法

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724754818759-48577615-23ca-4aa1-aca7-043231326d15.png)

详情参见json注释：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724755038031-9c7e1315-6aaf-4ad2-b553-fe54733a549a.png)



需要注意的是，对于unity项目，生命周期函数和各种回调，都是私有的，但不能被重命名，否则unity无法通过反射找到这些方法并调用。同时被UnityEngine.SerializeField标记的字段，也不应该进行重命名。



我们在下图中提供了这些过滤项，并希望作为一个Unity配置模板，不断添加扩展：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724755231500-9c40d12c-50cf-4a7c-b4ac-69db1f081f6c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724755281574-8441865f-1cf7-4e91-8ca2-e1490e12984e.png)

模板：

```lua
{
    "AnyClass" :
    {
        "ignoreRenameMethodsDes":"对于任何class，在下面这个列表中的非公开方法，不进行重命名",
        "ignoreRenameMethods": 
        [
            "Awake",
            "OnEnable",
            "Reset",
            "Start",
            "OnDisable",
            "OnDestroy",

            "Update",
            "FixedUpdate",
            "LateUpdate",

            "OnStateMachineEnter",
            "OnStateMachineExit",
            "OnStateEnter",
            "OnStateUpdate",
            "OnStateExit",
            "OnAnimatorMove",

            "OnTriggerEnter",
            "OnTriggerStay",
            "OnTriggerExit",

            "OnCollisionEnter",
            "OnCollisionStay",
            "OnCollisionExit",

            "OnMouseEnter",
            "OnMouseDown",
            "OnMouseUp",
            "OnMouseExit",
            "OnMouseOver",
            "OnBeginDrag",
            "OnDrag",
            "OnEndDrag",

            "OnApplicationQuit",
            "OnApplicationQuitPause",

            "OnGUI",
            "OnDrawGizmos",

            "OnRectTransformPositionChanged",
            "OnBeforeTransformParentChanged",
            "OnTransformParentChanged",

            "OnWillRenderObject",
            "OnPreCull",
            "OnBecameVisible",
            "OnBecamelnvisible",
            "OnPreRender",
            "OnRenderObject",
            "OnPostRender",
            "OnRenderlmage"
        ],

        "ignoreRenameFieldAttrsDes":"对于任何class，如果字段标记有下面中的某一个特性，不进行重命名",
        "ignoreRenameFieldAttrs": 
        [
            "UnityEngine.SerializeField"
        ],

        "ignoreRenameMethodAttrsDes":"对于任何class，如果方法标记有下面中的某一个特性，不进行重命名",
        "ignoreRenameMethodAttrs": 
        [
            "UnityEngine.SerializeField"
        ]
    },

    "OneClass" :
    {
        "ignoreRenameClassDes":"在下面这个列表中的class的类名 和 方法名 和 字段名，不进行重命名",
        "ignoreRenameClass": 
        [
          "UT.RS.SingletonMgr",
          "UT.RS.ssss"
        ]
    },

    "OneClassPart" :
    [
        {
            "className":"UT.RS.Patch.HttpDownloader",

            "ignoreRenameMethodsDes":"对于本className，在下面这个列表中的方法名，不进行重命名",
            "ignoreRenameMethods":
            [
                "_PullFromWaiting",
                "_SaveFile"
            ],
            
            "ignoreRenameFieldDes":"对于本className，在下面这个列表中的字段名，不进行重命名",
            "ignoreRenameFields": 
            [
                "_retryTimes",
                "_activeDownloads"
            ],

            "openStringEncryptionDes":"对于本className，在下面这个列表中的方法内的字面字符串，会进行字符串加密",
            "openStringEncryptionFunction": 
            [
                "_SaveFile4"
            ]
        }
    ]
}
```



如下图，展示了这个小的exe工具的使用方法：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1724755478598-7c74fe43-3187-4dd9-b8e6-cbbe2e57309b.png)

在exe所在路径内打开命令行

.\ConvertToDotfuscatorCfg.exe  json地址 xml地址

