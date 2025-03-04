using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] Image[] starts;

    public float animationDuration = 3f;

    private int lastDisplayedPoints = -1; // Armazena o último valor mostrado


    private void Start()
    {
        UpdateHUD();

    }

    void UpdateHUD()
    {
        float score = GameManager.instance.playerScore;
        int starsNumber = 0;

        if (score >= 10 && score <= 20)
        {
            starsNumber = 1;

        }
        else if (score > 20 && score <= 30)
        {
            starsNumber = 2;
        }
        else if (score > 30)
        {
            starsNumber = 3;
        }

        AnimatePoints(score);
        StartCoroutine(FillStars(starsNumber));
    }

    IEnumerator FillStars(int numberStars)
    {
        for (int i = 0; i < numberStars; i++) 
        {
            float j = 0;
            while( j <= 1)
            {
                starts[i].fillAmount += j;
                j += 0.1f;
                yield return new WaitForSeconds(0.09f);
            }
            
            
        }
        
    }

    void AnimatePoints(float score)
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
    }

    void AnimateScale()
    {
        playerScore.transform.DOScale(1.3f, 0.1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => playerScore.transform.DOScale(1f, 0.1f));
    }
}
