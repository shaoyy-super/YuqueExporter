

原因：关卡整体抬升，连带InstancedFoliageActor一起抬升，InstancedFoliageActor位置改变后，Foliage的依附关系就会被破坏，变为invalid Foliage



Foliage失效后果: 

无法跟随依附物体的位置变化更新位置，无法编辑（新刷的不影响），编辑Foliage依附物体时（Landscape，模型等），失效Foliage会被清空



挽救方法：（不保证全都能救）

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1721892299018-52854df4-51d3-415d-859c-93d257c345bb.png)

Foliage模式下选中Invalid, 可以选中所有失效的Foliage，然后按w键进入移动模式，将失效的Foliage移动位置至地表（或其他依附物体）上方的位置，距离越近吸附成功率越高，然后按End键，失效Foliage会自动重新吸附，但不保证所有失效Foliage都能重新吸附成功





