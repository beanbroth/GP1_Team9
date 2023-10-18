using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Health : MonoBehaviour
{
    public int health; // Health
    public int numOfHearts; // Max number of hearts

    public GameObject[] hearts;
    public Sprite fullHeart;

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
