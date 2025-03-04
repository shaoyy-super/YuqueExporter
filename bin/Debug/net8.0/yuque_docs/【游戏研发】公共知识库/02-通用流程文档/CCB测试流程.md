## 测试信息
测试性质：删档测试

测试渠道：盟友

测试开始：**2021-05-10**10点

测试结束：2021-05-1618点

出包日期：2021-05-07前完成渠道包打包

## 发行对接
1.确认发行对接人：

2.创建发行对接群：

3.明确发行素材需求：

+ ICON
+ 宣传图
+ 宣传素材
+ 游戏说明
+ 游戏攻略

## 研发流程
### 1.TB版本流程
 1.封版后创建“CCB版本发布“任务分组 

2.所有需要进版本的内容需要走合并流程，将任务创建到发布任务分组

3.版本正式发布后关闭旧版本分组，所有未完成任务移动至新版本分组内

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712716997067-3d9adc97-81b0-48d3-943f-924d3a177f48.png)

### 2.分支流程
1.封版后，新建release测试分支：用于内容合并、bug验收

2.开发人员创建自己分支，修改后由qa验收修复后程序自行合并到release分支，并同步合并到develop分支

3.完成正式包完整测试后，同步最新release创建master出包分支：用于对外版本发布

4.每天处理的需求、bug由开发修复后先放在自己的分支，策划确认合并内容后合并release与 develop

目前前期阶段：只合release，等到版本稳定再同步develop

### 3.需求流程
 1.由策划每日在“CCB版本发布”分组创建需求任务，确定任务优先级

2.需求任务需经主策划确认

3.开发人员完成后自行合并release

### 4.BUG流程
 1.QA在“CCB版本发布”分组创建版本bug，确定bug优先级，（目前阶段先放在此版本，等待0.3版本发布出去后，此版本关闭，再执行此条）

2.QALeader确认bug是否需要修复，无需修复的bug移回版本任务分组

## 测试前准备工作：
上线前创建一份流程确认文档，逐条确认准备工作

### **1.内部准备**
+ 正式发布Jenkins环境搭建@程序
+ 渠道sdk参数接入@程序@PM
+ 渠道sdk测试@QA
+ 提前两天完成整包测试@QA5.7
+ 礼包码制作@策划（需先确认渠道是否有礼包码的需求）
+ 礼包码测试@QA
+ 测试服环境搭建@程序
+ 测试服环境测试@QA

### 2.提审准备
+ 提审服环境搭建@运维
+ 提审服环境测试@QA
+ 渠道包提审@QA

### 3.上线准备
+ 正式服环境搭建@运维
+ 正式服环境测试 @运维
+ GM工具部署 @运维
+ 后台部署 @运维
+ 开服公告@运维@策划周鹏
+ 开服检查服务器、检查数据库@运维
+ 开服检查@运维@QA
+ 渠道正式开服@运维

### 4.**对外交流**
发行对接群：

客服QQ群：

玩家群：

QA、策划加入发行对接群

QA、策划加入客服QQ群

开发人员加入玩家群

### 5.明确责任人（PM、策划、QA）
+ 渠道安装包交付、渠道对接责任人
    - PM
+ 测试服、正式服、提审服搭建责任人
    - 运维/策划/后端
+ 测试服、正式服、提审服测试责任人
    - QA
+ 开服检查责任人
    - QA
+ BUG收集责任人
    - QA
+ 完整版本测试责任人
    - QA
+ 线上问题修复迭代责任人
    - QA
+ 优化反馈收集责任人
    - 对接群意见收集：策划/QA
    - 玩家群意见收集：策划
+ 疑问解答责任人
    - 策划

## 测试时的工作（策划、QA）：
### 1.bug收集（QA）
1. 内网开发bug收集：收集开发人员群内反馈的bug，每日整理文档，并创建tb单
2. 运营反馈bug收集：收集运营/客服反馈的bug，每日整理文档，内部确认后创建tb单
3. 外网qq群bug收集：收集玩家qq群反馈的bug，每日整理文档，内部确认后创建tb单
4. 以**1天**为单位，QA、策划双方确认bug，标注优先级，创建bug确认修复时间
5. 优先级**1，2**的bug,需当天修复并期望合入更新外网

### **2.版本跟踪**（策划、PM、QA）
1. 版本内容处理：优化内容、修复bug、确认期望合入的内容@策划
2. 版本更新时间：处理需修复、需合入截至时间、确认版本更新时间@策划/PM
3. 版本合并构建：内容合并、内容测试、版本构建@QA
4. 版本发布单：记录发布单合入相关信息，整理版本更新清单@PM/QA
5. 线上重要问题跟踪：记录线上重要问题并跟踪具体原因@PM
6. 版本总结：涉及版本时间、版本内容、版本质量、版本问题总结@PM

### 3.体验优化收集（策划）
1. 比较严重的体验问题
    1. 以**天**为单位，Excel表格形式汇总所有优化建议
    2. 当天策划组内全员会议，确定优先级，分配任务
    3. 创建任务排期开发修改，排期时间、更新时间需同步体现在Excel表内
2. 可优化的体验问题
    1. **测试结束**后，Excel表格形式汇总所有优化建议
    2. 策划组内全员会议，讨论优化内容，确定优先级，分配任务
    3. 创建任务排期开发，排期时间需同步体现在Excel表内

## 测试后的工作（全员）：
### **1.测试结束**
整理反馈数据

统计记录保存玩家关键信息

关服时间：2021-05-16 18:00

### 1.明确计划
1. 明确零碎bug修复计划
2. 明确优化排期计划

### 2.测试总结
1. 策划总结
2. 测试总结
3. 排期及版本质量总结

相关表格：

[POK220210510CCB1BUG优化反馈.xlsx](https://tcs.teambition.net/storage/3224be63a4d11094bcda84121ca5768bcca8?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxMTYwNjY1OCwiaWF0IjoxNzExMDAxODU4LCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMyMjRiZTYzYTRkMTEwOTRiY2RhODQxMjFjYTU3NjhiY2NhOCJ9.ec5y7sQgYURfD4q58CZSWq8Jh2tZY0ZwcuqzejVK9sc&download=POK220210510CCB1BUG%E4%BC%98%E5%8C%96%E5%8F%8D%E9%A6%88.xlsx)

[pok2CCB1渠道反馈表.xlsx](https://tcs.teambition.net/storage/3224d0be7f0203e5b548a77dade4fbb2a142?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxMTYwNjY1OCwiaWF0IjoxNzExMDAxODU4LCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMyMjRkMGJlN2YwMjAzZTViNTQ4YTc3ZGFkZTRmYmIyYTE0MiJ9.yoV_Rut0IAI1hL4vwnjnTOWd32NT0qDSCDnC970hSwg&download=pok2CCB1%E6%B8%A0%E9%81%93%E5%8F%8D%E9%A6%88%E8%A1%A8.xlsx)

