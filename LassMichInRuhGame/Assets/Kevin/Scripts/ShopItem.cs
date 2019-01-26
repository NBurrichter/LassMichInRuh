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

    private void Start()
    {
        icon.sprite = upgrade.icon;
        title.text = upgrade.name;
        description.text = upgrade.description;
        currentCost = upgrade.cost;
        MoneyController.instance.amountChanged.AddListener(MoneyChanged);
        MoneyChanged();
    }

    private void MoneyChanged()
    {
        buyText.text = $"Buy {currentCost}";
        buyButton.interactable = MoneyController.Amount >= upgrade.cost;
    }

    public void TriggerPurchase()
    {
        if (MoneyController.Amount >= upgrade.cost)
        {
            currentCost += upgrade.costIncrease;
            MoneyController.Amount -= upgrade.cost;
            onPurchase?.Invoke();
        }
    }
}
