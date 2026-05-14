using UnityEngine;

public class PlayerFishingController : MonoBehaviour
{
    public FishingSpot currentSpot;
    public FishProgressController fishingSystem;
    public GameObject pressEPrompt;

    public bool isFishing = false;

    void Update()
    {
        if (currentSpot != null && currentSpot.playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && isFishing == false)
            {
                startFishing();
            }
        }
    }

    void startFishing()
    {
        isFishing = true;

        if(pressEPrompt != null )
            pressEPrompt.SetActive(false);

        fishingSystem.startFishing();
    }

    void OnTriggerEnter(Collider other)
    {
        FishingSpot spot = other.GetComponent<FishingSpot>();
        if (spot != null)
            currentSpot = spot;
    }

    void OnTriggerExit(Collider other)
    {
        FishingSpot spot = other.GetComponent<FishingSpot>();
        if (spot != null && currentSpot == spot)
            currentSpot = null;
    }
}

