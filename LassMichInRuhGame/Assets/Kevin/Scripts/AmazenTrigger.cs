using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmazenTrigger : MonoBehaviour
{
    public GameObject store;
    public GameObject notification;
    bool inside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inside = false;
            if (store.activeSelf)
            {
                store.SetActive(false);
            }
            
        }
    }

    private void Update()
    {
        if (inside && Input.GetButtonDown("Jump"))
        {
            store.SetActive(!store.activeSelf);
        }

        if (inside && !store.activeSelf)
        {
            notification.SetActive(true);
        }
        else if (notification.activeSelf)
        {
            notification.SetActive(false);
        }
    }
}
