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

    private void Start()
    {
        icon.sprite = upgrade.icon;
        title.text = upgrade.name;
        description.text = upgrade.description;
        buyText.text = $"Buy {upgrade.cost}";
        buyButton.interactable = MoneyController.Amount >= upgrade.cost;
    }

    public void TriggerPurchase()
    {
        if (MoneyController.Amount >= upgrade.cost)
        {
            MoneyController.Amount -= upgrade.cost;
            onPurchase?.Invoke();
        }
    }
}
