using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPCounter : MonoBehaviour
{

    public levelupcard levelupcard;
    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToLevelUp = 10;
    public float xpIncreaseFactor = 1.2f; // Adjust this to increase the XP required per level
    public int enemyXP = 1;
    public void AddXP(int xpAmount)
    {
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
        xpToLevelUp = Mathf.RoundToInt(xpToLevelUp * xpIncreaseFactor);
        if (levelupcard != null)
        {
            levelupcard.LevelUp2();
        }
        // Increase XP required for next level
        // You can add code here to adjust player stats or abilities based on the new level
    }
}

