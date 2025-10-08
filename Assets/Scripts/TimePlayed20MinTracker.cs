using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "Ach_TimePlayed_20min", menuName = "Achievements/Time Played (20 min)")]
public class TimePlayed20MinAchievement : Achievement
{
    [SerializeField] private int thresholdSeconds = 20 * 60;   // 1200
    private const string SuffixUnlocked = "_UNLOCKED";
    private const string SuffixSeconds  = "_SECONDS";

    private int _secondsPlayed;
    private bool _isUnlocked;

    public override string AchievementTitle => "Time Played — 20 Minutes";

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
        if (_isUnlocked) return;

        _secondsPlayed++;
        Save(); // persist progress

        if (_secondsPlayed >= thresholdSeconds)
        {
            _isUnlocked = true;
            Save();
            GetAchievement(); // notifies via AchievementEvents.OnAchievementGet
            Debug.Log($"{AchievementTitle} Achieved!");
        }
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(AchievementSaveKey + SuffixSeconds, _secondsPlayed);
        PlayerPrefs.SetInt(AchievementSaveKey + SuffixUnlocked, _isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public override void Load()
    {
        _secondsPlayed = PlayerPrefs.GetInt(AchievementSaveKey + SuffixSeconds, 0);
        _isUnlocked    = PlayerPrefs.GetInt(AchievementSaveKey + SuffixUnlocked, 0) == 1;
    }
}