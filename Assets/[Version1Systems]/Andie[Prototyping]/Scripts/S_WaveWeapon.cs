using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaveWeapon : MonoBehaviour
{
    [SerializeField] private float scaleOverTime;
    private Vector3 ogScale;

    private void Start()
    {
        ogScale = transform.localScale;
    }

    private void Awake()
    {
        //transform.localScale = ogScale;
    }

    private void OnDisable()
    {
        transform.localScale = ogScale; 
    }
    private void FixedUpdate()
    {
        transform.localScale = transform.localScale * scaleOverTime;
    }
}
