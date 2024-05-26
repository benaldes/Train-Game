using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private List<AudioSource> AudioSources = new List<AudioSource>();
    
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
            Debug.Log(transform.parent.name + " is missing an audio sound");
            return;
        }

        foreach (AudioSource audioSource in AudioSources)
        {
            if(audioSource.isPlaying) continue;
            
            Play(audioSource, sound, Volume);
            return;
        }
        
        GameObject soundGameObject = new GameObject("AudioSound");
        soundGameObject.transform.parent = this.transform;
        AudioSource newAudioSource = soundGameObject.AddComponent<AudioSource>();
        AudioSources.Add(newAudioSource);
        
        Play(newAudioSource, sound, Volume);
    }
    
    public void PlaySound(AudioClip sound)
    {
        PlaySound(sound,transform);
    }

    private void Play(AudioSource audioSource, AudioClip audioClip, float volume)
    {
        audioSource.clip = audioClip;
        audioSource.volume = Volume; 
        audioSource.Play();
    }

   
}
