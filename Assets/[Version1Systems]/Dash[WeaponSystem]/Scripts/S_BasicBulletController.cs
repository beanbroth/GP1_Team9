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

    private float timer;

    private void OnEnable()
    {
        GetComponentInChildren<Renderer>().material.color = bulletColor;
        _transform = transform;
        _transform.localScale *= bulletSizeMultiplier;
        timer = bulletLifeTime;
    }
    
    private void FixedUpdate()
    {
        transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ObjectPoolManager.Destroy(gameObject);
        }
    }
}