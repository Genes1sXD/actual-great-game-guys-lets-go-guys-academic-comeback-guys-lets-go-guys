using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float health = 3;

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                ScoreManager.instance.AddPoint();
                RemoveEnemy();
            }
        }
        get { return health; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
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
}

