using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreCounter : MonoBehaviour
{
    public Text highscoreText; // Reference to the UI Text object
    private int highscore = 0;
    public XPCounter XPCounter;

    void Start()
    {
    }

    void Update()
    {
        // Update the highscore based on the current game state
        UpdateHighscore();

        // Update the text to display the current highscore
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    void UpdateHighscore()
    {
        int currentLevel = XPCounter.currentLevel;
        // Calculate the current score (time survived * level)
        int currentScore = (int)(Time.timeSinceLevelLoad * currentLevel);

        // Update the highscore if the current score is higher
        if (currentScore > highscore)
        {
            highscore = currentScore;

        }
    }
}
