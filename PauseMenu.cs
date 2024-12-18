using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the pause menu in the game. It allows the player to pause, resume, and return to the main menu.

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;   // Reference to the pause menu panel UI

    private bool isPaused = false;      // Tracks whether the game is currently paused

    // Checks for player input to toggle pause and resume states
    void Update()
    {
        // Toggle pause when the Escape or P key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();  // Resume if already paused
            }
            else
            {
                PauseGame();   // Pause if not currently paused
            }
        }
    }

    // Method to pause the game and display the pause menu
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);  // Show the pause menu panel
        Time.timeScale = 0f;             // Freeze game time to pause the game
        isPaused = true;                 // Set paused state to true
    }

    // Method to resume the game from a paused state
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Hide the pause menu panel
        Time.timeScale = 1f;             // Resume game time
        isPaused = false;                // Set paused state to false
    }

    // Method to return to the main menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;             // Ensure game time is normal when returning to the menu
        SceneManager.LoadScene("Menu");  // Load the main menu scene (replace "Menu" with your main menu scene name if different)
    }
}
