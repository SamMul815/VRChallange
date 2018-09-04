Shader "Custom/rim" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_rimpower("Rimpower",Range(0,100))=0
		_rimcolor ("Rim Color", Color) = (1,1,1,1)
		_maincolor ("Main Color", Color) = (1,1,1,1)
		_emissionbar ("Emission Bar", Range(0,50))=0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Opaque"="Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf hn fullforwardshadows alpha:fade
		#pragma target 3.0

		sampler2D _MainTex;
		float _rimpower;
		float4 _rimcolor;
		float4 _maincolor;
		float _emissionbar;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float4 color:Color;
		};

		void surf (Input IN, inout SurfaceOutput o) {
		float4 c = tex2D (_MainTex, IN.uv_MainTex)*_maincolor;
		o.Emission=c.rgb*_emissionbar;
		o.Alpha = c.a;
		}

		float4 Lightinghn (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {

		
		float3 ndotl = saturate(dot(s.Normal,viewDir));
		float rim = pow(ndotl,_rimpower);

			float4 finalcolor;
			
			finalcolor.rgb= _rimcolor.rgb*s.Emission;
			finalcolor.a=rim*s.Alpha;
			return finalcolor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
