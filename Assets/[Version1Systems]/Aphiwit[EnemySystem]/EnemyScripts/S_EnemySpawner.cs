using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemySpawner : MonoBehaviour
{
    public LayerMask groundLayerMask;

    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] float _originalSpawnInterval;
    [SerializeField] float _spawnIntervalReductionValue;
    [SerializeField] float _finalSpawnInterval;

    [SerializeField] float _minSpawnRange=20f;
    [SerializeField] float _maxSpawnRange=25f;

    [Header("For pattern spawner")]
    [SerializeField] GameObject[] _enemyPatternList;
    [SerializeField] Vector2 _patternCooldown;
    private float _inSceneCooldown;
    [SerializeField] private float _tempSpawnInterval;

    void Start()
    {
        _tempSpawnInterval = _originalSpawnInterval;
    }

    void Update()
    {
        // SpawnTimer gets reduced every second by a intervall variable and time
        _tempSpawnInterval -= Time.deltaTime;

        if (_tempSpawnInterval < 0) { SpawnEnemy(); }

        // Foreach second that passes; reduce og value by set reduction value.
        if (_originalSpawnInterval > _finalSpawnInterval) { _originalSpawnInterval -= _spawnIntervalReductionValue * Time.deltaTime; }

        if (_originalSpawnInterval <= _finalSpawnInterval)
        {
            Debug.Log("Spawn interval has reached stone bottom!");
        }
    }

    private (Vector3, bool) GenerateSpawnPoint()
    {
        Vector3 spawnPoint;
        float randomAngle = Random.Range(0, 360);
        float randomDistance = Random.Range(_minSpawnRange, _maxSpawnRange);

        spawnPoint = new Vector3(transform.position.x + randomDistance * Mathf.Cos(randomAngle), 0, transform.position.z + randomDistance * Mathf.Sin(randomAngle));

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            spawnPoint = hit.position; 
            //Debug.Log("Enemy inside navmesh!");
            return (spawnPoint, true);
        }

        return (spawnPoint, false);
    }

    private float SpawnEnemy()
    {
        var result = GenerateSpawnPoint();
        Vector3 spawnPoint = result.Item1;
        bool canSpawn = result.Item2;
        if (canSpawn)
        {
            ObjectPoolManager.Instantiate(_enemyPrefabList[Random.Range(0, _enemyPrefabList.Length)], spawnPoint, Quaternion.identity);
            canSpawn = false;
        }
        return _tempSpawnInterval = _originalSpawnInterval;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minSpawnRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxSpawnRange);
    }
}