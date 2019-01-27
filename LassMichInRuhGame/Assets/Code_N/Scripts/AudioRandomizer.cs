using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
