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
        GetComponent<SpriteRenderer>().color = bulletColor;
        _transform = transform;
        _transform.localScale *= bulletSizeMultiplier;
        //invoke after a delay
        Invoke(nameof(ReturnBulletToPool), bulletLifeTime);
    }
    
    private void ReturnBulletToPool()
    {
        //Debug.Log("returning object to pool");
        S_ObjectPoolManager.Instance.ReturnObject(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        S_ObjectPoolManager.Instance.ReturnObject(gameObject);
    }


    private void FixedUpdate()
    {
        _transform.position += transform.up * (bulletSpeed * Time.fixedDeltaTime);
    }
}