using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyPatternSpawner : MonoBehaviour
{
    [Header("Necessary Variables for Pattern Spawner")]
    [SerializeField] EnemyPattern[] enemyPatterns;
    [SerializeField] string patternName;
    [SerializeField] enemyPatternType patternType;
    [SerializeField] int patternRepeatAmount;
    [SerializeField] Vector3 patternRepeatOffset;

    [SerializeField] GameObject[] enemyModels;
    [SerializeField] patternDesignType patternUsageType;
    [SerializeField] int enemyAmount;
    [SerializeField] float distanceBetweenEnemies;
    [SerializeField] float enemySpawnIntervall;
    [SerializeField] Vector2 patternCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
