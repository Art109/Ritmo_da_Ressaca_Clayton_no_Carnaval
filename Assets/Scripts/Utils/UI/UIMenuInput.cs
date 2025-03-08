using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuInput : MonoBehaviour
{
    public Image image;
    public Sprite normal;
    public TMP_InputField playerScoreName;
    public TextMeshProUGUI normalText;

    [System.Obsolete]
    public void Save()
    {
        if (playerScoreName != null && !string.IsNullOrEmpty(playerScoreName.text))
        {
            // 🚀 Em vez de adicionar um novo score, chama SaveScore() da UIWinScreen!
            FindObjectOfType<UIWinScreen>().SaveScore();
        }
    }


    public void EnableInteraction(bool value)
    {
        playerScoreName.interactable = value;
    }

    public void ResetSprite()
    {
        image.sprite = normal;
        normalText.color = Color.white;
    }
}
