using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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
    S_SceneTransition sceneTransitionManager;

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
        sceneTransitionManager = FindFirstObjectByType<S_SceneTransition>();
        playerControls.Player.Turn.performed += context =>
        {
            float turnValue = context.ReadValue<float>();
            if (turnValue == 1f && isDead)
            {
                sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.menu);
            }

            if (turnValue == -1f && isDead)
            {
                sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.game);
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

    public void LoseGame()
    {
        lossMenuCanvas.gameObject.SetActive(true);
        isDead = true;
        Time.timeScale = 0f;
        loseTimerText.text = winTimer.TimerText();
        losePhaseText.text = phaseManager.GetCurrentPhaseString();
    }
}
