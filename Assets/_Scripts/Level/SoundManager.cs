using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private Slider volumeSlider;
    public float Volume = 1f;

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
        

        Volume = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = Volume;
    }
    private void Update()
    {
        Volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume",Volume);
    }

    public void PlaySound(AudioClip sound, Transform transform)
    {
        if (sound == null)
        {
            Debug.Log("your are missing an audio sound");
            return;
        }
            
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.volume = Volume; 
        audioSource.Play();


        Destroy(soundGameObject, sound.length);
    }
    
    public void PlaySound(AudioClip sound)
    {
        PlaySound(sound,transform);
    }

   
}
