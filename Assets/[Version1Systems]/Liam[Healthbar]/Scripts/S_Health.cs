using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        UpdateHealthUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isInvincible) // Checks if the player is invincible, if it's not, it takes damage and becomes invincible for 2 seconds
            {
                //Debug.Log("Player collided with an enemy!");
                health--;
                health = Mathf.Clamp(health, 0, numOfHearts);
                UpdateHealthUI();
                isInvincible = true;
                Invoke("DisableInvincibility", cooldownDuration);

                if (health <= 0)
                {
                    // Player has lost all their health, switch to the "Lose" scene
                    SceneManager.LoadScene("Lose");
                }
            }
        }
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
    }

    /*void Update()
    {
        health = Mathf.Clamp(health, 0, numOfHearts);

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
    }*/

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
