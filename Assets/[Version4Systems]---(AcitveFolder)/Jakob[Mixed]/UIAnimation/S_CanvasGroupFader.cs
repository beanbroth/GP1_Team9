using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CanvasGroupFader : MonoBehaviour
{
    [SerializeField] CanvasRenderer canvasRenderer;
    [SerializeField] float animationFPS = 60f;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] bool ignoreTimeScale = true;

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
        float timePerFrame = 1 / animationFPS;
        float currentAlpha = 0;
        if (!fadeIn)
            currentAlpha = 1;

        while (animationTime < animationDuration)
        {
            canvasRenderer.SetAlpha(currentAlpha);
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
    }
}
