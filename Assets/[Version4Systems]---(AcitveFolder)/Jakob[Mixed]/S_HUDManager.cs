using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class S_HUDManager : MonoBehaviour
{
    private void OnEnable()
    {
        S_Health.OnDeath += TurnHUDOff;
    }
    
    private void OnDisable()
    {
        S_Health.OnDeath -= TurnHUDOff;
    }

    [SerializeField] GameObject[] hudPanels;

    public void TurnHUDOn()
    {
        foreach (GameObject panel in hudPanels)
        {
            panel.SetActive(true);
        }
    }

    public void TurnHUDOff()
    {
        Debug.Log("hud off");
        foreach (GameObject panel in hudPanels)
        {
            panel.SetActive(false);
        }
    }
}