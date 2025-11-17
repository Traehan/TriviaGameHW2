using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResultsScreenUI : MonoBehaviour
{
    public Text resultsText;

    private void Start()
    {
        var state = GameSession.State;
        resultsText.text = $"Score: {state.CorrectAnswerCount}/{state.MaxQuestions}";
    }

    public void OnClickRestart()
    {
        GameSession.State.Reset();
        AppStateController.Instance.GoToSelection();
    }

    public void OnClickMainMenu()
    {
        AppStateController.Instance.GoToStart(); // Main Menu
    }
}