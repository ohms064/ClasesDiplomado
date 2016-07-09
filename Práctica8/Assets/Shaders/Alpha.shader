Shader "Diplomado/Alpha"{
	Properties{
		_MainTex("Textura Base", 2D ) = "white"{}
		_TransVal("Transparencia", Range(0,1)) = 0.5
		_NormalTex("Textura de normales", 2D) = "bump"{}
	}

	SubShader{
		CGPROGRAM

			#pragma surface surf Lambert alpha
			
			sampler2D _MainTex;
			sampler2D _NormalTex;
			float _TransVal;
			
			struct Input{
				float2 uv_MainTex;
				float2 uv_NormalTex;
			};

			void surf(Input IN, inout SurfaceOutput o){
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = 1;
				float3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
				o.Normal = normalMap.rgb;
			}

		ENDCG
	}
}