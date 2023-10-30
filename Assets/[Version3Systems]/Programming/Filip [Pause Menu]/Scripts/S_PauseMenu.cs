using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private S_PlayerControls playerControls;
    private static bool isPauseMenuActive = false;
    bool originalPauseState;

    public static bool IsPauseMenuActive
    {
        get => isPauseMenuActive;
    }

    private void Awake()
    {
        playerControls = new S_PlayerControls();
        playerControls.Player.Pause.performed += context =>
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            isPauseMenuActive = pauseMenu.activeSelf;
            if (pauseMenu.activeSelf)
            {
                originalPauseState = PauseManager.IsPaused;
                Debug.Log("Pause button pressed: " + originalPauseState);

                PauseManager.Pause();
            }
            if (!pauseMenu.activeSelf)
            {
                if (originalPauseState)
                    PauseManager.Pause();
                else
                    PauseManager.Unpause();
            }
        };
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