// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Diplomado/ColorMesh" {
	
	Properties{
		_Color1("Color arriba del plano XZ", Color) = (1,1,1,1)
		_Color2("Color abajo del plano XZ", Color) = (1,1,1,1)
	}

	SubShader {
		Pass{
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				float4 _Color1;
				float4 _Color2;

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
					float4 auxPos;
					
					//mul es una operación de la gpu de multiplicación.
					//UNITY_MATRIX_MVP es la matriz de model, view y projection
					//o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					//o.pos = mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, v.vertex));
					//auxPos = o.pos;
					auxPos = mul(unity_ObjectToWorld, v.vertex);
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

					if(auxPos.y > 0){
						o.color.rgb = _Color1.rgb;
					}else{
						o.color.rgb = _Color2.rgb;
					}
					//o.color.rgb = v.normal * 0.5 + 0.5; //Pasamos del dominio del vector normal (-1, 1) al dominio de los color (0, 1)
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
