using System;
using System.Collections;
using UnityEngine;
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        [SerializeField] private AudioClip deathSound;
        private Stats stats;
        private ParticleManager particleManager;

        public override void InitializeCoreComponent()
        {
            base.InitializeCoreComponent();
            stats = core.GetCoreComponent(typeof(Stats)) as Stats;
            particleManager = core.GetCoreComponent(typeof(ParticleManager)) as ParticleManager;
            stats.OnHealthZero += Die;
        }

        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                particleManager.StartParticles(particle);
            }
            
            SoundManager.Instance.PlaySound(deathSound);
            
            core.transform.parent.gameObject.SetActive(false);
        }
        
        
        private void OnDisable()
        {
            stats.OnHealthZero -= Die;
        }
    }
