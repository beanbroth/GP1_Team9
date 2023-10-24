using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnerManager : MonoBehaviour
{
    // Key time points
    //[SerializeField] float[] phaseTimeWindow;

    // Attached spawners
    [SerializeField] GameObject[] enemySpawners;
    [SerializeField] bool disableSpawnersOnAwake = true;

    /*private int currentIndex = 0;
    private float elapsedTime = 0;
    private bool canIncrease = false;*/

    
    // Start is called before the first frame update
    void OnEnable()
    {
        S_WinTimer.newPhase += ActivateSpawner;
    }
    void OnDisable()
    {
        S_WinTimer.newPhase -= ActivateSpawner;
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

    // Update is called once per frame
    /*void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= phaseTimeWindow[currentIndex])
        {
            enemySpawners[currentIndex].SetActive(true);
            canIncrease = true;
        }

        if (canIncrease && currentIndex != phaseTimeWindow.Length - 1)
        {
            currentIndex++;
            canIncrease = false;
        }
    }*/
    void ActivateSpawner(int spawnerIndex)
    {
        enemySpawners[spawnerIndex-1].SetActive(true);
    }
}
