# Shader 结构

    Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
    }

## shader 关键字
 Properties 、Color、SubShader、FallBack
## Properties
Properties 属性参数，对应Unity 编辑器面板

* "Color" 属性名  Color 属性类型(shader的关键字) (1,1,1,1)变量值

## SubShader
为GPU渲染，编写的专门shader片段

至少有一个subshader, 如果当前第一个subshader可以良好的被硬件使用，就使用第一个subshader;否则尝试使用后续的subshader.

如果没有一个subshader被硬件支持，就使用fallback

## FallBack
字面意思就是回滚

没有一个subshader被正常执行，就回滚使用系统提供的一些简单shader（内建shader）。

* Unlit 不发光，这只是一个纹理，不被任何光照影响； 经常用于UI
* VertexLit 顶点光照
* Diffuse 漫反射
* Normal mapped 法线贴图，比漫反射更昂贵；增加了一个或更多纹理(法线贴图)和几个着色器结构
* Specular 高光。这增加了特殊的高光计算
* Normal Mapped Specular 高光法线贴图。这比高光更昂贵一点
* Parallax Normal Mapped 视差法线贴图。这增加了视差法线贴图计算
* Parallax Normal Mapped Specullar 视差法线高光贴图。 这增加了视差法线贴图和镜面高光计算

