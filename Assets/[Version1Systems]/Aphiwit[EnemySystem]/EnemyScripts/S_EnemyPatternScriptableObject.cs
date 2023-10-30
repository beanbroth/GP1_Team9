using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum enemyPatternType
{
    row,
    line,
    circle
}

[CreateAssetMenu(fileName = "New Pattern", menuName = "Enemies/PatternDesigner")]
public class S_EnemyPatternScriptableObject : ScriptableObject
{
    public string patternName;
    public enemyPatternType patternType;
    public int patternAmount;

    public GameObject[] enemyModels;
    public int enemyAmount;
    public float distanceBetweenEnemies;
    public float spawnIntevallBetweenEnemies;
    public Vector2 patternCooldown;

}
