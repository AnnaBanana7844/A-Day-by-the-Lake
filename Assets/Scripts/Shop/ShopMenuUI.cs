using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenuUI : MonoBehaviour
{
    public static ShopMenuUI instance;

    public GameObject shopMenuPanel;
    public Transform shopContentParent;
    public GameObject shopSlotPrefab;
    public TMP_Text moneyText;

    private ShopInventory currentShop;

    private void Awake()
    {
        instance = this;
    }

    public void openShop(ShopInventory shop)
    {
        moneyText.text = "$" + PlayerCurrency.instance.money;
        currentShop = shop;
        refreshUI();
        shopMenuPanel.SetActive(true);

        cameraController.uiOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.uiOpen = true;
    }

    public void closeShop()
    {
        shopMenuPanel.SetActive(false);

        cameraController.uiOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.uiOpen = false;
    }

    public void refreshUI()
    {
        foreach (Transform child in shopContentParent)
            Destroy(child.gameObject);

        foreach (ShopItem item in currentShop.itemsForSale)
        {
            GameObject slot = Instantiate(shopSlotPrefab, shopContentParent);

            slot.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
            slot.transform.Find("Price").GetComponent<TMP_Text>().text = "$" + item.price;

            slot.GetComponent<Button>().onClick.AddListener(() =>
            {
                tryBuyItem(item);
            });
        }
    }

    private void tryBuyItem(ShopItem item)
    {
        if (PlayerCurrency.instance.money >= item.price)
        {
            PlayerCurrency.instance.money -= item.price;
            moneyText.text = "$" + PlayerCurrency.instance.money;

            Debug.Log("Bought: " + item.itemName);

            // give the item to the player
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
