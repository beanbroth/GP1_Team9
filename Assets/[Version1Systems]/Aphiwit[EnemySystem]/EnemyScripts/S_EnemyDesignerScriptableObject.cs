using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDesigner", menuName = "Enemies/Enemy Designer")]
public class S_EnemyDesignerScriptableObject : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public GameObject enemyModel;
}
