using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityBar : MonoBehaviour
{
    public RectTransform maskBar;

    public void Update()
    {
        var percent = SanityController.instance.Sanity / SanityController.instance.maxSanity;

        var anchorMax = maskBar.anchorMax;
        anchorMax.y = percent;
        maskBar.anchorMax = anchorMax;
    }
}
