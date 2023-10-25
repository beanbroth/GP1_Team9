using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu; // Reference to the pause screen.

    private S_PlayerControls playerControls; // Reference to player inputs.

    private bool isPaused = false; // Variable for if the game is paused or not.

    private void Awake()
    {
        playerControls = new S_PlayerControls(); // Initialize the player inputs.
        playerControls.Player.Pause.performed += context =>
        {
            Pause(); // Subscribe Pause() to the "Pause" input.
        };

        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();

            if (turnValue == 1f && isPaused)
            {
                SceneManager.LoadScene("Menu");
            }

            if (turnValue == -1f && isPaused)
            {
                Pause();
            }
        };
    }

    private void Pause()
    {
        isPaused = !isPaused; // Switch the bool, paused or not.
        bool alreadyPaused = (Time.timeScale == 0);

        if (isPaused) // If paused show the pause screen and pause time.
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else // Otherwise hide the pause screen and resumeause time.
        {
            PauseMenu.SetActive(false);
            if (!alreadyPaused)
                Time.timeScale = 1;
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    {
}
