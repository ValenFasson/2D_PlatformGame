using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI (TextMeshPro)")]
    public TMP_Text scoreText;                  
    public TMP_Text queueText;                  

    [Header("Configuración")]
    public bool processOnSceneChange = true;    

    [Header("Auto-destrucción en escenas")]
    [SerializeField] string defeatSceneName = "DefeatScene";
    [SerializeField] string winSceneName = "WinScene";
    [SerializeField] string mainMenuSceneName = "MainMenu";
    [SerializeField] string bootstrapSceneName = "Bootstrap";

    public static ScoreManager instance;

    // Cola FIFO de valores (monedas positivas, multiplicadores como negativos: -2 == x2)
    Queue<int> coinValues = new Queue<int>();

    // Puntaje total acumulado (solo cambia al procesar la cola)
    int totalScore = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        TrySelfDestructForScene(SceneManager.GetActiveScene());
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCoinValue(int value)
    {
        if (value <= 0)
        {
            return;
        }

        if (coinValues.Count == 0)
        {
            coinValues.Enqueue(value);
        }
        else
        {
            // Intentar sumar con la última moneda si la cola termina en moneda
            List<int> tempList = new List<int>(coinValues);
            int lastIndex = tempList.Count - 1;
            if (tempList[lastIndex] > 0)
            {
                tempList[lastIndex] += value;
                coinValues.Clear();
                for (int i = 0; i < tempList.Count; i++)
                {
                    coinValues.Enqueue(tempList[i]);
                }
            }
            else
            {
                coinValues.Enqueue(value);
            }
        }

        UpdateUI();
    }

    // Añade un multiplicador a la cola (como negativo: -2 = x2)
    public void AddMultiplier(int factor = 2)
    {
        if (factor < 2)
        {
            return;
        }

        coinValues.Enqueue(-Mathf.Abs(factor));

        UpdateUI();
    }

    // Procesa SOLO la cola actual y suma su resultado al total.
    // Los multiplicadores afectan únicamente al subtotal de esta cola.
    public int ProcessQueue()
    {
        int sessionTotal = 0; // subtotal de la cola actual

        foreach (int token in coinValues)
        {
            if (token >= 0)
            {
                sessionTotal += token;
            }
            else
            {
                int factor = Mathf.Abs(token);
                sessionTotal *= factor;
            }
        }

        totalScore += sessionTotal; // sumar el resultado de la cola al total acumulado de antes
        coinValues.Clear();

        UpdateUI();
        return totalScore;
    }

    // Hook de cambio de escena
    void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        if (IsAutoDestroyScene(newScene.name))
        {
            UnloadBootstrapIfLoaded();
            Destroy(gameObject);
            return;
        }

        if (!processOnSceneChange) return;

        if (coinValues.Count > 0)
        {
            ProcessQueue();
        }
        else
        {
            UpdateUI();
        }

    }

    // Procesa el puntaje antes de cambiar de escena
    public void ProcessScoreBeforeSceneChange()
    {
        if (coinValues.Count > 0)
        {
            ProcessQueue();
        }
        else
        {
            UpdateUI();
        }
    }

    // Obtiene el puntaje total
    public int GetTotalScore()
    {
        return totalScore;
    }

    // Obtiene el número de valores en la cola
    public int GetQueueCount()
    {
        return coinValues.Count;
    }

    // Actualiza la UI
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalScore}";
        }

        if (queueText != null)
        {
            string queueDisplay = "Queue: ";
            if (coinValues.Count == 0)
            {
                queueDisplay += "Vacía";
            }
            else
            {
                List<string> displayValues = new List<string>();
                foreach (int value in coinValues)
                {
                    if (value < 0)
                    {
                        displayValues.Add($"x{Mathf.Abs(value)}");
                    }
                    else
                    {
                        displayValues.Add(value.ToString());
                    }
                }
                queueDisplay += string.Join(", ", displayValues);
            }
            queueText.text = queueDisplay;
        }
    }

    //Autodestrucción
    bool IsAutoDestroyScene(string sceneName)
    {
        return sceneName == mainMenuSceneName || sceneName == defeatSceneName || sceneName == winSceneName;
    }

    void TrySelfDestructForScene(Scene scene)
    {
        if (IsAutoDestroyScene(scene.name))
        {
            UnloadBootstrapIfLoaded();
            Destroy(gameObject);
        }
    }

    void UnloadBootstrapIfLoaded()
    {
        if (string.IsNullOrEmpty(bootstrapSceneName)) return;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sc = SceneManager.GetSceneAt(i);
            if (sc.isLoaded && sc.name == bootstrapSceneName)
            {
                SceneManager.UnloadSceneAsync(sc);
                break;
            }
        }
    }
}