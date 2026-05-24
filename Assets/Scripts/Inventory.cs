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

    public void addFish(FishItem fish)
    {
        fishList.Add(fish);
        InventoryUI.instance.refreshUI();
    }

    public void removeFish(FishItem fish)
    {
        if (fishList.Contains(fish))
        {
            fishList.Remove(fish);
            InventoryUI.instance.refreshUI();
        }

    }
}
