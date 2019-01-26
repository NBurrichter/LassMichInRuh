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
    public LayerMask collisionMask;
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
            float correction = 0;
            for (var i = 0; i < castsPerCount; i++)
            {
                var originX = transform.position.x;
                var startY = col.bounds.max.z;
                var endY = col.bounds.min.z;
                var rayDirection = direction.x < 0 ? Vector3.left : Vector3.right;
                var rayDistance = col.bounds.extents.x + Mathf.Abs(direction.x * distance);
                var origin = new Vector3(originX, transform.position.y, Mathf.Lerp(startY, endY, (float)i / (castsPerCount - 1)));
                var hitCount = Physics.RaycastNonAlloc(origin, rayDirection, hits, rayDistance, collisionMask);
                Debug.DrawLine(origin, origin + rayDirection * rayDistance);
                
                for (var j = 0; j < hitCount; j++)
                {
                    if (hits[j].transform == col.transform || hits[j].collider.isTrigger)
                    {
                        continue;
                    }

                    var point = hits[j].point;
                    var hitDistance = rayDistance - Mathf.Abs(point.x - origin.x);
                    correction = Mathf.Max(hitDistance, correction);
                }
            }
            movement.x += direction.x < 0 ? correction : -correction;
        }

        if (movement.z != 0)
        {
            float correction = 0;
            for (var i = 0; i < castsPerCount; i++)
            {
                var originX = transform.position.z;
                var startY = col.bounds.max.x;
                var endY = col.bounds.min.x;
                var rayDirection = new Vector3( 0, 0, direction.z < 0 ? -1 : 1);
                var rayDistance = Mathf.Abs(col.bounds.extents.z) + Mathf.Abs(direction.z * distance);
                var origin = new Vector3(Mathf.Lerp(startY, endY, (float)i / (castsPerCount - 1)), transform.position.y, originX);
                var hitCount = Physics.RaycastNonAlloc(origin, rayDirection, hits, rayDistance, collisionMask);
                Debug.DrawLine(origin, origin + rayDirection * rayDistance);
                Debug.Log($"{origin} {origin + rayDirection * rayDistance}");
                for (var j = 0; j < hitCount; j++)
                {
                    if (hits[j].transform == col.transform || hits[j].collider.isTrigger)
                    {
                        continue;
                    }

                    var point = hits[j].point;
                    var hitDistance = rayDistance - Mathf.Abs(point.z - origin.z);
                    correction = Mathf.Max(hitDistance, correction);
                }
            }
            movement.z += direction.z < 0 ? correction : -correction;
        }

        transform.position += movement;

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
