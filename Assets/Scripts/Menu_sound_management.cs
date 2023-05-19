using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_sound_management : MonoBehaviour
{
    public AudioClip introAudioClip;
    public AudioClip loopAudioClip;
    public AudioClip outroAudioClip;
    public AudioSource audioSource;

    void Start()
    {
        GetComponent<AudioSource>().clip = introAudioClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = false;
    }

    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = loopAudioClip;
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
    }
}
