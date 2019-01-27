using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveLabel : MonoBehaviour
{
    public WaveController controller;
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();    
    }

    private void OnEnable()
    {
        text.text = $"Wave {controller.GetWave()}";
        if (SanityController.instance.Sanity > 0)
        {
            text.enabled = true;
            StartCoroutine(Animation());
        }
        else
        {
            text.enabled = false;
        }
    }

    IEnumerator Animation()
    {
        const float duration = 1f;
        var start = Time.time;
        var startScale = transform.localScale = new Vector3(1, 1, 1);
        var startColor = text.color = Color.white;
        while (Time.time - start < duration)
        {
            var lerp = (Time.time - start) / duration;
            transform.localScale = Vector3.Lerp(startScale, new Vector3(1.5f, 1.5f, 1.5f), lerp);
            text.color = Color.Lerp(startColor, new Color(1, 1, 1, 0f), lerp);

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
