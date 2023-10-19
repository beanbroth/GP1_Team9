using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AreaDamage : MonoBehaviour
{
    public float radius;
    public float maxDistance;
    public LayerMask enemyMask;
    public GameObject areaObject;

    public float damage;
    public float waitTime;


    private void Start()
    {
        InvokeRepeating("SphereCheck", 0.0f, waitTime);
    }

    private void Update()
    {
        areaObject.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
    }

    private void SphereCheck()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, enemyMask);

        foreach (var hitCollider in hitColliders)
        {
            print("hit1");
            if (hitCollider.GetComponent<S_AndieEnemy>() != null)
            {
                print("hit");
                Damage(damage, hitCollider.GetComponent<S_AndieEnemy>());

            }
        }
    }

    public void Damage(float damage, S_AndieEnemy enemy)
    {
        enemy.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}