using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StartLookAt : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        transform.LookAt(target);
    }

}
