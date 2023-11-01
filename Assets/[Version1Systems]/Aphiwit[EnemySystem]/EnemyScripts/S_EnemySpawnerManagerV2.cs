using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawnerManagerV2 : MonoBehaviour
{
    [SerializeField] S_WinTimer timer;
    [SerializeField] GameObject[] _enemyPatternSpawnerArray;
    [SerializeField] List<S_EnemyPatternSpawner> _enemyPatternSpawnerList; // The current list type is not fully supported
    [SerializeField] int _currentPatternSpawnerPhase;

    // Start is called before the first frame update
    void Start()
    {
        _currentPatternSpawnerPhase = timer.currentPhase;

        foreach (GameObject spawner in _enemyPatternSpawnerArray)
        {
            _enemyPatternSpawnerList.Add(spawner.gameObject.GetComponent<S_EnemyPatternSpawner>());
            spawner.SetActive(false);
        }

        if (_enemyPatternSpawnerArray[0] != null)
        {
            // Set active the first spawner if the list is not empty
            _enemyPatternSpawnerArray[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPatternSpawnerPhase != timer.currentPhase && _currentPatternSpawnerPhase !> _enemyPatternSpawnerArray.Length)
        {
            _currentPatternSpawnerPhase = timer.currentPhase;
            if (_enemyPatternSpawnerArray != null)
            {
                foreach (S_EnemyPatternSpawner spawner in _enemyPatternSpawnerList)
                {
                    spawner._currentSpawnerPhase++;                
                }
            }
        }
    }
}
