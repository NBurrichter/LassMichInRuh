using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static int Amount
    {
        get { return instance.amount; }
        set { instance.amount = value; }
    }
    public int startingMoney = 0;
    int amount;
    static MoneyController instance;

    private void Awake()
    {
        amount = startingMoney;
        instance = this;
    }
}
