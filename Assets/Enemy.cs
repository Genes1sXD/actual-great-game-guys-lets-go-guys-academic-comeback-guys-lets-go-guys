using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float health = 3;
    private GameObject player;
    public int enemyXP = 1;
    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                ScoreManager.instance.AddPoint();
                Die();
                RemoveEnemy();
            }
        }
        get { return health; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    void Die()
    {
        player.GetComponent<XPCounter>().AddXP(enemyXP);

    }
}

