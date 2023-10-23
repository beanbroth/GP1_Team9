using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class S_BasicBulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int bulletDamage = 5;
    [SerializeField] private float bulletSizeMultiplier = 1;
    [SerializeField] private Color bulletColor = Color.cyan;
    private Transform _transform;
    [SerializeField] private float bulletLifeTime = 5f;


    private void OnEnable()
    {
        GetComponentInChildren<Renderer>().material.color = bulletColor;
        _transform = transform;
        _transform.localScale *= bulletSizeMultiplier;
        //invoke after a delay
        Invoke(nameof(ReturnBulletToPool), bulletLifeTime);
    }
    
    private void ReturnBulletToPool()
    {
        //Debug.Log("returning object to pool");
        ObjectPoolManager.ReturnObject(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        ObjectPoolManager.ReturnObject(gameObject);
    }


    private void FixedUpdate()
    {
        transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
    }
}