using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AnimateCardSelectUI : MonoBehaviour
{
    [SerializeField] bool animateLeft = false;
    [SerializeField] bool animateRight = false;
    [SerializeField] bool animateMiddle = false;
    UIScaleBounce bouncer;
    private void Awake()
    {
        bouncer = GetComponent<UIScaleBounce>();
    }

    private void OnEnable()
    {
        if (animateLeft)
            S_UpgradeManager.ScrollLeft += Bounce;
        if (animateRight)
            S_UpgradeManager.ScrollRight += Bounce;
        if (animateMiddle)
            S_UpgradeManager.SelectCard += Bounce;
    }
    private void OnDisable()
    {
        if(animateLeft)
            S_UpgradeManager.ScrollLeft -= Bounce;
        if(animateRight)
            S_UpgradeManager.ScrollRight -= Bounce;
        if( animateMiddle)
            S_UpgradeManager.SelectCard -= Bounce;
    }
    void Bounce()
    {
        bouncer.PerformBounceAnimation();
    }
}
