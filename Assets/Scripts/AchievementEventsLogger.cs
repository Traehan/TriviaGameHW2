using UnityEngine;

public class AchievementEventsLogger : MonoBehaviour
{
    private void Awake()
    {
        AchievementEvents.OnQuestionAnswered += (AchievementEvents.OnQuestionAnsweredArgs args) =>
        {
            string correctText = args.AnsweredCorrectly ? "correctly" : "incorrectly";
            Debug.Log($"Question Answered! Answered {correctText} with {args.TimeRemaining} seconds remaining!");
        };

        AchievementEvents.OnRoundEnded += (AchievementEvents.OnRoundEndedArgs args) =>
        {
            string questionsCorrectRatio = $"{args.NumCorrectQuestions} / {args.NumQuestionsAnswered}";
            Debug.Log($"Round Ended! Answered {questionsCorrectRatio} questions correctly in a total of {args.TotalTimeTaken} seconds!");
        };

        AchievementEvents.OnQuestionClicked += () =>
        {
            Debug.Log("Huh?");
        };
    }
}
