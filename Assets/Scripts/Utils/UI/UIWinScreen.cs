using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWinScreen : MonoBehaviour
{
    public List<Ranking> rankings; // Referência para os textos do ranking na tela
    private List<ScoreEntry> scores;
    private int playerScore;

    private void Start()
    {
        playerScore = GameManager.instance.playerScore;
        UpdateScoresOnUi();
    }

    public void UpdateScoresOnUi()
    {
        scores = ScoreManager.Instance.GetScores();
        int index = 0;
        int playerIndex = -1; // Índice onde o jogador entraria no ranking

        foreach (ScoreEntry entry in scores)
        {
            if (entry != null)
            {
                rankings[index].name.text = entry.playerName;
                rankings[index].points.text = entry.score.ToString();

                // Encontra a posição correta para o novo score
                if (playerScore > entry.score && playerIndex == -1)
                {
                    playerIndex = index;
                }
            }
            ++index;
        }

        if (scores.Count == 0)
        {
            for (int i = 0; i < rankings.Count; ++i)
            {
                rankings[i].name.text = "";
                rankings[i].points.text = "0";
            }
        }

        // Verifica se o jogador pode entrar no ranking
        bool canEnterRanking = scores.Count < 5 || playerScore > scores[scores.Count - 1].score;

        // Desativa todos os inputs antes de ativar o correto
        foreach (var rank in rankings)
        {
            rank.uiMenuInput.EnableInteraction(false);
        }

        if (canEnterRanking)
        {
            if (playerIndex == -1)
            {
                // Se o score for o menor da lista, adiciona no final
                playerIndex = scores.Count < 5 ? scores.Count : scores.Count - 1;
            }

            // Ativa o input field apenas na posição correta
            rankings[playerIndex].uiMenuInput.EnableInteraction(true);
        }
    }

    public void SaveScore()
    {
        foreach (var rank in rankings)
        {
            if (rank.uiMenuInput.playerScoreName.interactable)
            {
                string playerName = rank.uiMenuInput.playerScoreName.text;

                if (!string.IsNullOrEmpty(playerName))
                {
                    // Salva o score e desativa o input
                    ScoreManager.Instance.AddScore(playerName, playerScore);
                    UpdateScoresOnUi();
                    rank.uiMenuInput.EnableInteraction(false);
                }
                break;
            }
        }
    }
}
