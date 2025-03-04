记录一些Git的问题的解决方案

## 1、推送本地版本库到GitLab的新仓库（带提交历史）
1）在GitLab上新建仓库并进行初始

2）本地命令行执行操作

```git
# 在本地仓库添加新的远程仓库（可以先将之前的远程仓库删掉）
git remote add origin http://192.168.2.240/programmer/clientdist.git

# 拉取 GitLab 上的空仓库到本地仓库，以合并历史记录
git pull origin main --allow-unrelated-histories --rebase

# 把本地所有分支都推送到远端
git push -u origin --all --no-verify --force-with-lease
# 只推送当前分支可以用（main是当前分支的名字）
# git push -u origin main --no-verify --force-with-lease
```



## 2、本地开启代理后，仍会出现Git更新失败的问题
在Git设置里添加代理的配置

```bash
git config --global https.proxy "http://192.168.0.221:10811"
```



## 3、版本库丢失lfs对象，导致拉取失败
```bash
Downloading client/Assets/ArtContent/Audio/VO_Story/vocn/VO_Guide_L0/vo_guide_51001.wav (455 KB)

Error downloading object: client/Assets/ArtContent/Audio/VO_Story/vocn/VO_Guide_L0/vo_guide_51001.wav (25bc3e0): Smudge error: Error downloading client/Assets/ArtContent/Audio/VO_Story/vocn/VO_Guide_L0/vo_guide_51001.wav (25bc3e061ae9e938f10160b2ca20ed78d4c6a3efecf4c381f1ed6d2b5a1e3da0): [25bc3e061ae9e938f10160b2ca20ed78d4c6a3efecf4c381f1ed6d2b5a1e3da0] Object does not exist on the server or you don't have permissions to access it: [404] Object does not exist on the server or you don't have permissions to access it

Errors logged to 'E:\U3D\fafnir\.git\lfs\logs\20240430T104534.8151826.log'.

Use `git lfs logs last` to view the log.

external filter 'git-lfs filter-process' failed

client/Assets/ArtContent/Audio/VO_Story/vocn/VO_Guide_L0/vo_guide_51001.wav: smudge filter lfs failed
```



**解决**

```bash
git lfs install --skip-smudge
```





## 4、Revert "merge commit"后，希望重新合并之前的分支
**需求情景：**某人在错误的时机提交了一个分支合并，当下需要先回退，在未来的某个时间点重新合入。

1）revert一条合并提交，需要用-m参数指定还原到哪个父节点：`git revert -m [1/2] `

用-m参数的1、2指定父节点

2）重新合并的时候，直接merge会没有效果，因为git中已经合并过的提交无法进行二次合并（即便revert了也不行）。

能够把内容合入的操作：

1. 先把前面revert的提交revert掉，即：revert revert merge commit【这一步为了把前面revert掉的内容还原回来】
2. 重新合并分支【如果分支后面有新提交，这一步是把新提交合入】



**另类情景：**某人合并时，错误的把其他人的内容丢弃，导致版本内容出错。此时回退是为了纠错，不能通过上述方式来再次合并。

<font style="color:#DF2A3F;">只能手动处理，将被revert掉的资源找回</font>。



**两种情景的区别**

情景1：只是merge的时机不对，内容本身没有问题，可以在后面的节点把内容还原

情景2：merge本身是错的，里面的内容不对，只能手动找回内容。



## 5、大文件莫名修改，且无法撤销：Encountered x files that should have been pointers, but weren't:
```lua
 fatal: unknown write failure on standard output
Encountered 3 files that should have been pointers, but weren't:
client/Assets/Art/Particle/Mesh/Fbx_FX_Cpt_banyuan_01.fbx
client/Assets/Art/Particle/Mesh/Fbx_FX_Fbq_Cylinder_01.FBX
client/Assets/Art/Particle/Mesh/Fbx_FX_Fbq_Cylinder_02.FBX  
```



```shell
git rm .gitattributes
git reset --hard HEAD
```

