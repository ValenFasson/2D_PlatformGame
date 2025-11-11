using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    static Singleton instance;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI timerText;
    public int CurrentScore;
    [SerializeField] private float TimerCounter;
    private float maxTime = 30; // este es el tiempo de cada escena
    private bool firstSceneLoaded = false;
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
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene) //una funcion propia de una libreria
    {
        Debug.Log($"Cambiamos de escena: {oldScene.name} A la escena {newScene.name}");

        if (!firstSceneLoaded)
        {
            firstSceneLoaded = true;
            return;
        }
        //Aca se puede ajustar todas las variables que quiero que se reinicien en cada cambio de escena
        CurrentScore += Mathf.FloorToInt(TimerCounter);
        TimerCounter = maxTime;
    }
}
