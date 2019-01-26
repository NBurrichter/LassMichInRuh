using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WaveController : MonoBehaviour
{
    private float waveCooldown = 5;
    private float currentWaveCooldown;
    int currentwave = 0;

    [SerializeField]
    private WaveSetting[] waves;
    [SerializeField]
    private SpawnController[] spawners;
    [SerializeField]
    private Enemy neighbour;

    public UnityEvent newWave;

    private void Update()
    {
        waveCooldown -= Time.deltaTime;
        Debug.Log(currentwave);
        if (waveCooldown <= 0)
        {
            foreach(int spawnerNumber in waves[currentwave].spawnerToActivate)
            {
                spawners[spawnerNumber].SpawnChar();
            }
            if(waves[currentwave].neighbour)
            {
                neighbour.Reset();
            }

            currentwave++;
            if(currentwave >= waves.Length)
            {
                currentwave = waves.Length - 1;
            }
            waveCooldown = waves[currentwave].waitTime;
            newWave?.Invoke();
        }

    }

    public int GetWave() => currentwave;

}
