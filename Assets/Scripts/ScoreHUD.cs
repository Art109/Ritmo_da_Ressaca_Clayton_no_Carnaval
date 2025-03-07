using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;

    public float animationDuration = 3f;

    private int lastDisplayedPoints = -1; // Armazena o último valor mostrado

    private void Start()
    {
        UpdateHUD();

    }

    void UpdateHUD()
    {
        float score = GameManager.instance.playerScore;

        StartCoroutine(AnimatePoints(score));
    }

    IEnumerator AnimatePoints(float score)
    {
        float currentPoints = 0;

        DOTween.To(() => currentPoints, x =>
        {
            currentPoints = x;
            int roundedPoints = Mathf.FloorToInt(currentPoints);

            // Só atualiza o texto e chama a animação se o número realmente mudou
            if (roundedPoints != lastDisplayedPoints)
            {
                playerScore.text = roundedPoints.ToString();
                AnimateScale();
                lastDisplayedPoints = roundedPoints; // Atualiza o último valor mostrado
            }
        }, score, animationDuration)
        .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(2f);
    }

    void AnimateScale()
    {
        playerScore.transform.DOScale(1.3f, 0.1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => playerScore.transform.DOScale(1f, 0.1f));
    }
}
