using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancerRainbowText : MonoBehaviour
{
    public float interval = 1;
    public List<Color> rainbow;
    Text text;
    int index;

    public void Start()
    {
        text = GetComponent<Text>();
        text.color = GetNextColor();
        StartCoroutine(CancerRainbow());
    }

    IEnumerator CancerRainbow()
    {
        
        while (true)
        {
            var start = text.color;
            var target = GetNextColor();
            var t = Time.time;
            while (Time.time - t < interval)
            {
                var passed = Time.time - t;
                text.color = Color.Lerp(start, target, passed / interval);
                text.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(1.3f, 1.3f, 1.3f), Mathf.PingPong(Time.time*2, 1));
                yield return null;
            }
            text.color = target;
        }
    }

    Color GetNextColor()
    {
        var color = rainbow[index];
        index = (index + 1) % rainbow.Count;
        return color;
    }
}
