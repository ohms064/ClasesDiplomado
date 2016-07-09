// shadertype=unity
Shader "Diplomado/FirstShader" {//El nombre del shader, la diagonal crea jerarquías en el editor

	Properties{
		//Aquí deben los inputs del editor
		
		_MainTex("Textura RGB", 2D) = "white"{}
		_Color("Color base", Color) = (1, 1, 1, 1)
		//<Nombre de la variable en el shader> (<Nombre de la variable en el inspector>, <tipo>) = <inicialización>.

		
	}

	SubShader{
		//Aquí se escribe el código del shader.
		 
			Tags{"Render Type" = "Opaque"}
			//Indica el tipo de rendering que tendra este shader

			Pass{
				//La pasada del shader, se pueden tener n cantidad de Pass
				Material {
					Diffuse[_Color] 
				}
				Lighting On
				SetTexture[_MainTex]{
					combine previous * texture
				}
			}
	}

	Fallback "Diffuse" //En caso que el shader no pueda ser ejecutado en alguna tarjeta gráfica.
}