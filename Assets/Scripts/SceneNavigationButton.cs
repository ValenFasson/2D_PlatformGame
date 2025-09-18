using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigationButton : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No hay una siguiente escena en Build Settings.");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}