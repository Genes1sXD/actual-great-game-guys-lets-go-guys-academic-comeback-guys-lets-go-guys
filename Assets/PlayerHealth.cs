using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvulnerable = false;

    public event System.Action<int> OnHealthChanged;
    public event System.Action OnPlayerDied;

    void Start()
    {
        currentHealth = maxHealth; // Set current health to maximum at the start
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth <= 0) return; // Do not heal if player is already dead

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        OnPlayerDied?.Invoke();
        Debug.Log("Player Died!");
        // Disable player controls or trigger death animation
        // Consider disabling the script or the component that handles player input
    }

    // Example method to toggle invulnerability, useful for power-ups or special game mechanics
    public void SetInvulnerability(bool isInvul)
    {
        isInvulnerable = isInvul;
    }
}
