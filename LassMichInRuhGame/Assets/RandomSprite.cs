using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite randomSprite;
    public int chance = 100;

    private void Start()
    {
        if (Random.Range(0, chance) == 0)
        {
            GetComponent<SpriteRenderer>().sprite = randomSprite;
        }
    }
}
