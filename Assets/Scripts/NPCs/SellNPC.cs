using UnityEngine;

public class SellNPC : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject pressEPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            pressEPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            pressEPrompt.SetActive(false);
            SellMenuUI.instance.CloseSellMenu();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            pressEPrompt.SetActive(false);
            SellMenuUI.instance.OpenSellMenu();
        }
    }
}
