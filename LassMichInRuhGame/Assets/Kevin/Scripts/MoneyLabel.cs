using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyLabel : MonoBehaviour
{
    Text text;
    float currentAmount;
    float velocity;

    void Start()
    {
        text = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        currentAmount = Mathf.SmoothDamp(currentAmount, MoneyController.Amount, ref velocity, 0.2f);
        if (Mathf.Abs(currentAmount - MoneyController.Amount) < 3)
        {
            currentAmount = MoneyController.Amount;
        }
        text.text = Mathf.RoundToInt(currentAmount).ToString();
    }
}
