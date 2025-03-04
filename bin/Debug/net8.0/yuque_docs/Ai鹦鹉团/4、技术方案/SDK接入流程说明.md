## 1. SDK概况
### 1.1 SDK介绍
> 《口袋通行证》实现账号的创建以及登录流程，并支持微信和支付宝两种方式实现游戏内支付
>

### 1.2 使用场景
> 玩家游戏账号注册，登录，以及游戏内支付流程
>
> 玩家账户实名认证和游戏内容合规审核
>

## 2. 安卓端接入流程
### 2.1 前期准备
> 开发工具 Android Studio
>
> SDK开发包
>

### 2.2 配置文件
<font style="color:rgb(51,51,51);">将 </font><font style="color:rgb(51,51,51);">dx-captcha-without-risk-v5.4.4r.2ea069da.aar </font><font style="color:rgb(51,51,51);">、 </font><font style="color:rgb(51,51,51);">dx-risk-v7_3_16r_493ae717.aar </font><font style="color:rgb(51,51,51);">、 </font>

<font style="color:rgb(51,51,51);">shanyan_sdk_v2.3.5.3.aar </font><font style="color:rgb(51,51,51);">、 </font><font style="color:rgb(51,51,51);">passport-xxx.aar </font><font style="color:rgb(51,51,51);">导入 </font><font style="color:rgb(51,51,51);">app\libs </font><font style="color:rgb(51,51,51);">文件夹下</font><font style="color:rgb(51,51,51);">,</font><font style="color:rgb(51,51,51);">其中 </font>

<font style="color:rgb(51,51,51);">shanyan_sdk_v2.3.5.3.aar 按需接入，其它jar包为必须导入。 </font>

**<font style="color:rgb(51,51,51);">在 AndroidMainfest.xml 文件中添加网络权限 </font>**

<font style="color:rgb(51,51,51);"><uses-permission android:name="android.permission.INTERNET"/> </font>

<font style="color:rgb(51,51,51);"><uses-permission android:name="android.permission.ACCESS_WIFI_STATE"/> </font>

<font style="color:rgb(51,51,51);"><uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/></font>

**<font style="color:rgb(51,51,51);">在 build.gradle android 中添加引用 </font>**

<font style="color:rgb(51,51,51);">implementation fileTree(include: ['*.aar'], dir: 'libs') </font>

<font style="color:rgb(51,51,51);">implementation 'com.squareup.okhttp3:okhttp:4.12.0' </font>

<font style="color:rgb(51,51,51);">implementation 'com.google.code.gson:gson:2.10.1' </font>

<font style="color:rgb(51,51,51);">implementation 'com.tencent.mm.opensdk:wechat-sdk-android:+' //</font><font style="color:rgb(51,51,51);">微信支付 </font>

<font style="color:rgb(51,51,51);">implementation 'com.alipay.sdk:alipaysdk-android:+@aar'//支付宝支付</font>

### 2.3 SDK接口
**2.3.1 初始化**

<font style="color:rgb(51,51,51);">MetaSdk.init(Context context,SDKOptions options)</font>

**2.3.2 用户注册**

<font style="color:rgb(51,51,51);">MetaSDK.register(String mobile, String pwd, String code, String area, CallBack<Void> callback)</font>

**2.3.3 更改密码**

<font style="color:rgb(51,51,51);">MetaSDK.changePwd(String mobile, String area, String code, String newPwd,CallBack<Void> callback)</font>

**2.3.4 通过账号密码获得授权码**

<font style="color:rgb(0,0,0);">MetaSDK</font><font style="color:rgb(51,51,51);">.</font><font style="color:rgb(0,0,0);">authByPwd</font><font style="color:rgb(51,51,51);">(</font><font style="color:rgb(0,136,85);">String </font><font style="color:rgb(0,0,0);">mobile</font><font style="color:rgb(51,51,51);">, </font><font style="color:rgb(0,136,85);">String </font><font style="color:rgb(0,0,0);">pwd</font><font style="color:rgb(51,51,51);">, </font><font style="color:rgb(0,0,0);">CallBack</font><font style="color:rgb(152,26,26);"><</font><font style="color:rgb(0,0,0);">AuthCode</font><font style="color:rgb(152,26,26);">> </font><font style="color:rgb(0,0,0);">callBack</font><font style="color:rgb(51,51,51);">)</font>

**2.3.5 通过短信验证获得授权码**

<font style="color:rgb(51,51,51);">MetaSDK.authBySms(String mobile, String code, String area,CallBack<AuthCode> callBack)</font>

**2.3.6 通过手机号一键获得授权码**

<font style="color:rgb(51,51,51);">MetaSDK.authByOneKey(@NotNull String oneKeyToken,@NotNull String oneKeyAppId, @NotNull CallBack<AuthCode> callBack)</font>

**2.3.7 获取手机验证码**

<font style="color:rgb(51,51,51);">MetaSDK.getSmsCode(String mobile, String area, boolean needCheck, CallBack<Void> callback)</font>

**2.3.8 校验手机验证码**

<font style="color:rgb(51,51,51);">MetaSDK.checkSmsCode(String mobile, String code, CallBack<Void> callback)</font>

**2.3.9 获取支付渠道**

<font style="color:rgb(51,51,51);">MetaSDK.getPayWay(CallBack<List<PayWay>> callback)</font>

**2.3.10 支付宝支付**

<font style="color:rgb(51,51,51);">MetaSDK.aliPay(Activity activity, Handler handler, String orderInfo)</font>

**2.3.11 微信支付**

<font style="color:rgb(51,51,51);">MetaSDK.weChatPay(Activity activity, String orderInfo)</font>

### <font style="color:rgb(51,51,51);">2.4 PHP接口</font>
**2.4.1 初始化**

