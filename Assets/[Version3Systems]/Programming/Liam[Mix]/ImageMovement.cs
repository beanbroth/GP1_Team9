using UnityEngine;

public class ImageMovement : MonoBehaviour
{
    public float rotationSpeed = 45f; // Rotation speed adjustable in the Inspector.
    public float moveSpeed = 3f; // Movement speed adjustable in the Inspector.

    private void Update()
    {
        // Handle rotation input
        float rotationInput = Input.GetAxis("Horizontal");
        RotateImage(rotationInput);

        // Handle movement input
        float moveInput = Input.GetAxis("Vertical");
        MoveImage(moveInput);
    }

    private void RotateImage(float rotationInput)
    {
        // Calculate rotation based on input
        float rotation = rotationInput * rotationSpeed * Time.deltaTime;

        // Rotate the image around the z-axis (assuming it's a 2D image)
        transform.Rotate(0f, 0f, -rotation);
    }

    private void MoveImage(float moveInput)
    {
        // Calculate movement based on input
        float movement = moveInput * moveSpeed * Time.deltaTime;

        // Translate the image in its local space
        transform.Translate(Vector3.up * movement);
    }
}
