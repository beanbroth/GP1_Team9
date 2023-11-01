using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnvironmentDetailSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] detailPrefabs;
    [SerializeField] List<Vector3> savedPositions;
    [SerializeField] private Vector2 scaleRange;
    [SerializeField] private float radius;
    [SerializeField] private int amount;
    [SerializeField] private float height;
    [SerializeField] private bool onlyRotateY;

    void Awake()
    {
        // Iterate x times depending on amount and instantiate the prefab depending on functions, RandomPosition(), RandomRotation(), RandomScale()
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = RandomPosition();
            Quaternion rotation = RandomRotation();
            Vector3 scale = RandomScale();
            
            GameObject prefabToInstantiate = detailPrefabs[Random.Range(0, detailPrefabs.Length)];

            // For each saved position, check that the position is not the same as a saved one (does not solve clumping, but is not needed for environment details)
            foreach (Vector3 savedPosition in savedPositions)
            {
                if (position == savedPosition)
                {
                    RandomPosition();
                }
            }

            Instantiate(prefabToInstantiate, position, rotation).transform.localScale = scale;
            savedPositions.Add(position);
        }
    }
    Vector3 RandomPosition()
    {
        // Give a random angle and do a degrees to radians conversion
        float angle = Random.Range(0f, 360f);
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate a random radius within the specified range
        float randomRadius = Random.Range(0f, radius);

        // Calculate the x and z positions using the random radius and angle
        float xPos = Mathf.Cos(angleInRadians) * randomRadius;
        float zPos = Mathf.Sin(angleInRadians) * randomRadius;

        Vector3 position = new Vector3(xPos, height, zPos);
        return position;
    }

    Quaternion RandomRotation()
    {
        // Return Quaternion random rotation on all angles, 0 to 360 degrees
        if (onlyRotateY == true)
            return Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        return Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
    }

    Vector3 RandomScale()
    {
        // Return random scale based on values in Vector2 scaleRange
        return Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
    }
}
