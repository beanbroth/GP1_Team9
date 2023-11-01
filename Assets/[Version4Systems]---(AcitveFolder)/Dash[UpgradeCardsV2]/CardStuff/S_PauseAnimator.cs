using UnityEngine;

public class S_PauseAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PauseManager.OnPauseStateChange += OnPauseChange; // Subscribe to the OnGamePaused event
    }

    private void OnDisable()
    {
        PauseManager.OnPauseStateChange -= OnPauseChange; // Unsubscribe from the OnGamePaused event
    }

    private void OnPauseChange(bool gamePaused)
    {
        if (gamePaused)
        {
            PauseAnimator();
        }
        else
        {
            ResumeAnimator();
        }
    }

    private void PauseAnimator()
    {
        animator.speed = 0; // Pause the animator
    }

    private void ResumeAnimator()
    {
        animator.speed = 1; // Resume the animator
    }
}