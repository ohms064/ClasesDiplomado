// shadertype=unity
Shader "Diplomado/CGDiffuse"{
	Properties{
		_MainTex("Textura Base", 2D ) = "white"{}
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader{
		Tags{"Render Type" = "Opaque"}
		CGPROGRAM

			#pragma surface surf Lambert
			sampler2D _MainTex;
			float4 _Color;

			struct Input{
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput io){
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				io.Albedo = c.rgb * _Color.xyz;
				io.Alpha = c.a;
			}

		ENDCG

		
	}

	Fallback "Diffuse"
}