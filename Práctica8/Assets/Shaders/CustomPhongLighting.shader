Shader "Diplomado/CustomPhongLighting" {
	Properties {
		_MainTex ("Textura Base", 2D) = "white" {}
		_TintColor("Color", Color) = (1,1,1,1)
		_SpecularColor("Color Especular", Color) = (1,1,1,1)
		_SpecPower("Potencia Especular", Range(0,30)) = 1
	}
	SubShader {
		CGPROGRAM

			//Unity busca el modelo de iluminación dentro de sus variables, si no la encuentra lo buscará en nuestros scripts.
			#pragma surface surf MyPhong
			
			sampler2D _MainTex;
			float4 _TintColor;
			float4 _SpecularColor;
			float _SpecPower;

			struct Input{
				float2 uv_MainTex;
			};

			//Nuestro modelo de iluminación debe llamarse igual a como se declaró anteriormente antecediéndolo con Lighting
			//para que Unity lo reconozca como modelo de iluminación.
			inline fixed4 LightingMyPhong(SurfaceOutput s, fixed lightDir, half3 viewDir, fixed atten){
				float diff = dot(s.Normal, lightDir); //Diff de difusa
				float3 reflectionVector = normalize(2.0 * s.Normal * diff - lightDir); //El vector de reflección con respecto a la normal \|/
				float spec = pow(max(0, dot(reflectionVector, viewDir)), _SpecPower); //No queremos colores negativos, esto es una especie de clamp de 0 a Inf.
				float3 finalSpec = _SpecularColor * spec;
				fixed4 c;
				//_LightColor0 es una variable global de Unity, que indica el color de la Luz de la fuente.
				c.rgb = (s.Albedo * _LightColor0.rgb * 2.0 * diff * atten) + (_LightColor0.rgb * finalSpec); //Formulazo
				c.a = 1.0;
				return c;
			}

			void surf(Input IN, inout SurfaceOutput o){
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Specular = _SpecPower;
				o.Gloss = 1.0;
				o.Albedo = c.rgb * _TintColor.rgb;
				o.Alpha = c.a;
			}

			

		ENDCG
	}
	FallBack "Diffuse"
}
