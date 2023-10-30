using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BounceOnKeyPress : MonoBehaviour
{
    public UIScaleBounce uiScaleBounce; // Reference to the UIScaleBounce script attached to your UI element

    private void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            // Check if the UIScaleBounce script reference is set
            if (uiScaleBounce != null)
            {
                // Trigger the bounce animation
                uiScaleBounce.PerformBounceAnimation();
            }
            else
            {
                Debug.LogWarning("UIScaleBounce script reference is not set. Please assign it in the Inspector.");
            }
        }
    }
}
