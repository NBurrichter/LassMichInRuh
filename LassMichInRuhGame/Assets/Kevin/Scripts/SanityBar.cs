using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class

public class SanityBar : MonoBehaviour
{
    public RectTransform maskBar;
    public Image bar;
    float currentDisplay;
    float velocity;

    private void Start()
    {
        currentDisplay = 1;
    }

    public void Update()
    {
        var percent = SanityController.instance.Sanity / SanityController.instance.maxSanity;

        currentDisplay = Mathf.SmoothDamp(currentDisplay, percent, ref velocity, 0.1f);

        if (currentDisplay < 0.2f)
        {
            //curre
        }
        var anchorMax = maskBar.anchorMax;
        anchorMax.y = currentDisplay;
        maskBar.anchorMax = anchorMax;
    }
}
