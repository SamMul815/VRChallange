Shader "Custom/hand" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)

	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" ="Transparent" }
		LOD 200
		//1pass
		CGPROGRAM

		#pragma surface surf Lambert noambient alpha:fade


		#pragma target 3.0

		struct Input {
			float4 color:COLOR;
			float3 viewDir;
		};

		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutput o) {
			float rim = dot(o.Normal, IN.viewDir);
			rim = pow(1 - rim,2);
			o.Emission = _Color;
			o.Alpha = rim;
		}
		ENDCG
			/*
			//2pass
			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows


			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
			float2 uv_MainTex;
			};

			fixed4 _Color;


			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;

				o.Alpha = c.a;
			}
			ENDCG
			*/
	}
	FallBack "Diffuse"
}
