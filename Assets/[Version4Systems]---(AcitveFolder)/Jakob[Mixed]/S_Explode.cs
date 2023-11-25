using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Explode : MonoBehaviour
{
    S_FlashMaterials flasher;
    [SerializeField] float[] beepTimes = {1,1,0.5f,0.5f,0.25f,0.25f,0.1f,0.1f,0.1f};
    [SerializeField] int damage = 30;
    [SerializeField] float radius = 5;
    [SerializeField] GameObject explosion;
    bool isExploding = false;
    S_EnemyHealthController healthManager;
    bool gameIsPaused = false;
    private void OnEnable()
    {
        PauseManager.OnPauseStateChange += OnPauseChange;
    }

    private void OnDisable()
    {
        PauseManager.OnPauseStateChange -= OnPauseChange;
    }
    private void OnPauseChange(bool gamePaused)
    {
        gameIsPaused = gamePaused;
    }


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
            while (gameIsPaused)
            {
                print("paused");
                yield return null;
            }
            print("not paused");
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
            if(enemy.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemy.GetComponent<S_EnemyHealthController>().TakeDamage(damage);
            }
            else if(enemy.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                enemy.GetComponent<S_Health>().TakeDamage();
            }
        }
        healthManager.DissolveEnemy();
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
