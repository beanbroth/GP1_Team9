using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private float flashDuration = 0.1f;
    private Color originalColor;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private GameObject quarkPrefab;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private S_EnemyAiBehviour enemyAiBehviour;
    [SerializeField] GameObject directionalHitEffectPrefab;
    [SerializeField] GameObject topHitEffectPrefab;

    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip deathSound; //Not used at the moment
    AudioSource audioSource;

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
        originalColor = enemyRenderer.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        ObjectPoolManager.Instantiate(topHitEffectPrefab, transform.position, Quaternion.identity);
        currentHealth -= damage;
        audioSource.PlayOneShot(damageSound);
        StartCoroutine(FlashOnDamage());
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
        audioSource.PlayOneShot(damageSound);
        currentHealth -= damage;
        StartCoroutine(FlashOnDamage());
    }

    private IEnumerator FlashOnDamage()
    {
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