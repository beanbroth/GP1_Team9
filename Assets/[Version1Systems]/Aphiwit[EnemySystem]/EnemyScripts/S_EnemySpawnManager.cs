using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnManager : MonoBehaviour
{
    // This script will manage the enemies and their spawn rate. 
    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] float _spawnRate;
    [SerializeField] float _spawnPointRange;
    [SerializeField] bool _isSpawning;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void SearchSpawnPoint()
    //{
    //    // Generate a random position to walk to
    //    float randomZ = UnityEngine.Random.Range(-_spawnPointRange, _spawnPointRange);
    //    float randomX = UnityEngine.Random.Range(-_spawnPointRange, _spawnPointRange);

    //    _spawnPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //        // Check if point generated exists
    //        if (Physics.Raycast(_spawnPoint, -transform.up, 2f, groundLayerMask))
    //        {
    //            _spawnPointSet = true;
    //        }
    //}

}
