using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuInput : MonoBehaviour
{
    public TMP_InputField playerScoreName;

    public void Save()
    {
        if (playerScoreName != null && playerScoreName.text != "")
        {
            ScoreManager.Instance.AddScore(playerScoreName.text,
                GameManager.instance.playerScore);
        }
    }
}
