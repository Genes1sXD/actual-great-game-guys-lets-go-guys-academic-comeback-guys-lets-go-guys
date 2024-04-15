using System.Collections;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 2f;
    public float patrolRadius = 5f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public LayerMask obstacleLayer;
    public float detectionRadius = 10f;  // Detection radius for player
    public float stopChaseDistance = 1f; // Minimum distance to stop moving towards the player
    public Transform player;             // Player's transform

    private Rigidbody2D rb;
    private Vector2 nextPatrolPoint;
    private bool isWaiting = false;
    private int retryCount = 0;
    private const int maxRetries = 5;
    private Animator animator; // Reference to the Animator component
    private bool isChasing = false; // Is the slime currently chasing the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Update isChasing based on the distance to the player
        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > stopChaseDistance)
            {
                isChasing = true;
            }
            else
            {
                // Close enough to "catch" the player or stop movement
                isChasing = false;
                // Additional behavior on catching the player could go here
            }
        }
        else
        {
            // Player is outside the detection radius
            isChasing = false;
        }

        if (!isWaiting)
        {
            if (isChasing)
            {
                MoveTowardsPlayer();
            }
            else
            {
                MoveTowardsNextPatrolPoint();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        animator.SetBool("isMoving", true);
    }

    private void MoveTowardsNextPatrolPoint()
    {
        if (Vector2.Distance(transform.position, nextPatrolPoint) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPatrolPoint, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            isWaiting = true;
            StartCoroutine(WaitAndSetNextPatrolPoint());
        }
    }

    private void SetNextPatrolPoint()
    {
        // Existing logic to set the next patrol point
    }

    IEnumerator WaitAndSetNextPatrolPoint()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        SetNextPatrolPoint();
    }

    // Handle collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = false; // Stop chasing when colliding with the player
            // Optionally add logic to move away from player or perform an action
        }
    }
}

