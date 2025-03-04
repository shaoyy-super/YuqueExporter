# 捕鱼Layer特殊用法
![](https://cdn.nlark.com/yuque/0/2024/png/1660870/1729145007421-0887d26a-0f9a-4433-8109-a8ea204c066c.png)

捕鱼中Layer的用法和继承自FA Layer方案的用法略有差别：

1. 捕鱼**Character、Character2、Character3**都用做鱼或者炮台使用。其中存在于上图Layer Priority中的Layer，可以屏蔽部分压暗效果的影响。在Layer Priority中越靠上的Layer，受压暗的影响越小。因此可以看出来: 
    1. **Character**是鱼场中正常游动鱼的通用设置，意思是会完全被压暗影响到，被Timeline中的表现覆盖掉。
    2. **Character2**是炮台的设置，它和EffectSkill属于同一层，意思是会屏蔽部分压暗的影响，但是会被Character3层的TML表现给覆盖掉。例如，炮台常驻Character2，也就是说炮台永远比鱼场里的鱼优先级高，不受低层压暗影响，但是会被TML中高级Boss鱼的表现给覆盖掉。
    3. **Character3**是TML中Boss鱼出场、死亡动画时用的设置，意思是不受压暗影响，会覆盖掉层级比它低的图层。
2. 捕鱼中绝大部分特效默认使用**EffectSkill**层，但是由于播放TML时，鱼和鱼身上的特效需要处于同一层级，所以鱼身上的特效常常会被设置为和鱼同样的层级，比如Character或者Character3，具体取决于是否正在播放TML。

