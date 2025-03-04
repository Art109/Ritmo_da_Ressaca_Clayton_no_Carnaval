using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] Image[] starts;


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

        StartCoroutine(FillScore(score));
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

    IEnumerator FillScore(float score) 
    {
        for (int i = 0; i <= score; i++) 
        {
            playerScore.text = i.ToString();
            i++;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
