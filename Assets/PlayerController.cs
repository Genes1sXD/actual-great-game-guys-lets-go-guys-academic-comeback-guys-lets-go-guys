using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.1f;
    private float dashTimeLeft;
    private bool isDashing;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    Vector2 lastDirection;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (isDashing)
            {
                ContinueDash();
            }
            else
            {
                if (movementInput != Vector2.zero)
                {
                    bool success = TryMove(movementInput);

                    if (!success)
                    {
                        success = TryMove(new Vector2(movementInput.x, 0));
                    }

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }

                    animator.SetBool("isMoving", success);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }

                if (movementInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (movementInput.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    private void ContinueDash()
    {
        if (dashTimeLeft > 0)
        {
            rb.MovePosition(rb.position + lastDirection * dashSpeed * Time.fixedDeltaTime);
            dashTimeLeft -= Time.fixedDeltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        if (!isDashing)
        {
            movementInput = movementValue.Get<Vector2>();
            lastDirection = movementInput.normalized;
        }
    }

    public void OnDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
        }
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
    }
}

