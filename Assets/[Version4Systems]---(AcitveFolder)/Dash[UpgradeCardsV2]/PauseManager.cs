using UnityEngine;

public static class PauseManager
{
    public delegate void OnGamePausedDelegate(bool isPaused);
    public static event OnGamePausedDelegate OnPauseStateChange;

    private static bool isPaused;

    public static bool IsPaused { get => isPaused;}

    // public static void TogglePause()
    // {
    //     isPaused = !IsPaused;
    //     // Call the pause event.
    //     OnPauseStateChange?.Invoke(IsPaused);
    //
    //     Debug.Log("PauseManager: Game paused: " + IsPaused);
    // }

    public static void Pause()
    {
        if (IsPaused) return;

        isPaused = true;
        OnPauseStateChange?.Invoke(IsPaused);
        //Debug.Log("PauseManager: Game paused: " + IsPaused);
    }

    public static void Unpause()
    {
        if (!IsPaused) return;

        isPaused = false;
        OnPauseStateChange?.Invoke(IsPaused);
        //Debug.Log("PauseManager: Game unpaused: " + IsPaused);
    }
}