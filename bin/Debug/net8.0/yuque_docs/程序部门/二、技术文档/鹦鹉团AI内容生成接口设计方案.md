# 一、简介
调用北京AI接口，生成音乐、文字、动画等资源。

规范AI接口调用的流程控制和资源管理。

需要实现以下功能：

1. 文生音，根据提示词生成音乐。
2. 根据音乐生成舞蹈的动画文件。

# 二、结构设计
内容：

:::tips
1. 新增AIGCManager.cs，提供创建AI任务的接口、管理AI任务流程、管理AI资源。
2. 新增IAIGCProxy.cs，作为AI代理的接口
3. 新增AIGCProxyBJ.cs，提供北京AI接口代理。

:::



# 三、详细结构设计
内容：

:::info
1. **用到的AI接口及其参数**
    1. **签名生成**
        1. 所有AI接口发送消息需要在header填入签名
        2. 签名的生成：根据当前时间和API_KEY、SECRET_KEY、APP_ID，使用SHA1加密。

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730352236071-3ccbde6e-fb84-4e28-83f3-89cd9eb10bd2.png)



    2. **文生音**
        1. 请求Url：http://maas.48.cn/api/microservices
        2. 请求方式：Post
        3. 参数名：
            1. server_id，string类型，服务id，固定传“34”
            2. text，string类型，提示文字
            3. user_callback_url，string类型，回调地址，默认为空
            4. user_callback_data，string类型，回调参数，默认为空
        4. 任务结果：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730357621880-a18621e3-5577-4bd9-8baa-465b494f437f.png)
            1. people_name：演唱者名称，列表
            2. clothes_names：服装名，列表
            3. people_number：演唱人数
            4. music_name：歌曲名
            5. music_url：歌曲url
            6. img_url：封面url
            7. img_large_url：大封面url
            8. beat：节拍
            9. bpm：每分钟节拍数
            10. duration：时长（秒）
            11. lyric：歌词
    3. **音生舞**
        1. 请求Url：http://maas.48.cn/api/microservices
        2. 请求方式：Post
        3. 参数名：
            1. server_id，string类型，服务id，固定传“43”
            2. music_url，string类型，音乐地址
            3. clothes_names，string类型，衣服名称
            4. beat，string类型，拍号，例如“4/4拍”
            5. bpm，int类型，每分钟节拍数
            6. people_number，int类型，舞蹈人数
            7. music_name，string类型，音乐名称
            8. duration，float类型，音乐时长
        4. 任务结果：
            1. ![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730354310173-5ff0ce51-349f-4745-a53c-7b9901c24c0f.png)
                1. video_url：视频url，无效，不使用
                2. fbxurl_list：fbx的url列表
                3. fbx_data_url：生成的额外数据，目前只有请求时传入的服装名称



    4. **轮询任务结果**
        1. 请求后返回trace_id，使用trace_id轮询结果。轮询间隔10秒。
        2. 请求Url：[https://maas.48.cn/api/microservices/maas-task-result](https://maas.48.cn/api/microservices/maas-task-result)
        3. 请求方式：Get
        4. 参数名：
            1. trace_id，string类型
        5. Header：
            1. accessToken，"<font style="color:rgba(0, 0, 0, 0.8);">bCREZMrvPO87i210EU6oDPBC7iy7xl-ekDgrXPva1jw"，固定值</font>
        6. 查询结果
            1. 任务未完成![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730353654637-60f077e8-af69-42a6-a5aa-fcd789fa52d8.png)
            2. 任务完成：见每个接口的任务结果说明
2. **流程控制**
    1. **任务数据结构：**
        1. 任务状态：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730360158236-257e922b-db43-4bfb-8ab3-1d479017a111.png)
        2. 任务基类：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730453202770-301fb1a2-83d6-4653-bc56-c5ef0e47e368.png)
        3. 文生音任务类：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730453215466-4143a431-2ff9-42ef-bcf4-77ff0bd7628e.png)
        4. 音生舞任务类：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730453227406-819402da-30b8-4660-b548-dfbac6352552.png)
        5. 文生音参数结构体：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730453267832-b4a294dd-c52c-4314-9726-220b8937cbed.png)
        6. 音生舞参数结构体：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730447931571-b5254018-0598-43a7-8ec1-c2fe7aa38718.png)
    2. **任务管理：**AIGCManager维护任务列表，提供接口进行任务查询、新建、状态更新等。按一定间隔轮询未完成的任务。
    3. **数据存取：**
        1. 任务列表存到本地AITask.json文件，方便重启游戏后获取所有任务数据。结构如图：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730438003925-5983e224-a8aa-42cf-8dc6-4e3d21fb5b1f.png)
        2. 单个任务数据存放在本地AITaskData文件夹下，文件名为该任务的trace_id。

文件夹结构如图：                                                                                ![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730438234699-ca72e4b6-4692-43e7-b49d-ba768380896f.png)

        3. 文生音json结构如图：![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730438542633-34b3c712-c669-44ab-a7de-93f7f0cd0595.png)
        4. 音生舞json结构如图：       ![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730438705923-b6c037c7-9b9b-485c-93e5-6a1a0817ab5a.png)
        5. 删除任务或者取消任务后，应删除AITask.json及AITaskData文件夹中对应数据和文件。
    4. **资源管理：**
        1. 存储路径：
            1. 音生舞产生的FBX文件：Application.persistentDataPath+"/AIGC/MusicToDance/FBX"
            2. 音生舞产生的gltf和bin文件：Application.persistentDataPath+"/AIGC/MusicToDance/GLTF"
        2. 删除临时资源：
            1. 音生舞中途产生的fbx文件，在提交到云之后要及时删除。
3. **类的依赖关系及重要方法**

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730458378547-54ecf8b6-f8be-4af4-9d7d-e6517a4232d8.png)

:::



# 四、流程设计
1. 文生音流程

![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730453525954-2c2ff7e2-38f4-4b8f-bb69-3b050d27b617.png)

2. 音生舞接口调用流程



![](https://cdn.nlark.com/yuque/0/2024/png/35004992/1730453570683-ca161b33-6002-4ee8-8d55-496a55cf47af.png)

# 五、提出问题




---

# 六、总结整理（开发完成后撰写）
开发过程中，很可能产生跟上面方案不一致的地方。可能补充了更多的细节内容，可能由于某些未想到的原因推翻了方案中的一些设定。

所有开发完成后有价值的内容，都可以记录在这里。

