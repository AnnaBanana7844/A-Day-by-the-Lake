using UnityEngine;



[CreateAssetMenu]


public class gunStats : ScriptableObject
{
    public GameObject gunModel;

   [Range(1,10)] public int shootDamage;
    [Range(3, 1000)] public int shootDistance;
    [Range(.1f, 2f)] public float shootRate;
    public string gunName; // Make sure this matches your ShopItem itemName!
    public int buyPrice;
    public int sellPrice;

    public int ammoCur;
    [Range(5, 50)] public int ammoMax;

    public ParticleSystem hitEffect;
    public AudioClip[] shootSound;
    [Range(0, 1)] public float shootSoundVol;

}
