参考阅读：[https://blog.csdn.net/weixin_50273713/article/details/136517339](https://blog.csdn.net/weixin_50273713/article/details/136517339)

原效果使用连连看

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1725269527389-ffacff3d-bac2-4afa-a3a8-b9c06b131066.png)

##### 获取voronoi函数：
使用连连看调整得到效果

自动编译shader代码，搜索得到voronoi函数代码：

```plain
float2 voronoihash100( float2 p )
{

    p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
    return frac( sin( p ) *43758.5453);
}

float voronoi100( float2 v, float time, inout float2 id, inout float2 mr, float smoothness, inout float2 smoothId )
{
    float2 n = floor( v );
    float2 f = frac( v );
    float F1 = 8.0;
    float F2 = 8.0; float2 mg = 0;
    for ( int j = -1; j <= 1; j++ )
    {
        for ( int i = -1; i <= 1; i++ )
        {
            float2 g = float2( i, j );
            float2 o = voronoihash100( n + g );
            o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
            float d = 0.5 * dot( r, r );
            if( d<F1 ) {
                F2 = F1;
                F1 = d; mg = g; mr = r; id = o;
            } else if( d<F2 ) {
                F2 = d;

            }
        }
    }
    return F1;
}
```



直接搬运封装hlsl文件



voronoi100函数：

输入：uv 扰动值 id mr  smoothness

输出： id mr  smoothness

smoothness这里没有用



使用voronoi函数生成多边形进行扰动：

#include "voronoi.hlsl"

前面记得导入voronoi.hlsl

##### SpecularFlakes函数计算noise高光：
                float2 idvoronoi = 0;

                float2 uvvoronoi = 0;    

初始化uv和id为0

                voronoi100(UV*_FlakesScale , _FlakesAngle, idvoronoi, uvvoronoi, 0);

添加参数_FlakesScale （数量）和_FlakesAngle（角度）控制

                half3 voronoiNormal = half3(idvoronoi , 1.0); 

使用voronoi函数生成的id作为扰动法向的xy

                half3 noiseNormalWS = normalize(mul(voronoiNormal,TBN));

转换到世界空间坐标系下面

                noiseNormalWS = NormalizeNormalPerPixel(noiseNormalWS);



                half3 flakesBaseColor =  SAMPLE_TEXTURE2D(_FlakesMap, sampler_FlakesMap,idvoronoi );

  <font style="color:rgb(77, 77, 77);">使用voronoi生成的id去采样颜色图片，然后和一个颜色相乘控制强度，相乘的颜色如果为0，那么亮片消失</font>              

                half3 flakesColor = SpecularFlakes(flakesBaseColor,_Roughness0,_Roughness1,WorldPos,noiseNormalWS,ViewDir);

添加_Roughness0和_Roughness1控制两层粗糙度

传入SpecularFlakes函数：贴图颜色 _Roughness0 _Roughness1 世界顶点 noise后的法线 视线



SpecularFlakes函数：由DualSpecularGGX改写，F项直接改为DualSpecularGGX,计算noise双层高光

```plain
float3 DualSpecularFlakes( float Lobe0Roughness,float Lobe1Roughness,float LobeMix, float3 SpecularColor,float NoH,float NoV, float NoL,float VoH)
{
	float Lobe0Alpha2 = Pow4( Lobe0Roughness );
	float Lobe1Alpha2 = Pow4( Lobe1Roughness );
	float AverageAlpha2 = Pow4( (Lobe0Roughness + Lobe1Roughness) * 0.5 );

	// Generalized microfacet specular
	float D = lerp(D_GGX_UE4( Lobe0Alpha2, NoH ),D_GGX_UE4( Lobe1Alpha2, NoH ),1.0 - LobeMix);
	float Vis = Vis_SmithJointApprox( AverageAlpha2, NoV, NoL );
	float3 F = DualSpecularGGX;

	return (D * Vis) * F;
}

float3 SpecularFlakes(float3 SpecularColor,float Lobe0Roughness,float Lobe1Roughness,float3 WorldPos, float3 N, float3 V)
{
	#if defined(_MAIN_LIGHT_SHADOWS_SCREEN) && !defined(_SURFACE_TYPE_TRANSPARENT)
	float4 positionCS = TransformWorldToHClip(WorldPos);
    float4 ShadowCoord = ComputeScreenPos(positionCS);
	#else
    float4 ShadowCoord = TransformWorldToShadowCoord(WorldPos);
	#endif
	float4 ShadowMask = float4(1.0,1.0,1.0,1.0);
    //--------直接光照--------
    //主光
    half3 DirectLighting_MainLight = half3(0,0,0);
    {
		Light MainLight = GetMainLight(ShadowCoord,WorldPos,ShadowMask);
		half3 L = MainLight.direction;
		half3 LightColor = MainLight.color;
		float Shadow = MainLight.shadowAttenuation;

		float3 H = normalize(L + V);
		float NoH = saturate(dot(N,H));
		float NoV = saturate(abs(dot(N,V)) + 1e-5);
		float NoL = saturate(dot(N,L));
		float VoH = saturate(dot(V,H));
		float3 Radiance = NoL * LightColor * Shadow * PI;

		DirectLighting_MainLight = DualSpecularFlakes(Lobe0Roughness, Lobe1Roughness, 0.85, SpecularColor, 
								NoH, NoV, NoL, VoH) * Radiance;
    }
    //附加光
    half3 DirectLighting_AddLight = half3(0,0,0);
    #if defined(_ADDITIONAL_LIGHTS)
    int pixelLightCount = GetAdditionalLightsCount();
    for (int lightIndex = 0u; lightIndex < pixelLightCount; ++lightIndex)
    {
		Light light = GetAdditionalLight(lightIndex,WorldPos,ShadowMask);
        half3 L = light.direction;
        half3 LightColor = light.color;
        half Shadow = light.shadowAttenuation * light.distanceAttenuation;

		float3 H = normalize(L + V);
		float NoH = saturate(dot(N,H));
		float NoV = saturate(abs(dot(N,V)) + 1e-5);
		float NoL = saturate(dot(N,L));
		float VoH = saturate(dot(V,H));
		float3 Radiance = NoL * LightColor * Shadow * PI;

        DirectLighting_AddLight += DualSpecularFlakes(Lobe0Roughness, Lobe1Roughness, 0.85, SpecularColor, 
							NoH, NoV, NoL, VoH) * Radiance;
    }
    #endif
    float3 DirectLighting = DirectLighting_MainLight + DirectLighting_AddLight;

	return DirectLighting;
}

```





得到最终亮片颜色flakesColor

  half4 color = half4(XXXXXXX+flakesColor,1);

  return color;

把flakesColor加到最后输出结果即可，得到噪点亮片

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1725435209192-8dcec258-f0f5-48c9-82db-b558fc623605.png)

加颜色贴图效果

![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1725332461756-78e27216-f901-49f8-8758-f11a98db70b1.png)

##### 




##### 
