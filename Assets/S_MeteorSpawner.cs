using UnityEngine;

public class S_MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] meteorPrefabs;

    [SerializeField]
    private float spawnInterval = 20.0f; // Editable time interval in seconds

    [SerializeField]
    private float xOffset = 12.0f; // Default X offset
    [SerializeField]
    private float yOffset = -15.0f; // Default Y offset

    [SerializeField]
    private float minZOffset = 7.0f; // Minimum Z offset
    [SerializeField]
    private float maxZOffset = 18.0f; // Maximum Z offset

    [SerializeField]
    private float xSpeed = 2.0f; // Customizable X movement speed
    [SerializeField]
    private float zRotationSpeed = 90.0f; // Customizable Z-axis rotation speed (degrees per second)

    private float timer;

    private Camera mainCamera; // Reference to the main camera

    private void Start()
    {
        // Initialize the timer
        timer = spawnInterval;

        // Find the main camera in the scene
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera found in the scene.");
        }
    }

    private void Update()
    {
        if(PauseManager.IsPaused)
            return;
            
        // Update the timer
        timer -= Time.deltaTime;

        // Check if the timer has reached zero
        if (timer <= 0)
        {
            // Spawn a meteor prefab as a child of the camera
            SpawnMeteorAsChildOfCamera();

            // Reset the timer to the spawn interval
            timer = spawnInterval;
        }

        // Move the spawned meteorites to the left (decrease X position)
        MoveMeteors();

        // Rotate the spawned meteorites around the Z-axis (spin)
        RotateMeteors();
    }

    private void SpawnMeteorAsChildOfCamera()
    {
        if (meteorPrefabs.Length == 0)
        {
            Debug.LogWarning("No meteor prefabs assigned to the array.");
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera found in the scene.");
            return;
        }

        // Randomly select a meteor prefab
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
        GameObject meteorPrefab = meteorPrefabs[randomIndex];

        // Calculate the spawn position with customizable offsets
        float randomZOffset = Random.Range(minZOffset, maxZOffset);
        Vector3 spawnPosition = mainCamera.transform.position + new Vector3(xOffset, yOffset, randomZOffset);

        // Spawn the selected meteor prefab as a child of the camera
        GameObject meteorInstance = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity, mainCamera.transform);
    }

    private void MoveMeteors()
    {
        // Find all meteor children of the camera
        Transform cameraTransform = mainCamera.transform;
        int childCount = cameraTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = cameraTransform.GetChild(i);
            // Move the meteor to the left
            child.position -= new Vector3(xSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void RotateMeteors()
    {
        // Find all meteor children of the camera
        Transform cameraTransform = mainCamera.transform;
        int childCount = cameraTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = cameraTransform.GetChild(i);
            // Rotate the meteor around the Z-axis
            child.Rotate(Vector3.forward, zRotationSpeed * Time.deltaTime);
        }
    }
}
