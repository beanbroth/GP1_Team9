using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemySpawner : MonoBehaviour
{
    public LayerMask groundLayerMask;

    [SerializeField] string _spawnerType;

    [Header("For normal spawner")]
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


    private float _tempSpawnInterval;

    Transform _player;

    void Start()
    {
        _tempSpawnInterval = _originalSpawnInterval;
        _inSceneCooldown = GetRandomPatternCooldown();
        _player = FindFirstObjectByType<S_PlayerMovement>().transform;
    }

    float GetRandomPatternCooldown()
    {
        return Random.Range(_patternCooldown.x, _patternCooldown.y);
    }

    void Update()
    {
        if (_spawnerType == "normal")
        {
            // SpawnTimer gets reduced every second by a intervall variable and time
            _tempSpawnInterval -= Time.deltaTime;

            if (_tempSpawnInterval < 0) { RandomSpawnEnemy(); }

            // Foreach second that passes; reduce og value by set reduction value.
            if (_originalSpawnInterval > _finalSpawnInterval) 
            { 
                _originalSpawnInterval -= _spawnIntervalReductionValue * Time.deltaTime; 
            }

            if (_originalSpawnInterval <= _finalSpawnInterval)
            {
                Debug.Log("Spawn interval has reached stone bottom!");
            }
        } 
        
        else if (_spawnerType == "pattern")
        {
            _inSceneCooldown -= Time.deltaTime;

            // Temp values
            bool canSpawn = false;
            Vector3 enemyPatternBoxPosition = transform.position;

            // Check if pattern box is inside navmesh
            var spawnCheck = CheckIfSpawnPointExist(enemyPatternBoxPosition, canSpawn);
            enemyPatternBoxPosition = spawnCheck.Item1;
            canSpawn = spawnCheck.Item2;

            if (canSpawn && _inSceneCooldown < 0 && enemyPatternBoxPosition != null)
            {
                // Spawns enemy pattern on the location of the spawner
                ObjectPoolManager.Instantiate(_enemyPatternList[Random.Range(0, _enemyPrefabList.Length)], enemyPatternBoxPosition, _player.rotation);
                _inSceneCooldown = GetRandomPatternCooldown();
            }
            
        }
    }

    private (Vector3, bool) GenerateRandomSpawnPoint()
    {
        Vector3 spawnPoint;
        bool returnValue = false;
        float randomAngle = Random.Range(0, 360);
        float randomDistance = Random.Range(_minSpawnRange, _maxSpawnRange);

        spawnPoint = new Vector3(transform.position.x + randomDistance * Mathf.Cos(randomAngle), 0, transform.position.z + randomDistance * Mathf.Sin(randomAngle));

        var updatedResult = CheckIfSpawnPointExist(spawnPoint, returnValue);
        spawnPoint = updatedResult.Item1;
        returnValue = updatedResult.Item2;

        return (spawnPoint, returnValue);
    }

    private (Vector3, bool) CheckIfSpawnPointExist(Vector3 position, bool returnValue)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
        {
            position = hit.position;
            return (position, true);
        } else
        {
            return (position, false);
        }            
    }

    private float RandomSpawnEnemy()
    {
        var result = GenerateRandomSpawnPoint();
        Vector3 spawnPoint = result.Item1;
        bool canSpawn = result.Item2;
        if (canSpawn)
        {
            Debug.Log("can spawn!");
            ObjectPoolManager.Instantiate(_enemyPrefabList[Random.Range(0, _enemyPrefabList.Length)], spawnPoint, Quaternion.identity);
        }
        return _tempSpawnInterval = _originalSpawnInterval;
    }

    //private float SpawnEnemyPattern()
    //{
    //    foreach (GameObject enemy in enemyPattern)
    //    {
    //        // CheckIfSpawnPointExist(EnemyPatterns transform.position, bool canSpawn);
    //        var result = CheckIfSpawnPointExist(enemy.transform.position, bool);


    //        bool canSpawn
    //        if (canSpawn)
    //        {
    //            ObjectPoolManager.Instantiate();
    //        }
    //    }

    //    return _tempSpawnInterval = _originalSpawnInterval;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minSpawnRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxSpawnRange);
    }
}