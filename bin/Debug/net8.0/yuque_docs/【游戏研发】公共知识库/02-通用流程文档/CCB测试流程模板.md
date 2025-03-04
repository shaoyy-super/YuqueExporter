以下测试准备信息需在开测前1周全部确认完毕

## 测试信息
测试性质：删档测试

测试渠道：

测试开始：

测试结束：

出包日期：

## 发行对接
1.确认发行对接人：

2.创建发行对接群：

3.明确发行素材需求：

+ ICON
+ 宣传图
+ 宣传素材
+ 游戏说明
+ 游戏攻略

## 研发流程变更
一旦进入版本构建、版本发布阶段，TB和Git都需要采用不一样的流程

版本封版后进行如下变更：

[thoughts 文档](https://thoughts.teambition.com/workspaces/65fa97cacf0c850018df9956/docs/65fbcdf055ffae000187acdb)

Release分支地址：

每日BUG审核人：

每日需求审核人：

## 测试前准备工作：
上线前创建一份流程确认文档，逐条确认准备工作

### 1.版本准备
版本内容：

+ 提前一周准备好冒烟版本
+ 提前两天完成版本整包测试

SDK准备：

+ 渠道sdk参数接入@程序@PM
+ 渠道sdk测试@QA

版本功能：

+ 充值测试
+ 强更测试
+ 重载测试

礼包码准备：

+ 礼包码制作@策划
+ 礼包码测试@QA

### 2.后台准备
+ 确认数数后台准备完毕
+ 确认GM工具后台准备完毕
+ 运维mog后台功能验收测试（公告、跑马灯、发送邮件）
+ 开服公告

### 3.运维准备
+ 测试服环境搭建@运维
+ 测试服环境测试@QA
+ 清理数据库、数数的接入数据

### 4.上线准备
+ 正式服环境搭建@运维
+ 正式服环境测试 @运维
+ 开服检查服务器、检查数据库@运维
+ 运维确认录像服、cross服配置无误
+ 运维开启log输出方式
+ 运维开启监控
+ 开服检查@运维@QA
+ 渠道正式开服@运维

### 5.**对外交流**
发行对接群：

客服QQ群：

玩家群：

QA、策划加入发行对接群

QA、策划加入客服QQ群

开发人员加入玩家群

## 明确责任人（PM、策划、QA）
+ 渠道安装包交付、渠道对接责任人
    - PM
+ 测试服、正式服、提审服搭建责任人
    - 运维/策划/后端
+ 测试服、正式服、提审服测试责任人
    - QA
+ 开服检查责任人
    - 运维、QA
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

## 应急处理
### 1.紧急bug处理：
1. 通知CT组，确认责任人并立刻处理，初步约定修复时间并通知对接策划
2. 问题修复人员创建hotfix紧急分支并修复
3. 疑问解答策划群内告知运营并通知处理进展
4. QA在hotfix验收后，合并并发布补丁

### 2.维护处理：
1. 策划提前准备好跑马灯内容并转交运营
2. 策划提前准备好维护公告内容并转交运营
3. 群内通知关服维护，确认没问题后进行维护
4. QA验收通过后开服
5. 策划提前准备好补偿邮件并转交运营

## 测试时的工作（策划、QA）：
### 1.bug收集（QA）
1. 内网开发bug收集：收集开发人员群内反馈的bug，每日整理文档，并创建tb单
2. 运营反馈bug收集：收集运营/客服反馈的bug，每日整理文档，内部确认后创建tb单
3. 外网qq群bug收集：收集玩家qq群反馈的bug，每日整理文档，内部确认后创建tb单
4. 以**1天**为单位，QA、策划双方确认bug，标注优先级，创建bug确认修复时间
5. 优先级**1，2**的bug,需当天修复并期望合入更新外网
6. 每日根据bug表整理质量报告，总结问题，召开CT组质量确认会

### **2.版本跟踪**（策划、PM、QA）
1. 版本内容处理：确认需合入的内容并明确版本更新时间@周鹏@PM
2. 版本合并构建：内容合并、内容测试、版本构建@QA
3. 版本发布单：记录发布单合入相关信息，整理版本更新清单@PM/QA
4. 线上重要问题跟踪：记录线上重要问题并跟踪具体原因@PM

### 3.体验优化收集（策划）
1. 每日体验汇总
    1. 以**天**为单位，Thoughts汇总所有优化建议
    2. 当天策划组内全员会议，确定优先级，分配任务，形成Excel任务表格
    3. 创建任务排期开发，排期时间需同步体现在Excel表内
2. 测试体验总结
    1. **测试结束**后，Excel表格形式汇总所有优化建议
    2. 策划组内全员会议，讨论优化内容，确定优先级，分配任务，形成Excel任务表格
    3. 创建任务排期开发，排期时间需同步体现在Excel表内

## 测试后的工作（全员）：
### **1.测试结束**
关服时间：

策划整理玩家、运营反馈数据

统计所有测试时间内服务器报错情况

### 2.明确计划
1. 明确零碎bug修复计划
2. 明确优化排期计划
3. 明确后续版本排期计划

### 2.测试总结
1. 策划总结
2. 版本质量总结

### 3.版本总结
版本时间、版本内容、版本质量、版本问题总结

相关表格：

[CCB1BUG优化反馈.xlsx](https://tcs.teambition.net/storage/3234c3b13f4e2e333c652160ef501befa5c6?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxMzMyMTQ0MCwiaWF0IjoxNzEyNzE2NjQwLCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMyMzRjM2IxM2Y0ZTJlMzMzYzY1MjE2MGVmNTAxYmVmYTVjNiJ9.FH6xJkYqcz1wbnYMSl3y6Lh_CEZHENlMIREpwmWo5-Y&download=CCB1BUG%E4%BC%98%E5%8C%96%E5%8F%8D%E9%A6%88.xlsx)

[CCB1渠道反馈表.xlsx](https://tcs.teambition.net/storage/3234229880f43e7ff4b26eff3fb98589b955?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxMzMyMTQ0MCwiaWF0IjoxNzEyNzE2NjQwLCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMyMzQyMjk4ODBmNDNlN2ZmNGIyNmVmZjNmYjk4NTg5Yjk1NSJ9.bBUrS5T2sqV0xltQlZKmfXd0ibgufdUE4ULndt0H9Ks&download=CCB1%E6%B8%A0%E9%81%93%E5%8F%8D%E9%A6%88%E8%A1%A8.xlsx)

