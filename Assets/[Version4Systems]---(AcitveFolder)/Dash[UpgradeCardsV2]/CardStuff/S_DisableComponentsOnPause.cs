using System.Collections.Generic;
using UnityEngine;

public class S_DisableComponentsOnPause : MonoBehaviour
{
    [SerializeField]
    protected List<Behaviour> componentsToDisable;
    private bool previousPauseState;

    private void Awake()
    {
        PauseManager.OnPauseStateChange += OnPauseChange;
        previousPauseState = PauseManager.IsPaused;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseStateChange -= OnPauseChange;
    }

    private void Update()
    {
        if (PauseManager.IsPaused != previousPauseState)
        {
            OnPauseChange(PauseManager.IsPaused);
            previousPauseState = PauseManager.IsPaused;
        }
    }

    private void OnPauseChange(bool gamePaused)
    {
        if (gamePaused)
        {
            DisableComponents();
        }
        else
        {
            EnableComponents();
        }
    }

    public virtual void DisableComponents()
    {
        foreach (var component in componentsToDisable)
        {
            component.enabled = false;
        }
    }

    public virtual void EnableComponents()
    {
        foreach (var component in componentsToDisable)
        {
            component.enabled = true;
        }
    }
}