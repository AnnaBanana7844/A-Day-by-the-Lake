using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    // Settings Data Members
    public TMP_Dropdown Graphics;
    public Slider MasterVolume, MusicVolume, SFXVolume;
    public AudioMixer SettingsMixer;

    //Fishing Sound Effects----R




    //Graphics Quality Dropdown---T
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(Graphics.value);

    }


    //Different volume Sliders----T
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
