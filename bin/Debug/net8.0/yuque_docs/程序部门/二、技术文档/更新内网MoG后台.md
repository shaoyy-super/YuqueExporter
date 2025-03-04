### 以口袋项目举例
1. 拉取MoG项目，mog-master.tar.gz。
2. 传入MoG后台所在的服务器，解压出 mog-master
3. 执行拷贝 \cp -rf mog-master/* mog/拷贝进运行目录
4. 执行更新脚本。
+ php artisan migrate --force
+ php artisan db:seed --force

创建管理员用户

php artisan admin:create-user

配置生效

php artisan <u>config:cache</u>

导出配置

php artisan admin:export-seed

清除路由缓存

<font style="color:rgb(79, 79, 79);background-color:rgb(238, 240, 244);">php artisan route:clear</font>

