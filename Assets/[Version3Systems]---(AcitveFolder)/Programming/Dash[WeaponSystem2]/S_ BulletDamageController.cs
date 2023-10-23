using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BulletDamageController : MonoBehaviour
{
    [SerializeField] private bool enemyDestroyOnCollision = true;
    [SerializeField] private bool otherDestroyOnCollision = true;
    [SerializeField] int damage = 1;
    [SerializeField] private int penetration;
    private int bulletHealth;

    private void Start()
    {
        bulletHealth = penetration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            S_EnemyHealthController emc = other.GetComponent<S_EnemyHealthController>();
            if (emc != null)
            {
                emc.TakeDamage(damage);
            }

            if (enemyDestroyOnCollision)
            {
                bulletHealth--;
                if (bulletHealth <= 0)
                {
                    ObjectPoolManager.Destroy(gameObject);
                }
            }
        }

        if (other.gameObject.tag != "Player" && otherDestroyOnCollision)
        {
            ObjectPoolManager.Destroy(gameObject);
        }
    }
}