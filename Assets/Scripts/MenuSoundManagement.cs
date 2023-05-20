using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManagement : MonoBehaviour
{
    public AudioClip introAudioClip;
    public AudioClip loopAudioClip;
    public AudioClip outroAudioClip;
    public AudioSource audioSource;
    public SoundManagement soundManagement;

    void Start()
    {
        GetComponent<AudioSource>().clip = introAudioClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().volume = 0.5f;
    }

    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = loopAudioClip;
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
        GetComponent<AudioSource>().volume = soundManagement.musicVolume;
    }
}
