using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;

    [SerializeField] private GameObject quarkPrefab;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private S_EnemyAiBehviour enemyAiBehviour;
    [SerializeField] GameObject directionalHitEffectPrefab;
    [SerializeField] GameObject topHitEffectPrefab;

    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip deathSound; //Not used at the moment
    AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(damageSound);
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
        audioSource.PlayOneShot(damageSound);
        currentHealth -= damage;
        flasher.Flash();
    }

    public void TrySpawnQuark()
    {
        if (currentHealth <= 0)
        {
            ObjectPoolManager.Instantiate(quarkPrefab, transform.position, Quaternion.identity);

            ObjectPoolManager.Destroy(gameObject);
        }
    }
}