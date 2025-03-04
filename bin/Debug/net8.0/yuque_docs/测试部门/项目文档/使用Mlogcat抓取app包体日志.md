## 通过工具实时显示当前包体运行日志
Mlogcat:[mLogcat_1_2_4_0.7z](https://snh48group.yuque.com/attachments/yuque/0/2024/7z/43256946/1712735468063-12c2ccd6-a10b-42c6-9141-5e0b6cde3904.7z)

### 1.查找当前连接设备并右键弹出当前日志
在cmd页面输入adb devices查看是否与连接设备id一致

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712735561150-c7a308f5-2ba1-42a1-99f5-a899ab377a87.png)



### 2.选中导出日志右键：Refilter ltem
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712735653328-6a9c701e-452e-46e3-8112-c4d08442ddf3.png)



### 3.by Tag一栏填入Unity并点击OK，会根据日志的等级显示不同的颜色，INFO是蓝色，WARNING是黄色，ERROR是粉色
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712735680580-fd4bf3df-e5a4-4b13-a80e-29977b552029.png)



### 4.想要查看实时日志，需关闭掉暂停按钮（鼠标不能放在日志弹窗上），字母代表筛选出不同等级的日志，橡皮擦按钮是清除按钮
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712735760591-07664e49-2e06-4be1-b207-52b96e1d59e6.png)



### 5.最终效果演示
[mlocat scrcpy使用效果.mp4](https://snh48group.yuque.com/attachments/yuque/0/2024/mp4/43256946/1712735939252-4b3eddc3-8905-4a0b-9630-e2e9adc8b103.mp4)



### 6.日志导出并输出到本地
（1）ctrl+a选中目前筛选完的所有日志

（2）右键选中Export Items to Text

（3）选择保存目录和文件名

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712738652607-779f1847-98c1-428a-bb2f-55457f287a2d.png)

[mlogcat日志导出并输出到本地.mp4](https://snh48group.yuque.com/attachments/yuque/0/2024/mp4/43256946/1712738690429-ad58051a-cf87-4dfc-9593-1f64c44d6763.mp4)

