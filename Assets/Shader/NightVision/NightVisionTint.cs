using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVisionTint : MonoBehaviour
{
    public Shader postShader;
    public Material postEffectMaterial;

    public Color screenTint;

    public float LuminosityMP;
    public float LuminosityIntensity;
    public float NightVisionEntensity;

    public RenderTexture finalPostRenderTexture;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (postEffectMaterial == null)
        {
            postEffectMaterial = new Material(postShader);
        }
        if (finalPostRenderTexture == null)
        {
            finalPostRenderTexture = new RenderTexture(source.width, source.height, 0, source.format);
        }

        postEffectMaterial.SetColor("_ScreenTint", screenTint);

        postEffectMaterial.SetFloat("_LuminosityMP", LuminosityMP);
        postEffectMaterial.SetFloat("_LuminosityIntensity", LuminosityIntensity);
        postEffectMaterial.SetFloat("_NightVisionEntensity", NightVisionEntensity);

        int width = source.width;
        int height = source.height;

        RenderTexture startRenderTexture = RenderTexture.GetTemporary(width, height, 0, source.format);
        Graphics.Blit(source, startRenderTexture, postEffectMaterial, 0);
        Graphics.Blit(startRenderTexture, destination);
        RenderTexture.ReleaseTemporary(startRenderTexture);

    }
}
