using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WutAder : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = Mathf.Clamp(1 / (SanityController.instance.Sanity / 100),0.2f,1);
    }
}
