using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float gravity = 0;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float travelDistance;

    private Vector3 startpos;

    private Rigidbody rb;

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
}
