using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(PerfectDivider), fileName = nameof(PerfectDivider))]

public class PerfectDivider : Achievement
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
        if (obj.CurrentMode == ModeKind.Division && obj.NumCorrectQuestions == obj.NumQuestionsAnswered)
            GetAchievement();
    }
}
