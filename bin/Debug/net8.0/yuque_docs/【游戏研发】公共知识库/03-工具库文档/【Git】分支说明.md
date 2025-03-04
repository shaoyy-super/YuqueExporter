GitFlow工作流定义了一个围绕项目发布的严格分支模型，是一种Git使用方法论，它为**不同的分支分配了明确的角色**，并定义分支之间何时以及如何进行交互。

# 分支介绍
## 长期分支
### Master
master分支存储了正式发布的历史，分支上的产品要求随时处于**可部署状态**。master分支只能通过与其他分支合并来更新内容，禁止直接在master分支进行修改。

### Develop
develop分支作为功能的集成分支，分支上的产品可以是缺失功能模块的半成品，但是已有的功能模块不能是半成品。develop分支只能通过与其他分支合并来更新内容，禁止直接在develop分支进行修改。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457556281-5ae042a9-2aa9-4117-9acf-60d73ff9539b.png)

## 短期分支
### Feature
每个新功能位于一个自己的分支，这样可以push到中央仓库以备份和协作。 但功能分支不是从master分支上拉出新分支，而是使用develop分支作为父分支。当新功能完成时，合并回develop分支。 新功能提交应该从不直接与master分支交互。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457556695-7a26b29c-df17-4947-b74a-b73e04c0e444.png)

### Release
**一旦develop分支上有了做一次发布（或者说快到了既定的发布日）的足够功能，就从develop分支上fork一个发布分支。 新建**的分支用于开始发布循环，所以从这个时间点开始之后新的功能不能再加到这个分支上—— 这个分支只应该做Bug修复、文档生成和其它面向发布任务。 一旦对外发布的工作都完成了，发布分支合并到master分支并分配一个版本号打好Tag。 另外，这些从新建发布分支以来的做的修改要合并回develop分支。

使用一个用于发布准备的专门分支，使得_一个团队可以在完善当前的同时发布版本的，另一个团队可以继续开发下个版本的功能。 这也打造定义良好的开发阶段（比_如，可以很轻松地说，『这周我们要做准备发布版本4.0』，并且在仓库的目录结构中可以实际看到）。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457557086-865ee04a-805b-42ec-a722-8ba916cbb64e.png)

### Hotfix
Hotfix分支用于生成快速给产品发布版本（production releases）打补丁，这是唯一可以直接从master分支fork出来的分支。 修复0000完成，修改应该马上合并回master分支和develop分支（当前的发布分支），master分支应该用新的版本号打好Tag。

为Bug修复使用专门分支，让团队可以处理掉问题而不用打断其它工作或是等待下一个发布循环。 你可以把维护分支想成是一个直接在master分支上处理的临时发布。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457557461-1de8df10-452b-4689-9197-231d40b83389.png)

# 代码Review
在Gitflow工作流中使用Pull Request让开发者在Develop分支或是Hotfix分支上工作时， 可以有个方便的地方对关于发布分支或是维护分支的问题进行交流。

当一个功能、发布或是热修复分支需要Review时，开发者简单发起一个Pull Request， 团队的其它成员会通过Bitbucket收到通知。

新功能一般合并到develop分支，而发布和热修复则要同时合并到develop分支和master分支上。 Pull Request可能用做所有合并的正式管理。

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457557811-d70ac976-ca1e-4c13-b2e1-83ee338daefd.png)

# GitFlow使用
![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712457558292-b1a963bb-73a4-41e2-b984-37485b1b5854.png)

# 初始化git flow项目，会创建master和develop两分支

git flow init



# 开始新Feature:

git flow feature start FeatureName



# publish一个Feature(也就是push到远程)

git flow feature publish FeatureName



# 获取publish的Feature

git flow feature pull origin FeatureName



# 完成一个Feature

git flow feature finish FeatureName



# 开始一个Release

git flow release start ReleaseName [BASE]



# publish一个Release

git flow release publish ReleaseName



# 发布Release

git flow release finish ReleaseName



# 开始一个Hotfix

git flow hotfix start VERSION [BASENAME]



# 发布一个Hotfix

git flow hotfix finish VERSION

