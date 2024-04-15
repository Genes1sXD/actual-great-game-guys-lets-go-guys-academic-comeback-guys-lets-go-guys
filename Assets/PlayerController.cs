using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1.0f;  // Cooldown duration in seconds
    private float dashTimeLeft;
    private float lastDashTime = -10f;  // Initialize with a value that allows dashing immediately at game start
    private bool isDashing;  // Boolean to check if the player is currently dashing

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack; // Make sure SwordAttack is correctly implemented and accessible

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

    void Update()
    {
        if (!isDashing)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) // Assume KeyCode.F is for firing/attacking
        {
            OnFire();
        }

        UpdateAnimationAndDirection();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (isDashing)
            {
                ContinueDash();
            }
            else if (movementInput != Vector2.zero)
            {
                MoveCharacter();
            }
        }
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
        SwordAttack();
    }

    public void SwordAttack()
    {
        // Depending on how your SwordAttack class is set up, you may call a method here
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    private void StartDash()
    {
        if (!isDashing && movementInput != Vector2.zero)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            lastDirection = movementInput.normalized;
            lastDashTime = Time.time;
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

    private void MoveCharacter()
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

    private void UpdateAnimationAndDirection()
    {
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction)
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

    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
    }
}


