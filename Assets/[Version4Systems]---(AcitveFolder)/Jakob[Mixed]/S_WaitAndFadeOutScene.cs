using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WaitAndFadeOutScene : MonoBehaviour
{
    S_SceneTransition sceneTransition;
    [SerializeField] float delayUntilFadeOut = 1f;
    [SerializeField] sceneEnum sceneRef;
    [SerializeField] Color fadeColor = Color.white;

    private void Awake()
    {
        sceneTransition = FindFirstObjectByType<S_SceneTransition>();
        Invoke("TransitionScene",delayUntilFadeOut);
    }

    void TransitionScene()
    {
        sceneTransition.SceneFadeOutAndLoadScene(fadeColor,S_SceneIndexManager.GetIndexFromEnum(sceneRef));
    }
}
