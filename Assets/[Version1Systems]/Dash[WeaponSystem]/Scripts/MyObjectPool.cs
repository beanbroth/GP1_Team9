using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyObjectPool
{
    private Queue<GameObject> pool;
    private GameObject prefab;

    public int MaxSpawnedObjects { get; private set; }
    public int ObjectCount => pool.Count;
    public MyObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        pool = new Queue<GameObject>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count == 0)
        {
            prefab.SetActive(false);
            GameObject obj = Object.Instantiate(prefab);
            pool.Enqueue(obj);
        }

        GameObject pooledObj = pool.Dequeue();
        pooledObj.SetActive(true);
        
        Debug.Log((pool.Count) + " items in the pool");
        
        
        return pooledObj;
        
        
        
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}