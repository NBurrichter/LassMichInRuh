using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SanityController : MonoBehaviour
{
    public static SanityController instance { get; private set; }
    public float maxSanity = 100;
    public UnityEvent OnInsane;
    float sanity;

    private void Awake()
    {
        instance = this;
        sanity = maxSanity;
    }

    public void AddSanity(float amount)
    {
        sanity = Mathf.Max(sanity + amount, maxSanity);
    }

    public void RemoveSanity(float amount)
    {
        sanity -= amount;
        if (sanity <= 0)
        {
            sanity = 0;
            OnInsane.Invoke();
        }
    }
}
