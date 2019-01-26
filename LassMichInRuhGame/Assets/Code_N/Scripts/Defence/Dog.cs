﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Transform anchor;
    public float anchorDistance;
    public int randomDelay = 100;
    public float attackDistance;
    public float loseDistance;
    public Enemy target;
    public float movespeed;
    private bool hasTarget = false;
    private Vector3 idleTarget;
    public float idlesize = 2;

    private void Start()
    {
        Vector2 randCirc = Random.insideUnitCircle;
        idleTarget = anchor.position + new Vector3(randCirc.x * idlesize, 0, randCirc.y * idlesize);
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 200) <= 1)
        {
            Vector2 randCirc = Random.insideUnitCircle;
            idleTarget = anchor.position + new Vector3(randCirc.x * idlesize, 0, randCirc.y* idlesize);
        }
    }

    void Update()
    {

        if(hasTarget == true)
        {
            if (Vector3.Distance(transform.position + (target.transform.position - transform.position).normalized * movespeed * Time.deltaTime, anchor.position) < anchorDistance)
            {
                transform.position += (target.transform.position- transform.position).normalized * movespeed * Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, target.transform.position) > loseDistance)
            {
                hasTarget = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, idleTarget) >= 0.1f)
            {
                transform.position += (idleTarget - transform.position).normalized * movespeed * Time.deltaTime;
            }

            if (Random.Range(0, randomDelay) == 0)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackDistance);
                int i = 0;
                while (i < hitColliders.Length)
                {
                    Enemy enemy = hitColliders[i].GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        target = enemy;
                        hasTarget = true;
                    }
                    i++;
                }
            }
        }
    }
}
