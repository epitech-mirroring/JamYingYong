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

    void Start()
    {
        musicVolume = 0.5f;
        soundVolume = 0.5f;
    }

    public void ChangeMusicVolume(float volume)
    {
        Debug.Log("Music Volume: " + volume);
        musicVolume = volume;
    }

    public void ChangeSoundVolume(float volume)
    {
        Debug.Log("Sound Volume: " + volume);
        soundVolume = volume;
    }

    public void PlayUIClick()
    {
        GetComponent<AudioSource>().PlayOneShot(uiClickAudioClip, soundVolume);
    }
}
