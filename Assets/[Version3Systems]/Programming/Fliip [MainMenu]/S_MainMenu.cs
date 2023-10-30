using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenu : MonoBehaviour
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
               //quit application
                Application.Quit();
                Debug.Log("quitting application");
           }
           
           if (turnValue == -1f)
           {
                // We have to change this name so that the start menu is found! 
                SceneManager.LoadScene(1);
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
