using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum enemyPatternType
{
    Row,
    Line,
    Circle
}

[System.Serializable]
public enum patternUsageType
{
    Static,
    Random
}

[CreateAssetMenu(fileName = "New Pattern", menuName = "Enemies/PatternDesigner")]
public class EnemyPattern : ScriptableObject
{
    public string patternName;
    public enemyPatternType patternType;
    public int patternRepeatAmount;
    public Vector3 patternRepeatOffset;

    public GameObject[] enemyModels;
    public patternUsageType patternUsageType;
    public int enemyAmount;
    public float distanceBetweenEnemies;
    public float enemySpawnIntervall;
    public Vector2 patternCooldown;

}
