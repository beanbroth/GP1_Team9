using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SimpleBulletSpawn : MonoBehaviour
{
    public float shootRate;
    public GameObject bulletPrefab;

    private void Start()
    {
        InvokeRepeating("Shoot", 0.0f, shootRate);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab);
    }
}
