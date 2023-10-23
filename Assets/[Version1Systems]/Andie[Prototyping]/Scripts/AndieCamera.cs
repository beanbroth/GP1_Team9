using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndieCamera : MonoBehaviour
{
    public Transform target;

    public float xOffset;
    public float yOffset;
    public float zOffset;

    public float camSpeed = 5;

    private void Update()
    {
        Vector3 targetPos = Vector3.Lerp(transform.position, new Vector3(target.position.x + xOffset, target.position.y + yOffset, target.position.z + zOffset), camSpeed);

        transform.position = targetPos;
    }
}
