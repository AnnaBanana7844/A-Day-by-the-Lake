using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject pressEPrompt;
    public ShopInventory shopInventory;

    private bool playerInRange = false;

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
            ShopMenuUI.instance.closeShop();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            pressEPrompt.SetActive(false);
            ShopMenuUI.instance.openShop(shopInventory);
        }
    }
}
