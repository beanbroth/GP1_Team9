using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    [SerializeField] private float tiltAngle = 15f;
    [SerializeField] private float tiltSpeed = 0.1f;
    [SerializeField] private float bobAmplitude = 0.1f;
    [SerializeField] private float bobFrequency = 1f;
    [SerializeField] private float yRotationSpeed = 0.1f;
    [SerializeField] private float tiltSmoothTime = 0.1f;
    [SerializeField] private float randomRotationRange = 2f;

    [SerializeField] private float forwardBackRotationAngle = 10f;

    private bool toggleForwardBackRotation = false;

    private Quaternion randomRotation;
    private Vector3 initialPosition;
    private float currentZAngle;
    private float zAngleVelocity;
    private bool isAtBottom;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        UpdateTilt(horizontalInput);
        UpdateBob();
        UpdateYRotation(horizontalInput);
        ApplyRandomRotation();

        if (IsAtBottomOfBob())
        {
            ChangeRandomRotation();
        }
    }

    private void UpdateTilt(float horizontalInput)
    {
        float targetZAngle = -horizontalInput * tiltAngle;
        currentZAngle = Mathf.SmoothDampAngle(currentZAngle, targetZAngle, ref zAngleVelocity, tiltSmoothTime);
        transform.localRotation = Quaternion.Euler(0, 0, currentZAngle);
    }

    private void UpdateBob()
    {
        float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.localPosition = initialPosition + new Vector3(0, bobOffset, 0);
    }

    private void UpdateYRotation(float horizontalInput)
    {
        float targetYRotation = horizontalInput * tiltAngle;
        float currentYRotation = transform.localEulerAngles.y;
        float newYRotation = Mathf.LerpAngle(currentYRotation, targetYRotation, yRotationSpeed);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, newYRotation, transform.localEulerAngles.z);
    }

    private void ApplyRandomRotation()
    {
        transform.localRotation *= randomRotation;
    }

    private bool IsAtBottomOfBob()
    {
        float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        if (bobOffset < -bobAmplitude * 0.99f && !isAtBottom)
        {
            isAtBottom = true;
            return true;
        }
        else if (bobOffset > -bobAmplitude * 0.99f)
        {
            isAtBottom = false;
        }
        return false;
    }

    private void ChangeRandomRotation()
    {
        float randomXRotation = Random.Range(-randomRotationRange, randomRotationRange);
        float randomZRotation = Random.Range(-randomRotationRange, randomRotationRange);
        float randomYRotation = Random.Range(-randomRotationRange, randomRotationRange);
        // Toggle forward and back rotation
        toggleForwardBackRotation = !toggleForwardBackRotation;
        float forwardBackRotation = toggleForwardBackRotation ? forwardBackRotationAngle : -forwardBackRotationAngle;

        randomRotation = Quaternion.Euler((randomXRotation/4) + forwardBackRotation, randomYRotation , randomZRotation);
    }
}