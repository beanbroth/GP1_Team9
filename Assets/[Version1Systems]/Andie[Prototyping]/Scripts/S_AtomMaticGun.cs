using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_AtomMaticGun : MonoBehaviour
{
    GameObject[] enemies;
    Transform[] enemiesTransform;
    public float bulletSpeed = 20;
    [SerializeField] private int damage;

    private GameObject player;

    Transform targetEnemy;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position = player.transform.position;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemiesTransform = enemies.Select(f => f.transform).ToArray();

        targetEnemy = GetClosestEnemy(enemiesTransform);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, bulletSpeed * Time.deltaTime);
        if (enemies.Length <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<S_EnemyHealthController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
