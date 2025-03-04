### 1.Monkey测试介绍
生成伪随机用户事件流，如点击、触摸、手势以及一些系统级事件。你可以通过Monkey以一种随机且可以重复的方式来对正在开发的程序进行压力测试。(仅仅是单台手机压力测试的一种方法)

扫盲贴：[https://mp.weixin.qq.com/s/xY-r2nziNuiRi4nnLcuU-Q](https://mp.weixin.qq.com/s/xY-r2nziNuiRi4nnLcuU-Q)

### 2.Monkey基本介绍
[https://testerhome.com/topics/12013](https://testerhome.com/topics/12013)

（1）基本操作介绍

（2）测试日志讲解

（3）Monkey+Logcat+DDMS 内存泄漏分析及定位

（4）Monkey+Logcat+traces  查找以及分析定位ANR问题

（5）Monkey+battery-historian 电量测试



### 3.工具设计
Pyuic设计和拼接界面，使用QT Designer来转换代码

![](https://cdn.nlark.com/yuque/0/2024/jpeg/43256946/1712733715536-1a9c6134-3159-41f5-9c65-f46169ae9a9b.jpeg)

### 4.常用操作指令
（1）**<font style="color:rgb(64, 64, 64);">-p 包名</font>**<font style="color:rgb(64, 64, 64);">：指定应用程序。例如：adb shell monkey -p 包名 事件总数</font>

（2）**<font style="color:rgb(64, 64, 64);">-v</font>**<font style="color:rgb(64, 64, 64);">：打印 log 级别，-v 越多日志信息越详细，最多支持 3 个。例如：adb shell monkey -p 包名 -v -v -v 事件总数</font>

<font style="color:rgb(64, 64, 64);">（3）</font>**<font style="color:rgb(64, 64, 64);">-s</font>**<font style="color:rgb(64, 64, 64);">：伪随机数生成器的 seed 值，通俗的说就是个标记，后面跟数字，例如：执行 adb shell monkey -s 1 -p 包名 事件总数，这个我标记了-s 1，命令操作完之后，我发现有日志报错，我想重新执行这个 monkey 操作,那你就可以继续执行这个命令，排错时常用</font>

<font style="color:rgb(64, 64, 64);">（4）</font>**<font style="color:rgb(64, 64, 64);">-f</font>**<font style="color:rgb(64, 64, 64);">：后接测试脚本名，例如：adb shell monkey -f 脚本名 事件总数</font>

<font style="color:rgb(64, 64, 64);">（5）</font>**<font style="color:rgb(64, 64, 64);">--throttle</font>**<font style="color:rgb(64, 64, 64);">：翻译减速的意思，后面接时间，单位为 ms，,表示事件之间的固定延迟，如果不接该项，monkey 将不会延迟，例如:adb shell monkey --throttle 500 -p 包名 事件总数</font>

<font style="color:rgb(64, 64, 64);">（6）</font>**<font style="color:rgb(64, 64, 64);">--pct-事件类别 11 个事件百分比控制</font>**<font style="color:rgb(64, 64, 64);">（有的是 9 种事件，没有--pct-pinchzoom，--pct-rotation 事件）由安卓 SDK 决定</font>

<font style="color:rgb(64, 64, 64);"></font>

### <font style="color:rgb(64, 64, 64);">5.基础事件参数</font>
（1）**<font style="color:rgb(64, 64, 64);">pct-touch ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：0</font>  
<font style="color:rgb(64, 64, 64);">翻译触摸，触摸事件泛指发生在某一位置的一个 down-up 事件，点击</font>

（2）**<font style="color:rgb(64, 64, 64);">pct-motion ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：1</font>  
<font style="color:rgb(64, 64, 64);">翻译动作，动作事件泛指从某一位置接下（即 down 事件）后经过一系列伪随机事件后弹出</font>

（3）**<font style="color:rgb(64, 64, 64);">pct-pinchzoom ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：2</font>  
<font style="color:rgb(64, 64, 64);">翻译二指缩放，智能机上的放大缩小手势操作事件</font>

（4）**<font style="color:rgb(64, 64, 64);">pct-trackball ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">： 3</font>  
<font style="color:rgb(64, 64, 64);">翻译轨迹，轨迹事件包括一系列的随机移动，以及偶尔跟随在移动后面的点击事件</font>

（5）**<font style="color:rgb(64, 64, 64);">pct-rotation ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：4</font>  
<font style="color:rgb(64, 64, 64);">翻译屏幕旋转，横屏竖屏事件</font>

（6）**<font style="color:rgb(64, 64, 64);">pct-nav ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：5</font>  
<font style="color:rgb(64, 64, 64);">翻译基本导航，基本导航事件主要来自方向输入设备的上、下、左、右事件</font>

（7）**<font style="color:rgb(64, 64, 64);">pct-majornav ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：6</font>  
<font style="color:rgb(64, 64, 64);">翻译主要导航，主要导航事件通常指引发图形界面的一些动作，如键盘中间按键、返回按键、菜单按键等</font>

（8）**<font style="color:rgb(64, 64, 64);">pct-syskeys ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：7</font>  
<font style="color:rgb(64, 64, 64);">翻译系统按键，系统按键事件通常指仅供系统使用的保留按键，如 HOME 键、BACK 键、拨号键、挂断键、音量键等</font>

（9）**<font style="color:rgb(64, 64, 64);">pct-appswitch ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：8</font>  
<font style="color:rgb(64, 64, 64);">翻译应用启动，应用启动事件（activity launches) 即打开应用，通过调用 startActivity() 方法最大限度地开启该 package 下的所有应用</font>

（10）**<font style="color:rgb(64, 64, 64);">pct-flip ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：9</font>  
<font style="color:rgb(64, 64, 64);">翻译翻转，键盘轻弹百分比，如点击输入框，键盘弹起，点击输入框以外区域，键盘收回</font>

<font style="color:rgb(64, 64, 64);">（11）</font>**<font style="color:rgb(64, 64, 64);">pct-anyevent ｛+ 百分比｝</font>**<font style="color:rgb(64, 64, 64);">：10  
</font><font style="color:rgb(64, 64, 64);">翻译其他类型，其他类型事件指上文中未涉及的所有其他事件，如 keypress、不常用的 button 等</font>

<font style="color:rgb(64, 64, 64);"></font>

### <font style="color:rgb(64, 64, 64);">6.为什么要进行Monkey测试</font>
（1）为了提高项目的稳定性和健壮性。  
（2）为了提高项目的留存率。  
（3）为了发现日常测试无法发现的问题以及各种极端情况。  
（4）为了对当前项目进行一些破坏性的测试和间接性的打断测试。

### <font style="color:rgb(64, 64, 64);">7.使用方式</font>
<font style="background-color:#FFFFFF;">（1）打开打包好的exe并运行程序</font>  
<font style="background-color:#FFFFFF;">（2）选择连接的设备（若遇到卡死或者异常Bug需要停止执行，则只能连接单台设备）</font>  
<font style="background-color:#FFFFFF;">（3）自行设置事件次数、seed值、延迟时间、及事件比例（事件比例总和不能超过100%，seed值只是一个指标用来复现Bug，一般来说单个模块的事件次数为5W-10W次，也可根据测试的情况或不同的测试场景适当增减）</font>  
<font style="background-color:#FFFFFF;">（4）输入要测试的包名（将查看包名的指令已经写在工具里）</font>  
<font style="background-color:#FFFFFF;">（5）开始执行  
</font><font style="background-color:#FFFFFF;">（6）可实时监控游戏内压力测试情况，若出现Bug、卡死、崩溃等情况，可停止执行并查看Log及尝试复现</font>  


###   
8.加入其他测试环境丰富Monkey测试
（1）可以加入QNET，将触摸事件比例调至50%及以上，将QNET弹窗展开时，也可以会触发和自动切换各种网络异常状态，将压力测试与各种异常网络相结合，来模拟更极端的测试环境，触发更多平常测试中无法发现和难以测试到的问题。  
（2）在打包时本地接入uwa的sdk，这样在做压力测试的同时实时监控游戏内的性能测试。  
（3）在压力测试时，打开perfdog来实时监控设备的各项数据，来分析游戏内各项指标



### 9.举例说明可以发现那些问题
（1）在新手引导下进行压力测试，不断的去切换网络状况和频繁操作，会在某些特定的时间节点或操作间隙，打断或跳过某些引导导致游戏卡死。  
（2）压力测试下的频繁的点击会让页面间快速切换，可能会暴露某些页面的层级错乱或者UI显示重叠。  
（3）压力测试甚至会出现在正常流程中意想不到的情况下疯狂测试，比如：在游戏更新时，一直打断清除缓存，导致游戏出现黑屏无法正常进行等。  
（4）压力测试还会发现因代码不规范跳转而导致的转圈游戏卡死等问题。



### 10.对于压力测试发现的问题应该怎样去复现？
（1）对于整个压力测试的过程需全程录屏和实时监控，回放录屏过程来尝试用手工测试复现Bug。  
（2）根据游戏内的Log信息，查看并推测出压力测试的测试步骤，并尝试出现或将报错报错信息截图给对应模块负责程序，查看游戏内代码。  
（3）若游戏在压力测试下出现崩溃、闪退，可先行查看性能测试报告查看是否有明显性能问题，再打开mLogcat查看游戏崩溃前的Log信息。  
（4）<font style="color:#333333;background-color:#FFFFFF;">若以上步骤还不能找到，可以使用之前执行monkey命令，再执行一遍，注意使用的seed值要一样（很大概率会复现该问题）。</font>

