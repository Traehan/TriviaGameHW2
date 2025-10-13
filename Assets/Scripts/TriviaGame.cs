using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TriviaGame : MonoBehaviour
{
    public GameObject UI;
    public GameObject TriviaPanel;
    public GameObject ResultPanel;
    public CountDown roundTimer;
    public Text Question;
    public Text Answer_1, Answer_2, Answer_3;
    public Text ResultsText;

    private int questionCount = 0;
    private int CorrectAnswerCount = 0;
    private float totalTimeTaken = 0;
    private bool answer_1, answer_2, answer_3;
    private bool roundActive = false;

    private Coroutine _watch; // <— watcher for “when time <= 1”

    void Start()
    {
        questionCount = 0;
        CorrectAnswerCount = 0;
        StartRound();
    }

    void StartRound()
    {
        // kill any previous watcher
        if (_watch != null)
        {
            StopCoroutine(_watch);
            _watch = null;
        }

        FillQuestion();
        roundActive = true;

        roundTimer.StartCountdown(10f);
        _watch = StartCoroutine(WaitUntilZeroThenAdvance());
    }

    IEnumerator WaitUntilZeroThenAdvance()
    {
        // advance when the timer shows 1 (or below), not 0
        while (roundActive && roundTimer.countdownTime > 0f)
            yield return null;

        if (roundActive) EndRound();
    }

    void EndRound()
    {
        if (!roundActive) return;
        roundActive = false;

        if (_watch != null)
        {
            StopCoroutine(_watch);
            _watch = null;
        } // stop watcher

        roundTimer.StopCountdown(); // stop timer

        questionCount++;
        if (questionCount < 3) StartRound();
        else ShowResults();
    }

    void ShowResults()
    {
        TriviaPanel.SetActive(false);
        ResultPanel.SetActive(true);
        ResultsText.text = $"Score: {CorrectAnswerCount}/3";
        AchievementEvents.OnRoundEnded?.Invoke(new AchievementEvents.OnRoundEndedArgs
        {
            NumCorrectQuestions = CorrectAnswerCount,
            NumQuestionsAnswered = questionCount,
            TotalTimeTaken = totalTimeTaken 
        });
        
    }

    void FillQuestion()
    {
        int x = Random.Range(1, 13);
        int y = Random.Range(1, 13);
        int product = x * y;
        int tinkerNumber = Random.Range(1, 8);

        Question.text = $"{x}x{y}?";

        answer_1 = answer_2 = answer_3 = false;
        int slot = Random.Range(1, 4); // 1..3

        if (slot == 1)
        {
            Answer_1.text = product.ToString();
            Answer_2.text = (product + tinkerNumber).ToString();
            Answer_3.text = (product - tinkerNumber).ToString();
            answer_1 = true;
        }
        else if (slot == 2)
        {
            Answer_1.text = (product + tinkerNumber).ToString();
            Answer_2.text = product.ToString();
            Answer_3.text = (product - tinkerNumber).ToString();
            answer_2 = true;
        }
        else
        {
            Answer_1.text = (product - tinkerNumber).ToString();
            Answer_2.text = (product + tinkerNumber).ToString();
            Answer_3.text = product.ToString();
            answer_3 = true;
        }
    }
    public void OnAnswerClick(int answerIndex)
    {
        // Check if the selected answer is correct
        if ((answerIndex == 1 && answer_1) ||
            (answerIndex == 2 && answer_2) ||
            (answerIndex == 3 && answer_3))
        {
            CorrectAnswerCount++;
            AchievementEvents.OnQuestionAnswered?.Invoke(new AchievementEvents.OnQuestionAnsweredArgs
            {
                AnsweredCorrectly = true,
                TimeRemaining = roundTimer.countdownTime
            });
        }
        else
        {
            AchievementEvents.OnQuestionAnswered?.Invoke(new AchievementEvents.OnQuestionAnsweredArgs
            {
                AnsweredCorrectly = false,
                TimeRemaining = roundTimer.countdownTime
            });
        }

        totalTimeTaken += 10 - (int)roundTimer.countdownTime;
        EndRound();
    }
    


    // restart from results
    public void OnClickRestartRound()
    {
        UI.SetActive(false);
        ResultPanel.SetActive(false);
        TriviaPanel.SetActive(true);
        UI.SetActive(true);
        questionCount = 0;
        CorrectAnswerCount = 0;
        totalTimeTaken = 0f;
        StartRound();
    }

}
