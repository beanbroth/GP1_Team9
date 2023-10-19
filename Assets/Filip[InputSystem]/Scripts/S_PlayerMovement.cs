using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement : MonoBehaviour
{
    S_PlayerControls playerControls;

    private float turnDirection = 0f;
    [SerializeField] private float turnSpeed = 0.7f;
    [SerializeField] private float moveSpeed = 6f;

    private void Awake()
    {
        playerControls = new S_PlayerControls();
        playerControls.Enable();

        playerControls.Player.Turn.performed += context =>
        {
            turnDirection = context.ReadValue<float>();
        };
        
        playerControls.Player.Turn.canceled += context =>
        {
            turnDirection = 0f;
        };
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        // Debug.Log(playerControls.Player.Turn.ReadValue<float>());
        // rb.rotation = new
        Turn();
        ForwardMovement();
    }
    private void ForwardMovement()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void Turn()
    {
        transform.Rotate(0.0f, turnDirection * turnSpeed, 0.0f);
    }
    
}
