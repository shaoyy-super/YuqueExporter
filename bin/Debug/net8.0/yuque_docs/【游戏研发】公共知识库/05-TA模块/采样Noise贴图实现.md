<font style="color:rgb(77, 77, 77);">视角与法线的点积作为offset去干扰noise贴图的采样，实现noise贴图随着视角变化</font>

<font style="color:rgb(77, 77, 77);"></font>

	float ndotv = dot(normal, ViewDir);

法线dot视线

	half noise0 = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex, half2(uv.x + ndotv, uv.y)).r;

	half noise1 = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex, half2(uv.x, uv.y + ndotv)).r;

采样两次Noise贴图

用NdotV影响uv的xy分量

	float sparkle = pow(noise0 * noise1, _Sparkle); 

对两次Noise相乘并添加幂指数_Sparkle控制强度

	float highlight = pow(ndotv, _HighlightRange);

实现视线中心更亮效果，求简单的高光范围添加_HighlightRange控制范围

	half3 sparkleColor = sparkle * _SparkleColor.rgb * highlight;

sparkleColor  = 亮片Noise * 颜色* 范围



可以直接把sparkleColor 输出到最后颜色用_SparkleColor控制颜色

  half4 color = half4(XXXXXXX+sparkleColor ,1);

  return color;



也可以直接把sparkle * highlight作Alpha去裁剪原有颜色



![](https://cdn.nlark.com/yuque/0/2024/png/45145007/1725331624317-80efbdc2-b19a-47a1-b01a-bfb50c061620.png)

```plain
Shader "Custom/bulingbuling"
{
    Properties
    {
		_NoiseTex ("Noise Tex", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
		_Sparkle ("_Sparkle", Range(0.75, 5)) = 1
		_HighlightRange ("Highlight Range", Range(0, 5)) = 1 
		[HDR]_SparkleColor ("Sparkle Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        LOD 300

        Pass
        {
            Tags{"LightMode" = "UniversalForward"}
            Cull[_Cull]

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"


             TEXTURE2D(_NoiseTex);    SAMPLER(sampler_NoiseTex);

            CBUFFER_START(UnityPerMaterial)
			float4 _NoiseTex_ST;
			half4 _Color;
			float _Sparkle;
			half4 _SparkleColor;
			float _HighlightRange;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
                float2 texcoord     : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                half3 normalWS : TEXCOORD2;
                half4 tangentWS : TEXCOORD3;    
                float4 positionCS : SV_POSITION;
            };

            Varyings vert (Attributes input)
            {
                       Varyings output = (Varyings)0;


                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);

                output.uv = input.texcoord;
                output.normalWS = normalInput.normalWS;
                real sign = input.tangentOS.w * GetOddNegativeScale();
                half4 tangentWS = half4(normalInput.tangentWS.xyz, sign);
                output.tangentWS = tangentWS;
           
                output.positionWS = vertexInput.positionWS;
                output.positionCS = vertexInput.positionCS;

                return output;
            }

            half4 frag (Varyings i) : SV_Target
            {
				half3 normal = normalize(i.normalWS);
				half3 ViewDir = GetWorldSpaceNormalizeViewDir( i.positionWS);
                half2 uv = i.uv;
                uv =uv *_NoiseTex_ST.zy + _NoiseTex_ST.zw;
				float ndotv = dot(normal, ViewDir);
				half noise0 = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex, half2(uv.x + ndotv, uv.y)).r;
				half noise1 = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex, half2(uv.x, uv.y + ndotv)).r;

				float sparkle = pow(noise0 * noise1, _Sparkle); 
				float highlight = pow(ndotv, _HighlightRange);

				half3 sparkleColor = sparkle * _SparkleColor.rgb * highlight;

                return half4(_Color.rgb + sparkleColor, _Color.a);
            }
            ENDHLSL
        }
    }
}

```

