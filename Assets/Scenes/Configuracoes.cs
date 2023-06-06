using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Configuracoes : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Dropdown resolucoes_dropdown;
    Resolution[] resolution;

    public void Start()
    {
        resolution = Screen.resolutions;
        resolucoes_dropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + "x" + resolution[i].height;
            options.Add(option);
            if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolucoes_dropdown.AddOptions(options);
        resolucoes_dropdown.value = currentResolutionIndex;
        resolucoes_dropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolucoes = resolution[resolutionIndex];
        Screen.SetResolution(resolucoes.width, resolucoes.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
