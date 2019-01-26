﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellDude : MonoBehaviour
{
    [SerializeField]
    private float annoydelay = 5;
    [SerializeField]
    private float annoy = 1;

    void Update()
    {
        annoydelay -= Time.deltaTime;
        if (annoydelay <= 0)
        {
            annoydelay = 3;
            SanityController.instance.RemoveSanity(annoy);
        }
    }

}
