using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        Debug.Log("SceneLoader called: " + sceneIndex);
        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("MainMenu");
                break;
            case 1:
                SceneManager.LoadScene("Game");
                break;
            case 2:
               SceneManager.LoadScene("ResultsScreen");
                break;
            case 3:
                SceneManager.LoadScene("GameSelectionScene");
                break;
            default:
                Debug.LogWarning("Invalid scene index provided.");
                break;
        }
    }
}