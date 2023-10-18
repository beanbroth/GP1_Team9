using System.Collections.Generic;
using UnityEngine;

public class S_ObjectPoolManager : MonoBehaviour
{
    public static S_ObjectPoolManager Instance { get; private set; }
    private Dictionary<string, object> pools = new Dictionary<string, object>();
    public List<PoolInfo> PoolInfoList => GetPoolInfoList();

    [System.Serializable]
    public struct PoolInfo
    {
        public string Key;
        public int MaxSpawnedObjects;
        public int ObjectCount;
    }

    private List<PoolInfo> GetPoolInfoList()
    {
        List<PoolInfo> poolInfoList = new List<PoolInfo>();
        foreach (var key in pools.Keys)
        {
            MyObjectPool pool = pools[key] as MyObjectPool;
            poolInfoList.Add(new PoolInfo
            {
                Key = key, MaxSpawnedObjects = pool.MaxSpawnedObjects, ObjectCount = pool.ObjectCount
            });
        }

        return poolInfoList;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            pools = new Dictionary<string, object>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePool(GameObject prefab, int initialSize)
    {
        string key = prefab.name.Replace("(Clone)", "");
        if (!pools.ContainsKey(key))
        {
            pools[key] = new MyObjectPool(prefab, initialSize);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        string key = prefab.name.Replace("(Clone)", "");
        if (!pools.ContainsKey(key))
        {
            CreatePool(prefab, 5);
        }

        MyObjectPool pool = pools[key] as MyObjectPool;
        return pool.GetObject();
    }

    public void ReturnObject(GameObject obj)
    {
        string key = obj.name.Replace("(Clone)", "");
        if (!pools.ContainsKey(key))
        {
            Debug.Log("pool not found, creating new pool" + key + " " + obj.name);
            CreatePool(obj, 5);
        }

        MyObjectPool pool = pools[key] as MyObjectPool;
        Debug.Log("returning object to pool: " + key + " " + obj.name + pool.ObjectCount); 
        pool.ReturnObject(obj);
    }
}