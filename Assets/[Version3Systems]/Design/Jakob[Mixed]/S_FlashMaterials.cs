using System.Collections;
using UnityEngine;

public class S_FlashMaterials : MonoBehaviour
{
    [SerializeField] private Transform parentWithMaterials;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float defaultFlashDuration = 0.1f;
    
    private Renderer[] renderers;
    private Material[] defaultMaterials;

    private void Start()
    {
        InitializeMaterials();
    }

    private void InitializeMaterials()
    {
        renderers = parentWithMaterials.GetComponentsInChildren<Renderer>();
        defaultMaterials = new Material[renderers.Length];
        
        for (int i = 0; i < renderers.Length; i++)
        {
            defaultMaterials[i] = renderers[i].material;
        }
    }

    public void DefaultFlash()
    {
        Flash(defaultFlashDuration);
    }

    public void Flash(float flashTime)
    {
        StartCoroutine(FlashCoroutine(flashTime));
    }
    
    private IEnumerator FlashCoroutine(float flashTime)
    {
        SetAllMaterials(flashMaterial);
        yield return new WaitForSeconds(flashTime);
        RestoreDefaultMaterials();
    }

    private void SetAllMaterials(Material material)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
        }
    }

    private void RestoreDefaultMaterials()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = defaultMaterials[i];
        }
    }
}