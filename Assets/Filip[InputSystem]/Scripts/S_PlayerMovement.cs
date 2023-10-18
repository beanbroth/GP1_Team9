using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement : MonoBehaviour
{
    S_PlayerControls playerControls;

    private float turnDirection = 0f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        playerControls = new S_PlayerControls();
        playerControls.Enable();

        playerControls.Player.Turn.performed += context =>
        {
            turnDirection = context.ReadValue<float>();
        };
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        Turn();
        ForwardMovement();
    }
    private void ForwardMovement()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }
    private void Turn()
    {
        transform.Rotate(0.0f, turnDirection * turnSpeed, 0.0f);
    }
    
}
