using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable] struct spawnersPerPhase
{
    public GameObject[] spawners;
}
public class S_PatternEnemySpawnerManagerV2 : MonoBehaviour
{
    [SerializeField] S_WinTimer timer;
    [SerializeField]
    spawnersPerPhase[] _spawnersToEnable, _spawnersToDisable;
    [SerializeField] bool disableSpawnersOnWin = true,disableSpawnersOnAwake = true;

    private void OnEnable()
    {
        S_WinTimer.enemySpawnerUpdate += ActivateSpawner;
        S_WinTimer.enemySpawnerUpdate += DisableSpawner;
        if (disableSpawnersOnWin)
            S_WinTimer.winEvent += DisableAllSpawners;
    }
    private void OnDisable()
    {
        S_WinTimer.enemySpawnerUpdate -= ActivateSpawner;
        S_WinTimer.enemySpawnerUpdate -= DisableSpawner;
        if (disableSpawnersOnWin)
            S_WinTimer.winEvent -= DisableAllSpawners;
    }

    private void Awake()
    {
        if(disableSpawnersOnAwake)
            DisableAllSpawners();
    }
    private void Start()
    {
        ActivateSpawner(1);
    }

    void DisableAllSpawners()
    {
        foreach (spawnersPerPhase spawners in _spawnersToEnable)
        {
            foreach(GameObject spawner in spawners.spawners)
            {
                spawner.SetActive(false);
            }
        }
    }


    void ActivateSpawner(int phase)
    {
        int phaseAdjustedForIndex = phase - 1;
        if(phaseAdjustedForIndex < _spawnersToEnable.Length && phaseAdjustedForIndex >= 0)
        {
            if (_spawnersToEnable[phaseAdjustedForIndex].spawners.Length > 0)
            {
                foreach(GameObject spawner in _spawnersToEnable[phaseAdjustedForIndex].spawners)
                {
                    spawner.SetActive(true);
                }
            }
            else
            {
                print("There are no spawners in this phase.");
            }
        }
        else
        {
            print("Phase not in _spawnersToEnable index.");
        }
    }
    void DisableSpawner(int phase)
    {
        int phaseAdjustedForIndex = phase - 1;
        if (phaseAdjustedForIndex < _spawnersToDisable.Length && phaseAdjustedForIndex >= 0)
        {
            if (_spawnersToDisable[phaseAdjustedForIndex].spawners.Length >= 0)
            {
                foreach (GameObject spawner in _spawnersToDisable[phaseAdjustedForIndex].spawners)
                {
                    spawner.SetActive(false);
                }
            }
            else
            {
                print("There are no spawners in this phase.");
            }
        }
        else
        {
            print("Phase not in _spawnersToEnable index.");
        }
    }
}
