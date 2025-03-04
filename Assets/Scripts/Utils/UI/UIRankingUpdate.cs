using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRankingUpdate : MonoBehaviour
{
    public List<Ranking> rakings;

    List<ScoreEntry> scores;


    private void Start()
    {
        UpdateScoresOnUi();
    }

    public void UpdateScoresOnUi()
    {
        scores = ScoreManager.Instance.GetScores();

        int index = 0;

        foreach (ScoreEntry entry in scores)
        {
            if (entry != null)
            {
                rakings[index].name.text = entry.playerName;
                rakings[index].points.text = entry.score.ToString();
            }
            else
            {
                rakings[index].name.text = "";
                rakings[index].points.text = "0";
            }
            ++index;
        }
    }
}

[System.Serializable]
public class Ranking
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI points;
}
