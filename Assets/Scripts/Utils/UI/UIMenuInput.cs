using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuInput : MonoBehaviour
{
    public Image image;
    public TMP_InputField playerScoreName;

    public void Save()
    {
        if (playerScoreName != null && playerScoreName.text != "")
        {
            ScoreManager.Instance.AddScore(playerScoreName.text,
                GameManager.instance.playerScore);
        }
    }

    public void EnableInteraction(bool value)
    {
        playerScoreName.interactable = value;
    }

    public void SetSprite(Sprite sprite)
    {
        this.image.sprite = sprite;
    }
}
