using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public Transform playerTransform;
    public float minSpawnDistance = 15f;
    public float maxSpawnDistance = 30f;
    public float spawnInterval = 5f;
    private float spawnTimer;

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
            spawnTimer = 0f;
        }
    }

    private void SpawnSlime()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            GameObject newSlime = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
            newSlime.GetComponent<SlimeMovement>().player = playerTransform;  // Assign the player's transform
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 potentialSpawnPosition = playerTransform.position + randomDirection * randomDistance;

            if (!IsInView(potentialSpawnPosition))
            {
                potentialSpawnPosition.z = 0;
                return potentialSpawnPosition;
            }
        }
        return Vector3.zero;
    }

    private bool IsInView(Vector3 position)
    {
        Vector3 viewportPosition = playerCamera.WorldToViewportPoint(position);
        return viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;
    }
}


