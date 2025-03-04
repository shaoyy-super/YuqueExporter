## 1、工具启动后卡死（大概几秒的时间）
可能是本地开着代理/VPN导致软件内部的网络请求出错

**<font style="color:#DF2A3F;">解决：关掉代理/VPN</font>**

**<font style="color:#DF2A3F;"></font>**

## **<font style="color:#000000;">2、工具启动后在某一步操作后卡死</font>**
<font style="color:#000000;">猜测有发包验证, 破解版验证时未通过. 可以在hosts里直接把指向ip转移.</font>

**<font style="color:#DF2A3F;">解决：在C:\Windows\System32\drivers\etc 路径下的hosts文件内加入以下2行.</font>**

**<font style="color:#DF2A3F;">221.133.234.222 mailak</font>**

**<font style="color:#DF2A3F;">192.168.2.248 mailak</font>**

**<font style="color:#DF2A3F;">该文件可能有保护, 可以复制出来修改后再粘贴回去替换掉.</font>**









