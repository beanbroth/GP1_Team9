using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndiePlayerMovment : MonoBehaviour
{
    public float speed;
    private bool isMoving = true;
    private Rigidbody rb;
    public float turnSpeed;
    public float turnSpeedMultiplier;

    //private bool hasFlipped = false;
    public float flipCooldown;

    float acceleration;


    private void Update()
    {
        
        if (Input.GetKey("left"))
        {
            /*acceleration += turnSpeedMultiplier + Time.deltaTime;
            transform.Rotate(0.0f, -acceleration / 1000, 0.0f);*/
            transform.Rotate(0.0f, -turnSpeed, 0.0f);
        }
        else if (Input.GetKey("right"))
        {
            /*acceleration += turnSpeedMultiplier + Time.deltaTime;
            transform.Rotate(0.0f, acceleration / 1000, 0.0f);*/
            transform.Rotate(0.0f, turnSpeed, 0.0f);
        }
        else
            acceleration = 0;


        /*if (Input.GetKey("right") && Input.GetKey("left") && !hasFlipped)
        {
            hasFlipped = true;
            transform.Rotate(0.0f, 180f, 0.0f);
            Invoke("ResetFlip", flipCooldown);
        }*/
    }
    
    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void ResetFlip()
    {
        //hasFlipped = false;
    }
}
