using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class LevelCountdownTimer : MonoBehaviour
{
    [SerializeField] private float countdownTime = 30f;
    [SerializeField] private TextMeshProUGUI countdownText;

    private float timeLeft;
    private bool timerActive = false;

    void Start()
    {
        timeLeft = countdownTime;
        timerActive = true;
    }

    void Update()
    {
        if (!timerActive) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            timerActive = false;
        }

        int secondsLeft = Mathf.CeilToInt(timeLeft);
        countdownText.text = $"Zombies will spawn in: {secondsLeft} second{(secondsLeft != 1 ? "s" : "")}";
    }
}
