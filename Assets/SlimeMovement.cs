using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float maxDistance = 100f;
    public float minVolume = 0f;
    public float maxVolume = 1f;
    public float speed = 2f;
    public float detectionRadius = 10f; // Detection radius for player
    public float stopChaseDistance = 1f; // Minimum distance to stop moving towards the player
    public Transform player; // Player's transform, to be set by the spawner
    public Transform cameraTransform;

    private Animator animator; // Reference to the Animator component
    private bool isChasing = false; // Is the slime currently chasing the player

    public AudioSource audioSource;
    public AudioClip jumpSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        FindCamera();
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

        float distanceToCamera = Vector3.Distance(transform.position, cameraTransform.position);
        float clampedDistance = Mathf.Clamp(distanceToCamera, 0f, maxDistance);
        float t = clampedDistance / maxDistance;

        float volume = Mathf.Lerp(maxVolume, minVolume, t);
        volume = Mathf.Clamp01(volume);

        audioSource.volume = volume;
    }

    private void FindCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraTransform = mainCamera.transform;
        }
    }
    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        animator.SetBool("isMoving", isChasing);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}



