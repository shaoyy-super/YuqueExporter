### 组件的用途
挂载使用单个资源的组件，降低动态加载资源使用的复杂度

主要用来加载ui界面上的子页签panel。当一个ui界面层次比较多，资源比较大，可以把不同的特殊的元素单独做成panel，动态加载；

当一个界面有一个或多个子页签，点击按钮或者toggle切换不同的子页签情况下使用

### 组件的使用
下面以主城界面为例子，ui主界面组件设置如下：

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870939645-0eae7c89-0365-4161-b195-ca0a52c38321.png)![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870969050-014f6a78-c2d8-4366-a44f-12f94aded32b.png)



AssetHolder：c#脚本，需要绑定用到AssetHolder的ui控件上

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725869849716-a8f80cd9-a6a0-4e5d-b064-f7c922101494.png)

##### <font style="color:#DF2A3F;">当只有一个panel页签时，有两种方式都可以实现效果</font>
1.可以把预设拖拽到GO字段上，然后lua里面调用组件的InstGameObject属性，需要自己设置父节点

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870054935-0aa00781-a802-4850-b87f-62bd33941477.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870159099-de873dfc-58c6-4bf9-a101-896b50595721.png)

2.可以把资源拖拽到Assets列表里，然后lua里调用c4l的InstantiateAsset接口，传入参数（AssetHolder的index，资源在资源列表里的index，panel页签的父节点index），不需要自己额外设置父节点

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870341566-188527aa-7a98-409e-b759-14008be25606.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870612038-9e0cc262-8dc4-4ff3-824b-4cc5c120f50f.png)

##### <font style="color:#DF2A3F;">当有多个panel页签时，只能用第二种方法，把页签预设，拖入Assets列表，然后调用c4l的InstantiateAsset接口，传入参数。</font>


lua里面panel页签脚本：需要<font style="color:#DF2A3F;">继承UI_CompBase</font>，实现需要的接口方法

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725871347380-9b0c877c-d425-4b2a-94d9-2bd86c9a62f4.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725871514365-c25b8a38-db29-43a8-84f5-0f3a3c586816.png)

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725871558692-a8cd7515-959a-432b-96eb-f82bd6d353ea.png)

多个页签切换：

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725871757341-fc7b6a4e-b74b-4cc7-8edf-e1a74dd017be.png)

### 组件扩展
需要扩展的话，直接在c#代码AssetHolder里面扩展，添加需要的字段属性，然后在Component4Lua脚本里扩展对应的可供lua调用的方法。

![](https://cdn.nlark.com/yuque/0/2024/png/46334471/1725870612038-9e0cc262-8dc4-4ff3-824b-4cc5c120f50f.png?x-oss-process=image%2Fformat%2Cwebp)

### 注意事项
1.不要直接引用一个Atlas的Texture作为独立Asset，这样会导致打包的时候Texture被打两份（作为Sprite图集一份，作为独立的纹理资源一份）

2.Lua中尽量不要直接调用Assets，会有多次对象转换性能低

