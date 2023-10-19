using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Health : MonoBehaviour
{
    public int health; // Health
    public int numOfHearts; // Max number of hearts

    public GameObject[] hearts; // GameObject for the hearts in the topleft
    public Sprite fullHeart;
    [SerializeField]
    private bool isInvincible = false; // Player's invincibility status

    [SerializeField]
    private float cooldownDuration = 2.0f; // Cooldown duration in seconds (for invincibility)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isInvincible) // Checks if the player is invincible, if it's not, it takes damage and becomes invincible for 2 seconds
            {
                Debug.Log("Player collided with an enemy!");
                health--;
                isInvincible = true;
                Invoke("DisableInvincibility", cooldownDuration);
            }
        }
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
    }

    void Update()
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
    }
}
