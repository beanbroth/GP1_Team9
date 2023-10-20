using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnManagerV2 : MonoBehaviour
{
    public LayerMask groundLayerMask;

    [SerializeField] GameObject[] _enemyPrefabList;
    [SerializeField] float _spawnRate;
    [SerializeField] float _minSpawnRange=20f;
    [SerializeField] float _maxSpawnRange=25f;

    private float _spawnTimer;

    void Start()
    {
        _spawnTimer = _spawnRate;
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer < 0)
        {
            Vector3 spawnPoint = GenerateSpawnPoint();
            Instantiate(_enemyPrefabList[Random.Range(0, _enemyPrefabList.Length)], spawnPoint, Quaternion.identity);
            _spawnTimer = _spawnRate;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minSpawnRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxSpawnRange);
    }
}