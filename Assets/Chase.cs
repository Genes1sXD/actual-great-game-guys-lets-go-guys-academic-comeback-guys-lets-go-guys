using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;       // Player's transform
    public float speed = 5.0f;     // Speed at which the enemy moves

    void Update()
    {
        // Move the enemy towards the player each frame
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}

