using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float health = 10;

    public UnityEvent OnDie;

    public GameObject[] deathObjects;

    public GameObject[] hitObject;

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage");
        health -= damage;
        if (health <= 0)
        {
            foreach(GameObject obj in deathObjects)
            {
                Instantiate(obj, transform.position, transform.rotation);
            }
            OnDie.Invoke();
            Destroy(gameObject);
        }
        else
        {
            foreach (GameObject obj in hitObject)
            {
                Instantiate(obj, transform.position, transform.rotation);
            }
        }
    }
  
}
