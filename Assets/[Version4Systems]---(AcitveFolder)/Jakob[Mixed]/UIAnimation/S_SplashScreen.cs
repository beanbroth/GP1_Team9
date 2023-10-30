using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SplashScreen : MonoBehaviour
{
    [SerializeField] float bounceDelay = 2.5f;
    [SerializeField] float animationDelay = 0.5f;
    [SerializeField] float loadMenuDelay = 4f;
    UIScaleBounce bouncer;
    [SerializeField] Animator rotationAnimator;
    void Start()
    {
        bouncer = GetComponent<UIScaleBounce>();
        Invoke("PrepareAnimation", animationDelay);
        Invoke("PrepareBounce", bounceDelay);
        Invoke("LoadMenu", loadMenuDelay);
    }
    void PrepareAnimation()
    {
        rotationAnimator.SetTrigger("Rotate");
    }
    void PrepareBounce()
    {
        bouncer.PerformBounceAnimation();
    }
    void LoadMenu()
    {
        SceneManager.LoadScene(S_SceneIndexManager.menuIndex);
    }
}
