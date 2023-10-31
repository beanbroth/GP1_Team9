using UnityEngine;

public class S_RotateAnotherPlane : MonoBehaviour
{
    [SerializeField]
    private Transform targetPlane; // Reference to the plane you want to rotate

    public float rotationSpeed = 30.0f;

    private void Update()
    {
        if (targetPlane != null)
        {
            // Rotate the target plane based on the rotation of this plane
            targetPlane.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
