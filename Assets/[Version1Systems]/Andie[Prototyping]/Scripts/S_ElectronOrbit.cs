using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ElectronOrbit : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime * 100);
    }

}
