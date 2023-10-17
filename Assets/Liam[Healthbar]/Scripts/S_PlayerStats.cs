using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;

    public S_HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) 
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
    }

    private void Update() // Using "K" as a debugger for damage
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20f); // Main way of taking damage
        }
    }
}
