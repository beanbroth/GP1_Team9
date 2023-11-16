using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DissolveController : MonoBehaviour
{
    [SerializeField] Transform parentWithMaterials;
    Material[] materialArray;
    Renderer[] renderers;
    bool dissolveStarted = false;
    [SerializeField] float dissolveDuration = 2f;
    float timeSinceDissolveStarted = 0f;
    [SerializeField] bool dissolveOnStart = false;
    [SerializeField] bool ignoreIsPaused = false;
    [SerializeField] bool dissolveOnWin = true;

    private void OnEnable()
    {
        S_Health.OnDeath += StartDissolve;
        if(dissolveOnWin)
            S_WinTimer.winEvent += StartDissolve;
        Reset();
    }

    private void OnDisable()
    {
        S_Health.OnDeath -= StartDissolve;
        if (dissolveOnWin)
            S_WinTimer.winEvent -= StartDissolve;
    }

    private void Start()
    {
        if (dissolveOnStart)
            StartDissolve();
    }

    public void Reset()
    {
        dissolveStarted = false;
        timeSinceDissolveStarted = 0f;
        UpdateMaterials(1f);
    }

    private void Awake()
    {
        if (parentWithMaterials == null)
        {
            parentWithMaterials = transform;
        }

        renderers = parentWithMaterials.GetComponentsInChildren<Renderer>();
        materialArray = new Material[renderers.Length];
        int idx = 0;
        foreach (Renderer renderer in renderers)
        {
            materialArray[idx] = renderer.material;
            idx++;
        }
    }

    //Coroutines reserve a core in the processor so think it might be to heavy too use it for dissolving all enemies. Think it's cheaper to put in update, and even cheaper to put in fixed that's run less often
    private void FixedUpdate()
    {
        if (dissolveStarted && (ignoreIsPaused || (!ignoreIsPaused && !PauseManager.IsPaused)))
        {
            timeSinceDissolveStarted += Time.fixedDeltaTime;
            UpdateMaterials(1 - timeSinceDissolveStarted / dissolveDuration);
            if (timeSinceDissolveStarted > dissolveDuration)
            {
                dissolveStarted = false;
            }
        }
    }

    public void StartDissolve()
    {
        Reset();
        dissolveStarted = true;
    }

    void UpdateMaterials(float dissolveAmount)
    {
        foreach (Material material in materialArray)
        {
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    public float GetDissolveDuration()
    {
        return dissolveDuration;
    }
}