using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum enemySpawnerType
{
    normal,
    pattern
}

public class S_NormalEnemySpawner : EnemySpawnerMethods
{
    public LayerMask groundLayerMask;

    [SerializeField] int _currentSpawnerPhase = 1; 
    [SerializeField] enemySpawnerType _spawnerType;

    [Header("For normal spawner")]
    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] float _originalSpawnInterval;
    [SerializeField] float _spawnIntervalReductionValue;
    [SerializeField] float _finalSpawnInterval;
    [SerializeField] float _minSpawnRange=20f;
    [SerializeField] float _maxSpawnRange=25f;

    [Header("For pattern spawner")]
    [SerializeField] EnemyPhaseWithPatterns[] _enemyPhaseAndPatternArrays;
    [SerializeField] EnemyPhaseWithPatterns _currentSelectedElement;
    [SerializeField] GameObject[] _currentEnemyPattern;

    [SerializeField] float minPatternCooldown, maxPatternCooldown;
    [SerializeField] Vector2 _patternCooldown;
    private float _inSceneCooldown;

    private float _tempSpawnInterval;

    Transform _player;

    void Start()
    {  
        _tempSpawnInterval = _originalSpawnInterval;
        _inSceneCooldown = GetRandomPatternCooldown(minPatternCooldown, maxPatternCooldown);
        _player = FindFirstObjectByType<S_PlayerMovement>().transform;
        _currentSpawnerPhase = GetComponentInParent<S_EnemySpawnerManager>().currentPhaseIndex;
    }

    void Update()
    {
        _currentSpawnerPhase = GetComponentInParent<S_EnemySpawnerManager>().currentPhaseIndex;

        if (PauseManager.IsPaused)
            return;

        if (_spawnerType == enemySpawnerType.normal)
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
        
        else if (_spawnerType == enemySpawnerType.pattern)
        {
            _inSceneCooldown -= Time.deltaTime;

            // START OF NEW STUFF
            if (_enemyPhaseAndPatternArrays.Length == 0)
            {
                print("Different element array is empty");
            }

            print("Phase and pattern array length: " + _enemyPhaseAndPatternArrays.Length);

            // Add if statement here to reduce phase index if it's more than array length

            if (_currentSpawnerPhase >= 0 && _currentSpawnerPhase < _enemyPhaseAndPatternArrays.Length)
            {
                // Code here is useable as long as conditions fufilled
                _currentSelectedElement = _enemyPhaseAndPatternArrays[_currentSpawnerPhase - 1];
                _currentEnemyPattern = _currentSelectedElement.enemyPatterns;
            }

            if (_currentEnemyPattern.Length == 0)
            {
                //print("Enemy pattern to use is empty! ");
            }

            // END OF NEW STUFF

            // Temp values
            bool canSpawn = false;
            Vector3 enemyPatternBoxPosition = transform.position;

            // Check if pattern box is inside navmesh
            var spawnCheck = CheckIfSpawnPointExist(enemyPatternBoxPosition, canSpawn);
            enemyPatternBoxPosition = spawnCheck.Item1;
            canSpawn = spawnCheck.Item2;

            if (canSpawn && _inSceneCooldown < 0 && enemyPatternBoxPosition != null && _currentEnemyPattern.Length != 0)
            {
                // Spawns enemy pattern on the location of the spawner
                ObjectPoolManager.Instantiate(_currentEnemyPattern[UnityEngine.Random.Range(0, _currentEnemyPattern.Length)], enemyPatternBoxPosition, _player.rotation);
                _inSceneCooldown = GetRandomPatternCooldown(minPatternCooldown, maxPatternCooldown);
            }
        }

        // print("current spawner phase: " + currentSpawnerPhase);
    }

    private (Vector3, bool) GenerateRandomSpawnPoint()
    {
        Vector3 spawnPoint;
        bool returnValue = false;
        float randomAngle = UnityEngine.Random.Range(0, 360);
        float randomDistance = UnityEngine.Random.Range(_minSpawnRange, _maxSpawnRange);

        spawnPoint = new Vector3(transform.position.x + randomDistance * Mathf.Cos(randomAngle), 0, transform.position.z + randomDistance * Mathf.Sin(randomAngle));

        var updatedResult = CheckIfSpawnPointExist(spawnPoint, returnValue);
        spawnPoint = updatedResult.Item1;
        returnValue = updatedResult.Item2;

        return (spawnPoint, returnValue);
    }
    
    private float RandomSpawnEnemy()
    {
        var result = GenerateRandomSpawnPoint();
        Vector3 spawnPoint = result.Item1;
        bool canSpawn = result.Item2;
        if (canSpawn)
        {
            ObjectPoolManager.Instantiate(_enemyPrefabList[UnityEngine.Random.Range(0, _enemyPrefabList.Length)], spawnPoint, Quaternion.identity);
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