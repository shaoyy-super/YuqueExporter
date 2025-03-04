删档测试日期：**2021-05-10**10点开测

测试渠道：盟友

出包提审：2021-05-07前完成渠道包打包

测试开始：2021-05-10日10点

测试结束：2021-05-16日18点

## 对外CE测试上线前准备工作（PM）：
上线前创建一份流程确认文档，逐条确认准备工作

### **1.内部准备**（客户端、QA）
+ 提前两天QA内部整包测试通过
+ 新建出debug测试分支
+ 新建publish出包分支

### 2.上线准备（PM、QA、策划、服务器、客户端、运维）
+ 新建渠道对接群
+ 渠道sdk参数接入
+ 渠道sdk测试
+ 礼包码制作
+ 礼包码测试
+ 安装包交付安卓（接入sdk）
+ 测试服环境搭建
+ 测试服环境测试
+ 正式服环境搭建
+ 正式服环境测试
+ 提审服环境搭建
+ 提审服环境测试
+ 渠道包提审
+ gm工具部署
+ 后台部署
+ 开服公告
+ 开服检查服务器、检查数据库
+ 开服检查
+ 渠道正式开服

### 3.**对外交流**
QA、策划加入渠道对接群

QA、策划加入客服qq群

### 4.明确责任人（PM、策划、QA）
+ 渠道安装包交付、渠道对接责任人 责任人：PM
+ 测试服、正式服、提审服搭建责任人 责任人：运维/策划/后端
+ 测试服、正式服、提审服测试责任人 责任人：QA
+ 开服检查责任人 责任人：QA
+ BUG收集责任人 责任人：QA
+ 完整版本测试责任人 责任人：QA
+ 线上问题修复迭代责任人 责任人：QA
+ 优化反馈收集责任人 责任人：策划/QA
+ 疑问解答责任人 责任人：策划

## 测试时的工作（策划、QA）：
### 1.bug收集（QA）
1. 内网开发bug收集：收集开发人员群内反馈的bug，每日整理文档，并创建tb单
2. 运营反馈bug收集：收集运营/客服反馈的bug，每日整理文档，内部确认后创建tb单
3. 外网qq群bug收集：收集玩家qq群反馈的bug，每日整理文档，内部确认后创建tb单
4. 以**1天**为单位，QA、策划双方确认bug，标注优先级，创建bug确认修复时间
5. 优先级**1，2**的bug,需当天修复并期望合入更新外网

### **2.版本跟踪**（策划、PM、QA）
版本内容处理：优化内容、修复bug、确认期望合入的内容 责任人：周鹏

版本更新时间：处理需修复、需合入截至时间、确认版本更新时间 责任人：周鹏/PM

版本合并构建：内容合并、内容测试、版本构建 责任人：QA

版本发布单：记录发布单合入相关信息，整理版本更新清单 责任人：PM/QA

线上重要问题跟踪：记录线上重要问题并跟踪具体原因 责任人：PM

版本总结：涉及版本时间、版本内容、版本质量、版本问题总结 责任人：PM

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

2021-05-16 18:00关服

### 1.明确计划
1. 明确零碎bug修复计划
2. 明确优化排期计划

### 2.测试总结
1. 策划总结
2. 测试总结
3. 排期及版本质量总结

相关表格：

[POK220210510CCB1BUG优化反馈.xlsx](https://tcs.teambition.net/storage/3224be63a4d11094bcda84121ca5768bcca8?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxMTYwNjY1MiwiaWF0IjoxNzExMDAxODUyLCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMyMjRiZTYzYTRkMTEwOTRiY2RhODQxMjFjYTU3NjhiY2NhOCJ9.ezpiu9RMQDhSA2WB7FzmaA0o26V22IGoixd0QUrz8jM&download=POK220210510CCB1BUG%E4%BC%98%E5%8C%96%E5%8F%8D%E9%A6%88.xlsx)

[pok2CCB1渠道反馈表.xlsx](https://tcs.teambition.net/storage/3224d0be7f0203e5b548a77dade4fbb2a142?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxMTYwNjY1MiwiaWF0IjoxNzExMDAxODUyLCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMyMjRkMGJlN2YwMjAzZTViNTQ4YTc3ZGFkZTRmYmIyYTE0MiJ9.cE4Z2s3zsS377qJp-HD5Tj7YUF7tCKPT6qMn4ETk3nY&download=pok2CCB1%E6%B8%A0%E9%81%93%E5%8F%8D%E9%A6%88%E8%A1%A8.xlsx)

