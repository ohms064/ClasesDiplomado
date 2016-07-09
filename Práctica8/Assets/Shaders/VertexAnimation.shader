Shader "Diplomado/VertexAnimation"
{
	Properties
	{
		_MainTex ("Textura Base", 2D) = "white" {}
		_Speed("Wave Speed", Range(0.1, 80)) = 5
		_Frequency("Frecuencia", Range(0,5)) = 2
		_Amplitude("Amplitud", Range(-1, 1)) = 1

	}
	SubShader
{
		CGPROGRAM

			#pragma surface surf Lambert vertex:vert

			sampler2D _MainTex;
			float _Speed;
			float _Frequency;
			float _Amplitude;

			struct Input{
				float2 uv_MainTex;
				float3 vertColor;
			};

			void vert(inout appdata_full v, out Input o){
				UNITY_INITIALIZE_OUTPUT(Input, o);
				//_Time es una variable de cg que indica el tiempo de nuestra aplicación.
				float time = _Time * _Speed;
				float wave = sin(time + v.vertex.x * _Frequency) * _Amplitude;
				v.vertex.xyz = float3(v.vertex.x + wave, v.vertex.y, v.vertex.z);
				//Al modificar un vértice se debe recalcular la normal y las uv's.

				v.texcoord.xy = float2(v.texcoord.x * wave, v.texcoord.y);
				v.normal.xyz = normalize(float3(v.normal.x + wave, v.normal.y, v.normal.z));
				o.vertColor = float3(1,1,1);
			}

			void surf(Input IN, inout SurfaceOutput o){
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}

		ENDCG
	}

	Fallback "Diffuse"
}
