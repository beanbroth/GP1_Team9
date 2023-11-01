using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OnboardingUIPopUp : MonoBehaviour
{
    [SerializeField] GameObject sceneTransition;
    [SerializeField] S_CanvasGroupFader onboardingUI;
    [SerializeField] float delayUntilEnabledControls = 1.5f;
    private S_PlayerControls playerControls;
    [SerializeField] S_CanvasGroupFader button;
    bool controlsEnabled = false;
    private void Awake()
    {
        sceneTransition.GetComponent<Image>().enabled = true;
        if(onboardingUI == null)
            onboardingUI = GetComponent<S_CanvasGroupFader>();

        playerControls = new S_PlayerControls();
        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();

            if (turnValue == 1f && controlsEnabled)
            {
                onboardingUI.FadeOut();
                button.GetComponent<UIScaleBounce>().PerformBounceAnimation();
                sceneTransition.GetComponent<S_SceneTransition>().SceneFadeIn(Color.white);
                StartCoroutine(DelayedDestruction(onboardingUI.animationDuration));
            }
        };

        StartCoroutine(EnableButton());
    }
    private void Start()
    {
        Time.timeScale = 0;
        onboardingUI.FadeIn();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    IEnumerator EnableButton()
    {
        yield return new WaitForSecondsRealtime(delayUntilEnabledControls);
        button.FadeIn();
        controlsEnabled = true;
    }

    IEnumerator DelayedDestruction(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }
}
