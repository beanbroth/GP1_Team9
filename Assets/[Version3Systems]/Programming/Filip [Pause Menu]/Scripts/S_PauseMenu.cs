using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private S_PlayerControls playerControls;
    private static bool isPauseMenuActive = false;
    bool originalPauseState;
    [SerializeField] private UIScaleBounce leftButtonUI;
    [SerializeField] private UIScaleBounce rightButtonUI;
    [SerializeField] private S_CanvasGroupFader canvasGroupFader;
    S_SceneTransition sceneTransitionManager;
    [SerializeField] float resumeDelay = 1f;

    public static bool IsPauseMenuActive
    {
        get => isPauseMenuActive;
    }

    private void Awake()
    {
        sceneTransitionManager = FindFirstObjectByType<S_SceneTransition>();

        playerControls = new S_PlayerControls();
        playerControls.Player.Pause.performed += context =>
        {
            //pauseMenu.SetActive(!pauseMenu.activeSelf);
            //isPauseMenuActive = pauseMenu.activeSelf;
            isPauseMenuActive = canvasGroupFader.GetIsActive();
            if (!isPauseMenuActive)
            {
                originalPauseState = PauseManager.IsPaused;
                Debug.Log("Pause button pressed: " + originalPauseState);
                canvasGroupFader.FadeIn();
                isPauseMenuActive = true;
                if (!originalPauseState)
                {
                    PauseManager.Pause();
                }
            }
            else
            {
                canvasGroupFader.FadeOut();
                isPauseMenuActive = false;
                if (!originalPauseState)
                {
                    Invoke("DelayedUnpause", resumeDelay);
                }
            }
        };

        playerControls.Player.Turn.performed += context =>
        {
            if (isPauseMenuActive)
            {
                float turnValue = context.ReadValue<float>();

                if (turnValue == 1f)
                {
                    rightButtonUI.PerformBounceAnimation();
                    isPauseMenuActive = false;
                    sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.menu);
                }

                if (turnValue == -1f)
                {
                    if (!originalPauseState)
                    {
                        Invoke("DelayedUnpause", resumeDelay);
                    }
                    canvasGroupFader.FadeOut();
                    leftButtonUI.PerformBounceAnimation();
                    isPauseMenuActive = false;
                }
                AudioManager.Instance.PlaySound3D("Menu_Button_Press", transform.position);
            }
        };
    }

    void DelayedUnpause()
    {
        PauseManager.Unpause();
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