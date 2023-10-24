using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GeneralMethods 
{
    public static (bool, Vector3) GeneratePointOnNavMesh(Transform transform, float minSpawnRange, float maxSpawnRange, Vector3 spawnPoint, LayerMask walkableLayerMask, bool spawnPointSet)
    {
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        
        float distance = UnityEngine.Random.Range(minSpawnRange, maxSpawnRange);

        
        float randomX = transform.position.x + distance * Mathf.Cos(angle);
        float randomZ = transform.position.z + distance * Mathf.Sin(angle);

        spawnPoint = new Vector3(randomX, transform.position.y, randomZ);

        
        if (Physics.Raycast(spawnPoint, -transform.up, 2f, walkableLayerMask))
        {
            spawnPointSet = true;
        }

        Debug.Log("spawnpoint: " + spawnPoint);
        return (spawnPointSet, spawnPoint);
    }
}
