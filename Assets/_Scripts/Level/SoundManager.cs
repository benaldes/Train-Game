using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public float Volume = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySound(AudioClip sound, Transform transform)
    { 
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.volume = Volume; // Set the volume of the AudioSource
        audioSource.Play();

        // Automatically destroy the GameObject and AudioSource when the sound finishes playing
        Destroy(soundGameObject, sound.length);
    }
}
