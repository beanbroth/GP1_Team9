using UnityEngine;

public class S_TurnIndicator : MonoBehaviour
{
    [SerializeField] private S_PlayerMovement playerMovement; // Reference to the PlayerMovement script.

    // Reference to the child objects.
    [SerializeField] private GameObject forwardRadius;
    [SerializeField] private GameObject leftRadius;
    [SerializeField] private GameObject rightRadius;

    void Start()
    {
        // Ensure the initial state of the child objects.
        if (forwardRadius != null) forwardRadius.SetActive(true);
        if (leftRadius != null) leftRadius.SetActive(false);
        if (rightRadius != null) rightRadius.SetActive(false);
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
            // Turn on LeftRadius and turn off the other two.
            if (leftRadius != null) leftRadius.SetActive(true);
            if (forwardRadius != null) forwardRadius.SetActive(false);
            if (rightRadius != null) rightRadius.SetActive(false);
        }
        else if (turnDirection > 0f)
        {
            // Turn on RightRadius and turn off the other two.
            if (rightRadius != null) rightRadius.SetActive(true);
            if (forwardRadius != null) forwardRadius.SetActive(false);
            if (leftRadius != null) leftRadius.SetActive(false);
        }
        else
        {
            // If turnDirection is 0f, only "ForwardRadius" is turned on.
            if (forwardRadius != null) forwardRadius.SetActive(true);
            if (leftRadius != null) leftRadius.SetActive(false);
            if (rightRadius != null) rightRadius.SetActive(false);
        }
    }
}
