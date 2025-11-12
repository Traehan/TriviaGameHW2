using UnityEngine;

public class OnQuestionClickedBroadcast : MonoBehaviour
{
    public void onQuestionClicked()
    {
        AchievementEvents.OnQuestionClicked();
        Debug.Log("[Button] Question clicked - huh? why?");
    }
}
