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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            var p = FindObjectOfType<PlayerHealth>();
            if (p != null)
            {
                p.currentHealth -= 99;
                GameEvents.PlayerDamaged(1, p.currentHealth);
                if (p.currentHealth <= 0)
                    GameEvents.PlayerDied();
            }
        }
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
