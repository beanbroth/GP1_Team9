using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;

    [SerializeField] private GameObject quarkPrefab;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private S_EnemyAiBehviour enemyAiBehviour;
    [SerializeField] GameObject directionalHitEffectPrefab;
    [SerializeField] GameObject topHitEffectPrefab;
    bool dying = false;


    [SerializeField] Animator animator;
    S_FlashMaterials flasher;
    S_DissolveController dissolveController;

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
        dissolveController = GetComponent<S_DissolveController>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!dying)
        {
            ObjectPoolManager.Instantiate(topHitEffectPrefab, transform.position, Quaternion.identity);
            currentHealth -= damage;
            AudioManager.Instance.PlaySound3D("EnemyHit", transform.position);

            animator.SetTrigger("Take Damage");
            flasher.Flash();
        }
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        if (!dying)
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
        }
    }

    public void TrySpawnQuark()
    {
        if (currentHealth <= 0)
        {
            dying = true;

            AudioManager.Instance.PlaySound3D("EnemyDeath", transform.position);
            enemyAiBehviour.enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            dissolveController.StartDissolve();
            animator.SetTrigger("Death");
            ObjectPoolManager.Instantiate(quarkPrefab, transform.position, Quaternion.identity);
            Invoke("DestroyGameobject", dissolveController.GetDissolveDuration());
        }
    }

    void DestroyGameobject()
    {
        ObjectPoolManager.Destroy(gameObject);
    }
}