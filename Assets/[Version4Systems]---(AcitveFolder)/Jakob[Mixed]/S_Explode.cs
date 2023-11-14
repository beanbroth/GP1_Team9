using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Explode : MonoBehaviour
{
    S_FlashMaterials flasher;
    [SerializeField] float[] beepTimes = {1,1,0.5f,0.5f,0.25f,0.25f,0.1f,0.1f,0.1f};
    [SerializeField] int damage = 30;
    [SerializeField] float radius = 3;
    [SerializeField] GameObject explosion;
    bool isExploding = false;
    S_EnemyHealthController healthManager;

    private void Awake()
    {
        flasher = GetComponent<S_FlashMaterials>();
        healthManager = GetComponent<S_EnemyHealthController>();
    }
    public void InitiateExplosion()
    {
        isExploding = true;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        foreach(float i in beepTimes)
        {
            Beep();
            flasher.Flash(i / 2);
            yield return new WaitForSeconds(i);
        }
        print("BOOM");
        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation, null);
        }
        AudioManager.Instance.PlaySound3D("PlayerDeath", transform.position);
        Collider[] closeEnemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (Collider enemy in closeEnemies)
        {
            enemy.GetComponent<S_EnemyHealthController>().TakeDamage(damage);
        }

        Collider[] cols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player"));
        print(closeEnemies.Length);
        GameObject player = null;
        if(cols.Length > 0)
        {
            player = cols[0].gameObject;
            print(cols[0]);
        }
        if (player != null)
        {
            player.GetComponent<S_Health>().TakeDamage();
        }
        healthManager.DissolveEnemy();
    }

    void Beep()
    {
        AudioManager.Instance.PlaySound3D("PlayerHit",transform.position);
    }
    public bool GetIsExploding()
    {
        return isExploding;
    }
}
