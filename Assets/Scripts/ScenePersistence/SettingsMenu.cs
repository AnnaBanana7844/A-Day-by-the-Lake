using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    // Settings Data Members
    [SerializeField] public TMP_Dropdown Graphics;
    [SerializeField] public TMP_Dropdown Resolution;
    [SerializeField] public Slider MasterVolume, MusicVolume, SFXVolume, sensitivity, Brightness;
    [SerializeField] public Image BrightOverlay;
    [SerializeField] public Toggle FullscreenToggle;
    [SerializeField] public AudioMixer SettingsMixer;
    private Resolution[] resolutions;

    private const string masterVolumeKey = "SavedMasterVolume";

    //set minimum value for slider to dodge log0 errors
    private float minMasterVolume = -80f;


    private void Start()
    {
        // Load saved values, defaulting to maximum volume (1.0) if no save exists
        MasterVolume.value = PlayerPrefs.GetFloat("SavedMasterVolume", 0f);
        MusicVolume.value = PlayerPrefs.GetFloat("SavedMusicVolume", 0f);
        SFXVolume.value = PlayerPrefs.GetFloat("SavedSFXVolume", 0f);

        sensitivity.value = PlayerPrefs.GetFloat("Sensitivity", 10f);
        // Force the mixer to update to the loaded values

        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
        ChangeSensitivity();
        ChangeBrightness();
        SetFullscreen(FullscreenToggle.isOn);
    }

    private void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        Resolution.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRateRatio.value.ToString("F0") + "Hz";
            options.Add(option);

            // Check if this resolution matches the current system screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        Resolution.AddOptions(options);

        // Load saved resolution preference if it exists, otherwise use current index
        int savedResIndex = PlayerPrefs.GetInt("SavedResolutionIndex", currentResolutionIndex);
        Resolution.value = savedResIndex;
        Resolution.RefreshShownValue();

        // Hook up the function to run when the dropdown option changes
        Resolution.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("SavedResolutionIndex", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("SavedFullscreen", isFullscreen ? 1 : 0);
    }

    // Brightness control via UI overlay panel alpha inversion
    public void ChangeBrightness()
    {
        if (BrightOverlay != null)
        {
            // Slider value 1 = Full brightness (0 alpha overlay)
            // Slider value 0 = Dark screen (0.8 alpha overlay)
            float alphaValue = 1f - Brightness.value;
            Color overlayColor = BrightOverlay.color;
            overlayColor.a = Mathf.Clamp(alphaValue, 0f, 0.85f);
            BrightOverlay.color = overlayColor;
        }

        PlayerPrefs.SetFloat("SavedBrightness", Brightness.value);
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

    private void ChangeSensitivity()
    {

    }

}
