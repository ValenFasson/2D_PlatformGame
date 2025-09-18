using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    // Volver al menú principal (asegúrate de que el menú esté en Build Settings)
    // Puedes cambiar el nombre "MainMenu" si tu escena se llama distinto
    [SerializeField] string mainMenuSceneName = "MainMenu";

    // Cargar el menú principal por nombre
    public void LoadMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogWarning("mainMenuSceneName no está configurado en el Inspector.");
        }
    }

    // Cargar la escena 1 (índice 1 en Build Settings)
    public void LoadScene1()
    {
        const int sceneIndex = 1;
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("La escena 1 no está configurada en Build Settings.");
        }
    }

    // Salir del juego (funciona en build; en editor detiene el Play)
    public void QuitGame()
    {
        Application.Quit();
    }
}