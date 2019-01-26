using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePool : MonoBehaviour
{
    public List<GameObject> pool;
    int currentIndex = 0;

    public void Upgrade()
    {
        if (pool.Count <= currentIndex)
        {
            return;
        }

        pool[currentIndex].SetActive(true);
        currentIndex++;
    }
}
