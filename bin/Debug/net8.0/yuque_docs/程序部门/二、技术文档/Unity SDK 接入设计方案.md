# 一、简介
内容：将unity客户端接入相关Andriod 、IOS、PC平台，提供对应平台的登录、充值、分享、上报数据接口，

在业务层提供统一的接口，屏蔽不同平台的区别







# 二、结构设计
内容：

    1.BuildSetting.xlsm:添加AppId、分享类型、数据上报类型列

   2.ShareTypeEnum:列举了所有支持的分享方式

   3.SubmitTypeEnum : 列举了所有数据上报方式

   6. SDKManager.Lua 业务层SDK服务类，充值、分享、数据上报接口功能，提供给Lua层业务模块调用  

   7. SDKManager.cs  是对第三方平台SDK功能封装,提供统一的接口

   8.GameRoleInfo:是对游戏玩家信息抽象

   9.OrderInfo:游戏中订单信息抽象

   10.GoodsInfo:游戏中商品信息抽象





# 三、详细结构设计
接口介绍：

Share(dict<string,string>): content {url:"",imageFilePath:""}

Pay(dict<string,string> productItem) {skuType:"商品类型"，gooodsId:"商品ID",goodsDesc:"商品描述"，“amount":"商品金额"，currency:"货币类型"}



UploadData(string eventName,string data):data是JSON对象字符串，{key,value,...} 根据具体待定



![](https://cdn.nlark.com/yuque/0/2024/png/49817211/1731575735128-04f73a95-d004-4dcc-82fe-5525da8627cd.png)

<font style="color:rgba(0, 0, 0, 0);background-color:rgb(251, 251, 251);">3B%26quot%3B%26gt%3B%2BSubmitData(submitData)%3A%20void%26lt%3B%2Fp%26gt%3B%26lt%3Bp%20style%3D%26quot%3Bmargin%3A0px%3Bmargi</font>

# 四、流程设计
内容：SDK初始化 登录时序图

目的：讲解流程。通过画图让自己思考更深入、让其他人看的更直观。

![](https://cdn.nlark.com/yuque/0/2024/png/49817211/1731467575664-b0cb0885-deb1-40c6-9160-58ea2eb23d04.png)





# 五、提出问题


---

# 六、总结整理（开发完成后撰写）
开发过程中，很可能产生跟上面方案不一致的地方。需要在开发过程中完善相关内容。

1. Andriod 登录方式 需要在SDK后台配置，IOS需要写死在包里(通过同意走配置)
2. 商品需要在 SDK平台配置，同时需要在App Store & Google Play 配置
3. QuickSDK 那边不进行分包

