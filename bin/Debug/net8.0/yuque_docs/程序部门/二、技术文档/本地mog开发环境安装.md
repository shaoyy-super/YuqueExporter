1. 下载php集成环境

[https://www.phpenv.cn/download.html](https://www.phpenv.cn/download.html)

1. 点击 **启动服务**
2. 点击 **命令行终端**, 进入到 **mog-master** 文件夹
3. # 安装依赖
4. $ **composer install**
5. # 拷贝.env.example到.env并配置

$ **copy .env.example .env**修改.env中 

DB_DATABASE 为mog 

DB_USERNAME 为root, 

密码为123456

1. # 生成KEY

$ **php artisan key:generate**

1. # 拷贝资源

$ **php artisan aetherupload:publish**

1. # 创建数据库

Mysql –h127.0.0.1 –uroot –p123456

$ MySql> **CREATE DATABASE IF NOT EXISTS mog default charset utf8 COLLATE utf8_general_ci;**

1. # 数据库安装

**php artisan migrate**

**php artisan db:seed**

**php artisan storage:link**

1. **php artisan admin:create-user 创建用户**
2. **php artisan serve** 开启服务
3. 将.env中GAME_URL 改为自己本地gm服务器的端口[http://localhost:8002/gm](http://localhost:8002/gm)
4. Mog/config/game.php中GAME_URL 改为本地gm服务器的端口
5. Vagrantfile中将虚拟机接口映射出来
6. 访问网址[http://127.0.0.1:8000/admin](http://127.0.0.1:8000/admin)
7. 开发内容: GmCmd_W2S.lua
8. GM后台的log在mog/storage/logs中



##############################################################################

# linux下环境安装部署:
1. 拷贝mog到 /data/mog
2. cd /data/mog/ root用户执行 sh init.sh -h 127.0.0.1 -u root -p 123456 -d mog -n 9095 -l /data/fish
3. vagrant 启动映射 9090端口 

  config.vm.network "forwarded_port", guest: 9090, host: 9090

4. [http://127.0.0.1:9090/admin](http://127.0.0.1:9090/admin) 默认用户名密码 admin/Longame123



初始化脚本说明:

sh init.sh -h host -u user -p password -d db_name -n ngnix_port -l link_dir

host:		mysql 地址

user:		mysql 用户

password:	mysql 密码

db_name:	mog数据库名

nginx_port:	使用端口号

link_dir:		目录服group所在的文件夹

示例: sh init.sh -h 127.0.0.1 -u root -p 123456 -d mog -n 9095 -l /data/fish





