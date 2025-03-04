# 面板说明
![](https://cdn.nlark.com/yuque/0/2024/png/22817384/1713943000261-dc7a712c-2ee0-4d67-97eb-efe4cd69a85a.png)

1.只对己方生效：勾选状态下该轨道只有在己方行动中会播放音频。

2.Pos Type：可以选择在自身位置播放还是在目标位置播放

3.PlaySpecificAudio：勾选状态为播放特定的音频（AudioRange中的一个音频或Audio字段中选择的音频）；非勾选下则为播放AudioType1或者AudioType2（俩者都有值的情况下随机播放）

4.AudioType1：音频类型（AudioType2一样）

5.Volume1：AudioType1音量（Volume2一样）

6.Audio：勾选（3. PlaySpecificAudio）且AudioRange中没有值时播放的指定音频文件

7.AudioRange：一个音频列表，在勾选（3. PlaySpecificAudio）的时候会根据列表中设置的权重随机列表中的一个音频播放

8.volume：AudioRange的音频音量

9.GroupPath：填入一道音轨。注意！你选择的audio会被放在此处填入的音轨中播放！默认为skill0音轨。请注意修改！对应AudioSetting中ctrl_group下的那些音轨。

10.SoundType：音频类型

11.IsLoop：是否循环播放

12.UseTimeScale：是否受到时间缩放影响

13.是否暂停BGM：勾选则为播放此音频时暂停BGM

# 相关文档
[thoughts 文件夹](https://thoughts.teambition.com/workspaces/5df8bf6497d77a00134e3c27/folders/62133b4a04d0f2000160a529)

