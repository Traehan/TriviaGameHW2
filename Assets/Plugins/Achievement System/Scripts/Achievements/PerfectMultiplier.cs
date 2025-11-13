using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(PerfectMultiplier), fileName = nameof(PerfectMultiplier))]

public class PerfectMultiplier : Achievement
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
        if (obj.CurrentMode == ModeKind.Multiplication && obj.NumCorrectQuestions == obj.NumQuestionsAnswered)
            GetAchievement();
    }
}
