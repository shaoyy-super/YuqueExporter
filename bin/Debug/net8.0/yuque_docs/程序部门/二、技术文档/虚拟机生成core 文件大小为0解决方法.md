1. 原因

vagrant 共享目录是window 文件夹,与linux操作系统存在差异导致

2. 解决方式,定位产生core文件的路径为linux 系统路径(比如 /data/)

> sudo bash -c "echo /data/core.%e > /proc/sys/kernel/core_pattern"
>



:::tips
vagrant@bullseye:/data$ ulimit -c unlimited

bash: ulimit: core file size: cannot modify limit: Operation not permitted

:::



**用root用户修改core 上限**

vi /etc/security/limits.conf

> vagrant          hard    core            unlimited
>
> vagrant          soft     core            unlimited
>

**重启虚拟机**

