using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private float poopTime = 5;
    private float currentPoopTime;

    [SerializeField]
    private GameObject poop;

    private void Start()
    {
        currentPoopTime = poopTime + Random.Range(1,3);
    }

    private void Update()
    {
        currentPoopTime -= Time.deltaTime;
        if(currentPoopTime <= 0)
        {
            currentPoopTime = poopTime + Random.Range(1, 3);
            Instantiate(poop, transform.position, poop.transform.rotation);
        }
    }

}
