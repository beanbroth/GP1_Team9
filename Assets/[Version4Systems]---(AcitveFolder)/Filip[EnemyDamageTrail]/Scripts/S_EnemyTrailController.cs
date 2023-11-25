using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class S_EnemyTrailController : MonoBehaviour
{
    public GameObject trailColliderPrefab;
    public float spawnInterval = 1.0f;
    public float trailLengthTime = 10.0f;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;
    private float spawnTimer;
    private int maxTrailLength;
    private Queue<GameObject> trailBulletQueue;
    private float trailWdith;
    private TrailRenderer trailRenderer;
    private bool isOnEnemy;

    private void Start()
    {
        isOnEnemy = gameObject.transform.root.tag == "Enemy";
        maxTrailLength = Mathf.FloorToInt(trailLengthTime / spawnInterval);
        trailBulletQueue = new Queue<GameObject>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailWdith = trailRenderer.startWidth;

        trailRenderer.time = trailLengthTime;
    }

    private void Update()
    {
        if (!isOnEnemy)
        {
            transform.localPosition = new Vector3(0f, 0f, 3 + math.sin(Time.time * 3) * 3f);
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawntrailBullet();
            spawnTimer = 0;
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