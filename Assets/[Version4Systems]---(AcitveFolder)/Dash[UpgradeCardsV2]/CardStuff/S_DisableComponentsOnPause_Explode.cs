using System.Collections.Generic;
using UnityEngine;

public class S_DisableComponentsOnPauseExplode : S_DisableComponentsOnPause
{
    bool isExploding = false;
    S_Explode explodeManager;

    private void Start()
    {
        explodeManager = GetComponent<S_Explode>();
    }

    public override void DisableComponents()
    {
        foreach (var component in componentsToDisable)
        {
            component.enabled = false;
        }
        isExploding = explodeManager.isExploding;
    }

    public override void EnableComponents()
    {
        if (!isExploding)
        {
            foreach (var component in componentsToDisable)
            {
                component.enabled = true;
            }
        }
    }
}