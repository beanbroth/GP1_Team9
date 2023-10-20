using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AndieAreaDamage : MonoBehaviour
{
    public float radius;
    public LayerMask enemyMask;
    public GameObject areaObject;
    //public float damage;
    [FormerlySerializedAs("waitTime")] public float timeBetweenAttacks;
    public float flashDuration = 0.1f;
    private Renderer _renderer;
    [FormerlySerializedAs("color")] [SerializeField] private Color flashColor;

    private void Start()
    {
        _renderer = areaObject.GetComponent<Renderer>();
        InvokeRepeating("SphereCheck", 0.0f, timeBetweenAttacks);
    }

    private void Update()
    {
        areaObject.transform.localScale = new Vector3(radius * 2, 0.25f, radius * 2);
    }

    private void OnValidate()
    {
        areaObject.transform.localScale = new Vector3(radius * 2, 0.07f, radius * 2);
    }

    private void SphereCheck()
    {
        StartCoroutine(FlashWhite());

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, enemyMask);
        foreach (var hitCollider in hitColliders)
        {
            //damage enmy   
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