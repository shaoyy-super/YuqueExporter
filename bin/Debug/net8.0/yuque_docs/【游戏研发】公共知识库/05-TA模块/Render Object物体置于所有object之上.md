最终效果：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1722410248029-a2118322-7f08-43e9-8eb9-1fadcf3eef13.png)



管线设置：

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1722488825471-7b73bae8-ce81-457b-96a2-f17b7b1e1e3a.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1722488839322-7e1d9045-5e49-48d7-9889-2b3083911afb.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1722483319604-5299296b-dd08-4848-8586-f8757ad00d7b.png)

前景再拆分为2个层级，一个转盘层级，一个骰子层级

**第一个Render Object: **渲染骰子层级Opaque物体，RenderPassEvent: BeforeRenderingOpaque，所有转盘之上的物体使用这个层级（目前只有骰子，包括未来可能加入的其他物体）

**第二个Render Object: **渲染转盘层级Opaque物体，RenderPassEvent: BeforeRenderingOpaque

**第三个Render Object: **渲染转盘与骰子之间的Transparent物体，RenderPassEvent: AfterRenderingTransparent

**第四个Render Object: **渲染骰子之上的Transparent物体，RenderPassEvent: AfterRenderingTransparent



**注意事项：**四个Render Object层级的Stencil Value需为Dice Transparent > Dice Opaque > DicePanel Transparent > DicePanel Opaque

如果需要在骰子和转盘之间再加层级的话需创建额外layer与对应的Opaque和Teansparent Render Object, 并满足上述的Stencil Value设置（可以但不建议...)





**SSAO问题：**

调整SSAO Pass与材质，与美术确认需使用的转盘和骰子材质，增加是否使用SSAO的开关



**场景材质：**

与美术确认需使用的场景材质，调整默认stencil设置





**转盘与骰子：**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1722431181633-5cef6205-0fdb-4d58-9f1e-622427a655af.png)

和美术重新对过，转盘和骰子使用的是另外的光源，无需考虑场景阴影对其的影响，因此不再考虑调整位置然后用RenderObject画回来的做法



**临时场景材质Stencil修改方法:**

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1722489424719-3e949171-96e0-48c2-bba1-20c468672976.png)

Comp: Equal

Pass: Keep

Stencil Ref: 0



没有上述选项的material不用管

