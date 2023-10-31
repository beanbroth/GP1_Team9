using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum sceneEnum
{
    splashScreen,
    menu,
    introCutScene,
    game,
    outroCutScene,
    victoryScreen,
    credits
}
public class S_SceneIndexManager : MonoBehaviour
{
    [SerializeField] public static readonly int splashScreenIndex = 0;
    [SerializeField] public static readonly int menuIndex = 1;
    [SerializeField] public static readonly int introCutSceneIndex = 2;
    [SerializeField] public static readonly int gameIndex = 3;
    [SerializeField] public static readonly int outroCutSceneIndex = 4;
    [SerializeField] public static readonly int victoryScreenIndex = 5;
    [SerializeField] public static readonly int creditsIndex = 6;

    public static int GetIndexFromEnum(sceneEnum sceneName)
    {
        switch (sceneName)
        {
            case sceneEnum.splashScreen: return splashScreenIndex;
            case sceneEnum.menu: return menuIndex;
            case sceneEnum.introCutScene: return introCutSceneIndex;
            case sceneEnum.game: return gameIndex;
            case sceneEnum.outroCutScene: return outroCutSceneIndex;
            case sceneEnum.victoryScreen: return victoryScreenIndex;
            case sceneEnum.credits: return creditsIndex;
            default: return -1;

        }
    }
}
