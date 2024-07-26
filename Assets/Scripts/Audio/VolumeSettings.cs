using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sFXSlider;
    

    public void Initialize()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
            LoadMusicVolumeData();
        else
            SetMusicVolume();
        
        if (PlayerPrefs.HasKey("sfxVolume"))
            LoadSFXVolumeData();
        else
            SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sFXSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadMusicVolumeData()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }

    private void LoadSFXVolumeData()
    {
        sFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetSFXVolume();
    }

}
