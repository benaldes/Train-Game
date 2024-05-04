using System;
using System.Collections;
using UnityEngine;
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        [SerializeField] private AudioClip deathSound;
        
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                core.ParticleManager.StartParticles(particle);
            }
            
            SoundManager.Instance.PlaySound(deathSound);
            
            core.transform.parent.gameObject.SetActive(false);
        }
        

        private void Start()
        {
            core.Stats.OnHealthZero += Die;
        }
        
        private void OnDisable()
        {
            core.Stats.OnHealthZero -= Die;
        }
    }
