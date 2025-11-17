using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
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
    
    private bool countdownHandled = false;
    
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
    
    private void OnEnable()
    {
        countdownHandled = false;
    }

    public void OnMainMenuClickStart()
    {
        AppStateController.Instance.GoToSelection();
    }
    
    public void OnOptionsClickStart()
    {
        OptionsPanel.SetActive(false);
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
        if (countdownHandled)
        {
            Debug.Log("[UI] HandleCountdownDone called but already handled - ignoring.");
            return;
        }

        // extra safety: check the timer's finished flag if available
        if (startTimer != null && !startTimer.HasFinished)
        {
            Debug.LogWarning("[UI] HandleCountdownDone called but startTimer.HasFinished is false. Ignoring.");
            return;
        }

        countdownHandled = true;
        Debug.Log("[UI] HandleCountdownDone - starting ShowGoThenStart coroutine.");
        StartCoroutine(ShowGoThenStart());
    }
    
    private System.Collections.IEnumerator ShowGoThenStart()
    {
        CountDownPanel.SetActive(false);
        GoPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        GoPanel.SetActive(false);

        // Load the next scene after countdown finishes and "GO" disappears
        AppStateController.Instance.GoToGame();
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
