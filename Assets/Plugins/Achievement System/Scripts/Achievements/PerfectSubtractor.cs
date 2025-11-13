using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(PerfectSubtractor), fileName = nameof(PerfectSubtractor))]

public class PerfectSubtractor : Achievement
{
    public override void Subscribe()
    {
        AchievementEvents.OnRoundEnded += OnRoundEnded;
    }
    public override void Unsubscribe()
    {
        AchievementEvents.OnRoundEnded -= OnRoundEnded;
    }

    private void OnRoundEnded(AchievementEvents.OnRoundEndedArgs obj)
    {
        if (obj.CurrentMode == ModeKind.Subtraction && obj.NumCorrectQuestions == obj.NumQuestionsAnswered)
            GetAchievement();
    }
}
