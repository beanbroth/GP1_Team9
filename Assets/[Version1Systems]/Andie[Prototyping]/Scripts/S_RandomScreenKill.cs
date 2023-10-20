using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class S_RandomScreenKill : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    List<GameObject> killableEnemies = new List<GameObject>();
    public float killDistance;
    public float killTime;
    public float numberOfKills;

    private void Awake()
    {
        InvokeRepeating("Kill", 0.0f, killTime);
    }

    private void Update()
    {
    }


    private void Kill()
    {
        killableEnemies.Clear();
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, killDistance, enemyLayer))
        {
            Debug.Log(enemy.name);
            killableEnemies.Add(enemy.gameObject);
        }

        if (killableEnemies.Count <= 0)
            return;

        GameObject tempEnemy = killableEnemies[Random.Range(0, killableEnemies.Count)];
        S_EnemyHealthController tempHealthController = tempEnemy.GetComponent<S_EnemyHealthController>();
        tempHealthController.TakeDamage(tempHealthController.MaxHealth);

        tempEnemy.gameObject.SetActive(false);

        //.GetComponent<S_EnemyHealthController>().TakeDamage(GetComponent<S_EnemyHealthController>().MaxHealth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, killDistance);
    }
}
