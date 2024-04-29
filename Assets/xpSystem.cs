using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToLevelUp = 100;
    public float xpIncreaseFactor = 1.2f; // Adjust this to increase the XP required per level

    // Function to add XP to the player
    public void AddXP(int xpAmount)
    {
        xpAmount = Random.Range(5, 15); // Random XP between 5 and 15
        currentXP += xpAmount;
        if (currentXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    // Function to handle leveling up
    void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToLevelUp;
        xpToLevelUp = Mathf.RoundToInt(xpToLevelUp * xpIncreaseFactor); // Increase XP required for next level
        // You can add code here to adjust player stats or abilities based on the new level
    }
}
