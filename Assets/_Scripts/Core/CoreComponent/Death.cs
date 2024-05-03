using System;
using System.Collections;
using UnityEngine;
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;
        
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                core.ParticleManager.StartParticles(particle);
            }
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
