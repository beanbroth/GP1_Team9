using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndieAreaDamage : MonoBehaviour
{
    public float radius;
    public float maxDistance;
    public LayerMask enemyMask;

    public float damage;
    private bool hasDamaged;
    public float waitTime;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, enemyMask);

        foreach (var hitCollider in hitColliders)
        {
            print("hit1");
            if (hitCollider.GetComponent<AndieEnemy>() != null)
            {
                print("hit");
                Damage(damage, hitCollider.GetComponent<AndieEnemy>());
            }
        }

    }

    public void Damage(float damage, AndieEnemy enemy)
    {
        if (!hasDamaged)
        {
            enemy.TakeDamage(damage);
            hasDamaged = true;
            Invoke("ResetAttack", waitTime);
        }
    }

    private void ResetAttack()
    {
        hasDamaged = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
