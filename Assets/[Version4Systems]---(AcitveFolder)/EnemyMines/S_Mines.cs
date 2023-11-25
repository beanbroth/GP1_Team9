using System.Collections;
using UnityEngine;

public class S_Mines : MonoBehaviour
{
    S_FlashMaterials flasher;
    [SerializeField] float[] beepTimes = {1,1,0.5f,0.5f,0.25f,0.25f,0.1f,0.1f,0.1f};
    [SerializeField] int damage = 30;
    [SerializeField] float radius = 5;
    [SerializeField] GameObject explosion;
    [SerializeField] bool hurtEnemies = true;
    bool isExploding = false;

    private void Awake()
    {
        flasher = GetComponent<S_FlashMaterials>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has touched the mine, initiate explosion
            InitiateExplosion();
        }
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
        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation, null);
        }
        AudioManager.Instance.PlaySound3D("Explosion", transform.position);
        Collider[] closeEnemies = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy","Player"));
        foreach (Collider enemy in closeEnemies)
        {
            if(hurtEnemies && enemy.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            {
                enemy.GetComponent<S_EnemyHealthController>().TakeDamage(damage);
            }
            
            if(enemy.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                enemy.GetComponent<S_Health>().TakeDamage();
            }
        }
        // Destroy the mine
        Destroy(gameObject);
    }

    void Beep()
    {
        AudioManager.Instance.PlaySound3D("ExplosionBeep",transform.position);
    }
    public bool GetIsExploding()
    {
        return isExploding;
    }
}
