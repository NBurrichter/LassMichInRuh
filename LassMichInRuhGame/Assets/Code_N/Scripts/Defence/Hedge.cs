using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedge : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
