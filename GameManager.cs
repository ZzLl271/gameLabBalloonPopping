using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script manages the game's main functions, including tracking score, handling level transitions,
// managing balloons, and displaying a victory image when all levels are completed.

public class GameManager : MonoBehaviour
{
    public Text scoreText;               // Reference to the UI Text for displaying the score
    private int score = 0;               // Current total score

    private int cumulativeScore = 0;   
    private int activeBalloons = 0;      // Tracks the number of active balloons
    private bool levelReset = false;     // Tracks if the level has been reset

    public int baseScore = 50;           // Starting score for the smallest balloon
    public int minScore = 1;             // Minimum score for larger balloons
    public int maxGrowthStages = 10;     // Maximum growth stages for the balloon

    public int scoreThreshold = 100;     // Score required to advance to the next level
    public GameObject victoryImage;      // Reference to the victory image displayed upon game completion

    // Initializes the game level by resetting score and balloon counts
    void Start()
    {
        // Load the cumulative score from PlayerPrefs
        cumulativeScore = PlayerPrefs.GetInt("CumulativeScore", 0);
        UpdateScoreText();
        ResetLevelState(); 
    }

    // Resets the level state, including score, balloon count, and text updates
    private void ResetLevelState()
    {
        score = 0;
        levelReset = false;
        activeBalloons = 0;
        UpdateScoreText();
    }

    // Adds score based on the balloon's growth stage and updates the score text
    public void AddScore(int growthStage)
    {
        int points = baseScore - ((baseScore - minScore) * growthStage / maxGrowthStages);
        points = Mathf.Max(points, minScore);

        score += points;
        UpdateScoreText();
        
        // Check if the score threshold has been met to load the next level or show victory
        if (score >= scoreThreshold)
        {
            CompleteLevel();
        }
    }

    // Updates the score displayed on the UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + (cumulativeScore + score);
    }

    // Loads the next level if available; otherwise, displays the victory screen
    private void LoadNextLevelOrShowVictory()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Load the next scene if it exists; otherwise, show the victory screen
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            ShowVictory();
        }
    }

        private void CompleteLevel()
    {
        // Save cumulative score
        cumulativeScore += score;
        PlayerPrefs.SetInt("CumulativeScore", cumulativeScore);
        PlayerPrefs.Save();

        // Load next level or show victory
        LoadNextLevelOrShowVictory();
    }

    

    // Shows the victory image and starts the delay for returning to the main menu
    private void ShowVictory()
    {
        Time.timeScale = 1f;  // Ensure the game time scale is normal
        if (victoryImage != null)
        {
            victoryImage.SetActive(true);  // Display the victory image
        }
        Debug.Log("Congratulations! You've completed all levels.");
        
        // Save the high score at the end of the game
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        int finalScore = PlayerPrefs.GetInt("CumulativeScore", 0);
        HighScoreManager.SaveHighScore(playerName, finalScore);
        // Start a coroutine to load the main menu after a delay
        StartCoroutine(LoadMainMenuAfterDelay(10f));
    }
    
    // Waits for a specified delay before loading the main menu
    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay
        SceneManager.LoadScene("Menu");  // Load the main menu scene (replace "Menu" with your actual main menu scene name)
    }

    // Resets the current level when all balloons are gone and the score threshold is not met
    private void ResetLevel()
    {
        levelReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called when a balloon is destroyed, decreasing the active balloon count and checking if a reset is needed
    public void BalloonDestroyed()
    {
        activeBalloons--;

        // Reset the level if no balloons are left, the threshold is not met, and the level hasn't been reset
        if (activeBalloons <= 0 && score < scoreThreshold && !levelReset)
        {
            ResetLevel();
        }
    }

    // Called when a new balloon is added to increase the active balloon count
    public void AddBalloon()
    {
        activeBalloons++;
    }
}
