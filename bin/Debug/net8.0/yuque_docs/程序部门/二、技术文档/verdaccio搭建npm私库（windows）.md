#### ** Node.js的安装(已安装完可以跳过该步骤)**
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822515324-5a38f0bd-991e-490a-a87c-ace9daa62fb1.png)

(进入官网 [https://nodejs.org/en/download](https://nodejs.org/en/download) 选择相应的版本下载)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822537147-ab09326d-6c8f-4f2e-b08e-3913657b1122.png)

(一路next直到这里, 勾上该选项)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822552356-4a475393-077c-43b7-9a6f-603da1f25baf.png)

(按流程一路安装下去, 中间需要下载安装python之类的, 可能需要等一会)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822557915-bd162432-2e6b-43a9-9a2c-867920e8118b.png)

(终端输入 node –version 显示版本号信息则为安装成功)

#### ** Verdaccio的安装**
![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822590059-fbbc76cb-21a0-425a-bb26-b5b2813e3e1d.png)

(同样在终端上执行npm install verdaccio –g 命令, 等待下载完成(我是卸载了之后安装的, 安装完成时的提示可能会不一样, 这个不影响)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822623196-2aa28fca-718b-4fc1-a447-96b292d636b5.png)

(安装完成后输入 verdaccio 命令运行)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822650404-895de334-ca77-427c-98a9-93d90abc7f81.png)

(修改该位置 (C:\Users\jialiang_li\AppData\Roaming\verdaccio, 大概这个路径, 具体根据电脑修改) 下的config.yaml )

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822664139-9b6cfb56-279e-484b-97fc-d7bcd2abee34.png)

(在末尾加入listen: 0.0.0.0:4873)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822673207-feff78fb-8f81-4372-985a-37c90b5214ea.png)

(在终端输入 ipconfig , 获得ipv4地址, 比如我的是192.168.5.148)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822681561-b1096f01-d851-4419-b98b-0ccfd27929c7.png)

(转回浏览器, 输入http://ipv4地址:4873/ , 比如我的是[http://192.168.5.148:4873/](http://192.168.5.148:4873/)  , 至此Verdaccio安装与配置流程完成)

#### **3.   使用pm2进行进程守护, 避免关掉窗口之后Verdaccio进程自动关闭.**


附:pm2常用指令:

pm2 start app.js          # 启动 app.js

pm2 stop app.js         # 停止 <app_name|namespace|id|'all'|json_conf>

pm2 restart app.js        # 重启 <app_name|namespace|id|'all'|json_conf>

pm2 delete app.js         # 删除 <app_name|namespace|id|'all'|json_conf>

pm2 reload app.js         # 重新加载 app.js

pm2 kill              # 杀死所有进程

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822721112-0bab5e66-4a38-40d4-9b17-f25dbe11abe1.png)

(先使用ctrl+c退出Verdaccio进程)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822729838-04bb3178-caf6-494e-83b4-3e21b26a96be.png)

(使用npm install pm2 –g 指令下载pm2)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822738111-50ee6c58-60ac-4ae1-aea4-47ab5b12abbb.png)

(使用pm2 start verdaccio 指令启动Verdaccio. 这里踩了个坑, 如果出现图上的情况实际上启动失败了, 猜测是启动脚本没有正确的找到进程位置)

![](https://cdn.nlark.com/yuque/0/2024/png/43288772/1712822747537-787e5a36-88f5-4685-8246-dc54f97ba81e.png)

(使用pm2 start c:\users\jialiang_li\AppData\Roaming\npm\node_modules\verdaccio\bin\verdaccio 确定启动位置直接启动. 具体地址每台电脑都会不一样, 但大体路径一致. 显示online即为启动成功. 后续关掉终端不会关闭Verdaccio服务)





