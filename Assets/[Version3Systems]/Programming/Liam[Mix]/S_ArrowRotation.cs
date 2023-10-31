using UnityEngine;

public class S_ArrowRotation : MonoBehaviour
{
    [SerializeField]
    private S_PlayerMovement playerMovement;

    [SerializeField]
    private Transform objectToRotate; // The object you want to animate.

    [SerializeField]
    private float rotationAngleNegative = 80.0f; // Adjust the negative rotation angle.

    [SerializeField]
    private float rotationAnglePositive = 80.0f; // Adjust the positive rotation angle.

    [SerializeField]
    private float rotationSpeed = 5.0f; // Adjust the speed of the rotation animation.

    private float targetRotationY; // The target Y rotation for the object.

    private void Update()
    {

        if(PauseManager.IsPaused)
        return;
        // Get the player's Y rotation and subtract 180 degrees to make it the opposite.
        float playerRotationY = playerMovement.transform.rotation.eulerAngles.y - 180f;

        // Check the value of the turnDirection variable from S_PlayerMovement.
        float turnDirection = playerMovement.turnDirection;

        // Calculate the target rotation for the object.
        if (turnDirection == 0f)
        {
            targetRotationY = playerRotationY;
        }
        else if (turnDirection == -1f)
        {
            targetRotationY = playerRotationY - rotationAngleNegative;
        }
        else if (turnDirection == 1f)
        {
            targetRotationY = playerRotationY + rotationAnglePositive;
        }

        // Smoothly animate the object's rotation towards the target.
        Quaternion targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}