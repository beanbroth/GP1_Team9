using UnityEngine;

public class S_MeteorLifetime : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5.0f; // Editable lifetime in seconds

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the meteor game object after 'lifetime' seconds
    }
}
