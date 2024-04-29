using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPCounter : MonoBehaviour
{
    public Text xpText; // Reference to the UI Text element

    // Reference to the PlayerXP script attached to the player object
    private int playerLevel = 0;
    private int playerXP = 0;
    int[] levelUpXPNeeded = { 5, 10, 20, 35, 55, 80, 110, 145, 185, 210 };

    void Start()
    {
        UpdateXPText();
    }

    void Update()
    {
        UpdateXPText();
    }

    void UpdateXPText()
    {
        RecursiveLvlUp();

        xpText.text = "Level: " + playerLevel + "\n" +
                      "XP: " + playerXP + "/" + levelUpXPNeeded[playerLevel];
    }

    void RecursiveLvlUp()
    {
        if (playerXP - levelUpXPNeeded[playerLevel] >= 0)
        {
            playerXP -= levelUpXPNeeded[playerLevel];
            playerLevel++;
            RecursiveLvlUp();
        }
    }
}