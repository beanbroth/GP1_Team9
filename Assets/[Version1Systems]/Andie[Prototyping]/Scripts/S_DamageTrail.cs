using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class SpikeTrail : MonoBehaviour
{
    public GameObject spikePrefab;
    public float spawnInterval = 1.0f;
    public float trailLengthTime = 10.0f;
    public float damageTickSpeed = 1.0f;
    public int damageAmount = 10;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;
    private float spawnTimer;
    private float damageTimer;
    private int maxTrailLength;
    private Queue<GameObject> spikeQueue;
    private float trailWdith;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        maxTrailLength = Mathf.FloorToInt(trailLengthTime / spawnInterval);
        spikeQueue = new Queue<GameObject>();
        trailWdith = GetComponent<TrailRenderer>().startWidth;
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnSpike();
            spawnTimer = 0;
        }

        if (damageTimer >= damageTickSpeed)
        {
            DamageEnemies();
            damageTimer = 0;
        }
    }

    private void SpawnSpike()
    {
        Vector3 spawnPosition = transform.position;
        GameObject newSpike;
        if (spikeQueue.Count < maxTrailLength)
        {
            newSpike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity);
            newSpike.transform.localScale = Vector3.one * trailWdith;
        }
        else
        {
            newSpike = spikeQueue.Dequeue();
            newSpike.transform.position = spawnPosition;
        }

        spikeQueue.Enqueue(newSpike);
    }

    private void DamageEnemies()
    {
        StartCoroutine(FlashTrail());
        foreach (GameObject spike in spikeQueue)
        {
            Collider[] overlappingColliders = Physics.OverlapSphere(spike.transform.position, trailWdith / 2);
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
        Gradient originalGradient = trailRenderer.colorGradient;
        Gradient flashGradient = new Gradient();
        flashGradient.SetKeys(originalGradient.colorKeys, originalGradient.alphaKeys);
        GradientColorKey[] colorKeys = flashGradient.colorKeys;
        for (int i = 0; i < colorKeys.Length; i++)
        {
            colorKeys[i].color = flashColor;
        }

        flashGradient.colorKeys = colorKeys;
        trailRenderer.colorGradient = flashGradient;
        yield return new WaitForSeconds(flashDuration);
        trailRenderer.colorGradient = originalGradient;
    }
}