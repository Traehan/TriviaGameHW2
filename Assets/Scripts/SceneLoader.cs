using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                break;
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
                break;
            case 2:
                UnityEngine.SceneManagement.SceneManager.LoadScene("ResultsScreen");
                break;
            default:
                Debug.LogWarning("Invalid scene index provided.");
                break;
        }
    }
}