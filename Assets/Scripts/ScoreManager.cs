using System.IO;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class Scoreboard
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public class ScoreManager : MonoBehaviour
{
    private string savePath;
    private Scoreboard scoreboard = new Scoreboard();
    private const int maxEntries = 3; // Define o máximo de registros no ranking

    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        savePath = Application.persistentDataPath + "/ranking.json";
        
        LoadScores();
    }

    public void AddScore(string playerName, int score)
    {
        Debug.Log("Antes de salvar");
        scoreboard.scores.Add(new ScoreEntry { playerName = playerName, score = score });
        scoreboard.scores.Sort((a, b) => b.score.CompareTo(a.score)); // Ordena do maior para o menor
        Debug.Log("Depois de salvar");
        if (scoreboard.scores.Count > maxEntries)
        {
            scoreboard.scores.RemoveAt(scoreboard.scores.Count - 1); // Mantém o máximo permitido
        }

        SaveScores();
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreboard);
        Debug.Log(json);
        File.WriteAllText(savePath, json);
        Debug.Log("Salvei!");
    }

    private void LoadScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            scoreboard = JsonUtility.FromJson<Scoreboard>(json);
        }
    }

    public List<ScoreEntry> GetScores()
    {
        return scoreboard.scores;
    }
}
