using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public GameObject inventoryPanel;
    public Transform contentParent;
    public GameObject fishSlotPrefab;
    public GameObject text;

    void Awake()
    {
        instance = this;
    }

    public void refreshUI()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (FishItem fish in Inventory.instance.fishList)
        {
            GameObject slot = Instantiate(fishSlotPrefab, contentParent);
            slot.transform.Find("Name").GetComponent<TMP_Text>().text = fish.fishName;
        }
    }

    public void toggleInventory()
    {
        bool active = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(active);

        Player.uiOpen = active;
        cameraController.uiOpen = active;
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;

        if (active)
            refreshUI();
    }
}
