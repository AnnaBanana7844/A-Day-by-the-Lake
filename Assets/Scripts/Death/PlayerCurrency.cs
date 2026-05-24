using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public static PlayerCurrency instance;
    public int money = 0;

    private void Awake()
    {
        instance = this;
    }

    public void addMoney(int amount)
    {
        money += amount;
        Debug.Log("Money = " + money);
    }
}
