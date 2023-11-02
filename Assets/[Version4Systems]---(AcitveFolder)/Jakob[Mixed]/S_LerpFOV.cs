using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LerpFOV : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float targetFOV = 15f;
    float startFOV;
    [SerializeField] float lerpDuration = 3f;
    [SerializeField] bool zoomOnWin = false;
    [SerializeField] bool zoomOnLose = true;

    private void OnEnable()
    {
        if(zoomOnLose)
            S_Health.OnDeath += LerpFOV;
        if (zoomOnWin)
            S_WinTimer.winEvent += LerpFOV;
    }
    
    private void OnDisable()
    {
        if (zoomOnLose)
            S_Health.OnDeath -= LerpFOV;
        if (zoomOnWin)
            S_WinTimer.winEvent -= LerpFOV;
    }

    private void Awake()
    {
        if(cam == null)
            cam = Camera.main;

        startFOV = cam.fieldOfView;
    }

    public void LerpFOV()
    {
        StartCoroutine(FOVCoroutine());
    }

    IEnumerator FOVCoroutine()
    {
        float currentTime = 0;
        while(currentTime < lerpDuration)
        {
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, currentTime / lerpDuration);
            currentTime += 0.015f;
            yield return new WaitForSecondsRealtime(0.015f);
        }
    }
}
