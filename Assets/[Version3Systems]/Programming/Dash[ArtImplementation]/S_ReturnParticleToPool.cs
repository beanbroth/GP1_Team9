using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ReturnParticleToPool : MonoBehaviour
{
    ParticleSystem ps;

    // When the particle system is done playing, destroy the object
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        if (ps)
        {
            ps.Play();
        }
    }

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                ObjectPoolManager.Destroy(gameObject);
            }
        }
    }
}