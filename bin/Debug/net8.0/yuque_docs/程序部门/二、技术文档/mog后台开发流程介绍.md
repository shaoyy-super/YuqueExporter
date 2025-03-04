官方文档:

[<u>https://laravel-admin.org/docs/zh/2.x/content-message</u>](https://laravel-admin.org/docs/zh/2.x/content-message)

## 添加界面显示
### 1.**添加所需数据库使用表格(如果需要)**
![](https://cdn.nlark.com/yuque/0/2024/png/43288467/1712657405305-726cc618-9551-4cba-945d-6455f4ae1c58.png)

如果需要单独的审批如发送邮件审批,玩家数据操作审批等,则需创建数据库表保留操作记录

生成对应数据库php文件, 会生成在 **database/migrations** 文件夹中

php artisan make:migration create_remove_items_table

在 生成的文件中create方法内编写数据表结构使用指令生成数据库表

php artisan migrate

### 2.**添加对应模型**
![](https://cdn.nlark.com/yuque/0/2024/png/43288467/1712657405664-0ff9564f-f4aa-4d23-bab1-b5fce14fac76.png)

php artisan make:model App\Models\Player\RemoveItem

模型与数据库表显示关联

protected $table = 'remove_items';

### 3.**创建模型对应控制文件(路由器文件)**
![](https://cdn.nlark.com/yuque/0/2024/png/43288467/1712657406001-74e25773-10a0-4dda-85cf-164c2e4b38e7.png)

php artisan admin:make App\Http\Controllers\Player\RemoveItemController --model=App\Models\Player\RemoveItem

**注意:如果model文件中有向服务器发送的请求,需要声明静态变量,servercmd,说明该消息是发送到哪个服务器,10002是发送到gm服务器,10003是发送到指定session服务器(需要指定zoneid)**

protected static $server_cmd = 10003; // 发送到session获取当前已选中区服: 'ZoneId' => Game::getZone()

### 4.**添加路由配置**
在laravel-admin的路由配置文件app/Admin/routes.php里添加一行：

$router->resource('player/remove_items', Player\RemoveItemController::class);

其中resource的前半部分即为指定的网页链接地址(

[http://127.0.0.1:8000/admin/player/remove_items),](http://127.0.0.1:8000/admin/player/remove_items),) 后半部分为刚添加的路由文件

### 5.**添加左侧菜单栏链接**
打开http://localhost:8000/admin/auth/menu，(或者左侧点击管理员/菜单)添加对应的menu

链接中admin之后的部分即为上一步指定的地址,如player/remove_items

使用命令php artisan admin:export-seed导出权限和菜单,git上提交

PS:保存之后,可能需要手动输入访问一次这个地址才会正确刷新出来

### 6.**页面详情**
![](https://cdn.nlark.com/yuque/0/2024/png/43288467/1712657406392-02562f35-2a00-404d-bf62-165ec2b5a2c3.png)

默认页面是一个模型表格(grid),里面的操作进行行操作时再到model进行请求,所以在页面创建之后会显示grid自带的各种控件,需自己手动在controller中控制,详见文档

**所有的页面操作和布局在controller中编写,所有的与数据产生交互的地方都在model中**

### 7.**页面内容编辑 controller**
对应的controller文件中编写页面内容

index() 标题显示

grid() 控制主体显示

内容筛选 参照文档: [https://laravel-admin.org/docs/zh/model-grid-filters](https://laravel-admin.org/docs/zh/model-grid-filters)

$grid->filter(function($filter)action控制行数据的操作$grid->actions(function ($actions){)}detail() 查看详情,对应action中的view, 隐藏:$actions->disableView();

update() 可调用model中的接口进行请求,参照各模块代码

列内容显示,前面就是对应的数据字段名,括号内为列头显示内容,没有本地化转义时就直接显示

$grid->itemid(trans('game.info.itemid'));

### 8.权限控制
1. 在管理员/权限中 添加对应的权限控制,标识为代码中所有标识
2. 在管理员/角色中 添加各角色对应权限
3. 代码中的应用 Admin::user()->can('mail.approval')
4. 使用命令php artisan admin:export-seed导出权限和菜单,git上提交

### 9.调试方法
引入头文件 use Illuminate\Support\Facades\Log;直接调用方法: Log::info($error); Log::debug($error);

**storage/logs** 中可以查看打印log

重要方法:弹框提示:

admin_error("title", "messages");

