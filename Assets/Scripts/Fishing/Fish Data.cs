using UnityEngine;

public enum FishRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical
}

[System.Serializable]
public class FishData
{
    public string fishName;
    public FishRarity rarity;
    public float minZoneSize;
    public float maxZoneSize;
    public float fishValue;
}
