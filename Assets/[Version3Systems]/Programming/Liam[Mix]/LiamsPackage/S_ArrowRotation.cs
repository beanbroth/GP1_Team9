using UnityEngine;
using System.Collections;

public class S_ArrowRotation : MonoBehaviour
{
    [SerializeField]
    private S_PlayerMovement playerMovement;

    private Material arrowMaterial; // No need to serialize it
    [SerializeField] private Material radiusMaterial; // No need to serialize it

    private S_Health playerHealth;

    [SerializeField]
    private Transform objectToRotate;

    [SerializeField]
    private float rotationAngleNegative = 80.0f;
    [SerializeField]
    private float rotationAnglePositive = 80.0f;

    [SerializeField]
    private float rotationSpeed = 5.0f;

    private float targetRotationY;

private void OnEnable()
{
    S_Health.OnDeath += StartMaterialFadeAnimation;
    InitializeMaterials();
}

private void OnDisable()
{
    S_Health.OnDeath -= StartMaterialFadeAnimation;
}

private void InitializeMaterials()
{
    // Try to find the arrow material in the Renderer component of this GameObject
    Renderer arrowRenderer = GetComponent<Renderer>();
    if (arrowRenderer != null)
    {
        arrowMaterial = arrowRenderer.material;
    }
    else
    {
        Debug.LogWarning("Arrow material not found on this GameObject.");
    }

    // Find GameObject with the "Radius" tag and get its material
    GameObject radiusObject = GameObject.FindWithTag("Radius");
    if (radiusObject != null)
    {
        Renderer radiusRenderer = radiusObject.GetComponent<Renderer>();
        if (radiusRenderer != null)
        {
            radiusMaterial = radiusRenderer.material;
        }
        else
        {
            Debug.LogWarning("Radius material not found on the object with the 'Radius' tag.");
        }
    }
    else
    {
        Debug.LogWarning("Object with the 'Radius' tag not found.");
    }

    // Apply the same treatment to both materials
    SetMaterialAlpha(arrowMaterial, 1);
    SetMaterialAlpha(radiusMaterial, 1);
}

    private void StartMaterialFadeAnimation()
    {
        StartCoroutine(FadeMaterials(1, 0, 1.0f)); // Start a fade animation from alpha 1 to 0 in 1 second
    }

    private void SetMaterialAlpha(Material material, float alpha)
    {
        if (material != null)
        {
            Color materialColor = material.color;
            materialColor.a = alpha;
            material.color = materialColor;
        }
    }

    private IEnumerator FadeMaterials(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetMaterialAlpha(arrowMaterial, alpha);
            SetMaterialAlpha(radiusMaterial, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the materials are set to the endAlpha value
        SetMaterialAlpha(arrowMaterial, endAlpha);
        SetMaterialAlpha(radiusMaterial, endAlpha);
    }


    private void Update()
    {
        if (PauseManager.IsPaused)
            return;

        float playerRotationY = playerMovement.transform.rotation.eulerAngles.y - 180f;

        float turnDirection = playerMovement.TurnDirection;

        if (turnDirection == 0f)
        {
            targetRotationY = playerRotationY;
        }
        else if (turnDirection == -1f)
        {
            targetRotationY = playerRotationY - rotationAngleNegative;
        }
        else if (turnDirection == 1f)
        {
            targetRotationY = playerRotationY + rotationAnglePositive;
        }

        Quaternion targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}