using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnManagerV2 : MonoBehaviour
{
    public LayerMask groundLayerMask;

    // This script will manage the enemies and their spawn rate. 
    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] Vector3 _enemySpawnPoint;
    [SerializeField] float _spawnRate;
    [SerializeField] float _minSpawnRange;
    [SerializeField] float _maxSpawnRange;

    [SerializeField] bool _spawnPointSet;
    [SerializeField] bool _isSpawning;
    
    private float _spawnTimer;

    void Start()
    {
        _spawnTimer = _spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        var CheckSpawn = S_GeneralMethods.GeneratePointOnNavMesh(transform, _minSpawnRange, _maxSpawnRange, _enemySpawnPoint, groundLayerMask, _spawnPointSet);

        _spawnPointSet = CheckSpawn.Item1;
        _enemySpawnPoint = CheckSpawn.Item2;     

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer < 0)
        {
            _spawnTimer = _spawnRate;
            Instantiate(_enemyPrefabList[UnityEngine.Random.Range(0, _enemyPrefabList.Length)], _enemySpawnPoint, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minSpawnRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxSpawnRange);
    }
}
