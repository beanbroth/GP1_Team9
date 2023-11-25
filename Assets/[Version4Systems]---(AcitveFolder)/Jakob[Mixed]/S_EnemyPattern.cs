using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum enemyPatternType2
{
    row,
    line,
    cone,
    circle,
    spiral,
    v,
    reverseV,
    horizontalWalls,
    verticalWalls
}

public class S_EnemyPattern : MonoBehaviour
{
    [SerializeField] enemyPatternType2 patternType;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float spacing = 3f;
    [SerializeField] string enemyPrefabPattern = "0";
    string fullPatternString = "";
    [SerializeField] bool turnTowardsParent = true;
    [Header("Type Specific Vairables")]
    [SerializeField] float radius = 22.5f;
    [SerializeField] float spiralDelay = 0.25f;
    bool gameIsPaused = false;
    private void OnEnable()
    {
        PauseManager.OnPauseStateChange += OnPauseChange;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseStateChange -= OnPauseChange;
    }
    private void OnPauseChange(bool gamePaused)
    {
        gameIsPaused = gamePaused;
    }

    private void Awake()
    {
        if (numberOfEnemies > enemyPrefabPattern.Length)
        {
            float patternRepetition = numberOfEnemies / enemyPrefabPattern.Length;
            for(int i = 0; i <= Mathf.CeilToInt(patternRepetition); i++)
            {
                fullPatternString += enemyPrefabPattern;
            }
        }
        else
        {
            fullPatternString = enemyPrefabPattern;
        }
        switch (patternType)
        {
            case enemyPatternType2.spiral:
                StartCoroutine(SpawnSpiral());
                return;
            case enemyPatternType2.row:
                SpawnRow(numberOfEnemies,false, 0);
                break;
            case enemyPatternType2.line:
                SpawnRow(numberOfEnemies,true, 0);
                break;
            case enemyPatternType2.circle:
                SpawnCircle();
                break;
            case enemyPatternType2.cone:
                SpawnCone();
                break;
            case enemyPatternType2.v:
                SpawnV();
                break;
            case enemyPatternType2.reverseV:
                SpawnReverseV();
                break;
            case enemyPatternType2.horizontalWalls:
                SpawnWalls(false);
                break;
            case enemyPatternType2.verticalWalls:
                SpawnWalls(true);
                break;
        }
        if (turnTowardsParent)
        {
            transform.LookAt(transform.parent.position);
        }
        DetatchChildrenAndDestroy();
    }
    int GetEnemyIndexInEnemyPattern(int i)
    {
        return int.Parse(fullPatternString[i].ToString());
    }

    private void SpawnCircle()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float angle = i * 360f / numberOfEnemies;
            GameObject orbitingObject =
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(i)], Vector3.zero, Quaternion.identity, transform);
            Vector3 spawnPosition =
                new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            orbitingObject.transform.localPosition = spawnPosition;
            if (turnTowardsParent)
            {
                orbitingObject.transform.LookAt(transform.parent.position);
            }
        }
    }
    IEnumerator SpawnSpiral()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            while (gameIsPaused)
            {
                yield return null;
            }
            float angle = i * 360f / numberOfEnemies;
            GameObject orbitingObject =
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(i)], Vector3.zero, Quaternion.identity, transform);
            Vector3 spawnPosition =
                new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            orbitingObject.transform.localPosition = spawnPosition;
            if (turnTowardsParent)
            {
                orbitingObject.transform.LookAt(transform.parent.position);
            }
            orbitingObject.transform.parent = null;
            yield return new WaitForSeconds(spiralDelay);
        }
        DetatchChildrenAndDestroy();
    }

    void DetatchChildrenAndDestroy()
    {
        transform.DetachChildren();
        Destroy(gameObject);
    }

    void SpawnWalls(bool vertical)
    {
        if (vertical)
        {
            SpawnRow(numberOfEnemies,true,radius);
            SpawnRow(numberOfEnemies, true, -radius);
        }
        else
        {
            SpawnRow(numberOfEnemies, false, radius);
            SpawnRow(numberOfEnemies, false, -radius);
        }
    }
    void SpawnRow(int amount, bool vertical = false, float relativeOffset = 0, int startPatternIndex = 0)
    {
        if (!vertical)
        {
            float startX = -(amount * spacing) / 2;
            float posX = startX;
            for (int i = 0; i < amount; i++)
            {
                int idx = startPatternIndex + i;
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(idx)], new Vector3(posX, 0, relativeOffset), Quaternion.identity, transform);
                posX += spacing;
            }
        }
        else
        {
            float startY = -(amount * spacing) / 2;
            float posY = startY;
            for (int i = 0; i < amount; i++)
            {
                int idx = startPatternIndex + i;
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(idx)], new Vector3(relativeOffset, 0, posY), Quaternion.identity, transform);
                posY += spacing;
            }
        }
    }

    void SpawnCone()
    {
        List<int> configurations = new List<int> { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66};
        if (configurations.Contains(numberOfEnemies))
        {
            int numberOfRows = configurations.IndexOf(numberOfEnemies);
            int spawnCount = 0;
            for (int i = 1; i <= numberOfRows; i++)
            {
                SpawnRow(i,false,-spacing*(i-1),spawnCount);
                spawnCount += i;
            }
        }
        else
        {
            print("Enemy Cone Pattern: Number of enemies incompatible with configurations.");
        }
    }
    void SpawnV()
    {
        List<int> configurations = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27 };
        if (configurations.Contains(numberOfEnemies))
        {
            int numberOfRows = Mathf.CeilToInt(numberOfEnemies/2);
            ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(0)], Vector3.zero, Quaternion.identity, transform);
            for (int i = 1; i <= numberOfRows; i++)
            {
                float relativeOffset = (spacing * i)/2;
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(i)], new Vector3(relativeOffset, 0, -spacing * i), Quaternion.identity, transform);
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(i)], new Vector3(-relativeOffset, 0, -spacing * i), Quaternion.identity, transform);
            }
        }
        else
        {
            print("Enemy V Pattern: Number of enemies incompatible with configurations.");
        }
    }
    void SpawnReverseV()
    {
        List<int> configurations = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27 };
        if (configurations.Contains(numberOfEnemies))
        {
            int numberOfRows = Mathf.CeilToInt(numberOfEnemies / 2);
            //ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(0)], Vector3.zero, Quaternion.identity, transform);
            for (int i = numberOfRows; i >= 0; i--)
            {
                float relativeOffset = (spacing * i) / 2;
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern((numberOfRows - i))], new Vector3(relativeOffset, 0, -spacing * (numberOfRows-i)), Quaternion.identity, transform);
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern((numberOfRows - i))], new Vector3(-relativeOffset, 0, -spacing * (numberOfRows - i)), Quaternion.identity, transform);
            }
        }
        else
        {
            print("Enemy V Pattern: Number of enemies incompatible with configurations.");
        }
    }
}
