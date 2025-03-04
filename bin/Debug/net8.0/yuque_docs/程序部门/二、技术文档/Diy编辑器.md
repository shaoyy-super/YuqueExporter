# 一、简介
内容：捏人,换装,捏脸 游戏通用编辑器系统

![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1718677844044-16100207-9aba-4af7-a284-f1cc33fe1b96.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1396%2Climit_0)

![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1718678508098-69908c2c-866a-4efd-8fe4-f8b42ba6c0b9.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1469%2Climit_0)

![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1718678871020-8f2bb767-56a0-4689-af20-12110110bf63.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1465%2Climit_0)



# 二、结构设计
## 1、捏人 捏脸 换装
C# : 

      新增 Customizer 文件夹

ClothesResLoader.cs  换装配置读取  换装资源获取资源释放接口

CustomizerInfo.cs	 捏人换装数据获取接口

Lua :

新增 Customizer 文件夹

 CustomizerAcotrMgr.lua	   单位创建卸载

 CustomizerResMgr.lua        Prefab 和 Texture 加载 跟 卸载     对象池管理  （ 目前只需要支持两种资源类型 ）

 GL_CustomizerAcotr.lua      单位

BodyController			捏人控制器

ClothesController		换装控制器

FaceController			捏脸控制器

## 2、操作相关
镜头 ：CustomizerCamera.cs

新增 ：Page_Diy_Ctrl  UI_Diy_Ctrl  UI_Diy_Panel

# 三、详细结构设计
![](https://cdn.nlark.com/yuque/0/2024/png/43289321/1721579471012-3746f592-0ec7-43bd-9a4d-5794de028abd.png)

# 四、流程设计
![](https://cdn.nlark.com/yuque/0/2024/png/43289321/1721581952965-11c77fd8-c05e-4fb1-abe1-7baf72e33ddc.png)



# 五、提出问题.
1.部位冲突的逻辑时在c# package里面已经写好了 需要写到lua吗？

2.塑性,妆容配置存在美术编辑默认的一些吗？

![](https://cdn.nlark.com/yuque/0/2024/png/43289321/1721614189075-ef95e7d8-4db2-4d4f-b8ba-39e9952746a6.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43289321/1721613944647-c4730f91-8c45-44e3-bcbb-61feafe98e96.png)





会议问题：

换装冲突关系问题

---

# 六、总结整理（开发完成后撰写）
开发过程中，很可能产生跟上面方案不一致的地方。可能补充了更多的细节内容，可能由于某些未想到的原因推翻了方案中的一些设定。

所有开发完成后有价值的内容，都可以记录在这里。

