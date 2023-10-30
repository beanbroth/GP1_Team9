using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_SceneTransition : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] private Color defaultTransitionColor = Color.white;
    [SerializeField] bool fadeInAtStart = true;
    [SerializeField] float animationFPS = 60f;
    [SerializeField] float animationDuration = 2f;

    private void Start()
    {
        if (fadeInAtStart)
        {
            Time.timeScale = 0;
            SceneFadeIn(defaultTransitionColor);
        }
    }

    public void SceneFadeIn(Color fadeColor)
    {
        backgroundImage.enabled = true;
        backgroundImage.color = new Color(fadeColor.r,fadeColor.g, fadeColor.b,1);
        StartCoroutine(FadeRoutine(fadeColor,false));
    }

    public void SceneFadeOutAndLoadScene(Color fadeColor, sceneEnum sceneRef)
    {
        backgroundImage.enabled = true;
        backgroundImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0);
        StartCoroutine(FadeRoutine(fadeColor, true));
        StartCoroutine(LoadSceneRoutine(S_SceneIndexManager.GetIndexFromEnum(sceneRef)));
    }

    IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(animationDuration);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
    
    IEnumerator FadeRoutine(Color fadeColor, bool fadeIn = true)
    {
        
        float animationTime = 0f;
        float timePerFrame = 1 / animationFPS;
        float currentAlpha = 0;
        if (!fadeIn)
            currentAlpha = 1;
        
        while (animationTime < animationDuration)
        {
            backgroundImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, currentAlpha);
            animationTime += timePerFrame;
            if (fadeIn)
            {
                currentAlpha = Mathf.Lerp(0, 1, animationTime / animationDuration);
            }
            else
            {
                currentAlpha = Mathf.Lerp(1, 0, animationTime / animationDuration);
            }
            
            yield return new WaitForSecondsRealtime(timePerFrame);
        }
        Time.timeScale = 1;
    }
}
