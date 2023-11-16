using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnerManager : MonoBehaviour
{
    // Key time points
    //[SerializeField] float[] phaseTimeWindow;

    // Attached Spawners
    [SerializeField] GameObject[] enemySpawners;
    [SerializeField] GameObject[] enemySpawnersToDisable;
    [SerializeField] bool disableSpawnersOnAwake = true;
    [System.NonSerialized] public int currentPhaseIndex = 1;
    [SerializeField] bool disableSpawnersOnWin = true;

    // Attached Managers
    [SerializeField] S_WinTimer timer;

    /*private int currentIndex = 0;
    private float elapsedTime = 0;
    private bool canIncrease = false;*/

    
    // Start is called before the first frame update
    void OnEnable()
    {
        S_WinTimer.enemySpawnerUpdate += ActivateSpawner;
        S_WinTimer.enemySpawnerUpdate += DeactivateSpawner;
        if (disableSpawnersOnWin)
            S_WinTimer.winEvent += DisableAllSpawners;
    }

    void OnDisable()
    {
        S_WinTimer.enemySpawnerUpdate -= ActivateSpawner;
        S_WinTimer.enemySpawnerUpdate -= DeactivateSpawner;
        if (disableSpawnersOnWin)
            S_WinTimer.winEvent -= DisableAllSpawners;
    }
    void DisableAllSpawners()
    {
        foreach (GameObject spawner in enemySpawners)
        {
            spawner.SetActive(false);
        }
    }

    private void Awake()
    {
        if (disableSpawnersOnAwake)
        {
            foreach (var spawner in enemySpawners)
            {
                spawner.SetActive(false);
            }
        }
    }
    private void Start()
    {
        ActivateSpawner(1);
    }

    private void Update()
    {
        currentPhaseIndex = timer.currentPhase;
    }
    
    void ActivateSpawner(int spawnerIndex)
    {
        if(spawnerIndex < enemySpawners.Length)
            enemySpawners[spawnerIndex-1].SetActive(true);
    }

    void DeactivateSpawner(int spawnerIndex)
    {
        if (spawnerIndex < enemySpawnersToDisable.Length)
            enemySpawnersToDisable[spawnerIndex - 1].SetActive(false);
    }
}
