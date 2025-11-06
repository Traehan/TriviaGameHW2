using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResultsScreenUI : MonoBehaviour
{
    public Text resultsText;
    [FormerlySerializedAs("sceneManager")] public SceneLoader sceneLoader;

    private void Start()
    {
        var state = GameSession.State;
        resultsText.text = $"Score: {state.CorrectAnswerCount}/{state.MaxQuestions}";
    }

    public void OnClickRestart()
    {
        GameSession.State.Reset();
        sceneLoader.LoadSceneByIndex(1); // Game
    }

    public void OnClickMainMenu()
    {
        sceneLoader.LoadSceneByIndex(0); // Main Menu
    }
}