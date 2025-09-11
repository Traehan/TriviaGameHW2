using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float countdownTime;
    public Text countdownText;
    
    public event Action onTimerFinished;

    private Coroutine timerCoroutine;
    

    

    public void StartTimer(float startTime)
    {
        // stops any running timers
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        countdownTime = startTime;
        timerCoroutine = StartCoroutine(TimerCountdown());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void ResetTimer(float newTime)
    {
        StopTimer();
        countdownTime = newTime;
        UpdateTimerDisplay();
        timerCoroutine = StartCoroutine(TimerCountdown());
    }
    
    private IEnumerator TimerCountdown()
    {
        while (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }

        countdownTime = 0;
        UpdateTimerDisplay();
        timerCoroutine = null;
        onTimerFinished?.Invoke();
    }

    public void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
