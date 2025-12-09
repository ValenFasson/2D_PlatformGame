using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMementoManager : MonoBehaviour
{
    public static GameMementoManager Instance;

    public bool IsRestoring;

    private GameMemento savedMemento;
    private PlayerOriginator playerOriginator;
    private GameStatsOriginator statsOriginator;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasMemento => savedMemento != null;

    public void SaveCheckpoint()
    {
        if (playerOriginator == null)
        {
            playerOriginator = FindObjectOfType<PlayerOriginator>();
        }

        if (statsOriginator == null)
        {
            statsOriginator = FindObjectOfType<GameStatsOriginator>();
        }

        if (playerOriginator == null || statsOriginator == null)
        {
            return;
        }

        savedMemento = new GameMemento(
            SceneManager.GetActiveScene().name,
            playerOriginator.SaveState(),
            statsOriginator.SaveState()
        );
    }

    public void RestoreFromDamage()
    {
        if (savedMemento == null)
        {
            return;
        }

        if (IsRestoring)
        {
            return;
        }

        IsRestoring = true;

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != savedMemento.SceneName)
        {
            SceneManager.LoadScene(savedMemento.SceneName);
            StartCoroutine(RestoreAfterSceneLoad());
            return;
        }

        RestoreInCurrentScene();
        StartCoroutine(EndRestoreFlag());
    }

    void RestoreInCurrentScene()
    {
        if (playerOriginator == null)
        {
            playerOriginator = FindObjectOfType<PlayerOriginator>();
        }

        if (statsOriginator == null)
        {
            statsOriginator = FindObjectOfType<GameStatsOriginator>();
        }

        if (playerOriginator != null)
        {
            playerOriginator.RestoreState(savedMemento.PlayerState);
        }

        if (statsOriginator != null)
        {
            statsOriginator.RestoreState(savedMemento.StatsState);
        }
    }

    IEnumerator RestoreAfterSceneLoad()
    {
        yield return null;

        playerOriginator = FindObjectOfType<PlayerOriginator>();
        statsOriginator = FindObjectOfType<GameStatsOriginator>();

        RestoreInCurrentScene();

        yield return null;
        IsRestoring = false;
    }

    IEnumerator EndRestoreFlag()
    {
        yield return null;
        IsRestoring = false;
    }

    public void ClearMemento()
    {
        savedMemento = null;
    }
}
