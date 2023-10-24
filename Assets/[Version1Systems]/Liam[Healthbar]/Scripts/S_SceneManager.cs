using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    private S_PlayerControls playerControls;
    private bool turnKeyPressed;

    private void Awake()
    {
        playerControls = new S_PlayerControls();
        playerControls.Player.Turn.performed += context =>
        {
            turnKeyPressed = true;
        };

        playerControls.Player.Turn.canceled += context =>
        {
            turnKeyPressed = false;
        };
    }

    private void Update()
    {
        if (turnKeyPressed)
        {
            LoadGameScene();
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

    private void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}