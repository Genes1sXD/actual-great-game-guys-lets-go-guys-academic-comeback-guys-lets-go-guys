using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public AudioSource audioSource;
    public float loopDuration = 3f;
    float audioLoopTimer;
    public AudioClip walkingSound;

    Vector2 movementInput;
    Vector2 lastDirection;
    Vector2 zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            audioSource.Stop();
        }
        rb = GetComponent<Rigidbody2D>();

        if (!isDashing)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (movementInput != Vector2.zero)
            {
                UpdateAnimationAndDirection(); // Update the direction and animation state
            }

            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }
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

                if (!audioSource.isPlaying && Time.timeScale == 1f)
                {
                    audioSource.PlayOneShot(walkingSound);
                }

            }
            else if (movementInput == Vector2.zero)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
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
            animator.SetBool("isDashing", true);  // Set the dashing animation state
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
            animator.SetBool("isDashing", false);  // Reset the dashing animation state
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

        // Ensure the movement animation state is correctly set based on movement success
        animator.SetBool("isMoving", movementInput != Vector2.zero);
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
    public void IncreaseSpeed()
    {
        moveSpeed += 0.1f; // Increase speed by 0.5
    }
}



