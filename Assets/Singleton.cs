using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    static Singleton instance;
    [SerializeField] public TextMeshProUGUI scoreText;
    public int CurrentScore;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        scoreText.text = CurrentScore.ToString();
    }
}
