using UnityEngine;

[CreateAssetMenu(fileName = "Ach_ClickQuestionOnce", menuName = "Achievements/Click Question Once")]
public class ClickQuestionAchievement : Achievement
{
    private const string Suffix = "_UNLOCKED";
    private bool _unlocked;

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
        if (_unlocked) return;

        _unlocked = true;
        Save();
        Debug.Log("Achievement unlocked: Just Click It");
        GetAchievement(); // fires your global OnAchievementGet etc.
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(AchievementSaveKey + Suffix, _unlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public override void Load()
    {
        _unlocked = PlayerPrefs.GetInt(AchievementSaveKey + Suffix, 0) == 1;
    }
}
