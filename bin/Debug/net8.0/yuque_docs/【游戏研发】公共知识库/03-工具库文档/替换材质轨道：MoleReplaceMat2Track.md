## 简介：
使用clip中指定的材质，替换物体原有材质。以及修改现有材质的属性。

## 参数细节介绍
### 1.轨道参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942687336-78b18169-34b3-45e9-84a8-4e2ae692eacf.png)

| 参数 | 说明 |
| :--- | :--- |
| TargetSelect | [<u>TargetSelect</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/60cab52041cef6000179ed2a) |
| OnlySkinMeshRenderer | 若勾选，只对SkinMeshRenderer生效；<br/>若不勾选，则对所有renderer都会生效 |
| UseMaterialSetWay | 若勾选，则使用MaterialSet的方式修改材质；<br/>否则将使用MaterialBlock的方式修改材质 |
| LeaveAsIs | LeaveAsIs |
| 生效节点路径 | 仅对填入的节点及其子节点生效，不填时默认对整个角色生效 |
| 替换材质节点列表 | 因为手动填入**生效节点路径**太麻烦，所以这里是一个辅助功能，通过列表选择指定节点后，点击下方**选择按钮，**可自动填入**生效节点路径** |


### 2.片段参数
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713942687538-8682c34f-f27e-4872-86eb-8f4dac304db7.png)

| 参数 | 说明 |
| :--- | :--- |
| Replace | 要替换的材质，可以有多个 |
| MaterialOperations | [<u>对现有材质的修改操作</u>](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/docs/620230fd04d0f2000108be29) |


