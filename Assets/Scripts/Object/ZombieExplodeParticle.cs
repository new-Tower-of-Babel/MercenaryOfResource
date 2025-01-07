using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieExplodeParticle : MonoBehaviour
{
    private ParticleSystem explosionParticles;

    void Awake()
    {
        explosionParticles = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        explosionParticles.Stop();
    }
}
