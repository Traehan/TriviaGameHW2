using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    
    public GameObject StartScreen;
    public GameObject TriviaGame;
    public GameObject OptionsPanel;
    public GameObject CountDownPanel;
    public GameObject GoPanel;
    public GameObject AchievementsPanel;
    public GameObject StartPanel;
    public Button startButton;   
    public CountDown startTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // make sure we only subscribe once
        if (startTimer != null)
        {
            startTimer.onCountdownFinished -= HandleCountdownDone;
            startTimer.onCountdownFinished += HandleCountdownDone;
        }
        
    }

    public void OnButtonClickStart()
    {
        CountDownPanel.SetActive(true);
        startTimer.StartCountdown(5f);
    }

    public void OnButtonClickAchievements()
    {
        StartPanel.SetActive(false);
        AchievementsPanel.SetActive(true);
    }

    public void OnButtonClickBack()
    {
        AchievementsPanel.SetActive(false);
        StartPanel.SetActive(true);
    }
    
    private void HandleCountdownDone()
    {
        StartCoroutine(ShowGoThenStart());
    }
    
    private System.Collections.IEnumerator ShowGoThenStart()
    {
        // show "GO" for ~1s
        CountDownPanel.SetActive(false);
        GoPanel.SetActive(true);
        yield return new WaitForSeconds(1f);       // or WaitForSecondsRealtime(1f)
        GoPanel.SetActive(false);

        // move to game UI
        CountDownPanel.SetActive(false);
        StartScreen.SetActive(false);
        TriviaGame.SetActive(true);
        OptionsPanel.SetActive(true);
    }

    public void OnButtonClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode(); // or: UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit(); // quits the built game
#endif
    }
}
