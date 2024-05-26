using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    
    void Start()
    {
        if(musicSource.isPlaying) return;
        
        musicSource.Play();
    }

   
}
