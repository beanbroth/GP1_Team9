using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class S_Health : MonoBehaviour
{
    public UnityEvent OnDeath; // Event for when the player dies
    
    
    public int health; // Health
    public int numOfHearts; // Max number of hearts

    public GameObject[] hearts; // GameObject for the hearts in the top left
    public Sprite fullHeart;
    [SerializeField]
    private bool isInvincible = false; // Player's invincibility status

    [SerializeField]
    private float cooldownDuration = 2.0f; // Cooldown duration in seconds (for invincibility)

    [SerializeField] Animator playerAnimator;
    S_LossMenu loseMenu;

    S_FlashMaterials flasher;
    S_DissolveController dissolveController;
    [SerializeField] S_CanvasGroupFader redScreenFlash;

    private void Awake()
    {
        UpdateHealthUI();
        loseMenu = FindFirstObjectByType<S_LossMenu>();
        flasher = GetComponent<S_FlashMaterials>();
        dissolveController= GetComponent<S_DissolveController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isInvincible) // Checks if the player is invincible, if it's not, it takes damage and becomes invincible for 2 seconds
            {
                health--;
                health = Mathf.Clamp(health, 0, numOfHearts);
                UpdateHealthUI();
                flasher.Flash();
                if (health <= 0)
                {
                    PlayerDeath();
                }
                else
                {
                    AudioManager.Instance.PlaySound3D("PlayerHit", transform.position);
                    redScreenFlash.FadeInAndOut();
                    isInvincible = true;
                    playerAnimator.SetTrigger("Take Damage");
                    Invoke("DisableInvincibility", cooldownDuration);
                }
            }
        }
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
    }

    void PlayerDeath()
    {
        PauseManager.Pause();
        playerAnimator.SetTrigger("Death");
        dissolveController.StartDissolve();
        AudioManager.Instance.PlaySound3D("PlayerDeath", transform.position);
        Invoke("OpenLoseMenu", dissolveController.GetDissolveDuration());
    }

    void OpenLoseMenu()
    {
        loseMenu.LoseGame();
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
