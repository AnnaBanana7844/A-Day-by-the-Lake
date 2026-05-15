using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown Graphics;
    public Slider MasterVolume, MusicVolume, SFXVolume;
    public AudioMixer SettingsMixer;

    //Graphics Quality Dropdown
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(Graphics.value);

    }


    //Different volume Sliders
  public void ChangeMasterVolume()
    {
        SettingsMixer.SetFloat("Master", MasterVolume.value); 
    }
  public void ChangeMusicVolume()
    {
        SettingsMixer.SetFloat("Music", MusicVolume.value); 
    }
  public void ChangeSFXVolume()
    {
        SettingsMixer.SetFloat("SFX", SFXVolume.value); 
    }



}
