using UnityEngine;
using System.Collections;

public class NightVisionImageEffect : MonoBehaviour 
{
	public Shader nightVisionShader;
	[Range(0.0f, 4.0f)] public float contrast= 2.0f;
	[Range(0.0f, 2.0f)]public float brightness= 1.0f;
	public Color nightVisionColor = Color.white;
	public Texture2D vignetteTexture;
	public Texture2D scanLineTexture;
	public float scanLineAmount;
	public Texture2D nightVisionNoise;
	public float noiseXSpeed = 100.0f;
	public float noiseYSpeed= 100.0f;
	[Range(-1.0f, 1.0f)] public float distorsion = 0.2f;
	[Range(0.0f, 3.0f)] public float scale= 0.8f;
	private float randomValue = 0.0f;
	private Material currentMaterial;

	Material material
	{
		get
		{
			if (currentMaterial == null) 
			{				
				currentMaterial = new Material (nightVisionShader);
				currentMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return currentMaterial;
		}
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (nightVisionShader != null) 
		{
			material.SetFloat ("_Contrast", contrast);
			material.SetFloat ("_Brightness", brightness);
			material.SetColor ("_NightVisionColor", nightVisionColor);
			material.SetFloat ("_RandomValue", randomValue);
			material.SetFloat ("_distortion", distorsion);
			material.SetFloat ("_scale", scale);
			if (vignetteTexture)
				material.SetTexture ("_VignetteTex",vignetteTexture);
			if (scanLineTexture) 
				material.SetTexture ("_ScanLineTex", scanLineTexture);
			material.SetFloat ("_ScanLineTileAmount", scanLineAmount);
			if (nightVisionNoise) 
			{
				material.SetTexture ("_NoiseTex", nightVisionNoise);
				material.SetFloat ("_NoiseXSpeed", noiseXSpeed);
				material.SetFloat ("_NoiseYSpeed", noiseYSpeed);
			}
			Graphics.Blit (sourceTexture,destTexture,material);
		}
		else
			Graphics.Blit (sourceTexture,destTexture);
	}

	void Update()
	{
		randomValue = Random.Range (-1f,1f);
	}
}
