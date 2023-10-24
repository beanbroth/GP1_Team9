using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class S_DamageTrailController: MonoBehaviour
{
    public GameObject trailColliderPrefab;
    public float spawnInterval = 1.0f;
    public float trailLengthTime = 10.0f;
    public float damageTickInterval = 1.0f;
    public int damageAmount = 10;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;
    private float spawnTimer;
    private float damageTimer;
    private int maxTrailLength;
    private Queue<GameObject> trailBulletQueue;
    private float trailWdith;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        maxTrailLength = Mathf.FloorToInt(trailLengthTime / spawnInterval);
        trailBulletQueue = new Queue<GameObject>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailWdith = trailRenderer.startWidth;

        trailRenderer.time = trailLengthTime;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawntrailBullet();
            spawnTimer = 0;
        }

        if (damageTimer >= damageTickInterval)
        {
            DamageEnemies();
            damageTimer = 0;
        }
    }

    private void SpawntrailBullet()
    {
        Vector3 spawnPosition = transform.position;
        GameObject newtrailBullet;
        if (trailBulletQueue.Count < maxTrailLength)
        {
            newtrailBullet = Instantiate(trailColliderPrefab, spawnPosition, Quaternion.identity);
            newtrailBullet.transform.localScale = Vector3.one * trailWdith;
        }
        else
        {
            newtrailBullet = trailBulletQueue.Dequeue();
            newtrailBullet.transform.position = spawnPosition;
        }

        trailBulletQueue.Enqueue(newtrailBullet);
    }

    private void DamageEnemies()
    {
        StartCoroutine(FlashTrail());
        foreach (GameObject trailBullet in trailBulletQueue)
        {
            Collider[] overlappingColliders = Physics.OverlapSphere(trailBullet.transform.position, trailWdith / 2);
            foreach (Collider col in overlappingColliders)
            {
                S_EnemyHealthController enemy = col.gameObject.GetComponent<S_EnemyHealthController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
        }
    }

    
    private IEnumerator FlashTrail()
    {
        Color originalColor = trailRenderer.startColor;
        trailRenderer.startColor = flashColor;
        trailRenderer.endColor = flashColor;

        yield return new WaitForSeconds(flashDuration);

        trailRenderer.endColor = originalColor;
        trailRenderer.startColor = originalColor;
    }
}