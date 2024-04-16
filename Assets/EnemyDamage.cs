using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public float attackCooldown = 2f; // Time between attacks in seconds
    public int damage = 10;           // Damage dealt per attack
    public float attackRange = 3f;    // Attack range, editable from the Inspector
    public float requiredTimeInRange = 2f; // Time the player must stay in range before damage is dealt

    private float lastAttackTime = 0f; // Time when the last attack occurred
    private float timePlayerInRange = 0f; // Timer to track how long the player has been in range
    private Transform player;          // Reference to the player's transform
    private PlayerHealth playerHealth; // Reference to the player's health script

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth component not found on player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found. Ensure the player is tagged correctly.");
        }
    }

    void Update()
    {
        if (player == null) return;

        bool playerIsInRange = IsPlayerInAttackRange();

        if (playerIsInRange)
        {
            timePlayerInRange += Time.deltaTime;
        }
        else
        {
            timePlayerInRange = 0f; // Reset the timer if the player leaves the range
        }

        // Check if it's time to attack again, if the player is in range and has been in range long enough
        if (Time.time > lastAttackTime + attackCooldown && playerIsInRange && timePlayerInRange >= requiredTimeInRange)
        {
            Attack();
        }
    }

    private bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    private void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            lastAttackTime = Time.time; // Reset the last attack time
            timePlayerInRange = 0f; // Reset the in-range timer after an attack
            Debug.Log("Attacking player at: " + Time.time);
        }
        else
        {
            Debug.LogError("PlayerHealth not set. Cannot attack.");
        }
    }
}




