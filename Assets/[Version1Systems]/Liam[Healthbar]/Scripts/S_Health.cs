using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class S_Health : MonoBehaviour
{
    //action for when the player dies
    public static UnityAction OnDeath;

    //action for when the player takes damage
    public static UnityAction OnDamage;
    public int health; // Health
    public int numOfHearts; // Max number of hearts
    public GameObject[] hearts; // GameObject for the hearts in the top left
    public Sprite fullHeart;
    [SerializeField] private bool isInvincible = false; // Player's invincibility status
    [SerializeField] private float cooldownDuration = 2.0f; // Cooldown duration in seconds (for invincibility)
    [SerializeField] Animator playerAnimator;

    [SerializeField] float loseScreenDelay;

    // S_LossMenu loseMenu;
    S_FlashMaterials flasher;

     [SerializeField] S_CanvasGroupFader redScreenFlash;
    [SerializeField] UIScaleBounce healthBarContainer;

    private void Awake()
    {
        UpdateHealthUI();
        flasher = GetComponent<S_FlashMaterials>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        if (!isInvincible) // Checks if the player is invincible, if it's not, it takes damage and becomes invincible for 2 seconds
        {
            health--;
            health = Mathf.Clamp(health, 0, numOfHearts);
            UpdateHealthUI();
            flasher.DefaultFlash();
            if (health <= 0)
            {
                PlayerDeath();
                OnDeath?.Invoke();
            }
            else
            {
                PlayerHurt();
                OnDamage?.Invoke();
            }
        }
    }

    private void PlayerHurt()
    {
        AudioManager.Instance.PlaySound3D("PlayerHit", transform.position);
        //redScreenFlash.FadeInAndOut();
        //healthBarContainer.PerformBounceAnimation();
        isInvincible = true;
        playerAnimator.SetTrigger("Take Damage");
        Invoke("DisableInvincibility", cooldownDuration);
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
    }

    void PlayerDeath()
    {
        PauseManager.Pause();
        playerAnimator.SetTrigger("Death");
        //hudManager.ToggleHUD(false);
        //cameraFOVLerper.LerpFOV();
        //dissolveController.StartDissolve();
        AudioManager.Instance.PlaySound3D("PlayerDeath", transform.position);
        Invoke("OpenLoseMenu", loseScreenDelay);
    }

    void OpenLoseMenu()
    {
        FindFirstObjectByType<S_LossMenu>().LoseGame();
    }

    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd;
        health = Mathf.Clamp(health, 0, numOfHearts);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                if (i != 0)
                {
                    hearts[i].transform.rotation = hearts[i - 1].transform.rotation;
                }
                hearts[i].GetComponent<Image>().sprite = fullHeart;
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false); // Turns off the gameobject
            }
        }
    }
}