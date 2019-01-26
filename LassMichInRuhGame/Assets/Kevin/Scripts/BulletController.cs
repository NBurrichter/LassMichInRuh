using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(transform.position + direction);
    }

    private void Update()
    {
        rb.MovePosition(transform.position + direction * speed);
    }
}
