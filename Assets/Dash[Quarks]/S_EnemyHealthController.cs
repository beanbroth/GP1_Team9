using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth = 3;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private GameObject quarkPrefab;
    [SerializeField] private Renderer enemyRenderer;
    private Color originalColor;
    [SerializeField] private S_EnemyAiBehviour enemyAiBehviour;

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
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashOnDamage());
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameObject temp = S_ObjectPoolManager.Instance.GetObject(quarkPrefab);
            temp.transform.position = transform.position;
        }
    }

    private IEnumerator FlashOnDamage()
    {
        enemyRenderer.material.color = flashColor;
        enemyAiBehviour.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        enemyRenderer.material.color = originalColor;
        enemyAiBehviour.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.tag == "PlayerBullet")
            TakeDamage(1);
    }
}