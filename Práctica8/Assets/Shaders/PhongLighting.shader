Shader "Diplomado/PhongLighting"
{
	Properties
	{
		_MainTex ("Textura Base", 2D) = "white" {}
		_TintColor("Color", Color) = (1,1,1,1)
		_SpecularColor("Color Especular", Color) = (1,1,1,1)
		_SpecPower("Potencia Especular", Range(0,1)) = 0.5
	}
	SubShader
	{
		CGPROGRAM

			#pragma surface surf BlinnPhong
			
			sampler2D _MainTex;
			float4 _TintColor;
			float4 _SpecularColor;
			float _SpecPower;

			struct Input{
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput io){
				half4 color = tex2D(_MainTex, IN.uv_MainTex) * _TintColor;
				io.Specular = _SpecPower;
				io.Gloss = 1.0;
				io.Albedo = color.rgb;
				io.Alpha = color.a;
			}

		ENDCG
	}

	Fallback "Diffuse"
}
