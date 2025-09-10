using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime;
    public Text countdownText;
    

    // Update is called once per frame
    void Update()
    {
        TimerCountdown();
        if (countdownTime <= 0)
        {
            countdownText.text = "00:00";
            // display a screen that moves on to the next question
        }
    }

    public void TimerCountdown()
    {
        countdownTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
