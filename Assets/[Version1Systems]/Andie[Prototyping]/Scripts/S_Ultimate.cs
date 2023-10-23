using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Ultimate : MonoBehaviour
{
    S_PlayerControls playerControls;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float killRange;


    private void Awake()
    {
        playerControls = new S_PlayerControls();
        playerControls.Enable();
    }

    private void Update()
    {
        
    }

    private void Kill()
    {
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, killRange, enemyLayer))
        {
            Destroy(enemy);
        }
    }
}
