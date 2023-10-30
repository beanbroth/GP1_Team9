using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;

    [Header("Flash When Taking Damage")]
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Color flashColor = Color.red;
    private Color originalColor;

    [SerializeField] private GameObject quarkPrefab;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private S_EnemyAiBehviour enemyAiBehviour;
    [SerializeField] GameObject directionalHitEffectPrefab;
    [SerializeField] GameObject topHitEffectPrefab;


    [SerializeField] Animator animator;
    S_FlashMaterials flasher;

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    private void Awake()
    {
        flasher = GetComponent<S_FlashMaterials>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        ObjectPoolManager.Instantiate(topHitEffectPrefab, transform.position, Quaternion.identity);
        currentHealth -= damage;
        AudioManager.Instance.PlaySound3D("EnemyHit", transform.position);

        animator.SetTrigger("Take Damage");
        flasher.Flash();
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        if (direction.y < 0) // Assuming the hit is from the top if the direction has a negative Y value
        {
            ObjectPoolManager.Instantiate(topHitEffectPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Quaternion effectRotation = Quaternion.LookRotation(direction);
            ObjectPoolManager.Instantiate(directionalHitEffectPrefab, transform.position, effectRotation);
        }
        animator.SetTrigger("Take Damage");
        AudioManager.Instance.PlaySound3D("EnemyHit", transform.position);
         currentHealth -= damage;
        flasher.Flash();
        //StartCoroutine(FlashOnDamage());
    }

    public void TrySpawnQuark()
    {
        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlaySound3D("EnemyDeath", transform.position);

            ObjectPoolManager.Instantiate(quarkPrefab, transform.position, Quaternion.identity);

            ObjectPoolManager.Destroy(gameObject);
        }
    }

    private IEnumerator FlashOnDamage()
    {
        // Change from color editing to materials instead
        enemyRenderer.material.color = flashColor;
        enemyAiBehviour.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
        enemyAiBehviour.enabled = true;
        if (currentHealth <= 0)
        {
            ObjectPoolManager.Instantiate(quarkPrefab, transform.position, Quaternion.identity);
            
            ObjectPoolManager.Destroy(gameObject);
        }
    }
}