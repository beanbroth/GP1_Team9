using UnityEngine;

public class S_TurnIndicator : MonoBehaviour
{
    [SerializeField] private S_PlayerMovement playerMovement; // Reference to the PlayerMovement script.

    // Reference to the child objects.
    [SerializeField] private GameObject forwardRadius;
    [SerializeField] private GameObject leftRadius;
    [SerializeField] private GameObject rightRadius;
    [SerializeField] private GameObject midLeftRadius;
    [SerializeField] private GameObject midRightRadius;
    [SerializeField] private GameObject midLeft1Radius;
    [SerializeField] private GameObject midRight1Radius;

    [SerializeField] private float transitionDelay = 0.3f; // Delay for transitioning between states.

    private float transitionTimer = 0f;
    private int currentState = 0; // 0 for Forward, 1 for MidLeft/MidRight, 2 for Left/Right

    void Start()
    {
        // Ensure the initial state of the child objects.
        if (forwardRadius != null) forwardRadius.SetActive(true);
        if (leftRadius != null) leftRadius.SetActive(false);
        if (rightRadius != null) rightRadius.SetActive(false);
        if (midLeftRadius != null) midLeftRadius.SetActive(false);
        if (midRightRadius != null) midRightRadius.SetActive(false);
        if (midLeft1Radius != null) midLeft1Radius.SetActive(false);
        if (midRight1Radius != null) midRight1Radius.SetActive(false);
    }

    void Update()
    {
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement reference is not set.");
            return;
        }

        float turnDirection = playerMovement.turnDirection;

        if (turnDirection < 0f)
        {
            if (currentState == 0) // Transition from Forward to MidLeft
            {
                currentState = 1;
                transitionTimer = transitionDelay;
                if (forwardRadius != null) forwardRadius.SetActive(false);
                if (midLeftRadius != null) midLeftRadius.SetActive(true);
            }
            else if (currentState == 1 && transitionTimer <= 0f) // Transition from MidLeft to Left
            {
                currentState = 2;
                if (midLeftRadius != null) midLeftRadius.SetActive(false);
                if (leftRadius != null) leftRadius.SetActive(true);
            }
            else if (currentState == 1 && transitionTimer <= -0.3f) // Transition from MidLeft to MidLeft1
            {
                if (midLeft1Radius != null) midLeft1Radius.SetActive(true);
            }
        }
        else if (turnDirection > 0f)
        {
            if (currentState == 0) // Transition from Forward to MidRight
            {
                currentState = 1;
                transitionTimer = transitionDelay;
                if (forwardRadius != null) forwardRadius.SetActive(false);
                if (midRightRadius != null) midRightRadius.SetActive(true);
            }
            else if (currentState == 1 && transitionTimer <= 0f) // Transition from MidRight to Right
            {
                currentState = 2;
                if (midRightRadius != null) midRightRadius.SetActive(false);
                if (rightRadius != null) rightRadius.SetActive(true);
            }
            else if (currentState == 1 && transitionTimer <= -0.3f) // Transition from MidRight to MidRight1
            {
                if (midRight1Radius != null) midRight1Radius.SetActive(true);
            }
        }
        else
        {
            if (currentState == 2) // Transition from Left/Right to Forward
            {
                currentState = 0;
                transitionTimer = transitionDelay;
                if (leftRadius != null) leftRadius.SetActive(false);
                if (rightRadius != null) rightRadius.SetActive(false);
                if (midLeftRadius != null) midLeftRadius.SetActive(false);
                if (midRightRadius != null) midRightRadius.SetActive(false);
                if (midLeft1Radius != null) midLeft1Radius.SetActive(false);
                if (midRight1Radius != null) midRight1Radius.SetActive(false);
                if (forwardRadius != null) forwardRadius.SetActive(true);
            }
        }

        // Update the transition timer.
        if (transitionTimer > 0f)
        {
            transitionTimer -= Time.deltaTime;
        }
    }
}
