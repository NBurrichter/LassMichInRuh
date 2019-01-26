using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    public float smoothingTime = 0.1f;
    public int awardAmount = 30;
    Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.transform;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Homing());
        }
    }

    IEnumerator Homing()
    {
        var velocity = Vector3.zero;
        while ((transform.position - target.position).magnitude > 0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothingTime);
            yield return null;
        }

        MoneyController.Amount += awardAmount;
        Destroy(gameObject);
    }
}
