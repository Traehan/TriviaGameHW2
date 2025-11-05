using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public float countdownTime;
    public Text countdownText;
    
    public event Action onCountdownFinished;

    private Coroutine timerCoroutine;
    

    

    public void StartCountdown(float startTime)
    {
        StopCountdown();
        countdownTime = startTime;
        UpdateCountdownDisplay();
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
            //call from new class and increment delta time to overall playTime achievement
            UpdateCountdownDisplay();
            // this checks every frame. We tried to do every second but then it stopped working
            yield return null;
        }
        
        countdownTime = 0;
        UpdateCountdownDisplay();
        
        // makes sure that when the countdown ends, it stays as 0, updates the display, ends the coroutine
        // finished
        timerCoroutine = null;

        // fire event first so UI can show "GO"
        onCountdownFinished?.Invoke();
    }

    public void UpdateCountdownDisplay()
    {
        int total = Mathf.CeilToInt(countdownTime); // keeps "1" up to the end
        total = Mathf.Max(total, 1);                // never display 0 during the loop

        int minutes = total / 60;
        int seconds = total % 60;
        countdownText.text = $"{minutes:00}:{seconds:00}";
    }
}
