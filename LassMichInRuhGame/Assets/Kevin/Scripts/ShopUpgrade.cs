﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Shop Upgrade")]
public class ShopUpgrade : ScriptableObject
{
    public Sprite icon;
    public string description;
    public int cost;
    public int costIncrease;
    public int maxUpgrades;
}
