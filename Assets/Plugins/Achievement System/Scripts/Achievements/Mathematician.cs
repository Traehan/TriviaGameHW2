using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/" + nameof(Mathematician), fileName = nameof(Mathematician))]
public class Mathematician : Achievement
{
    [SerializeField] private int thresholdSeconds = 20 * 60;   // 1200
    private const string SuffixUnlocked = "_UNLOCKED";
    private const string SuffixSeconds  = "_SECONDS";

    private int _secondsPlayed;
    

    

    public override void Subscribe()
    {
        AchievementEvents.OnSecondPassed += HandleSecondPassed;  // subscribe to global tick  
    }

    public override void Unsubscribe()
    {
        AchievementEvents.OnSecondPassed -= HandleSecondPassed;  // unsubscribe
    }

    private void HandleSecondPassed()
    {

        _secondsPlayed++;
        Save(); // persist progress

        if (_secondsPlayed >= thresholdSeconds)
        {
            Save();
            GetAchievement(); // notifies via AchievementEvents.OnAchievementGet
            Debug.Log($"{AchievementTitle} Achieved!");
        }
    }

    
}
