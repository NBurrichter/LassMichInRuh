using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCollar : MonoBehaviour
{

    public Transform obj1;
    public Transform obj2;
    public Transform sprite;
    public SpriteRenderer sR;

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = sprite.localScale;
        newScale.x = Vector3.Distance(obj1.position, obj2.position) * 2f;
        sprite.localScale = newScale;
        transform.position = (obj1.position - obj2.position) / 2 + obj2.position;
        transform.rotation = Quaternion.LookRotation(obj1.position - obj2.position);
        if(Vector3.Dot(obj1.position -obj2.position , Vector3.left) > 0)
        {
            sR.flipY = true;
        }
        else
        {
            sR.flipY = false;
        }
    }
}
