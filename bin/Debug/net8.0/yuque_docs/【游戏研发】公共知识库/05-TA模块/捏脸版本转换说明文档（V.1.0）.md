:::danger
**术语：**

**Netural**：代表所有BlendShape为0时，所代表的模型

:::

# 版本预处理及留存文件
## 美术文件
### MAX源文件/Maya文件
源文件需有以下两个部分：

1. 头部模型的BlendShape混合模型（带有所有的BlendShape的权重），且当前所有权重为0，名称为Face
2. 每个单个的BlendShape权重为最大值时的模型，名称为每个BlendShape的模型

:::danger
这里的仅仅只需要捏脸所需要的BlendShape，不需要表情的！

:::

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725938605417-6555d4d1-1258-4790-9afd-ad99417ed584.png)

### 说明文档
同时出一份文档，说明每个BlendShape所对应的含义：方便不同版本之间进行对比（目前先统一了一份Execl文档）

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725945904419-4e6c8f3f-3499-4fc3-836b-98e4ed8fa049.png)

## 程序处理留存文件
### OBJ原始文件
:::danger
关于OBJ格式说明可参照：[https://all3dp.com/2/obj-file-format-simply-explained/](https://all3dp.com/2/obj-file-format-simply-explained/)

:::

OBJ原始文件从Max或者Maya文件中导出，作为程序端的原始数据留存

OBJ文件分为两个部分：

:::color1
**Netural的OBJ**

命名为**Face**，是所有BlendShape权重归零时的Mesh文件

:::

:::color1
**BlendShape形变OBJ**

命名为**BlendShape的名称**，每一个BlendShape值最大时形成的Mesh文件

:::

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725946934470-c18ed327-3200-4ab8-bf81-efd72c8cd5b4.png)

### Data数据文件
:::danger
Data数据文件用于捏脸版本转换时所需要的文件，存储运算时所必须要的数据信息，是我们内部自定义的文件

:::

**Data文件存储内容：**

1. **均值向量：**Netural的vertex向量，维度为：（1，顶点的数量*3）
2. **系数矩阵：**每个BlendShape，维度为：（BlendShape数量，定点数量*3）

:::color1
利用Data文件并不能还原Mesh，因为Data文件中并不存储三角面的构成信息，只能够还原顶点数据信息

:::

- [x] 关于Data数据文件的存储格式，还需要讨论

# 版本构建流程
## 概览
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725955438564-f032edd2-8959-4c76-a5dd-31d8c2339b92.png)

首先美术将原始美术文件准备好，并留存

利用MaxScirpt或者Maya python脚本导出Netural和BlendShape模型，成为OBJ文件，并留存

利用python脚本工具生成Data文件用于实际使用

## 美术文件
在maya或者Max中存有BlendShape混合模型和单个BlendShape模型，如下图：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725938605417-6555d4d1-1258-4790-9afd-ad99417ed584.png?x-oss-process=image%2Fformat%2Cwebp%2Fresize%2Cw_1406%2Climit_0)

需要注意，所有的模型，位移归零后，需要在同一坐标位置：

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725950976375-80d5375c-b89d-403b-a8f2-6bb1f884c68b.png)

## 导出OBJ脚本工具
### MaxScript
暂无（可以导出FBX到Maya处理）

### Maya
#### 脚本（注意第六行为自己的目录路径）
```python
import maya.cmds as cmds
import os

selected_objects = cmds.ls(selection=True)

outputDir = r"D:\WorkingProject\DIYBODY\obj"

print(selected_objects)

for object in selected_objects:
    cmds.select(object)
    path = os.path.join(outputDir,str(object) + ".obj")
    print(path)
    cmds.setAttr(str(object) + ".translateX",0)
    cmds.setAttr(str(object) + ".translateY",0)
    cmds.setAttr(str(object) + ".translateZ",0)
    cmds.file(path,pr=1,typ="OBJexport",es=1,op="groups=0; ptgroups=0;materials=0; smoothing=0; normals=0")

print("end")
```

#### 流程
1. 打开Maya脚本编辑器，切换到Python，粘贴脚本，**并修改outputDir为自己的路径**

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725951367801-325f0443-af80-4e6f-bc2d-7557ccd2d800.png)

2. 选中所有的BlendShape

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725951078747-36107617-e300-4efe-b68b-244f7af17efe.png)

3. 运行脚本

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725951510255-43630e7b-428f-41cf-8836-ac2c00468caf.png)

如果文件夹下面输出对应的OBJ，则代表脚本运行成功

4. 选择Netural（BlendShape混合模型文件，确保当前权重均为0），重复上述脚本步骤，导出Face.obj（**如果不是导出后重命名为Face**）

![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725951682579-04aac372-95b0-44b0-8a14-bbd31b475158.png)

## Python脚本构建Data数据
### 脚本：（后续可以做成一个交互式工具）
```python
import numpy as np
import os

blendshapeNum = 106
vertexNum = 4877

basepath = r"D:\WorkingProject\DIYBODY"
filepath = os.path.join(basepath,"obj")
outputPath = os.path.join(basepath,"test.data2")

dim = (blendshapeNum,vertexNum * 3)

def readOBJ(filename):
    f = open(filename, "r")
    lines = f.readlines()

    vertexArray = []
    for line in lines:
        if line.startswith("#"):
            continue
        if line.startswith("v "):
            verts = line.split(' ')
            vertexArray.append(float(verts[1]))
            vertexArray.append(float(verts[2]))
            vertexArray.append(float(verts[3]))

    npArray = np.array(vertexArray)
    #print(npArray.shape)
    return npArray

f = []


for (dirpath, dirnames, filenames) in os.walk(filepath):
    f.extend(filenames)

# for file in f:
#     print(file)


a = np.zeros(dim,dtype=float)

#print(matrix[0][:].shape)
mean = readOBJ(filepath + r"\Face.obj")

i = 0
for file in f:
    if file.startswith("I"):
        array = readOBJ(filepath + r"\\" + file)
        a[i] = array - mean
        i = i + 1

with open(outputPath,'wb') as f:
    np.save(f,mean)
    np.save(f,a)
```

> **blendshapeNum: **blendshape的数量
>
> **vertexNum：**顶点数量
>
> **basepath：**工作路径
>
> **filepath：**OBJ文件路径
>
> **outputPath：**输出Data文件路径
>

**运行后即可输出Data文件**

# 版本转换流程
## 服务器端转换方案
![](https://cdn.nlark.com/yuque/0/2024/png/46024715/1725953313012-013424d5-0783-4840-99bd-5c56649d5e8b.png)

| **优点** | **缺点** |
| :---: | :---: |
| 与客户端版本分离 | 暂无？ |
| 客户端不需要计算 |  |




