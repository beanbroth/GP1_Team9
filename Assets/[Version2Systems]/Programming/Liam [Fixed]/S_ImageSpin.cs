using UnityEngine;

public class S_ImageSpin : MonoBehaviour
{
    public float rotationSpeed = 90.0f; // Customizable rotation
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);         // Rotating the image around its pivot point
    }
}
