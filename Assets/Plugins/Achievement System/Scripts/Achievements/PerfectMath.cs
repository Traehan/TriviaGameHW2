using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(PerfectMath), fileName = nameof(PerfectMath))]

public class PerfectMath : Achievement
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
        if (obj.CurrentMode == ModeKind.All && obj.NumCorrectQuestions == obj.NumQuestionsAnswered)
            GetAchievement();
    }
}
