using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelupcard : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button[] upgradeButtons;

    // Call this method when the player levels up
    public void LevelUp2()
    {
        Time.timeScale = 0; // Pause the game
        upgradePanel.SetActive(true); // Show upgrade panel

        // Enable upgrade buttons
        foreach (Button button in upgradeButtons)
        {
            button.interactable = true;
        }
    }

    // Call this method when the player selects an upgrade
    public void SelectUpgrade(int upgradeIndex)
    {
        // Apply upgrade based on selected index
        Debug.Log("Selected upgrade: " + upgradeIndex);

        // Disable all buttons after selection
        foreach (Button button in upgradeButtons)
        {
            button.interactable = false;
        }

        upgradePanel.SetActive(false); // Hide upgrade panel
        Time.timeScale = 1; // Resume game
    }
}



