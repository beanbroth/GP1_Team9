using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpawnMines : MonoBehaviour
{
    [SerializeField] GameObject mine;
    [SerializeField] float dropInterval = 1.5f;
    [SerializeField] float dropDistance = 1.0f; // Default distance, how far back to place the mine.

    // Start is called before the first frame update
    void Start()
    {
        // Every Drop Interval (1.5f seconds) drop a mine forever.
        InvokeRepeating(nameof(DropMines), 0f, dropInterval);
    }

    void DropMines()
    {
        // Spawn the mine at the enemy's position or just behind it
        Vector3 spawnPosition = transform.position - transform.forward * dropDistance;
        Quaternion spawnRotation = Quaternion.identity;
        ObjectPoolManager.Instantiate(mine, spawnPosition, spawnRotation);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
