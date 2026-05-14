using UnityEngine;
using TMPro;

public class FishingSpot : MonoBehaviour
{
    public bool playerInRange = false;
    [SerializeField] TMP_Text fishText;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            playerInRange = true;
            fishText.text = "Press 'E' to fish";

    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            playerInRange = false;
        fishText.text = "";
    }
}
