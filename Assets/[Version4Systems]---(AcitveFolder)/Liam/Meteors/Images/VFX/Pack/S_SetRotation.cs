using UnityEngine;

public class S_SetRotation : MonoBehaviour
{
    [SerializeField]
    private float desiredZRotation = 90.0f; // The desired Z-axis rotation in degrees
    [SerializeField]
    private float desiredXRotation = 90f; // The desired X-axis rotation in degrees

    private void Update()
    {
        // Get the current rotation
        Vector3 currentRotation = transform.eulerAngles;

        // Set the Z and X-axis rotations to the desired values
        currentRotation.z = desiredZRotation;
        currentRotation.x = desiredXRotation;

        // Apply the new rotation
        transform.eulerAngles = currentRotation;
    }
}
