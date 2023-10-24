using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDesigner", menuName = "EnemyScriptableObjects/EnemyDesigner")]
public class S_EnemyDesignerScriptableObject : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;

}
