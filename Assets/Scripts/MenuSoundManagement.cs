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
        audioSource.clip = introAudioClip;
        audioSource.Play();
        audioSource.loop = false;
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = loopAudioClip;
            audioSource.Play();
            audioSource.loop = true;
        }
        audioSource.volume = soundManagement.musicVolume;
    }
}
