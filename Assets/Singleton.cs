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
    private float maxTime = 30; // este es el tiempo de cada escena
    //private bool firstSceneLoaded = false;

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
        scoreText.text = CurrentScore.ToString();
        timerText.text = TimerCounter.ToString("f1");
        TimerCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Escena actual: " + SceneManager.GetActiveScene().name);
        }
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene) //una funcion propia de una libreria
    {
        //Debug.Log($"Cambiamos de escena: {oldScene.name} A la escena {newScene.name}");
        if (SceneManager.GetActiveScene().name == "Bootstrap")
        { 
            return;
        }
        else if(SceneManager.GetActiveScene().name == "ScoreBoard") 
        {
            QuickInfo.QSinfo.newPlayer(playerName, CurrentScore); // guardamos la info aca
            playerName = "";
            CurrentScore = 0; // y reseteamos
        }
            //Aca se puede ajustar todas las variables que quiero que se reinicien en cada cambio de escena
            CurrentScore += Mathf.FloorToInt(TimerCounter);
            TimerCounter = maxTime;
    }
}
