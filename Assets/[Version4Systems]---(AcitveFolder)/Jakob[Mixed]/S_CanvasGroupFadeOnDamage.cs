using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CanvasGroupFadeOnDamage : MonoBehaviour
{
    S_CanvasGroupFader fader;

    private void Awake()
    {
        fader = GetComponent<S_CanvasGroupFader>();
    }
    private void OnEnable()
    {
            S_Health.OnDamage += FadeInAndOut;
    }

    private void OnDisable()
    {
        S_Health.OnDamage -= FadeInAndOut;
    }

    private void FadeInAndOut()
    {
        fader.FadeInAndOut();
    }
}
