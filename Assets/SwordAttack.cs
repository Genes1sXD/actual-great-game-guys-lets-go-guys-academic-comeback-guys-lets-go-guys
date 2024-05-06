using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;

    // Variable to store player's damage
    private float playerDamage = 1;

    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        DealDamage();
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
        DealDamage();
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    void DealDamage()
    {
        // Apply damage to enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, swordCollider.bounds.size.x);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Deal damage to the enemy
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(playerDamage);
                }
            }
        }
    }

    // Method to increase player's damage
    public void IncreaseDamage()
    {
        // Increase player's damage by 1
        playerDamage += 1;
    }
}
