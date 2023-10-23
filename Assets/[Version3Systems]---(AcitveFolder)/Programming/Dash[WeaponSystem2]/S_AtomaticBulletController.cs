using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_AtomaticBulletController : MonoBehaviour
{

    public float bulletSpeed = 20f;
    public int damage = 1;

    private Transform targetEnemy;

    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, bulletSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            S_EnemyHealthController emc = other.GetComponent<S_EnemyHealthController>();
            if (emc != null) { emc.TakeDamage(damage); }
            ObjectPoolManager.Destroy(gameObject);
        }

        if (other.gameObject.tag != "Player")
        {
            ObjectPoolManager.Destroy(gameObject);
        }

    }
}