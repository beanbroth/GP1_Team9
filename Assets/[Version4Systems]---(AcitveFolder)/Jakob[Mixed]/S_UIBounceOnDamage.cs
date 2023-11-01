using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UIBounceOnDamage : MonoBehaviour
{
    UIScaleBounce bouncer;

    private void Awake()
    {
        bouncer = GetComponent<UIScaleBounce>();
    }
    private void OnEnable()
    {
        S_Health.OnDamage += Bounce;
    }

    private void OnDisable()
    {
        S_Health.OnDamage -= Bounce;
    }

    void Bounce()
    {
        bouncer.PerformBounceAnimation();
    }
}
