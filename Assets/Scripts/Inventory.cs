using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<FishItem> fishList = new List<FishItem>();

    void Awake()
    {
        instance = this;
    }

    public void AddFish(FishItem fish)
    {
        fishList.Add(fish);
        InventoryUI.instance.refreshUI();
    }
}
