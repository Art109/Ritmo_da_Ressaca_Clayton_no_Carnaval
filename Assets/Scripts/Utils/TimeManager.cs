using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    private float timeRemaining = 600f; // 10 minutos em segundos
    private bool isRunning = true;

    public static TimeManager Instance;

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else
            Destroy(gameObject);
    }

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
            // Game Over
        }
    }

    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        _timeText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void ApplyTimeBonus()
    {
        int bonus = Mathf.FloorToInt(timeRemaining * 0.2f); 
        Player.instance.PlayerScore += bonus;
    }
}
