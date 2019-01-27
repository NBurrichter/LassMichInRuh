using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private GameObject altSpawn;
    [SerializeField]
    private float altChance = 0.5f;
    [SerializeField]
    private float spawnCooldown = 5;
    [SerializeField]
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

    [SerializeField]
    private Transform firstWaypoint;

    public bool disabled = true;

    void Start()
    {
        GetNewCooldown();
    }

    void Update()
    {
        if (disabled)
            return;

        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0)
        {
            repeatCounter = 0;
            SpawnCharAlt();
            GetNewCooldown();
        }
    }

    private void GetNewCooldown()
    {
        currentCooldown = Random.Range(minRandomTime, maxRandomTime) + spawnCooldown;
        spawnCooldown *= spawnTimeReduction;
        minRandomTime *= spawnTimeReduction;
        maxRandomTime *= spawnTimeReduction;

        if(spawnCooldown < 1)
        {
            spawnCooldown = 1;
        }
    }

    public void SpawnChar()
    {
        if (Random.Range(0.0f, 1.0f) > altChance && altSpawn != null)
        {
            GameObject enemy = Instantiate(altSpawn, transform.position, Quaternion.Euler(0, 0, 0));
            enemy.GetComponent<EnemyController>().SetWalkTarget(firstWaypoint);
        }
        else
        {
            GameObject enemy = Instantiate(spawnObject, transform.position, Quaternion.Euler(0, 0, 0));
            enemy.GetComponent<EnemyController>().SetWalkTarget(firstWaypoint);
        }
        /*repeatCounter++;
        if(repeatCounter >= repeats)
        {
            CancelInvoke();
        }*/
    }

    void SpawnCharAlt()
    {
        Instantiate(spawnObject, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
