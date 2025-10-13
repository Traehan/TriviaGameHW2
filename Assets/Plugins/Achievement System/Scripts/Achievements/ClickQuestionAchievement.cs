using UnityEngine;

[CreateAssetMenu(fileName = "Ach_ClickQuestionOnce", menuName = "Achievements/Click Question Once")]
public class ClickQuestionAchievement : Achievement
{
    private const string Suffix = "_UNLOCKED";

    public override string AchievementTitle => "Huh???";

    public override void Subscribe()
    {
        AchievementEvents.OnQuestionClicked += HandleClicked;
    }

    public override void Unsubscribe()
    {
        AchievementEvents.OnQuestionClicked -= HandleClicked;
    }

    private void HandleClicked()
    {
        Save();
        Debug.Log("Achievement unlocked: Just Click It");
        GetAchievement(); // fires your global OnAchievementGet etc.
    }

    
}
