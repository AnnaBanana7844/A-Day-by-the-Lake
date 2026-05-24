using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    // Settings Data Members
   [SerializeField] public TMP_Dropdown Graphics;
    [SerializeField] public Slider MasterVolume, MusicVolume, SFXVolume;
    [SerializeField] public AudioMixer SettingsMixer;

    private const string masterVolumeKey = "SavedMasterVolume";

    //set minimum value for slider to dodge log0 errors
    private float minMasterVolume = 0.0001f;


    private void Start()
    {
        // Load saved values, defaulting to maximum volume (1.0) if no save exists
        MasterVolume.value = PlayerPrefs.GetFloat("SavedMasterVolume", 0f);
        MusicVolume.value = PlayerPrefs.GetFloat("SavedMusicVolume", 0f);
        SFXVolume.value = PlayerPrefs.GetFloat("SavedSFXVolume", 0f);

        // Force the mixer to update to the loaded values
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }


    //Graphics Quality Dropdown---T
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(Graphics.value);

    }


    //Different volume Sliders----T
  public void ChangeMasterVolume()
    {
        SetMixerVolume("Master", MasterVolume.value);
        PlayerPrefs.SetFloat("SavedMasterVolume", MasterVolume.value);
    }
  public void ChangeMusicVolume()
    {
        SetMixerVolume("Music", MusicVolume.value);
        PlayerPrefs.SetFloat("SavedMusicVolume", MusicVolume.value);
    }
  public void ChangeSFXVolume()
    {
        SetMixerVolume("SFX", SFXVolume.value);
        PlayerPrefs.SetFloat("SavedSFXVolume", SFXVolume.value);
    }


    private void SetMixerVolume(string name, float value)
    {
        float dbValue = Mathf.Log10(Mathf.Max(value, minMasterVolume)) * 20f;
        SettingsMixer.SetFloat(name, dbValue);

    }



}
