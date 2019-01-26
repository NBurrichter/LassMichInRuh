using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooDrop : MonoBehaviour
{
    public float sanityDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SanityController.instance.RemoveSanity(sanityDamage);
            other.GetComponent<MainCharacterController>().ApplyPoo();
            Destroy(gameObject);
        }
    }
}
