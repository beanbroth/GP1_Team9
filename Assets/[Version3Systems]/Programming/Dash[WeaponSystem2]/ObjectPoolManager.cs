using System.Collections.Generic;
using UnityEngine;


public static class ObjectPoolManager
{
    private static Dictionary<string, object> pools = new Dictionary<string, object>();

    public static List<PoolInfo> PoolInfoList => GetPoolInfoList();

    [System.Serializable]
    public struct PoolInfo
    {
        public string Key;
        public int MaxSpawnedObjects;
        public int ObjectCount;
    }

    private static List<PoolInfo> GetPoolInfoList()
    {
        List<PoolInfo> poolInfoList = new List<PoolInfo>();
        foreach (var key in pools.Keys)
        {
            MyObjectPool pool = pools[key] as MyObjectPool;
            poolInfoList.Add(new PoolInfo
            {
                Key = key,
                MaxSpawnedObjects = pool.MaxSpawnedObjects,
                ObjectCount = pool.ObjectCount
            });
        }

        return poolInfoList;
    }


    public static void CreatePool(GameObject prefab, int initialSize)
    {
        string key = prefab.name.Replace("(Clone)", "");
        if (!pools.ContainsKey(key))
        {
            pools[key] = new MyObjectPool(prefab, initialSize);
        }
    }

    public static GameObject GetObject(GameObject prefab)
    {
        string key = prefab.name.Replace("(Clone)", "");
        if (!pools.ContainsKey(key))
        {
            CreatePool(prefab, 5);
        }

        MyObjectPool pool = pools[key] as MyObjectPool;
        return pool.GetObject();
    }

    public static void ReturnObject(GameObject obj)
    {
        string key = obj.name.Replace("(Clone)", "");
        if (!pools.ContainsKey(key))
        {
            //Debug.Log("pool not found, creating new pool" + key + " " + obj.name);
            CreatePool(obj, 5);
        }

        MyObjectPool pool = pools[key] as MyObjectPool;
        //Debug.Log("returning object to pool: " + key + " " + obj.name + pool.ObjectCount); 
        pool.ReturnObject(obj);
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetObject(prefab);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject obj = GetObject(prefab);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent, false);
        obj.SetActive(true);
        return obj;
    }

    public static GameObject Instantiate(GameObject prefab, Transform parent)
    {
        GameObject obj = GetObject(prefab);
        obj.transform.SetParent(parent, false);
        obj.SetActive(true);
        return obj;
    }

    public static void Destroy(GameObject obj)
    {
        obj.SetActive(false);
        ReturnObject(obj);
    }

    public static void ClearPools()
    {
        pools.Clear();
    }
}
