using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3;
    public Vector2 rightAttackOffset = new Vector2(1f, 0); // Adjust this in the Inspector

    private void Awake()
    {
        // Disable the collider at start to prevent the sword from attacking immediately
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        // Consider using localPosition for offset to be relative to the player
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        // Flip the attack offset for the left attack
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Assuming the Enemy script has a method to handle taking damage
                enemy.TakeDamage(damage);
            }
        }
    }
}
