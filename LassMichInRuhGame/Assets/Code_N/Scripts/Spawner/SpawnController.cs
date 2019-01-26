using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private float spawnCooldown = 5;
    private float spawnTimeReduction = 0.95f;
    [SerializeField]
    private float minRandomTime = 0.5f;
    [SerializeField]
    private float maxRandomTime = 1.5f;
    private float currentCooldown = 0;
    [SerializeField]
    private int repeats = 1;
    private int repeatCounter = 0;
    [SerializeField]
    private float repeatTime = 0.5f;

    void Start()
    {
        GetNewCooldown();
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            Instantiate(spawnObject, transform.position, Quaternion.Euler(0, 0, 0));
            GetNewCooldown();
        }
    }

    private void GetNewCooldown()
    {
        currentCooldown = Random.Range(minRandomTime, maxRandomTime) + spawnCooldown;
        spawnCooldown *= spawnTimeReduction;
        minRandomTime *= spawnTimeReduction;
        maxRandomTime *= spawnTimeReduction;
    }
}
