using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class S_RandomScreenKill : MonoBehaviour
{
    [SerializeField] private GameObject boltPrefab;
    private List<GameObject> boltPool = new List<GameObject>();
    [SerializeField] private LayerMask enemyLayer;
    List<GameObject> killableEnemies = new List<GameObject>();
    public int initialBoltPoolSize = 10;
    public int damage = 999;
    public float killRange = 25;
    public float timeBetweenKills = 1f;
    public float boltLifetime = 0.1f;
    public int numKills = 1;
    private float elapsedTime = 0f;

    private void Awake()
    {
        CreateBoltPool(initialBoltPoolSize);
    }

    private void OnEnable()
    {
        Kill();
        elapsedTime = 0;
    }

    private void OnDisable()
    {
        foreach (GameObject bolt in boltPool)
        {
            if (bolt != null)
                bolt.SetActive(false);
        }
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeBetweenKills)
        {
            elapsedTime = 0f;
            Kill();
        }
    }

    private void CreateBoltPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject bolt = Instantiate(boltPrefab);
            bolt.SetActive(false);
            boltPool.Add(bolt);
        }
    }

    private GameObject GetBoltFromPool()
    {
        foreach (GameObject bolt in boltPool)
        {
            if (!bolt.activeInHierarchy)
            {
                return bolt;
            }
        }

        GameObject newBolt = Instantiate(boltPrefab);
        boltPool.Add(newBolt);
        return newBolt;
    }

    private void Kill()
    {
        killableEnemies.Clear();
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, killRange, enemyLayer))
        {
            // Debug.Log(enemy.name);
            killableEnemies.Add(enemy.gameObject);
        }

        if (killableEnemies.Count <= 0)
            return;
        for (int i = 0; i < numKills; i++)
        {
            KillRandomEnemy();
        }
        AudioManager.Instance.PlaySound3D("Cern-TainDeath", transform.position);
    }

    private void KillRandomEnemy()
    {
        GameObject tempEnemy = killableEnemies[Random.Range(0, killableEnemies.Count)];
        S_EnemyHealthController tempHealthController = tempEnemy.GetComponent<S_EnemyHealthController>();
        tempHealthController.TakeDamage(damage);
        CreateBolt(tempEnemy.transform);
    }

    private void CreateBolt(Transform enemyTransform)
    {
        GameObject bolt = GetBoltFromPool();
        bolt.transform.position = enemyTransform.position;
        bolt.transform.rotation = Quaternion.identity;
        bolt.SetActive(true);
        StartCoroutine(DisableBoltAfterTime(bolt, boltLifetime));
    }

    private IEnumerator DisableBoltAfterTime(GameObject bolt, float time)
    {
        yield return new WaitForSeconds(time);
        bolt.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, killRange);
    }
}