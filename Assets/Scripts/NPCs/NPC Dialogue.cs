using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject shopPanel;
    public NPCShop shop;
    public void openDialogue()
    {
        dialoguePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CameraControl.uiOpen = true;
        PlayerController.uiOpen = true;
    }

    public void closeAll()
    {
        dialoguePanel.SetActive(false);
        shopPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CameraControl.uiOpen = false;
        PlayerController.uiOpen = false;
    }

    public void onTalkButton()
    {
        Debug.Log("NPC: Hello");
    }

    public void onShopButton()
    {
        dialoguePanel.SetActive(false);
        shopPanel.SetActive(true);
        shop.openShop();
    }

    public void onLeaveButton()
    {
        closeAll();
    }
}