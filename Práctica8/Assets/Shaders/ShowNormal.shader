Shader "Diplomado/ShowNormal" {
	
	SubShader {
		Pass{
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				struct appdata{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct v2f{
					float4 pos : SV_POSITION;
					fixed4 color : COLOR;
				};

				v2f vert(appdata v){
					v2f o;
					
					//mul es una operación de la gpu de multiplicación.
					//UNITY_MATRIX_MVP es la matriz de model, view y projection
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.color.rgb = v.normal * 0.5 + 0.5; //Pasamos del dominio del vector normal (-1, 1) al dominio de los color (0, 1)
					o.color.a = 1.0;
					return o;
				}

				fixed4 frag(v2f i) : COLOR{
					return i.color;
				}

			ENDCG
		}
	}

	FallBack "Diffuse"
}
