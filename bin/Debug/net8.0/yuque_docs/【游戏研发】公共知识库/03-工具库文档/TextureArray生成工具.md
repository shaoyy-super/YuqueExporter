

## 使用方法：
1、选择要生成纹理数组的文件夹，`右键->Create->MoleTools->TextureArray->CreateWithFolder`

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1712743762469-f6f54b41-7c8b-4e11-873d-deba71a4092d.png)



2、会在相同的文件夹下生成 `文件夹名_[N]`纹理数组文件

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1712743986881-b2ba7f42-e708-4353-9524-d4c5cd92ac8d.png)



3、目前也支持在同一个文件夹下，选中多个纹理生成纹理数组`右键->Create->MoleTools->TextureArray->CreateWithTextures`

![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1712744610760-e3e655eb-b1ce-4d6c-a70c-c734633e2d52.png)



_**后续使用过程中，再确认是否只保留一种方式**_



## 注意事项：
1、必须在Android平台下操作，也就是BuildSettings要保证是Android

2、要生成纹理数组的纹理要保证大小都一样，比如都是1024*1024或256*256

3、如果是选择多个纹理生成，要保证这些纹理在同一个文件夹下

4、选择多个纹理，生成的<font style="color:#DF2A3F;">纹理数组下标和选择的先后顺序一致</font>，比如文件夹下虽然是01、02，如果先选了02、后选了01，则纹理数组下标对应02、01

5、纹理压缩格式要选择<font style="color:#DF2A3F;">RGB(A) ASTC 6x6 block</font>（具体规则后续再确定）



