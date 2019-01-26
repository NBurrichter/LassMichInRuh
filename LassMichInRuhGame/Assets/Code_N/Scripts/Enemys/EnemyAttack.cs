using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private float aggroRange;

    private Transform attackTarget;
    private bool hasTarget = false;
    [SerializeField]
    private float attackCooldown = 3;
    private float currentCooldown;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private int repeats = 1;
    private int repeatcount = 0;
    [SerializeField]
    private float repeatTime = 0.1f;
    [SerializeField]
    private Transform firePoint;

    private void Start()
    {
        currentCooldown = attackCooldown;
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, aggroRange, attackMask);
        for (int i = 0; i < cols.Length; i++)
        {
            attackTarget = cols[i].transform;
            hasTarget = true;
        }
        if(cols.Length == 0)
        {
            hasTarget = false;
        }

        if(hasTarget)
        {
            currentCooldown -= Time.deltaTime;
            //Shot at player
            if(currentCooldown <= 0)
            {
                InvokeRepeating("SpawnBullet", 0, repeatTime);
                currentCooldown = attackCooldown + Random.Range(-1, 1);
            }
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, 0));
        Vector3 aimtarget = attackTarget.position;
        if (Vector3.Distance(firePoint.position, attackTarget.position) >= 1)
        {
            Vector3 ranSphere = Random.insideUnitSphere;
            ranSphere.y = 0;
            aimtarget += ranSphere;
        }
        bullet.transform.LookAt(aimtarget);
        repeatcount++;
        if(repeatcount >= repeats)
        {
            CancelInvoke();
            repeatcount = 0;
        }
    }

}
