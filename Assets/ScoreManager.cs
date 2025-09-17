using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text scoreText;        // Texto para mostrar puntaje actual
    public TMP_Text queueText;        // Texto para mostrar cola de valores

    [Header("Configuración")]
    public bool processOnSceneChange = true;    // Procesar al cambiar de escena

    // Singleton del manager de puntaje
    public static ScoreManager instance;

    // Cola FIFO de valores (monedas se suman, multiplicadores se añaden como elementos)
    Queue<int> coinValues = new Queue<int>();

    // Puntaje total actual
    int totalScore = 0;

    void Awake()
    {
        // Garantiza una única instancia
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    // Añade un valor de moneda a la cola (se suma al último valor si es una moneda)
    public void AddCoinValue(int value)
    {
        // Si la cola está vacía, añadir nueva moneda
        if (coinValues.Count == 0)
        {
            coinValues.Enqueue(value);
        }
        else
        {
            // Verificar si el último elemento es una moneda (valor positivo)
            // Necesitamos reconstruir la cola para verificar el último elemento
            List<int> tempList = new List<int>(coinValues);
            if (tempList.Count > 0 && tempList[tempList.Count - 1] > 0)
            {
                // El último elemento es una moneda, sumar
                tempList[tempList.Count - 1] += value;

                // Reconstruir la cola
                coinValues.Clear();
                foreach (int val in tempList)
                {
                    coinValues.Enqueue(val);
                }
            }
            else
            {
                // El último elemento es un multiplicador o no hay elementos, añadir nueva moneda
                coinValues.Enqueue(value);
            }
        }

        // Calcular puntaje en tiempo real
        CalculateRealTimeScore();

        Debug.Log($"Moneda añadida: {value}. Cola actual: {string.Join(", ", coinValues.ToArray())}");
        UpdateUI();
    }

    // Añade un multiplicador a la cola (se añade como elemento negativo para identificarlo)
    public void AddMultiplier()
    {
        // Los multiplicadores se representan como valores negativos en la cola
        coinValues.Enqueue(-2); // -2 representa multiplicador x2
        Debug.Log($"Multiplicador añadido a la cola: x2");

        // Calcular puntaje en tiempo real
        CalculateRealTimeScore();
        UpdateUI();
    }

    // Calcula el puntaje en tiempo real basado en la cola actual
    void CalculateRealTimeScore()
    {
        if (coinValues.Count == 0)
        {
            totalScore = 0;
            return;
        }

        // Convertir cola a lista para procesar
        List<int> values = new List<int>(coinValues);

        // Calcular usando paréntesis anidados
        int result = CalculateNestedExpression(values, 0, values.Count - 1);
        totalScore = result;
    }

    // Calcula expresión con paréntesis anidados recursivamente
    int CalculateNestedExpression(List<int> values, int start, int end)
    {
        if (start > end) return 0;
        if (start == end) return values[start] < 0 ? 0 : values[start];

        // Buscar el primer multiplicador desde el final
        int multiplierIndex = -1;
        for (int i = end; i >= start; i--)
        {
            if (values[i] < 0)
            {
                multiplierIndex = i;
                break;
            }
        }

        if (multiplierIndex == -1)
        {
            // No hay multiplicadores, solo sumar valores
            int sum = 0;
            for (int i = start; i <= end; i++)
            {
                if (values[i] > 0) sum += values[i];
            }
            return sum;
        }

        // Hay multiplicador, dividir en dos partes
        int leftResult = CalculateNestedExpression(values, start, multiplierIndex - 1);
        int rightResult = CalculateNestedExpression(values, multiplierIndex + 1, end);

        // Aplicar multiplicador
        int multiplier = Mathf.Abs(values[multiplierIndex]);
        return (leftResult + rightResult) * multiplier;
    }

    // Procesa la cola completa (ya no es necesario, se calcula en tiempo real)
    public void ProcessQueue()
    {
        // Ya no es necesario procesar, se calcula en tiempo real
        Debug.Log($"Puntaje final: {totalScore}");
    }

    // Procesa el puntaje antes de cambiar de escena
    public void ProcessScoreBeforeSceneChange()
    {
        // Ya no es necesario procesar, se calcula en tiempo real
        Debug.Log($"Puntaje final al cambiar de escena: {totalScore}");
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
            scoreText.text = $"Puntaje: {totalScore}";
        }

        if (queueText != null)
        {
            string queueDisplay = "Cola: ";
            if (coinValues.Count == 0)
            {
                queueDisplay += "Vacía";
            }
            else
            {
                // Mostrar cola con multiplicadores como "x2"
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

    // Para debug: muestra estado actual
    [ContextMenu("Mostrar Estado")]
    void ShowState()
    {
        Debug.Log($"=== Estado del ScoreManager ===");
        Debug.Log($"Puntaje total: {totalScore}");
        Debug.Log($"Valores en cola: {string.Join(", ", coinValues.ToArray())}");
        Debug.Log($"Cantidad en cola: {coinValues.Count}");
    }
}