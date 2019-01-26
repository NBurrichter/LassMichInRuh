using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public float speed = 1f;
    public float fireRate = 10;
    public GameObject bulletPrefab;
    public Sprite leftSprite;
    public Sprite upSprite;
    public Sprite downSprite;
    private Collider col;
    private SpriteRenderer renderer;
    RaycastHit[] hits = new RaycastHit[1024];
    Collider[] cols = new Collider[1024];
    float lastFire = 0;

    const int castsPerCount = 5;

    private void Awake()
    {
        col = GetComponent<Collider>();
        renderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        var axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var direction = axis.normalized;
        var distance = Mathf.Min(1, axis.magnitude) * speed;
        var movement = direction * distance;
        var xLimit = direction.x > 0 ? Mathf.Infinity : -Mathf.Infinity;

        if (movement.magnitude > 0)
        {
            AdjustOrientation(direction);
        }
        

        if (movement.x != 0)
        {
            for (var i = 0; i < castsPerCount; i++)
            {
                var xBounds = direction.x < 0 ? col.bounds.min : col.bounds.max;
                var originX = xBounds.x;
                var startY = col.bounds.max.z;
                var endY = col.bounds.min.z;
                var rayDirection = direction;
                var rayDistance = Mathf.Abs(direction.x * distance);
                rayDirection.z = 0;
                var origin = new Vector3(originX, transform.position.y, Mathf.Lerp(startY, endY, (float)i / (castsPerCount - 1)));
                var hitCount = Physics.RaycastNonAlloc(origin, rayDirection, hits, rayDistance);
                Debug.DrawLine(origin, origin + rayDirection * rayDistance);
                for (var j = 0; j < hitCount; j++)
                {
                    if (hits[j].transform == col.transform || hits[j].collider.isTrigger)
                    {
                        continue;
                    }

                    GetComponent<SanityController>().RemoveSanity(1);
                    var point = hits[j].point;
                    var hitDistance = point.x - origin.x;
                    if (Mathf.Abs(movement.x) > Mathf.Abs(hitDistance))
                    {
                        movement.x = hitDistance;
                    }
                }
            }
        }

        if (movement.z != 0)
        {
            for (var i = 0; i < castsPerCount; i++)
            {
                var xBounds = direction.z < 0 ? col.bounds.min : col.bounds.max;
                var originX = xBounds.z;
                var startY = col.bounds.max.x;
                var endY = col.bounds.min.x;
                var rayDirection = direction;
                var rayDistance = Mathf.Abs(direction.z * distance);
                rayDirection.x = 0;
                var origin = new Vector3(Mathf.Lerp(startY, endY, (float)i / (castsPerCount - 1)), transform.position.y, originX);
                var hitCount = Physics.RaycastNonAlloc(origin, rayDirection, hits, rayDistance);
                Debug.DrawLine(origin, origin + rayDirection * rayDistance);
                for (var j = 0; j < hitCount; j++)
                {
                    if (hits[j].transform == col.transform || hits[j].collider.isTrigger)
                    {
                        continue;
                    }

                    GetComponent<SanityController>().RemoveSanity(1);
                    var point = hits[j].point;
                    var hitDistance = point.z - origin.z;
                    if (Mathf.Abs(movement.z) > Mathf.Abs(hitDistance))
                    {
                        movement.z = hitDistance;
                    }
                }
            }
        }

        transform.position += movement;

        var colCount = Physics.OverlapBoxNonAlloc(col.bounds.center, col.bounds.extents / 2, cols);
        for (var i = 0; i < colCount; i++)
        {
            var other = cols[i];
            if (other == col || other.isTrigger)
            {
                continue;
            }
            Debug.Log("Overlap");
            Vector3 trans;
            float transDist;
            Physics.ComputePenetration(col, transform.position, transform.rotation, other, other.transform.position, other.transform.rotation, out trans, out transDist);
            transform.position += trans * transDist;
        }

        var adjustOrientation = direction;
        if (Input.GetButton("Fire1"))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var fireDirection = mousePos - transform.position;
            if (Time.time >= lastFire + (1/fireRate))
            {
                
                fireDirection.y = 0;
                fireDirection = fireDirection.normalized;
                lastFire = Time.time;
                var bullet = Instantiate(bulletPrefab, col.bounds.ClosestPoint(mousePos),Quaternion.Euler(90, 0, 0));
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
            AdjustOrientation(fireDirection);
        }

    }

    public void AdjustOrientation(Vector3 direction)
    {
        if (direction.z == 0 && direction.x == 0)
        {
            return;
        }
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            renderer.sprite = leftSprite;
            if (direction.x < 0)
            {
                renderer.flipX = false;
            }
            else if (direction.x > 0)
            {
                renderer.flipX = true;
            }

        }
        else
        {
            if (direction.z > 0)
            {
                renderer.sprite = upSprite;
            }
            else if (direction.z < 0)
            {
                renderer.sprite = downSprite;
            }
        }
    }
        
}
