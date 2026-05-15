using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FishProgressController : MonoBehaviour
{
    public GameObject skillCheckUI;

    public AudioSource audioSource;

    [Header("UI")]
    public Slider progressBar;

    [Header("Sound Effects")]
    public AudioClip skillCheckSuccessSound;
    public AudioClip skillCheckFailSound;
    public AudioClip fishCaughtSound;
    public AudioClip fishLostSound;
    public AudioClip mythicalFishCatchSound;

    [Header("Settings")]
    public float startingProgress = 0f;
    public float successGain = 10f;
    public float failLoss = 15f;

    [Header("References")]
    public SkillCheckController skillCheck;

    [Header("Catch UI")]
    public GameObject caughtFishUI;
    public TMPro.TextMeshProUGUI caughtFishText;

    [Header("Fish Table")]
    public FishData[] fishTable;

    private float progress = 20f;
    private FishData currentFish;

    public void startFishing()
    {
        audioSource = GetComponent<AudioSource>();

        currentFish = rollFish();
        
        skillCheckUI.SetActive(true);


        progress = startingProgress;
        progressBar.value = startingProgress;
        skillCheck.resetPointerSpeed();

        triggerNextSkillCheck();
    }

    public void skillCheckSuccess()
    {
        audioSource.PlayOneShot(skillCheckSuccessSound);
        progress += successGain;
        progress = Mathf.Clamp(progress, 0, 100);
        progressBar.value = progress;

        if (progressBar.value >= 100)
        {
            fishCaught();
        }
        else
        {
            triggerNextSkillCheck();
        }
    }

    public void skillCheckFail()
    {
        audioSource.PlayOneShot(skillCheckFailSound);
        progress -= failLoss;
        progress = Mathf.Clamp(progress, 0, 100);
        progressBar.value = progress;

        if (progressBar.value <= 0)
        {
            fishEscaped();
        }
        else
        {
            triggerNextSkillCheck();
        }
    }

    public void triggerNextSkillCheck()
    {


        float zoneSize = Random.Range(currentFish.minZoneSize, currentFish.maxZoneSize);
        float zonePos = Random.Range(0f, 360f);

        skillCheck.startSkillCheck(zoneSize, zonePos);
    }

    private void fishCaught()
    {
        caughtFishUI.SetActive(true);
        caughtFishText.text = "You caught a " + currentFish.fishName + "!";

        if(currentFish.rarity == FishRarity.Mythical)
        {
            audioSource.PlayOneShot(mythicalFishCatchSound);
        }
        else
        {
            audioSource.PlayOneShot(fishCaughtSound);
        }
            
        skillCheckUI.SetActive(false);

        var player = FindFirstObjectByType<PlayerFishingController>();
        player.isFishing = false;

        if (player.pressEPrompt != null)
            player.pressEPrompt.SetActive(true);

        skillCheck.stopSkillCheck();

        StartCoroutine(hideCaughtUI());
    }

    private void fishEscaped()
    {
        caughtFishUI.SetActive(true);
        caughtFishText.text = "The fish escaped!";

        audioSource.PlayOneShot(fishLostSound);
        skillCheckUI.SetActive(false);

        var player = FindFirstObjectByType<PlayerFishingController>();
        player.isFishing = false;

        if (player.pressEPrompt != null)
            player.pressEPrompt.SetActive(true);

        skillCheck.stopSkillCheck();

        StartCoroutine(hideCaughtUI());
    }

    private FishData rollFish()
    {
        int roll = Random.Range(0, 100);
        
        if (roll < 40) return getFish(FishRarity.Common);
        if (roll < 70) return getFish(FishRarity.Uncommon);
        if (roll < 85) return getFish(FishRarity.Rare);
        if (roll < 95) return getFish(FishRarity.Epic);
        if (roll < 99) return getFish(FishRarity.Legendary);
        return getFish(FishRarity.Mythical);
    }

   private FishData getFish(FishRarity rarity)
    {
        foreach(FishData fish in fishTable)
        {
            if(fish.rarity == rarity)
                return fish;
        }
        return null;
    }

    private IEnumerator hideCaughtUI()
    {
        yield return new WaitForSeconds(3f);
        caughtFishUI.SetActive(false);
    }
}
    