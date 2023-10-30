using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement; 

public class S_LoseMenu : MonoBehaviour
{
    [SerializeField] private GameObject LoseMenu; // Reference to the pause screen.
    private S_PlayerControls playerControls; // Reference to player inputs.
    public S_Health playerHealth;

    private void Awake()
    {
        playerControls = new S_PlayerControls(); // Initialize the player inputs.
        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();

            if (turnValue == 1f)
            {
                // Go to main menu.
                SceneManager.LoadScene("Menu");
            }

            if (turnValue == -1f)
            {
                // Restart the game.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        };
    }

    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (playerHealth.health == 0)
        {
            Debug.Log("working");
            LoseMenu.SetActive(true);
            Time.timeScale = 0;
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
}
