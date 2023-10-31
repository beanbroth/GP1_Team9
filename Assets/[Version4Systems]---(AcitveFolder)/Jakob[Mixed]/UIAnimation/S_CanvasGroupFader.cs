using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private bool fadeOnDamage = true;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float animationFPS = 60f;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] bool ignoreTimeScale = true;
    float timePerFrame;


    private void OnEnable()
    {
        if (fadeOnDamage)
        {
            S_Health.OnDamage += FadeInAndOut;

        }
    }

    private void OnDisable()
    {
        if (fadeOnDamage)
        {
            S_Health.OnDamage -= FadeInAndOut;

        }
    }

    private void Awake()
    {
        if(canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        timePerFrame = 1 / animationFPS;
        canvasGroup.alpha = 0;
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(true));
    }
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(false));
    }

    IEnumerator FadeRoutine(bool fadeIn = true)
    {
        float animationTime = 0f;
        float currentAlpha = 0;
        if (!fadeIn)
            currentAlpha = 1;

        while (animationTime < animationDuration)
        {
            canvasGroup.alpha = currentAlpha;
            animationTime += timePerFrame;
            if (fadeIn)
            {
                currentAlpha = Mathf.Lerp(0, 1, animationTime / animationDuration);
            }
            else
            {
                currentAlpha = Mathf.Lerp(1, 0, animationTime / animationDuration);
            }
            if (ignoreTimeScale)
                yield return new WaitForSecondsRealtime(timePerFrame);
            else
                yield return new WaitForSeconds(timePerFrame);
        }
        if (fadeIn)
            canvasGroup.alpha = 1f;
        else
            canvasGroup.alpha = 0;
    }

    public void FadeInAndOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInAndOutRoutine());
    }

    IEnumerator FadeInAndOutRoutine()
    {
        StartCoroutine(FadeRoutine(true));
        if (ignoreTimeScale)
            yield return new WaitForSecondsRealtime(timePerFrame);
        else
            yield return new WaitForSeconds(timePerFrame);
        StartCoroutine(FadeRoutine(false));
    }

    public bool GetIsActive()
    {
        return canvasGroup.alpha > 0;
    }
}
