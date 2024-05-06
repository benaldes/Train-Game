using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleManager : CoreComponent
    {
        [SerializeField, HideInInspector] private Transform particleContainer;

        protected void OnValidate()
        {
            particleContainer = GameObject.FindWithTag("ParticleContainer").transform;
        }

        public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
        {
            return Instantiate(particlePrefab, position, rotation,particleContainer);
        }

        public GameObject StartParticles(GameObject particlePrefab)
        {
            return StartParticles(particlePrefab, transform.position, quaternion.identity);
        }

        public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
        {
            var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            return StartParticles(particlePrefab, transform.position, randomRotation);
        }
    }
