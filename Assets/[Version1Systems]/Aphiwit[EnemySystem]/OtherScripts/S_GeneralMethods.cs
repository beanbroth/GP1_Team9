using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GeneralMethods 
{
    public static (bool, Vector3) GeneratePointOnNavMesh(Transform transform, float minSpawnRange, float maxSpawnRange, Vector3 spawnPoint, LayerMask walkableLayerMask, bool spawnPointSet)
    {
        // Generate a random position to walk or spawn to
        float randomZ = UnityEngine.Random.Range(minSpawnRange, maxSpawnRange);
        float randomX = UnityEngine.Random.Range(minSpawnRange, maxSpawnRange);

        spawnPoint = new Vector3(randomX, transform.position.y, randomZ);

        // Check if point generated exists
        if (Physics.Raycast(spawnPoint, -transform.up, 2f, walkableLayerMask))
        {
            spawnPointSet = true;
        }

        return (spawnPointSet, spawnPoint);
    }
}
