using System.Linq;
using Unity.VisualScripting;
using UnityEngine;




public class S_EnemyPatternSpawnerV2 : EnemySpawnerMethods
{
    // Testing
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] GameObject[] enemyPatterns;

    [SerializeField] bool _useRandomPatternCooldown;
    [SerializeField] float _minPatternCooldown, _maxPatternCooldown;

    private float _patternCooldown;

    private Transform _playerTransform;

    [Header("Spawn Mode")]
    [SerializeField] bool useSpawnPoints = true;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform centerSpawnPoint;
    [SerializeField] int[] patternIndexesToSpawnInCenter;

    private void OnEnable()
    {
        Debug.Log("Pattern Spawner Got Enabled");
        S_PatternEnemySpawnerManager _patternManagerScript = GetComponentInParent<S_PatternEnemySpawnerManager>();
    }

    private void Awake()
    {
        _playerTransform = FindFirstObjectByType<S_PlayerMovement>().transform;
        _patternCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyPatterns.Length <= 0 || enemyPatterns == null)
        {
            return;
        }
        if (PauseManager.IsPaused)
        {
            return;
        }

        _patternCooldown -= Time.deltaTime;

        if (_patternCooldown <= 0)
        {
            Transform spawnPoint = transform;
            if (useSpawnPoints)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            }

            bool canSpawn = false;
            (Vector3, bool) spawnCheck = CheckIfSpawnPointExist(spawnPoint.position, canSpawn);
            if (!spawnCheck.Item2)
            {
                _patternCooldown = Mathf.Min(5, GetRandomCooldown());
                return;
            }
            
            int patternIndex = Random.Range(0, enemyPatterns.Length);
            if (patternIndexesToSpawnInCenter.Length > 0 && patternIndexesToSpawnInCenter.Contains(patternIndex))
            {
                spawnPoint = centerSpawnPoint;
            }

            GameObject enemyPattern = Instantiate(enemyPatterns[patternIndex], spawnPoint.position, useSpawnPoints ? spawnPoint.rotation : _playerTransform.rotation, spawnPoint.parent);
            print("spawned");
            //ObjectPoolManager.Instantiate(_currentEnemyPatterns[0], spawnPoint.position, useSpawnPoints ? spawnPoint.rotation : _playerTransform.rotation);
            //ObjectPoolManager.Instantiate(_currentEnemyPatterns[UnityEngine.Random.Range(0, _currentEnemyPatterns.Length)], enemyPatternSpawnPosition, _playerTransform.rotation);

            _patternCooldown = GetRandomCooldown();
        }
    }
    float GetRandomCooldown()
    {
        if (!_useRandomPatternCooldown)
        {
            return _maxPatternCooldown;
        }
        else
        {
            return UnityEngine.Random.Range(_minPatternCooldown, _maxPatternCooldown);
        }
    }
}
