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

        private void OnEnable()
        {
            StartCoroutine(testDeath());
        }

        IEnumerator testDeath()
        {
            yield return new WaitForSeconds(0.1f);
            core.Stats.OnHealthZero += Die;
        }

        private void OnDisable()
        {
            core.Stats.OnHealthZero -= Die;
        }
    }
