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
    private const int maxEntries = 5;

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
        // 🔍 Verifica se existe uma entrada vazia para esse score
        ScoreEntry existingEntry = scoreboard.scores.Find(s => s.score == score && string.IsNullOrEmpty(s.playerName));

        if (existingEntry != null)
        {
            // ✅ Atualiza a entrada vazia em vez de adicionar uma nova
            existingEntry.playerName = playerName;
        }
        else
        {
            // 🔄 Evita adicionar entradas com playerName vazio ou score 0
            if (!string.IsNullOrEmpty(playerName) && score > 0)
            {
                scoreboard.scores.Add(new ScoreEntry { playerName = playerName, score = score });
            }
        }

        // Reorganiza a lista
        scoreboard.scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Mantém no máximo 5 registros
        while (scoreboard.scores.Count > maxEntries)
        {
            scoreboard.scores.RemoveAt(scoreboard.scores.Count - 1);
        }

        SaveScores();
    }



    public void SaveInitialScore(int score)
    {
        // 🔍 Garante que a lista não seja nula
        if (scoreboard.scores == null)
        {
            scoreboard.scores = new List<ScoreEntry>();
        }

        // 🛑 Não adiciona um novo score se ele já existir
        if (!scoreboard.scores.Exists(s => s.score == score && string.IsNullOrEmpty(s.playerName)))
        {
            scoreboard.scores.Add(new ScoreEntry { playerName = "", score = score });

            // Ordena corretamente
            scoreboard.scores.Sort((a, b) => b.score.CompareTo(a.score));

            // 🔄 Remove entradas extras caso exceda o máximo
            while (scoreboard.scores.Count > maxEntries)
            {
                scoreboard.scores.RemoveAt(scoreboard.scores.Count - 1);
            }

            SaveScores();
        }
    }

    public void SaveScores()
    {
        // 🔄 Remove qualquer entrada vazia antes de salvar
        scoreboard.scores.RemoveAll(s => string.IsNullOrEmpty(s.playerName) && s.score == 0);

        string json = JsonUtility.ToJson(scoreboard);
        File.WriteAllText(savePath, json);
    }


    private void LoadScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            if (!string.IsNullOrWhiteSpace(json)) // ✅ Verifica se o arquivo não está vazio
            {
                scoreboard = JsonUtility.FromJson<Scoreboard>(json);
            }
        }

        // 🔄 Garante que scoreboard nunca seja nulo
        if (scoreboard == null)
        {
            scoreboard = new Scoreboard();
        }

        // 🔄 Garante que a lista nunca seja nula
        if (scoreboard.scores == null)
        {
            scoreboard.scores = new List<ScoreEntry>();
        }
    }





    public List<ScoreEntry> GetScores()
    {
        return scoreboard.scores;
    }
}
