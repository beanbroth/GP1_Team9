using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutroCutscene : MonoBehaviour
{
    S_SceneTransition sceneTransitionManager;
    private S_PlayerControls playerControls;
    [SerializeField] GameObject button;
    [SerializeField] float sceneTime = 2f;
    bool SceneDone = false;

    private void Awake()
    {
        sceneTransitionManager = FindFirstObjectByType<S_SceneTransition>();

        playerControls = new S_PlayerControls();
        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();
            if (turnValue == 1f && SceneDone)
            {
                SceneDone = false;
                UIScaleBounce bouncer = button.GetComponent<UIScaleBounce>();
                bouncer.PerformBounceAnimation();
                StartCoroutine(DisableButtonAndTransitionScene(bouncer, true));
 
            }
        };

        button.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(SceneTimer());
    }

    IEnumerator SceneTimer()
    {
        yield return new WaitForSeconds(sceneTime);
        SceneDone = true;
        button.SetActive(true);
    }

    IEnumerator DisableButtonAndTransitionScene(UIScaleBounce bouncer, bool transitionScene)
    {
        yield return new WaitForSecondsRealtime(bouncer.bounceDuration);
        button.SetActive(false);
        if (transitionScene)
            sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.credits);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
