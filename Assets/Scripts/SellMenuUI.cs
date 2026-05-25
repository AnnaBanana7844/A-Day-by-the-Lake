
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
        moneyText.text = "$" + PlayerCurrency.instance.money;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.uiOpen = true;
        cameraController.uiOpen = true;

        refreshUI();
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

    public void refreshUI()
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
                sellFish(fish);
            });
        }
    }

    public void sellFish(FishItem fish)
    {
        PlayerCurrency.instance.addMoney(fish.value);
        Inventory.instance.removeFish(fish);
        moneyText.text = "$" + PlayerCurrency.instance.money;
        refreshUI();
    }
}
