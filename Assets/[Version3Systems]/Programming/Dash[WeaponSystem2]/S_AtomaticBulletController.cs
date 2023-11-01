using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class S_AtomaticBulletController : MonoBehaviour
{
    public float maxBulletSpeed = 20f;
    public float bulletAcceleration = 5f;
    private float currentSpeed;
    private Transform targetEnemy;

    private void OnEnable()
    {
        currentSpeed = 0f;
    }

    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }

    private void Update()
    {
        if (currentSpeed < maxBulletSpeed)
        {
            currentSpeed += bulletAcceleration * Time.deltaTime;
        }

        if (targetEnemy != null && targetEnemy.gameObject.activeSelf)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, targetEnemy.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            targetEnemy = null;
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
    }
}