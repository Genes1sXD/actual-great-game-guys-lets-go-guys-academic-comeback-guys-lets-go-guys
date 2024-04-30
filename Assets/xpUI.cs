using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xpUI : MonoBehaviour
{
    public Text xpText; // Reference to the UI Text element

    // Reference to the PlayerXP script attached to the player object
    private XPCounter XPCounter;

    void Start()
    {
        // Find the player object and get its PlayerXP component
        XPCounter = GameObject.FindWithTag("Player").GetComponent<XPCounter>();

        // Update the XP text initially
        UpdateXPText();
    }

    void Update()
    {
        // Update the XP text every frame (optional)
        UpdateXPText();
    }

    void UpdateXPText()
    {
        // Calculate the XP needed to level up
        int xpNeeded = XPCounter.xpToLevelUp - XPCounter.currentXP;

        // Update the UI Text to display level and XP information
        xpText.text = "Level: " + XPCounter.currentLevel + "\n" +
                      "XP: " + XPCounter.currentXP + " / " + XPCounter.xpToLevelUp + "\n" +
                      "XP to Level Up: " + xpNeeded;
    }
}
