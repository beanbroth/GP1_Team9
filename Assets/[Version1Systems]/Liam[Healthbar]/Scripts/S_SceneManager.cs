using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    void Update()
    {
        // Check if the left arrow key is pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Load the "GamePrototypeV1Liam" scene
            SceneManager.LoadScene(gameSceneName);
        }

        // Check if the right arrow key is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Exit the application
            Debug.Log("Exiting the game.");
            Application.Quit();
        }
    }
}
