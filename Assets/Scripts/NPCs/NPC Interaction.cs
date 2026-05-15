using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject pressEPrompt;
    public NPCDialogue dialogueUI;

    private bool playerInRange = false;
    void Update()
    {
        if(playerInRange && Input.GetKeyUp(KeyCode.E))
        {
            dialogueUI.openDialogue();
            pressEPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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
        }
    }
}
