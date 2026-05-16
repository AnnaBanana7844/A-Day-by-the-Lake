using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public ShopItem[] itemsForSale;

    public void openShop()
    {
        // Shop
    }



    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public int price;
    }
}
