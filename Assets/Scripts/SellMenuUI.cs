
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellMenuUI : MonoBehaviour
{
    public static SellMenuUI instance;

    public GameObject sellMenuPanel;
    public Transform sellContentParent;
    public GameObject sellSlotPrefab;
    public TMP_Text moneyText;

    private void Awake()
    {
        instance = this;
    }

    public void OpenSellMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.uiOpen = true;
        cameraController.uiOpen = true;

        RefreshUI();
        sellMenuPanel.SetActive(true);
    }

    public void CloseSellMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.uiOpen = false;
        cameraController.uiOpen = false;
        sellMenuPanel.SetActive(false);
    }

    public void RefreshUI()
    {
        foreach (Transform child in sellContentParent)
            Destroy(child.gameObject);

        foreach (FishItem fish in Inventory.instance.fishList)
        {
            GameObject slot = Instantiate(sellSlotPrefab, sellContentParent);

            slot.transform.Find("Name").GetComponent<TMP_Text>().text = fish.fishName;
            slot.transform.Find("Price").GetComponent<TMP_Text>().text = "$" + fish.value;

           
            slot.GetComponent<Button>().onClick.AddListener(() =>
            {
                SellFish(fish);
            });
        }
    }

    public void SellFish(FishItem fish)
    {
        PlayerCurrency.instance.addMoney(fish.value);
        Inventory.instance.removeFish(fish);
        moneyText.text = "$" + PlayerCurrency.instance.money;
        RefreshUI();
    }
}
