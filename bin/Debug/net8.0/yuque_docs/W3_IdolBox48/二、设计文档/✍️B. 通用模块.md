:::info
**说明：**通用模块提供跨系统的通用功能，这些模块为不同的系统提供支持，确保了功能的一致性和复用性。

:::

**示例:**

![](https://cdn.nlark.com/yuque/0/2024/png/12926950/1712565103226-2ca303f3-212d-4289-9e6f-685ff25d6d3c.png)

【更新】item、drop、charge、task、activity、supplier、trigger、chat、speaker、email、friend、schedule用了FA的表架构



1、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645328714-c55f787d-c3e7-4112-8039-d155bccc38a7.png)

竞技场功能先保留，后面改，合到Arena表

2、BadWords表保留

3、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645239505-33218cf1-5140-46f7-84c5-2886b487d3e7.png)

合到chat表

4、Chatspeaker表保留

5、DefaultConfig表保留

6、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645437194-0151c61f-b78e-4963-b667-1bfba524ac2e.png)

合到Drop表，不用改。

7、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645664292-b615fb18-8585-4842-8328-87c55a94e2f7.png)

合到Equip表，后面改。

8、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645764851-ff7d1e9c-e0d8-497a-a173-84705152343d.png)三张表保留

9、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645777515-2776422c-e4e9-4e7f-b7fc-23b39e5e7c60.png)

合到GuideStep表，后面用的时候改。

10、HelpMap表保留，后面改

11、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712645858299-c8129173-5e9d-496d-ba0c-6dc3f2088a12.png)

合到Item表，基本没啥修改，supplier部分后面再看是否需要单拆一张表

12、MailDefine表保留，SystemEmail改名Mail

13、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712646676870-ac94c7c8-5dce-422b-9435-cca4992c40f9.png)

合到Monster表，主要修改NpcAttribute，简化属性，暂时留6个用于对比的属性

14、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647258919-9f377f48-f4c1-4cd8-9a82-b187eddb5216.png)两张表保留

15、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647228066-7900e453-c954-4ff1-adfb-e2d872afc003.png)

合到player表

16、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647282600-a1f2a66d-49ef-41e9-ae6f-1160ed2164f0.png)

功能暂时保留，但是后面会改，没有玩家形象了，后面改为展示角色形象用

17、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647432012-a32aee94-6130-42a2-8c3c-d010f386fdb3.png)

合到resource表，后面用的时候改

18、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647476919-a27dae9c-67d6-41bc-8a5e-a1e3beda5e1e.png)

合到robot表，后面用的时候改

19、schedule表保留

20、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647587867-66a05b0e-5e9c-402a-8e52-55c7b4e299e3.png)

合到shop表

21、sound表保留

22、Task_ob改名为Task，功能不变

23、![](https://cdn.nlark.com/yuque/0/2024/png/43256847/1712647744399-d56de4c4-82ad-4293-9dd0-82716c78a7f6.png)

合到TreasureChest表，后面改







