using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SamePositionAsTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool fixedUpdate = false;

    private void Update()
    {
        if(!fixedUpdate)
            transform.position = target.position;
    }
    private void FixedUpdate()
    {
        if (fixedUpdate)
            transform.position = target.position;
    }
}
