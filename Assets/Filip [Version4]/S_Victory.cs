using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Victory : MonoBehaviour
{
    private S_PlayerControls playerControls; // Reference to player inputs.

    private void Awake()
    {
        playerControls = new S_PlayerControls(); // Initialize the player inputs.
        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();

            if (turnValue == 1f)
            {
                // Switch to build index 0
                Application.Quit();
            }

            if (turnValue == -1f)
            {
                // Switch to build index 1
                SceneManager.LoadScene(0);
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