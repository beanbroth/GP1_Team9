using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum enemyPatternType2
{
    row,
    line,
    cone,
    circle,
    spiral
}

public class S_EnemyPattern : MonoBehaviour
{
    [SerializeField] enemyPatternType2 patternType;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int numberOfEnemies;
    [SerializeField] float spacing;
    [SerializeField] string enemyPrefabPattern = "0";
    string fullPatternString = "";
    [SerializeField] bool turnTowardsParent;
    [Header("Type Specific Vairables")]
    [SerializeField] float radius = 22.5f;
    [SerializeField] float spiralDelay = 0.25f;

    private void Awake()
    {
        if (numberOfEnemies > enemyPrefabPattern.Length)
        {
            float patternRepetition = numberOfEnemies / enemyPrefabPattern.Length;
            print(patternRepetition);
            for(int i = 0; i <= Mathf.CeilToInt(patternRepetition); i++)
            {
                fullPatternString += enemyPrefabPattern;
            }
        }
        else
        {
            fullPatternString = enemyPrefabPattern;
        }
        print("enemypattern: "+fullPatternString);
        switch (patternType)
        {
            case enemyPatternType2.spiral:
                StartCoroutine(SpawnSpiral());
                return;
            case enemyPatternType2.row:
                SpawnRow(false, 0);
                break;
            case enemyPatternType2.line:
                SpawnRow(true, 0);
                break;
            case enemyPatternType2.circle:
                SpawnCircle();
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
        return 0;// (int)fullPatternString[i];
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
            yield return new WaitForSeconds(spiralDelay);
        }
        DetatchChildrenAndDestroy();
    }

    void DetatchChildrenAndDestroy()
    {
        transform.DetachChildren();
        Destroy(gameObject);
    }

    void SpawnRow(bool vertical = false, float relativeOffset = 0)
    {
        if (!vertical)
        {
            float startX = -(numberOfEnemies * spacing) / 2;
            float posX = startX;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(i)], new Vector3(posX, 0, relativeOffset), Quaternion.identity, transform);
                posX += spacing;
            }
        }
        else
        {
            float startY = -(numberOfEnemies * spacing) / 2;
            float posY = startY;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                ObjectPoolManager.Instantiate(enemyPrefabs[GetEnemyIndexInEnemyPattern(i)], new Vector3(relativeOffset, 0, posY), Quaternion.identity, transform);
                posY += spacing;
            }
        }
    }
}
