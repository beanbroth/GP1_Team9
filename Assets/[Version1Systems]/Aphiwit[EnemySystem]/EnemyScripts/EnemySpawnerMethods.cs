using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnerMethods : MonoBehaviour
{

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

    public float GetRandomCooldown(float minPatternCooldown, float maxPatternCooldown)
    {
        return UnityEngine.Random.Range(minPatternCooldown, maxPatternCooldown);
    }
}
