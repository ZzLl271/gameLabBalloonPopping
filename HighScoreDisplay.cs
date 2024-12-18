using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text highScoreText; // Reference to the Text component to display high scores

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        // Load the high scores
        List<HighScoreEntry> highScores = HighScoreManager.LoadHighScores();

        // Update the Text component
        highScoreText.text = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreText.text += $"{i + 1}. {highScores[i].playerName}: {highScores[i].score}\n";
        }
    }
}
