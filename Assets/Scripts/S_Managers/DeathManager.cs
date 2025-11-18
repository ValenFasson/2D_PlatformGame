using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public string scoreSceneName = "ScoreBoard";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        GameEvents.OnPlayerDeath += OnDeath;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerDeath -= OnDeath;
    }

    void OnDeath()
    {
        Time.timeScale = 1f;

        var stack = FindObjectOfType<SceneStackManager>();
        if (stack != null)
            stack.isReturn = false;

        SceneManager.LoadScene(scoreSceneName);
    }
}
