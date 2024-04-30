using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public XPManager xpManager; // Reference to the XPManager script
    Vector2 rightAttackOffset;

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
        float damage = xpManager.GetCurrentDamage();

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
                    enemyScript.TakeDamage(damage);
                }
            }
        }
    }
}
