using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_BoltVisualsController : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public TrailRenderer trailRenderer1;

    private void OnDisable()
    {
        trailRenderer.emitting = false;
        trailRenderer1.emitting = false;
        trailRenderer.Clear();
        trailRenderer1.Clear();
    }

    void OnEnable()
    {
        trailRenderer.emitting = true;
        trailRenderer1.emitting = true;
    }
}