Shader "Custom/dragonlight" {
	Properties {
		[HDR]_Color1 ("Color", Color) = (1,1,1,1)
		[HDR]_Color2 ("Color", Color) = (1,1,1,1)
		_EmissionRange("Emission",Range(0,10)) = 1 
		_ColorRange("colorRange",Range(0,1)) = 0 
		_Bumpmap("Normalmap",2D) = "bump" {}
		_MainTex ("Albedo", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows


		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bumpmap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bumpmap;
		};

		float _EmissionRange;
		float _ColorRange;
		float4 _Color1;
		float4 _Color2;



		void surf (Input IN, inout SurfaceOutputStandard o) {


			float4 minColor;
			minColor = _Color2 - _Color1;

			float4 OutputColor = _Color1 + (minColor * _ColorRange);

			//_Color1.r = _Color1.r + minColor.r * _ColorRange;


			//float3 a = 
			float4 c = tex2D(_MainTex, IN.uv_MainTex) * OutputColor.rgba;
			float3 n = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap));
			o.Emission = c.rgb * _EmissionRange;

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
