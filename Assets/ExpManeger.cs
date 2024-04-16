using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public int currentXP = 0;
    public int xpPerEnemy = 10; // You can adjust this value
    public int currentLevel = 1;

    // Damage and speed variables
    public float baseDamage = 1.5f;
    public float damageIncreasePerLevel = 2f;

    // Method to add XP and check for level up
    public void AddXP(int amount)
    {
        currentXP += amount;
        CheckLevelUp();
    }

    // Method to check if the player leveled up
    void CheckLevelUp()
    {
        int requiredXP = Mathf.CeilToInt(100 * Mathf.Pow(1.1f, currentLevel - 1)); // Assuming initial XP requirement
        if (currentXP >= requiredXP)
        {
            currentLevel++;
            Debug.Log("Level Up! Choose to increase damage or speed.");
            currentXP = 0; // Reset XP after leveling up
        }
    }

    // Method to get current damage based on player level
    public float GetCurrentDamage()
    {
        return baseDamage + (damageIncreasePerLevel * (currentLevel - 1));
    }
}
