Shader "Custom/hn/noise&time" {
	Properties {
		[HDR]_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("texture", 2D) = "white" {}
		_MainTex02 ("noise texture", 2D) = "white" {}
		_Bar ("noise bar", Range(0,2)) = 0
		_Bar02 ("noise speed", Range(0,2)) = 0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		Cull Off

		CGPROGRAM
		#pragma surface surf Standard alpha:fade noambient

	
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex02;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex02;
			float4 color:Color;
		};

		fixed4 _Color;
		float _Bar;
		float _Bar02;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			float4 d = tex2D (_MainTex02, float2(IN.uv_MainTex02.x,IN.uv_MainTex02.y+ _Time.y*_Bar02));
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex+d.xy*_Bar) * _Color;
			
			o.Emission = c.rgb*IN.color.rgb;
			o.Alpha = c.a*IN.color.a;
		}


		ENDCG
	}
	FallBack "Diffuse"
}
