1.此功能需要nginx有ngx_http_geoip_module模块，默认安装没有该模块，需要额外安装并导入

#配置官方nginx源[nginx-stable]

name=nginx stable repo

baseurl=http://nginx.org/packages/centos/$releasever/$basearch/

gpgcheck=0

enabled=1

gpgkey=https://nginx.org/keys/nginx_signing.key

module_hotfixes=true#安装模块yum install nginx-module-geoip#注：新增模块需要重启nginx（否则不生效）

2.下载IP数据库

参考链接：[<u>https://www.miyuru.lk/geoiplegacy</u>](https://www.miyuru.lk/geoiplegacy)（第三方下载库）

![](https://cdn.nlark.com/yuque/0/2024/png/43288467/1713176632608-8158390c-83b9-4e1c-9667-3e8db851558c.png)

选择所需的库进行下载

解压文件到nginx配置目录

gun zip maxmind.dat.gz

3.创建白名单文件

#在白名单文件中添加ip地址vim /etc/nginx/ip-white.confIP 1;  #IP--改为具体ip地址

4.配置nginx

#全局配置中导入geo模块load_module /usr/lib64/nginx/modules/ngx_http_geoip_module.so;...#在http段配置    geoip_country /etc/nginx/GeoCountry.dat; #导入IP库    geo $ip_whitelist {

        default 0;

        include ip-white.conf;

    }...#在server段配置server {...      set $flag "allow"; #通过设置标记，控制黑白名单        if ($geoip_country_code ~ CN|JP) {

            set $flag "deny";

        }

        if ($ip_whitelist = 1) {

            set $flag "allow";

        }

        if ($flag = "deny") {

            return 403;

        }}

