using Unity.VisualScripting;
using UnityEngine;


public class S_EnemyPatternSpawner : EnemySpawnerMethods
{
    // Testing
    public LayerMask groundLayerMask;
    public int _currentSpawnerPhase = 0;

    [Header("For Updated Pattern Spawner")]
    [SerializeField] EnemyPhaseWithPatterns[] _enemyPhases;

    [SerializeField] EnemyPhaseWithPatterns _currentEnemyPhase;
    [SerializeField] GameObject[] _currentEnemyPatterns;

    [SerializeField] bool _randomPatternCooldown;
    [SerializeField] float _minPatternCooldown, _maxPatternCooldown;

    private float _patternCooldown;

    private Transform _playerTransform;

    [Header("Spawn Mode")]
    [SerializeField] bool useSpawnPoints = true;
    [SerializeField] Transform[] spawnPoints;
    //[Header("Scriptable Objects Related")]
    //[SerializeField] EnemyPattern[] enemyPatterns;
    //[SerializeField] string patternName;
    //[SerializeField] enemyPatternType patternType;
    //[SerializeField] int patternRepeatAmount;
    //[SerializeField] Vector3 patternRepeatOffset;

    //[SerializeField] GameObject[] enemyModels;
    //[SerializeField] patternDesignType patternUsageType;
    //[SerializeField] int enemyAmount;
    //[SerializeField] float distanceBetweenEnemies;
    //[SerializeField] float enemySpawnIntervall;
    //[SerializeField] Vector2 patternCooldown;

    private void OnEnable()
    {
        Debug.Log("Pattern Spawner Got Enabled");
        S_PatternEnemySpawnerManager _patternManagerScript = GetComponentInParent<S_PatternEnemySpawnerManager>();
        _currentSpawnerPhase = _patternManagerScript.CurrentPatternManagerPhase;
    }

    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("Current spawner phase: " + _currentSpawnerPhase);

        if (_enemyPhases == null)
        {
            Debug.Log("There exist no phases in the spawner!");
            gameObject.SetActive(false);
        }

        _patternCooldown = _maxPatternCooldown;

        _playerTransform = FindFirstObjectByType<S_PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // I have this here aswell in case future power up cleanses the _enemyPhases.
        if (_enemyPhases == null) { Debug.Log("There exist no phases in the spawner!"); return; }

        if (PauseManager.IsPaused)
        {
            return;
        }

        _patternCooldown -= Time.deltaTime;
        
        if (_currentSpawnerPhase >= 0 && _currentSpawnerPhase < _enemyPhases.Length + 1)
        {
            _currentEnemyPhase = _enemyPhases[0];//[_currentSpawnerPhase - 1];
            _currentEnemyPatterns = _currentEnemyPhase.enemyPatterns;
        }

        if (_patternCooldown <= 0)
        {
            //Vector3 enemyPatternSpawnPosition = transform.position;
            Transform spawnPoint = transform;
            if (useSpawnPoints)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            }

            bool canSpawn = false;
            (Vector3, bool) spawnCheck = CheckIfSpawnPointExist(spawnPoint.position, canSpawn);

            if (_currentEnemyPatterns != null)
            {
                GameObject enemyPattern = Instantiate(_currentEnemyPatterns[0], spawnPoint.position, useSpawnPoints ? spawnPoint.rotation : _playerTransform.rotation, spawnPoint.parent);
                print("spawned");
                //ObjectPoolManager.Instantiate(_currentEnemyPatterns[0], spawnPoint.position, useSpawnPoints ? spawnPoint.rotation : _playerTransform.rotation);
                //ObjectPoolManager.Instantiate(_currentEnemyPatterns[UnityEngine.Random.Range(0, _currentEnemyPatterns.Length)], enemyPatternSpawnPosition, _playerTransform.rotation);
            }

            if (_randomPatternCooldown && _patternCooldown <= 0)
            {
                _patternCooldown = UnityEngine.Random.Range(_minPatternCooldown, _maxPatternCooldown);
            } else if (!_randomPatternCooldown && _patternCooldown <= 0)
            {
                _patternCooldown = _maxPatternCooldown;
            }
        }
    }
}
