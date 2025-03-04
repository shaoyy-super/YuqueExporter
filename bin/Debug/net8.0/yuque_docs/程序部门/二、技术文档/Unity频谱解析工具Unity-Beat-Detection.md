# 一.简介
一款可以实时解析频谱的Unity开源工具。

# 二.目标
1.利用实时获取的<font style="color:#DF2A3F;">频谱</font>数据来处理场景中的物件。

2.什么是<font style="color:#DF2A3F;">频谱</font>：<font style="color:rgb(77, 77, 77);">就是频率的分布曲线，复杂振荡分解为振幅不同和频率不同的谐振荡，这些谐振荡的幅值按频率排列的的图形叫做频谱。</font>

<font style="color:rgb(77, 77, 77);">通俗点说就是声音信号通过傅里叶变换从时域转换到频率域。</font>

<font style="color:rgb(77, 77, 77);">再简单点理解就是声音信号在某一刻的某些频率上的震动幅度。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720158157528-444546c1-fede-4ede-82bf-73c9cfc226eb.png)<font style="color:rgb(77, 77, 77);"></font>

# 三.使用
### 1.导入Unity-Beat-Detection
下载Unity-Beat-Detection插件，将AudioProcessor(Mono)脚本放入Unity中。

### 2.使用AudioProcessor监听频谱
挂载AudioProcessor脚本，监听onSpectrum获取多段频谱值，监听onOnbeatDetected获取节拍事件以及节拍强度

```plain
void Start ()
{
	AudioProcessor processor = FindObjectOfType<AudioProcessor> ();
	processor.onBeat.AddListener (onOnbeatDetected);
	processor.onSpectrum.AddListener (onSpectrum);
}

void onOnbeatDetected (float beatValue)
{
  Debug.Log ("Beat!!!");
  Debug.Log("Beat!!! Value : " + beatValue);
}

void onSpectrum (float[] spectrum)
{
	//The spectrum is logarithmically averaged
	//to 12 bands
	for (int i = 0; i < spectrum.Length; ++i) 
  {
		  Vector3 start = new Vector3 (i, 0, 0);
		  Vector3 end = new Vector3 (i, spectrum [i], 0);
		  Debug.DrawLine (start, end);
	}
}
```

### 3.计算方法和原理
unity提供AudioSource.GetSpectrumData方法获取音乐播放的当前帧的频谱，根据均值计算可以获取当前帧下的音乐震动幅度。

求取多个频谱平均值 (频谱平均计算方式多样，后续多方测试选最佳)

```plain
Public float GetAverage()
{
    // 获取音频频谱数据
    audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.BlackmanHarris);
 
    // 计算音频频谱的平均值
    float average = 0f;
    for (int i = 0; i < audioSpectrum.Length; i++)
    {
        average += audioSpectrum[i];
    }
    average /= audioSpectrum.Length; 
    return average;
}
```

# 四.功能结合
1.假定的目标效果(<font style="color:#DF2A3F;">视频1</font>)。

[此处为语雀卡片，点击链接查看](https://www.yuque.com/lw0nsy/zeet2g/et6paw7cyd7ypqgy#zfNkL)

2.根据多段频谱可以实现频谱可视化的效果。

![](https://cdn.nlark.com/yuque/0/2024/png/46026893/1720163129806-c7f85fa4-6833-4e8a-ae5f-506f9b7cc64b.png)

3.根据节拍/平均频谱调节物件的缩放和灯光强度(<font style="color:#DF2A3F;">视频2</font>)。

[此处为语雀卡片，点击链接查看](https://www.yuque.com/lw0nsy/zeet2g/et6paw7cyd7ypqgy#RXlNV)

# 五.实现方式和需求
1.前端：每帧算出音乐的频谱，通过计算平均(最高)频谱值得出一个振幅。

2.策划：需确认哪些场景物件和效果需要根据音乐振幅来做变化，以及相互关联的规则，举例(场景中的音响缩放，场景中的物件glow变化，场景中的灯光明暗，场景中灯光的旋转等等)。

3.策划/前端：配置振幅域k1、k2、k3...kn(k值域暂定0-1)。例如(k1表示物件缩放的振幅阈值，即当前振幅超过k1的值则物件放大，k2表示glow的振幅阈值，超过k2则改变物件glow等等)，所有的k值可简化为同一个值。

4.策划/前端：配置各效果过渡的Lerp值L1、L2、L3...Ln，让效果变化更加柔和，所有的L值可简化为同一个值。

5.TA：如需有物件glow变化，shader提供物件的glow参数供外围修改。

6.美术：提供需要变化的场景美术物件。

# 六.引用相关文献地址
[https://github.com/allanpichardo/Unity-Beat-Detection](https://github.com/allanpichardo/Unity-Beat-Detection) 插件工具

[https://assetstore.unity.com/packages/tools/audio/music-beat-audio-visualizer-192722](https://assetstore.unity.com/packages/tools/audio/music-beat-audio-visualizer-192722) Music-Beat

[https://jingyan.baidu.com/article/fa4125ac08512b69ad709252.html](https://jingyan.baidu.com/article/fa4125ac08512b69ad709252.html) 频谱的概念

[https://www.bilibili.com/video/BV1Nq4y137fc/?spm_id_from=333.337.search-card.all.click&vd_source=e737f202e14d1d257c06a54698a50e7f](https://www.bilibili.com/video/BV1Nq4y137fc/?spm_id_from=333.337.search-card.all.click&vd_source=e737f202e14d1d257c06a54698a50e7f) 参考舞力全开的游戏视频

# 七.问题(后续会持续更新)
### 1.频谱/振幅暂时没有想到如何与物件旋转绑定。
### 2.未有耳机来验证节拍振幅的节奏度，后续会带设备来确认。
### 3.观察舞力全开游戏视频，大部分灯光或glow闪烁都是循环播放或者固定模式。
### 4.需要多首曲目来测试实际效果。
### 5.因为需要每帧获取并计算振幅，性能可能会有损耗，当前未开始观察性能。


