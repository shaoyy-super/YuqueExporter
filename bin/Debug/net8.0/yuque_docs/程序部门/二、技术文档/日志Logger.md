为了规范日志使用，每个功能模块需要获取自己的logger对象用于日志输出，并且提供模块名

服务器跟客户端的日志有一定区别，下面这组接口仅用于客户端（Presentation下）

## 1、获取自己模块的logger对象
```lua
-- 接口定义
function Debug.GetLogger(name)

-- 使用示例
local Log = Debug.GetLogger("Battle")
```



## 2、输出日志
### 2.1 日志级别定义
1. Info：信息输出级别，用来打印业务的主要流程【无堆栈】
2. Warn：警告级别，出现了预期外或者不推荐出现的状况，但是这个状况不会造成严重的不良后果，同时这种状况需要被注意到【无堆栈】
3. Error：错误级别，出现了严重的错误，可能导致后续卡死或者流程错误【有堆栈】
4. Debug：调试信息输出，这一级别的日志应该尽可能详细，提供足够多的信息，让程序可以通过日志定位问题。【有堆栈】



特别说明：

1、Info、Warn两个级别的日志不带Lua堆栈，要求输出的日志本身应带有必要的信息，不要依赖堆栈查问题

2、Debug的日志在Editor下默认开启，非Editor下默认关闭（在需要流程调试的时候特殊开启）

<font style="color:#DF2A3F;">3、禁止自行拼接Log的字符串，动态信息统一使用XxxFormat接口处理。也不要自己使用string.format。</font>



### 2.2 日志接口&示例
```lua
-- 接口定义
function Logger:Info(msg)
function Logger:InfoFormat(msg, ...)

function Logger:Warn(msg)
function Logger:WarnFormat(msg, ...)

function Logger:Error(msg, trace_lv)
function Logger:ErrorFormat(msg, ...)


-- 使用示例
Log:Info('战斗开始，加载角色')
Log:InfoFormat('战斗结束，force=%s胜利', 1)

Log:Warn('敌方阵营没有单位，我方直接胜利')
Log:WarnFormat('单位force=%d, battle_id=%d已经死亡，却仍然被访问', 1, 8)

Log:Error('cmd解析出错，战斗流程无法继续')
Log:ErrorFormat('找不到buff配置，id=%s, lv=%s', 10101, 3)
```

![](https://cdn.nlark.com/yuque/0/2024/png/43256857/1713868295666-408400ad-e532-4972-a3ec-3aacaa69a498.png)



```lua
-- 接口定义
function Logger:Debug(msg, trace_lv)
function Logger:DebugFormat(msg, ...)
-- 会对参数中的table进行序列化转换
function Logger:DebugSerialize(msg, ...)

-- 使用示例
Log:DebugFormat('技能释放,[%s]对[%s]，释放[%s]', 1, 8, 10203)
Log:DebugSerialize('BattlePlayer处理cmd：%s', { 'play_tml', 'TML_O_SKILL_01', 1, 2 })

-- 开启Debug输出，开启后Debug的信息才会输出
Log.level = Log.Level.Debug
```

![](https://cdn.nlark.com/yuque/0/2024/png/43256857/1713868318744-9da2f340-ffa0-4bfa-b4cc-6cabac92873d.png)

