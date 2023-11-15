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
            case enemyPatternType2.row:
                SpawnRow(false, 0);
                break;
            case enemyPatternType2.line:
                SpawnRow(true, 0);
                break;
            case enemyPatternType2.circle:

                break;
        }
        if (turnTowardsParent)
        {
            transform.LookAt(transform.parent.position);
        }
        transform.DetachChildren();
        Destroy(gameObject);
    }
    int GetEnemyIndexInEnemyPattern(int i)
    {
        return 0;// (int)fullPatternString[i];
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
