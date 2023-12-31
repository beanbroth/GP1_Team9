using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    private bool isDead = false;
    [SerializeField] private GameObject quarkPrefab;
    [SerializeField] private GameObject directionalHitEffectPrefab;
    [SerializeField] private GameObject topHitEffectPrefab;
    [SerializeField] private Animator animator;
    private S_FlashMaterials flasher;
    private S_DissolveController dissolveController;
    private S_EnemyAiBehviour enemyAiBehviour;

    private void Awake()
    {
        flasher = GetComponent<S_FlashMaterials>();
        dissolveController = GetComponent<S_DissolveController>();
        enemyAiBehviour = GetComponent<S_EnemyAiBehviour>();

    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void ResetEnemy()
    {
        currentHealth = maxHealth;

        enemyAiBehviour.enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        isDead = false;
    }

    public void TakeDamage(int damage, Vector3? direction = null)
    {
        if (isDead)
            return;
        currentHealth -= damage;
        AudioManager.Instance.PlaySound3D("EnemyHit", transform.position);
        flasher.DefaultFlash();
        InstantiateHitEffect(direction);
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Take Damage");
        }
    }

    private void InstantiateHitEffect(Vector3? direction)
    {
        GameObject hitEffectPrefab = topHitEffectPrefab;
        Quaternion rotation = Quaternion.identity;
        if (direction.HasValue)
        {
            hitEffectPrefab = directionalHitEffectPrefab;
            rotation = Quaternion.LookRotation(direction.Value);
        }

        ObjectPoolManager.Instantiate(hitEffectPrefab, transform.position, rotation);
    }

    private void Die()
    {
        isDead = true;
        AudioManager.Instance.PlaySound3D("EnemyDeath", transform.position);
        enemyAiBehviour.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        dissolveController.StartDissolve();
        animator.SetTrigger("Death");
        ObjectPoolManager.Instantiate(quarkPrefab, transform.position, Quaternion.identity);
        Invoke("DestroyGameObject", dissolveController.GetDissolveDuration());
    }

    private void DestroyGameObject()
    {
        ObjectPoolManager.Destroy(gameObject);
    }
}