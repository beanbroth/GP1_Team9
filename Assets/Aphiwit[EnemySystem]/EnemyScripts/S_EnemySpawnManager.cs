using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnManager : MonoBehaviour
{
    // This script will manage the enemies and their spawn rate. 
    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] float _spawnRate;
    [SerializeField] bool _isSpawning;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
