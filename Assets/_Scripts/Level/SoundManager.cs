using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private Slider volumeSlider;
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
    private void Update()
    {
        Volume = volumeSlider.value;
    }

    public void PlaySound(AudioClip sound, Transform transform)
    { 
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.volume = Volume; 
        audioSource.Play();


        Destroy(soundGameObject, sound.length);
    }

   
}
