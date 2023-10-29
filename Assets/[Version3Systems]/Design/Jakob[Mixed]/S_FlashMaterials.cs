using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FlashMaterials : MonoBehaviour
{
    [SerializeField] Transform parentWithMaterials;
    Material[] defaultMaterials;
    [SerializeField] Material flashMaterial;
    Renderer[] renderers;
    [SerializeField] bool isEnemy = true;
    S_EnemyHealthController enemyHealthController;
    [SerializeField] float flashDuration = 0.2f;
    [SerializeField] S_EnemyAiBehviour enemyAiBehviour;

    private void Awake()
    {
        if (isEnemy)
        {
            enemyHealthController = GetComponent<S_EnemyHealthController>();
            if (enemyAiBehviour == null)
            {
                enemyAiBehviour = GetComponent<S_EnemyAiBehviour>();
            }
            if (enemyAiBehviour == null)
            {
                Debug.Log("No S_EnemyAiBehviour assigned to FlashMaterials");
            }
        }
    }

    private void Start()
    {
        renderers = parentWithMaterials.GetComponentsInChildren<Renderer>();
        defaultMaterials = new Material[renderers.Length];
        int idx = 0;
        foreach (Renderer renderer in renderers)
        {
            defaultMaterials[idx] = renderer.material;
            idx++;
        }
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }
    private IEnumerator FlashCoroutine()
    {
        // Change from color editing to materials instead
        foreach(Renderer renderer in renderers)
        {

        }
        foreach (Renderer renderer in renderers)
        {
            renderer.material = flashMaterial;
        }
        if (isEnemy)
            enemyAiBehviour.enabled = false;
        yield return new WaitForSeconds(flashDuration);

        int idx = 0;
        foreach (Renderer renderer in renderers)
        {
            renderer.material = defaultMaterials[idx];
            idx++;
        }
        if (isEnemy)
        {
            enemyAiBehviour.enabled = true;
            enemyHealthController.TrySpawnQuark();
        }
    }
}
