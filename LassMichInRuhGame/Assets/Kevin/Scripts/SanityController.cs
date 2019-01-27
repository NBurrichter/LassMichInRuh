using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SanityController : MonoBehaviour
{
    public static SanityController instance { get; private set; }
    public float maxSanity = 100;
    public UnityEvent OnInsane;
    public float Sanity => sanity;
    public float TimeSane => Sanity > 0 ? Time.time - startTime : deathTime - startTime;
    float sanity;
    float startTime;
    float deathTime;

    private void Awake()
    {
        instance = this;
        sanity = maxSanity;
        startTime = Time.time;
    }

    public void AddSanity(float amount)
    {
        sanity = Mathf.Min(sanity + amount, maxSanity);
    }

    public void RemoveSanity(float amount)
    {
        sanity -= amount;
        if (sanity <= 0)
        {
            sanity = 0;
            deathTime = Time.time;
            OnInsane.Invoke();
        }
    }
}
