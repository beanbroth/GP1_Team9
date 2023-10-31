using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenu : MonoBehaviour
{
    private S_PlayerControls playerControls; // Reference to player inputs.
    S_SceneTransition sceneTransitionManager;
    [SerializeField] sceneEnum sceneToLoad = sceneEnum.introCutScene;
    [SerializeField] UIScaleBounce rightButtonUI;
    [SerializeField] UIScaleBounce leftButtonUI;


    private void Awake()
    {
        sceneTransitionManager = FindFirstObjectByType<S_SceneTransition>();
        playerControls = new S_PlayerControls(); // Initialize the player inputs.
        playerControls.Player.Turn.performed += context =>
        {

           float turnValue = context.ReadValue<float>();

           if (turnValue == 1f)
           {
                //quit application
                rightButtonUI.PerformBounceAnimation();
                Invoke("QuitGame",1);
           }
           
           if (turnValue == -1f)
           {
                // We have to change this name so that the start menu is found! 
                leftButtonUI.PerformBounceAnimation();
                sceneTransitionManager.SceneFadeOutAndLoadScene(Color.white, sceneToLoad);
           }

           AudioManager.Instance.PlaySound3D("Menu_Button_Press", transform.position);

        };
    }

    void QuitGame()
    {
        Debug.Log("quitting application");
        Application.Quit();
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
