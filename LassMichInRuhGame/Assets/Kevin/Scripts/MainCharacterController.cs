using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public float speed = 1f;
    public float fireRate = 10;
    public GameObject bulletPrefab;
    private Collider col;
    RaycastHit[] hits = new RaycastHit[1024];
    Collider[] cols = new Collider[1024];
    float lastFire = 0;

    const int castsPerCount = 5;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }


    private void Update()
    {
        var axis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        var direction = axis.normalized;
        var distance = Mathf.Min(1 , axis.magnitude) * speed;
        var movement = direction * distance;
        var xLimit = direction.x > 0 ? Mathf.Infinity : -Mathf.Infinity;

        var target = transform.position + movement;

        if (movement.x != 0)
        {
            for (var i = 0; i < castsPerCount; i++)
            {
                //Debug.Log($"{col.bounds.max} {col.bounds.min}");
                var originX = direction.x < 0 ? col.bounds.min.x : col.bounds.max.x;
                var startY = col.bounds.max.y;
                var endY = col.bounds.min.y;
                var origin = new Vector3(originX, Mathf.Lerp(startY, endY, (float)i / (castsPerCount - 1)), transform.position.z);
                var hitCount = Physics.RaycastNonAlloc(origin, direction, hits, distance);
                Debug.DrawRay(origin, movement);
                //Debug.Log($"{origin} {movement}");
                for (var j = 0; j < hitCount; j++)
                {
                    if (hits[j].transform == col.transform)
                    {
                        continue;
                    }
                    if (direction.x > 0)
                    {
                        xLimit = Mathf.Min(xLimit, hits[j].point.x);
                    }
                    else
                    {
                        xLimit = Mathf.Max(xLimit, hits[j].point.x);
                    }
                    Debug.Log($"{xLimit}");
                }
            }
        }
        /*
        var hitCount = Physics.BoxCastNonAlloc(col.transform.position, col.bounds.extents/2, direction, hits, col.transform.rotation, movement);
        for (var i = 0; i < hitCount; i++)
        {
            if (hits[i].collider == col)
            {
                continue;
            }
 
            movement = Mathf.Max(0, Mathf.Min(movement, Mathf.Abs( (hits[i].point - col.ClosestPointOnBounds(hits[i].point)).magnitude)));
        }
        */

        target.x = direction.x > 0 ? Mathf.Min(xLimit, target.x) : Mathf.Max(xLimit, target.x);
        transform.position = target;

        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= lastFire + (1/fireRate))
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var fireDirection = mousePos - transform.position;
                fireDirection.z = 0;
                fireDirection = fireDirection.normalized;
                lastFire = Time.time;
                var bullet = Instantiate(bulletPrefab, col.bounds.ClosestPoint(mousePos), Quaternion.identity);
                var controller = bullet.GetComponent<BulletController>();
                var bulletCollider = bullet.GetComponent<Collider>();
                Vector3 bullDirection;
                float bullDistance;
                Physics.ComputePenetration(bulletCollider, bullet.transform.position, bullet.transform.rotation, col, transform.position, transform.rotation, out bullDirection, out bullDistance);
                bullet.transform.position += bullDirection * bullDistance;


                controller.direction = fireDirection;
                controller.speed = 1;
                Destroy(bullet, 1);
            }
        }
    }
}
