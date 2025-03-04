### **<font style="color:rgb(34,34,34);">1.表头结构</font>**![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716176941340-e6d0665c-be21-4137-a603-cb9eb1828078.png)


如图，第一行和第二行进行了合并，表示Key，英文小写，使用下划线间隔单词，允许第一行的某几列合并，如图AE-AG，表示从属结构，如supplier.desc。



第三行为类型批注，详见下文。

第四行为中文描述，用以说明该列配置。



每个sheet代表一个数据表，sheet的名字需有意义，且不可重复，即使在不同的文件中，也不可重复命名sheet。

第一列必须命名为id，值为整数，不需要一定是连续的整数，在本sheet内不可重复。

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716192322701-9d57a4b4-8349-4d8e-8f9b-6c76675e4e2b.png)

### **<font style="color:rgb(34,34,34);">2.批注字段</font>**
<font style="color:rgb(34,34,34);">不区分大小写（建议保持首字母大写，增加辨识度）。</font>

<font style="color:rgb(34,34,34);"></font>

<font style="color:rgb(34,34,34);">时间相关：标注了Date或者Time的，导表时会检查格式是否正确：</font>

<font style="color:rgb(34,34,34);">Date:形如 "2021/10/18"</font>

<font style="color:rgb(34,34,34);">Time:形如 "10:24:00"</font>

<font style="color:rgb(34,34,34);"></font>

Array：数组，<font style="color:rgb(34,34,34);">使用</font><font style="color:#DF2A3F;">英文分号";"作为分割</font><font style="color:rgb(34,34,34);">，其中每一项一般为整数，或字符串。支持同时有字符串和数字，一般这种情况需要和程序约定索引意义。如：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716175905699-fd23262c-dec8-4db4-b477-c098c1a3415e.png)



<font style="color:rgb(34,34,34);">Normal:</font>

<font style="color:rgb(34,34,34);">整数数字。（注意，配置里</font><font style="color:#DF2A3F;">不允许出现小数</font><font style="color:rgb(34,34,34);">，如“0.1”，这种情况一般会配成100，即千分比，并和程序约定。）		</font>

<font style="color:rgb(34,34,34);">如：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716175274721-4c6d554c-940c-4a54-8c56-07e7c1da5137.png)

<font style="color:rgb(34,34,34);">字符串(非语言包)，如：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716175397502-50108110-8bac-4a63-bea4-76c1b12b888e.png)

父级标注（id列未合并，合并的情况参见table标注说明）：

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716175711729-470e0814-58bf-4d89-9d9e-e2882cabf244.png)



<font style="color:rgb(34,34,34);">Lang:语言包。</font><font style="color:rgb(34,34,34);">标注了Lang的列，且在Normal文件夹下的配置，会在执行Excel OutPut.exe时收集到Final String表，参与翻译。如：</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716175336222-f69a9db4-b97b-47d2-b9cc-ae59e1ae488f.png)





<font style="color:rgb(34,34,34);">Table：标注了Table的列，面向id列（第一列）合并单元格和单个单元格共存的情况。此时要在所有的单列的表头中加入Table，保证结构统一。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1716176189939-c9c20d2c-3454-4779-a63e-7e949b016ccb.png)





### **<font style="color:rgb(34,34,34);">3.复合表头</font>**
<font style="color:rgb(34,34,34);">表头有从属结构时，用</font><font style="color:rgb(230,36,18);">分号</font><font style="color:rgb(34,34,34);">分隔，</font><font style="color:rgb(230,36,18);">分号</font><font style="color:rgb(34,34,34);">左侧的标注针对父级生效，右侧针对子级生效。</font>![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1715408621279-a5cab168-117c-4320-a3e3-44e46f1bc9f1.png)

<font style="color:rgb(34,34,34);">D3格子Normal;Lang Normal针对exp_lv，Lang针对name。</font>

### **<font style="color:rgb(34,34,34);">4. 其他配置说明</font>**
<font style="color:rgb(34,34,34);">传参替换导出根目录：-p \client\Assets\</font>

<font style="color:rgb(34,34,34);"></font>

### <font style="color:rgb(34,34,34);">5.特殊隐藏机制</font>
<font style="color:rgb(34,34,34);">因历史遗留原因，如下图</font>

<font style="color:rgb(34,34,34);">左侧非复合表头，第一段和第二段需要使用英文</font><font style="color:#DF2A3F;">逗号</font><font style="color:rgb(34,34,34);">进行分割</font>

<font style="color:rgb(34,34,34);">右侧复合表头，第一段和第二段需要使用英文</font><font style="color:#DF2A3F;">分号</font><font style="color:rgb(34,34,34);">进行分割</font>

<font style="color:rgb(34,34,34);">否则标注可能不符合预期</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256925/1732160471654-f7b28dd1-347e-44fe-b1c3-eca80a5e2c28.png)

