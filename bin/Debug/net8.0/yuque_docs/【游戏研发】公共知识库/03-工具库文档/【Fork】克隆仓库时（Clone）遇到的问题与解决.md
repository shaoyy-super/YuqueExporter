# 现象一：Permission denied（或者是Access denied）（访问被拒绝）
![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457566072-12694479-8f68-4b2d-afb3-ee76c218c2d5.png)

### 原因一：没有项目权限。
#### 解决方案：找人给你权限。（管理员或PM）
[@黄欣瑀](undefined/huangxinyu-1wuda)待补充，没补充前找黄欣瑀

### 原因二：git账号密码不正确。
#### 解决方案：
[【Fork】【必读】文件提交_拉取_获取](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/rk0b5vum1ygrc4g5)

# 现象二：跳出下图所示的窗口让你输入密码
![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457566755-b2508db0-9959-4ab5-9364-625dc857601f.png)

### 原因一：git账号的密码不正确。
#### 解决方案：填入正确密码。（有点废话的意思了）
### 原因二：安装fork后首次克隆时绑定的sshkey丢失了
#### 解决方案：打开ssh管理窗口
![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457567277-c16c33d2-a198-413f-9394-2b0524025851.png)![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457567846-dc5e6c7e-504f-4a56-a9ea-256a99abfbc3.png)

**可以看到没有选择sshkey，此时勾选你自己的sshkey即可。**

如果这里没有sshkey，请移步Frok安装第二步

[步骤2：Fork安装](https://snh48group.yuque.com/org-wiki-snh48group-ec9yge/rgqlf2/zdlly6bsrntqicxv#j7YaP)



# 现象三：弹框提示GitError
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1717732590345-2a1ff6d8-6cb4-49c5-ac0e-f04414b94ad8.png)

解决方案一：以管理员身份运行cmd  
执行命令：

```plain
git config --system core.longpaths true
```

然后重新Clone



如果上述方案不行，尝试直接在命令行窗口执行 git clone 项目git地址

