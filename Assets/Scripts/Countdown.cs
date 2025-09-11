using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Countdown : MonoBehaviour
{
    public float countdownTime;
    public Text countdownText;
    
    public event Action onCountdownFinished;

    private Coroutine timerCoroutine;
    

    

    public void StartCountdown(float startTime)
    {
        // stops any running timers
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        
        // sets the time of the countdown and runs the coroutine
        countdownTime = startTime;
        timerCoroutine = StartCoroutine(CountdownCoroutine());
    }

    public void StopCountdown()
    {
        // if there is a timer running, stops the coroutine and makes it null
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    // This is never used, but could be should a future project with a countdown needs it
    public void ResetCountdown(float newTime)
    {
        StopCountdown();
        countdownTime = newTime;
        UpdateCountdownDisplay();
        timerCoroutine = StartCoroutine(CountdownCoroutine());
    }
    
    private IEnumerator CountdownCoroutine()
    {
        // While the countdown time is a number, it counts to 0
        while (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            UpdateCountdownDisplay();
            // this checks every frame. We tried to do every second but then it stopped working
            yield return null;
        }
        
        // makes sure that when the countdown ends, it stays as 0, updates the display, ends the coroutine
        countdownTime = 0;
        UpdateCountdownDisplay();
        timerCoroutine = null;
        
        // this invokes an event other classes can use to do something when the countdown ends
        onCountdownFinished?.Invoke();
    }

    public void UpdateCountdownDisplay()
    {
        // formats the countdown in minutes and seconds
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
