using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MusicSpawn : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip intro;
    public AudioClip loop;
    public BoxCollider box;
    private bool _isPassed = false;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource.loop = false;
        audioSource.clip = intro;
        audioSource.volume = 0.5f;
    }

    private void Update()
    {
        if (audioSource.isPlaying)
            return;
        if (_isPassed == false)
            return;
        audioSource.clip = loop;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (audioSource.isPlaying)
            return;
        audioSource.Play();
        _isPassed = true;
    }
}
