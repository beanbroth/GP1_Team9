using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class S_BulletDamageController : MonoBehaviour
{
    [SerializeField] private bool destroyOnEnemyCollision = true;
    [SerializeField] private bool destroyOnOtherCollision = true;
    [SerializeField] int damage = 1;
    [SerializeField] private int penetration;
    [SerializeField] private float lifeTime = 10f;
    private int bulletHealth;
    private Vector3 lastPos;
    private Vector3 velocityDir;
    private float timeSinceSpawn;

    private void Start()
    {
        lastPos = transform.position;
        bulletHealth = penetration;
    }

    private void OnEnable()
    {
        timeSinceSpawn = 0f;
    }

    private void FixedUpdate()
    {
        if (PauseManager.IsPaused)
        {
            return;
        }

        velocityDir = (transform.position - lastPos).normalized;
        lastPos = transform.position;
        if (destroyOnEnemyCollision || destroyOnOtherCollision)
        {
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn > lifeTime)
            {
                ObjectPoolManager.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            S_EnemyHealthController emc = other.GetComponent<S_EnemyHealthController>();
            if (emc != null)
            {
//                Debug.Log("velocity dir: " + velocityDir);
                emc.TakeDamage(damage, velocityDir);
            }

            if (destroyOnEnemyCollision)
            {
                bulletHealth--;
                if (bulletHealth <= 0)
                {
                    // Debug.Log("Bullet Destroyed Enemy" + other.gameObject.name);
                    ObjectPoolManager.Destroy(gameObject);
                }
            }
        }

        if (other.gameObject.tag != "Player" && destroyOnOtherCollision)
        {
            //Debug.Log("Bullet Destroyed on other" + other.gameObject.name);
            ObjectPoolManager.Destroy(gameObject);
        }
    }
}