using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    static public Singleton instance;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI timerText;

    [SerializeField] private float TimerCounter;
    private float maxTime = 30;

    public string playerName;
    public int CurrentScore;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        TimerCounter = maxTime;
        instance = this;
        SceneManager.activeSceneChanged += OnSceneChanged;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name != "ScoreBoard")
        {
            scoreText.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            scoreText.text = CurrentScore.ToString();
            timerText.text = TimerCounter.ToString("f1");
            TimerCounter -= Time.deltaTime;
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
        }
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        string newName = newScene.name;

        if (newName == "Bootstrap")
        {
            TimerCounter = maxTime;
            return;
        }

        if (oldScene.name != "Bootstrap" && oldScene.name != "ScoreBoard")
        {
            CurrentScore += Mathf.FloorToInt(TimerCounter);
            TimerCounter = maxTime;
        }

        if (newName == "ScoreBoard")
        {
            if (QuickInfo.QSinfo != null)
                QuickInfo.QSinfo.newPlayer(playerName, CurrentScore);

            playerName = "";
            CurrentScore = 0;
            return;
        }
    }
}
