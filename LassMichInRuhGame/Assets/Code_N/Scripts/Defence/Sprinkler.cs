using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler : MonoBehaviour
{
    public Transform rotateHead;
    public float rotateSpeed = 1;
    public float damage = 1;

    void Update()
    {
        rotateHead.Rotate(new Vector3(0, rotateSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        Enemy enemy = other.GetComponent<Enemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
