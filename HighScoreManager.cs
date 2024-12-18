using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScoreManager
{
    private const string HighScoresKey = "HighScores";

    public static void SaveHighScore(string playerName, int score)
    {
        List<HighScoreEntry> highScores = LoadHighScores();
        highScores.Add(new HighScoreEntry(playerName, score));

        // Sort scores and keep only the top 5
        highScores.Sort((a, b) => b.score.CompareTo(a.score));
        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }

        string json = JsonUtility.ToJson(new HighScoreList(highScores));
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
    }

    public static List<HighScoreEntry> LoadHighScores()
    {
        string json = PlayerPrefs.GetString(HighScoresKey, "{}");
        HighScoreList highScoreList = JsonUtility.FromJson<HighScoreList>(json);
        return highScoreList != null ? highScoreList.entries : new List<HighScoreEntry>();
    }
}

[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;

    public HighScoreEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}

[System.Serializable]
public class HighScoreList
{
    public List<HighScoreEntry> entries;

    public HighScoreList(List<HighScoreEntry> entries)
    {
        this.entries = entries;
    }
}
