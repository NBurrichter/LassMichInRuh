using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour
{
    public ShopUpgrade upgrade;
    public Image icon;
    public Text title;
    public Text description;
    public Button buyButton;
    public Text buyText;

    public UnityEvent onPurchase;

    int currentCost;
    int upgradesLeft;

    private void Start()
    {
        icon.sprite = upgrade.icon;
        title.text = upgrade.name;
        description.text = upgrade.description;
        currentCost = upgrade.cost;
        upgradesLeft = upgrade.maxUpgrades;
        MoneyController.instance.amountChanged.AddListener(MoneyChanged);
        MoneyChanged();
    }

    private void MoneyChanged()
    {
        buyText.text = $"Buy {currentCost}";
        if (upgradesLeft <= 0)
        {
            buyButton.interactable = false;
            buyText.text = "SOLD OUT";
            buyText.color = Color.red;
            icon.color = Color.grey;
            return;
        }
        else
        {
            buyButton.interactable = MoneyController.Amount >= currentCost;
            buyText.color = buyButton.interactable ? Color.black : Color.gray;
        }
        
    }

    public void TriggerPurchase()
    {
        if (MoneyController.Amount >= currentCost)
        {
            var cost = currentCost;
            currentCost += upgrade.costIncrease;
            upgradesLeft--;
            MoneyController.Amount -= cost;
            onPurchase?.Invoke();
        }
    }
}
