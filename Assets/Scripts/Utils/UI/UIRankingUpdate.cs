using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UIRankingUpdate : MonoBehaviour
{
    public List<Ranking> rakings;

    List<ScoreEntry> scores;

    public bool winScreen = false;

    private void Start()
    {
        if (!winScreen) 
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
            ++index;
        }

        if (scores.Count == 0)
        {
            for (int i = 0; i < rakings.Count; ++i)
            {
                rakings[i].name.text = "";
                rakings[i].points.text = "0";
            }
        }
    }
}

[System.Serializable]
public class Ranking
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI points;
    public UIMenuInput uiMenuInput; 
}
