using System.Collections.Generic;
using UnityEngine;

public class S_RadiationAreaController : MonoBehaviour
{
    public float radius;
    public LayerMask enemyMask;
    public GameObject areaObject;
    public int damage;
    public float timeBetweenAttacks;
    Quaternion startRotation;
    //private Renderer _renderer;
    //[SerializeField] private Color flashColor;
    //private Color originalColor;

    //private float timer;

    private void Start()
    {
        //_renderer = areaObject.GetComponent<Renderer>();
        //originalColor = _renderer.material.color;
        InvokeRepeating("DamageTick", 0.0f, timeBetweenAttacks);
        areaObject.transform.localScale = new Vector3(1, 1, 1) * radius/3;
        startRotation = areaObject.transform.rotation; 
    }
    private void Update()
    {
        areaObject.transform.rotation = startRotation;
    }

    /*private void Update()
    {
        // Constantly change color
        timer += Time.deltaTime;
        float lerpValue = (Mathf.Sin(timer / timeBetweenAttacks * Mathf.PI * 2) + 1) / 2; // Oscillates between 0 and 1
        _renderer.material.color = Color.Lerp(originalColor, flashColor, lerpValue);

        areaObject.transform.localScale = new Vector3(radius * 2, areaObject.transform.localScale.y, radius * 2);
    }*/

    private void DamageTick()
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}