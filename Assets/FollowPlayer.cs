using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollowPlayer : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player != null)
        {
            // Set the position of the ground to match the player's position
            transform.position = player.transform.position;
        }
    }
}
