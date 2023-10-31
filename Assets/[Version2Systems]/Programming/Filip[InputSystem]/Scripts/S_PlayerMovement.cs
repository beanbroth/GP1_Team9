using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    S_PlayerControls playerControls;
    private float turnDirection = 0f;
    [SerializeField] private float turnSpeed = 0.7f;
    [SerializeField] private float moveSpeed = 6f;

    public float TurnDirection
    {
        get => turnDirection;
    }

    private void Awake()
    {
        playerControls = new S_PlayerControls();
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        turnDirection = playerControls.Player.Turn.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        if (PauseManager.IsPaused)
            return; // If game is paused, exit method
        Turn();
        ForwardMovement();
    }

    private void ForwardMovement()
    {
        if (PauseManager.IsPaused)
            return; // If game is paused, exit method
        transform.Translate(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
    }

    private void Turn()
    {
        if (PauseManager.IsPaused)
            return; // If game is paused, exit method
        transform.Rotate(0.0f, turnDirection * turnSpeed, 0.0f);
    }
}