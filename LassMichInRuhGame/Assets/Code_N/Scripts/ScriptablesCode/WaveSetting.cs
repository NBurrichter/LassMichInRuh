using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSetting", menuName = "ScriptableObjects/WaveSetting", order = 1)]
public class WaveSetting : ScriptableObject
{
    public int[] spawnerToActivate;
    public float waitTime = 1;
    public bool neighbour = false;
}
