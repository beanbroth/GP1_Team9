using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnManager : MonoBehaviour
{
    public LayerMask groundLayerMask;

    // This script will manage the enemies and their spawn rate. 
    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] Vector3 _spawnPoint;
    [SerializeField] float _spawnRate;
    [SerializeField] float _spawnPointRange;
    [SerializeField] bool _spawnPointExist;
    [SerializeField] bool _isSpawning;
    
    private float _spawnTimer;

    S_GeneralMethods generalMethods;
    // S_GeneralVariables generalVariables;

    void Start()
    {
        _spawnTimer = _spawnRate;

        // generalVariables = gameObject.AddComponent<S_GeneralVariables>();
        // generalMethods = gameObject.AddComponent<S_GeneralMethods>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateSpawnPoint();

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer < 0)
        {
            _spawnTimer = _spawnRate;
            Instantiate(_enemyPrefabList[UnityEngine.Random.Range(0, _enemyPrefabList.Length)], _spawnPoint, Quaternion.identity);
        }
    }

    private void GenerateSpawnPoint()
    {
        // Generate a random position to spawn on
        float randomZ = UnityEngine.Random.Range(-_spawnPointRange, _spawnPointRange);
        float randomX = UnityEngine.Random.Range(-_spawnPointRange, _spawnPointRange);

        _spawnPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if point generated exists
        if (!Physics.Raycast(_spawnPoint, -transform.up, 2f, groundLayerMask))
        {
            _spawnPointExist = true;
            Debug.Log("Spawn point found!");
        }
    }
}
