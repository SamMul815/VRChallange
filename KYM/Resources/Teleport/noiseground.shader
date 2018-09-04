Shader "Custom/noiseground" {
	Properties {
		[HDR]_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("noise",2D) = "white"{}
		_power("noisepower",float) = 3
		_ypower("y축파워",Range(0,0.1)) = 0.03
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" } cull off
		LOD 200

		CGPROGRAM


		#pragma surface surf Lambert noambient alpha:fade


		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float2 _ypower;


		struct Input {
			float2 uv_MainTex;
			float2 uv_NoiseTex;
		};

		float4 _Color;
		float _power;


		void surf (Input IN, inout SurfaceOutput o) {
			float4 i = tex2D(_NoiseTex, IN.uv_NoiseTex - _Time.x * _power);
			float4 i2 = tex2D(_NoiseTex, IN.uv_NoiseTex - _Time.y * 0.04);
			float4 ne = (i + i2);
			float4 c = tex2D(_MainTex, IN.uv_MainTex + ne * 0.03);
			//o.Emission = lerp(c, ne, c.a * 2);
			o.Emission = c.rgb * _Color;
			o.Alpha = c.a * 0.5;
		}
		ENDCG
	}
	FallBack "Transparent"
}
