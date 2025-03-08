using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWinScreen : MonoBehaviour
{
    public Sprite normal;
    public Sprite selected;
    public List<Ranking> rankings; // Referência para os textos do ranking na tela
    private List<ScoreEntry> scores;
    private int playerScore;

    private void Start()
    {
        ScoreManager.Instance.SaveInitialScore(playerScore);
        StartCoroutine(DelayedUpdateScores());
    }

    private IEnumerator DelayedUpdateScores()
    {
        yield return new WaitForEndOfFrame(); // Aguarda o frame terminar
        playerScore = GameManager.instance.playerScore;

        // Agora que playerScore está atualizado, salva corretamente
        ScoreManager.Instance.SaveInitialScore(playerScore);

        UpdateScoresOnUi();
    }


    public void UpdateScoresOnUi()
    {
        scores = ScoreManager.Instance.GetScores();

        // 🛑 Evita ativar a edição de uma posição que não precisa ser editada
        int playerIndex = scores.FindIndex(s => string.IsNullOrEmpty(s.playerName) && s.score > 0);

        // 🔄 Atualiza os textos do ranking **antes** de definir interações
        for (int i = 0; i < rankings.Count; i++)
        {
            if (i < scores.Count)
            {
                rankings[i].name.text = scores[i].playerName;
                rankings[i].points.text = scores[i].score.ToString();
            }
            else
            {
                rankings[i].name.text = "";
                rankings[i].points.text = "0";
            }
        }

        // 🚫 Desativa todas as interações antes de ativar a correta
        foreach (var rank in rankings)
        {
            rank.uiMenuInput.EnableInteraction(false);
            rank.uiMenuInput.image.sprite = normal;
        }

        // ✅ Ativa SOMENTE se realmente for necessário editar um nome em branco com pontuação válida
        if (playerIndex != -1 && playerIndex < rankings.Count)
        {
            rankings[playerIndex].uiMenuInput.EnableInteraction(true);
            rankings[playerIndex].uiMenuInput.image.sprite = selected;
            rankings[playerIndex].uiMenuInput.playerScoreName.Select();
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
                    // 🔍 Encontra o score vazio correspondente ao jogador
                    var emptyEntry = scores.Find(s => string.IsNullOrEmpty(s.playerName) && s.score == playerScore);

                    if (emptyEntry != null)
                    {
                        emptyEntry.playerName = playerName; // Atualiza o nome corretamente
                        ScoreManager.Instance.SaveScores(); // Salva no JSON sem reordenar a lista
                    }

                    rank.uiMenuInput.EnableInteraction(false); // Desativa o input após edição
                    rank.uiMenuInput.ResetSprite(); // Reseta o sprite do input field

                    UpdateScoresOnUi(); // Atualiza a interface sem mexer na ordem
                }
                break;
            }
        }
    }


}
