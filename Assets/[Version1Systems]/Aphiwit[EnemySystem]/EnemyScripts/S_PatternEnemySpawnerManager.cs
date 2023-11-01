using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class S_PatternEnemySpawnerManager : MonoBehaviour
{
    public int CurrentPatternManagerPhase;

    [SerializeField] bool staticArray;
    [SerializeField] S_WinTimer _timer;
    [SerializeField] float[] _keyTimeFrames;
    [SerializeField] List<GameObject> _enemyPatternSpawnerArray;
    [SerializeField] List<S_EnemyPatternSpawner> _enemyPatternSpawnerList; 

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("Pattern manager has no connected patterns! ");
            gameObject.SetActive(false);
        }
        else if (transform.childCount != 0)
        {
            Debug.Log("Connected child objects: " + transform.childCount);
            Debug.Log("Array length in manager: " + _enemyPatternSpawnerArray.Count);

            if (!staticArray)
            {
                if (_enemyPatternSpawnerArray.Count != transform.childCount)
                {
                    _enemyPatternSpawnerArray.Clear();

                    // Add connected spawners to _enemyPatternSpawnerArray
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        GameObject _childSpawner = transform.GetChild(i).gameObject;
                        if (_childSpawner.gameObject.GetComponent<S_EnemyPatternSpawner>() != null)
                        {
                            _enemyPatternSpawnerArray.Add(_childSpawner);
                        }
                    }
                }
            }

            Debug.Log("New Array length in manager: " + _enemyPatternSpawnerArray.Count);
        }

        _timer = FindFirstObjectByType<S_WinTimer>();

        CurrentPatternManagerPhase = _timer.currentPhase;

        if (_enemyPatternSpawnerArray != null)
        {
            foreach (GameObject spawner in _enemyPatternSpawnerArray)
            {
                if (spawner.gameObject.GetComponent<S_EnemyPatternSpawner>() != null)
                {
                    _enemyPatternSpawnerList.Add(spawner.gameObject.GetComponent<S_EnemyPatternSpawner>());
                }
                spawner.SetActive(false);
            }
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
        if (CurrentPatternManagerPhase != _timer.currentPhase)
        {
            CurrentPatternManagerPhase = _timer.currentPhase;

            if (_enemyPatternSpawnerArray != null && CurrentPatternManagerPhase !> _enemyPatternSpawnerArray.Count)
            {
                foreach (S_EnemyPatternSpawner spawner in _enemyPatternSpawnerList)
                {
                    spawner.gameObject.SetActive(true);
                    spawner._currentSpawnerPhase = CurrentPatternManagerPhase;                
                }
            }
        }
        //foreach (float keyTimeFrame in _keyTimeFrames)
        //{
        //    if (_enemyPatternSpawnerArray != null && CurrentPatternManagerPhase! > _enemyPatternSpawnerArray.Count && keyTimeFrame > )
        //    {

        //    }
        //}
    }
}
