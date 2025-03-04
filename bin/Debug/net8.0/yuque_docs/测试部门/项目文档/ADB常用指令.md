ADB，即 Android Debug Bridge （安卓调试桥）。它就是一个命令行窗口，用于通过电脑端与模拟器或者真机设备进行交互。

## 1.介绍常用的ADB操作和Airtest封装好的ADB接口：
adb devices 命令去查看被测设备是否已经跟我们的电脑建立了连接

= android.get_default_device() 对应的ADB命令

raise AdbError(stdout, stderr)：本地ADB版本冲突

adb version 查看当前ADB版本

## 2.应用管理：
2.1 # 查看设备上的所有应用

adb shell pm list packages

2.2 # 查看设备上的第三方应用

adb shell pm list packages -3

= android.list_app(third_only=True)

2.3 # 查看设备上的系统应用

adb shell pm list packages -s 

2.4 # 安装Apk

adb install "D:/demo/tutorial-blackjack-release-signed.apk"

2.5 # 卸载应用

adb uninstall com.xxxxx.xxxx

2.6 # 查看应用详细信息

adb shell dumpsys package com.xxxxx.xxxx

## 3.文件管理
3.1 #复制设备里的文件到电脑

adb pull <设备里的文件路径>[电脑上的目录]

3.2 #复制电脑里的文件到设备

adb push <电脑上的文件路径> <设备里的目录>

## 4.模拟按键输入
4.1 # 模拟电源键 

adb shell input keyevent 26

4.2 # 模拟home键

adb shell input keyevent 3

4.3 # 模拟返回键

adb shell input keyevent 4

4.4 # 点亮/熄灭屏幕

# 点亮屏幕

adb shell input keyevent 224

# 熄灭屏幕

adb shell input keyevent 223

4.5 # 模拟滑动解锁

adb shell input swipe 200 1000 200 500

4.6 # 输入文本

adb shell input text airtest

## 5.查看设备信息
5.1 查看设备型号

adb -s RFCNA0RNBAK shell getprop ro.product.model

5.2 查看屏幕分辨率

adb -s RFCNA0RNBAK shell wm size

5.3 查看安卓系统版本

adb -s RFCNA0RNBAK shell getprop ro.build.version.release

-------------------------------------------------------------------------------------------

封装好的ADB接口：

## 1.返回应用的完整路径：path_app()
android = Android()

android.path_app("com.netease.cloudmusic")

## 2.检查应用是否存在于当前设备上：check_app()
android = Android()

android.check_app("com.netease.cloudmusic")

## 3.停止应用运行：stop_app()
stop_app("com.netease.cloudmusic")

# 启动应用: start_app()

start_app("com.netease.cloudmusic")

# 清除应用数据：clear_app()

clear_app("com.netease.cloudmusic")

## 4.安装应用：install_app()
install(r"D:\demo\tutorial-blackjack-release-signed.apk")

4.1 卸载应用：uninstall_app()

uninstall("org.cocos2dx.javascript")

## 5.关键词操作：keyevent()
keyevent("HOME")

keyevent("POWER")

## 6.唤醒设备：wake()
wake()

## 7.返回home:home()
home()

## 8.文本输入：text()
text("123")

## 9.检查屏幕是否打开：is_screenon()
android = Android()

android.is_screenon()

## 10.检查设备是否锁定：is_locked()
android = Android()

android.is_locked()

## 11.获取当前设备的分辨率：get_current_resolution()
android = Android()

android.get_current_resolution()

## 12.其他adb shell命令：shell()
shell("ls")

shell("pm list packages -3")

