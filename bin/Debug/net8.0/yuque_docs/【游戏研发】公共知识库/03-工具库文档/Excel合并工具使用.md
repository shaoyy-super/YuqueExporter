# 一. 用途
Excel合并工具用于将同一个表格的可合并的修改进行自动合并，避免繁琐的手动合并，节省策划时间。

工具地址：

file://192.168.0.231/AI及Web3游戏研发中心/公共资源/04_软件/办公软件/ExcelMergeTool/ExcelMerge.exe

<font style="color:#DF2A3F;">拷贝到自己电脑文件夹之后使用</font>

<font style="color:#DF2A3F;">拷贝到自己电脑文件夹之后使用</font>

<font style="color:#DF2A3F;">拷贝到自己电脑文件夹之后使用</font>

# 二. 在Fork中配置合并工具环境
打开Git客户端工具Fork，点击File并选择Preferences...，在弹出的窗口中选择Integration，如下所示：

![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735110204427-811a18d7-2d1c-4c5b-9deb-87c3ec697268.png)

这里需要将Merge Tool设置为自定义的Excel合并工具。Merger 选择Custom，Merger Path设置为工具执行文件的路径，Arguments设置为 **"$MERGED" "$REMOTE" "$LOCAL" "$BASE"**，如下所示：

![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735110742431-410adafe-2e3f-40e8-ad0a-80161bd5cf2e.png)

Fork合并工具配置完成。

# 三. 合并冲突及解决
这里先制造点冲突，先clone下两个相同的工程并对Customize这张表进行修改，Base数据如下：

![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735112021876-61e66bb1-897d-4267-8db4-0bcd63b4d849.png)

先将第一个工程的Customize表数据进行如下修改并进行Commit以及Push，Remote数据如下：

![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735112206005-3fe00a2f-bb35-4e43-ac80-c155c284b5db.png)

再将第二个工程的Customize表数据进行修改Commit，Local 数据如下：

![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735112377271-2282f806-0f4b-42b7-9404-81512af1b575.png)



由于双方同时对同一张Excel表进行了修改且Excel表格数据为二进制数据，所以fork无法进行合并故产生冲突，如下所示：![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735112793339-f43eff54-578c-4105-84d0-3524fc0a111f.png)

这里需要点击上图中的 **Merge in external merger **按钮进行Excel的合并，该按钮便是调用先前所设置的Merger Tool工具，点击后如下所示：![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735113061386-37c6b224-2046-4204-b7af-7ce3837d7a91.png)

出现"合并成功"字样则表示表格合并完成，窗口会自动关闭，请不要手动点击关闭。

勾选**Amend**选框，Staged窗口栏会出现修改的表格，如下所示：![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735113546094-4ca8f8dd-7d58-48a1-827d-2e2a85924f72.png)

执行完上述步骤后，界面会提示冲突已解决，如下所示：

![](https://cdn.nlark.com/yuque/0/2024/png/29579282/1735113762897-4114b7be-bd2a-46f8-ad74-e06202ef1d49.png)

点击Commit File 按钮合并操作就结束了，之后点击Push其他同学拉取的就是合并完成后的Excel表格数据了

# 四. 注意事项
1. Fork中没有找到只针对 .xlsm  类型文件设置合并工具的方式，因此需注意只在处理Excel表格合并冲突时才点击**Merge in external merger **调用外部工具进行合并
2. 当某一方对Excel表头数据进行了修改或修改了相同位置的数据时合并失败，需要自己手动进行合并
3. 当romate 对Excel表进行了删除Sheet时，合并工具不会在合并后的表进行删除同样的sheet，若提交合并后的Excel，先前的删除整个Sheet操作将还原
4. 不允许对Excel不同的列进行合并单元格，如A列不可与B列进行单元格合并，允许不同的行进行合并，如第10行可以与第11行的单元格进行合并
5. 当合并时提示“已经有一个程序窗口在运行！！！” 字样时，请关闭其他控制台窗口并删除包含“合并工具日志文件.txt”字样的文件。

