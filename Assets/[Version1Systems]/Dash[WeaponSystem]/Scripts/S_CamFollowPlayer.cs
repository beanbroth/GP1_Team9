using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CamFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        offset =  transform.position - player.position;
    }

    private void LateUpdate()
    {

            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = desiredPosition;
        
    }
    
}
