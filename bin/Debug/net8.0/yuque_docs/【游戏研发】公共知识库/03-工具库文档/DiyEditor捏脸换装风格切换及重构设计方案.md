# 简介
1. DIYEditor工程新增风格切换功能
2. DIYEdtor业务代码后续可能需要被用于多个项目中，因此有必要对现有工程代码进行重构优化，使其可直接拷贝到游戏项目中去。主要包含以下关注点：

### 风格切换
捏脸换装新增风格切换功能。角色模型分为男模、女模，分别有三种风格，写实、卡通以及二次元，每种风格对应一个model_id，由于同种模型（例如女模）可以共用捏脸换装数据，因此这里需要对捏人、捏脸以及换装数据进行类型的划分，同一个类型的捏脸换装数据可共用，这样即使后面新出的风格类型与前面的风格不共用数据也可通过配表解决。

修改ModelDefine配置如下：

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739005754055-d3273808-ba21-47bf-9238-f01624fc81d2.png)

由于不同的风格涉及到使用不同的模型，因此在进行风格切换时需首先进行更换模型，然后更具不同的风格更改材质属性，最后设置当前模型的捏脸换装数据

### 用户数据结构设置
UserData [

current_model_id:  当前正在使用的model -->number

**[repeated]** current_suit_data [ --正在使用的套装数据

type:套装类型 -->number

suit_id:套装槽位 -->number

template_id:uuid或本地预设模板id -->string

]

**[repeated]** current_body_data [ --正在使用的形体数据

type:形体类型 -->number

template_id:uuid或本地预设模板id -->string

]

**[repeated]** current_face_data [ --正在使用的面容数据

type:面容类型 -->number

template_id:uuid或本地预设模板id -->string

]

**[repeated]** upload_suit_data [ --上传的套装数据

type:套装类型 -->number

**[repeated]** suit_data [  --最多三个，因为只有三个槽位

uuid:唯一id -->string

suit_id:套装槽位 -->number

file_name:上传套装数据，可以是一个url -->string

]

]

**[repeated] **upload_body_data [ --上传的形体数据

type:形体的类型 -->number

**[repeated] **body_data [

uuid:唯一id -->string

icon:上传的形体图片，可以是一个url -->string

file_name:上传的形体数据，可以是一个url -->string

        - [ ] ]

]

**[repeated] **upload_face_data [ --上传的面容数据

type:面容类型 -->number

**[repeated] **face_data [

uuid：唯一id -->string

icon:上传的面容图片，可以是一个url -->string

file_name:上传的面容数据，可以是一个url -->string

]

]

]

在捏人捏脸的编辑器中这个数据可通过本地持久化保存与读取，游戏中需重写该接口改为通过服务器下发获取

### 数据上传
使用http协议进行数据的上传， url可配置或直接写在lua中的define脚本中，参数有folder、uuid、data、time、以及token，其中不同的用户拥有不同的token，在捏人捏脸编辑器中将指定一个token，游戏中可通过重写函数来获取设置自己的token。数据上传成功后通过接口更新UserData并本地化进行保存，游戏中需重写该接口使用TCP协议向服务器发送数据。



### 数据下载
使用http协议进行数据下载，具体细节待与服务端沟通，需将下载过的数据进行暂存，避免二次下载，



# 结构设计
内容

:::info
1. 新增UI脚本
    1. UI_DiySex_Ctrl和UI_DiySex_View：性别选择界面
    2. UI_DiyStyle_ctrl和UI_DiyStyle_View：风格选择界面
    3. UI_DiyShare_Ctrl和UI_DiyShare_View：分享界面
2. 新增工具脚本
    1. CustomizerUpdownloadHelper：上传下载捏脸换装数据
3. 新增用户数据脚本
    1.  CustomizerUserData：用户数据脚本，包含UserData，Token等数据及相关接口，捏脸编辑中需本地持久化保存并读取数据，游戏中通过重写的方式从服务器获取数据

:::



# 详细结构设计
内容

:::info
1. **初次进入**
    1. 初次进入判断：初次进入时需选择性别以及风格，不用的性别以及风格对应不同的model，初次进入时UserData的model_id不存在，因此可通过该字段来进行初次进入的相关设置
2. **性别与风格切换**
    1. 预览展示：切换界面展示角色模型，根据切换的性别与风格拿到对应的model_id，从配表Modeldefine中拿到该模型对应的捏人、捏脸及套装的类型，之后从UserData拿到当前正在使用的数据进行设置，无数据则使用默认设置并展示预览的角色模型
    2. 切换性别与风格后更新UserData的model_id并保存，游戏中需向服务器请求更新数据
    3. 更换角色模型并根据不同风格设置材质属性
3. **截图**
    1. 实例化一个相机并设置fieldOfView、clearFlags、nearClipPlane、farClipPlane以及相机的位置
    2. 设置相机的RenderTexture并通过ReadPixels完成截图
4. **捏人捏脸换装数据上传与下载**
    1. 从CustomizerInfo获取需要上传的数据，如捏脸数据、面容数据、套装数据
    2. 将数据转成二进制后使用Https协议进行上传
    3. 通过callBack回调获取数据上传的状态，成功则更新UserData并通知游戏服务器
    4. 使用Https协议进行数据的下载
    5. 通过callBack回调获取获取下载状态并设置一个字典用于保存已下载的数据，避免重复下载
5. **捏人捏脸数据删除**
    1. 在UserData中找到需要删除的捏人捏脸数据，移除该数据即可，游戏中则通知服务器删除该数据（重写），文件服中的数据是否删除待定，不影响流程
6. **分享**
    1. 打开分享界面时上传数据并显示效果图
    2. 使用Https协议对数据进行上传
    3. 通过callback获取上传的状态，成功则使用Zxing将下载链接生成二维码
    4. 点击保存到本地将带有二维码的贴图保存到手机相册，使用NativeGallery实现
7. **导入**
    1. 点击导入按钮进行导入
    2. 解析二维码获取下载链接
    3. 通过Https协议下载数据并记录下载数据
    4. <font style="color:#DF2A3F;">获取下载数据并进行应用，若数据的model_id与当前的model_id不同则需切换模型</font>
8. 部分重要类接口展示（**游戏中需要对标红接口其进行重写**）

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739182917911-30db2f5e-d365-4824-9ca5-b4a3eb4ebe35.png)

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739178398253-c4867923-e5f0-42ae-9921-8797dc82693b.png)



:::



# 流程设计
1. 捏脸换装编辑器进入流程

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739172596064-5f3d2360-1b24-48c7-9b4a-831f47afffcc.png)

2. 上传数据流程

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739179899470-2a208d00-f736-4b0b-abeb-7a2facec5c2b.png)

3. 分享流程

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739172983721-1bd32227-7c47-4ae2-97a5-90bee760646b.png)

4. 导入流程

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739173544978-90f5e830-8153-44fa-9b96-59ed194d6294.png)

5. 性别及风格切换流程

![](https://cdn.nlark.com/yuque/0/2025/png/29579282/1739173858106-157a5cb7-a857-4cb8-8035-24bc015dc8e4.png)

# 提出问题
如果存在重点、难点问题的解决方案没想清楚，可提出问题在会上讨论

# 总结整理（开发完成后撰写）
开发过程中，很可能产生跟上面方案不一致的地方。可能补充了更多的细节内容，可能由于某些未想到的原因推翻了方案中的一些设定。

所有开发完成后有价值的内容，都可以记录在这里。

