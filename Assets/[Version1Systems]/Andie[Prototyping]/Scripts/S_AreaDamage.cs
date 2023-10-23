using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AndieAreaDamage : MonoBehaviour
{
    public float radius;
    public LayerMask enemyMask;
    public GameObject areaObject;
    public int damage;
    [FormerlySerializedAs("waitTime")] public float timeBetweenAttacks;
    public float flashDuration = 0.1f;
    private Renderer _renderer;
    [FormerlySerializedAs("color")][SerializeField] private Color flashColor;

    private void Start()
    {
        _renderer = areaObject.GetComponent<Renderer>();
        InvokeRepeating("DamageTick", 0.0f, timeBetweenAttacks);
    }

    private void Update()
    {
        areaObject.transform.localScale = new Vector3(radius * 2, areaObject.transform.localScale.y, radius * 2);
    }

    private void OnValidate()
    {
        areaObject.transform.localScale = new Vector3(radius * 2, areaObject.transform.localScale.y, radius * 2);
    }

    private void DamageTick()
    {
        StartCoroutine(FlashWhite());

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, radius, enemyMask);

        foreach (Collider enemyCollider in enemiesInRange)
        {
            S_EnemyHealthController emc = enemyCollider.GetComponent<S_EnemyHealthController>();
            if (emc != null)
            {
                emc.TakeDamage(damage);
            }
        }
    }

    private IEnumerator FlashWhite()
    {
        Color originalColor = _renderer.material.color;
        _renderer.material.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        _renderer.material.color = originalColor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}