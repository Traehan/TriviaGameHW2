using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime;
    public Text countdownText;

    private bool isRunning;
    

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            TimerCountdown();
        }
    }

    public void TimerCountdown()
    {
        //counts down the timer
        countdownTime -= Time.deltaTime;
        if (countdownTime <= 0)
        {
            countdownTime = 0;
            isRunning = false;
            //here is where you can call a method that marks the question wrong and goes to the next one
        }

        UpdateTimerDisplay();
    }

    public void StartTimer(float startTime)
    {
        countdownTime = startTime;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer(float newTime)
    {
        countdownTime = newTime;
        isRunning = true;
        UpdateTimerDisplay();
    }

    public void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
