using UnityEngine;

public class S_CirclePlacement : MonoBehaviour
{
    public float radius = 5f;
    public int numberOfObjects = 10;
    public GameObject prefab;

    public void PlaceObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 newPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Instantiate(prefab, newPos, Quaternion.Euler(0, -angle * Mathf.Rad2Deg, 0));
        }
    }
}