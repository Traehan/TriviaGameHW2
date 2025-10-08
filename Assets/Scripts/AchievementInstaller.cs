using UnityEngine;

public class AchievementInstaller : MonoBehaviour
{
    [SerializeField] private Achievement[] achievements;

    private void Awake()
    {
        if (achievements == null) return;
        foreach (var a in achievements)
        {
            if (a == null) continue;
            a.Load();       // resume saved progress:contentReference
            a.Subscribe();  // start listening to events:contentReference
        }
    }

    private void OnDestroy()
    {
        if (achievements == null) return;
        foreach (var a in achievements)
        {
            if (a == null) continue;
            a.Unsubscribe(); // tidy up  :contentReference
            a.Save();        // persist on exit  :contentReference
        }
    }
}