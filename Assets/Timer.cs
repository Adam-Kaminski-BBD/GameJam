using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timerLength = 30.0f; // Length of the timer in seconds
    private float currentTime = 0.0f;
    private bool isTimerRunning = false;
    public Text timeText;

    private void Start()
    {
        StartTimer();
        currentTime = timerLength;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            DisplayTime(currentTime);
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                isTimerRunning = false;
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        currentTime = timerLength;
        isTimerRunning = false;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeText.text = string.Format("{0}: {1:00}","LOOP IN", timeToDisplay);
    }
}


