using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public GameObject musicSlider;
    public GameObject soundSlider;
    public float musicVolume;
    public float soundVolume;
    public AudioClip uiClickAudioClip;
    public AudioSource audioSource;

    void Start()
    {
        musicVolume = 0.5f;
        soundVolume = 0.5f;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void ChangeSoundVolume(float volume)
    {
        soundVolume = volume;
    }

    public void PlayUIClick()
    {
        audioSource.PlayOneShot(uiClickAudioClip, soundVolume);
    }
}
