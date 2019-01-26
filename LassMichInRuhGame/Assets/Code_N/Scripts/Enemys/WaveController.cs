using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private float waveCooldown = 15;
    private float currentWaveCooldown;
    int currentwave = 0;

    [SerializeField]
    private WaveSetting[] waves;
    [SerializeField]
    private SpawnController[] spawners;
    [SerializeField]
    private Enemy neighbour;

    private void Update()
    {
        waveCooldown -= Time.deltaTime;
        if(waveCooldown <= 0)
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
        }

    }

}
