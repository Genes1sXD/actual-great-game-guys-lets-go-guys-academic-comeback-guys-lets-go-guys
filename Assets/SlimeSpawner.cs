using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab; // Assign your Slime prefab in the Unity Inspector
    public Transform playerTransform; // Assign the player's Transform in the Inspector
    public float minSpawnDistance = 15f; // Minimum distance from the player to spawn slimes
    public float maxSpawnDistance = 30f; // Maximum distance from the player to spawn slimes
    public float spawnInterval = 5f; // Time interval between spawns in seconds
    private float spawnTimer;

    // Camera reference, automatically uses the main camera which should be controlled by Cinemachine
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnSlime();
            spawnTimer = 0f; // Reset timer after spawning a slime
        }
    }

    private void SpawnSlime()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        if (spawnPosition != Vector3.zero) // Check if a valid spawn position was found
        {
            Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        // Attempt to find a valid spawn position within a number of tries to avoid infinite loops
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 potentialSpawnPosition = playerTransform.position + randomDirection * randomDistance;

            // Check if the potential spawn position is outside the camera view
            if (!IsInView(potentialSpawnPosition))
            {
                potentialSpawnPosition.z = 0; // Ensure the z-axis value is correct
                return potentialSpawnPosition;
            }
        }
        return Vector3.zero; // Return a zero vector if no valid position is found
    }

    // Helper method to check if a position is within the camera's view
    private bool IsInView(Vector3 position)
    {
        Vector3 viewportPosition = playerCamera.WorldToViewportPoint(position);
        // Check if the viewport position is outside the 0-1 range for both x and y
        return viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;
    }
}

