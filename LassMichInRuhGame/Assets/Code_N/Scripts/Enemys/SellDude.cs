using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellDude : MonoBehaviour
{
    [SerializeField]
    private float annoydelay = 5;
    [SerializeField]
    private float annoy = 1;

    [SerializeField]
    private AudioClip sound;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        annoydelay -= Time.deltaTime;
        if (annoydelay <= 0)
        {
            annoydelay = 1;
            SanityController.instance.RemoveSanity(annoy);
            source.PlayOneShot(sound);

        }
    }

}
