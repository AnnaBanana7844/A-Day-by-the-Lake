using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenuUI : MonoBehaviour
{
    public static ShopMenuUI instance;

    public GameObject shopMenuPanel;
    public Transform shopContentParent;
    public GameObject shopSlotPrefab;

    private ShopInventory currentShop;

    private void Awake()
    {
        instance = this;
    }

    public void OpenShop(ShopInventory shop)
    {
        currentShop = shop;
        RefreshUI();
        shopMenuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.uiOpen = true;
    }

    public void CloseShop()
    {
        shopMenuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.uiOpen = false;
    }

    public void RefreshUI()
    {
        foreach (Transform child in shopContentParent)
            Destroy(child.gameObject);

        foreach (ShopItem item in currentShop.itemsForSale)
        {
            GameObject slot = Instantiate(shopSlotPrefab, shopContentParent);

            slot.transform.Find("Name").GetComponent<TMP_Text>().text = item.itemName;
            slot.transform.Find("Price").GetComponent<TMP_Text>().text = "$" + item.price;

            slot.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                TryBuyItem(item);
            });
        }
    }

    private void TryBuyItem(ShopItem item)
    {
        if (PlayerCurrency.instance.money >= item.price)
        {
            PlayerCurrency.instance.money -= item.price;

            Debug.Log("Bought: " + item.itemName);

            // Later: give the item to the player
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
