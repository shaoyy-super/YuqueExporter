## 一.录制回放
## 后续补充
##   
二.性能测试  
后续补充
##   
  
三.一机多控
需求：对于不会写代码进行批量自动化，或是没有时间去调试的测试人员在对一个有大量多人交互的测试场景

使用场景：  
（1）多人交互、匹配、PVP场景等测试

（2）批量机器进行兼容性测试



github地址：  
[发布 ·支付宝/SoloPi](https://github.com/alipay/SoloPi/releases)

  
app包：

\\192.168.0.231\AI及Web3游戏研发中心\公共资源\07_测试用\一机多控

  
adb环境搭建

  
1.下载并解压 \\192.168.0.231\AI及Web3游戏研发中心\公共资源\07_测试用\工具\pyqt5



2.找到adb.exe目录位置  
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731044529809-1ed0d206-9004-461e-9e4d-25018cb042e9.png)

3.将上述地址添加到系统变量里  
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731044748487-5c3b5b49-7bfe-4d84-9c9d-8283d930714f.png)  
  
4.手机连接电脑  
（1）手机打开开发者模式  
（2）用Usb连接电脑  
（3）输入cmd指令查看  
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731044846853-be3bc220-f5a5-4a6b-beb4-89c4ec0dac60.png)



5.开启wifi调试模式

执行命令：adb tcpip 5555

直到返回<font style="color:rgb(35, 38, 59);">restarting in TCP mode port: 5555 确保已经开启无线ADB调试模式</font>

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731051287596-f0e90e7c-890a-47df-a10b-2ffca0aa5828.png)

<font style="color:rgb(35, 38, 59);"></font>

<font style="color:rgb(35, 38, 59);"></font>

6.SoloPi权限

<font style="color:rgb(35, 38, 59);">VIVO设备，如果在开发者选项中包含“USB安全操作”，需要确保开启，否则录制回放与一机多控功能可能会无法正常操作。</font>

<font style="color:rgb(35, 38, 59);">小米设备需要开启开发者选项中的 USB安装 与 USB调试（安全设置） ，否则录制回放与一机多控功能会无法正常操作；此外，还需要手动开启Soloπ应用权限中的 后台弹出界面 选项，否则无法正常使用。</font>

<font style="color:rgb(35, 38, 59);">  
</font><font style="color:rgb(35, 38, 59);">所有权限全部开启，直到走到这一步  
</font>![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731051163558-8a678df8-7643-4db8-a467-eb722cced810.png)<font style="color:rgb(35, 38, 59);">  
  
</font><font style="color:rgb(35, 38, 59);">7.添加主机、从机，并让主机和从机关联</font>

<font style="color:rgb(35, 38, 59);">1.主机开启主机模式  
</font><font style="color:rgb(35, 38, 59);">2.从机开启从机模式  
</font><font style="color:rgb(35, 38, 59);">3.使用主机扫描从机的二维码</font>

<font style="color:rgb(35, 38, 59);">4.手动点击连接设备  
</font>![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731051996459-dd166262-17b0-4898-8a20-25430ff550b7.png)<font style="color:rgb(35, 38, 59);">  
</font>![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1731052077522-7d9eefe6-fc7a-47b5-be13-55282d8df690.png)<font style="color:rgb(35, 38, 59);">  
  
</font><font style="color:rgb(35, 38, 59);">8.最终效果展示：  
</font><font style="color:rgb(35, 38, 59);">捕鱼：  
</font>![](https://cdn.nlark.com/yuque/0/2024/gif/43256946/1731053158599-1d88b870-f9b6-4348-a1ff-1ee9de940455.gif)<font style="color:rgb(35, 38, 59);">  
</font><font style="color:rgb(35, 38, 59);">口袋48：</font>

![](https://cdn.nlark.com/yuque/0/2024/gif/43256946/1731053065939-65da9888-07e4-4a11-b198-b40bbb081492.gif)

