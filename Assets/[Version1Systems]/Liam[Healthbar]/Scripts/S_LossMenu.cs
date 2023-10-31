using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class S_LossMenu : MonoBehaviour
{
    [SerializeField] Canvas lossMenuCanvas;  // Reference to the LossMenu Canvas
    [SerializeField] TextMeshProUGUI loseTimerText;
    [SerializeField] TextMeshProUGUI losePhaseText;
    private S_PlayerControls playerControls; // Reference to player inputs.
    private bool isDead = false;
    S_WinTimer winTimer;
    S_PhaseManager phaseManager;
    S_SceneTransition sceneTransitionManager;
    [SerializeField] UIScaleBounce leftButtonUI;
    [SerializeField] UIScaleBounce rightButtonUI;
    Transform playerPosition;

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
                rightButtonUI.PerformBounceAnimation();
                sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.menu);
                AudioManager.Instance.PlaySound3D("Menu_Button_Press", transform.position);
            }

            if (turnValue == -1f && isDead)
            {
                leftButtonUI.PerformBounceAnimation();
                sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneEnum.game);
                AudioManager.Instance.PlaySound3D("Menu_Button_Press", transform.position);
            }
        };

        winTimer = FindFirstObjectByType<S_WinTimer>();
        phaseManager = FindFirstObjectByType<S_PhaseManager>();
        playerPosition = FindFirstObjectByType<S_PlayerMovement>().transform;
    }
    private void Start()
    {
        // Initially, set the LossMenu Canvas to inactive
        lossMenuCanvas.gameObject.SetActive(false);    
    }

    public void LoseGame()
    {
        lossMenuCanvas.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound3D("Game_Over", playerPosition.position);
        isDead = true;
        Time.timeScale = 0f;
        loseTimerText.text = winTimer.TimerText();
        losePhaseText.text = phaseManager.GetCurrentPhaseString();
    }
}
