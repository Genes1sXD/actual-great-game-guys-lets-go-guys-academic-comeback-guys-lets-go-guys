using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Text healthText;
    public GameObject playerPrefab;
    private PlayerHealth healthController;

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab not assigned to HealthDisplay script!");
            return;
        }

        // Find the player's HealthController script within the prefab
        healthController = playerPrefab.GetComponentInChildren<PlayerHealth>();
        if (healthController == null)
        {
            Debug.LogError("HealthController script not found on player prefab!");
            return;
        }

        // Ensure that the healthText reference is assigned
        if (healthText == null)
        {
            Debug.LogError("HealthText reference not assigned to HealthDisplay script!");
            return;
        }
    }

    void Update()
    {
        if (healthController != null && healthText != null)
        {
            UpdateHealthText();
        }
    }

    void UpdateHealthText()
    {
        int currentHealth = healthController.currentHealth;
        int maxHealth = healthController.maxHealth;
        healthText.text = "HP: " + currentHealth;
    }
}
