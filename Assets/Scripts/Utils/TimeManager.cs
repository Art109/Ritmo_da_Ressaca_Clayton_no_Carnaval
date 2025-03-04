using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    private float timeRemaining = 600f; // 10 minutos em segundos
    private bool isRunning = true;

    void Start()
    {
        UpdateTimeUI();
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (isRunning && timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            UpdateTimeUI();
        }

        if (timeRemaining <= 0)
        {
            isRunning = false;
            Debug.Log("Tempo acabou!");
        }
    }

    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        _timeText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
