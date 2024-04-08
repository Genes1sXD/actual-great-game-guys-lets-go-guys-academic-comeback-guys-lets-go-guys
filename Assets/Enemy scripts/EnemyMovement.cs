using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 2f;
    public float patrolRadius = 5f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public LayerMask obstacleLayer;

    private Rigidbody2D rb;
    private Vector2 nextPatrolPoint;
    private bool isWaiting = false;
    private int retryCount = 0;
    private const int maxRetries = 5;
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Initialize the Animator reference
        SetNextPatrolPoint();
    }

    void Update()
    {
        if (!isWaiting)
        {
            // Check if the slime is currently moving towards the next patrol point
            MoveTowardsNextPatrolPoint();
            if (Vector2.Distance(transform.position, nextPatrolPoint) > 0.1f)
            {
                // If the slime is moving, ensure the Animator knows it's moving
                animator.SetBool("isMoving", true);
            }
            else
            {
                // Reached the patrol point, no longer moving
                animator.SetBool("isMoving", false);
                isWaiting = true; // Start waiting
                StartCoroutine(WaitAndSetNextPatrolPoint());
            }
        }
    }

    private void SetNextPatrolPoint()
    {
        Vector2 patrolPointCandidate = (Vector2)transform.position + Random.insideUnitCircle * patrolRadius;
        Vector2 rayStart = (Vector2)transform.position + (patrolPointCandidate - (Vector2)transform.position).normalized * 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, patrolPointCandidate - rayStart, patrolRadius, obstacleLayer);
        Debug.DrawLine(rayStart, patrolPointCandidate, hit.collider == null ? Color.green : Color.red, 2f);

        if (hit.collider != null)
        {
            StartCoroutine(WaitAndTryAgain());
            return;
        }

        nextPatrolPoint = patrolPointCandidate;
        isWaiting = false; // Allow movement towards the next patrol point
        retryCount = 0;
    }

    IEnumerator WaitAndTryAgain()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime) / 2);
        if (retryCount <= maxRetries)
        {
            isWaiting = false;
            SetNextPatrolPoint();
        }
        else
        {
            Debug.Log("Fallback behavior initiated.");
            FallbackMovement();
            retryCount = 0;
        }
    }

    private void FallbackMovement()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * speed;
        StartCoroutine(ResetFromFallback());
    }

    IEnumerator ResetFromFallback()
    {
        yield return new WaitForSeconds(2);
        rb.velocity = Vector2.zero;
        isWaiting = false;
        SetNextPatrolPoint();
    }

    IEnumerator WaitAndSetNextPatrolPoint()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        SetNextPatrolPoint();
    }

    private void MoveTowardsNextPatrolPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPatrolPoint, speed * Time.deltaTime);
    }
}
