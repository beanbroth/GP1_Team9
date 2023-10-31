using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HUDManager : MonoBehaviour
{
    [SerializeField] GameObject[] hudPanels;

    public void ToggleHUD(bool newStatus)
    {
        foreach (GameObject panel in hudPanels)
        {
            panel.SetActive(newStatus);
        }
    }
}
