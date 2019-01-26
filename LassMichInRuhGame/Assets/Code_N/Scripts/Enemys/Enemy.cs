using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float health = 10;

    public UnityEvent OnDie;
    public UnityEvent OnNeighbourDie;
    public UnityEvent OnNeighbourReset;

    public GameObject[] deathObjects;

    public GameObject[] hitObject;

    public bool Neighbour = false;
    private bool flee = true;
    public Transform grillPoint;
    public float grillTime;
    public ParticleSystem smokeParticle;

    private void Start()
    {
        if(Neighbour)
        {
            StartWait();
        }
    }

    public void Update()
    {
        if (Neighbour)
        {
            if (Vector3.Distance(grillPoint.position, transform.position) <= 0.5f)
            {
                grillTime += Time.deltaTime;
                if (grillTime >= 3)
                {
                    smokeParticle.Play();
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("Damage");
        if (Neighbour && flee)
            return;

        health -= damage;
        if (health <= 0)
        {
            if (Neighbour)
            {
                OnNeighbourDie.Invoke();
                flee = true;
                smokeParticle.Stop();
                grillTime = 0;
            }
            else
            {
                foreach (GameObject obj in deathObjects)
                {
                    Instantiate(obj, transform.position, obj.transform.rotation);
                }
                OnDie.Invoke();
                Destroy(gameObject);
            }
        }
        else
        {
            foreach (GameObject obj in hitObject)
            {
                Instantiate(obj, transform.position, obj.transform.rotation);
            }
        }
    }
  
    public void StartWait()
    {
        StartCoroutine(ResetNeighbours());
    }

    IEnumerator ResetNeighbours()
    {
        yield return new WaitForSeconds(Random.Range(8, 10));
        health = 10;
        OnNeighbourReset.Invoke();
        flee = false;
    }
}
