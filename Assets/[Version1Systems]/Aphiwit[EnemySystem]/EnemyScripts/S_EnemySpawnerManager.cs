using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnerManager : MonoBehaviour
{
    // Key time points
    [SerializeField] float[] phaseTimeWindow;

    // Attached spawners
    [SerializeField] GameObject[] enemySpawners;

    private int currentIndex = 0;
    private float elapsedTime = 0;
    private bool canIncrease = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }
}
