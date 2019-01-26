using System.Collections;
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

    // Update is called once per frame
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
            Vector2 randCirc = Random.insideUnitCircle;
            transform.position += (anchor.position + new Vector3(randCirc.x,0,randCirc.y)  - transform.position).normalized * movespeed * Time.deltaTime;

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
