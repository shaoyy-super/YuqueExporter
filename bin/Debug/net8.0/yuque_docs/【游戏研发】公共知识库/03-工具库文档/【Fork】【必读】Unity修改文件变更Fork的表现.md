## <u>￥</u> **<u>所有对文件的修改都必须在Unity里或者打开一次Unity 再在Fork查看￥</u>**
# 一、新增（新文件导入到Unity或者在Unity新建）
Unity里新增的所有文件都会在Fork里“Local Changes”里相对应的文件夹自动更新显示出来，如下图。并且新增的文件前面会有特定的

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457860039-781b4dcc-6e85-4f52-ab60-096b2445de68.png)

绿色“新添加”标识，<font style="color:#DF2A3F;">每个新添加的文件都会有一个“.meta”后缀的文件这个文件一定要有：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457860434-3c259d54-060f-4674-8218-76f1de4ebc86.png)

:::color5
### <font style="color:#DF2A3F;">注意：</font><u><font style="color:#DF2A3F;">！！新加文件一定要有meta文件！！</font></u>
:::

# 二、修改（改属性）
1. **<font style="background-color:#FBDE28;">外部修改：非Unity内部直接修改文件，而是用外部其他软件如PS或者MAX修改了文件，重新导入到引擎目录中的操作。</font>**
2. **<font style="background-color:#FBDE28;">内部修改：直接在Unity内部修改了文件的属性的操作</font>**
3. **<font style="background-color:#FBDE28;">修改文件一定要保存（各种修改的保存方法）</font>**

**不管是外部修改还是内部修改，都必须进一次“模拟测试”验证游戏中修改内容效果是否正确，并且检查关联文件是否效果正常，有没有BUG。**

### 修改过的文件在Fork里的表现：
Unity里修改的所有文件都会在Fork里“Local Changes”里相对应的文件夹自动更新显示出来，如下图。并且新增的文件前面会有特定的

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457861090-12505a0e-068a-4d2b-a7c1-738c1f9527bb.png)

黄色“修改”标识。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457861717-ad0140d7-dc44-4365-9bd3-0e4e7d0c92b6.png)

### 注意：
1. **<font style="background-color:#FBDE28;">修改的文件大部分是没有“.meta”文件的，如果有是不正常的特别是GUID变化的，叫上组长进行查看</font>**
2. **修改的文件没有出现在“Local Changes”的Unstaged的情况，是没有在Unity里保存文件,重新回Unity保存。**

# 三、移动
## 3.1移动资源（对文件的移动操作必须在Unity里进行）
是指在Unity里移动文件位置的操作，不允许在winsome文件夹里对资源文件进行移动文件夹的操作，这会带来严重后果。

当我们在Unity把文件移动所在文件夹位置之后Fork里会出现如下情况：

1，原始文件夹下会出现红色图标的更改这表示是“删除”的意思：

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457862187-afd1c7b2-bf26-4bb2-84e7-d558e815993f.png)

“删除”标识

2，在目标文件夹下会出现绿色图标的更改文件

有以上两种情况同时出现才是移动文件的正确操作。

（帮助：对于计算机来说移动文件就相当与把原始目录的文件删了，再到目标文件夹加把文件添加进来，这样就会有原始文件夹删除，目标文件夹是添加。本质是个剪贴的过程。）

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457862531-41887561-d446-45b3-88a8-5b238d2e4811.png)

**注意：上面都是对已经上传的文件移动才会有的情况，如果你是第一次新增的文件要移动也必须在Unity里移动，但是Fork里只有一个新增的更改，不会有删除的**。

## 3.2提交移动的更改
1. **<font style="background-color:#FBDE28;">已有文件的移动后必须把“删除”和“新增”的更改一起提交</font>**
2. **<font style="background-color:#FBDE28;">移动文件一定生成有“.meta”文件，要一起提交，如果没有meta文件就是有问题的不能提交。</font>**

# 四、重命名
重命名文件必须在引擎里完成，否则后果自负

1. 重命名的文件在Fork里的情况差不多类似，也是有两个更改情况，一个是删除的更改记录一个是增加的更改记录：

下图中以“Mat__Eff_Star_19_02”重命名为“Mat__Eff_Star_19_02_Test”为例，Unstaged中会出现下列情况。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457863047-9a8e863a-80bd-4439-8b3c-c522f3f8fb44.png)

此过程必须在Unity引擎里修改。

当把重命名的文件从Unstaged通过“Stage”添加到“Staged”

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457863520-067ea363-2d27-4cd2-86f4-48afee17a43c.png)

会变成这样：会出现一个紫色的重命名图标，文件数量由之前的四个变成了两个。这是正常的。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457863919-c7595d44-1ad4-4584-939d-694a886c4766.png)![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457864349-b901494d-f4e0-4e2e-ba0c-799d234a3050.png)

“重命名”标识。

至此对文件的修改在Fork中的表现说完了。

# 五、总结：
:::color5
### **<u>看到meta需谨慎，没有meta看情况！</u>**
:::

:::color5
### **<u>初次meta才能见，再遇meta有问题！</u>**
:::

除了修改其他都会有“.meta”文件，反正则是有问题需要审核提交

重现导入引擎的文件一定要打开Unity，但是上传的是不会有“.meta”文件的

移动重命名一定要有删除记录和添加记录。

