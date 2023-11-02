using UnityEngine;

public class S_MeteorLifetime : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 7.0f; // Editable lifetime in seconds

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the meteor game object after 'lifetime' seconds
    }
}