using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndieOrbit : MonoBehaviour
{
    public float rotationSpeed;
    public Transform target;

    private void Update()
    {
        transform.position = target.position;

        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime * 100);
    }

    //Damage script on object
}
