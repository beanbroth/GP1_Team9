using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    public LayerMask groundLayerMask;

    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] float _originalSpawnInterval;
    [SerializeField] float _spawnIntervalReductionValue;
    [SerializeField] float _finalSpawnInterval;

    [SerializeField] float _minSpawnRange=20f;
    [SerializeField] float _maxSpawnRange=25f;

    [SerializeField] private float _tempSpawnInterval;

    void Start()
    {
        _tempSpawnInterval = _originalSpawnInterval;
    }

    void Update()
    {
        // SpawnTimer gets reduced every second by a intervall variable and time
        _tempSpawnInterval -= Time.deltaTime;

        if (_tempSpawnInterval < 0)
        {
            SpawnEnemy();
        }

        if (_tempSpawnInterval > _finalSpawnInterval)
        {
            _tempSpawnInterval -= _spawnIntervalReductionValue * Time.deltaTime;
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        Vector3 spawnPoint;
        float randomAngle = Random.Range(0, 360);
        float randomDistance = Random.Range(_minSpawnRange, _maxSpawnRange);

        spawnPoint = new Vector3(transform.position.x + randomDistance * Mathf.Cos(randomAngle), 0, transform.position.z + randomDistance * Mathf.Sin(randomAngle));

        RaycastHit hit;
        if (Physics.Raycast(spawnPoint + Vector3.up * 100, Vector3.down, out hit, 200, groundLayerMask))
        {
            spawnPoint = hit.point;
        }

        return spawnPoint;
    }

    private float SpawnEnemy()
    {
        Vector3 spawnPoint = GenerateSpawnPoint();
        Instantiate(_enemyPrefabList[Random.Range(0, _enemyPrefabList.Length)], spawnPoint, Quaternion.identity);
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