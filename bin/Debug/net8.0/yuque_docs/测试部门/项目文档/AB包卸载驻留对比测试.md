### 1.需要先用Jenkins打ab包：
<u>http://192.168.51.222:8080/view/FA%E5%BC%80%E5%8F%91/job/FA_AssetBundle_2021/</u>

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647954546-decb28b8-6cdf-44f0-832e-db3d7ea61385.png)

这里替换掉分支名

### 2.拉取打完ab包最新的分支，看有没有拉到那一条
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647954553-e34eb588-3b50-487b-b024-4d0107c1f0db.png)

### 3.修改本地文件，改变内存加载、缓存方式
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647954550-301b2250-65fe-4c93-8d5f-145544351dcd.png)

### 4.修改内存节点筛选
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647954523-57cb8a41-e705-415f-a496-9b7ca8f1f1af.png)

### 5.启动游戏（不更新AB包）
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647954536-7a24efb2-00ca-4ed4-ae21-c261e8d53cfc.png)

### 6.打开Unity Profiler，只勾选内存
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647955073-9e53985b-bf3e-45ca-9823-be9f9944f086.png)

### 7.进入游戏，切换场景就会看到内存变化
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647955110-d20012f7-73e1-444f-abfd-2c987c8daafd.png)

### 8.测试方法：老号首次进入主城，先截取一下内存保存再保存到本地
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647955158-8f419f76-ac59-4aa0-a387-e2dc3aa2558c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647955387-c7355ccd-38ae-46d8-8afd-84e834fadc68.png)

### 9.会保存在项目这里
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647955377-8e3a0b77-fc61-4e55-9d60-0f56337f59a0.png)

### 10.测试流程：前往世界地图——主线副本——拟像挑战——特训考核——阵型——返回主城 （可以有所变化，主要是为了多切几个场景再返回主城，对比AB包）
注意事项：上下阵阵型后，一定要进行一场战斗验证，内存才会进行额外的增加和卸载

### 11.回到主城后再重复8的操作，保存在本地后打开Beyond Campare进行对比
![](https://cdn.nlark.com/yuque/0/2024/png/43256946/1712647955579-5067bbe7-4ced-4ed9-994e-2fb5810c5b8a.png)

