## 简介：
用于给物体新增材质，及修改现有材质的属性。

## 参数细节介绍
### 1.轨道参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942662048-4a1ee03e-f87d-4ead-9793-dde9a3e6883c.png)

| 参数 | 说明 |
| :--- | :--- |
| TargetSelect | [<u>TargetSelect</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a) |
| OnlySkinMeshRenderer | 若勾选，只对SkinMeshRenderer生效；<br/>若不勾选，则对所有renderer都会生效 |
| 添加材质到首位 | 将新加的材质至于Renderer材质列表的首位 |
| UseMaterialSetWay | 若勾选，则使用MaterialSet的方式修改材质；<br/>否则将使用MaterialBlock的方式修改材质 |
| 生效节点路径 | 仅对填入的节点及其子节点生效，不填时默认对整个角色生效 |
| 替换材质节点列表 | 因为手动填入**生效节点路径**太麻烦，所以这里是一个辅助功能，通过列表选择指定节点后，点击下方**选择按钮，**可自动填入**生效节点路径** |


### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942662233-f0818298-c32c-468c-b97c-d05903e24dc8.png)

| 参数 | 说明 |
| :--- | :--- |
| 要新增的材质 | 要新增的材质 |
| MaterialOperations | [<u>对现</u>**<u>有材质</u>**<u>的修改操作</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/620230fd04d0f2000108be29) |


[FA_Mole Append Material Track_模型部分位置外发光.mp4](https://tcs.teambition.net/storage/332eef545013c3ddbb67dab010cdb972b91d?Signature=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJRCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9hcHBJZCI6IjU5Mzc3MGZmODM5NjMyMDAyZTAzNThmMSIsIl9vcmdhbml6YXRpb25JZCI6IiIsImV4cCI6MTcxNDU0NzQ1MSwiaWF0IjoxNzEzOTQyNjUxLCJyZXNvdXJjZSI6Ii9zdG9yYWdlLzMzMmVlZjU0NTAxM2MzZGRiYjY3ZGFiMDEwY2RiOTcyYjkxZCJ9.25nuA_WJYQ1I4HM2SUrxEDVK2M-Dl1xZ5YXZtfJizkU&download=FA_Mole%20Append%20Material%20Track_%E6%A8%A1%E5%9E%8B%E9%83%A8%E5%88%86%E4%BD%8D%E7%BD%AE%E5%A4%96%E5%8F%91%E5%85%89.mp4)

