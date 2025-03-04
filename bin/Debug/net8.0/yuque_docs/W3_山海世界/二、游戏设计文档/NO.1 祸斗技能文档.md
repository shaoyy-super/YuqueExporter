[此处为语雀卡片，点击链接查看](https://www.yuque.com/cod5mf/iwqppn/tcgoofe9hx1ov9z8#FReK8)



## 1、概述
宠物名：祸斗

宠物id：1

技能思路：攻击型、连续攻击





## 2、技能列表
| | <font style="color:black;">名称</font> | <font style="color:black;">概述</font> | <font style="color:black;">动作</font> | <font style="color:black;">攻击特效</font> | <font style="color:black;">弹道特效</font> | <font style="color:black;">受击特效</font> |
| --- | :---: | :---: | :---: | :---: | :---: | :---: |
| <font style="color:black;">通用技能</font> | 爪击 | <font style="color:black;">通用攻击动作1（近）</font> | <font style="color:black;">act_1_skill_01</font> | | | |
| | 吼叫 | <font style="color:black;">通用攻击动作2（远）</font> | <font style="color:black;">act_1_skill_02</font> | | | |
| <font style="color:black;">专属技能</font> | 嘶咬 | <font style="color:black;">冲到敌人面前，攻击敌人3次，造成伤害。</font> | <font style="color:black;">act_1_skill_sp01</font> | <font style="color:black;">eff_1_skill_sp01</font> | | <font style="color:black;">eff_1_hit_sp01</font> |
| | 炼狱 | <font style="color:black;">重击敌人一次，并增加己方所有宠物10%的攻击伤害。</font> | <font style="color:black;">act_1_skill_sp02</font> | <font style="color:black;">eff_1_skill_sp02</font> | <font style="color:black;">eff_1_bullet_sp02</font> | <font style="color:black;">eff_1_hit_sp02</font> |
| | 激怒姿态 | <font style="color:black;">释放后，你的所有攻击都记入激怒状态。激怒每达到5次使一个随机技能变为0费。持续3回合</font> | <font style="color:black;">act_1_skill_02</font> | <font style="color:black;">eff_1_skill_sp03</font> | | |




| | <font style="color:black;">名称</font> | <font style="color:black;">概述</font> | <font style="color:black;">动作</font> | <font style="color:black;">自身特效</font> | <font style="color:black;">触发特效</font> |
| --- | :---: | :---: | :---: | :---: | :---: |
| <font style="color:black;">专属BUFF</font> | <font style="color:black;">激怒</font> | <font style="color:black;">激怒：周身环绕火焰状的特效，5个时触发特殊效果</font> | | <font style="color:black;">eff_1_buff_01</font> | <font style="color:black;">eff_1_trigger_01</font> |






## 3、技能详述
### 3-1、通用技能：爪击
（通用技能随机调用的技能动作库，此动作主要适配所有近战技能）



技能描述：

移动到敌人目前，用爪子击打敌人，（衔接通用技能效果）。



使用技能后：

a、移动宠物至近战位置。

b、宠物触发一次攻击行为，播放此攻击动作。

c、在对应节点触发通用技能里的伤害或者技能效果。



### 3-2、通用技能：吼叫
（通用技能随机调用的技能动作库，此动作主要适配所有远战技能）



技能描述：

原地播放动作，吼叫一声，（衔接通用技能效果）。



使用技能后：

a、宠物触发一次攻击行为，播放此攻击动作。

b、在对应节点触发通用技能里的伤害或者技能效果。



### 3-3、专属技能：嘶咬


技能描述：

移动到敌人目前，撕咬敌人3次，分别造成{0}%，{0}%，{0}%三次伤害。



使用技能后：

a、技能释放，移动宠物至近战位置。

b、宠物触发第一次攻击，造成{0}%伤害。并触发一次攻击后需要结算效果

c、宠物触发第二次攻击，造成{0}%伤害。并触发一次攻击后需要结算效果

d、宠物触发第三次攻击，造成{0}%伤害。并触发一次攻击后需要结算效果

e、技能结束



### 3-4、专属技能：炼狱


技能描述：

<font style="color:black;">重击敌人一次，造成{0}%攻击伤害。并增加己方所有宠物{0}%的攻击伤害，持续至战斗结束。</font>



使用技能后：

a、技能释放，远程技能原地释放。

b、宠物触发第一次攻击，造成{0}%伤害。并触发一次攻击后需要结算效果

c、宠物释放buff效果一次，提升<font style="color:black;">己方所有宠物{0}%的攻击伤害。（直接获得buff效果）</font>

<font style="color:black;">d、技能结束。</font>





### 3-5、专属技能：激怒姿态


技能描述：

<font style="color:black;">释放后，你的所有攻击都记入激怒状态。激怒每达到{5}次使一个随机技能费用减少{100%}。持续{3}回合</font>



使用技能后：

a、技能释放，宠物原地播放动作

b、宠物释放buff效果一次，激怒层数初始为{0}层。<font style="color:black;">（直接获得buff效果）</font>



<font style="color:black;">buff</font>（激怒）<font style="color:black;">效果：</font>

<font style="color:black;">a、buff直接生效，显示buff特效</font>

<font style="color:black;">b、之后该宠物每次攻击，都让buff层数增加{1}。</font>

<font style="color:black;">c、到达{5}层计数时，触发此buff（再次释放技能，持续时间可以延长，但如果持续时间结束，层数<5，则清除buff并计数归零）</font>

<font style="color:black;"></font>

<font style="color:black;">buff（激怒）触发效果：</font>

<font style="color:black;">a、buff {5} 层时，插入技能释放队列，等待上一个技能结算完成。</font>

<font style="color:black;">b、触发buff，播放对应动作和特效</font>

<font style="color:black;">c、在技能牌中随机选中一个技能（除被动技能），使该技能费用减少 {100%} 。技能使用一次后此效果消失</font>

