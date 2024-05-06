using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveluplogic : MonoBehaviour
{
   
    public int maxHP = 100; // Change this value to your desired max HP
    public PlayerHealth playerHealth; // Reference to the player's health component

    void Start()
    {
        // Find and store a reference to the PlayerHealth component
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void OnButtonClick()
    {
        // Set the player's HP to the maximum value
        if (playerHealth != null)
        {
            playerHealth.SetHealth(maxHP);
        }
    }
}

