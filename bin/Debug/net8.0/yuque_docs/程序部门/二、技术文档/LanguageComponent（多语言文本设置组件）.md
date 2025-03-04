

# 设计初衷：
之前所有牵涉到多语言的问题，都需要拖到`Component4Lua`脚本，然后在Lua里动态设置语言Id

1、对于有动态参数的语言，这个是不可缺少的步骤

2、对于一些固定不变的文本，特别是这种文本大量存在时，略显麻烦

3、还有些UI预制体是通过TML创建的，原本是没有任何程序逻辑的，这些预制体一旦牵涉到多语言，就没法处理，或者额外关联Lua脚本动态设置



基于上边描述的问题2和3，添加一个组件，固定不变的文本，直接设置好对应id，Lua就不再关心这些文本的设置



# 设计思路：


前提：我们的多语言配置表，是在Lua里定义的number类型枚举，如下图所示

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1733371049962-06730f2e-f71a-46b6-8982-da2b7ceead1b.png)

为了方便在Inspector选择，在`Assets/ArtNoBuild/ConfigEnum/LangConfig.asset`里要配置一套枚举和Lua对应，纯Editor显示，不需要放到Art目录

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1733371160244-ba89df43-7592-449a-8a1d-640140ff664c.png)



### 方案一：
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1733370611641-8f457e69-3b8b-48f7-a4bf-fce307d12808.png)

直接在TextMeshPro Editor里做扩展，设置当前文本的语言配置表索引和语言Id

优点：很直观，直接看到这个文本内容，以及对应的配置表id

缺点：一旦配置表语言Id修改，再去查哪些文本有引用，相对比较麻烦，当然也可以写个工具查找



### 方案二：（使用的方案）
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1733370621909-9205a317-41b1-4da4-9a9b-c5b349819d94.png)

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1733371415255-339b253a-6146-467e-8f79-7d7606562f1a.png)

不和TextMeshPro绑定，单独一个脚本，一个多语言配置表，对应一组文本，单个文本设置具体的语言Id

等于是一个预制体只需要挂一个脚本，把所有固定的语言配置到脚本上即可



优点和缺点和方案一相反，另外该方案也避免了去修改TextMeshPro源码

后续有文本Id变化，只需要定位 LanguageComponent脚本引用即可



# 用法和扩展：
### 用法：
1、在需要的预制体上加上组件 LanguageComponent

2、添加一个多语言配置，并设置多语言表Id

3、把属于这个多语言表的固定的文本添加到文本数组中

4、设置对应的语言Id即可



### 扩展：
如果新加语言表Id，需要以下步骤

1、在Lua代码 LangConfig中扩展Id

2、在LangConfig.ConfigName中配置对应的语言表Json文件名

3、在`Assets/ArtNoBuild/ConfigEnum/LangConfig.asset`添加对应的id



# 注意事项：
<font style="color:#DF2A3F;">添加新的多语言表时，要在程序群里通知一下</font>，避免出现分支A添加了Id：10,

分支B也添加了Id：10，造成LanguageComponent上选择的索引必然有一边是错的

