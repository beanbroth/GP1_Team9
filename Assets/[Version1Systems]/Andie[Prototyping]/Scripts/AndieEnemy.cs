using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndieEnemy : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public float health;
    private void Update()
    {
        transform.Translate(Vector3.ClampMagnitude((player.transform.position - transform.position), 1) * speed * Time.deltaTime);

        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

