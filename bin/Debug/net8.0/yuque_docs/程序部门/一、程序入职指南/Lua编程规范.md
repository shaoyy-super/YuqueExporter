## 1、命名规范


+ 变量名&参数：snake-case命名法（小写+下划线）

```lua
local is_auto_battle = false
```

+ 其他均为Pascal命名法：单词首字母大写的驼峰格式

```lua
Battle.PetEnergyRestoreCnt = 5
```

包含范围：脚本名、类名、函数名、枚举名、常量名等

## 2、Log规范


【系统/模块名】+说明+参数

+ 普通打印 :一些关键信息和数据的打印

```lua
Debug.Log("【活动】七日数据:",table.serialize(data))
```

+ 警告打印 :不影响结果但是不规范

```lua
Debug.LogWarning("【活动】content缺少prefab",content.name)
```

+ 报错打印：策划配置表结构或者服务器返回数据不对

```lua
Debug.LogError("【登录】服务器验证失败",Code_id);
```



## 3、C#使用时机


c#不可热更，因此逻辑相关的不要在c#写，可以c#提供接口在lua这边调用

部分公用挂载组件可在c#端

