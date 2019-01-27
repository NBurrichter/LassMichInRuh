using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooDrop : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<MainCharacterController>().ApplyPoo();
            Destroy(gameObject);
        }
    }
}
