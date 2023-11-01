using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnerMethods : MonoBehaviour
{
    private (Vector3, bool) NewGenerateRandom360SpawnPoint()
    {
        // Values to return
        Vector3 spawnPoint;
        bool canSpawn = false;

        float randomAngle = UnityEngine.Random.Range(0, 360);
        float randomDistance = UnityEngine.Random.Range(_minSpawnRange, _maxSpawnRange);

        spawnPoint = new Vector3(transform.position.x + randomDistance * Mathf.Cos(randomAngle), 0, transform.position.z + randomDistance * Mathf.Sin(randomAngle));

        var updatedResult = CheckIfSpawnPointExist(spawnPoint, canSpawn);
        spawnPoint = updatedResult.Item1;
        canSpawn = updatedResult.Item2;

        return (spawnPoint, canSpawn);
    }

    public (Vector3, bool) CheckIfSpawnPointExist(Vector3 position, bool returnValue)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
        {
            position = hit.position;
            return (position, true);
        }
        else
        {
            return (position, false);
        }
    }

    public float GetRandomPatternCooldown(float minPatternCooldown, float maxPatternCooldown)
    {
        return UnityEngine.Random.Range(minPatternCooldown, maxPatternCooldown);
    }
}
