using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNameManager : MonoBehaviour
{
    public GameObject playerNameTextPrefab;
    private static PlayerNameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayerNameText();
    }

    private void SpawnPlayerNameText()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null && playerNameTextPrefab != null)
        {
            GameObject playerNameUI = Instantiate(playerNameTextPrefab, canvas.transform);
            RectTransform rectTransform = playerNameUI.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-256, 163);

            Text playerNameText = playerNameUI.GetComponent<Text>();
            string playerName = PlayerPrefs.GetString("PlayerName", "Player");
            playerNameText.text = "Player: " + playerName;
        }
    }
}