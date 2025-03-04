# 一、Volume
## （一）EnvironmentVolume
+ 角色环境色
+ SSAO
+ 自定义阴影

## （二）LightSettingVolume
+ 雾效控制
+ 环境光控制
+ 体积光
+ 阴影
+ 屏幕空间平面反射SSPR



Volume相关代码已经移植，具体每一个选项的支持还未处理

# 二、Feature


+ DepthOnlyFeatureWithoutClear
+ EditorDebugFeature TAA Debug
+ CustomSSAOFeature
+ ScreenSpaceDecalFeature
+ ForwardPlusWithZBiningRenderFeature
+ CustomShadowRenderFeature
+ OverdrawFeature
+ EffectCopyDepthFeature
+ CustomTransparentDepthRenderFeature
+ ScreenSpacePlanarReflectionFeature

## 三、部分shader Tags LightMode改动


+ AtomSphere_A.shader
+ VolumetricFog2DURP.shader
+ FASceneObject.shader
+ Mole_Effects_General.shader



## 四、自定义后期


+ TAA  现在移植的代码对应功能临时注释掉了



## 五、Shader Debug功能
接入、扩展URP自带的Shader Debug功能，方便查问题

