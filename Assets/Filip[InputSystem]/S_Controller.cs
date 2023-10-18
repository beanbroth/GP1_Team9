using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Controller : MonoBehaviour
{
    //private PlayerInput playerInput;

    //// Store the controls 
    //private InputAction turnLeftAction;

    //private void Awake()
    //{
    //    playerInput = GetComponent<PlayerInput>();
    //    turnLeftAction = playerInput.actions["TurnLeft"];
    //    turnLeftAction.ReadValue<float>();
    //}
    private S_Controls playerControls;

    private void awake()
    {
        playerControls = new S_Controls();
    }

    private void onenable()
    {
        playerControls.Enable();
    }
    private void ondisable()
    {
        playerControls.Disable();
        playerControls.Move.TurnLeft.started -= TurnLeft;
    }

    void start()
    {
        playerControls.Move.TurnLeft.started += TurnLeft;
        playerControls.Move.TurnLeft.performed += TurnLeft;
        playerControls.Move.TurnLeft.canceled += TurnLeft;

        playerControls.Move.TurnLeft.started -= TurnLeft;
        playerControls.Move.TurnLeft.performed -= TurnLeft;
        playerControls.Move.TurnLeft.canceled -= TurnLeft;
    }
    public void TurnLeft(InputAction.CallbackContext context)
    {
        Debug.Log("turn left");
        context.ReadValue<float>();
        context.ReadValueAsButton();
    }
    private void update()
    {
        //if (playercontrols.move.turnleft.triggered)
        //    debug.log("turnleft");
    }
}
