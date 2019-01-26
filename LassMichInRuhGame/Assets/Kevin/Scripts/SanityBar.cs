using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class

public class SanityBar : MonoBehaviour
{
    public RectTransform maskBar;
    public Image bar;
    public Image smiley;
    public Color fine;
    public Color ok;
    public Color annoyed;
    public Color critical;
    public Sprite fineSprite;
    public Sprite okSprite;
    public Sprite annoyedSprite;
    public Sprite criticalSprite;
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
            bar.color = critical;
            smiley.sprite = criticalSprite;
        }
        else if (currentDisplay < 0.5f)
        {
            bar.color = annoyed;
            smiley.sprite = annoyedSprite;
        }
        else if (currentDisplay < 0.75f)
        {
            bar.color = ok;
            smiley.sprite = okSprite;
        }
        else
        {
            bar.color = fine;
            smiley.sprite = fineSprite;
        }
        var anchorMax = maskBar.anchorMax;
        anchorMax.y = currentDisplay;
        maskBar.anchorMax = anchorMax;
    }
}
