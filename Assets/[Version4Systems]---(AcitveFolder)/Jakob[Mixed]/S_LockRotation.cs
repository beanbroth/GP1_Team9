using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LockRotation : MonoBehaviour
{
    Quaternion startRotation;
    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = startRotation;
    }
}
