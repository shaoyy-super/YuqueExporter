# 山海项目概况：
**Git地址：**[http://192.168.2.240/metamonsters/Monsters](http://192.168.2.240/metamonsters/Monsters)

### **项目基本信息：**
**玩法：**抓宠

**场景类型：**城市（任务相关），野外（抓宠），战斗场景

# 移植流程：
### 切换到安卓平台
### 修改manifest.json文件，接入ut-rendering
这里主要要替换urp和安装ut-rendering

```json
"com.unity.render-pipelines.core": "http://192.168.2.240/programmer/ut-graphics.git?path=/Packages/com.unity.render-pipelines.core",
"com.unity.render-pipelines.universal": "http://192.168.2.240/programmer/ut-graphics.git?path=/Packages/com.unity.render-pipelines.universal",
"com.ut.rendering": "http://192.168.2.240/programmer/ut-graphics.git?path=/Packages/com.ut.rendering",
```

**这里需要注意的是：**

<font style="background-color:#E4495B;">一定要删掉Packages下面原有的urp库，否则的话，并不会更新</font>

### 删除原有项目中冲突的内容
如果原有项目中有ut-rendering中存在的shader或者实现内容，要删掉

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724036559181-4be03059-55c3-487f-af7b-e74d6ed31341.png)

### 修改原有工程中的项目用的shader
#### 一些浅层次的修改（例如修改include目录）
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724037526251-6e70e55f-b12f-4219-b646-4b8496523d4e.png)

#### 一些深层次的修改
例如有的功能会修改到urp中的一些文件，例如要修改Lit

这个时候可以把Urp中原有的文件拷贝一份出来，写成自己需要的样子，用在项目中，如果是通用需求，可以在Ut-rendering库中进行修改，如果是项目的特殊需求，需要保留项目中的修改，修复冲突的部分

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724039082427-9d7ebc55-500b-4e5e-a290-c948dabeca5c.png)

# 相关问题及注意事项
### 拉群让相关人员（场景，人物，特效等）建立验收标准
### 遇到的部分问题
#### volume失效
原因volume的layer层级没有选对，根据项目规则必须选以下这三个layer：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724061966409-5e222440-b408-4502-afa5-4ad94abd1684.png)

#### FA材质阴影失效
原因是FA材质中阴影关键字判断是 <font style="background-color:#FBDE28;">_MAIN_LIGHT_SHADOWS </font> 

但是如果开启级联阴影的情况下，需要加上关键字 <font style="background-color:#FBDE28;">_MAIN_LIGHT_SHADOWS_CASCADE </font> 

FA材质shader中的 <font style="background-color:#FBDE28;">ActorShadowCaster</font>  改成 <font style="background-color:#FBDE28;">ShadowCaster</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724062168544-3e80a440-def2-4c8e-b948-7159141a4eb0.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724062226063-16de3d68-67ff-4ce9-ad78-ee24400a4583.png)

URP中级联阴影的设置：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724062263497-b5923497-c92a-4959-8531-0a6cb4a88823.png)

当Cascade Count大于1的时候，会走<font style="background-color:#FBDE28;">_MAIN_LIGHT_SHADOWS_CASCADE </font> 关键字

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724062332508-1b43d73b-dde4-467a-996c-fcec53c8ff9a.png)

#### 场景attachment resolutions尺寸报错
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724062483764-c313dc56-3340-4066-9390-93e87685e994.png)

原因是场景中只能有两个Camera，别的camera不能开启post processing

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724062695288-1f8b41e6-a87e-4c74-9607-de5b366570df.png)

这个深层次的出错原因还没有找到

# 项目现有遗留的临时功能
### 级联阴影
在后续Ut接入自阴影以后，应该去掉级联阴影的使用

### YYD和FA相关的材质shader
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724064754323-7bc9681a-2c65-42c9-bdaa-975e120214b1.png)

这些后续都要替换掉

### UI相关材质shader
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724065020597-10ac1676-5c99-4746-b09a-0e2aebd5b858.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1724065062251-83680b9c-981d-4e9e-80ed-e65051cda2bf.png)



### 后续要添加自动创建角色Prefab功能
