using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float travelDistance;
    [SerializeField]
    private float damage;

    private Vector3 startpos;

    private Rigidbody rb;

    public LayerMask wallLayer;
    public LayerMask hitLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startpos = transform.position;
    }

    void FixedUpdate()
    {
        //rb.MovePosition(transform.position + transform.forward * moveSpeed);
        if(Vector3.Distance(startpos,transform.position) >= travelDistance)
        {
            Destroy(gameObject);
        }
        if (Physics.Raycast(transform.position, transform.forward, moveSpeed, wallLayer))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (wallLayer == (wallLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
        if (hitLayer == (hitLayer | (1 << collision.gameObject.layer)))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }
}
