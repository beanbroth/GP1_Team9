using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class S_LossMenu : MonoBehaviour
{
    [SerializeField] S_Health sHealthScript;  // Reference to the S_Health script
    [SerializeField] Canvas lossMenuCanvas;  // Reference to the LossMenu Canvas
    [SerializeField] TextMeshProUGUI loseTimerText;
    [SerializeField] TextMeshProUGUI losePhaseText;
    private S_PlayerControls playerControls; // Reference to player inputs.
    private bool isDead = false;
    S_WinTimer winTimer;
    S_PhaseManager phaseManager;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {
        playerControls = new S_PlayerControls(); // Initialize the player inputs.
        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();
            print("pressed button");
            if (turnValue == 1f && isDead)
            {
                print("menu");
                // Go to main menu.
                SceneManager.LoadScene("Menu");
            }

            if (turnValue == -1f && isDead)
            {
                print("reload");
                // Restart the game.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        };

        winTimer = FindFirstObjectByType<S_WinTimer>();
        phaseManager = FindFirstObjectByType<S_PhaseManager>();
    }
    private void Start()
    {
        // Initially, set the LossMenu Canvas to inactive
        lossMenuCanvas.gameObject.SetActive(false);    
    }

    private void Update()
    {
        if (sHealthScript != null && lossMenuCanvas != null)
        {
            int healthValue = sHealthScript.health;

            // Check if the health is 0 or less
            if (healthValue <= 0)
            {
                // If health is 0 or less, activate the LossMenu Canvas
                lossMenuCanvas.gameObject.SetActive(true);
                isDead = true;
                Time.timeScale = 0f;
                loseTimerText.text = winTimer.TimerText();
                losePhaseText.text = phaseManager.GetCurrentPhaseString();
            }
            else
            {
                // If health is greater than 0, make sure the LossMenu Canvas is inactive
                lossMenuCanvas.gameObject.SetActive(false);
            }
        }
    }
}
