using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_OrbitWeaponController : MonoBehaviour
{
    public GameObject orbitingObjectPrefab;
    public int numberOfRings;
    [SerializeField] public List<int> objectsPerRingList;
    public float radius;
    public float distanceBetweenRings;
    public float rotationSpeed;
    private List<List<Transform>> rings;
    private Transform player;

    private void Start()
    {
        player = transform.root;
        rings = new List<List<Transform>>();
        CreateOrbitingObjects();
    }

    private void OnValidate()
    {
        numberOfRings = objectsPerRingList.Count;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Inverse(player.rotation);
        for (int i = 0; i < numberOfRings; i++)
        {
            float direction = i % 2 == 0 ? 1 : -1;
            Vector3 rotationAxis = Vector3.up;
            foreach (Transform orbitingObject in rings[i])
            {
                orbitingObject.RotateAround(transform.position, rotationAxis,
                    direction * rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void CreateOrbitingObjects()
    {
        for (int ring = 0; ring < numberOfRings; ring++)
        {
            List<Transform> currentRing = new List<Transform>();
            float currentRadius = radius + (ring * distanceBetweenRings);
            int objectsPerRing = objectsPerRingList[ring];
            for (int i = 0; i < objectsPerRing; i++)
            {
                float angle = i * 360f / objectsPerRing;
                GameObject orbitingObject =
                    Instantiate(orbitingObjectPrefab, Vector3.zero, Quaternion.identity, transform);
                Vector3 spawnPosition =
                    new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * currentRadius;
                orbitingObject.transform.localPosition = spawnPosition;
                currentRing.Add(orbitingObject.transform);
            }

            rings.Add(currentRing);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int ring = 0; ring < numberOfRings; ring++)
        {
            float currentRadius = radius + (ring * distanceBetweenRings);
            int objectsPerRing = objectsPerRingList[ring];
            for (int i = 0; i < objectsPerRing; i++)
            {
                float angle = i * 360f / objectsPerRing;
                Vector3 spawnPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * currentRadius;
                Gizmos.DrawWireSphere(transform.position + spawnPosition, 0.1f);
            }
        }
    }
}