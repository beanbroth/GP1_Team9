using System.Collections.Generic;
using UnityEngine;

public class S_AtomaticGunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int numberOfBullets = 3;
    public float timeBetweenShots = 1f;
    public float maxRange = 10f;
    public LayerMask enemyLayer;

    private float nextFireTime;
    [SerializeField] AudioClip shootSound;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            List<Transform> targetEnemies = GetClosestEnemiesInRange(numberOfBullets);

            if (targetEnemies.Count > 0)
            {
                foreach (Transform targetEnemy in targetEnemies)
                {
                    if (targetEnemy != null)
                    {
                        Shoot(targetEnemy);
                    }
                }
            }
            else
            {
                ShootRandom();
            }
            nextFireTime = Time.time + timeBetweenShots;
        }
    }

    private void Shoot(Transform targetEnemy)
    {
        audioSource.PlayOneShot(shootSound);
        GameObject bullet = ObjectPoolManager.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        S_AtomaticBulletController bulletController = bullet.GetComponent<S_AtomaticBulletController>();
        bulletController.SetTarget(targetEnemy);
    }

    private void ShootRandom()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        ObjectPoolManager.Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(randomDirection));
    }

    private List<Transform> GetClosestEnemiesInRange(int numberOfTargets)
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, maxRange, enemyLayer);
        List<Transform> remainingEnemies = new List<Transform>(enemyColliders.Length);

        foreach (Collider enemyCollider in enemyColliders)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                remainingEnemies.Add(enemyCollider.transform);
            }
        }

        List<Transform> closestEnemies = new List<Transform>(numberOfTargets);

        for (int i = 0; i < numberOfTargets; i++)
        {
            float minDistSqr = Mathf.Infinity;
            int closestEnemyIndex = -1;

            for (int j = 0; j < remainingEnemies.Count; j++)
            {
                float distSqr = (remainingEnemies[j].position - transform.position).sqrMagnitude;
                if (distSqr < minDistSqr)
                {
                    closestEnemyIndex = j;
                    minDistSqr = distSqr;
                }
            }

            if (closestEnemyIndex != -1)
            {
                closestEnemies.Add(remainingEnemies[closestEnemyIndex]);
                remainingEnemies.RemoveAt(closestEnemyIndex);
            }
            else
            {
                break;
            }
        }

        return closestEnemies;
    }
}