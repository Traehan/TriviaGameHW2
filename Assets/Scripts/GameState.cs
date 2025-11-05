using UnityEngine;

public class GameState
{
    public int QuestionCount { get; private set; }
    public int CorrectAnswerCount { get; private set; }
    public float TotalTimeTaken { get; private set; }

    public Question CurrentQuestion { get; private set; }
    public bool RoundActive { get; private set; }

    public int MaxQuestions = 3;
    public float PerQuestionTime = 10f;

    public void Reset()
    {
        QuestionCount = 0;
        CorrectAnswerCount = 0;
        TotalTimeTaken = 0f;
        CurrentQuestion = default;
        RoundActive = false;
    }

    public void BeginRound()  { RoundActive = true; }
    public void EndRound()    { RoundActive = false; }

    public void NextQuestion(Question q)
    {
        CurrentQuestion = q;
        RoundActive = true;
    }

    public void RecordAnswer(bool correct, float timeRemaining)
    {
        if (RoundActive) // only record if we're mid-round
        {
            if (correct) CorrectAnswerCount++;
            TotalTimeTaken += Mathf.Clamp(PerQuestionTime - timeRemaining, 0f, PerQuestionTime);
            QuestionCount++;
        }
    }


    public bool HasMoreQuestions() => QuestionCount < MaxQuestions;

    public struct Question
    {
        public string Prompt;
        public int CorrectIndex;   // 0..2
        public string[] Choices;   // length 3
    }
}