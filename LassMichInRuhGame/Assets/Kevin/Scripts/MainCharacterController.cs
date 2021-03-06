﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public float speed = 1f;
    public float acceleration = 0.2f;
    public float fireRate = 10;
    public GameObject bulletPrefab;
    public Sprite leftSprite;
    public Sprite upSprite;
    public Sprite downSprite;
    public LayerMask collisionMask;
    private Collider col;
    public SpriteRenderer renderer;
    RaycastHit[] hits = new RaycastHit[1024];
    Collider[] cols = new Collider[1024];
    float lastFire = 0;
    Vector3 velocity = Vector3.zero;
    float recoilLeft;
    Vector3 recoilDirection;
    float currentSpeed;
    int activeSlows = 0;
    [SerializeField]
    Transform firePoint;

    const int castsPerCount = 5;

    public AudioSource shootSound;
    public Animator anim;

    private void Awake()
    {
        col = GetComponent<Collider>();
        currentSpeed = speed;
    }


    private void Update()
    {
        var axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(Mathf.Abs(axis.x)> 0.1f || Mathf.Abs(axis.z) > 0.1f)
        {
            anim.speed = 1;
        }
        else
        {
            anim.speed = 0;
        }
        var direction = axis.normalized;
        if (activeSlows <= 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, speed, Time.deltaTime);
        }
        else if (activeSlows > 1)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, speed/2, Time.deltaTime);
        }
        velocity = acceleration * direction * Mathf.Min(1, axis.magnitude) * currentSpeed + (1 - acceleration) * velocity;

       
        if (velocity.magnitude > 0)
        {
            AdjustOrientation(direction);
        }

        CheckFire();

        if (recoilLeft > 0)
        {
            velocity += recoilDirection;
            recoilLeft -= Time.deltaTime;
        }

        var distance = velocity.magnitude * Time.deltaTime;
        var movement = velocity * Time.deltaTime;



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
    }

    public void CheckFire()
    {
        if (Input.GetButton("Fire1"))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, Vector3.zero);
            float enter;
            plane.Raycast(ray, out enter);
            var mousePos = ray.GetPoint(enter);
            var fireDirection = mousePos - transform.position;
            if (Time.time >= lastFire + (1 / fireRate) * SanityController.instance.Sanity / 50)
            {
                fireDirection.y = 0;
                fireDirection = fireDirection.normalized;
                /*
                recoilLeft = 0.3f;
                recoilDirection = -fireDirection;
                */
                lastFire = Time.time;
                var bullet = Instantiate(bulletPrefab, /*col.bounds.ClosestPoint(mousePos)*/firePoint.position, Quaternion.Euler(90, 0, 0));
                var controller = bullet.GetComponent<BulletController>();
                var bulletCollider = bullet.GetComponent<Collider>();
                Vector3 bullDirection;
                float bullDistance;
                //Physics.ComputePenetration(bulletCollider, bullet.transform.position, bullet.transform.rotation, col, transform.position, transform.rotation, out bullDirection, out bullDistance);
                bullet.transform.position += fireDirection * 0.5f;


                controller.direction = fireDirection;
                //controller.speed = 1;
                //Destroy(bullet, 1);

                shootSound.pitch = Random.Range(0.9f, 1.1f);
                shootSound.Play();
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

            if (direction.x < 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Side", true);
                anim.SetBool("Down", false);
                renderer.flipX = false;
            }
            else if (direction.x > 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Side", true);
                anim.SetBool("Down", false);
                renderer.flipX = true;
            }

        }
        else
        {
            if (direction.z > 0)
            {
                anim.SetBool("Up", true);
                anim.SetBool("Side", false);
                anim.SetBool("Down", false);
            }
            else if (direction.z < 0)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Side", false);
                anim.SetBool("Down", true);
            }
        }
    }

    public void ApplyPoo()
    {
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        activeSlows++;
        currentSpeed *= 0.5f;
        yield return new WaitForSeconds(2);
        activeSlows--;
    }
        
}
