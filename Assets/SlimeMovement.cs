using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRadius = 10f; // Detection radius for player
    public float stopChaseDistance = 1f; // Minimum distance to stop moving towards the player
    public Transform player; // Player's transform, to be set by the spawner

    private Animator animator; // Reference to the Animator component
    private bool isChasing = false; // Is the slime currently chasing the player

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not set on " + gameObject.name);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius && distanceToPlayer > stopChaseDistance)
        {
            isChasing = true;
            MoveTowardsPlayer();
        }
        else
        {
            isChasing = false;
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        animator.SetBool("isMoving", isChasing);
    }
}



