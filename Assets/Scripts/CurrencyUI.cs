using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public static CurrencyUI instance;

    public GameObject currencyPanel;
    public TMP_Text moneyText;

    private void Awake()
    {
        instance = this;
    }

    public void updateMoney(int amount)
    {
        moneyText.text = "$" + amount;
    }

    public void showMoney()
    {
        currencyPanel.SetActive(true);
        updateMoney(PlayerCurrency.instance.money);
    }

    public void hideMoney()
    {
        currencyPanel.SetActive(false);
    }
}
