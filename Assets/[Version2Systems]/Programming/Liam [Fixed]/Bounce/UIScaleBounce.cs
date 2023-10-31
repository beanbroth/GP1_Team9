using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScaleBounce : MonoBehaviour
{
    public RectTransform uiElement;  // Reference to the UI element's RectTransform
    public float bounceScale = 1.2f; // The scale factor for the bounce
    public float bounceDuration = 0.5f; // Duration of the bounce animation
    [SerializeField] bool ignoreTimeScale = false;

    private Vector3 originalScale;  // The original scale of the UI element

    private void Awake()
    {
        if(uiElement == null)
        {
            uiElement = GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        if (uiElement == null)
        {
            Debug.LogError("UI element is not assigned.");
            enabled = false; // Disable the script if the UI element is not assigned
        }

        // Store the original scale of the UI element
        originalScale = uiElement.localScale;
    }

    public void PerformBounceAnimation()
    {
        // Stop any previous bounce coroutine
        StopAllCoroutines();

        // Start the bounce animation
        StartCoroutine(BounceAnimation());
    }

    private IEnumerator BounceAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < bounceDuration)
        {
            float t = elapsedTime / bounceDuration;

            // Calculate the new scale with bounce effect
            Vector3 newScale = originalScale + (bounceScale - 1) * Mathf.Sin(t * Mathf.PI) * originalScale;

            // Apply the new scale to the UI 
            uiElement.localScale = newScale;

            if (!ignoreTimeScale)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            else
            {
                elapsedTime += 0.015f;
                yield return new WaitForSecondsRealtime(0.015f);
            }
        }

        // Ensure the scale is back to the original size
        uiElement.localScale = originalScale;
    }
}
