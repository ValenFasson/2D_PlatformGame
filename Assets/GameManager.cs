using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string defeatSceneName = "DefeatScene";
    [SerializeField] private string winSceneName = "WinScene";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Llamado por el jugador cuando muere
    public void PlayerDied()
    {
        LoadDefeatScene();
    }

    public void PlayerWon()
    {
        LoadWinScene();
    }

    private void LoadDefeatScene()
    {
        if (string.IsNullOrWhiteSpace(defeatSceneName))
        {
            return;
        }
        SceneManager.LoadScene(defeatSceneName);
    }

    private void LoadWinScene()
    {
        if (string.IsNullOrWhiteSpace(winSceneName))
        {
            return;
        }
        SceneManager.LoadScene(winSceneName);
    }

}