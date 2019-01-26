using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyController : MonoBehaviour
{
    public static int Amount
    {
        get { return instance?.amount ?? 0; }
        set
        {
            if (instance == null) return;
            instance.amount = value;
            instance.amountChanged?.Invoke();
        }
    }
    public int startingMoney = 0;
    public UnityEvent amountChanged;
    int amount;
    public static MoneyController instance { get; set; }

    private void Awake()
    {
        amount = startingMoney;
        instance = this;
    }
}
