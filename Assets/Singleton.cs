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
    public Pila pila;

    public float TimerCounter;
    private float maxTime = 30;

    public string playerName;
    public int CurrentScore;
    public int pilaScore;

    void Awake()
    {
        pila = new Pila();
        pila.InicializarPila();
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
            pilaScore = 0;
            DesapilarResultado();
            CurrentScore += pilaScore;
            CurrentScore += Mathf.FloorToInt(TimerCounter);
            TimerCounter = maxTime;
        }

        if (newName == "ScoreBoard")
        {
            if (QuickInfo.QSinfo != null)
                QuickInfo.QSinfo.newPlayer(playerName, CurrentScore);

            var manager = FindObjectOfType<GameMementoManager>();
            if (manager != null)
                manager.ClearMemento();

            playerName = "";
            CurrentScore = 0;
            return;
        }
    }

    private void DesapilarResultado()
    {
        while (!pila.PilaVacia())
        {
            Operation container;
            container = pila.Primero();
            switch (container.op)
            {
                case "suma":
                    pilaScore += container.amount;
                    break;
                case "mult":
                    pilaScore *= container.amount;
                    break;
            }
            pila.Desapilar();
        }
    }
}
