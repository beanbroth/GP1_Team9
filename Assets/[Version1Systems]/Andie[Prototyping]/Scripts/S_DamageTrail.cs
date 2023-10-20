using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DamageTrail : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<S_EnemyHealthController>().TakeDamage(damage);
        }
    }
}
