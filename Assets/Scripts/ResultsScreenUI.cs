using UnityEngine;
using UnityEngine.UI;

public class ResultsScreenUI : MonoBehaviour
{
    public Text resultsText;
    public SceneManager sceneManager;

    private void Start()
    {
        var state = GameSession.Instance.CurrentState;
        resultsText.text = $"Score: {state.CorrectAnswerCount}/{state.MaxQuestions}";
    }

    public void OnClickRestart()
    {
        GameSession.Instance.ResetState();
        sceneManager.LoadSceneByIndex(1); // Game
    }

    public void OnClickMainMenu()
    {
        sceneManager.LoadSceneByIndex(0); // Main Menu
    }
}