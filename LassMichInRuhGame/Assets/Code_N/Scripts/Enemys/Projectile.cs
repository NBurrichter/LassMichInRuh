using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
    public LayerMask playerLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startpos = transform.position;
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * moveSpeed);
        if(Vector3.Distance(startpos,transform.position) >= travelDistance)
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
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            SanityController.instance.RemoveSanity(damage);
            Destroy(gameObject);
        }
    }
}
