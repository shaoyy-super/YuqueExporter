# 一、捏人编辑器项目概况：
**Git地址：**[**Programmer / DIYEditor · GitLab**](http://192.168.2.240/programmer/DIYEditor)



# 二、相关问题及注意事项：
## 目前场景仍然使用的URP雾、SSAO、光源阴影、及天空盒等，暂未使用UT-Rendering的雾、SSAO、光源及阴影、角色环境色。
**原因**：

该项目目前衣服资产多数仍是使用北京来的YYD着色器以及贴图，且短时间内没法都改成UT-Rendering里的衣服Shader，所以没法使用UT-Rendering的管线自定义功能。只能先处于项目既有YYD材质，又有自定义衣服材质的阶段。



## 变体收集工具使用时，如果删了一些Shader，那要记得手动清空ShaderVariantCollector文件夹。然后再执行自动收集和手动收集。
## 变体收集工具配置时，Exclude Shaders和Global Keywords尽量少放点，尤其是Global Keywords只能放必要的，不要多放。不然对打包速度会有一个数量级的影响。
